using Microsoft.VisualStudio.TestTools.UnitTesting;
using FF12RNGHelper;

namespace UnitTests
{
    [TestClass]
    public class SpellTests
    {
        [TestMethod]
        public void TestSpellPower()
        {
            Spell cure = new Spell(Spells.Cure);
            Assert.AreEqual(20, cure.GetSpellPower());

            Spell cura = new Spell(Spells.Cura);
            Assert.AreEqual(45, cura.GetSpellPower());

            Spell curaga = new Spell(Spells.Curaga);
            Assert.AreEqual(85, curaga.GetSpellPower());

            Spell curaja = new Spell(Spells.Curaja);
            Assert.AreEqual(145, curaja.GetSpellPower());

            Spell curaIzjsTza = new Spell(Spells.CuraIzjsTza);
            Assert.AreEqual(46, curaIzjsTza.GetSpellPower());

            Spell curagaIzjsTza = new Spell(Spells.CuragaIzjsTza);
            Assert.AreEqual(86, curagaIzjsTza.GetSpellPower());

            Spell curajaIzjsTza = new Spell(Spells.CurajaIzjsTza);
            Assert.AreEqual(120, curajaIzjsTza.GetSpellPower());
        }

        [TestMethod]
        public void TestNames()
        {
            Spell cure = new Spell(Spells.Cure);
            Assert.AreEqual("Cure", cure.GetName());

            Spell cura = new Spell(Spells.Cura);
            Assert.AreEqual("Cura", cura.GetName());

            Spell curaga = new Spell(Spells.Curaga);
            Assert.AreEqual("Curaga", curaga.GetName());

            Spell curaja = new Spell(Spells.Curaja);
            Assert.AreEqual("Curaja", curaja.GetName());

            Spell curaIzjsTza = new Spell(Spells.CuraIzjsTza);
            Assert.AreEqual("Cura IZJS/TZA", curaIzjsTza.GetName());

            Spell curagaIzjsTza = new Spell(Spells.CuragaIzjsTza);
            Assert.AreEqual("Curaga IZJS/TZA", curagaIzjsTza.GetName());

            Spell curajaIzjsTza = new Spell(Spells.CurajaIzjsTza);
            Assert.AreEqual("Curaja IZJS/TZA", curajaIzjsTza.GetName());
        }
    }
}
