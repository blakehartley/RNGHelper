using Microsoft.VisualStudio.TestTools.UnitTesting;
using FF12RNGHelper;

namespace UnitTests
{
    [TestClass]
    public class CharacterTests
    {
        #region even spell power

        [TestMethod]
        public void TestCureHealMax_NoSerenity()
        {
            Character character = new Character(3, 23, Spells2.Cure, false);
            Assert.AreEqual(97, character.HealMax());
        }

        [TestMethod]
        public void TestCureHealMax_Serenity()
        {
            Character character = new Character(3, 23, Spells2.Cure, true);
            Assert.AreEqual(146, character.HealMax());
        }

        [TestMethod]
        public void TestCureHealMin_NoSerenity()
        {
            Character character = new Character(3, 23, Spells2.Cure, false);
            Assert.AreEqual(86, character.HealMin());
        }

        [TestMethod]
        public void TestCureHealMin_Serenity()
        {
            Character character = new Character(3, 23, Spells2.Cure, true);
            Assert.AreEqual(130, character.HealMin());
        }

        [TestMethod]
        public void TestCureGetHealValue_NoSerenity()
        {
            Character character = new Character(3, 23, Spells2.Cure, false);
            Assert.AreEqual(91, character.GetHealValue(1372235862));
        }

        [TestMethod]
        public void TestCureGetHealValue_Serenity()
        {
            Character character = new Character(3, 23, Spells2.Cure, true);
            Assert.AreEqual(137, character.GetHealValue(1372235862));
        }

        #endregion even spell power

        #region odd spell power

        [TestMethod]
        public void TestCuraHealMax_NoSerenity()
        {
            Character character = new Character(3, 23, Spells2.Cura, false);
            Assert.AreEqual(219, character.HealMax());
        }

        [TestMethod]
        public void TestCuraHealMax_Serenity()
        {
            Character character = new Character(3, 23, Spells2.Cura, true);
            Assert.AreEqual(329, character.HealMax());
        }

        [TestMethod]
        public void TestCuraHealMin_NoSerenity()
        {
            Character character = new Character(3, 23, Spells2.Cura, false);
            Assert.AreEqual(195, character.HealMin());
        }

        [TestMethod]
        public void TestCuraHealMin_Serenity()
        {
            Character character = new Character(3, 23, Spells2.Cura, true);
            Assert.AreEqual(292, character.HealMin());
        }

        [TestMethod]
        public void TestCuraGetHealValue_NoSerenity()
        {
            Character character = new Character(3, 23, Spells2.Cura, false);
            Assert.AreEqual(215, character.GetHealValue(1372235862));
        }

        [TestMethod]
        public void TestCuraGetHealValue_Serenity()
        {
            Character character = new Character(3, 23, Spells2.Cura, true);
            Assert.AreEqual(322, character.GetHealValue(1372235862));
        }

        #endregion odd spell power
    }
}
