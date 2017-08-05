using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]
namespace FF12RNGHelper
{
    /// <summary>
    /// This class calculates the number of combos
    /// in an attack
    /// </summary>
    internal static class Combo
    {
        // Public constants
        public const int IndexOfPreviousComboRng = 5;

        public const int RngConsumedForAttack = 10;

        // Private constants
        private const int ComboChance = 3;

        /// <summary>
        /// Perform a ptest to see if we combo
        /// </summary>
        public static bool IsSucessful(uint prng)
        {
            return (prng % 100) < ComboChance;
        }
    }
}
