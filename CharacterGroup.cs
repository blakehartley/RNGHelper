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
    protected int index;

    public CharacterGroup()
    {
      this.characters = new List<Character>();
      this.index = 0;
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
      this.index = 0;
    }

    public int GetIndex()
    {
      return this.index;
    }

    public void SetIndex(int i)
    {
      this.index = i;
    }

    public uint GetHealValue(uint rngValue)
    {
      uint healValue = this.characters[this.index].GetHealValue(rngValue);
      if (this.index == this.characters.Count - 1)
        this.index = 0;
      else
        ++this.index;
      return healValue;
    }

    public uint HealMax()
    {
      return this.characters[this.index].HealMax();
    }

    public uint HealMin()
    {
      return this.characters[this.index].HealMin();
    }

    public bool ValidateHealValue(uint value)
    {
      if (value <= this.characters[this.index].HealMax())
        return value >= this.characters[this.index].HealMin();
      return false;
    }
  }
}
