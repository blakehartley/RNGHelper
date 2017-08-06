using System;

namespace FF12RNGHelper
{
    /// <summary>
    /// The character class encapsulates all of the necessary information required
    /// to calculate the damage of a single spell whose power is determined at
    /// creation.
    /// </summary>
    // TO-DO: Fix the naming as this class really encapsulates a single instance 
    //        of a spell cast.
    public class Character
    {
        //Constants
        private const double MaxBonusMultiplier = 0.125; // Goku-mode.
        private const double MinBonusMultiplier = 0; // Not much of a bonus. :(
        private const double NoSerenityBoost = 1.0;
        private const double SerenityBoost = 1.5;

        // Character Info
        private readonly double _level;
        private readonly double _magic;
        private readonly double _spellPower;
        private readonly double _serenityMult;
        private readonly Spell _spell;

        [Obsolete]
        public Character(double level, double magic, double spellpower, bool serenity)
        {
            _level = level;
            _magic = magic;
            _spellPower = spellpower;
            _serenityMult = serenity ? SerenityBoost : NoSerenityBoost;
        }

        public Character(double level, double magic, Spells spell, bool serenity)
        {
            _level = level;
            _magic = magic;
            _spell = new Spell(spell);
            _spellPower = _spell.GetSpellPower();
            _serenityMult = serenity ? SerenityBoost : NoSerenityBoost;
        }

        /// <summary>
        /// Calculates the HP value of the next spell based on the RNG value.
        /// </summary>
        /// <param name="rngValue">RNG value from PRNG</param>
        public int GetHealValue(uint rngValue)
        {
            double bonusSpellPower = rngValue % Math.Floor(_spellPower * 12.5) / 100.0;
            return CalculateHeal(bonusSpellPower);
        }

        /// <summary>
        /// Calculates the highest possible heal value based on current stats.
        /// </summary>
        public int HealMax()
        {
            double bonusSpellPower = MaxBonusMultiplier * _spellPower;
            return CalculateHeal(bonusSpellPower);
        }

        /// <summary>
        /// Calculates the lowest possible heal value based on current stats.
        /// </summary>
        /// <returns></returns>
        public int HealMin()
        {
            return CalculateHeal(MinBonusMultiplier);
        }

        /// <summary>
        /// Calculates the value of the spell based on stats and bonus spell 
        /// power based on PRNG.
        /// </summary>
        /// <param name="bonusSpellPower">Bonus spell power to be added to 
        /// base spell power.</param>
        /// <returns></returns>
        private int CalculateHeal(double bonusSpellPower)
        {
            double totalSpellPower = _spellPower + bonusSpellPower;
            double regularDamage = totalSpellPower * 
                (2.0 + _magic * (_level + _magic) / 256.0);
            double finalDamage = regularDamage * _serenityMult;
            return (int) finalDamage;
        }
    }
}
