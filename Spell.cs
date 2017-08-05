using System;
using System.Windows.Forms;

namespace FF12RNGHelper
{
    /// <summary>
    /// Encapsulates a single cure spell
    /// </summary>
    [Obsolete("Use Spell2 class")]
    class Spell
    {
        private string Name;
        private Spells spell;
        private int Power;

        public Spell(ComboBox spell)
        {
            initialize(spell.SelectedItem.ToString());
        }

        private void initialize(string name)
        {
            Name = name;
            spell = (Spells)Enum.Parse(typeof(Spells),
                name.Replace(" ", "").TrimEnd(new char[] { '/', 'Z', 'A' }));
            Power = spell.getSpellPower();
        }

        public int getPower()
        {
            return Power;
        }
    }

    /// <summary>
    /// A class encapsulating all possible cure spells
    /// and their potencies
    /// </summary>
    enum Spells : int
    {
        Cure = 20,
        Cura = 45,
        Curaga = 85,
        Curaja = 145,
        CuraIZJS = 46,
        CuragaIZJS = 86,
        CurajaIZJS = 120
    }

    /// <summary>
    /// Extension methods for the spells enum.
    /// Allows the caller to cleanly get the spellpower.
    /// </summary>
    static class SpellsMethods
    {
        public static int getSpellPower(this Spells spell)
        {
            return (int)spell;
        }
    }
}
