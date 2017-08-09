using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FF12RNGHelper.Core;

namespace UnitTests
{
    [TestClass]
    public class StealFutureRngTests
    {
        [TestMethod]
        public void TestGetTotalFutureRngPositions()
        {
            StealFutureRng future = new StealFutureRng();
            Assert.AreEqual(0, future.GetTotalFutureRngPositions());
            future.AddNextRngInstance(GetStealFutureRngInstance());
            Assert.AreEqual(1, future.GetTotalFutureRngPositions());
            future.AddNextRngInstance(GetStealFutureRngInstance());
            Assert.AreEqual(2, future.GetTotalFutureRngPositions());
        }

        [TestMethod]
        public void TestGetRngInstanceAt()
        {
            StealFutureRng future = new StealFutureRng();
            future.AddNextRngInstance(GetStealFutureRngInstance());
            future.AddNextRngInstance(GetStealFutureRngInstance());
            StealFutureRngInstance instance = GetStealFutureRngInstance();
            instance.Lv99RedChocobo = true;
            instance.NormalReward = StealType.Rare;

            future.AddNextRngInstance(instance);
            StealFutureRngInstance copy = future.GetRngInstanceAt(2);
            Assert.AreEqual(instance.Lv99RedChocobo, copy.Lv99RedChocobo);
            Assert.AreEqual(instance.NormalReward, copy.NormalReward);
        }

        [TestMethod]
        public void TestGetRngInstanceAt_ArgumentOutOfBounds()
        {
            StealFutureRng future = new StealFutureRng();
            future.AddNextRngInstance(GetStealFutureRngInstance());
            future.AddNextRngInstance(GetStealFutureRngInstance());
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                delegate
                {
                    future.GetRngInstanceAt(2);
                }
            );
        }

        [TestMethod]
        public void TestGetStealDirections()
        {
            StealFutureRng future = new StealFutureRng();
            StealDirections directions = future.GetStealDirections();
            Assert.IsNotNull(directions);
        }

        [TestMethod]
        public void TestSetStealDirections()
        {
            StealFutureRng future = new StealFutureRng();
            StealDirections directions = future.GetStealDirections();
            directions.AdvanceForRare = 5;
            directions.AdvanceForRareCuffs = 24;
            future.SetStealDirections(directions);

            StealDirections copy = future.GetStealDirections();
            Assert.AreEqual(
                directions.AdvanceForRare, copy.AdvanceForRare);
            Assert.AreEqual(
                directions.AdvanceForRareCuffs, copy.AdvanceForRareCuffs);
        }

        private static StealFutureRngInstance GetStealFutureRngInstance()
        {
            return new StealFutureRngInstance();
        }
    }
}
