using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using FF12RNGHelper.Core;

namespace FF12RNGHelper.Forms
{
    public partial class FormSteal : Form
    {
        private const int findNextTimeout = 60;
        private const int maxSearchIndexSupported = (int)1e7; // 10 million
        private const int SearchBufferSize = 1000000;

        private IRNG searchRNG;
        private IRNG dispRNG;
        private int index;	// Current index in the PRNG list
        private CircularBuffer<uint> searchBuff;	// buffer of PRNG numbers
        private List<int> healVals;  // List of heal values input by user
        private CharacterGroup group = new CharacterGroup();

        Stopwatch aStopwatch = new Stopwatch();

        public FormSteal()
        {
            InitializeComponent();

            index = 0;

            ddlSpellPow1.SelectedIndex = 0;
            ddlSpellPow2.SelectedIndex = 0;
            ddlSpellPow3.SelectedIndex = 0;
            cbPlatform.SelectedIndex = 0;
            healVals = new List<int>();
            searchRNG = new RNG1998();
            dispRNG = new RNG1998();
            toolStripStatusLabelPercent.Text = "";
            toolStripStatusLabelProgress.Text = "";

            LoadCharacters();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadCharacters()
        {
            group.ClearCharacters();

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
            int startIndex = spellPowerBox.SelectedItem.ToString().IndexOf("(") + 1;
            int length = spellPowerBox.SelectedItem.ToString().IndexOf(")") - startIndex;
            double spellpower = double.Parse(spellPowerBox.SelectedItem.ToString().Substring(startIndex, length));
            group.AddCharacter(new Character(level, magic, spellpower, serenityBox.Checked));
        }

        private int ParseNumRows()
        {
            int numRows;
            int.TryParse(tbNumRows.Text, out numRows);
            if (numRows < 30)
                numRows = 30;
            if (numRows > 10000)
                numRows = 10000;
            return numRows;
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            btnContinue.Enabled = true;
            bConsume.Enabled = true;
            //Parse the boxes
            group.ResetIndex();
            LoadCharacters();

            healVals.Clear();
            searchBuff = new CircularBuffer<uint>(SearchBufferSize);
            searchRNG.sgenrand();
            searchBuff.Add(searchRNG.genrand());
            index = 0;
            if (!FindNext(int.Parse(tbLastHeal.Text)))
            {
                btnContinue.Enabled = false;
                MessageBox.Show("Impossible Heal Value entered.");
                return;
            }
            int numRows = ParseNumRows();
            displayRNG(index, index + numRows);
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            DateTime begint = DateTime.Now;
            group.IncrimentIndex();
            if (!FindNext(int.Parse(tbLastHeal.Text)))
            {
                MessageBox.Show("Impossible Heal Value entered.");
                return;
            }
            DateTime endt = DateTime.Now;
            toolStripStatusLabelPercent.Text = (endt - begint).ToString();
            int numRows = ParseNumRows();
            displayRNG(index - healVals.Count + 1, index + numRows);
        }

        private bool FindNext(int value)
        {
            // Do a range check before trying this out to avoid entering an infinite loop.
            if (!group.ValidateHealValue(value))
            {
                return false;
            }

            // Store the current character while searching:
            int indexStatic = group.GetIndex();

            // Add the given heal value to the heal list.
            healVals.Add(value);
            index++;

            // Pull an extra PRNG draw to see whether it matches.
            searchBuff.Add(searchRNG.genrand());

            // Otherwise, continue moving through the RNG to find the next matching position
            bool match;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            do
            {
                // Quit if it's taking too long.
                if (timer.Elapsed.TotalSeconds > findNextTimeout ||
                    index > maxSearchIndexSupported)
                {
                    timer.Stop();
                    group.SetIndex(indexStatic);
                    return false;
                }

                // Reset the index to start the scan again
                group.ResetIndex();
                match = true;
                for (int i = 0; i < healVals.Count; i++)
                {
                    // index of first heal:
                    int index0 = index - healVals.Count + 1;

                    if (group.GetHealValue(searchBuff[index0 + i]) != healVals[i])
                    {
                        match = false;
                        break;
                    }
                }
                if (!match)
                {
                    searchBuff.Add(searchRNG.genrand());
                    index++;
                }
            } while (!match);
            timer.Stop();

            group.SetIndex(indexStatic);
            return true;
        }

        double randToPercent(uint toConvert)
        {
            return toConvert % 100;
        }

        private void displayRNG(int end)
        {
            displayRNG(0, end);
        }

        private void displayRNG(int start, int end)
        {
            IRNG displayRNG;
            if (cbPlatform.SelectedItem as string == "PS2")
            {
                displayRNG = new RNG1998();
            }
            else
            {
                displayRNG = new RNG2002();
            }
            //Clear datagridview
            dataGridView1.Rows.Clear();
            //Consume RNG seeds before our desired index
            //This can take obscene amounts of time.
            DateTime startTime = DateTime.Now;
            for (int i = 0; i < start; i++)
            {
                displayRNG.genrand();
            }
            DateTime endTime = DateTime.Now;
            toolStripStatusLabelPercent.Text = (endTime - startTime).Milliseconds.ToString();

            int rarePosition = 0;
            int rarePositionCuffs = 0;

            bool rareSteal = false;
            bool rareStealCuffs = false;

            // Use these variables to check for first punch combo
            bool comboFound = false;
            int comboPos = 0;

            uint aVal1 = displayRNG.genrand();
            uint aVal2 = displayRNG.genrand();
            uint aVal3 = displayRNG.genrand();

            // We want to preserve the character index, since this loop is just for display:
            int indexStatic = group.GetIndex();
            group.ResetIndex();

            //group.ResetIndex();
            for (int index = start; index < end; index++)
            {
                // Index starting at 0
                int loopIndex = index - start;

                // Get the heal value once:
                int currentHeal = group.GetHealValue(aVal1);
                int nextHeal = group.PeekHealValue(aVal2);

                // Put the next expected heal in the text box
                if (index == start + healVals.Count - 1)
                {
                    tbLastHeal.Text = nextHeal.ToString();
                }

                // Advance the RNG before starting the loop in case we want to skip an entry
                uint aVal1_temp = aVal1;
                uint aVal2_temp = aVal2;
                uint aVal3_temp = aVal3;
                aVal1 = aVal2;
                aVal2 = aVal3;
                aVal3 = displayRNG.genrand();

                // Skip the entry if it's too long ago
                if (loopIndex < healVals.Count - 5)
                    continue;

                //Start actually displaying
                dataGridView1.Rows.Add();

                // Color consumed RNG green
                if (index < start + healVals.Count)
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGreen;

                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = index;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = currentHeal;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = randToPercent(aVal1_temp);

                handleSteal(aVal1_temp, aVal2_temp, aVal3_temp, 3);
                handleStealCuffs(aVal1_temp, aVal2_temp, aVal3_temp, 4);

                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[5].Value = (aVal1_temp < 0x1000000);

                // Check if the chests are in a position offset by a fixed amount
                if ((aVal1_temp % 100) < 3 && !rareSteal && loopIndex >= healVals.Count)
                {
                    rareSteal = true;
                    rarePosition = loopIndex - healVals.Count;
                }

                if ((aVal1_temp % 100) < 6 && !rareStealCuffs && loopIndex >= healVals.Count)
                {
                    rareStealCuffs = true;
                    rarePositionCuffs = loopIndex - healVals.Count;
                }

                // Check for combo during string of punches
                int comboCheck = loopIndex - healVals.Count - 5 + 1;
                if (comboCheck % 10 == 0 && comboCheck >= 0 && !comboFound &&
                    Combo.IsSucessful(aVal1_temp))
                {
                    comboFound = true;
                    comboPos = comboCheck / 10;
                }
            }

            tbRare.Text = rarePosition.ToString();
            tbRareCuffs.Text = rarePositionCuffs.ToString();

            tbCombo.Text = comboPos.ToString();

            tbLastHeal.Focus();
            tbLastHeal.SelectAll();

            group.SetIndex(indexStatic);
        }

        private void handleSteal(uint PRNG0, uint PRNG1, uint PRNG2, int column)
        {
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[column].Value = Steal.CheckSteal(PRNG0, PRNG1, PRNG2);
        }

        private void handleStealCuffs(uint PRNG0, uint PRNG1, uint PRNG2, int column)
        {
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[column].Value = Steal.CheckStealCuffs(PRNG0, PRNG1, PRNG2);
        }

        private void handleStealHelper(string text, int loopIndex, int column, CheckBox checkBox, bool expectCheck,
            ref int itemPosition, ref bool chestFound)
        {
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[column].Value = (object)text;

            // Check if the items are in this position
            if (((checkBox.Checked == expectCheck) && loopIndex >= healVals.Count) && !chestFound)
            {
                itemPosition = loopIndex - healVals.Count;
                chestFound = true;
            }
        }

        private void tbLevel_Validating(object sender, CancelEventArgs e)
        {
            double tempVal;
            if (!double.TryParse(tbLevel1.Text, out tempVal))
            {
                tbLevel1.Text = "0";
            }
        }

        private void tbMagic_Validating(object sender, CancelEventArgs e)
        {
            double tempVal;
            if (!double.TryParse(tbMagic1.Text, out tempVal))
            {
                tbMagic1.Text = "0";
            }
        }

        private void tbLastHeal_Validating(object sender, CancelEventArgs e)
        {
            int tempVal;
            if (!int.TryParse(tbLastHeal.Text, out tempVal))
            {
                tbLastHeal.Text = "0";
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FF12 RNG Helper v1.02\nSo many features, so little time...");
        }

        private void cbPlatform_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbPlatform.SelectedItem as string == "PS2" && searchRNG is RNG2002)
            {
                btnContinue.Enabled = false;
                dataGridView1.Rows.Clear();
                dispRNG = new RNG1998();
                searchRNG = new RNG1998();
            }
            else if (cbPlatform.SelectedItem as string == "PS4" && searchRNG is RNG1998)
            {
                btnContinue.Enabled = false;
                dataGridView1.Rows.Clear();
                dispRNG = new RNG2002();
                searchRNG = new RNG2002();
            }
        }

        private void FormChest_Load(object sender, EventArgs e)
        {
            int numRows = ParseNumRows();
            displayRNG(index - healVals.Count + 1, index + numRows);
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

        private void rareGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormSpawn().Show();
            this.FindForm().Hide();
        }

        private void chestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormChest2().Show();
            Hide();
        }

        private void bConsume_Click(object sender, EventArgs e)
        {
            DateTime begint = DateTime.Now;
            int consume;
            int.TryParse(tbConsume.Text, out consume);
            for (int i = 0; i < consume; i++)
            {
                group.IncrimentIndex();
                if (!FindNext(int.Parse(tbLastHeal.Text)))
                {
                    MessageBox.Show("Impossible Heal Value entered.");
                    return;
                }
                displayRNG(index - healVals.Count + 1, index + 1);
            }
            DateTime endt = DateTime.Now;
            toolStripStatusLabelPercent.Text = (endt - begint).ToString();
            int numRows = ParseNumRows();
            displayRNG(index - healVals.Count + 1, index + numRows);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnContinue.Enabled = false;
            dataGridView1.Rows.Clear();
            tbLastHeal.Focus();
            tbLastHeal.SelectAll();
        }
    }
}