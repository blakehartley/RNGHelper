using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace FF12RNGHelper
{
    public partial class FormSpawn : Form
    {
        private const int findNextTimeout = 60;
        private const int maxSearchIndexSupported = (int)1e7; // 10 million
        private const int SearchBufferSize = 1000000;

        IRNG searchRNG;
        IRNG dispRNG;
        int index;	// Current index in the PRNG list
        CircularBuffer<UInt32> searchBuff;	// buffer of PRNG numbers
        List<UInt32> healVals;  // List of heal values input by user
        CharacterGroup group = new CharacterGroup();

        System.Diagnostics.Stopwatch aStopwatch = new System.Diagnostics.Stopwatch();

        public FormSpawn()
        {
            InitializeComponent();

            ddlSpellPow1.SelectedIndex = 0;
            ddlSpellPow2.SelectedIndex = 0;
            ddlSpellPow3.SelectedIndex = 0;
            cbPlatform.SelectedIndex = 0;
            healVals = new List<UInt32>();
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
            this.group.ClearCharacters();
            if (this.tbLevel1.Text != string.Empty && this.tbMagic1.Text != string.Empty)
            {
                double level = double.Parse(this.tbLevel1.Text);
                double magic = double.Parse(this.tbMagic1.Text);
                int startIndex = this.ddlSpellPow1.SelectedItem.ToString().IndexOf("(") + 1;
                int length = this.ddlSpellPow1.SelectedItem.ToString().IndexOf(")") - startIndex;
                double spellpower = double.Parse(this.ddlSpellPow1.SelectedItem.ToString().Substring(startIndex, length));
                this.group.AddCharacter(new Character(level, magic, spellpower, cbSerenity1.Checked));
            }
            if (this.tbLevel2.Text != string.Empty && this.tbMagic2.Text != string.Empty)
            {
                double level = double.Parse(this.tbLevel2.Text);
                double magic = double.Parse(this.tbMagic2.Text);
                int startIndex = this.ddlSpellPow2.SelectedItem.ToString().IndexOf("(") + 1;
                int length = this.ddlSpellPow2.SelectedItem.ToString().IndexOf(")") - startIndex;
                double spellpower = double.Parse(this.ddlSpellPow2.SelectedItem.ToString().Substring(startIndex, length));
                this.group.AddCharacter(new Character(level, magic, spellpower, cbSerenity2.Checked));
            }
            if (this.tbLevel3.Text == string.Empty || this.tbMagic3.Text == string.Empty)
                return;
            double level1 = double.Parse(this.tbLevel3.Text);
            double magic1 = double.Parse(this.tbMagic3.Text);
            int startIndex1 = this.ddlSpellPow3.SelectedItem.ToString().IndexOf("(") + 1;
            int length1 = this.ddlSpellPow3.SelectedItem.ToString().IndexOf(")") - startIndex1;
            double spellpower1 = double.Parse(this.ddlSpellPow3.SelectedItem.ToString().Substring(startIndex1, length1));
            this.group.AddCharacter(new Character(level1, magic1, spellpower1, cbSerenity3.Checked));
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
            searchBuff = new CircularBuffer<UInt32>(SearchBufferSize);
            searchRNG.sgenrand();
            searchBuff.Add(searchRNG.genrand());
            index = 0;
            if (!FindNext(UInt32.Parse(tbLastHeal.Text)))
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
            if (!FindNext(UInt32.Parse(tbLastHeal.Text)))
            {
                MessageBox.Show("Impossible Heal Value entered.");
                return;
            }
            DateTime endt = DateTime.Now;
            toolStripStatusLabelPercent.Text = (endt - begint).ToString();
            int numRows = ParseNumRows();
            displayRNG(index - healVals.Count + 1, index + numRows);
        }

        private bool FindNext(uint value)
        {
            // Do a range check before trying this out to avoid entering an infinite loop.
            if (value > group.HealMax() || value < group.HealMin())
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

        UInt32 randToPercent(UInt32 toConvert)
        {
            return toConvert % 100;
        }

        private void displayRNG(int end)
        {
            displayRNG(0, end);
        }

        private bool rareCheck(double chance, double min, double max)
        {
            if (chance > min && chance < max)
                return true;
            else
                return false;
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
            DateTime startt = DateTime.Now;
            for (int index = 0; index < start; index++)
            {
                displayRNG.genrand();
            }
            DateTime endtt = DateTime.Now;
            toolStripStatusLabelPercent.Text = (endtt - startt).Milliseconds.ToString();

            // Rare 1:
            double spawnMin1, spawnMax1;
            int rareRNGPosition1;

            double.TryParse(tbMin1.Text, out spawnMin1);
            double.TryParse(tbMax1.Text, out spawnMax1);
            int.TryParse(tbRNG1.Text, out rareRNGPosition1);

            // Convert to fraction:
            spawnMin1 /= 100.0;
            spawnMax1 /= 100.0;

            // Rare 2:
            double spawnMin2, spawnMax2;
            int rareRNGPosition2;

            double.TryParse(tbMin2.Text, out spawnMin2);
            double.TryParse(tbMax2.Text, out spawnMax2);
            int.TryParse(tbRNG2.Text, out rareRNGPosition2);

            // Convert to fraction:
            spawnMin2 /= 100.0;
            spawnMax2 /= 100.0;

            // Use these variables to check for first instance of chest and contents
            bool rareSpawn1 = false;
            int rareFoundPos1 = 0;

            bool rareSpawn2 = false;
            int rareFoundPos2 = 0;

            // Last rare 1 before rare 2 variable, for Ishteen
            int rareFoundPos3 = 0;

            // Use these variables to check for first punch combo
            bool comboFound = false;
            int comboPos = 0;

            UInt32 aVal1 = displayRNG.genrand();
            UInt32 aVal2 = displayRNG.genrand();

            // We want to preserve the character index, since this loop is just for display:
            int indexStatic = group.GetIndex();
            group.ResetIndex();

            //group.ResetIndex();
            for (int index = start; index < end; index++)
            {
                // Index starting at 0
                int index0 = index - start;

                // Get the heal value once:
                int healNow = group.GetHealValue(aVal1);
                int healNext = group.PeekHealValue(aVal2);

                // Get chance to spawn rare game
                float spawnChance = (float)aVal1 / 4294967296;

                // Put the next expected heal in the text box
                if (index == start + healVals.Count - 1)
                {
                    tbLastHeal.Text = healNext.ToString();
                    //tbAppear1.Text = group.HealMin().ToString();
                    //tbItem1.Text = group.HealMax().ToString();
                }

                // Advance the RNG before starting the loop in case we want to skip an entry
                UInt32 aVal1_temp = aVal1;
                UInt32 aVal2_temp = aVal2;
                aVal1 = aVal2;
                aVal2 = displayRNG.genrand();

                // Skip the entry if it's too long ago
                if (index0 < healVals.Count - 5)
                    continue;

                //Start actually displaying
                dataGridView1.Rows.Add();

                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = index;
                //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = aVal1;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = healNow;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = spawnChance;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = aVal1_temp.ToString("N0");

                // Check if the rares are in a position offset by a fixed amount

                if (rareCheck(spawnChance, spawnMin1, spawnMax1))
                {
                    int chestFirstChance = healVals.Count + rareRNGPosition1 - 1;

                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightBlue;
                    if (index0 >= chestFirstChance && !rareSpawn1)
                    {
                        rareFoundPos1 = index0 - healVals.Count - rareRNGPosition1 + 1;
                        rareSpawn1 = true;
                    }
                }
                if (rareCheck(spawnChance, spawnMin2, spawnMax2))
                {
                    int chestFirstChance = healVals.Count + rareRNGPosition2 - 1;

                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Crimson;
                    if (index0 >= chestFirstChance && !rareSpawn2)
                    {
                        rareFoundPos2 = index0 - healVals.Count - rareRNGPosition2 + 1;
                        rareSpawn2 = true;
                    }
                }
                if (rareCheck(spawnChance, spawnMin1, spawnMax1) && rareCheck(spawnChance, spawnMin2, spawnMax2))
                {
                    //int chestFirstChance = healVals.Count + Math.Max(rareRNGPosition1, rareRNGPosition2)+ 1;

                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Orchid;
                }
                if (rareCheck(spawnChance, spawnMin1, spawnMax1))
                {
                    // Check if rare 2 has been found yet
                    if (!rareSpawn2)
                    {
                        rareFoundPos3 = index0 - healVals.Count - rareRNGPosition1 + 1;
                    }
                }

                // Color consumed RNG green
                if (index0 < healVals.Count)
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGreen;

                int comboCheck = index0 - healVals.Count - 5 + 1;
                if (comboCheck % 10 == 0 && comboCheck >= 0 && !comboFound &&
                    Combo.IsSucessful(aVal1_temp))
                {
                    comboFound = true;
                    comboPos = comboCheck / 10;
                }
            }

            tbAppear1.Text = rareFoundPos1.ToString();

            tbAppear2.Text = rareFoundPos2.ToString();

            tbAppear12.Text = rareFoundPos3.ToString();

            tbCombo.Text = comboPos.ToString();

            tbLastHeal.Focus();
            tbLastHeal.SelectAll();

            group.SetIndex(indexStatic);
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
            UInt32 tempVal;
            if (!UInt32.TryParse(tbLastHeal.Text, out tempVal))
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

        private void stealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormSteal().Show();
            this.FindForm().Hide();
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
                if (!FindNext(UInt32.Parse(tbLastHeal.Text)))
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