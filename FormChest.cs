using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FF12RNGHelper
{
    public partial class FormChest : Form
    {
        private IRNG searchRNG;
        private IRNG dispRNG;
        private int index;	// Current index in the PRNG list
        private List<uint> searchBuff;	// buffer of PRNG numbers
        private List<int> healVals;  // List of heal values input by user
        private CharacterGroup group = new CharacterGroup();

        Stopwatch aStopwatch = new Stopwatch();

        public FormChest()
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

        /*
        private void parseThings()
        {
            //Parse the boxes
            level = double.Parse(tbLevel1.Text);
            mag = double.Parse(tbMagic1.Text);
            switch (ddlSpellPow1.SelectedIndex)
            {
                case 0:
                    spell = (uint)Spells.Cure;
                    break;
                case 1:
                    spell = (uint)Spells.Cura;
                    break;
                case 2:
                    spell = (uint)Spells.Curaga;
                    break;
                case 3:
                    spell = (uint)Spells.Curaja;
                    break;
                case 4:
                    spell = (uint)Spells.CuraIZJS;
                    break;
                case 5:
                    spell = (uint)Spells.CuragaIZJS;
                    break;
                case 6:
                    spell = (uint)Spells.CurajaIZJS;
                    break;
                default:
                    spell = (uint)Spells.Cure;
                    break;
            }
            if (cbSerenity1.Checked)
            {
                serenityMult = 1.5;
            }
            else serenityMult = 1;
        }*/

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
            searchBuff = new List<uint>();
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
                if (timer.Elapsed.TotalSeconds > 60)
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

        private bool chestCheck(uint PRNG, double chestChance, bool flip)
        {
            if (flip)
                return (randToPercent(PRNG) < chestChance);
            else
                return (randToPercent(PRNG) > chestChance);
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

            // Chest/Item 1:
            Chest chest1 = new Chest(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);

            // Chest/Item 2:
            Chest chest2 = new Chest(textBox10.Text, textBox9.Text, textBox8.Text, textBox7.Text, textBox6.Text);

            // Use these variables to check for first instance of chest and contents
            bool chestSpawn1 = false, chestFound1 = false;
            int chestFoundPos1 = 0, chestItemPos1 = 0;

            bool chestSpawn2 = false, chestFound2 = false;
            int chestFoundPos2 = 0, chestItemPos2 = 0;

            uint aVal1 = displayRNG.genrand();
            uint aVal2 = displayRNG.genrand();

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
                aVal1 = aVal2;
                aVal2 = displayRNG.genrand();

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

                // Check if the chests are in a position offset by a fixed amount
                if (chest1.checkSpawn(aVal1_temp))
                {
                    handleChestSpawn(chest1, loopIndex, 3, ref chestFoundPos1, ref chestSpawn1); 
                }

                if (chest2.checkSpawn(aVal1_temp))
                {
                    handleChestSpawn(chest2, loopIndex, 4, ref chestFoundPos2, ref chestSpawn2);
                }

                // This is a big conditional to see what is in both chests.
                // There may be a better way, but this was fast to write and doesn't call the RNG.
                // Calculate the contents of the chest. First, gil:
                if (chest1.checkIfGil(aVal1_temp))
                {
                    handleGilReward(chest1, aVal2_temp, 3);
                } 
                // Otherwise, what item is it, and where is the first desired item
                else
                {
                    handleItemReward(chest1, aVal2_temp, loopIndex, 3, checkBox1, ref chestItemPos1, ref chestFound1);
                }

                if (chest2.checkIfGil(aVal1_temp))
                {
                    handleGilReward(chest2, aVal2_temp, 4);
                } 
                // Otherwise, what item is it, and where is the first desired item
                else
                {
                    handleItemReward(chest2, aVal2_temp, loopIndex, 4, checkBox2, ref chestItemPos2, ref chestFound2);
                }
            }

            tbAppear1.Text = chestFoundPos1.ToString();
            tbItem1.Text = chestItemPos1.ToString();

            tbAppear2.Text = chestFoundPos2.ToString();
            tbItem2.Text = chestItemPos2.ToString();

            tbLastHeal.Focus();
            tbLastHeal.SelectAll();

            group.SetIndex(indexStatic);
        }

        private void handleChestSpawn(Chest chest, int loopIndex, int column,
            ref int chestFoundPosition, ref bool chestSpawn)
        {
            int chestFirstChance = healVals.Count + chest.getRNGPosition();

            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[column].Style.Font = new Font(dataGridView1.CurrentCell.InheritedStyle.Font, FontStyle.Bold);
            if (loopIndex >= chestFirstChance && !chestSpawn)
            {
                chestFoundPosition = loopIndex - healVals.Count - chest.getRNGPosition();
                chestSpawn = true;
            }
        }

        private void handleItemReward(Chest chest, uint PRNG, int loopIndex, int column, CheckBox checkBox,
            ref int itemPosition, ref bool chestFound)
        {
            if (chest.checkIfFirstItem(PRNG))
            {
                handleItemRewardHelper("Item 1", loopIndex, column, checkBox, true, ref itemPosition, ref chestFound);
            }
            else
            {
                handleItemRewardHelper("Item 2", loopIndex, column, checkBox, false, ref itemPosition, ref chestFound);
            }
        }

        private void handleItemRewardHelper(string text, int loopIndex, int column, CheckBox checkBox, bool expectCheck,
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

        private void handleGilReward(Chest chest, uint PRNG, int column)
        {
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[column].Value = chest.getGilAmount(PRNG);
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

        private void backgroundWorkerConsume_DoWork(object sender, DoWorkEventArgs e)
        {
            //Start the party!
            Tuple<ulong, ulong> inputArgs = (Tuple<ulong, ulong>)e.Argument;
            BackgroundWorker bw = sender as BackgroundWorker;
            dispRNG.consumeBG(inputArgs.Item1, bw, e);
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                e.Result = inputArgs;
                aStopwatch.Stop();
            }

        }

        private void backgroundWorkerConsume_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                //We made it!
                dataGridView1.Rows.Clear();
                Tuple<int, int> inputArgs = (Tuple<int, int>)e.Result;

                for (int i = inputArgs.Item1; i < inputArgs.Item2; i++)
                {
                    //Start actually displaying
                    uint aVal = dispRNG.genrand();
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = i;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = aVal;
                    //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = randToHeal(aVal);
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = randToPercent(aVal);
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value = (aVal % 65000);
                }
            }
        }

        private void backgroundWorkerConsume_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBarPercent.Value = e.ProgressPercentage;
            toolStripStatusLabelPercent.Text = e.ProgressPercentage.ToString() + "%";
            toolStripStatusLabelProgress.Text = aStopwatch.Elapsed.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            backgroundWorkerConsume.CancelAsync();
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
            new FormSpawn().Show();
            this.FindForm().Hide();
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