using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FF12RNGHelper.Core
{
    /// <summary>
    /// The BaseRngHelper class contains all of the generic
    /// members and functionality that is required to satisfy
    /// the IRngHelper Interface. Extending this class is not
    /// required, but is recommended.
    ///  
    /// Extending classes MUST implement the following methods:
    /// protected override void CalculateRngHelper();
    /// 
    /// Extending classes MUST implement ONE of the following
    /// methods:
    /// public new ChestFutureRng GetChestFutureRng();
    /// public new StealFutureRng GetStealFutureRng();
    /// public new SpawnFutureRng GetSpawnFutureRng();
    /// </summary>
    public abstract class BaseRngHelper : IRngHelper
    {
        #region constants

        protected const int HistoryToDisplay = 5;

        private const int FindNextTimeout = 30;
        private const int SearchBufferSize = (int) 1e6; // 1 million
        private const int MaxSearchIndexSupported = (int) 1e7; // 10 million

        private const string ExtendingClassMustImplementMsg =
            "Extending class must implement this method if appropriate.";

        #endregion constants

        #region internal state

        // These members MUST be set by the CalculateRngHelper method
        // implemented by an extending class.
        protected int NextHealValue;
        protected int AttacksBeforeNextCombo;
        protected int ComboPosition;
        protected bool ComboFound;
        protected IRNG DisplayRng;
        protected int LoopIndex;

        // Members provided for data access. Can be updated.
        protected int Index; // Current index in the PRNG list
        protected int FutureRngPositionsToCalculate = 100;
        protected List<int> HealVals; // List of heal values input by user
        protected CharacterGroup Group = new CharacterGroup();

        private CircularBuffer<uint> _searchBuff; // buffer of PRNG numbers
        private IRNG _searchRng;
        private PlatformType _platformType = PlatformType.Ps2;
        private bool _foundFirstRngPosition;
        private RngState _saveState;

        #endregion internal state

        #region constructors/initilization

        protected BaseRngHelper(PlatformType platform, CharacterGroup group)
        {
            Initialize(platform, group);
        }

        private void Initialize(PlatformType platform, CharacterGroup group)
        {
            Group = group;
            _platformType = platform;
            Initialize();
        }

        private void Initialize()
        {
            Index = 0;
            _foundFirstRngPosition = false;
            Group.ResetIndex();
            _searchBuff = new CircularBuffer<uint>(SearchBufferSize);
            HealVals = new List<int>();
            _searchRng = InitializeRng();

            _searchRng.sgenrand();
            _searchBuff.Add(_searchRng.genrand());
        }

        #endregion constructors/initilization

        #region abstract methods

        // These methods must be implemented by any concrete classes 
        // extending this class
        /// <summary>
        /// This method does the "Heavy Lifting" of actually
        /// calculating the future Rng that is appropriate
        /// for the class. This method is responsible for leaving
        /// the IFutureRng object that is returned by the
        /// appropriate fatty interface method in it's final state.
        /// </summary>
        protected abstract void CalculateRngHelper();

        #endregion abstract methods

        #region Fatty interface methods

        // One of these methods that return a FutureRng object must 
        // also be implemented by the extending class
        public ChestFutureRng GetChestFutureRng()
        {
            throw new NotImplementedException(ExtendingClassMustImplementMsg);
        }

        public StealFutureRng GetStealFutureRng()
        {
            throw new NotImplementedException(ExtendingClassMustImplementMsg);
        }

        public SpawnFutureRng GetSpawnFutureRng()
        {
            throw new NotImplementedException(ExtendingClassMustImplementMsg);
        }

        #endregion Fatty interface methods

        #region public methods

        public void Reinitialize()
        {
            Initialize();
        }

        public int GetAttacksUntilNextCombo()
        {
            return AttacksBeforeNextCombo;
        }

        public int GetNextExpectedHealValue()
        {
            return NextHealValue;
        }

        public bool FindFirstRngPosition(int firstHealValue)
        {
            // A little bit non-optimal for the first use,
            // as we initialize twice.
            // TO-DO: Optimize
            Initialize();
            _foundFirstRngPosition = FindRngPositionHelper(firstHealValue);
            return _foundFirstRngPosition;
        }

        public bool FindNextRngPosition(int nextHealValue)
        {
            // If we haven't found the first position yet
            // our group index will be incorrect.
            if (!_foundFirstRngPosition)
            {
                return false;
            }

            // Store all of the information we need to restore our state
            // if we fail
            SaveState();

            Group.IncrimentIndex();
            bool matchFound = FindRngPositionHelper(nextHealValue);

            if (!matchFound)
            {
                // Restore state since we failed
                RestoreState();
            }

            return matchFound;
        }

        public bool ConsumeNextNRngPositions(int positionsToConsume)
        {
            // If we haven't found the first position yet
            // our group index will be incorrect.
            if (!_foundFirstRngPosition)
            {
                return false;
            }

            for (int position = 1; position <= positionsToConsume; position++)
            {
                Group.IncrimentIndex();
                FindRngPositionHelper(GetNextExpectedHealValue());
                // Don't calculate future RNG during the final iteration.
                // This is the callers responsibility
                if (position != positionsToConsume)
                {
                    CalculateRng(1);
                }
            }

            return true;
        }

        public void CalculateRng(int rowsToRender)
        {
            DisplayRng = InitializeRng();
            // Consume RNG seeds before our desired index
            // This can take obscene amounts of time.
            // This method still takes considerably less time
            // than FindNext, however.
            int start = GetLoopStartIndex();
            for (int burn = 0; burn < start; burn++)
            {
                DisplayRng.genrand();
            }

            SetFutureRngPositionsToCalculate(rowsToRender);

            CalculateRngHelper();
        }

        #endregion public methods

        #region protected methods

        protected IRNG InitializeRng()
        {
            if (_platformType.Equals(PlatformType.Ps2))
            {
                return new RNG1998();
            }

            return new RNG2002();
        }

        protected int GetLoopStartIndex()
        {
            return Index - HealVals.Count + 1;
        }

        protected void CheckForCombo(uint firstRngValTemp)
        {
            int comboCheck = LoopIndex - HealVals.Count + 1 - Combo.IndexOfPreviousComboRng;

            if (comboCheck % Combo.RngConsumedForAttack == 0 && comboCheck >= 0)
            {
                if (!ComboFound && Combo.IsSucessful(firstRngValTemp))
                {
                    ComboFound = true;
                    ComboPosition = comboCheck / Combo.RngConsumedForAttack;
                }
            }
        }

        #endregion protected methods

        #region private methods

        private bool FindRngPositionHelper(int healValue)
        {
            // Do a range check before trying this out to avoid 
            // entering an infinite loop.
            if (!Group.ValidateHealValue(healValue))
            {
                return false;
            }

            // Store the current character while searching:
            int indexStatic = Group.GetIndex();

            // Add the given heal value to the heal list.
            HealVals.Add(healValue);
            Index++;

            // Pull an extra PRNG draw to see whether it matches.
            _searchBuff.Add(_searchRng.genrand());

            // Otherwise, continue moving through the RNG to find the 
            // next matching position
            bool match = false;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (!match)
            {
                // Quit if it's taking too long.
                if (timer.Elapsed.TotalSeconds > FindNextTimeout ||
                    Index > MaxSearchIndexSupported)
                {
                    timer.Stop();
                    return false;
                }

                Group.ResetIndex();
                for (int i = 0; i < HealVals.Count; i++)
                {
                    // index of first heal:
                    int index0 = Index - HealVals.Count + 1;

                    if (!(match = Group.GetHealValue(_searchBuff[index0 + i]) == HealVals[i]))
                    {
                        break;
                    }
                }
                if (!match)
                {
                    _searchBuff.Add(_searchRng.genrand());
                    Index++;
                }
            }
            timer.Stop();

            Group.SetIndex(indexStatic);
            return true;
        }

        private void SetFutureRngPositionsToCalculate(int positionsToCalculate)
        {
            FutureRngPositionsToCalculate =
                ValidatePositionsToCalculate(positionsToCalculate);
        }

        private int ValidatePositionsToCalculate(int positionsToCalulate)
        {
            const int minValidPositions = 30;
            const int maxValidPositions = 10000;

            int validatedNumber = positionsToCalulate;
            if (validatedNumber < minValidPositions)
            {
                return minValidPositions;
            }
            if (validatedNumber > maxValidPositions)
            {
                return maxValidPositions;
            }
            return validatedNumber;
        }

        private void SaveState()
        {
            _saveState = new RngState
            {
                GroupIndex = Group.GetIndex(),
                Index = Index,
                // We have to Deep Copy this data
                HealVals = new List<int>(HealVals),
                SearchBuff = _searchBuff.DeepClone(),
                SearchRng = _searchRng.DeepClone()
            };
        }

        private void RestoreState()
        {
            Group.SetIndex(_saveState.GroupIndex);
            Index = _saveState.Index;
            // Deep copy the data to be safe.
            // TO-DO: Optimize out deep copy
            HealVals = new List<int>(_saveState.HealVals);
            _searchBuff = _saveState.SearchBuff.DeepClone();
            _searchRng = _saveState.SearchRng.DeepClone();
        }

        private struct RngState
        {
            public int GroupIndex;
            public int Index;
            public List<int> HealVals;
            public CircularBuffer<uint> SearchBuff;
            public IRNG SearchRng;
        }

        #endregion private methods
    }
}