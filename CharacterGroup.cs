using System.Collections.Generic;

namespace FF12RNGHelper
{
    /// <summary>
    /// This class encapsulates a group of characters casting spells
    /// in order by index.
    /// </summary>
    public class CharacterGroup
    {
        // private members
        private List<Character> characters;
        private int charIndex;

        public CharacterGroup()
        {
            characters = new List<Character>();
            charIndex = 0;
        }

        /// <summary>
        /// Clear the character list
        /// </summary>
        public void ClearCharacters()
        {
            characters.Clear();
        }

        /// <summary>
        /// Get the character count
        /// </summary>
        public int CharacterCount()
        {
            return characters.Count;
        }

        /// <summary>
        /// Add a character to the group
        /// </summary>
        /// <param name="newCharacter">The character to add</param>
        public void AddCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
        }

        /// <summary>
        /// Resets the spell casting order starting with the first
        /// character
        /// </summary>
        public void ResetIndex()
        {
            charIndex = 0;
        }

        /// <summary>
        /// Get the current index of the character casting a spell
        /// </summary>
        public int GetIndex()
        {
            return charIndex;
        }

        /// <summary>
        /// Set the current index of the character casting a spell
        /// </summary>
        /// <param name="i"></param>
        public void SetIndex(int i)
        {
            charIndex = i;
        }

        /// <summary>
        /// Increment the index of the character casting a spell
        /// </summary>
        public void IncrimentIndex()
        {
            charIndex = (charIndex + 1) % CharacterCount();
        }

        /// <summary>
        /// Cast the next spell. Get the value of the heal
        /// </summary>
        /// <param name="rngValue">RNG value supplied by PRNG</param>
        public int GetHealValue(uint rngValue)
        {
            int healValue = characters[charIndex].GetHealValue(rngValue);
            IncrimentIndex();
            return healValue;
        }

        /// <summary>
        /// Get the value of the next heal without casting
        /// </summary>
        /// <param name="rngValue">RNG value supplied by PRNG</param>
        public int PeekHealValue(uint rngValue)
        {
            return characters[charIndex].GetHealValue(rngValue);
        }

        /// <summary>
        /// Get the maximum possible value of the current characters 
        /// next spell
        /// </summary>
        public int HealMax()
        {
            return characters[charIndex].HealMax();
        }

        /// <summary>
        /// Get the minimum possible value of the current characters 
        /// next spell
        /// </summary>
        public int HealMin()
        {
            return characters[charIndex].HealMin();
        }

        /// <summary>
        /// Verify if a heal value is within range of what the current
        /// character is capable of
        /// </summary>
        /// <param name="value"></param>
        public bool ValidateHealValue(int value)
        {
            return value <= HealMax() && value >= HealMin();
        }
    }
}
