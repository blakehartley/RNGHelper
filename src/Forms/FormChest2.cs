using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FF12RNGHelper.Core;

namespace FF12RNGHelper.Forms
{
    public partial class FormChest2 : Form
    {
        #region internal state

        private ChestRngHelper _rngHelper;
        private ChestFutureRng _futureRng;
        private readonly CharacterGroup _group = new CharacterGroup();
        private readonly List<Chest> _chests = new List<Chest>();
        private PlatformType _platform = FormUtils.GetDefaultPlatform();

        #endregion internal state

        #region construction/initialization

        public FormChest2()
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
            LoadChests();
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
                FormUtils.LoadCharacter(_group, tbLevel1, tbMagic1, ddlSpellPow1, cbSerenity1);
            }
            if (tbLevel2.Text != string.Empty && tbMagic2.Text != string.Empty)
            {
                FormUtils.LoadCharacter(_group, tbLevel2, tbMagic2, ddlSpellPow2, cbSerenity2);
            }
            if (tbLevel3.Text != string.Empty && tbMagic3.Text != string.Empty)
            {
                FormUtils.LoadCharacter(_group, tbLevel3, tbMagic3, ddlSpellPow3, cbSerenity3);
            }
        }

        private void LoadChests()
        {
            _chests.Clear();

            Chest chest1 = new Chest(
                int.Parse(textBox1.Text),
                int.Parse(textBox2.Text),
                int.Parse(textBox3.Text),
                int.Parse(textBox4.Text),
                int.Parse(textBox5.Text),
                cbWantItem1First.Checked);
            Chest chest2 = new Chest(
                int.Parse(textBox10.Text),
                int.Parse(textBox9.Text),
                int.Parse(textBox8.Text),
                int.Parse(textBox7.Text),
                int.Parse(textBox6.Text),
                cbWantItem1Second.Checked);

            _chests.Add(chest1);
            _chests.Add(chest2);
        }

        private void InitializeFutureRng()
        {
            const string ps2 = "PS2";

            _platform = cbPlatform.SelectedItem as string == ps2
                ? PlatformType.Ps2
                : PlatformType.Ps4;

            _rngHelper = new ChestRngHelper(_platform, _group, _chests);
        }

        #endregion construction/initialization

        #region update UI methods

        private void DisplayFutureRng()
        {
            _futureRng = _rngHelper.GetChestFutureRng();

            dataGridView.Rows.Clear();

            UpdateDataGridView();

            UpdateAdvanceData();

            FormUtils.UpdateNextHealData(_rngHelper, tbLastHeal);

            FormUtils.UpdateComboData(_rngHelper, tbCombo);

            SetLastHealFocus();
        }

        private void UpdateDataGridView()
        {
            int positionsCalculated = _futureRng.GetTotalFutureRngPositions();

            for (int i = 0; i < positionsCalculated; i++)
            {
                ChestFutureRngInstance rngInstance = _futureRng.GetRngInstanceAt(i);
                int rowNumber = dataGridView.Rows.Add();
                DataGridViewRow row = dataGridView.Rows[rowNumber];

                UpdateRowStandardInfo(rngInstance, row);
                UpdateRowChestInfo(rngInstance, row);

                if (rngInstance.IsPastRng)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }

        private void UpdateRowStandardInfo(ChestFutureRngInstance rngInstance,
            DataGridViewRow row)
        {
            row.Cells[0].Value = rngInstance.Index;
            row.Cells[1].Value = rngInstance.CurrentHeal;
            row.Cells[2].Value = rngInstance.RandToPercent;
        }

        private void UpdateRowChestInfo(ChestFutureRngInstance rngInstance,
            DataGridViewRow row)
        {
            const string item1 = "Item 1";
            const string item2 = "Item 2";
            const int cellOffset = 3; // start filling data in the 4th spot

            int chestCount = rngInstance.ChestRewards.Count;
            for (int chestIndex = 0; chestIndex < chestCount; chestIndex++)
            {
                ChestReward reward = rngInstance.ChestRewards.ElementAt(chestIndex);
                if (reward.Reward is RewardType.Gil)
                {
                    row.Cells[chestIndex + cellOffset].Value = reward.GilAmount;
                }
                else if (reward.Reward is RewardType.Item1)
                {
                    row.Cells[chestIndex + cellOffset].Value = item1;
                }
                else
                {
                    row.Cells[chestIndex + cellOffset].Value = item2;
                }

                if (reward.ChestWillSpawn)
                {
                    DataGridViewCell currentCell = row.Cells[chestIndex + cellOffset];
                    Font currentStyle = currentCell.InheritedStyle.Font;
                    currentCell.Style.Font = new Font(currentStyle, FontStyle.Bold);
                }
            }
        }

        private void UpdateAdvanceData()
        {
            AdvanceDirections directions1 = _futureRng.GetAdvanceDirectionsAtIndex(0);
            tbAppear1.Text = FormUtils.ConvertDirectionsToText(directions1.AdvanceToAppear);
            tbItem1.Text = FormUtils.ConvertDirectionsToText(directions1.AdvanceForItem);

            AdvanceDirections directions2 = _futureRng.GetAdvanceDirectionsAtIndex(1);
            tbAppear2.Text = FormUtils.ConvertDirectionsToText(directions2.AdvanceToAppear);
            tbItem2.Text = FormUtils.ConvertDirectionsToText(directions2.AdvanceForItem);
        }

        private void HandleImpossibleHealVal()
        {
            MessageBox.Show(FormConstants.ImpossibleHealMsg);
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
            dataGridView.Rows.Clear();

            _rngHelper.Reinitialize();
            SetLastHealFocus();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(FormConstants.AboutMsg);
        }

        #endregion click methods

        private void cbPlatform_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbPlatform.SelectedItem as string == "PS2" && _platform is PlatformType.Ps4)
            {
                btnContinue.Enabled = false;
                dataGridView.Rows.Clear();
                LoadData();
            }
            else if (cbPlatform.SelectedItem as string == "PS4" && _platform is PlatformType.Ps2)
            {
                btnContinue.Enabled = false;
                dataGridView.Rows.Clear();
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

        private void FormChest_Load(object sender, EventArgs e)
        {
            LoadData();
            int numRows = FormUtils.ParseNumRows(tbNumRows.Text);

            _rngHelper.CalculateRng(numRows);
            DisplayFutureRng();
        }

        #region change form methods

        private void rareGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormSpawn2().Show();
            Hide();
        }

        private void stealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormSteal2().Show();
            Hide();
        }

        #endregion change form methods

        #region text box validation

        private void tbLevel_Validating(object sender, CancelEventArgs e)
        {
            FormUtils.ValidateIntegerTextBox(tbLevel1);
        }

        private void tbMagic_Validating(object sender, CancelEventArgs e)
        {
            FormUtils.ValidateIntegerTextBox(tbMagic1);
        }

        private void tbLastHeal_Validating(object sender, CancelEventArgs e)
        {
            FormUtils.ValidateIntegerTextBox(tbLastHeal);
        }

        #endregion text box validation

        private void FormChest2_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormUtils.CloseApplication();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FormManager.SaveForm(this, new FormChestLoader());
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            FormManager.LoadForm(this, new FormChestLoader());
        }
    }
}