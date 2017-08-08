namespace FF12RNGHelper.Core
{
    /// <summary>
    /// The StealRngHelper contains the necessary logic
    /// to calculate and predict the outcome of stealing.
    /// </summary>
    public class StealRngHelper : BaseRngHelper
    {
        #region privates

        private StealFutureRng _futureRng;

        #endregion privates

        #region construction/initialization

        public StealRngHelper(PlatformType platform, CharacterGroup group)
            : base(platform, group)
        {
        }

        #endregion construction/intialization

        #region public methods 

        public new StealFutureRng GetStealFutureRng()
        {
            return _futureRng;
        }

        #endregion public methods

        #region protected methods

        protected override void CalculateRngHelper()
        {
            int rarePosition = -1;
            int rarePositionCuffs = -1;

            bool rareSteal = false;
            bool rareStealCuffs = false;

            _futureRng = new StealFutureRng();

            // Use these variables to check for first punch combo
            ComboFound = false;
            ComboPosition = -1;

            uint firstRngVal = DisplayRng.genrand();
            uint secondRngVal = DisplayRng.genrand();
            uint thirdRngVal = DisplayRng.genrand();

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
                uint thirdRngValTemp = thirdRngVal;
                firstRngVal = secondRngVal;
                secondRngVal = thirdRngVal;
                thirdRngVal = DisplayRng.genrand();

                // Skip the entry if it's too long ago
                if (LoopIndex < HealVals.Count - HistoryToDisplay)
                    continue;

                // Start actually collating data
                StealFutureRngInstance newRngInstance =
                    new StealFutureRngInstance();

                // check if we are calculating past RNG
                if (index < start + HealVals.Count)
                {
                    newRngInstance.IsPastRng = true;
                }

                // Set useful information
                newRngInstance.Index = index;
                newRngInstance.CurrentHeal = currentHeal;
                newRngInstance.RandToPercent = Utils.RandToPercent(firstRngValTemp);
                newRngInstance.Lv99RedChocobo = firstRngValTemp < 0x1000000;

                // Handle stealing
                newRngInstance.NormalReward = Steal.CheckSteal2(
                    firstRngValTemp, secondRngValTemp, thirdRngValTemp);
                newRngInstance.CuffsReward = Steal.CheckStealCuffs2(
                    firstRngValTemp, secondRngValTemp, thirdRngValTemp);

                // Stealing directions
                CalculateStealDirections(firstRngValTemp, false,
                    ref rareSteal, ref rarePosition);
                CalculateStealDirections(firstRngValTemp, true,
                    ref rareStealCuffs, ref rarePositionCuffs);

                // Check for combo during string of punches
                CheckForCombo(firstRngValTemp);

                _futureRng.AddNextRngInstance(newRngInstance);
            }

            WriteStealDirectionsInfo(rarePosition, rarePositionCuffs);

            AttacksBeforeNextCombo = ComboPosition;

            Group.SetIndex(indexStatic);
        }

        #endregion protected methods

        #region private methods

        private void CalculateStealDirections(
            uint prng, bool cuffs, ref bool found, ref int position)
        {
            if (Steal.WouldRareStealSucceed(prng, cuffs) && 
                !found && LoopIndex >= HealVals.Count)
            {
                found = true;
                position = LoopIndex - HealVals.Count;
            }
        }

        private void WriteStealDirectionsInfo(int rarePosition, int rarePositionCuffs)
        {
            _futureRng.SetStealDirections(new StealDirections
            {
                AdvanceForRare = rarePosition,
                AdvanceForRareCuffs = rarePositionCuffs
            });
        }

        #endregion private methods
    }
}