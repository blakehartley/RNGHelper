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
