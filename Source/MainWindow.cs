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
    public partial class MainWindow : Form
    {
        private Form parentWindow;
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(Form parent)
        {
            parentWindow = parent;
            InitializeComponent();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            var runners = new DataTable();
            var results = new DataTable();
            var timing = new DataTable();
            this.dataGridRunners.AllowUserToAddRows = true;
            using (var conn = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "select FirstName, LastName, BibId, age from RaceRunner rr join Runners r where rr.RunnerID=r.RunnerID;";
                    var daRunners = new SQLiteDataAdapter(cmd);
                    daRunners.Fill(runners);

                    cmd.CommandText = "select  ( SELECT COUNT(*) + 1  FROM  RaceResults where time< r.time) as Position, BibID, CAST(Time as varchar(10)) as Time from RaceResults r order by Time";
                    var daTimer = new SQLiteDataAdapter(cmd);
                    daTimer.Fill(timing);
                }
                conn.Close();
            }

            dataGridRunners.AllowUserToAddRows = false;
            //dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dgv_CellEndEdit);
            //dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dgv_CellValidating);
            dataGridRunners.DataSource = runners;
            dataGridTiming.DataSource = timing;
        }
        
        private void btnAddRunner_Click(object sender, EventArgs e)
        {
            var user = new NewUserWindow();
            user.Show();
        }

        private void mainMenueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(parentWindow!=null)
            { 
                parentWindow.Show();
                this.Dispose();
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (parentWindow != null)
            {
                this.Dispose();
                parentWindow.Close();
            }
        }

    }
}
