using System;
using System.Collections.Generic;
using FF12RNGHelper.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class ChestRngHelperTests
    {
        [TestMethod]
        public void TestGetStealFutureRngThrowsException()
        {
            Assert.ThrowsException<NotImplementedException>(
                delegate
                {
                    GetDefaultHelper().GetStealFutureRng();
                }
            );
        }

        [TestMethod]
        public void TestGetChestFutureRngNotNull()
        {
            ChestRngHelper helper = GetDefaultHelper();
            Assert.IsNotNull(helper.GetChestFutureRng());
        }

        [TestMethod]
        public void TestGetChestFutureRng()
        {
            ChestRngHelper helper = GetDefaultHelper();
            helper.FindFirstRngPosition(88);
            helper.CalculateRng(100);
            Assert.IsNotNull(helper.GetChestFutureRng());
        }

        [TestMethod]
        public void TestGetAttacksUntilNextCombo()
        {
            ChestRngHelper helper = GetDefaultHelper();
            helper.FindFirstRngPosition(89);
            helper.FindNextRngPosition(87);
            helper.FindNextRngPosition(97);
            helper.CalculateRng(100);
            Assert.AreEqual(6, helper.GetAttacksUntilNextCombo());
        }

        [TestMethod]
        public void TestGetNextExpecteHealValue()
        {
            ChestRngHelper helper = GetDefaultHelper();
            helper.FindFirstRngPosition(94);
            helper.FindNextRngPosition(89);
            helper.FindNextRngPosition(97);
            helper.CalculateRng(100);
            Assert.AreEqual(94, helper.GetNextExpectedHealValue());
        }

        [TestMethod]
        public void TestConsumeNextNRngPositions()
        {
            ChestRngHelper helper = GetDefaultHelper();
            helper.FindFirstRngPosition(94);
            helper.FindNextRngPosition(89);
            Assert.IsTrue(helper.ConsumeNextNRngPositions(10));
            helper.FindNextRngPosition(95);
            helper.CalculateRng(100);
            Assert.AreEqual(94, helper.GetNextExpectedHealValue());
        }

        private static ChestRngHelper GetDefaultHelper()
        {
            return new ChestRngHelper(
                PlatformType.Ps2,
                TestUtils.GetSimpleCharacterGroup(),
                new List<Chest>
                {
                    TestUtils.GetDefaultChest() 
                });
        }
    }
}
