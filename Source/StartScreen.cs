﻿using System;
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
            int firstNameRow = -1;
            int lastNameRow = -1;
            int bibIDRow = -1;
            int dobRow = -1;
            int genderRow = -1;
            int teamRow = -1;
            int orgRow = -1;
            int shirtRow = -1;
            int curRow = 1;
            LogFile logErrors = new LogFile();
            try {
                Cursor.Current = Cursors.WaitCursor;
                lblProgress.Text = "Scanning Excel document...";
                importProgressPanel.Visible = true;
                var excelApp = new Excel.Application();
                var workbook = excelApp.Workbooks.Open(filename);
                var worksheet=workbook.Worksheets[1] as Excel.Worksheet;//first page
                Excel.Range range = worksheet.UsedRange as Excel.Range;
                //parse the first line to get the positions of each column
                int i = 1;
                while (range.Cells[1, i].Value2 != null && range.Cells[1, i].Value2+"" != "")
                {
                    string title = range.Cells[1, i].Value2 + "";
                    switch (title.ToLower()) {
                        case "bibid":
                            bibIDRow = i;
                            break;
                        case "firstname":
                            firstNameRow = i;
                            break;
                        case "lastname":
                            lastNameRow = i;
                            break;
                        case "dob":
                            dobRow = i;
                            break;
                        case "gender":
                            genderRow = i;
                            break;
                        case "orginization":
                            orgRow = i;
                            break;
                        case "team":
                            teamRow = i;
                            break;
                        case "shirt":
                            shirtRow = i;
                            break;
                        default:
                            break;
                    }
                    i++;
                }
                if(workbook.Worksheets.Count>0)
                {
                    int rowCount = range.Rows.Count;
                    int colCount = range.Columns.Count;
                    string race=this.comboBox1.SelectedItem.ToString();
                    if(rowCount>1 && colCount>4)//minimum valid input
                    {
                        //curRo-1 becouse the first row is headers
                        var FirstNames = new string[rowCount-1];
                        var LastNames = new string[rowCount - 1];
                        var DOBs = new DateTime[rowCount - 1];
                        var Genders = new char[rowCount - 1];
                        var BibIDs = new string[rowCount - 1];
                        var Orginizations = new string[rowCount - 1];
                        var Teams = new string[rowCount - 1];
                        Dictionary<string, int> dictionary = new Dictionary<string, int>();
                        bool duplicateBibsFound = false;
                        for (curRow = 2; curRow <= rowCount; curRow++)
                        {        
                            FirstNames[curRow-2] = range.Cells[curRow, firstNameRow].Value2 as string;
                            LastNames[curRow-2] = range.Cells[curRow, lastNameRow].Value2 as string;
                            DOBs[curRow - 2] = DateTime.FromOADate(range.Cells[curRow, dobRow].Value2);
                            //might be male, might be female, just take first letter
                            Genders[curRow - 2] = (range.Cells[curRow, genderRow].Value2 as string).ToUpper().ToCharArray()[0];
                            string BibID = range.Cells[curRow, bibIDRow].Value2.ToString() ?? "";
                            if (!dictionary.ContainsKey(BibID))
                            {
                                dictionary.Add(BibID, 1);
                                BibIDs[curRow - 2] = range.Cells[curRow, bibIDRow].Value2.ToString() ?? "";
                            }
                            else
                            {
                                duplicateBibsFound = true;
                                string date = DateTime.Now.ToString(" M-d-yyyy (hh:mm)");
                                logErrors.WriteToErrorLog("Duplicate bib #" + BibID + " found." + date + "\r\n");
                            }
                            Teams[curRow-2] = range.Cells[curRow, teamRow].Value2 as string ?? "";
                            Orginizations[curRow-2] = range.Cells[curRow, orgRow] as string ?? "";
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
                        //keep user from messing things up
                        LockGUI(true);
                        //waits for runners to be inserted asynchrously
                        await CommonSQL.ProcessRunners(FirstNames,LastNames, DOBs, Genders, BibIDs, Teams, Orginizations, race, CommonSQL.SQLiteConnection, progress);
                        //re-enable user interaction
                        LockGUI(false);
                    }
                    lblProgress.Text = "Import complete for " + race;
                    importProgressPanel.Visible = false;
                    progressBar1.Value = 0;
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

        private void LockGUI(bool lockGui)
        {
            comboBox1.Enabled = !lockGui;
            btnRace.Enabled = !lockGui;
            btnImport.Enabled = !lockGui;
        }



      }
}
