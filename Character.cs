// Decompiled with JetBrains decompiler
// Type: FF12RNGHelper.Character
// Assembly: FF12RNGHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11070DA2-2CBB-49ED-8F82-7EE610A4DB07
// Assembly location: C:\Users\TyCobb\Downloads\FF12RNGHelper.exe

namespace FF12RNGHelper
{
  public class Character
  {
    public double Level { get; set; }

    public double Magic { get; set; }

    public double SpellPower { get; set; }

    public double SerenityMult { get; set; }

    public Character(double level, double magic, double spellpower, double serenitymult)
    {
      this.Level = level;
      this.Magic = magic;
      this.SpellPower = spellpower;
      this.SerenityMult = serenitymult;
    }

    public uint GetHealValue(uint rngValue)
    {
      return (uint) ((this.SpellPower + (double) rngValue % (this.SpellPower * 12.5) / 100.0) * (2.0 + this.Magic * (this.Level + this.Magic) / 256.0) * this.SerenityMult);
    }

    public uint HealMax()
    {
      return (uint) (this.SpellPower * 1.125 * (2.0 + this.Magic * (this.Level + this.Magic) / 256.0) * this.SerenityMult);
    }

    public uint HealMin()
    {
      return (uint) (this.SpellPower * (2.0 + this.Magic * (this.Level + this.Magic) / 256.0) * this.SerenityMult);
    }
  }
}
