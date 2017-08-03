using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FF12RNGHelper
{
    public partial class FormChest2 : Form
    {
        #region constants

        private const string ImpossibleHealMsg =
            "Impossible Heal Value Entered";

        private const string AboutMsg =
            "FF12 RNG Helper v1.02\nSo many features, so little time...";

        private const string IntDefaultValue = "0";

        #endregion constants

        #region internal state

        private ChestRngHelper _rngHelper;
        private ChestFutureRng _futureRng;
        private CharacterGroup _group = new CharacterGroup();
        private List<Chest> _chests = new List<Chest>();
        private PlatformType _platform = PlatformType.Ps2;

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
            Spell spell = new Spell(spellPowerBox);
            _group.AddCharacter(new Character(level, magic, spell.getPower(),
                serenityBox.Checked));
        }

        private void LoadChests()
        {
            _chests.Clear();

            Chest chest1 = new Chest(textBox1.Text, textBox2.Text, textBox3.Text,
                textBox4.Text, textBox5.Text);
            Chest chest2 = new Chest(textBox10.Text, textBox9.Text, textBox8.Text,
                textBox7.Text, textBox6.Text);

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

            dataGridView1.Rows.Clear();

            UpdateDataGridView();

            UpdateAdvanceData();

            UpdateNextHealData();

            UpdateComboData();

            SetLastHealFocus();
        }

        private void UpdateDataGridView()
        {
            int positionsCalculated = _futureRng.GetTotalFutureRngPositions();

            for (int i = 0; i < positionsCalculated; i++)
            {
                ChestFutureRngInstance rngInstance = _futureRng.GetRngInstanceAt(i);
                int rowNumber = dataGridView1.Rows.Add();
                DataGridViewRow row = dataGridView1.Rows[rowNumber];

                UpdateRowStandardInfo(rngInstance, row);
                UpdateRowChestInfo(rngInstance, row);

                if (rngInstance.IsPastRng)
                {
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1]
                        .DefaultCellStyle.BackColor = Color.LightGreen;
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
            tbAppear1.Text = ConvertAdvanceDirectionsToText(directions1.AdvanceToAppear);
            tbItem1.Text = ConvertAdvanceDirectionsToText(directions1.AdvanceForItem);

            AdvanceDirections directions2 = _futureRng.GetAdvanceDirectionsAtIndex(1);
            tbAppear2.Text = ConvertAdvanceDirectionsToText(directions2.AdvanceToAppear);
            tbItem2.Text = ConvertAdvanceDirectionsToText(directions2.AdvanceForItem);
        }

        private static string ConvertAdvanceDirectionsToText(int index)
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

        private int ParseNumRows()
        {
            int numRows = int.Parse(tbNumRows.Text);
            if (numRows < 30)
                numRows = 30;
            if (numRows > 10000)
                numRows = 10000;
            return numRows;
        }

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

            int numRows = ParseNumRows();
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

            int numRows = ParseNumRows();
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

            int numRows = ParseNumRows();
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

        #region text box validation

        private void tbLevel_Validating(object sender, CancelEventArgs e)
        {
            double tempVal;
            if (!double.TryParse(tbLevel1.Text, out tempVal))
            {
                tbLevel1.Text = IntDefaultValue;
            }
        }

        private void tbMagic_Validating(object sender, CancelEventArgs e)
        {
            double tempVal;
            if (!double.TryParse(tbMagic1.Text, out tempVal))
            {
                tbMagic1.Text = IntDefaultValue;
            }
        }

        private void tbLastHeal_Validating(object sender, CancelEventArgs e)
        {
            int tempVal;
            if (!int.TryParse(tbLastHeal.Text, out tempVal))
            {
                tbLastHeal.Text = IntDefaultValue;
            }
        }

        #endregion text box validation

        private void FormChest_Load(object sender, EventArgs e)
        {
            LoadData();
            int numRows = ParseNumRows();
            _rngHelper.CalculateRng(numRows);
            DisplayFutureRng();
        }

        #region change form methods

        private void rareGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormSpawn().Show();
            Hide();
        }

        private void stealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormSteal().Show();
            Hide();
        }

        #endregion change form methods

        #region backgroundworker stuff

        private void backgroundWorkerConsume_DoWork(object sender, DoWorkEventArgs e)
        {
            //Start the party!
            Tuple<ulong, ulong> inputArgs = (Tuple<ulong, ulong>) e.Argument;
            BackgroundWorker bw = sender as BackgroundWorker;
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                e.Result = inputArgs;
            }
        }

        private void backgroundWorkerConsume_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                //We made it!
                dataGridView1.Rows.Clear();
                Tuple<int, int> inputArgs = (Tuple<int, int>) e.Result;
            }
        }

        private void backgroundWorkerConsume_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBarPercent.Value = e.ProgressPercentage;
            toolStripStatusLabelPercent.Text = e.ProgressPercentage.ToString() + "%";
        }

        #endregion backgroundworker stuff
    }
}