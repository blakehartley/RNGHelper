using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FF12RNGHelper
{
    public partial class FormChest : Form
    {
        IRNG searchRNG;
        IRNG dispRNG;
        UInt64 index;
        CircularBuffer<UInt32> searchBuff;
        List<UInt32> healVals;
        double level;
        double mag;
        uint spell;
        double serenityMult;
        enum Spells : uint {Cure=20,Cura=45, Curaga=85, Curaja=145, CuraIZJS=46, CuragaIZJS=86, CurajaIZJS=120 }
        System.Diagnostics.Stopwatch aStopwatch = new System.Diagnostics.Stopwatch();

        public FormChest()
        {
            InitializeComponent();

            cbSpellPow.SelectedIndex = 0;
            cbPlatform.SelectedIndex = 0;
            healVals = new List<UInt32>();
            searchRNG = new RNG1998();
            dispRNG = new RNG1998();
            toolStripStatusLabelPercent.Text = "";
            toolStripStatusLabelProgress.Text = "";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void parseThings()
        {
            //Parse the boxes
            level = double.Parse(tbLevel.Text);
            mag = double.Parse(tbMagic.Text);
            switch (cbSpellPow.SelectedIndex)
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
            if (chkbSerenity.Checked)
            {
                serenityMult = 1.5;
            }
            else serenityMult = 1;
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            btnContinue.Enabled = true;
            //Parse the boxes
            parseThings();
            
            healVals.Clear();
            searchBuff = new CircularBuffer<UInt32>(100);
            searchRNG.sgenrand();
            searchBuff.Add(searchRNG.genrand());
            index = 0;
            if (!findNext(UInt32.Parse(tbLastHeal.Text)))
            {
                btnContinue.Enabled = false;
                MessageBox.Show("Impossible Heal Value entered.");
                return;
            }
            displayRNG(index, index + 500);
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            DateTime begint = DateTime.Now;
            if(!findNext(UInt32.Parse(tbLastHeal.Text)))
            {
                MessageBox.Show("Impossible Heal Value entered.");
                return;
            }
            DateTime endt = DateTime.Now;
            toolStripStatusLabelPercent.Text = (endt - begint).ToString();
            displayRNG(index-(ulong)healVals.Count+1, index + 500);
        }

        private bool findNext(UInt32 value)
        {
            //Do a range check before trying this out to avoid entering an infinite loop.
            if(value > healMax() || value < healMin())
            {
                return false;
            }
            healVals.Add(value);
            searchBuff.Add(searchRNG.genrand());
            index++;
            bool match;
            do
            {
                match = true;
                for (int i = 0; i < healVals.Count; i++)
                {
                    if (randToHeal(searchBuff[index - (ulong)i]) != healVals[healVals.Count - 1 - i])
                    {
                        match = false;
                        break;
                    }
                }
                if(!match)
                {
                    searchBuff.Add(searchRNG.genrand());
                    index++;
                }
            } while (!match);
            return true;
        }

        UInt32 randToHeal(UInt32 toConvert)
        {
            double healAmount = (spell + (toConvert % (spell * 12.5)) / 100) * (2 + mag * (level + mag) / 256) * serenityMult;
            return (UInt32)healAmount;
        }

        UInt32 healMax()
        {
            return (UInt32)(spell * 1.125 * (2 + mag * (level + mag) / 256) * serenityMult);
        }

        UInt32 healMin()
        {
            return (UInt32)(spell * (2 + mag * (level + mag) / 256) * serenityMult);
        }

        UInt32 randToPercent(UInt32 toConvert)
        {
            return toConvert % 100;
        }

        private void displayRNG(UInt64 end)
        {
            displayRNG(0, end);
        }

        private bool chestCheck(UInt32 PRNG, double chestChance, bool flip)
        {
            if (flip)
                return (randToPercent(PRNG) < chestChance);
            else
                return (randToPercent(PRNG) > chestChance);
        }

        private void displayRNG(UInt64 start, UInt64 end)
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
            for (UInt64 i = 0; i < start; i++)
            {
                displayRNG.genrand();
            }
            DateTime endtt = DateTime.Now;
            toolStripStatusLabelPercent.Text = (endtt - startt).Milliseconds.ToString();

            // Chest/Item 1:
            double chestSpawnChance1, chestGilChance1, chestItemChance1, chestGilAmount1;
            uint chestRNGPosition1;

            double.TryParse(textBox1.Text, out chestSpawnChance1);
            uint.TryParse(textBox2.Text, out chestRNGPosition1);
            double.TryParse(textBox3.Text, out chestGilChance1);
            double.TryParse(textBox4.Text, out chestItemChance1);
            double.TryParse(textBox5.Text, out chestGilAmount1);

            // Chest/Item 2:
            double chestSpawnChance2, chestGilChance2, chestItemChance2, chestGilAmount2;
            uint chestRNGPosition2;

            double.TryParse(textBox10.Text, out chestSpawnChance2);
            uint.TryParse(textBox9.Text, out chestRNGPosition2);
            double.TryParse(textBox8.Text, out chestGilChance2);
            double.TryParse(textBox7.Text, out chestItemChance2);
            double.TryParse(textBox6.Text, out chestGilAmount2);

            // Use these variables to check for first instance of chest and contents
            bool chestFound1 = false;
            uint chestFoundPos1 = 0, chestItemPos1 = 0;

            bool chestFound2 = false;
            uint chestFoundPos2 = 0, chestItemPos2 = 0;

            UInt32 aVal1 = displayRNG.genrand();
            UInt32 aVal2 = displayRNG.genrand();

            for (UInt64 i = start; i < end; i++)
            {
                // Index starting at 0
                uint j = (uint)i - (uint)start;

                // Put the next expected heal in the text box
                if (i == start + (ulong)healVals.Count - 1)
                    tbLastHeal.Text = randToHeal(aVal2).ToString();

                //Start actually displaying
                dataGridView1.Rows.Add();

                // Color consumed RNG green
                if (i < start + (ulong)healVals.Count)
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGreen;

                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = i;
                //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = aVal1;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = randToHeal(aVal1);
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = randToPercent(aVal1);

                // Check if the chests are in a position offset by a fixed amount
                
                if ( chestCheck(aVal1, chestSpawnChance1, true) )
                {
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Style.Font = new Font(dataGridView1.CurrentCell.InheritedStyle.Font, FontStyle.Bold);
                    if (j >= healVals.Count && !chestFound1)
                        chestFoundPos1 = j - (uint)healVals.Count - chestRNGPosition1;
                }
                if ( chestCheck(aVal1, chestSpawnChance2, true))
                {
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Style.Font = new Font(dataGridView1.CurrentCell.InheritedStyle.Font, FontStyle.Bold);
                    if (j >= healVals.Count && !chestFound2)
                        chestFoundPos2 = j - (uint)healVals.Count - chestRNGPosition2;
                }

                // This is a big conditional to see what is in both chests.
                // There may be a better way, but this was fast to write and doesn't call the RNG.
                // Calculate the contents of the chest. First, gil:
                if ( chestCheck(aVal1, chestGilChance1, true) )
                {
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = 1 + (aVal2 % chestGilAmount1);
                } // Otherwise, what item is it, and where is the first desired item
                else
                {
                    if (chestCheck(aVal2, chestItemChance1, true))
                    {
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = (object)"Item 1";

                        // Check if the items are in this position
                        if ((checkBox1.Checked && j >= healVals.Count) && !chestFound1)
                        {
                            chestItemPos1 = j - (uint)healVals.Count;
                            chestFound1 = true;
                        }
                    }
                    else
                    {
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = (object)"Item 2";

                        // Check if the items are in this position
                        if ((!checkBox1.Checked && j >= healVals.Count) && !chestFound1)
                        {
                            chestItemPos1 = j - (uint)healVals.Count;
                            chestFound1 = true;
                        }
                    }
                }
                if (chestCheck(aVal1, chestGilChance2, true))
                {
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value = 1 + (aVal2 % chestGilAmount2);
                } // Otherwise, what item is it, and where is the first desired item
                else
                {
                    if (chestCheck(aVal2, chestItemChance2, true))
                    {
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value = (object)"Item 1";

                        // Check if the items are in this position
                        if ((checkBox2.Checked && j >= healVals.Count) && !chestFound2)
                        {
                            chestItemPos2 = j - (uint)healVals.Count;
                            chestFound2 = true;
                        }
                    }
                    else
                    {
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value = (object)"Item 2";

                        // Check if the items are in this position
                        if ((!checkBox2.Checked && j >= healVals.Count) && !chestFound2)
                        {
                            chestItemPos2 = j - (uint)healVals.Count;
                            chestFound2 = true;
                        }
                    }
                }

                // Advance the RNG
                aVal1 = aVal2;
                aVal2 = displayRNG.genrand();
            }

            tbAppear1.Text = chestFoundPos1.ToString();
            tbItem1.Text = chestItemPos1.ToString();

            tbAppear2.Text = chestFoundPos2.ToString();
            tbItem2.Text = chestItemPos2.ToString();

            tbLastHeal.Focus();
            tbLastHeal.SelectAll();
        }

        private string stealCompute(uint currentVal, IRNG anRNG, bool thiefCuff)
        {
            string returnStr = "";
            //Lazy way of looking ahead in the RNG. Probably adds some overhead to the display function.
            RNGState rngState = anRNG.saveState();
            uint firstPercent = currentVal % 100;
            uint secondPercent = anRNG.genrand() % 100;
            uint thirdPercent = anRNG.genrand() % 100;
            anRNG.loadState(rngState);
            if(thiefCuff)
            {
                if(firstPercent < 6)
                {
                    returnStr += "Rare";
                }
                if(secondPercent < 30)
                {
                    returnStr += " + Uncommon";
                }
                if(thirdPercent < 80)
                {
                    returnStr += " + Common";
                }
            }
            else
            {
                if(firstPercent < 3)
                {
                    returnStr = "Rare";
                }
                else if(secondPercent < 10)
                {
                    returnStr = "Uncommon";
                }
                else if(thirdPercent < 55)
                {
                    returnStr = "Common";
                }
            }
            if (returnStr == "")
            {
                returnStr = "None";
            }
            return returnStr.TrimStart(new char[]{' ','+'});
        }
        private void tbLevel_Validating(object sender, CancelEventArgs e)
        {
            double tempVal;
            if(!double.TryParse(tbLevel.Text,out tempVal))
            {
                tbLevel.Text = "0";
            }
        }

        private void tbMagic_Validating(object sender, CancelEventArgs e)
        {
            double tempVal;
            if (!double.TryParse(tbMagic.Text, out tempVal))
            {
                tbMagic.Text = "0";
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

        private void backgroundWorkerConsume_DoWork(object sender, DoWorkEventArgs e)
        {
            //Start the party!
            Tuple<ulong,ulong> inputArgs = (Tuple<ulong, ulong>)e.Argument;
            BackgroundWorker bw = sender as BackgroundWorker;
            dispRNG.consumeBG(inputArgs.Item1, bw, e);
            if(bw.CancellationPending)
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
                Tuple<ulong, ulong> inputArgs = (Tuple<ulong, ulong>)e.Result;
                
                for (UInt64 i = inputArgs.Item1; i < inputArgs.Item2; i++)
                {
                    //Start actually displaying
                    UInt32 aVal = dispRNG.genrand();
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = i;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = aVal;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = randToHeal(aVal);
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
            if(cbPlatform.SelectedItem as string == "PS2" && searchRNG is RNG2002)
            {
                btnContinue.Enabled = false;
                dataGridView1.Rows.Clear();
                dispRNG = new RNG1998();
                searchRNG = new RNG1998();
            }
            else if(cbPlatform.SelectedItem as string == "PS4" && searchRNG is RNG1998)
            {
                btnContinue.Enabled = false;
                dataGridView1.Rows.Clear();
                dispRNG = new RNG2002();
                searchRNG = new RNG2002();
            }
        }

        private void FormChest_Load(object sender, EventArgs e)
        {
            displayRNG(index - (ulong)healVals.Count + 1, index + 500);
        }

        private void stealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormMain().Show();
            this.FindForm().Hide();
        }

        private void tbLastHeal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnContinue_Click(sender, e);
            }
        }
    }
}
