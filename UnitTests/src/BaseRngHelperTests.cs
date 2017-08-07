using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FF12RNGHelper.Core;

namespace UnitTests
{
    [TestClass]
    public class BaseRngHelperTests
    {
        [TestMethod]
        public void TestFattyInterfaceMethodsThrowExceptions()
        {
            MockRngHelper rng = GetHelper();
            Assert.ThrowsException<NotImplementedException>(
                delegate
                {
                    rng.GetChestFutureRng();
                }
            );
        }

        [TestMethod]
        public void TestFindFirstRngPositionSuccessful()
        {
            MockRngHelper rng = GetHelper();
            Assert.IsTrue(rng.FindFirstRngPosition(86));
            Assert.IsTrue(rng.FindFirstRngPosition(88));
            Assert.IsTrue(rng.FindFirstRngPosition(90));
            Assert.IsTrue(rng.FindFirstRngPosition(92));
            Assert.IsTrue(rng.FindFirstRngPosition(95));
            Assert.IsTrue(rng.FindFirstRngPosition(97));
        }

        [TestMethod]
        public void TestFindFirstRngPositionFailure()
        {
            MockRngHelper rng = GetHelper();
            Assert.IsFalse(rng.FindFirstRngPosition(10));
            Assert.IsFalse(rng.FindFirstRngPosition(85));
            Assert.IsFalse(rng.FindFirstRngPosition(98));
            Assert.IsFalse(rng.FindFirstRngPosition(500));
        }

        [TestMethod]
        public void TestFindNextRngPositionFirstPositionNotYetFound()
        {
            MockRngHelper rng = GetHelper();
            Assert.IsFalse(rng.FindNextRngPosition(88));
            Assert.IsFalse(rng.FindNextRngPosition(95));
            Assert.IsFalse(rng.FindNextRngPosition(110));
        }

        [TestMethod]
        public void TestConsumeNextNRngPositionsFirstPositionNotYetFound()
        {
            MockRngHelper rng = GetHelper();
            Assert.IsFalse(rng.ConsumeNextNRngPositions(10));
            Assert.IsFalse(rng.ConsumeNextNRngPositions(15));
            Assert.IsFalse(rng.ConsumeNextNRngPositions(25));
        }

        [TestMethod]
        public void TestFindNextRngPositionFirstPosition_Basic()
        {
            MockRngHelper rng = GetHelper();
            Assert.IsTrue(rng.FindFirstRngPosition(91));
            Assert.IsTrue(rng.FindNextRngPosition(88));
            Assert.IsTrue(rng.FindNextRngPosition(92));
            Assert.IsTrue(rng.FindNextRngPosition(94));
            Assert.IsTrue(rng.FindNextRngPosition(94));
            Assert.IsTrue(rng.FindNextRngPosition(94));
            Assert.IsTrue(rng.FindNextRngPosition(95));
        }

        [TestMethod]
        public void TestFindNextRngPositionFirstPosition()
        {
            MockRngHelper rng = GetHelper();
            Assert.IsTrue(rng.FindFirstRngPosition(92));
            Assert.IsTrue(rng.FindNextRngPosition(95));
            Assert.IsTrue(rng.FindNextRngPosition(88));
            Assert.IsTrue(rng.FindNextRngPosition(89));
            Assert.IsTrue(rng.FindNextRngPosition(95));
            Assert.IsTrue(rng.FindNextRngPosition(93));
            Assert.IsTrue(rng.FindNextRngPosition(94));
            Assert.IsTrue(rng.FindNextRngPosition(96));
            Assert.IsTrue(rng.FindNextRngPosition(92));
            Assert.IsTrue(rng.FindNextRngPosition(97));
            Assert.IsTrue(rng.FindNextRngPosition(88));
            Assert.IsFalse(rng.FindNextRngPosition(94));
        }

        [TestMethod]
        public void TestFindNextRngPositionFirstPositionCanContinueAfterFailing()
        {
            MockRngHelper rng = GetHelper();
            Assert.IsTrue(rng.FindFirstRngPosition(92));
            Assert.IsTrue(rng.FindNextRngPosition(95));
            Assert.IsTrue(rng.FindNextRngPosition(88));
            Assert.IsTrue(rng.FindNextRngPosition(89));
            Assert.IsTrue(rng.FindNextRngPosition(95));
            Assert.IsFalse(rng.FindNextRngPosition(100));
            Assert.IsTrue(rng.FindNextRngPosition(93));
            Assert.IsTrue(rng.FindNextRngPosition(94));
            Assert.IsTrue(rng.FindNextRngPosition(96));
            Assert.IsFalse(rng.FindNextRngPosition(85));
            Assert.IsTrue(rng.FindNextRngPosition(92));
            Assert.IsTrue(rng.FindNextRngPosition(97));
            Assert.IsTrue(rng.FindNextRngPosition(88));
            Assert.IsFalse(rng.FindNextRngPosition(94));
            Assert.IsTrue(rng.FindNextRngPosition(97));
        }

        [TestMethod]
        public void TestFindNextRngPositionWithMultipleGroupMembers()
        {
            MockRngHelper rng = GetHelper(TestUtils.GetComplexCharacterGroup());
            Assert.IsTrue(rng.FindFirstRngPosition(94));
            Assert.IsTrue(rng.FindNextRngPosition(142));
            Assert.IsTrue(rng.FindNextRngPosition(215));
            Assert.IsTrue(rng.FindNextRngPosition(91));
            Assert.IsTrue(rng.FindNextRngPosition(139));
            Assert.IsTrue(rng.FindNextRngPosition(203));
            Assert.IsTrue(rng.FindNextRngPosition(94));
            Assert.IsTrue(rng.FindNextRngPosition(140));
            Assert.IsTrue(rng.FindNextRngPosition(219));
            Assert.IsTrue(rng.FindNextRngPosition(88));
            Assert.IsTrue(rng.FindNextRngPosition(141));
            Assert.IsFalse(rng.FindNextRngPosition(90));
            Assert.IsFalse(rng.FindNextRngPosition(205));
        }

        [TestMethod]
        public void TestReinitialize()
        {
            MockRngHelper rng = GetHelper();
            Assert.IsTrue(rng.FindFirstRngPosition(92));
            Assert.IsTrue(rng.FindNextRngPosition(95));
            rng.Reinitialize();
            Assert.IsFalse(rng.FindNextRngPosition(88));

        }

        #region helpers
        private static MockRngHelper GetHelper()
        {
            return GetHelper(PlatformType.Ps2, 
                TestUtils.GetSimpleCharacterGroup());
        }

        private static MockRngHelper GetHelper(CharacterGroup group)
        {
            return GetHelper(PlatformType.Ps2, group);
        }

        private static MockRngHelper GetHelper(
            PlatformType platform, CharacterGroup group)
        {
            return new MockRngHelper(platform, group);
        }
        #endregion helpers

        /// <summary>
        /// Concrete wrapper for Abstract class
        /// </summary>
        private class MockRngHelper : BaseRngHelper
        {
            public MockRngHelper(PlatformType platform, CharacterGroup @group) : base(platform, @group)
            {
                // No-op
            }

            protected override void CalculateRngHelper()
            {
                // No-op
            }
        }
    }
}
