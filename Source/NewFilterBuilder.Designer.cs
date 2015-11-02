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
               ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
               this.SuspendLayout();
               // 
               // label1
               // 
               this.label1.AutoSize = true;
               this.label1.Location = new System.Drawing.Point(258, 20);
               this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
               this.label1.Name = "label1";
               this.label1.Size = new System.Drawing.Size(94, 20);
               this.label1.TabIndex = 0;
               this.label1.Text = "Filter Name:";
               this.label1.Click += new System.EventHandler(this.label1_Click);
               // 
               // textBox1
               // 
               this.textBox1.Location = new System.Drawing.Point(368, 15);
               this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
               this.textBox1.Name = "textBox1";
               this.textBox1.Size = new System.Drawing.Size(148, 26);
               this.textBox1.TabIndex = 1;
               this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
               // 
               // checkBox1
               // 
               this.checkBox1.AutoSize = true;
               this.checkBox1.Location = new System.Drawing.Point(20, 20);
               this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
               this.checkBox1.Name = "checkBox1";
               this.checkBox1.Size = new System.Drawing.Size(89, 24);
               this.checkBox1.TabIndex = 2;
               this.checkBox1.Text = "Gender";
               this.checkBox1.UseVisualStyleBackColor = true;
               // 
               // checkBox2
               // 
               this.checkBox2.AutoSize = true;
               this.checkBox2.Location = new System.Drawing.Point(18, 77);
               this.checkBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
               this.checkBox2.Name = "checkBox2";
               this.checkBox2.Size = new System.Drawing.Size(64, 24);
               this.checkBox2.TabIndex = 3;
               this.checkBox2.Text = "Age";
               this.checkBox2.UseVisualStyleBackColor = true;
               // 
               // btnCreateFilter
               // 
               this.btnCreateFilter.DialogResult = System.Windows.Forms.DialogResult.OK;
               this.btnCreateFilter.Location = new System.Drawing.Point(225, 346);
               this.btnCreateFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
               this.btnCreateFilter.Name = "btnCreateFilter";
               this.btnCreateFilter.Size = new System.Drawing.Size(255, 35);
               this.btnCreateFilter.TabIndex = 4;
               this.btnCreateFilter.Text = "Create Filter";
               this.btnCreateFilter.UseVisualStyleBackColor = true;
               this.btnCreateFilter.Click += new System.EventHandler(this.button1_Click);
               // 
               // trackBar1
               // 
               this.trackBar1.Location = new System.Drawing.Point(34, 152);
               this.trackBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
               this.trackBar1.Maximum = 100;
               this.trackBar1.Minimum = 1;
               this.trackBar1.Name = "trackBar1";
               this.trackBar1.Size = new System.Drawing.Size(628, 69);
               this.trackBar1.TabIndex = 6;
               this.trackBar1.Value = 1;
               this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
               // 
               // btnSetMin
               // 
               this.btnSetMin.Location = new System.Drawing.Point(171, 254);
               this.btnSetMin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
               this.btnSetMin.Name = "btnSetMin";
               this.btnSetMin.Size = new System.Drawing.Size(132, 35);
               this.btnSetMin.TabIndex = 7;
               this.btnSetMin.Text = "Set Min Age";
               this.btnSetMin.UseVisualStyleBackColor = true;
               this.btnSetMin.Click += new System.EventHandler(this.button2_Click);
               // 
               // btnSetMax
               // 
               this.btnSetMax.Location = new System.Drawing.Point(507, 258);
               this.btnSetMax.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
               this.btnSetMax.Name = "btnSetMax";
               this.btnSetMax.Size = new System.Drawing.Size(132, 35);
               this.btnSetMax.TabIndex = 8;
               this.btnSetMax.Text = "Set Max Age";
               this.btnSetMax.UseVisualStyleBackColor = true;
               this.btnSetMax.Click += new System.EventHandler(this.btnSetMax_Click);
               // 
               // txtMinAge
               // 
               this.txtMinAge.Location = new System.Drawing.Point(82, 255);
               this.txtMinAge.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
               this.txtMinAge.Name = "txtMinAge";
               this.txtMinAge.Size = new System.Drawing.Size(56, 26);
               this.txtMinAge.TabIndex = 9;
               // 
               // txtMaxAge
               // 
               this.txtMaxAge.Location = new System.Drawing.Point(422, 258);
               this.txtMaxAge.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
               this.txtMaxAge.Name = "txtMaxAge";
               this.txtMaxAge.Size = new System.Drawing.Size(56, 26);
               this.txtMaxAge.TabIndex = 10;
               // 
               // label2
               // 
               this.label2.AutoSize = true;
               this.label2.Location = new System.Drawing.Point(30, 266);
               this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
               this.label2.Name = "label2";
               this.label2.Size = new System.Drawing.Size(38, 20);
               this.label2.TabIndex = 11;
               this.label2.Text = "Min:";
               // 
               // label3
               // 
               this.label3.AutoSize = true;
               this.label3.Location = new System.Drawing.Point(368, 266);
               this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
               this.label3.Name = "label3";
               this.label3.Size = new System.Drawing.Size(42, 20);
               this.label3.TabIndex = 12;
               this.label3.Text = "Max:";
               // 
               // NewFilterBuilder
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.ClientSize = new System.Drawing.Size(681, 397);
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
               this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
    }
}