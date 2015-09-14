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
using System.Data.OleDb;

namespace TimingForToby
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
        public void loadComboBox()
        {
            //empty out the comboBox
            comboBox1.Items.Clear();
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

        private void comboBox1_Click(object sender, EventArgs e)
        {
            loadComboBox();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileExplorer = new OpenFileDialog();
            DialogResult dialogResult = openFileExplorer.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = openFileExplorer.FileName;
            }
            
        }

        private void displayContentBtn_Click(object sender, EventArgs e)
        {
            string filePath = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + textBox1.Text + ";Extended Properties=\"Excel 8.0;HDR=Yes;\";";
            OleDbConnection connection = new OleDbConnection(filePath);
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter("SELECT * FROM [" + textBox2.Text + "$]", connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }
    }
}
