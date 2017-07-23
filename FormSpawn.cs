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
    public partial class FormSpawn : Form
    {
        IRNG searchRNG;
        IRNG dispRNG;
        UInt64 index;	// Current index in the PRNG list
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

		private long ParseNumRows()
		{
			long numRows;
			long.TryParse(tbNumRows.Text, out numRows);
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
            searchBuff = new CircularBuffer<UInt32>(100);
            searchRNG.sgenrand();
            searchBuff.Add(searchRNG.genrand());
            index = 0;
            if (!FindNext(UInt32.Parse(tbLastHeal.Text)))
            {
                btnContinue.Enabled = false;
                MessageBox.Show("Impossible Heal Value entered.");
                return;
            }
			long numRows = ParseNumRows();
			displayRNG(index, index + (ulong) numRows);
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            DateTime begint = DateTime.Now;
			group.IncrimentIndex();
            if(!FindNext(UInt32.Parse(tbLastHeal.Text)))
            {
                MessageBox.Show("Impossible Heal Value entered.");
                return;
            }
            DateTime endt = DateTime.Now;
            toolStripStatusLabelPercent.Text = (endt - begint).ToString();
			long numRows = ParseNumRows();
			displayRNG(index-(ulong)healVals.Count+1, index + (ulong) numRows);
        }

        private bool FindNext(uint value)
        {
            // Do a range check before trying this out to avoid entering an infinite loop.
            if(value > group.HealMax() || value < group.HealMin())
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
            do
            {
				// Reset the index to start the scan again
				group.ResetIndex();
				match = true;
				for (int i = 0; i < healVals.Count; i++)
                {
					// index of first heal:
					long index0 = (long) index - healVals.Count + 1;

					if (group.GetHealValue(searchBuff[index0 + i]) != healVals[i])
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

			group.SetIndex(indexStatic);
			return true;
        }

        UInt32 randToPercent(UInt32 toConvert)
        {
            return toConvert % 100;
        }

        private void displayRNG(UInt64 end)
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

			// Rare 1:
			double spawnMin1, spawnMax1;
            uint rareRNGPosition1;

            double.TryParse(tbMin1.Text, out spawnMin1);
			double.TryParse(tbMax1.Text, out spawnMax1);
			uint.TryParse(tbRNG1.Text, out rareRNGPosition1);

			rareRNGPosition1++;

			// Convert to fraction:
			spawnMin1 /= 100.0;
			spawnMax1 /= 100.0;

			// Rare 2:
			double spawnMin2, spawnMax2;
			uint rareRNGPosition2;

			double.TryParse(tbMin2.Text, out spawnMin2);
			double.TryParse(tbMax2.Text, out spawnMax2);
			uint.TryParse(tbRNG2.Text, out rareRNGPosition2);

			rareRNGPosition2++;

			// Convert to fraction:
			spawnMin2 /= 100.0;
			spawnMax2 /= 100.0;

			// Use these variables to check for first instance of chest and contents
			bool rareSpawn1 = false;
			uint rareFoundPos1 = 0;

			bool rareSpawn2 = false;
			uint rareFoundPos2 = 0;

            UInt32 aVal1 = displayRNG.genrand();
            UInt32 aVal2 = displayRNG.genrand();

			// We want to preserve the character index, since this loop is just for display:
			int indexStatic = group.GetIndex();
			group.ResetIndex();

			//group.ResetIndex();
            for (UInt64 i = start; i < end; i++)
            {
                // Index starting at 0
                uint j = (uint)i - (uint)start;

				// Get the heal value once:
				int healNow = group.GetHealValue(aVal1);
				int healNext = group.PeekHealValue(aVal2);

				// Get chance to spawn rare game
				float spawnChance = (float)aVal1 / 4294967296;

				// Put the next expected heal in the text box
				if (i == start + (ulong)healVals.Count - 1)
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
				if (j < healVals.Count - 5)
					continue;

				//Start actually displaying
				dataGridView1.Rows.Add();

                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = i;
                //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = aVal1;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = healNow;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = spawnChance;
				dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = aVal1_temp.ToString("N0");

				// Check if the chests are in a position offset by a fixed amount

				if ( rareCheck(spawnChance, spawnMin1, spawnMax1) )
                {
					int chestFirstChance = healVals.Count + (int)rareRNGPosition1;

					dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightBlue;
					if (j >= chestFirstChance && !rareSpawn1)
					{
						rareFoundPos1 = j - (uint)healVals.Count - rareRNGPosition1;
						rareSpawn1 = true;
					}
                }
				if ( rareCheck(spawnChance, spawnMin2, spawnMax2) )
				{
					int chestFirstChance = healVals.Count + (int)rareRNGPosition2;

					dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Crimson;
					if (j >= chestFirstChance && !rareSpawn2)
					{
						rareFoundPos2 = j - (uint)healVals.Count - rareRNGPosition2;
						rareSpawn2 = true;
					}
				}
				if (rareCheck(spawnChance, spawnMin1, spawnMax1) && rareCheck(spawnChance, spawnMin2, spawnMax2))
				{
					int chestFirstChance = healVals.Count + (int)rareRNGPosition2;

					dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.MediumPurple;
				}

				// Color consumed RNG green
				if (i < start + (ulong)healVals.Count)
					dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGreen;
			}

			tbAppear1.Text = rareFoundPos1.ToString();

			tbAppear2.Text = rareFoundPos2.ToString();

            tbLastHeal.Focus();
            tbLastHeal.SelectAll();

			group.SetIndex(indexStatic);
        }

        private void tbLevel_Validating(object sender, CancelEventArgs e)
        {
            double tempVal;
            if(!double.TryParse(tbLevel1.Text,out tempVal))
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

        private void backgroundWorkerConsume_DoWork(object sender, DoWorkEventArgs e)
        {
            //Start the party!
            Tuple<ulong,ulong> inputArgs = (Tuple<ulong, ulong>)e.Argument;
            BackgroundWorker bw = sender as BackgroundWorker;
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
			long numRows = ParseNumRows();
			displayRNG(index - (ulong)healVals.Count + 1, index + (ulong) numRows);
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
			new FormChest().Show();
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
				if (!FindNext(UInt32.Parse(tbLastHeal.Text)))
				{
					MessageBox.Show("Impossible Heal Value entered.");
					return;
				}
				displayRNG(index - (ulong)healVals.Count + 1, index +1);
			}
			DateTime endt = DateTime.Now;
			toolStripStatusLabelPercent.Text = (endt - begint).ToString();
			long numRows = ParseNumRows();
			displayRNG(index - (ulong)healVals.Count + 1, index + (ulong)numRows);
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