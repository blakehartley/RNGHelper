using FF12RNGHelper.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class StealTests
    {
        [TestMethod]
        public void CheckModulo()
        {
            Assert.AreEqual(
                "Rare",
                Steal.CheckSteal(100, 0, 0));
        }

        #region CheckSteal

        [TestMethod]
        public void CheckSteal_StealRare()
        {
            Assert.AreEqual(
                "Rare",
                Steal.CheckSteal(0, 0, 0));
        }

        [TestMethod]
        public void CheckSteal_StealUncommon()
        {
            Assert.AreEqual(
                "Uncommon",
                Steal.CheckSteal(99, 0, 0));
        }

        [TestMethod]
        public void CheckSteal_StealCommon()
        {
            Assert.AreEqual(
                "Common",
                Steal.CheckSteal(99, 99, 0));
        }

        [TestMethod]
        public void CheckSteal_StealNone()
        {
            Assert.AreEqual(
                "None",
                Steal.CheckSteal(99, 99, 99));
        }

        [TestMethod]
        public void CheckSteal_StealRare_EdgeCase()
        {
            Assert.AreEqual(
                "Rare",
                Steal.CheckSteal(2, 0, 0));
        }

        [TestMethod]
        public void CheckSteal_StealUncommon_EdgeCase()
        {
            Assert.AreEqual(
                "Uncommon",
                Steal.CheckSteal(3, 9, 0));
        }

        [TestMethod]
        public void CheckSteal_StealCommon_EdgeCase()
        {
            Assert.AreEqual(
                "Common",
                Steal.CheckSteal(3, 10, 54));
        }

        [TestMethod]
        public void CheckSteal_StealNone_EdgeCase()
        {
            Assert.AreEqual(
                "None",
                Steal.CheckSteal(3, 10, 55));
        }

        #endregion CheckSteal

        #region CheckStealCuffs

        [TestMethod]
        public void CheckStealCuffs_StealRare()
        {
            Assert.AreEqual(
                "Rare",
                Steal.CheckStealCuffs(0, 99, 99));
        }

        [TestMethod]
        public void CheckStealCuffs_StealRareUncommon()
        {
            Assert.AreEqual(
                "Rare + Uncommon",
                Steal.CheckStealCuffs(0, 0, 99));
        }

        [TestMethod]
        public void CheckStealCuffs_StealRareUncommonCommon()
        {
            Assert.AreEqual(
                "Rare + Uncommon + Common",
                Steal.CheckStealCuffs(0, 0, 0));
        }

        [TestMethod]
        public void CheckStealCuffs_StealRareCommon()
        {
            Assert.AreEqual(
                "Rare + Common",
                Steal.CheckStealCuffs(0, 99, 0));
        }

        [TestMethod]
        public void CheckStealCuffs_StealUncommon()
        {
            Assert.AreEqual(
                "Uncommon",
                Steal.CheckStealCuffs(99, 0, 99));
        }

        [TestMethod]
        public void CheckStealCuffs_StealUncommonCommon()
        {
            Assert.AreEqual(
                "Uncommon + Common",
                Steal.CheckStealCuffs(99, 0, 0));
        }

        [TestMethod]
        public void CheckStealCuffs_StealCommon()
        {
            Assert.AreEqual(
                "Common",
                Steal.CheckStealCuffs(99, 99, 0));
        }

        [TestMethod]
        public void CheckStealCuffs_StealNone()
        {
            Assert.AreEqual(
                "None",
                Steal.CheckStealCuffs(99, 99, 99));
        }

        #endregion CheckStealCuffs
    }
}
