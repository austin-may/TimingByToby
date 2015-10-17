namespace TimingForToby
{
    partial class MainWindow
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabRunners = new System.Windows.Forms.TabPage();
            this.btnAddRunner = new System.Windows.Forms.Button();
            this.dataGridRunners = new System.Windows.Forms.DataGridView();
            this.tabResults = new System.Windows.Forms.TabPage();
            this.resultTable = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.tabTiming = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonKB = new System.Windows.Forms.RadioButton();
            this.radioButtonTM = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnEndRace = new System.Windows.Forms.Button();
            this.btnStartRace = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridTiming = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabRunners.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRunners)).BeginInit();
            this.tabResults.SuspendLayout();
            this.tabTiming.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTiming)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabRunners);
            this.tabControl1.Controls.Add(this.tabResults);
            this.tabControl1.Controls.Add(this.tabTiming);
            this.tabControl1.Location = new System.Drawing.Point(16, 33);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1313, 561);
            this.tabControl1.TabIndex = 0;
            // 
            // tabRunners
            // 
            this.tabRunners.AutoScroll = true;
            this.tabRunners.Controls.Add(this.btnAddRunner);
            this.tabRunners.Controls.Add(this.dataGridRunners);
            this.tabRunners.Location = new System.Drawing.Point(4, 25);
            this.tabRunners.Margin = new System.Windows.Forms.Padding(4);
            this.tabRunners.Name = "tabRunners";
            this.tabRunners.Padding = new System.Windows.Forms.Padding(4);
            this.tabRunners.Size = new System.Drawing.Size(1305, 532);
            this.tabRunners.TabIndex = 0;
            this.tabRunners.Text = "Runners";
            this.tabRunners.UseVisualStyleBackColor = true;
            // 
            // btnAddRunner
            // 
            this.btnAddRunner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRunner.Location = new System.Drawing.Point(1149, 26);
            this.btnAddRunner.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddRunner.Name = "btnAddRunner";
            this.btnAddRunner.Size = new System.Drawing.Size(100, 28);
            this.btnAddRunner.TabIndex = 1;
            this.btnAddRunner.Text = "Add Runner";
            this.btnAddRunner.UseVisualStyleBackColor = true;
            this.btnAddRunner.Click += new System.EventHandler(this.btnAddRunner_Click);
            // 
            // dataGridRunners
            // 
            this.dataGridRunners.AllowUserToOrderColumns = true;
            this.dataGridRunners.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridRunners.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridRunners.Location = new System.Drawing.Point(25, 7);
            this.dataGridRunners.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridRunners.Name = "dataGridRunners";
            this.dataGridRunners.Size = new System.Drawing.Size(1069, 514);
            this.dataGridRunners.TabIndex = 0;
            // 
            // tabResults
            // 
            this.tabResults.Controls.Add(this.resultTable);
            this.tabResults.Controls.Add(this.button1);
            this.tabResults.Controls.Add(this.label1);
            this.tabResults.Controls.Add(this.checkedListBox1);
            this.tabResults.Location = new System.Drawing.Point(4, 25);
            this.tabResults.Margin = new System.Windows.Forms.Padding(4);
            this.tabResults.Name = "tabResults";
            this.tabResults.Padding = new System.Windows.Forms.Padding(4);
            this.tabResults.Size = new System.Drawing.Size(1305, 532);
            this.tabResults.TabIndex = 1;
            this.tabResults.Text = "Results";
            this.tabResults.UseVisualStyleBackColor = true;
            // 
            // resultTable
            // 
            this.resultTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultTable.AutoScroll = true;
            this.resultTable.ColumnCount = 1;
            this.resultTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.resultTable.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.resultTable.Location = new System.Drawing.Point(9, 9);
            this.resultTable.Margin = new System.Windows.Forms.Padding(4);
            this.resultTable.Name = "resultTable";
            this.resultTable.RowCount = 2;
            this.resultTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.resultTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95F));
            this.resultTable.Size = new System.Drawing.Size(1039, 513);
            this.resultTable.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1195, 494);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 3;
            this.button1.Text = "Export";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1121, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filters";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Age",
            "Sex",
            "Real Bib Ids"});
            this.checkedListBox1.Location = new System.Drawing.Point(1071, 38);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(179, 106);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // tabTiming
            // 
            this.tabTiming.Controls.Add(this.groupBox1);
            this.tabTiming.Controls.Add(this.btnEndRace);
            this.tabTiming.Controls.Add(this.btnStartRace);
            this.tabTiming.Controls.Add(this.label2);
            this.tabTiming.Controls.Add(this.dataGridTiming);
            this.tabTiming.Location = new System.Drawing.Point(4, 25);
            this.tabTiming.Margin = new System.Windows.Forms.Padding(4);
            this.tabTiming.Name = "tabTiming";
            this.tabTiming.Size = new System.Drawing.Size(1305, 532);
            this.tabTiming.TabIndex = 2;
            this.tabTiming.Text = "Timing";
            this.tabTiming.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonKB);
            this.groupBox1.Controls.Add(this.radioButtonTM);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Location = new System.Drawing.Point(971, 84);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(300, 123);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Timing Method";
            // 
            // radioButtonKB
            // 
            this.radioButtonKB.AutoSize = true;
            this.radioButtonKB.Checked = true;
            this.radioButtonKB.Location = new System.Drawing.Point(8, 23);
            this.radioButtonKB.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonKB.Name = "radioButtonKB";
            this.radioButtonKB.Size = new System.Drawing.Size(91, 21);
            this.radioButtonKB.TabIndex = 1;
            this.radioButtonKB.TabStop = true;
            this.radioButtonKB.Text = "KeyBoard";
            this.radioButtonKB.UseVisualStyleBackColor = true;
            this.radioButtonKB.CheckedChanged += new System.EventHandler(this.radioButtonKB_CheckedChanged);
            // 
            // radioButtonTM
            // 
            this.radioButtonTM.AutoSize = true;
            this.radioButtonTM.Location = new System.Drawing.Point(8, 52);
            this.radioButtonTM.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonTM.Name = "radioButtonTM";
            this.radioButtonTM.Size = new System.Drawing.Size(117, 21);
            this.radioButtonTM.TabIndex = 2;
            this.radioButtonTM.Text = "Time Machine";
            this.radioButtonTM.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(152, 20);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 22);
            this.textBox1.TabIndex = 4;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(143, 50);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(141, 24);
            this.comboBox1.TabIndex = 3;
            // 
            // btnEndRace
            // 
            this.btnEndRace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEndRace.Location = new System.Drawing.Point(1192, 491);
            this.btnEndRace.Margin = new System.Windows.Forms.Padding(4);
            this.btnEndRace.Name = "btnEndRace";
            this.btnEndRace.Size = new System.Drawing.Size(100, 28);
            this.btnEndRace.TabIndex = 7;
            this.btnEndRace.Text = "End Race";
            this.btnEndRace.UseVisualStyleBackColor = true;
            this.btnEndRace.Click += new System.EventHandler(this.StopRace);
            // 
            // btnStartRace
            // 
            this.btnStartRace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartRace.Location = new System.Drawing.Point(936, 492);
            this.btnStartRace.Margin = new System.Windows.Forms.Padding(4);
            this.btnStartRace.Name = "btnStartRace";
            this.btnStartRace.Size = new System.Drawing.Size(100, 28);
            this.btnStartRace.TabIndex = 6;
            this.btnStartRace.Text = "Start Race";
            this.btnStartRace.UseVisualStyleBackColor = true;
            this.btnStartRace.Click += new System.EventHandler(this.StartRace);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1020, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(225, 36);
            this.label2.TabIndex = 5;
            this.label2.Text = "Timing Method";
            // 
            // dataGridTiming
            // 
            this.dataGridTiming.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridTiming.Location = new System.Drawing.Point(4, 4);
            this.dataGridTiming.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridTiming.Name = "dataGridTiming";
            this.dataGridTiming.Size = new System.Drawing.Size(923, 518);
            this.dataGridTiming.TabIndex = 0;
            this.dataGridTiming.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.TimingTableCellChanging);
            this.dataGridTiming.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.TimingTableCellChange);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.mainMenueToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1345, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(53, 24);
            this.toolStripMenuItem1.Text = "Help";
            // 
            // mainMenueToolStripMenuItem
            // 
            this.mainMenueToolStripMenuItem.Name = "mainMenueToolStripMenuItem";
            this.mainMenueToolStripMenuItem.Size = new System.Drawing.Size(95, 24);
            this.mainMenueToolStripMenuItem.Text = "Main Menu";
            this.mainMenueToolStripMenuItem.Click += new System.EventHandler(this.mainMenueToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1345, 597);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainWindow";
            this.Text = "Race";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabRunners.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRunners)).EndInit();
            this.tabResults.ResumeLayout(false);
            this.tabResults.PerformLayout();
            this.tabTiming.ResumeLayout(false);
            this.tabTiming.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTiming)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabRunners;
        private System.Windows.Forms.TabPage tabResults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.TabPage tabTiming;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.BindingSource dataSet1BindingSource;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridRunners;
        private System.Windows.Forms.Button btnAddRunner;
        private System.Windows.Forms.ToolStripMenuItem mainMenueToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridTiming;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.RadioButton radioButtonTM;
        private System.Windows.Forms.RadioButton radioButtonKB;
        private System.Windows.Forms.Button btnEndRace;
        private System.Windows.Forms.Button btnStartRace;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel resultTable;
    }
}

