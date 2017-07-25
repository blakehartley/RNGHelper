using System;

namespace FF12RNGHelper
{
    /// <summary>
    /// This class encapsulates a single steal oportunity
    /// </summary>
    class Steal
    {
        // Strings for display in the UI
        private const string Rare = "Rare";
        private const string Uncommon = "Uncommon";
        private const string Common = "Common";
        private const string None = "None";
        private const string Linker = " + ";

        // Steal chances
        private const int CommonChance = 55;
        private const int UncommonChance = 10;
        private const int RareChance = 3;
        private const int CommonChanceCuffs = 80;
        private const int UncommonChanceCuffs = 30;
        private const int RareChanceCuffs = 6;

        /// <summary>
        /// Check if you steal anything while not wearing the Thief's Cuffs
        /// When not wearing Thief's Cuffs you may only steal one item.
        /// Once you are successful, you get that item and that's it.
        /// </summary>
        public string checkSteal(uint PRNG1, uint PRNG2, uint PRNG3)
        {
            if (stealSuccessful(PRNG1, RareChance))
            {
                return Rare;
            }
            if (stealSuccessful(PRNG2, UncommonChance))
            {
                return Uncommon;
            }
            if (stealSuccessful(PRNG3, CommonChance))
            {
                return Common;
            }
            return None;
        }

        /// <summary>
        /// Check if you steal anything while wearing the Thief's Cuffs
        /// When not wearing Thief's Cuffs you may steal more than one
        /// item, and you have better odds. Roll against all 3 and get
        /// everything you successfully steal.
        /// </summary>
        public string checkStealCuffs(uint PRNG1, uint PRNG2, uint PRNG3)
        {
            string returnStr = String.Empty;

            if (stealSuccessful(PRNG1, RareChanceCuffs))
            {
                returnStr += Rare;
            }
            if (stealSuccessful(PRNG2, UncommonChanceCuffs))
            {
                returnStr += Linker + Uncommon;
            }
            if (stealSuccessful(PRNG3, CommonChanceCuffs))
            {
                returnStr += Linker + Common;
            }
            if (returnStr == String.Empty)
            {
                returnStr = None;
            }
            return returnStr.TrimStart(Linker.ToCharArray());
        }

        /// <summary>
        /// Calculate if a steal attempt was successful
        /// </summary>
        private bool stealSuccessful(uint PRNG, int chance)
        {
            return randToPercent(PRNG) < chance;
        }

        /// <summary>
        /// Convert an RNG value into a percentage
        /// </summary>
        private int randToPercent(uint toConvert)
        {
            return (int) (toConvert % 100);
        }
    }
}
