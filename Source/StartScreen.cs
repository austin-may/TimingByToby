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
            var data = new RaceData(this, comboBox1.SelectedItem.ToString(), "Data Source=MyDatabase.sqlite;Version=3;");
            var mainWindow = new MainWindow(data);
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
            using (var conn = new SQLiteConnection(CommonSQL.SQLiteConnection))
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

        private async void AddUsersToRace(String filename)
        {
            int curRow = 1;
            try {
                Cursor.Current = Cursors.WaitCursor;
                lblProgress.Text = "Scanning Excel document...";
                var excelApp = new Excel.Application();
                var workbook = excelApp.Workbooks.Open(filename);
                if(workbook.Worksheets.Count>0)
                {
                    var worksheet=workbook.Worksheets[1] as Excel.Worksheet;//first page
                    var range = worksheet.UsedRange as Excel.Range;
                    int rowCount = range.Rows.Count;
                    int colCount = range.Columns.Count;
                    string race=this.comboBox1.SelectedItem.ToString();
                    if(rowCount>1 && colCount>4)//minimum valid input
                    {
                        //curRo-1 becouse the first row is headers
                        var FirstNames = new string[rowCount-1];
                        var LastNames = new string[rowCount - 1];
                        var DOBs = new DateTime[rowCount - 1];
                        var BibIDs = new string[rowCount - 1];
                        var Orginizations = new string[rowCount - 1];
                        var Teams = new string[rowCount - 1];
                        Dictionary<string, int> dictionary = new Dictionary<string, int>();
                        bool duplicateBibsFound = false;
                        for (curRow = 2; curRow <= rowCount; curRow++)
                        {        
                            FirstNames[curRow-2] = range.Cells[curRow, 1].Value2 as string;
                            LastNames[curRow-2] = range.Cells[curRow, 2].Value2 as string;
                            DOBs[curRow-2] = DateTime.FromOADate(range.Cells[curRow, 3].Value2);
                            string BibID = range.Cells[curRow, 4].Value2.ToString() ?? "";
                            if (!dictionary.ContainsKey(BibID))
                            {
                                dictionary.Add(BibID, 1);
                                BibIDs[curRow - 2] = range.Cells[curRow, 4].Value2.ToString() ?? "";
                            }
                            else
                            {
                              int duplicateBib = 2;
                              dictionary.TryGetValue(BibID, out duplicateBib);
                              duplicateBibsFound = true;
                            }
                            Teams[curRow-2] = range.Cells[curRow, 5].Value2 as string ?? "";
                            Orginizations[curRow-2] = range.Cells[curRow, 6] as string ?? "";
                        }
                        if (duplicateBibsFound == true) { MessageBox.Show("Some duplicate bibs were found. They can be edited in home page.");}
                        workbook.Close();
                        //CommonSQL.AddRunners(FirstNames, LastNames, DOBs, BibIDs, Teams, Orginizations, race, CommonSQL.SQLiteConnection);
                        var progress = new Progress<ProgressReport>();
                        lblProgress.Text = "Importing...";
                        //updates the progress bar
                        progress.ProgressChanged += (o, report) =>
                        {
                            progressBar1.Value = report.PercentComplete;
                            progressBar1.Update();
                        };
                        //waits for runners to be inserted asynchrously
                        await CommonSQL.ProcessRunners(FirstNames,LastNames, DOBs, BibIDs, Teams, Orginizations, race, CommonSQL.SQLiteConnection, progress);
                    }
                    lblProgress.Text = "Import complete for " + race;
                    CommonSQL.BackupDB();
                }
                else
                {
                    MessageBox.Show("No Valid Data to Import!");
                }
            }
            catch(Exception e) {
                MessageBox.Show("Error occured at row: " + curRow + e.Message);
            }
            finally
            {
                this.UseWaitCursor = false;
            }

        }

      }
}
