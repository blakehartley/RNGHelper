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
        // Number of rows to display before the current rng position
        private const int historyToDisplay = 5;
        private const int findNextTimeout = 60;
        private const int searchBufferSize = 1000000;

        private IRNG searchRNG;
        private IRNG dispRNG;
        private int index;	// Current index in the PRNG list
        private CircularBuffer<uint> searchBuff;	// buffer of PRNG numbers
        private List<int> healVals;  // List of heal values input by user
        private CharacterGroup group = new CharacterGroup();

        public FormChest()
        {
            InitializeComponent();

            index = 0;

            ddlSpellPow1.SelectedIndex = 0;
            ddlSpellPow2.SelectedIndex = 0;
            ddlSpellPow3.SelectedIndex = 0;
            cbPlatform.SelectedIndex = 0;
            healVals = new List<int>();
            searchRNG = InitializeRNG();
            dispRNG = InitializeRNG();
            toolStripStatusLabelPercent.Text = "";
            toolStripStatusLabelProgress.Text = "";

            LoadCharacters();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Load the group information from the form into our characters.
        /// </summary>
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
            Spell spell = new Spell(spellPowerBox);
            group.AddCharacter(new Character(level, magic, spell.getPower(), serenityBox.Checked));
        }

        private int ParseNumRows()
        {
            int numRows = int.Parse(tbNumRows.Text);
            if (numRows < 30)
                numRows = 30;
            if (numRows > 10000)
                numRows = 10000;
            return numRows;
        }

        /// <summary>
        /// The purpose of this method is to find our next spot in the RNG
        /// </summary>
        /// <param name="value">New heal value to process</param>
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
            bool match = false;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (!match)
            {
                // Quit if it's taking too long.
                if (timer.Elapsed.TotalSeconds > findNextTimeout)
                {
                    timer.Stop();
                    return false;
                }

                group.ResetIndex();
                for (int i = 0; i < healVals.Count; i++)
                {
                    // index of first heal:
                    int index0 = index - healVals.Count + 1;

                    if (!(match = group.GetHealValue(searchBuff[index0 + i]) == healVals[i]))
                    {
                        break;
                    }
                }
                if (!match)
                {
                    searchBuff.Add(searchRNG.genrand());
                    index++;
                }
				if (index > 1e7)
				{
					break;
				}
            }

            group.SetIndex(indexStatic);
            return true;
        }

        /// <summary>
        /// This method pre-rolls the RNG to the correct point
        /// so we can start matching on our cure list
        /// </summary>
        /// <param name="start">index of first cure</param>
        /// <param name="end">future RNGs to simulate</param>
        private void displayRNG(int start, int end)
        {
            IRNG dispRNG = InitializeRNG();
            //Consume RNG seeds before our desired index
            //This can take obscene amounts of time.
            for (int i = 0; i < start; i++)
            {
                dispRNG.genrand();
            }

            displayRNGHelper(dispRNG, start, end - start);
        }

        /// <summary>
        /// The purpose of this method is to display to chest information for the future
        /// based on our current location in the RNG
        /// </summary>
        /// <param name="displayRNG">RNG numbers to use</param>
        /// <param name="start">index where our first matching heal is</param>
        /// <param name="rowsToRender">How many rows to display</param>
        private void displayRNGHelper(IRNG displayRNG, int start, int rowsToRender)
        {
            //Clear datagridview
            dataGridView1.Rows.Clear();

            // Chest/Item 1:
            Chest chest1 = new Chest(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);

            // Chest/Item 2:
            Chest chest2 = new Chest(textBox10.Text, textBox9.Text, textBox8.Text, textBox7.Text, textBox6.Text);

            // Use these variables to check for first instance of chest and contents
            bool chestSpawn1 = false;
            bool chestFound1 = false;
            int chestFoundPos1 = 0;
            int chestItemPos1 = 0;

            bool chestSpawn2 = false;
            bool chestFound2 = false;
            int chestFoundPos2 = 0;
            int chestItemPos2 = 0;

			// Use these variables to check for first punch combo
			bool comboFound = false;
			int comboPos = 0;

            uint firstRNGVal = displayRNG.genrand();
            uint secondRNGVal = displayRNG.genrand();

            // We want to preserve the character index, since this loop is just for display:
            int indexStatic = group.GetIndex();
            group.ResetIndex();

            int end = start + rowsToRender;
            for (int index = start; index < end; index++)
            {
                // Index starting at 0
                int loopIndex = index - start;

                // Get the heal value once:
                int currentHeal = group.GetHealValue(firstRNGVal);
                int nextHeal = group.PeekHealValue(secondRNGVal);

                // Put the next expected heal in the text box
                if (index == start + healVals.Count - 1)
                {
                    tbLastHeal.Text = nextHeal.ToString();
                }

                // Advance the RNG before starting the loop in case we want to skip an entry
                uint firstRNGVal_temp = firstRNGVal;
                uint secondRNGVal_temp = secondRNGVal;
                firstRNGVal = secondRNGVal;
                secondRNGVal = displayRNG.genrand();

                // Skip the entry if it's too long ago
                if (loopIndex < healVals.Count - historyToDisplay)
                    continue;

                //Start actually displaying
                dataGridView1.Rows.Add();

                // Color consumed RNG green
                if (index < start + healVals.Count)
                {
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGreen;
                }

                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = index;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = currentHeal;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = randToPercent(firstRNGVal_temp);

                // Check if the chests are in a position offset by a fixed amount
                if (chest1.checkSpawn(firstRNGVal_temp))
                {
                    handleChestSpawn(chest1, loopIndex, 3, ref chestFoundPos1, ref chestSpawn1);
                }

                if (chest2.checkSpawn(firstRNGVal_temp))
                {
                    handleChestSpawn(chest2, loopIndex, 4, ref chestFoundPos2, ref chestSpawn2);
                }

                // This is a big conditional to see what is in both chests.
                // There may be a better way, but this was fast to write and doesn't call the RNG.
                // Calculate the contents of the chest. First, gil:
                if (chest1.checkIfGil(firstRNGVal_temp))
                {
                    handleGilReward(chest1, secondRNGVal_temp, 3);
                }
                // Otherwise, what item is it, and where is the first desired item
                else
                {
                    handleItemReward(chest1, secondRNGVal_temp, loopIndex, 3, checkBox1, ref chestItemPos1, ref chestFound1);
                }

                if (chest2.checkIfGil(firstRNGVal_temp))
                {
                    handleGilReward(chest2, secondRNGVal_temp, 4);
                }
                // Otherwise, what item is it, and where is the first desired item
                else
                {
                    handleItemReward(chest2, secondRNGVal_temp, loopIndex, 4, checkBox2, ref chestItemPos2, ref chestFound2);
                }

				// Check for combo during string of punches
				int comboCheck = loopIndex - healVals.Count - 5 + 1 ;
				if (comboCheck%10 == 0 && comboCheck >= 0 && !comboFound && ((firstRNGVal_temp%100) < 3) )
				{
					comboFound = true;
					comboPos = comboCheck/10;
				}
            }

            tbAppear1.Text = chestFoundPos1.ToString();
            tbItem1.Text = chestItemPos1.ToString();

            tbAppear2.Text = chestFoundPos2.ToString();
            tbItem2.Text = chestItemPos2.ToString();

			tbCombo.Text = comboPos.ToString();

            tbLastHeal.Focus();
            tbLastHeal.SelectAll();

            group.SetIndex(indexStatic);
        }

        private IRNG InitializeRNG()
        {
            if (cbPlatform.SelectedItem as string == "PS2")
            {
                return new RNG1998();
            }
            else
            {
                return new RNG2002();
            }
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

        double randToPercent(uint toConvert)
        {
            return toConvert % 100;
        }

        private void tbLevel_Validating(object sender, CancelEventArgs e)
        {
            double tempVal;
            if (!double.TryParse(tbLevel1.Text, out tempVal))
            {
                tbLevel1.Text = "0";
            }
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            btnContinue.Enabled = true;
            bConsume.Enabled = true;

            group.ResetIndex();
            LoadCharacters();

            healVals.Clear();
            searchBuff = new CircularBuffer<uint>(searchBufferSize);
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
            // Store all of the information we need to restore our state if we fail
            int groupIndex_temp = group.GetIndex();
            int index_temp = index;
            // We have to Deep Copy this data
            List<int> healVals_temp = new List<int>(healVals);
            CircularBuffer<uint> searchBuff_temp = searchBuff.DeepClone();
            IRNG rng_temp = searchRNG.DeepClone();

            group.IncrimentIndex();
            if (!FindNext(int.Parse(tbLastHeal.Text)))
            {
                // Restore state
                group.SetIndex(groupIndex_temp);
                index = index_temp;
                // No Deep Copy needed here. We can just re-assign the temps
                // because we won't be touching them again.
                healVals = healVals_temp;
                searchBuff = searchBuff_temp;
                searchRNG = rng_temp;

                MessageBox.Show("Impossible Heal Value entered.");
                return;
            }

            int numRows = ParseNumRows();
            displayRNG(index - healVals.Count + 1, index + numRows);
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
            displayRNGHelper(searchRNG.DeepClone(), 0, numRows);
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