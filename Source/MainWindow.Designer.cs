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
            this.btnCreateFilter = new System.Windows.Forms.Button();
            this.resultTable = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.tabTiming = new System.Windows.Forms.TabPage();
            this.panelClock = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSeconds = new System.Windows.Forms.TextBox();
            this.textBoxMin = new System.Windows.Forms.TextBox();
            this.textBoxHours = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gbTimerOptions = new System.Windows.Forms.GroupBox();
            this.radioButtonKB = new System.Windows.Forms.RadioButton();
            this.radioButtonTM = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comPortComboBox = new System.Windows.Forms.ComboBox();
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
            this.panelClock.SuspendLayout();
            this.gbTimerOptions.SuspendLayout();
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
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(985, 456);
            this.tabControl1.TabIndex = 0;
            // 
            // tabRunners
            // 
            this.tabRunners.AutoScroll = true;
            this.tabRunners.Controls.Add(this.btnAddRunner);
            this.tabRunners.Controls.Add(this.dataGridRunners);
            this.tabRunners.Location = new System.Drawing.Point(4, 22);
            this.tabRunners.Name = "tabRunners";
            this.tabRunners.Padding = new System.Windows.Forms.Padding(4);
            this.tabRunners.Size = new System.Drawing.Size(977, 430);
            this.tabRunners.TabIndex = 0;
            this.tabRunners.Text = "Runners";
            this.tabRunners.UseVisualStyleBackColor = true;
            // 
            // btnAddRunner
            // 
            this.btnAddRunner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRunner.Location = new System.Drawing.Point(862, 21);
            this.btnAddRunner.Name = "btnAddRunner";
            this.btnAddRunner.Size = new System.Drawing.Size(75, 23);
            this.btnAddRunner.TabIndex = 1;
            this.btnAddRunner.Text = "Add Runner";
            this.btnAddRunner.UseVisualStyleBackColor = true;
            this.btnAddRunner.Click += new System.EventHandler(this.btnAddRunner_Click);
            // 
            // dataGridRunners
            // 
            this.dataGridRunners.AllowUserToAddRows = false;
            this.dataGridRunners.AllowUserToOrderColumns = true;
            this.dataGridRunners.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridRunners.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridRunners.Location = new System.Drawing.Point(19, 6);
            this.dataGridRunners.Name = "dataGridRunners";
            this.dataGridRunners.ReadOnly = true;
            this.dataGridRunners.Size = new System.Drawing.Size(802, 418);
            this.dataGridRunners.TabIndex = 0;
            this.dataGridRunners.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RunnerTableDoubleClick);
            this.dataGridRunners.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.DataGridViewRunnerRowDel);
            // 
            // tabResults
            // 
            this.tabResults.Controls.Add(this.btnCreateFilter);
            this.tabResults.Controls.Add(this.resultTable);
            this.tabResults.Controls.Add(this.button1);
            this.tabResults.Controls.Add(this.label1);
            this.tabResults.Controls.Add(this.checkedListBox1);
            this.tabResults.Location = new System.Drawing.Point(4, 22);
            this.tabResults.Name = "tabResults";
            this.tabResults.Padding = new System.Windows.Forms.Padding(4);
            this.tabResults.Size = new System.Drawing.Size(977, 430);
            this.tabResults.TabIndex = 1;
            this.tabResults.Text = "Results";
            this.tabResults.UseVisualStyleBackColor = true;
            // 
            // btnCreateFilter
            // 
            this.btnCreateFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateFilter.Location = new System.Drawing.Point(818, 147);
            this.btnCreateFilter.Name = "btnCreateFilter";
            this.btnCreateFilter.Size = new System.Drawing.Size(88, 53);
            this.btnCreateFilter.TabIndex = 6;
            this.btnCreateFilter.Text = "Create Custom Filter";
            this.btnCreateFilter.UseVisualStyleBackColor = true;
            this.btnCreateFilter.Click += new System.EventHandler(this.button2_Click);
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
            this.resultTable.Location = new System.Drawing.Point(7, 7);
            this.resultTable.Name = "resultTable";
            this.resultTable.RowCount = 2;
            this.resultTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.resultTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95F));
            this.resultTable.Size = new System.Drawing.Size(779, 417);
            this.resultTable.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(896, 401);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Export";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ExportResults);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(841, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 25);
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
            this.checkedListBox1.Location = new System.Drawing.Point(803, 31);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(135, 79);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // tabTiming
            // 
            this.tabTiming.Controls.Add(this.panelClock);
            this.tabTiming.Controls.Add(this.gbTimerOptions);
            this.tabTiming.Controls.Add(this.btnEndRace);
            this.tabTiming.Controls.Add(this.btnStartRace);
            this.tabTiming.Controls.Add(this.label2);
            this.tabTiming.Controls.Add(this.dataGridTiming);
            this.tabTiming.Location = new System.Drawing.Point(4, 22);
            this.tabTiming.Name = "tabTiming";
            this.tabTiming.Size = new System.Drawing.Size(977, 430);
            this.tabTiming.TabIndex = 2;
            this.tabTiming.Text = "Timing";
            this.tabTiming.UseVisualStyleBackColor = true;
            // 
            // panelClock
            // 
            this.panelClock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelClock.Controls.Add(this.label5);
            this.panelClock.Controls.Add(this.textBoxSeconds);
            this.panelClock.Controls.Add(this.textBoxMin);
            this.panelClock.Controls.Add(this.textBoxHours);
            this.panelClock.Controls.Add(this.label4);
            this.panelClock.Controls.Add(this.label3);
            this.panelClock.Location = new System.Drawing.Point(741, 175);
            this.panelClock.Name = "panelClock";
            this.panelClock.Size = new System.Drawing.Size(200, 100);
            this.panelClock.TabIndex = 9;
            this.panelClock.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(78, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Timer";
            // 
            // textBoxSeconds
            // 
            this.textBoxSeconds.Location = new System.Drawing.Point(118, 61);
            this.textBoxSeconds.MaxLength = 2;
            this.textBoxSeconds.Name = "textBoxSeconds";
            this.textBoxSeconds.Size = new System.Drawing.Size(28, 20);
            this.textBoxSeconds.TabIndex = 11;
            // 
            // textBoxMin
            // 
            this.textBoxMin.Location = new System.Drawing.Point(81, 61);
            this.textBoxMin.MaxLength = 2;
            this.textBoxMin.Name = "textBoxMin";
            this.textBoxMin.Size = new System.Drawing.Size(30, 20);
            this.textBoxMin.TabIndex = 10;
            // 
            // textBoxHours
            // 
            this.textBoxHours.Location = new System.Drawing.Point(47, 61);
            this.textBoxHours.MaxLength = 2;
            this.textBoxHours.Name = "textBoxHours";
            this.textBoxHours.Size = new System.Drawing.Size(26, 20);
            this.textBoxHours.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(110, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = ":";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = ":";
            // 
            // gbTimerOptions
            // 
            this.gbTimerOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTimerOptions.Controls.Add(this.radioButtonKB);
            this.gbTimerOptions.Controls.Add(this.radioButtonTM);
            this.gbTimerOptions.Controls.Add(this.textBox1);
            this.gbTimerOptions.Controls.Add(this.comPortComboBox);
            this.gbTimerOptions.Location = new System.Drawing.Point(728, 68);
            this.gbTimerOptions.Margin = new System.Windows.Forms.Padding(4);
            this.gbTimerOptions.Name = "gbTimerOptions";
            this.gbTimerOptions.Padding = new System.Windows.Forms.Padding(4);
            this.gbTimerOptions.Size = new System.Drawing.Size(225, 100);
            this.gbTimerOptions.TabIndex = 8;
            this.gbTimerOptions.TabStop = false;
            this.gbTimerOptions.Text = "Timing Method";
            // 
            // radioButtonKB
            // 
            this.radioButtonKB.AutoSize = true;
            this.radioButtonKB.Checked = true;
            this.radioButtonKB.Location = new System.Drawing.Point(6, 19);
            this.radioButtonKB.Name = "radioButtonKB";
            this.radioButtonKB.Size = new System.Drawing.Size(71, 17);
            this.radioButtonKB.TabIndex = 1;
            this.radioButtonKB.TabStop = true;
            this.radioButtonKB.Text = "KeyBoard";
            this.radioButtonKB.UseVisualStyleBackColor = true;
            this.radioButtonKB.CheckedChanged += new System.EventHandler(this.radioButtonKB_CheckedChanged);
            // 
            // radioButtonTM
            // 
            this.radioButtonTM.AutoSize = true;
            this.radioButtonTM.Location = new System.Drawing.Point(6, 42);
            this.radioButtonTM.Name = "radioButtonTM";
            this.radioButtonTM.Size = new System.Drawing.Size(92, 17);
            this.radioButtonTM.TabIndex = 2;
            this.radioButtonTM.Text = "Time Machine";
            this.radioButtonTM.UseVisualStyleBackColor = true;
            this.radioButtonTM.CheckedChanged += new System.EventHandler(this.radioButtonTM_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(114, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 4;
            // 
            // comPortComboBox
            // 
            this.comPortComboBox.FormattingEnabled = true;
            this.comPortComboBox.Location = new System.Drawing.Point(114, 43);
            this.comPortComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.comPortComboBox.Name = "comPortComboBox";
            this.comPortComboBox.Size = new System.Drawing.Size(100, 21);
            this.comPortComboBox.TabIndex = 3;
            // 
            // btnEndRace
            // 
            this.btnEndRace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEndRace.Enabled = false;
            this.btnEndRace.Location = new System.Drawing.Point(873, 400);
            this.btnEndRace.Margin = new System.Windows.Forms.Padding(4);
            this.btnEndRace.Name = "btnEndRace";
            this.btnEndRace.Size = new System.Drawing.Size(75, 23);
            this.btnEndRace.TabIndex = 7;
            this.btnEndRace.Text = "End Race";
            this.btnEndRace.UseVisualStyleBackColor = true;
            this.btnEndRace.Click += new System.EventHandler(this.StopRace);
            // 
            // btnStartRace
            // 
            this.btnStartRace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartRace.Location = new System.Drawing.Point(702, 400);
            this.btnStartRace.Name = "btnStartRace";
            this.btnStartRace.Size = new System.Drawing.Size(75, 23);
            this.btnStartRace.TabIndex = 6;
            this.btnStartRace.Text = "Start Race";
            this.btnStartRace.UseVisualStyleBackColor = true;
            this.btnStartRace.Click += new System.EventHandler(this.StartRace);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(765, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "Timing Method";
            // 
            // dataGridTiming
            // 
            this.dataGridTiming.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridTiming.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridTiming.Location = new System.Drawing.Point(3, 3);
            this.dataGridTiming.Name = "dataGridTiming";
            this.dataGridTiming.Size = new System.Drawing.Size(692, 421);
            this.dataGridTiming.TabIndex = 0;
            this.dataGridTiming.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.TimingTableCellChanging);
            this.dataGridTiming.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.TimingTableCellChange);
            this.dataGridTiming.CurrentCellDirtyStateChanged += new System.EventHandler(this.CellBeingEdited);
            this.dataGridTiming.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.DataGridViewTimingRowDel);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.mainMenueToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1009, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItem1.Text = "Help";
            // 
            // mainMenueToolStripMenuItem
            // 
            this.mainMenueToolStripMenuItem.Name = "mainMenueToolStripMenuItem";
            this.mainMenueToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.mainMenueToolStripMenuItem.Text = "Main Menu";
            this.mainMenueToolStripMenuItem.Click += new System.EventHandler(this.mainMenueToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 485);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
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
            this.panelClock.ResumeLayout(false);
            this.panelClock.PerformLayout();
            this.gbTimerOptions.ResumeLayout(false);
            this.gbTimerOptions.PerformLayout();
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
        private System.Windows.Forms.ComboBox comPortComboBox;
        private System.Windows.Forms.RadioButton radioButtonTM;
        private System.Windows.Forms.RadioButton radioButtonKB;
        private System.Windows.Forms.Button btnEndRace;
        private System.Windows.Forms.Button btnStartRace;
        private System.Windows.Forms.GroupBox gbTimerOptions;
        private System.Windows.Forms.TableLayoutPanel resultTable;
        private System.Windows.Forms.Panel panelClock;
        private System.Windows.Forms.TextBox textBoxHours;
        private System.Windows.Forms.TextBox textBoxMin;
        private System.Windows.Forms.TextBox textBoxSeconds;
        private System.Windows.Forms.Button btnCreateFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

