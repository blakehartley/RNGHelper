using System.Collections.Generic;
using System.Linq;

namespace FF12RNGHelper
{
    /// <summary>
    /// The ChestRngHelper contains the necessary logic
    /// to calculate and predict the spawning and contents
    /// of chests.
    /// </summary>
    public class ChestRngHelper : BaseRngHelper
    {
        #region privates

        private readonly List<Chest> _chests;
        private ChestFutureRng _futureRng;

        #endregion privates

        #region construction/initialization

        public ChestRngHelper(PlatformType platform, CharacterGroup group,
            List<Chest> chests)
            : base(platform, group)
        {
            _chests = chests;
        }

        #endregion construction/initialization

        #region public methods

        public new ChestFutureRng GetChestFutureRng()
        {
            return _futureRng;
        }

        #endregion public methods

        #region protected methods

        protected override void CalculateRngHelper()
        {
            foreach (Chest chest in _chests)
            {
                chest.ResetSpawnInfo();
            }

            _futureRng = new ChestFutureRng();

            // Use these variables to check for first punch combo
            ComboFound = false;
            ComboPosition = -1;

            uint firstRngVal = DisplayRng.genrand();
            uint secondRngVal = DisplayRng.genrand();

            // We want to preserve the character index, since this loop is just for display:
            int indexStatic = Group.GetIndex();
            Group.ResetIndex();

            int start = GetLoopStartIndex();
            int end = start + HealVals.Count + FutureRngPositionsToCalculate;
            for (int index = start; index < end; index++)
            {
                // Index starting at 0
                LoopIndex = index - start;

                // Get the heal value once
                int currentHeal = Group.GetHealValue(firstRngVal);
                int nextHeal = Group.PeekHealValue(secondRngVal);

                // Set the next expected heal value
                if (index == start + HealVals.Count - 1 || index == 1)
                {
                    NextHealValue = nextHeal;
                }

                // Advance the RNG before starting the loop in case we want to skip an entry
                uint firstRngValTemp = firstRngVal;
                uint secondRngValTemp = secondRngVal;
                firstRngVal = secondRngVal;
                secondRngVal = DisplayRng.genrand();

                // Skip the entry if it's too long ago
                if (LoopIndex < HealVals.Count - HistoryToDisplay)
                    continue;

                //Start actually collating data
                ChestFutureRngInstance newRngInstance =
                    new ChestFutureRngInstance(_chests.Count);

                // check if we are in calculating past RNG
                if (index < start + HealVals.Count)
                {
                    newRngInstance.IsPastRng = true;
                }

                // Set useful information
                newRngInstance.Index = index;
                newRngInstance.CurrentHeal = currentHeal;
                newRngInstance.RandToPercent = Utils.RandToPercent(firstRngValTemp);

                // Handle chest spawns
                for (int i = 0; i < _chests.Count; i++)
                {
                    Chest chest = _chests.ElementAt(i);
                    ChestReward reward = newRngInstance.ChestRewards.ElementAt(i);
                    if (chest.checkSpawn(firstRngValTemp))
                    {
                        HandleChestSpawn(chest, reward);
                    }
                }

                // Handle gil and item rewards
                for (int i = 0; i < _chests.Count; i++)
                {
                    Chest chest = _chests.ElementAt(i);
                    ChestReward reward = newRngInstance.ChestRewards.ElementAt(i);
                    // Check if gil
                    if (chest.checkIfGil(firstRngValTemp))
                    {
                        HandleGilReward(chest, reward, secondRngValTemp);
                    }
                    // Handle item reward
                    else
                    {
                        HandleItemReward(chest, reward, secondRngValTemp);
                    }
                }

                // Check for combo during string of punches
                CheckForCombo(firstRngValTemp);

                _futureRng.AddNextRngInstance(newRngInstance);
            }

            WriteAdvanceDirectionsInfo();

            AttacksBeforeNextCombo = ComboPosition;

            Group.SetIndex(indexStatic);
        }

        #endregion protected methods

        #region private methods

        private void WriteAdvanceDirectionsInfo()
        {
            foreach (Chest chest in _chests)
            {
                AdvanceDirections advanceDirections = new AdvanceDirections
                {
                    AdvanceToAppear = chest.GetChestFoundPosition(),
                    AdvanceForItem = chest.GetChestItemPosition()
                };
                _futureRng.AddAdvanceDirections(advanceDirections);
            }
        }

        private void HandleChestSpawn(Chest chest, ChestReward chestReward)
        {
            int chestFirstChance = HealVals.Count + chest.getRNGPosition();

            chestReward.ChestWillSpawn = true;
            if (LoopIndex >= chestFirstChance && !chest.HasChestSpawned())
            {
                chest.SetChestFoundPosition(LoopIndex - HealVals.Count - chest.getRNGPosition());
                chest.SetChestSpawned();
            }
        }

        private void HandleGilReward(Chest chest, ChestReward chestReward, uint prng)
        {
            chestReward.Reward = RewardType.Gil;
            chestReward.GilAmount = chest.getGilAmount(prng);
        }

        private void HandleItemReward(Chest chest, ChestReward chestReward,
            uint prng)
        {
            if (chest.checkIfFirstItem(prng))
            {
                HandleItemRewardHelper(chest, chestReward,
                    RewardType.Item1, true);
            }
            else
            {
                HandleItemRewardHelper(chest, chestReward,
                    RewardType.Item2, false);
            }
        }

        private void HandleItemRewardHelper(Chest chest, ChestReward chestReward,
            RewardType reward, bool expectItemOne)
        {
            chestReward.Reward = reward;

            // Check if the items are in this position
            if (((chest.WantItemOne() == expectItemOne) &&
                 LoopIndex >= HealVals.Count) && !chest.IsChestFound())
            {
                chest.SetChestItemPosition(LoopIndex - HealVals.Count);
                chest.SetChestFound();
            }
        }

        #endregion private methods
    }
}