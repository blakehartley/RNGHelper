using System.Windows.Forms;

namespace FF12RNGHelper
{
    partial class FormChest2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stealToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rareGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbLevel1 = new System.Windows.Forms.TextBox();
            this.tbMagic1 = new System.Windows.Forms.TextBox();
            this.ddlSpellPow1 = new System.Windows.Forms.ComboBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblMagic = new System.Windows.Forms.Label();
            this.lblSpellPow = new System.Windows.Forms.Label();
            this.cbSerenity1 = new System.Windows.Forms.CheckBox();
            this.gbStats = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbLevel3 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.tbMagic3 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbLevel2 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbMagic2 = new System.Windows.Forms.TextBox();
            this.cbSerenity3 = new System.Windows.Forms.CheckBox();
            this.ddlSpellPow3 = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cbSerenity2 = new System.Windows.Forms.CheckBox();
            this.ddlSpellPow2 = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Heal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Percent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.steal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Contents2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblLastHeal = new System.Windows.Forms.Label();
            this.tbLastHeal = new System.Windows.Forms.TextBox();
            this.btnBegin = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.groupBoxSearch = new System.Windows.Forms.GroupBox();
            this.tbNumRows = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.tbConsume = new System.Windows.Forms.TextBox();
            this.btnConsume = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBarPercent = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelPercent = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundWorkerConsume = new System.ComponentModel.BackgroundWorker();
            this.gbPlatform = new System.Windows.Forms.GroupBox();
            this.cbPlatform = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.tbAppear1 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tbItem1 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tbAppear2 = new System.Windows.Forms.TextBox();
            this.tbItem2 = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label25 = new System.Windows.Forms.Label();
            this.tbCombo = new System.Windows.Forms.TextBox();
            this.chestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.gbStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBoxSearch.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.gbPlatform.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(838, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chestToolStripMenuItem,
            this.stealToolStripMenuItem,
            this.rareGameToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // stealToolStripMenuItem
            // 
            this.stealToolStripMenuItem.Name = "stealToolStripMenuItem";
            this.stealToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stealToolStripMenuItem.Text = "Steal";
            this.stealToolStripMenuItem.Click += new System.EventHandler(this.stealToolStripMenuItem_Click);
            // 
            // rareGameToolStripMenuItem
            // 
            this.rareGameToolStripMenuItem.Name = "rareGameToolStripMenuItem";
            this.rareGameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rareGameToolStripMenuItem.Text = "Rare Game";
            this.rareGameToolStripMenuItem.Click += new System.EventHandler(this.rareGameToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tbLevel1
            // 
            this.tbLevel1.Location = new System.Drawing.Point(48, 19);
            this.tbLevel1.Name = "tbLevel1";
            this.tbLevel1.Size = new System.Drawing.Size(34, 20);
            this.tbLevel1.TabIndex = 2;
            this.tbLevel1.Text = "3";
            this.tbLevel1.Validating += new System.ComponentModel.CancelEventHandler(this.tbLevel_Validating);
            // 
            // tbMagic1
            // 
            this.tbMagic1.Location = new System.Drawing.Point(125, 19);
            this.tbMagic1.Name = "tbMagic1";
            this.tbMagic1.Size = new System.Drawing.Size(40, 20);
            this.tbMagic1.TabIndex = 3;
            this.tbMagic1.Text = "23";
            this.tbMagic1.Validating += new System.ComponentModel.CancelEventHandler(this.tbMagic_Validating);
            // 
            // ddlSpellPow1
            // 
            this.ddlSpellPow1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSpellPow1.FormattingEnabled = true;
            this.ddlSpellPow1.Items.AddRange(new object[] {
            "Cure",
            "Cura",
            "Curaga",
            "Curaja",
            "Cura IZJS/ZA",
            "Curaga IZJS/ZA",
            "Curaja IZJS/ZA"});
            this.ddlSpellPow1.Location = new System.Drawing.Point(45, 18);
            this.ddlSpellPow1.Name = "ddlSpellPow1";
            this.ddlSpellPow1.Size = new System.Drawing.Size(118, 21);
            this.ddlSpellPow1.TabIndex = 4;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(6, 22);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(36, 13);
            this.lblLevel.TabIndex = 5;
            this.lblLevel.Text = "Level:";
            // 
            // lblMagic
            // 
            this.lblMagic.AutoSize = true;
            this.lblMagic.Location = new System.Drawing.Point(88, 22);
            this.lblMagic.Name = "lblMagic";
            this.lblMagic.Size = new System.Drawing.Size(31, 13);
            this.lblMagic.TabIndex = 6;
            this.lblMagic.Text = "Mag:";
            // 
            // lblSpellPow
            // 
            this.lblSpellPow.AutoSize = true;
            this.lblSpellPow.Location = new System.Drawing.Point(6, 22);
            this.lblSpellPow.Name = "lblSpellPow";
            this.lblSpellPow.Size = new System.Drawing.Size(33, 13);
            this.lblSpellPow.TabIndex = 7;
            this.lblSpellPow.Text = "Spell:";
            // 
            // cbSerenity1
            // 
            this.cbSerenity1.AutoSize = true;
            this.cbSerenity1.Location = new System.Drawing.Point(169, 21);
            this.cbSerenity1.Name = "cbSerenity1";
            this.cbSerenity1.Size = new System.Drawing.Size(64, 17);
            this.cbSerenity1.TabIndex = 9;
            this.cbSerenity1.Text = "Serenity";
            this.cbSerenity1.UseVisualStyleBackColor = true;
            // 
            // gbStats
            // 
            this.gbStats.Controls.Add(this.label20);
            this.gbStats.Controls.Add(this.tbLevel3);
            this.gbStats.Controls.Add(this.label22);
            this.gbStats.Controls.Add(this.tbMagic3);
            this.gbStats.Controls.Add(this.label17);
            this.gbStats.Controls.Add(this.tbLevel2);
            this.gbStats.Controls.Add(this.label19);
            this.gbStats.Controls.Add(this.tbMagic2);
            this.gbStats.Controls.Add(this.lblLevel);
            this.gbStats.Controls.Add(this.tbLevel1);
            this.gbStats.Controls.Add(this.lblMagic);
            this.gbStats.Controls.Add(this.tbMagic1);
            this.gbStats.Location = new System.Drawing.Point(13, 82);
            this.gbStats.Name = "gbStats";
            this.gbStats.Size = new System.Drawing.Size(171, 108);
            this.gbStats.TabIndex = 1;
            this.gbStats.TabStop = false;
            this.gbStats.Text = "Character Stats";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 81);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(36, 13);
            this.label20.TabIndex = 20;
            this.label20.Text = "Level:";
            // 
            // tbLevel3
            // 
            this.tbLevel3.Location = new System.Drawing.Point(48, 78);
            this.tbLevel3.Name = "tbLevel3";
            this.tbLevel3.Size = new System.Drawing.Size(34, 20);
            this.tbLevel3.TabIndex = 17;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(88, 81);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(31, 13);
            this.label22.TabIndex = 21;
            this.label22.Text = "Mag:";
            // 
            // tbMagic3
            // 
            this.tbMagic3.Location = new System.Drawing.Point(125, 78);
            this.tbMagic3.Name = "tbMagic3";
            this.tbMagic3.Size = new System.Drawing.Size(40, 20);
            this.tbMagic3.TabIndex = 18;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 52);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(36, 13);
            this.label17.TabIndex = 13;
            this.label17.Text = "Level:";
            // 
            // tbLevel2
            // 
            this.tbLevel2.Location = new System.Drawing.Point(48, 49);
            this.tbLevel2.Name = "tbLevel2";
            this.tbLevel2.Size = new System.Drawing.Size(34, 20);
            this.tbLevel2.TabIndex = 10;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(88, 52);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(31, 13);
            this.label19.TabIndex = 14;
            this.label19.Text = "Mag:";
            // 
            // tbMagic2
            // 
            this.tbMagic2.Location = new System.Drawing.Point(125, 49);
            this.tbMagic2.Name = "tbMagic2";
            this.tbMagic2.Size = new System.Drawing.Size(40, 20);
            this.tbMagic2.TabIndex = 11;
            // 
            // cbSerenity3
            // 
            this.cbSerenity3.AutoSize = true;
            this.cbSerenity3.Location = new System.Drawing.Point(169, 80);
            this.cbSerenity3.Name = "cbSerenity3";
            this.cbSerenity3.Size = new System.Drawing.Size(64, 17);
            this.cbSerenity3.TabIndex = 23;
            this.cbSerenity3.Text = "Serenity";
            this.cbSerenity3.UseVisualStyleBackColor = true;
            // 
            // ddlSpellPow3
            // 
            this.ddlSpellPow3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSpellPow3.FormattingEnabled = true;
            this.ddlSpellPow3.Items.AddRange(new object[] {
            "Cure",
            "Cura",
            "Curaga",
            "Curaja",
            "Cura IZJS/ZA",
            "Curaga IZJS/ZA",
            "Curaja IZJS/ZA"});
            this.ddlSpellPow3.Location = new System.Drawing.Point(45, 77);
            this.ddlSpellPow3.Name = "ddlSpellPow3";
            this.ddlSpellPow3.Size = new System.Drawing.Size(118, 21);
            this.ddlSpellPow3.TabIndex = 19;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 81);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(33, 13);
            this.label21.TabIndex = 22;
            this.label21.Text = "Spell:";
            // 
            // cbSerenity2
            // 
            this.cbSerenity2.AutoSize = true;
            this.cbSerenity2.Location = new System.Drawing.Point(169, 51);
            this.cbSerenity2.Name = "cbSerenity2";
            this.cbSerenity2.Size = new System.Drawing.Size(64, 17);
            this.cbSerenity2.TabIndex = 16;
            this.cbSerenity2.Text = "Serenity";
            this.cbSerenity2.UseVisualStyleBackColor = true;
            // 
            // ddlSpellPow2
            // 
            this.ddlSpellPow2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSpellPow2.FormattingEnabled = true;
            this.ddlSpellPow2.Items.AddRange(new object[] {
            "Cure",
            "Cura",
            "Curaga",
            "Curaja",
            "Cura IZJS/ZA",
            "Curaga IZJS/ZA",
            "Curaja IZJS/ZA"});
            this.ddlSpellPow2.Location = new System.Drawing.Point(45, 48);
            this.ddlSpellPow2.Name = "ddlSpellPow2";
            this.ddlSpellPow2.Size = new System.Drawing.Size(118, 21);
            this.ddlSpellPow2.TabIndex = 12;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 52);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(33, 13);
            this.label18.TabIndex = 15;
            this.label18.Text = "Spell:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Position,
            this.Heal,
            this.Percent,
            this.steal,
            this.Contents2});
            this.dataGridView1.Location = new System.Drawing.Point(428, 27);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(398, 713);
            this.dataGridView1.TabIndex = 14;
            // 
            // Position
            // 
            this.Position.HeaderText = "Position";
            this.Position.Name = "Position";
            this.Position.ReadOnly = true;
            this.Position.ToolTipText = "Position in the RNG.";
            this.Position.Width = 70;
            // 
            // Heal
            // 
            this.Heal.FillWeight = 50F;
            this.Heal.HeaderText = "Heal";
            this.Heal.Name = "Heal";
            this.Heal.ReadOnly = true;
            this.Heal.ToolTipText = "How much you heal for at this number.";
            this.Heal.Width = 50;
            // 
            // Percent
            // 
            this.Percent.FillWeight = 50F;
            this.Percent.HeaderText = "%";
            this.Percent.Name = "Percent";
            this.Percent.ReadOnly = true;
            this.Percent.ToolTipText = "Random number mod 100 (percent chance events will use this for determinig sucess)" +
    ".";
            this.Percent.Width = 30;
            // 
            // steal
            // 
            this.steal.HeaderText = "Contents 1";
            this.steal.Name = "steal";
            this.steal.ToolTipText = "Current contents of Chest `";
            // 
            // Contents2
            // 
            this.Contents2.HeaderText = "Contents 2";
            this.Contents2.Name = "Contents2";
            this.Contents2.ToolTipText = "Current Contents of Chest 2";
            // 
            // lblLastHeal
            // 
            this.lblLastHeal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLastHeal.AutoSize = true;
            this.lblLastHeal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastHeal.Location = new System.Drawing.Point(5, 25);
            this.lblLastHeal.Name = "lblLastHeal";
            this.lblLastHeal.Size = new System.Drawing.Size(78, 20);
            this.lblLastHeal.TabIndex = 0;
            this.lblLastHeal.Text = "Last heal:";
            // 
            // tbLastHeal
            // 
            this.tbLastHeal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbLastHeal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLastHeal.Location = new System.Drawing.Point(92, 22);
            this.tbLastHeal.Name = "tbLastHeal";
            this.tbLastHeal.Size = new System.Drawing.Size(79, 26);
            this.tbLastHeal.TabIndex = 11;
            this.tbLastHeal.Text = "90";
            this.tbLastHeal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbLastHeal_KeyPress);
            this.tbLastHeal.Validating += new System.ComponentModel.CancelEventHandler(this.tbLastHeal_Validating);
            // 
            // btnBegin
            // 
            this.btnBegin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBegin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBegin.Location = new System.Drawing.Point(174, 22);
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.Size = new System.Drawing.Size(94, 26);
            this.btnBegin.TabIndex = 13;
            this.btnBegin.Text = "Start";
            this.btnBegin.UseVisualStyleBackColor = true;
            this.btnBegin.Click += new System.EventHandler(this.btnBegin_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnContinue.Enabled = false;
            this.btnContinue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinue.Location = new System.Drawing.Point(274, 22);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(91, 26);
            this.btnContinue.TabIndex = 12;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // groupBoxSearch
            // 
            this.groupBoxSearch.Controls.Add(this.tbNumRows);
            this.groupBoxSearch.Controls.Add(this.label24);
            this.groupBoxSearch.Controls.Add(this.btnClear);
            this.groupBoxSearch.Controls.Add(this.tbConsume);
            this.groupBoxSearch.Controls.Add(this.btnConsume);
            this.groupBoxSearch.Controls.Add(this.label23);
            this.groupBoxSearch.Controls.Add(this.tbLastHeal);
            this.groupBoxSearch.Controls.Add(this.btnContinue);
            this.groupBoxSearch.Controls.Add(this.lblLastHeal);
            this.groupBoxSearch.Controls.Add(this.btnBegin);
            this.groupBoxSearch.Location = new System.Drawing.Point(13, 443);
            this.groupBoxSearch.Name = "groupBoxSearch";
            this.groupBoxSearch.Size = new System.Drawing.Size(410, 122);
            this.groupBoxSearch.TabIndex = 2;
            this.groupBoxSearch.TabStop = false;
            this.groupBoxSearch.Text = "RNG Search";
            // 
            // tbNumRows
            // 
            this.tbNumRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbNumRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNumRows.Location = new System.Drawing.Point(91, 86);
            this.tbNumRows.Name = "tbNumRows";
            this.tbNumRows.Size = new System.Drawing.Size(79, 26);
            this.tbNumRows.TabIndex = 19;
            this.tbNumRows.Text = "100";
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(5, 89);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 20);
            this.label24.TabIndex = 18;
            this.label24.Text = "Rows:";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(274, 54);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(91, 26);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tbConsume
            // 
            this.tbConsume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbConsume.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbConsume.Location = new System.Drawing.Point(91, 54);
            this.tbConsume.Name = "tbConsume";
            this.tbConsume.Size = new System.Drawing.Size(79, 26);
            this.tbConsume.TabIndex = 15;
            this.tbConsume.Text = "10";
            // 
            // btnConsume
            // 
            this.btnConsume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConsume.Enabled = false;
            this.btnConsume.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsume.Location = new System.Drawing.Point(177, 54);
            this.btnConsume.Name = "btnConsume";
            this.btnConsume.Size = new System.Drawing.Size(91, 26);
            this.btnConsume.TabIndex = 16;
            this.btnConsume.Text = "Consume";
            this.btnConsume.UseVisualStyleBackColor = true;
            this.btnConsume.Click += new System.EventHandler(this.bConsume_Click);
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(5, 57);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(81, 20);
            this.label23.TabIndex = 14;
            this.label23.Text = "Consume:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBarPercent,
            this.toolStripStatusLabelPercent,
            this.toolStripStatusLabelProgress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 743);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(838, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBarPercent
            // 
            this.toolStripProgressBarPercent.Name = "toolStripProgressBarPercent";
            this.toolStripProgressBarPercent.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabelPercent
            // 
            this.toolStripStatusLabelPercent.Name = "toolStripStatusLabelPercent";
            this.toolStripStatusLabelPercent.Size = new System.Drawing.Size(17, 17);
            this.toolStripStatusLabelPercent.Text = "%";
            // 
            // toolStripStatusLabelProgress
            // 
            this.toolStripStatusLabelProgress.Name = "toolStripStatusLabelProgress";
            this.toolStripStatusLabelProgress.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabelProgress.Text = "toolStripStatusLabel1";
            // 
            // backgroundWorkerConsume
            // 
            this.backgroundWorkerConsume.WorkerReportsProgress = true;
            this.backgroundWorkerConsume.WorkerSupportsCancellation = true;
            this.backgroundWorkerConsume.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerConsume_DoWork);
            this.backgroundWorkerConsume.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerConsume_ProgressChanged);
            this.backgroundWorkerConsume.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerConsume_RunWorkerCompleted);
            // 
            // gbPlatform
            // 
            this.gbPlatform.Controls.Add(this.cbPlatform);
            this.gbPlatform.Location = new System.Drawing.Point(13, 27);
            this.gbPlatform.Name = "gbPlatform";
            this.gbPlatform.Size = new System.Drawing.Size(410, 49);
            this.gbPlatform.TabIndex = 2;
            this.gbPlatform.TabStop = false;
            this.gbPlatform.Text = "Platform";
            // 
            // cbPlatform
            // 
            this.cbPlatform.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlatform.FormattingEnabled = true;
            this.cbPlatform.Items.AddRange(new object[] {
            "PS2",
            "PS4"});
            this.cbPlatform.Location = new System.Drawing.Point(7, 18);
            this.cbPlatform.Name = "cbPlatform";
            this.cbPlatform.Size = new System.Drawing.Size(87, 21);
            this.cbPlatform.TabIndex = 10;
            this.cbPlatform.SelectionChangeCommitted += new System.EventHandler(this.cbPlatform_SelectionChangeCommitted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(13, 196);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 172);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chest 1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Want Item 1:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(101, 149);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(101, 123);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(49, 20);
            this.textBox5.TabIndex = 9;
            this.textBox5.Text = "100";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Gil Amount:";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(101, 97);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(49, 20);
            this.textBox4.TabIndex = 7;
            this.textBox4.Text = "50";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Item 1 Chance:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Gil Chance:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(101, 71);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(49, 20);
            this.textBox3.TabIndex = 4;
            this.textBox3.Text = "50";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(101, 45);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(49, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "RNG Position:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Appears Chance:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(101, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(49, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "50";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.checkBox2);
            this.groupBox2.Controls.Add(this.textBox6);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBox7);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.textBox8);
            this.groupBox2.Controls.Add(this.textBox9);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.textBox10);
            this.groupBox2.Location = new System.Drawing.Point(219, 196);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(204, 172);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chest 2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Want Item 1:";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(101, 149);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 10;
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(101, 123);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(49, 20);
            this.textBox6.TabIndex = 9;
            this.textBox6.Text = "100";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Gil Amount:";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(101, 97);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(49, 20);
            this.textBox7.TabIndex = 7;
            this.textBox7.Text = "50";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Item 1 Chance:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Gil Chance:";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(101, 71);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(49, 20);
            this.textBox8.TabIndex = 4;
            this.textBox8.Text = "50";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(101, 45);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(49, 20);
            this.textBox9.TabIndex = 3;
            this.textBox9.Text = "1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "RNG Position:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Appears Chance:";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(101, 19);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(49, 20);
            this.textBox10.TabIndex = 0;
            this.textBox10.Text = "50";
            // 
            // tbAppear1
            // 
            this.tbAppear1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAppear1.Enabled = false;
            this.tbAppear1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAppear1.Location = new System.Drawing.Point(125, 7);
            this.tbAppear1.Name = "tbAppear1";
            this.tbAppear1.Size = new System.Drawing.Size(79, 25);
            this.tbAppear1.TabIndex = 17;
            this.tbAppear1.Text = "?";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(102, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Advance to Appear:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 47);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(91, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "Advance for Item:";
            // 
            // tbItem1
            // 
            this.tbItem1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbItem1.Enabled = false;
            this.tbItem1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbItem1.Location = new System.Drawing.Point(125, 38);
            this.tbItem1.Name = "tbItem1";
            this.tbItem1.Size = new System.Drawing.Size(79, 25);
            this.tbItem1.TabIndex = 19;
            this.tbItem1.Text = "?";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.tbAppear1);
            this.groupBox3.Controls.Add(this.tbItem1);
            this.groupBox3.Location = new System.Drawing.Point(13, 374);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(204, 68);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.tbAppear2);
            this.groupBox4.Controls.Add(this.tbItem2);
            this.groupBox4.Location = new System.Drawing.Point(219, 374);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(204, 68);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(102, 13);
            this.label15.TabIndex = 12;
            this.label15.Text = "Advance to Appear:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 47);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(91, 13);
            this.label16.TabIndex = 18;
            this.label16.Text = "Advance for Item:";
            // 
            // tbAppear2
            // 
            this.tbAppear2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAppear2.Enabled = false;
            this.tbAppear2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAppear2.Location = new System.Drawing.Point(125, 7);
            this.tbAppear2.Name = "tbAppear2";
            this.tbAppear2.Size = new System.Drawing.Size(79, 25);
            this.tbAppear2.TabIndex = 17;
            this.tbAppear2.Text = "?";
            // 
            // tbItem2
            // 
            this.tbItem2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbItem2.Enabled = false;
            this.tbItem2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbItem2.Location = new System.Drawing.Point(125, 38);
            this.tbItem2.Name = "tbItem2";
            this.tbItem2.Size = new System.Drawing.Size(79, 25);
            this.tbItem2.TabIndex = 19;
            this.tbItem2.Text = "?";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblSpellPow);
            this.groupBox5.Controls.Add(this.cbSerenity3);
            this.groupBox5.Controls.Add(this.cbSerenity1);
            this.groupBox5.Controls.Add(this.ddlSpellPow1);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.ddlSpellPow3);
            this.groupBox5.Controls.Add(this.ddlSpellPow2);
            this.groupBox5.Controls.Add(this.cbSerenity2);
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Location = new System.Drawing.Point(187, 82);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(236, 108);
            this.groupBox5.TabIndex = 22;
            this.groupBox5.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label25);
            this.groupBox6.Controls.Add(this.tbCombo);
            this.groupBox6.Location = new System.Drawing.Point(13, 571);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(204, 40);
            this.groupBox6.TabIndex = 21;
            this.groupBox6.TabStop = false;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 16);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(110, 13);
            this.label25.TabIndex = 12;
            this.label25.Text = "Combo after punch #:";
            // 
            // tbCombo
            // 
            this.tbCombo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbCombo.Enabled = false;
            this.tbCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCombo.Location = new System.Drawing.Point(125, 7);
            this.tbCombo.Name = "tbCombo";
            this.tbCombo.Size = new System.Drawing.Size(73, 25);
            this.tbCombo.TabIndex = 17;
            this.tbCombo.Text = "?";
            // 
            // chestToolStripMenuItem
            // 
            this.chestToolStripMenuItem.Name = "chestToolStripMenuItem";
            this.chestToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.chestToolStripMenuItem.Text = "Chest";
            this.chestToolStripMenuItem.Click += new System.EventHandler(this.chestToolStripMenuItem_Click);
            // 
            // FormChest2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 765);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbPlatform);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBoxSearch);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.gbStats);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(557, 601);
            this.Name = "FormChest2";
            this.Text = "FF12 RNG Helper";
            this.Load += new System.EventHandler(this.FormChest_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbStats.ResumeLayout(false);
            this.gbStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBoxSearch.ResumeLayout(false);
            this.groupBoxSearch.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gbPlatform.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TextBox tbLevel1;
        private System.Windows.Forms.TextBox tbMagic1;
        private System.Windows.Forms.ComboBox ddlSpellPow1;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblMagic;
        private System.Windows.Forms.Label lblSpellPow;
        private System.Windows.Forms.CheckBox cbSerenity1;
        private System.Windows.Forms.GroupBox gbStats;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblLastHeal;
        private System.Windows.Forms.TextBox tbLastHeal;
        private System.Windows.Forms.Button btnBegin;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.GroupBox groupBoxSearch;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarPercent;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPercent;
        private System.ComponentModel.BackgroundWorker backgroundWorkerConsume;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelProgress;
        private System.Windows.Forms.GroupBox gbPlatform;
        private System.Windows.Forms.ComboBox cbPlatform;
        private System.Windows.Forms.ToolStripMenuItem stealToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox tbAppear1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbItem1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbAppear2;
        private System.Windows.Forms.TextBox tbItem2;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.CheckBox cbSerenity3;
        private System.Windows.Forms.TextBox tbLevel3;
        private System.Windows.Forms.ComboBox ddlSpellPow3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox tbMagic3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox cbSerenity2;
        private System.Windows.Forms.TextBox tbLevel2;
        private System.Windows.Forms.ComboBox ddlSpellPow2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbMagic2;
        private System.Windows.Forms.ToolStripMenuItem rareGameToolStripMenuItem;
        private System.Windows.Forms.TextBox tbConsume;
        private System.Windows.Forms.Button btnConsume;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox tbNumRows;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Position;
        private System.Windows.Forms.DataGridViewTextBoxColumn Heal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Percent;
        private System.Windows.Forms.DataGridViewTextBoxColumn steal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Contents2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tbCombo;
        private ToolStripMenuItem chestToolStripMenuItem;
    }
}

