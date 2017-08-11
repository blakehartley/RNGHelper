using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FF12RNGHelper.Core;

namespace FF12RNGHelper.Forms
{
    public partial class FormSpawn2 : Form
    {
        #region internal state

        private SpawnRngHelper _rngHelper;
        private SpawnFutureRng _futureRng;
        private readonly CharacterGroup _group = new CharacterGroup();
        private readonly List<Monster> _monsters = new List<Monster>();
        private PlatformType _platform = FormUtils.GetDefaultPlatform();

        #endregion internal state

        #region construction/initialization

        public FormSpawn2()
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
            LoadMonsters();
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

        private void LoadMonsters()
        {
            _monsters.Clear();

            Monster monster1 = new Monster(
                int.Parse(tbMin1.Text),
                int.Parse(tbMax1.Text),
                int.Parse(tbRNG1.Text));
            Monster monster2 = new Monster(
                int.Parse(tbMin2.Text),
                int.Parse(tbMax2.Text),
                int.Parse(tbRNG2.Text));

            _monsters.Add(monster1);
            _monsters.Add(monster2);
        }

        private void InitializeFutureRng()
        {
            const string ps2 = "PS2";

            _platform = cbPlatform.SelectedItem as string == ps2
                ? PlatformType.Ps2
                : PlatformType.Ps4;

            _rngHelper = new SpawnRngHelper(_platform, _group, _monsters);
        }

        #endregion construction/initialization

        #region update UI methods

        private void DisplayFutureRng()
        {
            _futureRng = _rngHelper.GetSpawnFutureRng();

            dataGridView.Rows.Clear();

            UpdateDataGridView();

            UpdateDirectionsData();

            FormUtils.UpdateNextHealData(_rngHelper, tbLastHeal);

            FormUtils.UpdateComboData(_rngHelper, tbCombo);

            SetLastHealFocus();
        }

        private void UpdateDataGridView()
        {
            int positionsCalculated = _futureRng.GetTotalFutureRngPositions();

            for (int i = 0; i < positionsCalculated; i++)
            {
                SpawnFutureRngInstance rngInstance = _futureRng.GetRngInstanceAt(i);
                int rowNumber = dataGridView.Rows.Add();
                DataGridViewRow row = dataGridView.Rows[rowNumber];

                UpdateRowInfo(rngInstance, row);
                UpdateRowColor(rngInstance, row);

                if (rngInstance.IsPastRng)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }

        private void UpdateRowInfo(SpawnFutureRngInstance rngInstance, DataGridViewRow row)
        {
            row.Cells[0].Value = rngInstance.Index;
            row.Cells[1].Value = rngInstance.CurrentHeal;
            row.Cells[2].Value = rngInstance.SpawnChance;
            row.Cells[3].Value = rngInstance.RawRngValue.ToString("N0");
        }

        private void UpdateRowColor(SpawnFutureRngInstance rngInstance, DataGridViewRow row)
        {
            bool monsterSpawn1 = rngInstance.MonsterSpawns[0];
            bool monsterSpawn2 = rngInstance.MonsterSpawns[1];

            if (monsterSpawn1)
            {
                row.DefaultCellStyle.BackColor = Color.LightBlue;
            }
            if (monsterSpawn2)
            {
                row.DefaultCellStyle.BackColor = Color.Crimson;
            }
            if (monsterSpawn1 && monsterSpawn2)
            {
                row.DefaultCellStyle.BackColor = Color.Orchid;
            }
        }

        private void UpdateDirectionsData()
        {
            tbAppear1.Text = FormUtils.ConvertDirectionsToText(
                _futureRng.GetSpawnDirectionsAtIndex(0).Directions);
            tbAppear2.Text = FormUtils.ConvertDirectionsToText(
                _futureRng.GetSpawnDirectionsAtIndex(1).Directions);
            tbAppear12.Text = ConvertNBeforeMDirectionToText(
                _futureRng.GetStepsToLastNSpawnBeforeMSpawn(0, 1));
        }

        private static string ConvertNBeforeMDirectionToText(int value)
        {
            return value < 0
                ? FormConstants.ValueNotYetFound
                : value.ToString();
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

        private void bConsume_Click(object sender, EventArgs e)
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
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                if (btnContinue.Enabled == false)
                    btnBegin_Click(sender, e);
                else
                    btnContinue_Click(sender, e);
            }
        }

        private void FormSpawn_Load(object sender, EventArgs e)
        {
            LoadData();
            int numRows = FormUtils.ParseNumRows(tbNumRows.Text);

            _rngHelper.CalculateRng(numRows);
            DisplayFutureRng();
        }

        #region change form methods

        private void stealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormSteal2().Show();
            Hide();
        }

        private void rareGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormChest2().Show();
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

        private void FormSpawn2_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormUtils.CloseApplication();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FormManager.SaveForm(this, new FormSpawnLoader());
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            FormManager.LoadForm(this, new FormSpawnLoader());
        }
    }
}