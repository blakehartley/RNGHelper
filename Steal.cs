using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]

namespace FF12RNGHelper
{
    /// <summary>
    /// This class encapsulates a single steal oportunity
    /// </summary>
    internal static class Steal
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
        public static string CheckSteal(uint prng1, uint prng2, uint prng3)
        {
            if (StealSuccessful(prng1, RareChance))
            {
                return Rare;
            }
            if (StealSuccessful(prng2, UncommonChance))
            {
                return Uncommon;
            }
            if (StealSuccessful(prng3, CommonChance))
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
        public static string CheckStealCuffs(uint prng1, uint prng2, uint prng3)
        {
            string returnStr = string.Empty;

            if (StealSuccessful(prng1, RareChanceCuffs))
            {
                returnStr += Rare;
            }
            if (StealSuccessful(prng2, UncommonChanceCuffs))
            {
                returnStr += Linker + Uncommon;
            }
            if (StealSuccessful(prng3, CommonChanceCuffs))
            {
                returnStr += Linker + Common;
            }
            if (returnStr == string.Empty)
            {
                returnStr = None;
            }
            return returnStr.TrimStart(Linker.ToCharArray());
        }

        /// <summary>
        /// Calculate if a steal attempt was successful
        /// </summary>
        private static bool StealSuccessful(uint prng, int chance)
        {
            return RandToPercent(prng) < chance;
        }

        /// <summary>
        /// Convert an RNG value into a percentage
        /// </summary>
        private static int RandToPercent(uint toConvert)
        {
            return (int) (toConvert % 100);
        }
    }
}
