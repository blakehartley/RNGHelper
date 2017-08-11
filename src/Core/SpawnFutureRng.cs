using System.Collections.Generic;
using System.Linq;

namespace FF12RNGHelper.Core
{
    /// <summary>
    /// The SpawnFutureRng object represents the entire
    /// foreseeable future of chest related events.
    /// </summary>
    public class SpawnFutureRng : IFutureRng
    {
        #region privates

        private readonly List<SpawnFutureRngInstance> _futureRng;
        private readonly List<SpawnDirections> _spawnDirections;
        private List<List<int>> _lastNBeforeM;

        #endregion privates

        #region construction/initialization

        public SpawnFutureRng()
        {
            _futureRng = new List<SpawnFutureRngInstance>();
            _spawnDirections = new List<SpawnDirections>();
            _lastNBeforeM = new List<List<int>>();
            /*
            for (int i = 0; i < monsterCount; i++)
            {
                _lastNBeforeM.Add(new List<int>(monsterCount));
                for (int j = 0; j < monsterCount; j++)
                {
                    _lastNBeforeM[i].Add(-1);
                }
            }
            */
        }

        #endregion construction/initialization

        #region public methods

        public int GetTotalFutureRngPositions()
        {
            return _futureRng.Count;
        }

        /// <summary>
        /// Return the number of SpawnDirections available
        /// </summary>
        public int GetSpawnDirectionsCount()
        {
            return _spawnDirections.Count;
        }

        /// <summary>
        /// Add an RNG instance to the FutureRng object
        /// </summary>
        public void AddNextRngInstance(SpawnFutureRngInstance rngInstance)
        {
            _futureRng.Add(rngInstance);
        }

        /// <summary>
        /// Gets the RNG position at a given index
        /// </summary>
        public SpawnFutureRngInstance GetRngInstanceAt(int position)
        {
            return _futureRng.ElementAt(position);
        }

        /// <summary>
        /// Get the Advance Directions for a chest at a given index
        /// </summary>
        public SpawnDirections GetSpawnDirectionsAtIndex(int position)
        {
            return _spawnDirections.ElementAt(position);
        }

        /// <summary>
        /// Add an SpawnDirections to the FutureRng object
        /// </summary>
        /// <param name="spawnDirections"></param>
        public void AddSpawnDirections(SpawnDirections spawnDirections)
        {
            _spawnDirections.Add(spawnDirections);
        }

        /// <summary>
        /// Sets the LastNBeforeM matrix
        /// </summary>
        /// <param name="lastNBeforeM">Fully calculated matrix</param>
        public void SetLastNBeforeMMatrix(List<List<int>> lastNBeforeM)
        {
            _lastNBeforeM = lastNBeforeM;
        }

        /// <summary>
        /// Gets the number of steps from the next M spawn
        /// to the most recent prior spawn of N.
        /// </summary>
        public int GetStepsToLastNSpawnBeforeMSpawn(int n, int m)
        {
            return _lastNBeforeM[n][m];
        }

        #endregion public methods
    }

    #region public types

    /// <summary>
    /// A SpawnFutureRngInstance represents a single
    /// step into the future
    /// </summary>
    public class SpawnFutureRngInstance
    {
        public bool IsPastRng;
        public int Index;
        public int CurrentHeal;
        public float SpawnChance;
        public long RawRngValue;
        public List<bool> MonsterSpawns;

        public SpawnFutureRngInstance(int monsterCount)
        {
            IsPastRng = false;
            Index = -1;
            CurrentHeal = -1;
            SpawnChance = -1;
            RawRngValue = -1;
            MonsterSpawns = new List<bool>(monsterCount);
            for (int i = 0; i < monsterCount; i++)
            {
                MonsterSpawns.Add(false);
            }
            
        }
    }

    /// <summary>
    /// This class is used to store the recommended
    /// actions based on info provided
    /// </summary>
    public class SpawnDirections
    {
        public int Directions;
    }

    #endregion public types
}