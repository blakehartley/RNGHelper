using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]

namespace FF12RNGHelper.Core
{
    /// <summary>
    /// This class encapsulates the cure spells
    /// </summary>
    internal class Spell
    {
        private readonly Spells _spell;
        private readonly int _power;

        // Convenient conversion map for displaying names.
        private static readonly Dictionary<Spells, string> SpellToNameMap =
            new Dictionary<Spells, string>
            {
                {Spells.Cure, "Cure"},
                {Spells.Cura, "Cura"},
                {Spells.Curaga, "Curaga"},
                {Spells.Curaja, "Curaja"},
                {Spells.CuraIzjsTza, "Cura IZJS/TZA"},
                {Spells.CuragaIzjsTza, "Curaga IZJS/TZA"},
                {Spells.CurajaIzjsTza, "Curaja IZJS/TZA"}
            };

        // Map spells to their spell power
        private static readonly Dictionary<Spells, int> SpellsToPowerMap =
            new Dictionary<Spells, int>
            {
                {Spells.Cure, 20},
                {Spells.Cura, 45},
                {Spells.Curaga, 85},
                {Spells.Curaja, 145},
                {Spells.CuraIzjsTza, 46},
                {Spells.CuragaIzjsTza, 86},
                {Spells.CurajaIzjsTza, 120},
            };

        public Spell(Spells spell)
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