using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FF12RNGHelper.Core;

namespace FF12RNGHelper.Forms
{
    public partial class FormSteal2 : Form
    {
        #region constants

        private const string ImpossibleHealMsg =
            "Impossible Heal Value Entered";

        private const string AboutMsg =
            "FF12 RNG Helper v1.02\nSo many features, so little time...";

        private const string IntDefaultValue = "0";

        private static readonly Dictionary<string, Spells> NameToSpellMap =
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

        #endregion constants

        #region internal state

        private StealRngHelper _rngHelper;
        private StealFutureRng _futureRng;
        private CharacterGroup _group = new CharacterGroup();
        private PlatformType _platform = PlatformType.Ps2;

        #endregion internal state

        #region construction/initialization

        public FormSteal2()
        {
            InitializeComponent();

            ddlSpellPow1.SelectedIndex = 0;
            ddlSpellPow2.SelectedIndex = 0;
            ddlSpellPow3.SelectedIndex = 0;
            cbPlatform.SelectedIndex = 0;
            toolStripStatusLabelPercent.Text = "";
            toolStripStatusLabelProgress.Text = "";

            LoadData();
        }

        private void LoadData()
        {
            LoadCharacters();
            InitializeFutureRng();
        }

        /// <summary>
        /// Load the group information from the form into our characters.
        /// </summary>
        private void LoadCharacters()
        {
            _group.ClearCharacters();

            if (tbLevel1.Text != string.Empty && tbMagic1.Text != string.Empty)
            {
                LoadCharacter(tbLevel1, tbMagic1, ddlSpellPow1, cbSerenity1);
            }
            if (tbLevel2.Text != string.Empty && tbMagic2.Text != string.Empty)
            {
                LoadCharacter(tbLevel2, tbMagic2, ddlSpellPow2, cbSerenity2);
            }
            if (tbLevel3.Text != string.Empty && tbMagic3.Text != string.Empty)
            {
                LoadCharacter(tbLevel3, tbMagic3, ddlSpellPow3, cbSerenity3);
            }
        }

        private void LoadCharacter(TextBox levelBox, TextBox magicBox, ComboBox spellPowerBox, CheckBox serenityBox)
        {
            double level = double.Parse(levelBox.Text);
            double magic = double.Parse(magicBox.Text);
            Spells spell = NameToSpellMap[spellPowerBox.SelectedItem.ToString()];
            _group.AddCharacter(new Character(level, magic, spell,
                serenityBox.Checked));
        }

        private void InitializeFutureRng()
        {
            const string ps2 = "PS2";

            _platform = cbPlatform.SelectedItem as string == ps2
                ? PlatformType.Ps2
                : PlatformType.Ps4;

            _rngHelper = new StealRngHelper(_platform, _group);
        }

        #endregion construction/initialization

        #region update UI methods

        private void DisplayFutureRng()
        {
            _futureRng = _rngHelper.GetStealFutureRng();

            dataGridView1.Rows.Clear();

            UpdateDataGridView();

            UpdateStealDirectionsData();

            UpdateNextHealData();

            UpdateComboData();

            SetLastHealFocus();
        }

