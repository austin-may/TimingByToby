﻿namespace TimingForToby
{
    partial class StartScreen
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
            this.components = new System.ComponentModel.Container();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnNewRace = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnRace = new System.Windows.Forms.Button();
            this.myDatabaseDataSet = new TimingForToby.MyDatabaseDataSet();
            this.myDatabaseDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.runnersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.runnersTableAdapter = new TimingForToby.MyDatabaseDataSetTableAdapters.RunnersTableAdapter();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.importProgressPanel = new System.Windows.Forms.Panel();
            this.ExportDatabase = new System.Windows.Forms.Button();
            this.RestoreDatabase = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.myDatabaseDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDatabaseDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.runnersBindingSource)).BeginInit();
            this.importProgressPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(43, 16);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(160, 24);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_Click);
            // 
            // btnNewRace
            // 
            this.btnNewRace.Location = new System.Drawing.Point(560, 128);
            this.btnNewRace.Margin = new System.Windows.Forms.Padding(4);
            this.btnNewRace.Name = "btnNewRace";
            this.btnNewRace.Size = new System.Drawing.Size(181, 28);
            this.btnNewRace.TabIndex = 1;
            this.btnNewRace.Text = "New Race";
            this.btnNewRace.UseVisualStyleBackColor = true;
            this.btnNewRace.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(560, 181);
            this.btnImport.Margin = new System.Windows.Forms.Padding(4);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(181, 28);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "Import Runners";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnRace
            // 
            this.btnRace.Location = new System.Drawing.Point(560, 236);
            this.btnRace.Margin = new System.Windows.Forms.Padding(4);
            this.btnRace.Name = "btnRace";
            this.btnRace.Size = new System.Drawing.Size(181, 28);
            this.btnRace.TabIndex = 3;
            this.btnRace.Text = "Race!";
            this.btnRace.UseVisualStyleBackColor = true;
            this.btnRace.Click += new System.EventHandler(this.button3_Click);
            // 
            // myDatabaseDataSet
            // 
            this.myDatabaseDataSet.DataSetName = "MyDatabaseDataSet";
            this.myDatabaseDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // myDatabaseDataSetBindingSource
            // 
            this.myDatabaseDataSetBindingSource.DataSource = this.myDatabaseDataSet;
            this.myDatabaseDataSetBindingSource.Position = 0;
            // 
            // runnersBindingSource
            // 
            this.runnersBindingSource.DataMember = "Runners";
            this.runnersBindingSource.DataSource = this.myDatabaseDataSet;
            // 
            // runnersTableAdapter
            // 
            this.runnersTableAdapter.ClearBeforeFill = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 30);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(420, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(3, 11);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(46, 17);
            this.lblProgress.TabIndex = 5;
            this.lblProgress.Text = "label1";
            // 
            // importProgressPanel
            // 
            this.importProgressPanel.Controls.Add(this.lblProgress);
            this.importProgressPanel.Controls.Add(this.progressBar1);
            this.importProgressPanel.Location = new System.Drawing.Point(4, 305);
            this.importProgressPanel.Margin = new System.Windows.Forms.Padding(4);
            this.importProgressPanel.Name = "importProgressPanel";
            this.importProgressPanel.Size = new System.Drawing.Size(428, 70);
            this.importProgressPanel.TabIndex = 6;
            this.importProgressPanel.Visible = false;
            // 
            // ExportDatabase
            // 
            this.ExportDatabase.Cursor = System.Windows.Forms.Cursors.Default;
            this.ExportDatabase.Location = new System.Drawing.Point(560, 287);
            this.ExportDatabase.Margin = new System.Windows.Forms.Padding(4);
            this.ExportDatabase.Name = "ExportDatabase";
            this.ExportDatabase.Size = new System.Drawing.Size(181, 28);
            this.ExportDatabase.TabIndex = 7;
            this.ExportDatabase.Text = "Export Database";
            this.ExportDatabase.UseVisualStyleBackColor = true;
            this.ExportDatabase.Click += new System.EventHandler(this.ExportDatabase_Click);
            // 
            // RestoreDatabase
            // 
            this.RestoreDatabase.Cursor = System.Windows.Forms.Cursors.Default;
            this.RestoreDatabase.Location = new System.Drawing.Point(563, 335);
            this.RestoreDatabase.Margin = new System.Windows.Forms.Padding(4);
            this.RestoreDatabase.Name = "RestoreDatabase";
            this.RestoreDatabase.Size = new System.Drawing.Size(181, 28);
            this.RestoreDatabase.TabIndex = 8;
            this.RestoreDatabase.Text = "Restore Database";
            this.RestoreDatabase.UseVisualStyleBackColor = true;
            this.RestoreDatabase.Click += new System.EventHandler(this.RestoreDatabase_Click);
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 388);
            this.Controls.Add(this.RestoreDatabase);
            this.Controls.Add(this.ExportDatabase);
            this.Controls.Add(this.importProgressPanel);
            this.Controls.Add(this.btnRace);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnNewRace);
            this.Controls.Add(this.comboBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StartScreen";
            this.Text = "Timing By Toby";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StartScreen_FormClosing);
            this.Load += new System.EventHandler(this.StartScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.myDatabaseDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDatabaseDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.runnersBindingSource)).EndInit();
            this.importProgressPanel.ResumeLayout(false);
            this.importProgressPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnNewRace;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnRace;
        private System.Windows.Forms.BindingSource myDatabaseDataSetBindingSource;
        private TimingForToby.MyDatabaseDataSet myDatabaseDataSet;
        private System.Windows.Forms.BindingSource runnersBindingSource;
        private TimingForToby.MyDatabaseDataSetTableAdapters.RunnersTableAdapter runnersTableAdapter;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Panel importProgressPanel;
        private System.Windows.Forms.Button ExportDatabase;
        private System.Windows.Forms.Button RestoreDatabase;
    }
}