using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FF12RNGHelper.Core;

namespace UnitTests
{
    [TestClass]
    public class SpawnFutureRngTests
    {
        [TestMethod]
        public void TestGetTotalFutureRngPositions()
        {
            SpawnFutureRng future = new SpawnFutureRng();
            Assert.AreEqual(0, future.GetTotalFutureRngPositions());
            future.AddNextRngInstance(GetSpawnFutureRngInstance());
            Assert.AreEqual(1, future.GetTotalFutureRngPositions());
            future.AddNextRngInstance(GetSpawnFutureRngInstance());
            Assert.AreEqual(2, future.GetTotalFutureRngPositions());
        }

        [TestMethod]
        public void TestGetSpawnDirectionsCount()
        {
            SpawnFutureRng future = new SpawnFutureRng();
            Assert.AreEqual(0, future.GetSpawnDirectionsCount());
            future.AddSpawnDirections(GetSpawnDirections());
            Assert.AreEqual(1, future.GetSpawnDirectionsCount());
            future.AddSpawnDirections(GetSpawnDirections());
            Assert.AreEqual(2, future.GetSpawnDirectionsCount());
        }

        [TestMethod]
        public void TestGetRngInstanceAt()
        {
            SpawnFutureRng future = new SpawnFutureRng();
            future.AddNextRngInstance(GetSpawnFutureRngInstance());
            future.AddNextRngInstance(GetSpawnFutureRngInstance());
            SpawnFutureRngInstance instance = GetSpawnFutureRngInstance();
            instance.Index = 5;
            instance.CurrentHeal = 9999;

            future.AddNextRngInstance(instance);
            SpawnFutureRngInstance copy = future.GetRngInstanceAt(2);
            Assert.AreEqual(instance.Index, copy.Index);
            Assert.AreEqual(instance.CurrentHeal, copy.CurrentHeal);
        }

        [TestMethod]
        public void TestGetRngInstanceAt_ArgumentOutOfBounds()
        {
            SpawnFutureRng future = new SpawnFutureRng();
            future.AddNextRngInstance(GetSpawnFutureRngInstance());
            future.AddNextRngInstance(GetSpawnFutureRngInstance());
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                delegate
                {
                    future.GetRngInstanceAt(2);

                });
        }

        [TestMethod]
        public void TestGetSpawnDirectionsAt()
        {
            SpawnFutureRng future = new SpawnFutureRng();
            future.AddSpawnDirections(GetSpawnDirections());
            future.AddSpawnDirections(GetSpawnDirections());
            future.AddSpawnDirections(GetSpawnDirections());
            SpawnDirections directions = GetSpawnDirections();
            directions.Directions = 7;

            future.AddSpawnDirections(directions);
            SpawnDirections copy = future.GetSpawnDirectionsAtIndex(3);
            Assert.AreEqual(directions.Directions, copy.Directions);
        }

        [TestMethod]
        public void TestGetSpawnDirectionsAt_ArgumentOutOfBounds()
        {
            SpawnFutureRng future = new SpawnFutureRng();
            future.AddSpawnDirections(GetSpawnDirections());
            future.AddSpawnDirections(GetSpawnDirections());
            future.AddSpawnDirections(GetSpawnDirections());
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                delegate
                {
                    future.GetSpawnDirectionsAtIndex(3);

                });
        }

        [TestMethod]
        public void TestSetLastNBeforeMMatrixGetStepsToLastNSpawnBeforeMSpawn()
        {
            SpawnFutureRng future = new SpawnFutureRng();
            future.SetLastNBeforeMMatrix(GetSimpleMatrix());
            Assert.AreEqual(5, future.GetStepsToLastNSpawnBeforeMSpawn(1, 1));
            Assert.AreEqual(9, future.GetStepsToLastNSpawnBeforeMSpawn(2, 2));
        }

        [TestMethod]
        public void TestGetStepsToLastNSpawnBeforeMSpawn_ArgumentOutOfBounds()
        {
            SpawnFutureRng future = new SpawnFutureRng();
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                delegate
                {
                    future.GetStepsToLastNSpawnBeforeMSpawn(1, 1);
                });
        }

        private static SpawnFutureRngInstance GetSpawnFutureRngInstance()
        {
            return new SpawnFutureRngInstance(2);
        }

        private SpawnDirections GetSpawnDirections()
        {
            return new SpawnDirections();
        }

        private List<List<int>> GetSimpleMatrix()
        {
            var simple = new List<List<int>>();
            int val = 0;
            for (int i = 0; i < 3; i++)
            {
                simple.Add(new List<int>());
                for (int j = 0; j < 3; j++)
                {
                    simple[i].Add(++val);
                }
            }

            return simple;
        }
    }
}
