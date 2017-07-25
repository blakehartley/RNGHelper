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
        private double Level;
        private double Magic;
        private double SpellPower;
        private double SerenityMult;

        public Character(double level, double magic, double spellpower, bool serenity)
        {
            Level = level;
            Magic = magic;
            SpellPower = spellpower;
            SerenityMult = serenity ? SerenityBoost : NoSerenityBoost;
        }

        /// <summary>
        /// Calculates the HP value of the next spell based on the RNG value.
        /// </summary>
        /// <param name="rngValue">RNG value from PRNG</param>
        public int GetHealValue(uint rngValue)
        {
            double bonusSpellPower = (double)rngValue % Math.Floor(SpellPower * 12.5) / 100.0;
            return calculateHeal(bonusSpellPower);
        }

        /// <summary>
        /// Calculates the highest possible heal value based on current stats.
        /// </summary>
        public int HealMax()
        {
            double bonusSpellPower = MaxBonusMultiplier * SpellPower;
            return calculateHeal(bonusSpellPower);
        }

        /// <summary>
        /// Calculates the lowest possible heal value based on current stats.
        /// </summary>
        /// <returns></returns>
        public int HealMin()
        {
            return calculateHeal(MinBonusMultiplier);
        }

        /// <summary>
        /// Calculates the value of the spell based on stats and bonus spell power based on PRNG.
        /// </summary>
        /// <param name="bonusSpellPower">Bonus spell power to be added to base spell power.</param>
        /// <returns></returns>
        private int calculateHeal(double bonusSpellPower)
        {
            return (int)((SpellPower + bonusSpellPower) * (2.0 + Magic * (Level + Magic) / 256.0) * SerenityMult);
        }
    }
}
