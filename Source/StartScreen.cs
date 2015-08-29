using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class StartScreen : Form
    {
        public StartScreen()
        {
            InitializeComponent();
            comboBox1.Items.Add("Race 1");
            comboBox1.Items.Add("Race 2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var mainWindow = new MainWindow(this);
            this.Hide();
            mainWindow.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var newRaceScreen = new NewRaceWindow();
            newRaceScreen.Show();
        }
    }
}
