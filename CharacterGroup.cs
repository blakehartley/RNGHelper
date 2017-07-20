// Decompiled with JetBrains decompiler
// Type: FF12RNGHelper.CharacterGroup
// Assembly: FF12RNGHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11070DA2-2CBB-49ED-8F82-7EE610A4DB07
// Assembly location: C:\Users\TyCobb\Downloads\FF12RNGHelper.exe

using System.Collections.Generic;

namespace FF12RNGHelper
{
	public class CharacterGroup
	{
	protected List<Character> characters;
	protected int charIndex;

	public CharacterGroup()
	{
		this.characters = new List<Character>();
		this.charIndex = 0;
	}

	public void ClearCharacters()
	{
		this.characters.Clear();
	}

	public int CharacterCount()
	{
		return this.characters.Count;
	}

	public void AddCharacter(Character c)
	{
		this.characters.Add(c);
	}

	public void ResetIndex()
	{
		this.charIndex = 0;
	}

    public int GetIndex()
	{
		return this.charIndex;
	}

    public void SetIndex(int i)
	{
		this.charIndex = i;
	}
	
	public void IncrimentIndex()
	{
		this.charIndex = (this.charIndex + 1) % this.CharacterCount();
	}

	public uint GetHealValue(uint rngValue)
	{
		uint healValue = this.characters[this.charIndex].GetHealValue(rngValue);
		IncrimentIndex();
		/*if (this.charIndex == this.characters.Count - 1)
		this.charIndex = 0;
		else
		++this.charIndex;*/
		return healValue;
	}

	public uint PeekHealValue(uint rngValue)
	{
		uint healValue = this.characters[this.charIndex].GetHealValue(rngValue);
		/*if (this.index == this.characters.Count - 1)
			this.index = 0;
		else
			++this.index;*/
		return healValue;
	}

	public uint HealMax()
    {
      return this.characters[this.charIndex].HealMax();
    }

    public uint HealMin()
    {
      return this.characters[this.charIndex].HealMin();
    }

    public bool ValidateHealValue(uint value)
    {
      if (value <= this.characters[this.charIndex].HealMax())
        return value >= this.characters[this.charIndex].HealMin();
      return false;
    }
  }
}
