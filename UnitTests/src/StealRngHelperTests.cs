using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FF12RNGHelper.Core;

namespace UnitTests
{
    [TestClass]
    public class StealRngHelperTests
    {
        [TestMethod]
        public void TestGetChestFutureRngThrowsException()
        {
            Assert.ThrowsException<NotImplementedException>(
                delegate
                {
                    GetDefaultHelper().GetChestFutureRng();
                }
            );
        }

        [TestMethod]
        public void TestGetStealFutureRngNotNull()
        {
            StealRngHelper helper = GetDefaultHelper();
            Assert.IsNotNull(helper.GetStealFutureRng());
        }

        [TestMethod]
        public void TestGetStealFutureRng()
        {
            StealRngHelper helper = GetDefaultHelper();
            helper.FindFirstRngPosition(88);
            helper.CalculateRng(100);
            Assert.IsNotNull(helper.GetStealFutureRng());
        }

        [TestMethod]
        public void TestGetAttacksUntilNextCombo()
        {
            StealRngHelper helper = GetDefaultHelper();
            helper.FindFirstRngPosition(89);
            helper.FindNextRngPosition(87);
            helper.FindNextRngPosition(97);
            helper.CalculateRng(100);
            Assert.AreEqual(6, helper.GetAttacksUntilNextCombo());
        }

        [TestMethod]
        public void TestGetNextExpecteHealValue()
        {
            StealRngHelper helper = GetDefaultHelper();
            helper.FindFirstRngPosition(94);
            helper.FindNextRngPosition(89);
            helper.FindNextRngPosition(97);
            helper.CalculateRng(100);
            Assert.AreEqual(94, helper.GetNextExpectedHealValue());
        }

        [TestMethod]
        public void TestConsumeNextNRngPositions()
        {
            StealRngHelper helper = GetDefaultHelper();
            helper.FindFirstRngPosition(94);
            helper.FindNextRngPosition(89);
            Assert.IsTrue(helper.ConsumeNextNRngPositions(10));
            helper.FindNextRngPosition(95);
            helper.CalculateRng(100);
            Assert.AreEqual(94, helper.GetNextExpectedHealValue());
        }

        private static StealRngHelper GetDefaultHelper()
        {
            return new StealRngHelper(
                PlatformType.Ps2,
                TestUtils.GetSimpleCharacterGroup());
        }
    }
}
