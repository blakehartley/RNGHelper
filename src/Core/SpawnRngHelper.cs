using System;
using System.Collections.Generic;
using System.Linq;

namespace FF12RNGHelper.Core
{
    /// <summary>
    /// The ChestRngHelper contains the necessary logic
    /// to calculate and predict the spawning and contents
    /// of monsters.
    /// </summary>
    public class SpawnRngHelper : BaseRngHelper
    {
        #region privates

        private readonly List<Monster> _monsters;
        private SpawnFutureRng _futureRng;

        #endregion privates

        #region construction/initialization

        public SpawnRngHelper(PlatformType platform, CharacterGroup group,
            List<Monster> monsters)
            : base(platform, group)
        {
            _monsters = monsters;
            _futureRng = new SpawnFutureRng();
        }

        #endregion construction/initialization

        #region public methods

        public new SpawnFutureRng GetSpawnFutureRng()
        {
            return _futureRng;
        }

        #endregion public methods

        #region protected methods

        protected override void CalculateRngHelper()
        {
            foreach (Monster monster in _monsters)
            {
                monster.ResetSpawnInfo();
            }

            _futureRng = new SpawnFutureRng();
            List<List<int>> lastNBeforeM = InitializeMatrix();

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
                firstRngVal = secondRngVal;
                secondRngVal = DisplayRng.genrand();

                // Skip the entry if it's too long ago
                if (LoopIndex < HealVals.Count - HistoryToDisplay)
                    continue;

                //Start actually collating data
                SpawnFutureRngInstance newRngInstance =
                    new SpawnFutureRngInstance(_monsters.Count);

                // check if we are in calculating past RNG
                if (index < start + HealVals.Count)
                {
                    newRngInstance.IsPastRng = true;
                }

                // Set useful information
                newRngInstance.Index = index;
                newRngInstance.CurrentHeal = currentHeal;
                float spawnChance = newRngInstance.SpawnChance =
                    (float)firstRngValTemp / Monster.RareSpawnValue;
                newRngInstance.RawRngValue = firstRngValTemp;

                CalculateMonsterSpawns(spawnChance, newRngInstance);

                CalculateLastNBeforeMMatrix(lastNBeforeM, spawnChance);

                CheckForCombo(firstRngValTemp);

                _futureRng.AddNextRngInstance(newRngInstance);
            }

            _futureRng.SetLastNBeforeMMatrix(lastNBeforeM);

            WriteSpawnDirectionsInfo();

            AttacksBeforeNextCombo = ComboPosition;

            Group.SetIndex(indexStatic);
        }

        #endregion protected methods

        #region private methods

        private void CalculateMonsterSpawns(float spawnChance, SpawnFutureRngInstance newRngInstance)
        {
            for (int i = 0; i < _monsters.Count; i++)
            {
                Monster monster = _monsters.ElementAt(i);
                if (monster.SpawnCheck(spawnChance))
                {
                    newRngInstance.MonsterSpawns[i] = true;

                    int chestChance = HealVals.Count +
                        monster.GetRngPosition() - 1;

                    if (LoopIndex >= chestChance &&
                        !monster.HasMonsterSpawned())
                    {
                        monster.SetMonsterFoundPosition(LoopIndex -
                            HealVals.Count - monster.GetRngPosition() + 1);
                        monster.SetMonsterSpawned();
                    }
                }
            }
        }

        private List<List<int>> InitializeMatrix()
        {
            List<List<int>> lastNBeforeM = new List<List<int>>();
            for (int i = 0; i < _monsters.Count; i++)
            {
                lastNBeforeM.Add(new List<int>(_monsters.Count));
                for (int j = 0; j < _monsters.Count; j++)
                {
                    lastNBeforeM[i].Add(int.MinValue);
                }
            }
            return lastNBeforeM;
        }

        private void CalculateLastNBeforeMMatrix(List<List<int>> lastNBeforeM, float spawnChance)
        {
            for (int n = 0; n < _monsters.Count; n++)
            {
                if (_monsters[n].SpawnCheck(spawnChance))
                {
                    for (int m = 0; m < _monsters.Count; m++)
                    {
                        if (!_monsters[m].HasMonsterSpawned())
                        {
                            if (n == m)
                            {
                                lastNBeforeM[n][m] = 0;
                            }
                            else
                            {
                                lastNBeforeM[n][m] = LoopIndex - HealVals.Count -
                                                     _monsters[n].GetRngPosition() + 1;
                            }
                        }
                    }
                }
            }
        }

        private void WriteSpawnDirectionsInfo()
        {
            foreach (Monster monster in _monsters)
            {
                SpawnDirections spawnDirections = new SpawnDirections
                {
                    Directions = monster.GetMonsterFoundPosition()
                };
                _futureRng.AddSpawnDirections(spawnDirections);
            }
        }

        #endregion private methods
    }
}