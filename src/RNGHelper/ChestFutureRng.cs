using System.Collections.Generic;
using System.Linq;

namespace FF12RNGHelper
{
    /// <summary>
    /// The ChestFutureRng object represents the entire
    /// foreseeable future of chest related events.
    /// </summary>
    public class ChestFutureRng : IFutureRng
    {
        #region privates

        private readonly List<ChestFutureRngInstance> _futureRng;
        private readonly List<AdvanceDirections> _advanceDirections;

        #endregion privates

        #region construction/initialization

        public ChestFutureRng()
        {
            _futureRng = new List<ChestFutureRngInstance>();
            _advanceDirections = new List<AdvanceDirections>();
        }

        #endregion construction/initialization

        #region public methods

        public int GetTotalFutureRngPositions()
        {
            return _futureRng.Count;
        }

        /// <summary>
        /// Return the number of AdvanceDirections available
        /// </summary>
        public int GetAdvanceDirectionsCount()
        {
            return _advanceDirections.Count;
        }

        /// <summary>
        /// Add an RNG instance to the FutureRng object
        /// </summary>
        public void AddNextRngInstance(ChestFutureRngInstance rngInstance)
        {
            _futureRng.Add(rngInstance);
        }

        /// <summary>
        /// Gets the RNG position at a given index
        /// </summary>
        public ChestFutureRngInstance GetRngInstanceAt(int position)
        {
            return _futureRng.ElementAt(position);
        }

        /// <summary>
        /// Get the Advance Directions for a chest at a given index
        /// </summary>
        public AdvanceDirections GetAdvanceDirectionsAtIndex(int position)
        {
            return _advanceDirections.ElementAt(position);
        }

        /// <summary>
        /// Add an AdvanceDirections to the FutureRng object
        /// </summary>
        /// <param name="advanceDirections"></param>
        public void AddAdvanceDirections(AdvanceDirections advanceDirections)
        {
            _advanceDirections.Add(advanceDirections);
        }

        #endregion public methods
    }

    #region public types

    /// <summary>
    /// A ChestFutureRngInstance represents a single
    /// step into the future
    /// </summary>
    public class ChestFutureRngInstance
    {
        public bool IsPastRng;
        public int Index;
        public int CurrentHeal;
        public int RandToPercent;
        public List<ChestReward> ChestRewards;

        public ChestFutureRngInstance(int chestCount)
        {
            IsPastRng = false;
            Index = -1;
            CurrentHeal = -1;
            RandToPercent = -1;
            ChestRewards = new List<ChestReward>();
            for (int i = 0; i < chestCount; i++)
            {
                ChestRewards.Add(new ChestReward());
            }
        }
    }

    /// <summary>
    /// A ChestReward represents the results of a single chest
    /// for a single ChestFutureRngInstance
    /// </summary>
    public class ChestReward
    {
        public bool ChestWillSpawn;
        public int GilAmount;
        public RewardType Reward;

        public ChestReward()
        {
            ChestWillSpawn = false;
            GilAmount = -1;
            Reward = RewardType.Gil;
        }
    }

    /// <summary>
    /// This class is used to store the recommended
    /// actions based on info provided
    /// </summary>
    public class AdvanceDirections
    {
        public int AdvanceToAppear;
        public int AdvanceForItem;
    }

    /// <summary>
    /// Enumeration of the possible chest rewards
    /// </summary>
    public enum RewardType
    {
        Gil,
        Item1,
        Item2
    }

    #endregion public types
}