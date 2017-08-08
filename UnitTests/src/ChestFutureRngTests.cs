using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FF12RNGHelper.Core;

namespace UnitTests
{
    [TestClass]
    public class ChestFutureRngTests
    {
        [TestMethod]
        public void TestGetTotalFutureRngPositions()
        {
            ChestFutureRng future = new ChestFutureRng();
            Assert.AreEqual(0, future.GetTotalFutureRngPositions());
            future.AddNextRngInstance(GetChestFutureRngInstance());
            Assert.AreEqual(1, future.GetTotalFutureRngPositions());
            future.AddNextRngInstance(GetChestFutureRngInstance());
            Assert.AreEqual(2, future.GetTotalFutureRngPositions());
        }

        [TestMethod]
        public void TestGetAdvanceDirectionsCount()
        {
            ChestFutureRng future = new ChestFutureRng();
            Assert.AreEqual(0, future.GetAdvanceDirectionsCount());
            future.AddAdvanceDirections(GetAdvanceDirections());
            Assert.AreEqual(1, future.GetAdvanceDirectionsCount());
            future.AddAdvanceDirections(GetAdvanceDirections());
            Assert.AreEqual(2, future.GetAdvanceDirectionsCount());
        }

        [TestMethod]
        public void TestGetRngInstanceAt()
        {
            ChestFutureRng future = new ChestFutureRng();
            future.AddNextRngInstance(GetChestFutureRngInstance());
            future.AddNextRngInstance(GetChestFutureRngInstance());
            ChestFutureRngInstance instance = GetChestFutureRngInstance();
            instance.Index = 5;
            instance.CurrentHeal = 9999;

            future.AddNextRngInstance(instance);
            ChestFutureRngInstance copy = future.GetRngInstanceAt(2);
            Assert.AreEqual(instance.Index, copy.Index);
            Assert.AreEqual(instance.CurrentHeal, copy.CurrentHeal);
        }

        [TestMethod]
        public void TestGetRngInstanceAt_ArgumentOutOfBounds()
        {
            ChestFutureRng future = new ChestFutureRng();
            future.AddNextRngInstance(GetChestFutureRngInstance());
            future.AddNextRngInstance(GetChestFutureRngInstance());
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                delegate
                {
                    future.GetRngInstanceAt(2);

                });
        }

        [TestMethod]
        public void TestGetAdvanceDirectionsAt()
        {
            ChestFutureRng future = new ChestFutureRng();
            future.AddAdvanceDirections(GetAdvanceDirections());
            future.AddAdvanceDirections(GetAdvanceDirections());
            future.AddAdvanceDirections(GetAdvanceDirections());
            AdvanceDirections directions = GetAdvanceDirections();
            directions.AdvanceForItem = 7;
            directions.AdvanceToAppear = 24;

            future.AddAdvanceDirections(directions);
            AdvanceDirections copy = future.GetAdvanceDirectionsAtIndex(3);
            Assert.AreEqual(directions.AdvanceForItem, copy.AdvanceForItem);
            Assert.AreEqual(directions.AdvanceToAppear, copy.AdvanceToAppear);
        }

        [TestMethod]
        public void TestGetAdvanceDirectionsAt_ArgumentOutOfBounds()
        {
            ChestFutureRng future = new ChestFutureRng();
            future.AddAdvanceDirections(GetAdvanceDirections());
            future.AddAdvanceDirections(GetAdvanceDirections());
            future.AddAdvanceDirections(GetAdvanceDirections());
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                delegate
                {
                    future.GetAdvanceDirectionsAtIndex(3);

                });
        }

        private static ChestFutureRngInstance GetChestFutureRngInstance()
        {
            return new ChestFutureRngInstance(1);
        }

        private AdvanceDirections GetAdvanceDirections()
        {
            return new AdvanceDirections();
        }
    }
}
