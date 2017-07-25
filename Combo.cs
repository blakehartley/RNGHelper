namespace FF12RNGHelper
{
    /// <summary>
    /// This class calculates the number of combos
    /// in an attack
    /// </summary>
    static class Combo
    {
        private const int ComboChance = 3;

        /// <summary>
        /// Perform a ptest to see if we combo
        /// </summary>
        public static bool IsSucessful(uint PRNG)
        {
            return (PRNG % 100) < ComboChance;
        }
    }
}
