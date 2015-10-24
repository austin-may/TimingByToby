using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimingForToby
{
    public partial class NewFilterBuilder : Form
    {
        //Default min for ages
        public int AgeMinimum = 1;
        //Default max for ages
        public int AgeMaximum = 100;
        //name of Filter
        public string FilterName; 
        public NewFilterBuilder()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Sets the potential min to the value of the trackbar
            int minAge = trackBar1.Value;
            //Checks that the potential new min is less than the max
            if (minAge > AgeMaximum)
            {
                MessageBox.Show("The min age must be less than the max age!");
            }    
                //On a pass, set the potential min to the age minimum
            else
            {
                txtMinAge.Text = minAge.ToString();
                AgeMinimum = minAge;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSetMax_Click(object sender, EventArgs e)
        {
            //Sets the potential max to the value of the trackbar
            int maxAge = trackBar1.Value;
            //Checks that the potential new max is more than the min
            if (maxAge < AgeMinimum)
            {
                MessageBox.Show("The max age must be greater than the min age!");
            }
            //On a pass, set the potential max to the age maximum
            else
            {
                txtMaxAge.Text = maxAge.ToString();
                AgeMaximum = maxAge;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Close the form
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Set the filter name 
            FilterName = textBox1.Text;
        }
    }
}
