using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsFormsApplication1
{
    public partial class StartScreen : Form
    {
        public StartScreen()
        {
            InitializeComponent();
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

        //populate the race comboBox
        private void loadComboBox()
        {
            //load the names of the races from file
            var races = new DataTable();
            using (var conn = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "select Name from Race;";
                    SQLiteDataReader r = cmd.ExecuteReader();

                    var daRaces = new SQLiteDataAdapter(cmd);
                    while (r.Read())
                    {
                        this.comboBox1.Items.Add(r[0]);
                    }
                }
                conn.Close();
            }
            //if there is data, load the first value
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void StartScreen_Load(object sender, EventArgs e)
        {
            loadComboBox();
        }
    }
}
