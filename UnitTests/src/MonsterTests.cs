using Microsoft.VisualStudio.TestTools.UnitTesting;
using FF12RNGHelper.Core;

namespace UnitTests
{
    [TestClass]
    public class MonsterTests
    {
        [TestMethod]
        public void TestGetRngPosition()
        {
            Monster monster = TestUtils.GetDefaultMonster();
            Assert.AreEqual(1, monster.GetRngPosition());
        }

        [TestMethod]
        public void TestSetGetMonsterFoundPosition()
        {
            Monster monster = TestUtils.GetDefaultMonster();
            Assert.AreEqual(-1, monster.GetMonsterFoundPosition());
            monster.SetMonsterFoundPosition(1);
            Assert.AreEqual(1, monster.GetMonsterFoundPosition());
        }

        [TestMethod]
        public void TestHasSetMonsterSpawned()
        {
            Monster monster = TestUtils.GetDefaultMonster();
            Assert.IsFalse(monster.HasMonsterSpawned());
            monster.SetMonsterSpawned();
            Assert.IsTrue(monster.HasMonsterSpawned());
        }

        [TestMethod]
        public void TestIsSetMonsterFound()
        {
            Monster monster = TestUtils.GetDefaultMonster();
            Assert.IsFalse(monster.HasMonsterSpawned());
            monster.SetMonsterSpawned();
            Assert.IsTrue(monster.HasMonsterSpawned());
        }

        [TestMethod]
        public void TestResetSpawnInfo()
        {
            Monster monster = TestUtils.GetDefaultMonster();
            monster.SetMonsterFoundPosition(1);
            monster.SetMonsterSpawned();
            monster.ResetSpawnInfo();
            Assert.AreEqual(-1, monster.GetMonsterFoundPosition());
            Assert.IsFalse(monster.HasMonsterSpawned());
        }

        [TestMethod]
        public void TestSpawnCheck_Success()
        {
            Monster monster = TestUtils.GetDefaultMonster();
            Assert.IsTrue(monster.SpawnCheck(0.011));
            Assert.IsTrue(monster.SpawnCheck(0.1836412));
            Assert.IsTrue(monster.SpawnCheck(0.0199));
        }

        [TestMethod]
        public void TestSpawnCheck_Failure()
        {
            Monster monster = TestUtils.GetDefaultMonster();
            Assert.IsFalse(monster.SpawnCheck(0.01));
            Assert.IsFalse(monster.SpawnCheck(1));
            Assert.IsFalse(monster.SpawnCheck(0.2));
        }
    }
}
