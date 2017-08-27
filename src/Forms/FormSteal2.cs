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
        #region internal state

        private StealRngHelper _rngHelper;
        private StealFutureRng _futureRng;
        private readonly CharacterGroup _group = new CharacterGroup();
        private PlatformType _platform = FormUtils.GetDefaultPlatform();

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

            dataGridView.Rows.Clear();

            UpdateDataGridView();

            UpdateStealDirectionsData();

            FormUtils.UpdateNextHealData(_rngHelper, tbLastHeal);

            FormUtils.UpdateComboData(_rngHelper, tbCombo);

            SetLastHealFocus();
        }

        private void UpdateDataGridView()
        {
            int positionsCalculated = _futureRng.GetTotalFutureRngPositions();

            for (int i = 0; i < positionsCalculated; i++)
            {
                StealFutureRngInstance rngInstance = _futureRng.GetRngInstanceAt(i);
                int rowNumber = dataGridView.Rows.Add();
                DataGridViewRow row = dataGridView.Rows[rowNumber];

                UpdateRowInfo(rngInstance, row);

                if (rngInstance.IsPastRng)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
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
            tbRare.Text = FormUtils.ConvertDirectionsToText(
                directions.AdvanceForRare);
            tbRareCuffs.Text = FormUtils.ConvertDirectionsToText(
                directions.AdvanceForRareCuffs);
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

        private void FormSteal_Load(object sender, EventArgs e)
        {
            LoadData();
            int numRows = FormUtils.ParseNumRows(tbNumRows.Text);

            _rngHelper.CalculateRng(numRows);
            DisplayFutureRng();
        }

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

        private void FormSteal2_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormUtils.CloseApplication();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FormManager.SaveForm(this, new FormStealLoader());
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            FormManager.LoadForm(this, new FormStealLoader());
        }
    }
}