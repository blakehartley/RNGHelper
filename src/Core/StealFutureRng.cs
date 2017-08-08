using System.Collections.Generic;
using System.Linq;

namespace FF12RNGHelper.Core
{
    /// <summary>
    /// The StealFutureRng object represents the entire
    /// foreseeable future of stealing related events.
    /// </summary>
    public class StealFutureRng : IFutureRng
    {
        #region privates

        private readonly List<StealFutureRngInstance> _futureRng;
        private StealDirections _stealDirections;

        #endregion privates

        #region construction/initialization

        public StealFutureRng()
        {
            _futureRng = new List<StealFutureRngInstance>();
            _stealDirections = new StealDirections();
        }

        #endregion construction/initialization

        #region public methods

        public int GetTotalFutureRngPositions()
        {
            return _futureRng.Count;
        }

        /// <summary>
        /// Add an RNG instance to the FutureRng object
        /// </summary>
        public void AddNextRngInstance(StealFutureRngInstance rngInstance)
        {
            _futureRng.Add(rngInstance);
        }

        /// <summary>
        /// Gets the RNG position at a given index
        /// </summary>
        public StealFutureRngInstance GetRngInstanceAt(int position)
        {
            return _futureRng.ElementAt(position);
        }

        /// <summary>
        /// Get the Advance Directions for a chest at a given index
        /// </summary>
        public StealDirections GetStealDirections()
        {
            return _stealDirections;
        }

        /// <summary>
        /// Add an StealDirections to the FutureRng object
        /// </summary>
        /// <param name="stealDirections"></param>
        public void SetStealDirections(StealDirections stealDirections)
        {
            _stealDirections = stealDirections;
        }

        #endregion public methods
    }

    #region public types

    /// <summary>
    /// A StealFutureRngInstance represents a single
    /// step into the future
    /// </summary>
    public class StealFutureRngInstance
    {
        public bool IsPastRng;
        public int Index;
        public int CurrentHeal;
        public int RandToPercent;
        public StealType NormalReward;
        public List<StealType> CuffsReward;
        public bool Lv99RedChocobo;

        public StealFutureRngInstance()
        {
            IsPastRng = false;
            Lv99RedChocobo = false;
            Index = -1;
            CurrentHeal = -1;
            RandToPercent = -1;
            NormalReward = StealType.None;
            CuffsReward = new List<StealType>
            {
                StealType.None
            };
        }
    }

    /// <summary>
    /// This class is used to store the recommended
    /// actions based on info provided
    /// </summary>
    public class StealDirections
    {
        public int AdvanceForRare;
        public int AdvanceForRareCuffs;
    }

    /// <summary>
    /// Enumeration of the possible steal rewards
    /// </summary>
    public enum StealType
    {
        Rare,
        Uncommon,
        Common,
        None
    }

    #endregion public types
}