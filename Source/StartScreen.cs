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
using System.IO;
using Excel=Microsoft.Office.Interop.Excel;

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
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Excel File|*.xlsx";
            int size = -1;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    if (file.Contains(".xlsx"))
                        AddUsersToRace(file);
                }
                catch (IOException)
                {
                }
            }
            Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
        }

        private void AddUsersToRace(String filename)
        {
            int curRow = 1;
            try {
                var excelApp = new Excel.Application();
                var workbook = excelApp.Workbooks.Open(filename);
                if(workbook.Worksheets.Count>0)
                {
                    var worksheet=workbook.Worksheets[1] as Excel.Worksheet;//first page
                    var range = worksheet.UsedRange as Excel.Range;
                    int rowCount = range.Rows.Count;
                    int colCount = range.Columns.Count;
                    if(rowCount>1 && colCount>3)//minimum valid input
                    using (var conn = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
                    {
                        conn.Open();
                        using (var cmd = new SQLiteCommand())
                        {
                            string FirstName, LastName, Team, Orginization, BibID;
                            DateTime DOB;
                            for (curRow = 2; curRow <= rowCount; curRow++)
                            {
                                FirstName = range.Cells[curRow, 1].Value2 as string;
                                LastName = range.Cells[curRow, 2].Value2 as string;
                                DOB = DateTime.FromOADate(range.Cells[curRow, 3].Value2);
                                BibID = range.Cells[curRow, 4].Value2 as string ?? "";
                                Team = range.Cells[curRow, 5].Value2 as string ?? "";
                                Orginization = range.Cells[curRow, 6] as string ?? "";
                                                       
                                cmd.Connection = conn;
                                cmd.CommandText = "Insert Into Runners(FirstName, LastName, DOB) Values(@FirstName, @LastName, @DOB);";//'"+DOB.ToString("MM/dd/yyyy")+"');";
                                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                                cmd.Parameters.AddWithValue("@LastName", LastName);
                                cmd.Parameters.AddWithValue("@DOB", DOB);
                                cmd.ExecuteNonQuery();
                                //cmd.Parameters.Add(new SQLiteParameter("@FirstName", SqlDbType.Text) { Value = FirstName });
                                //cmd.Parameters.Add(new SQLiteParameter("@LastName", SqlDbType.Text) { Value = LastName });
                                //cmd.Parameters.Add(new SQLiteParameter("@DOB", DbType.Date) { Value =  });
                                //cmd.Parameters.Add(DOB.ToString("MM/DD/YYYY"));
                                cmd.CommandText="Insert into RaceRunner(RunnerID, RaceID, BibID, Orginization, Team) Values("+
                                    "(select RunnerID from Runners where FirstName=@FirstName AND LastName=@LastName Limit 1),"+
                                    "(select RaceID from Race where Name=@Race Limit 1),"+
                                    "@BibID,@Orginization,@Team);";


                                cmd.Parameters.AddWithValue("@Race", this.comboBox1.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@BibID", BibID);
                                cmd.Parameters.AddWithValue("@Team", Team);
                                cmd.Parameters.AddWithValue("@Orginization", Orginization);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        conn.Close();
                    }
                }
            }
            catch(Exception e) {
                MessageBox.Show("Error occured at row: " + curRow + e.Message);
            }
        }
    }
}
