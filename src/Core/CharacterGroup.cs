using System;
using System.Collections.Generic;

namespace FF12RNGHelper.Core
{
    /// <summary>
    /// This class encapsulates a group of characters casting spells
    /// in order by index.
    /// </summary>
    public class CharacterGroup
    {
        // private members
        private readonly List<Character> _characters;
        private int _charIndex;

        public CharacterGroup()
        {
            _characters = new List<Character>();
            _charIndex = 0;
        }

        /// <summary>
        /// Clear the character list
        /// </summary>
        public void ClearCharacters()
        {
            _characters.Clear();
        }

        /// <summary>
        /// Get the character count
        /// </summary>
        public int CharacterCount()
        {
            return _characters.Count;
        }

        /// <summary>
        /// Add a character to the group
        /// </summary>
        /// <param name="newCharacter">The character to add</param>
        public void AddCharacter(Character newCharacter)
        {
            _characters.Add(newCharacter);
        }

        /// <summary>
        /// Resets the spell casting order starting with the first
        /// character
        /// </summary>
        public void ResetIndex()
        {
            _charIndex = 0;
        }

        /// <summary>
        /// Get the current index of the character casting a spell
        /// </summary>
        public int GetIndex()
        {
            return _charIndex;
        }

        /// <summary>
        /// Set the current index of the character casting a spell
        /// </summary>
        /// <param name="i"></param>
        public void SetIndex(int i)
        {
            if (i >= _characters.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            _charIndex = i;
        }

        /// <summary>
        /// Increment the index of the character casting a spell
        /// </summary>
        public void IncrimentIndex()
        {
            _charIndex = (_charIndex + 1) % CharacterCount();
        }

        /// <summary>
        /// Cast the next spell. Get the value of the heal
        /// </summary>
        /// <param name="rngValue">RNG value supplied by PRNG</param>
        public int GetHealValue(uint rngValue)
        {
            int healValue = _characters[_charIndex].GetHealValue(rngValue);
            IncrimentIndex();
            return healValue;
        }

        /// <summary>
        /// Get the value of the next heal without casting
        /// </summary>
        /// <param name="rngValue">RNG value supplied by PRNG</param>
        public int PeekHealValue(uint rngValue)
        {
            return _characters[_charIndex].GetHealValue(rngValue);
        }

        /// <summary>
        /// Get the maximum possible value of the current characters 
        /// next spell
        /// </summary>
        public int HealMax()
        {
            return _characters[_charIndex].HealMax();
        }

        /// <summary>
        /// Get the minimum possible value of the current characters 
        /// next spell
        /// </summary>
        public int HealMin()
        {
            return _characters[_charIndex].HealMin();
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
