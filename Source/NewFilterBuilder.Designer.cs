namespace TimingForToby
{
    partial class NewFilterBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewFilterBuilder));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.btnCreateFilter = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.btnSetMin = new System.Windows.Forms.Button();
            this.btnSetMax = new System.Windows.Forms.Button();
            this.txtMinAge = new System.Windows.Forms.TextBox();
            this.txtMaxAge = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAdditionalAge = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(172, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filter Name:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(245, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(13, 13);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(61, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Gender";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(12, 50);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(45, 17);
            this.checkBox2.TabIndex = 3;
            this.checkBox2.Text = "Age";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // btnCreateFilter
            // 
            this.btnCreateFilter.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCreateFilter.Location = new System.Drawing.Point(313, 223);
            this.btnCreateFilter.Name = "btnCreateFilter";
            this.btnCreateFilter.Size = new System.Drawing.Size(170, 23);
            this.btnCreateFilter.TabIndex = 4;
            this.btnCreateFilter.Text = "Create Filter";
            this.btnCreateFilter.UseVisualStyleBackColor = true;
            this.btnCreateFilter.Click += new System.EventHandler(this.button1_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(20, 99);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(419, 45);
            this.trackBar1.TabIndex = 6;
            this.trackBar1.Value = 1;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // btnSetMin
            // 
            this.btnSetMin.Location = new System.Drawing.Point(114, 165);
            this.btnSetMin.Name = "btnSetMin";
            this.btnSetMin.Size = new System.Drawing.Size(88, 23);
            this.btnSetMin.TabIndex = 7;
            this.btnSetMin.Text = "Set Min Age";
            this.btnSetMin.UseVisualStyleBackColor = true;
            this.btnSetMin.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnSetMax
            // 
            this.btnSetMax.Location = new System.Drawing.Point(338, 168);
            this.btnSetMax.Name = "btnSetMax";
            this.btnSetMax.Size = new System.Drawing.Size(88, 23);
            this.btnSetMax.TabIndex = 8;
            this.btnSetMax.Text = "Set Max Age";
            this.btnSetMax.UseVisualStyleBackColor = true;
            this.btnSetMax.Click += new System.EventHandler(this.btnSetMax_Click);
            // 
            // txtMinAge
            // 
            this.txtMinAge.Location = new System.Drawing.Point(55, 166);
            this.txtMinAge.Name = "txtMinAge";
            this.txtMinAge.Size = new System.Drawing.Size(39, 20);
            this.txtMinAge.TabIndex = 9;
            this.txtMinAge.TextChanged += new System.EventHandler(this.MinChanged);
            // 
            // txtMaxAge
            // 
            this.txtMaxAge.Location = new System.Drawing.Point(281, 168);
            this.txtMaxAge.Name = "txtMaxAge";
            this.txtMaxAge.Size = new System.Drawing.Size(39, 20);
            this.txtMaxAge.TabIndex = 10;
            this.txtMaxAge.TextChanged += new System.EventHandler(this.MaxChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Min:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(245, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Max:";
            // 
            // btnAdditionalAge
            // 
            this.btnAdditionalAge.Location = new System.Drawing.Point(91, 223);
            this.btnAdditionalAge.Name = "btnAdditionalAge";
            this.btnAdditionalAge.Size = new System.Drawing.Size(144, 23);
            this.btnAdditionalAge.TabIndex = 13;
            this.btnAdditionalAge.Text = "Add Age Filter";
            this.btnAdditionalAge.UseVisualStyleBackColor = true;
            this.btnAdditionalAge.Click += new System.EventHandler(this.btnAdditionalAge_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(463, 49);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 95);
            this.listBox1.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(493, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Age Filters";
            // 
            // labelValue
            // 
            this.labelValue.AutoSize = true;
            this.labelValue.Location = new System.Drawing.Point(216, 83);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(33, 13);
            this.labelValue.TabIndex = 16;
            this.labelValue.Text = "value";
            // 
            // NewFilterBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 258);
            this.Controls.Add(this.labelValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnAdditionalAge);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMaxAge);
            this.Controls.Add(this.txtMinAge);
            this.Controls.Add(this.btnSetMax);
            this.Controls.Add(this.btnSetMin);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.btnCreateFilter);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewFilterBuilder";
            this.Text = "Filter Builder";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button btnCreateFilter;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button btnSetMin;
        private System.Windows.Forms.Button btnSetMax;
        private System.Windows.Forms.TextBox txtMinAge;
        private System.Windows.Forms.TextBox txtMaxAge;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdditionalAge;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelValue;
    }
}