        private void UpdateDataGridView()
        {
            int positionsCalculated = _futureRng.GetTotalFutureRngPositions();

            for (int i = 0; i < positionsCalculated; i++)
            {
                StealFutureRngInstance rngInstance = _futureRng.GetRngInstanceAt(i);
                int rowNumber = dataGridView1.Rows.Add();
                DataGridViewRow row = dataGridView1.Rows[rowNumber];

                UpdateRowInfo(rngInstance, row);

                if (rngInstance.IsPastRng)
                {
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1]
                        .DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }

        private void UpdateRowInfo(
            StealFutureRngInstance rngInstance, DataGridViewRow row)
        {
            row.Cells[0].Value = rngInstance.Index;
            row.Cells[1].Value = rngInstance.CurrentHeal;
            row.Cells[2].Value = rngInstance.RandToPercent;
            row.Cells[5].Value = rngInstance.Lv99RedChocobo;

            row.Cells[3].Value = ConvertStealRewardToString(rngInstance.NormalReward);
            row.Cells[4].Value = ConvertStealRewardsToString(rngInstance.CuffsReward);
        }

        private string ConvertStealRewardToString(StealType reward)
        {
            return reward.ToString();
        }

        private string ConvertStealRewardsToString(List<StealType> rewards)
        {
            const string linker = " + ";

            string rewardsString = string.Empty;

            foreach (StealType reward in rewards)
            {
                rewardsString += reward + linker;
            }

            return rewardsString.TrimEnd(linker.ToCharArray());
        }

        private void UpdateStealDirectionsData()
        {
            StealDirections directions = _futureRng.GetStealDirections();
            tbRare.Text = ConvertStealDirectionsToText(
                directions.AdvanceForRare);
            tbRareCuffs.Text = ConvertStealDirectionsToText(
                directions.AdvanceForRareCuffs);
        }

        private static string ConvertStealDirectionsToText(int index)
        {
            const string advanceDirectionsNotFound = @"¯\_(ツ)_/¯";

            return index == -1
                ? advanceDirectionsNotFound
                : index.ToString();
        }

        private void UpdateNextHealData()
        {
            tbLastHeal.Text = _rngHelper.GetNextExpectedHealValue().ToString();
        }

        private void UpdateComboData()
        {
            const string safe = "SAFE";

            int attacksUntilCombo = _rngHelper.GetAttacksUntilNextCombo();
            tbCombo.Text = attacksUntilCombo == -1
                ? safe
                : attacksUntilCombo.ToString();
        }

        private void HandleImpossibleHealVal()
        {
            MessageBox.Show(ImpossibleHealMsg);
            SetLastHealFocus();
        }

        private void SetLastHealFocus()
        {
            tbLastHeal.Focus();
            tbLastHeal.SelectAll();
        }

        private void SetContinueButtonsEnabledStatus(bool enabled)
        {
            btnContinue.Enabled = enabled;
            btnConsume.Enabled = enabled;
        }

        #endregion update UI methods

        #region click methods

        private void btnBegin_Click(object sender, EventArgs e)
        {
            SetContinueButtonsEnabledStatus(true);

            LoadData();

            if (!_rngHelper.FindFirstRngPosition(int.Parse(tbLastHeal.Text)))
            {
                SetContinueButtonsEnabledStatus(false);
                HandleImpossibleHealVal();
                return;
            }

            int numRows = FormUtils.ParseNumRows(tbNumRows.Text);
            _rngHelper.CalculateRng(numRows);
            DisplayFutureRng();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (!_rngHelper.FindNextRngPosition(int.Parse(tbLastHeal.Text)))
            {
                HandleImpossibleHealVal();
                return;
            }

            int numRows = FormUtils.ParseNumRows(tbNumRows.Text);
            _rngHelper.CalculateRng(numRows);
            DisplayFutureRng();
        }

        private void btnConsume_Click(object sender, EventArgs e)
        {
            int consume;
            int.TryParse(tbConsume.Text, out consume);

            DateTime begint = DateTime.Now;
            _rngHelper.ConsumeNextNRngPositions(consume);
            DateTime endt = DateTime.Now;
            toolStripStatusLabelPercent.Text = (endt - begint).ToString();

            int numRows = FormUtils.ParseNumRows(tbNumRows.Text);
            _rngHelper.CalculateRng(numRows);
            DisplayFutureRng();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            SetContinueButtonsEnabledStatus(false);
            dataGridView1.Rows.Clear();

            _rngHelper.Reinitialize();
            SetLastHealFocus();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(AboutMsg);
        }

        #endregion click methods

        private void cbPlatform_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbPlatform.SelectedItem as string == "PS2" && _platform is PlatformType.Ps4)
            {
                btnContinue.Enabled = false;
                dataGridView1.Rows.Clear();
                LoadData();
            }
            else if (cbPlatform.SelectedItem as string == "PS4" && _platform is PlatformType.Ps2)
            {
                btnContinue.Enabled = false;
                dataGridView1.Rows.Clear();
                LoadData();
            }
        }

        private void tbLastHeal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                e.Handled = true;
                if (btnContinue.Enabled == false)
                    btnBegin_Click(sender, e);
                else
                    btnContinue_Click(sender, e);
            }
        }

        private void FormSteal_Load(object sender, EventArgs e)
        {
            LoadData();
            int numRows = FormUtils.ParseNumRows(tbNumRows.Text);

            _rngHelper.CalculateRng(numRows);
            DisplayFutureRng();
        }

        #region change form methods

        private void rareGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormSpawn().Show();
            Hide();
        }

        private void chestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormChest2().Show();
            Hide();
        }

        #endregion change form methods

        #region text box validation

        private void tbLevel_Validating(object sender, CancelEventArgs e)
        {
            ValidateIntegerTextBox(tbLevel1);
        }

        private void tbMagic_Validating(object sender, CancelEventArgs e)
        {
            ValidateIntegerTextBox(tbMagic1);
        }

        private void tbLastHeal_Validating(object sender, CancelEventArgs e)
        {
            ValidateIntegerTextBox(tbLastHeal);
        }

        private static void ValidateIntegerTextBox(TextBox tb)
        {
            if (!int.TryParse(tb.Text, out int tempVal))
            {
                tb.Text = IntDefaultValue;
            }
        }

        #endregion text box validation
    }
}