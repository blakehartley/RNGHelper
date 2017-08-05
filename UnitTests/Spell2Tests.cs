using Microsoft.VisualStudio.TestTools.UnitTesting;
using FF12RNGHelper;

namespace UnitTests
{
    [TestClass]
    public class Spell2Tests
    {
        [TestMethod]
        public void TestSpellPower()
        {
            Spell2 cure = new Spell2(Spells2.Cure);
            Assert.AreEqual(20, cure.GetSpellPower());

            Spell2 cura = new Spell2(Spells2.Cura);
            Assert.AreEqual(45, cura.GetSpellPower());

            Spell2 curaga = new Spell2(Spells2.Curaga);
            Assert.AreEqual(85, curaga.GetSpellPower());

            Spell2 curaja = new Spell2(Spells2.Curaja);
            Assert.AreEqual(145, curaja.GetSpellPower());

            Spell2 curaIzjsTza = new Spell2(Spells2.CuraIzjsTza);
            Assert.AreEqual(46, curaIzjsTza.GetSpellPower());

            Spell2 curagaIzjsTza = new Spell2(Spells2.CuragaIzjsTza);
            Assert.AreEqual(86, curagaIzjsTza.GetSpellPower());

            Spell2 curajaIzjsTza = new Spell2(Spells2.CurajaIzjsTza);
            Assert.AreEqual(120, curajaIzjsTza.GetSpellPower());
        }

        [TestMethod]
        public void TestNames()
        {
            Spell2 cure = new Spell2(Spells2.Cure);
            Assert.AreEqual("Cure", cure.GetName());

            Spell2 cura = new Spell2(Spells2.Cura);
            Assert.AreEqual("Cura", cura.GetName());

            Spell2 curaga = new Spell2(Spells2.Curaga);
            Assert.AreEqual("Curaga", curaga.GetName());

            Spell2 curaja = new Spell2(Spells2.Curaja);
            Assert.AreEqual("Curaja", curaja.GetName());

            Spell2 curaIzjsTza = new Spell2(Spells2.CuraIzjsTza);
            Assert.AreEqual("Cura IZJS/TZA", curaIzjsTza.GetName());

            Spell2 curagaIzjsTza = new Spell2(Spells2.CuragaIzjsTza);
            Assert.AreEqual("Curaga IZJS/TZA", curagaIzjsTza.GetName());

            Spell2 curajaIzjsTza = new Spell2(Spells2.CurajaIzjsTza);
            Assert.AreEqual("Curaja IZJS/TZA", curajaIzjsTza.GetName());
        }
    }
}
