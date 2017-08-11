using System.Collections.Generic;
using System.Windows.Forms;
using FF12RNGHelper.Core;

namespace FF12RNGHelper.Forms
{
    public static class FormUtils
    {
        public static void CloseApplication()
        {
            Application.Exit();
        }

        public static int ParseNumRows(string text)
        {
            const int minSupportedRows = 30;
            const int maxSupportedRows = 10000;

            int numRows = int.Parse(text);
            if (numRows < minSupportedRows)
                return minSupportedRows;
            if (numRows > maxSupportedRows)
                return maxSupportedRows;
            return numRows;
        }

        public static void ValidateIntegerTextBox(TextBox tb)
        {
            if (!int.TryParse(tb.Text, out int tempVal))
            {
                tb.Text = FormConstants.IntDefaultValue;
            }
        }

        public static string ConvertDirectionsToText(int index)
        {
            return index == -1
                ? FormConstants.ValueNotYetFound
                : index.ToString();
        }

        public static void UpdateNextHealData(IRngHelper helper, TextBox lastHeal)
        {
            lastHeal.Text = helper.GetNextExpectedHealValue().ToString();
        }

        public static void UpdateComboData(IRngHelper helper, TextBox combo)
        {
            

            int attacksUntilCombo = helper.GetAttacksUntilNextCombo();
            combo.Text = attacksUntilCombo == -1
                ? FormConstants.Safe
                : attacksUntilCombo.ToString();
        }

        public static void LoadCharacter(CharacterGroup group, TextBox levelBox, TextBox magicBox, 
            ComboBox spellPowerBox, CheckBox serenityBox)
        {
            double level = double.Parse(levelBox.Text);
            double magic = double.Parse(magicBox.Text);
            Spells spell = FormConstants.NameToSpellMap[spellPowerBox.SelectedItem.ToString()];
            group.AddCharacter(new Character(level, magic, spell,
                serenityBox.Checked));
        }

        public static PlatformType GetDefaultPlatform()
        {
            return PlatformType.Ps2;
        }
    }

    public static class FormConstants
    {
        public const string ImpossibleHealMsg =
            "Impossible Heal Value Entered";

        public const string AboutMsg =
            "FF12 RNG Helper v1.02\nSo many features, so little time...";

        public const string Safe = "SAFE";

        public const string MalformedError = "Document is malformed";

        public const string FormError = "Invalid Form";

        public const string FileError = "Invalid {0} RNG Save File";

        public const string IntDefaultValue = "0";

        public const string ValueNotYetFound = @"¯\_(ツ)_/¯";

        public static readonly Dictionary<string, Spells> NameToSpellMap =
            new Dictionary<string, Spells>
            {
                {"Cure", Spells.Cure},
                {"Cura", Spells.Cura},
                {"Curaga", Spells.Curaga},
                {"Curaja", Spells.Curaja},
                {"Cura IZJS/TZA", Spells.CuraIzjsTza},
                {"Curaga IZJS/TZA", Spells.CuragaIzjsTza},
                {"Curaja IZJS/TZA", Spells.CurajaIzjsTza},
            };

        public static readonly Dictionary<string, int> NameToIndexMap =
            new Dictionary<string, int>
            {
                {"Cure", 0},
                {"Cura", 1},
                {"Curaga", 2},
                {"Curaja", 3},
                {"Cura IZJS/TZA", 4},
                {"Curaga IZJS/TZA", 5},
                {"Curaja IZJS/TZA", 6},
            };

        public static readonly Dictionary<string, int> PlatformToIndex =
            new Dictionary<string, int>
            {
                {PlatformType.Ps2.ToString().ToUpper(), 0},
                {PlatformType.Ps4.ToString().ToUpper(), 1}
            };
    }
}