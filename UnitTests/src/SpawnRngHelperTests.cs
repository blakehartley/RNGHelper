using System;
using System.Collections.Generic;
using FF12RNGHelper.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class SpawnRngHelperTests
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
        public void TestGetSpawnFutureRngNotNull()
        {
            SpawnRngHelper helper = GetDefaultHelper();
            Assert.IsNotNull(helper.GetSpawnFutureRng());
        }

        [TestMethod]
        public void TestGetSpawnFutureRng()
        {
            SpawnRngHelper helper = GetDefaultHelper();
            helper.FindFirstRngPosition(88);
            helper.CalculateRng(100);
            Assert.IsNotNull(helper.GetSpawnFutureRng());
        }

        [TestMethod]
        public void TestGetAttacksUntilNextCombo()
        {
            SpawnRngHelper helper = GetDefaultHelper();
            helper.FindFirstRngPosition(89);
            helper.FindNextRngPosition(87);
            helper.FindNextRngPosition(97);
            helper.CalculateRng(100);
            Assert.AreEqual(6, helper.GetAttacksUntilNextCombo());
        }

        [TestMethod]
        public void TestGetNextExpecteHealValue()
        {
            SpawnRngHelper helper = GetDefaultHelper();
            helper.FindFirstRngPosition(94);
            helper.FindNextRngPosition(89);
            helper.FindNextRngPosition(97);
            helper.CalculateRng(100);
            Assert.AreEqual(94, helper.GetNextExpectedHealValue());
        }

        [TestMethod]
        public void TestConsumeNextNRngPositions()
        {
            SpawnRngHelper helper = GetDefaultHelper();
            helper.FindFirstRngPosition(94);
            helper.FindNextRngPosition(89);
            Assert.IsTrue(helper.ConsumeNextNRngPositions(10));
            helper.FindNextRngPosition(95);
            helper.CalculateRng(100);
            Assert.AreEqual(94, helper.GetNextExpectedHealValue());
        }

        [TestMethod]
        public void TestLastNBeforeMCalculation()
        {
            SpawnRngHelper helper = GetComplexHelper();
            helper.FindFirstRngPosition(89);
            helper.FindNextRngPosition(87);
            helper.FindNextRngPosition(97);
            helper.CalculateRng(100);
            Assert.AreEqual(1, helper.GetSpawnFutureRng()
                .GetStepsToLastNSpawnBeforeMSpawn(0, 1));
            helper.ConsumeNextNRngPositions(10);
            helper.CalculateRng(100);
            Assert.AreEqual(4, helper.GetSpawnFutureRng()
                .GetStepsToLastNSpawnBeforeMSpawn(0, 1));
        }

        private static SpawnRngHelper GetDefaultHelper()
        {
            return new SpawnRngHelper(
                PlatformType.Ps2,
                TestUtils.GetSimpleCharacterGroup(),
                new List<Monster>
                {
                    TestUtils.GetDefaultMonster() 
                });
        }

        private static SpawnRngHelper GetComplexHelper()
        {
            return new SpawnRngHelper(
                PlatformType.Ps2,
                TestUtils.GetSimpleCharacterGroup(),
                new List<Monster>
                {
                    new Monster(0, 20, 1),
                    new Monster(0, 10, 1)
                });
        }
    }
}
