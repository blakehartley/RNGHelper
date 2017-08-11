using Microsoft.VisualStudio.TestTools.UnitTesting;
using FF12RNGHelper.Core;

namespace UnitTests
{
    [TestClass]
    public class ChestTests
    {
        [TestMethod]
        public void TestGetRngPosition()
        {
            Chest chest = new Chest(50, 5, 50, 50, 100, false);
            Assert.AreEqual(4, chest.GetRngPosition());
        }

        [TestMethod]
        public void TestCheckIfGil_Success()
        {
            Chest chest = new Chest(50, 5, 50, 50, 100, false);
            Assert.IsTrue(chest.CheckIfGil(25));
        }

        [TestMethod]
        public void TestCheckIfGil_Failure()
        {
            Chest chest = new Chest(50, 5, 50, 50, 100, false);
            Assert.IsFalse(chest.CheckIfGil(75));
        }

        [TestMethod]
        public void TestCheckIfFirstItem_Success()
        {
            Chest chest = new Chest(50, 5, 50, 50, 100, false);
            Assert.IsTrue(chest.CheckIfFirstItem(25));
        }

        [TestMethod]
        public void TestCheckIfFirstItem_Failure()
        {
            Chest chest = new Chest(50, 5, 50, 50, 100, false);
            Assert.IsFalse(chest.CheckIfFirstItem(75));
        }

        #region GetGilAmount

        [TestMethod]
        public void TestGetGilAmount_1()
        {
            Chest chest = new Chest(50, 5, 50, 50, 100, false);
            Assert.AreEqual(76, chest.GetGilAmount(75));
        }

        [TestMethod]
        public void TestGetGilAmount_2()
        {
            Chest chest = new Chest(50, 5, 50, 50, 100, false);
            Assert.AreEqual(100, chest.GetGilAmount(99));
        }

        [TestMethod]
        public void TestGetGilAmount_3()
        {
            Chest chest = new Chest(50, 5, 50, 50, 100, false);
            Assert.AreEqual(1, chest.GetGilAmount(100));
        }

        [TestMethod]
        public void TestGetGilAmount_4()
        {
            Chest chest = new Chest(50, 5, 50, 50, 100, false);
            Assert.AreEqual(2, chest.GetGilAmount(101));
        }

        [TestMethod]
        public void TestGetGilAmount_5()
        {
            Chest chest = new Chest(50, 5, 50, 50, 100, false);
            Assert.AreEqual(63, chest.GetGilAmount(1372235862));
        }

        #endregion GetGilAmount

        [TestMethod]
        public void TestWantItemOne_True()
        {
            Chest chest = new Chest(50, 5, 50, 50, 100, true);
            Assert.IsTrue(chest.WantItemOne());
        }

        [TestMethod]
        public void TestWantItemOne_False()
        {
            Chest chest = new Chest(50, 5, 50, 50, 100, false);
            Assert.IsFalse(chest.WantItemOne());
        }

        [TestMethod]
        public void TestSetGetChestFoundPosition()
        {
            Chest chest = new Chest(50, 5, 50, 50, 100, false);
            Assert.AreEqual(-1, chest.GetChestFoundPosition());
            chest.SetChestFoundPosition(1);
            Assert.AreEqual(1, chest.GetChestFoundPosition());
        }

        [TestMethod]
        public void TestSetGetChestItemPosition()
        {
            Chest chest = new Chest(50, 5, 50, 50, 100, false);
            Assert.AreEqual(-1, chest.GetChestItemPosition());
            chest.SetChestItemPosition(1);
            Assert.AreEqual(1, chest.GetChestItemPosition());
        }

        [TestMethod]
        public void TestHasSetChestSpawned()
        {
            Chest chest = new Chest(50, 5, 50, 50, 100, false);
            Assert.IsFalse(chest.HasChestSpawned());
            chest.SetChestSpawned();
            Assert.IsTrue(chest.HasChestSpawned());
        }

        [TestMethod]
        public void TestIsSetChestFound()
        {
            Chest chest = new Chest(50, 5, 50, 50, 100, false);
            Assert.IsFalse(chest.IsChestFound());
            chest.SetChestFound();
            Assert.IsTrue(chest.IsChestFound());
        }

        [TestMethod]
        public void TestResetSpawnInfo()
        {
            Chest chest = new Chest(50, 5, 50, 50, 100, false);
            chest.SetChestFoundPosition(1);
            chest.SetChestItemPosition(1);
            chest.SetChestSpawned();
            chest.SetChestFound();
            chest.ResetSpawnInfo();
            Assert.AreEqual(-1, chest.GetChestFoundPosition());
            Assert.AreEqual(-1, chest.GetChestItemPosition());
            Assert.IsFalse(chest.HasChestSpawned());
            Assert.IsFalse(chest.IsChestFound());
        }
    }
}
