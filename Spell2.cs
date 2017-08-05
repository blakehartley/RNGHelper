using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]

namespace FF12RNGHelper
{
    /// <summary>
    /// This class encapsulates the cure spells
    /// </summary>
    internal class Spell2
    {
        private readonly Spells2 _spell;
        private readonly int _power;

        // Convenient conversion map for displaying names.
        private static readonly Dictionary<Spells2, string> SpellToNameMap =
            new Dictionary<Spells2, string>
            {
                {Spells2.Cure, "Cure"},
                {Spells2.Cura, "Cura"},
                {Spells2.Curaga, "Curaga"},
                {Spells2.Curaja, "Curaja"},
                {Spells2.CuraIzjsTza, "Cura IZJS/TZA"},
                {Spells2.CuragaIzjsTza, "Curaga IZJS/TZA"},
                {Spells2.CurajaIzjsTza, "Curaja IZJS/TZA"}
            };

        // Map spells to their spell power
        private static readonly Dictionary<Spells2, int> SpellsToPowerMap =
            new Dictionary<Spells2, int>
            {
                {Spells2.Cure, 20},
                {Spells2.Cura, 45},
                {Spells2.Curaga, 85},
                {Spells2.Curaja, 145},
                {Spells2.CuraIzjsTza, 46},
                {Spells2.CuragaIzjsTza, 86},
                {Spells2.CurajaIzjsTza, 120},
            };

        public Spell2(Spells2 spell)
        {
            _spell = spell;
            _power = SpellsToPowerMap[_spell];
        }

        /// <summary>
        /// Get the power of the spell
        /// </summary>
        public int GetSpellPower()
        {
            return _power;
        }

        /// <summary>
        /// Get the name of the spell
        /// </summary>
        public string GetName()
        {
            return SpellToNameMap[_spell];
        }
    }
}