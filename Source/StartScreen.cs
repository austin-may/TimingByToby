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
using System.Collections;
using Excel=Microsoft.Office.Interop.Excel;
using System.Web;

namespace TimingForToby
{
    public partial class StartScreen : Form
    {
        public StartScreen()
        {
            InitializeComponent();
        }
        //on "Race!" button click. create race data, hide this window, pop up new "Main" window
        private void BtnRace_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem==null || comboBox1.SelectedItem.ToString()=="")
                MessageBox.Show("No Race Selected");
            else {
                var data = new RaceData(this, comboBox1.SelectedItem.ToString(), CommonSQL.SQLiteConnectionString);
                var mainWindow = new MainWindow(data);
                this.Hide();
                mainWindow.Show(); 
            }
        }
        //on "New Race" button click.  prompt user for new race
        private void BtnNewRace_Click(object sender, EventArgs e)
        {
            var newRaceScreen = new NewRaceWindow();
            newRaceScreen.Show();
            
        }

        //populate the race comboBox
        public void LoadComboBox()
        {
            //empty out the comboBox
            comboBox1.Items.Clear();
            //load the names of the races from file
            var races = new DataTable();
            CommonSQL.BuildIfNotExsistDB();
            using (var conn = new SQLiteConnection(CommonSQL.SQLiteConnectionString))
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
        //populate combobox on load
        private void StartScreen_Load(object sender, EventArgs e)
        {
            LoadComboBox();
        }
        //onclick combobox. load combobox
        private void ComboBox1_Click(object sender, EventArgs e)
        {
            LoadComboBox();
        }
        //on "Import Runners" button click. prompt user to navigate to excel doc and import runner data from that file
        private void BtnImport_Click(object sender, EventArgs e)
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
                    importProgressPanel.Visible = false;
                }
            }
            Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
        }
        //take excel file and import runner data from that file
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
            int ageRow = -1;
            int curRow = 1;
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
                        case "bib #":
                            bibIDRow = i;
                            break;
                        case "firstname":
                        case "first name":
                            firstNameRow = i;
                            break;
                        case "lastname":
                        case "last name":
                            lastNameRow = i;
                            break;
                        case "dob":
                            dobRow = i;
                            break;
                        case "gender":
                            genderRow = i;
                            break;
                        case "organization":
                            orgRow = i;
                            break;
                        case "team":
                            teamRow = i;
                            break;
                        case "shirt":
                            shirtRow = i;
                            break;
                        case "age":
                            ageRow = i;
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
                    if (comboBox1.SelectedItem == null){
                        MessageBox.Show("No Race Selected");
                        return;
                    }
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
                        string errorMessage="Full Error log at "+LogFile.PATH+"\r\n";
                        int errrorThreashold = errorMessage.Length;
                        if(firstNameRow<1||lastNameRow<1||bibIDRow<1||(dobRow<1&&ageRow<1))
                        {
                            MessageBox.Show("Some columns not found. Need to see: FirstName, LastName, BibId, and either DOB or age");
                            return;
                        }
                        for (curRow = 2; curRow <= rowCount; curRow++)
                        {        
                            FirstNames[curRow-2] = range.Cells[curRow, firstNameRow].Value2 as string;
                            LastNames[curRow-2] = range.Cells[curRow, lastNameRow].Value2 as string;
                            if (dobRow > 0 && range.Cells[curRow, dobRow].Value2!=null)
                                DOBs[curRow - 2] = DateTime.FromOADate(range.Cells[curRow, dobRow].Value2);
                            else if(ageRow>0){
                                int age=0;
                                bool succesful=false;
                                var celVal=range.Cells[curRow, ageRow].Value2;
                                if(celVal!=null)
                                    succesful = Int32.TryParse(celVal.ToString(), out age);
                                if(succesful)
                                    DOBs[curRow-2]=DateTime.Now.AddYears(-age);
                                else{
                                    DOBs[curRow-2]=DateTime.Now;
                                    string date = string.Format("{0:G}", DateTime.Now);
                                    string dobError = "Runner " + FirstNames[curRow - 2] + " " + LastNames[curRow - 2] + " could not read age!";
                                    LogFile.WriteToErrorLog(dobError+" "+date+"\r\n");
                                    errorMessage += dobError + "\r\n";
                                }
                            }
                            //check to make sure that the dob is in the past, allow them to be entered but warn user
                            if (DOBs[curRow - 2] > DateTime.Now)
                            {
                                string date = string.Format("{0:G}", DateTime.Now);
                                string birthError = "Runner " + FirstNames[curRow - 2] + " " + LastNames[curRow - 2] + " has not been born yet!";
                                LogFile.WriteToErrorLog(birthError+" "+date+"\r\n");
                                errorMessage += birthError + "\r\n";
                            }
                            //might be male, might be female, just take first letter
                            string temp="N";
                            if(genderRow>0){
                                temp = range.Cells[curRow, genderRow].Value2 as string;
                            }
                            Genders[curRow - 2] = (temp!=null && temp.Length>0)? temp.ToUpper().ToCharArray()[0]: 'N';
                            string BibID = range.Cells[curRow, bibIDRow].Value2.ToString() ?? "";
                            if (!dictionary.ContainsKey(BibID))
                            {
                                dictionary.Add(BibID, 1);
                                BibIDs[curRow - 2] = range.Cells[curRow, bibIDRow].Value2.ToString() ?? "";
                            }
                            else
                            {
                                string date = string.Format("{0:G}", DateTime.Now);
                                string dateError = "Duplicate bib #" + BibID + " found. ";
                                LogFile.WriteToErrorLog(dateError + date + "\r\n");
                                errorMessage += dateError + "\r\n";
                            }
                            if (teamRow > 0)
                                Teams[curRow-2] = range.Cells[curRow, teamRow].Value2 as string ?? "";
                            if(orgRow>0)
                                Orginizations[curRow-2] = range.Cells[curRow, orgRow] as string ?? "";
                        }
                        if (errorMessage.Length > errrorThreashold)
                        { 
                            MessageBox.Show(errorMessage);
                        }
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
                        await CommonSQL.ProcessRunners(FirstNames,LastNames, DOBs, Genders, BibIDs, Teams, Orginizations, race, CommonSQL.SQLiteConnectionString, progress);
                        //re-enable user interaction
                        LockGUI(false);
                    }
                    lblProgress.Text = "Import complete for " + race;
                    progressBar1.Value = 0;
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
                importProgressPanel.Visible = false;
            }
        }
        //disable buttons on gui to prevent clicks 
        private void LockGUI(bool lockGui)
        {
            comboBox1.Enabled = !lockGui;
            btnRace.Enabled = !lockGui;
            btnImport.Enabled = !lockGui;
            ExportDatabase.Enabled = !lockGui;
            RestoreDatabase.Enabled = !lockGui;
        }
        //call on screen close. create backup
        private void StartScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Backup the database on close
            TobyTimer.TimeStampedBackup(true);
        }

        //prompts for a save dialog
        //will copy the original database
        //store its data into the exported database that user can point anywhere to on disk
        private void ExportDatabase_Click(object sender, EventArgs e)
        {
             SaveFileDialog saveFileDialog = new SaveFileDialog();
             saveFileDialog.Filter = "SQLite|*.sqlite";//type is sqlite
             saveFileDialog.FileName = "TheExportedDatabase";
             string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
             saveFileDialog.InitialDirectory = desktopPath + "\\TimingByToby\\Source";
             try
             {
                 if (saveFileDialog.ShowDialog() == DialogResult.OK)
                 {
                     CopyDB("MyDatabase.sqlite", saveFileDialog.FileName, 0);
                 } 
             }
             catch (FileNotFoundException exception)
             {
                 MessageBox.Show(exception.Message);
             }
             
        }
        //on "Import Database" button click. prompt user to navigate to db backup and replaces current db with that file
        private void RestoreDatabase_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "SQLite|*.sqlite";//type is sqlite
            //openFileDialog.FileName = "TheExportedDatabase";
            //path to the appdata folder
            string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //prompts the appdata folder to open on default
            openFileDialog.InitialDirectory = AppDataPath + "\\TimingForToby";
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //will show up in bin folder with the name "TheRestoredDatabase"
                    CopyDB(openFileDialog.FileName, "MyDatabase.sqlite", 1);
                }
            }
            catch (FileNotFoundException exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }

        //the int parameter signifies what messagebox will be shown based on whether it was an 
        //export of the database or restore
        private static void CopyDB(string ExistingFileName, string DestinationFileName, int actionInvoked)
        {
            //copies an existing file to a new file
            File.Copy(ExistingFileName, DestinationFileName, true);
            if (actionInvoked == 0) MessageBox.Show("Database exported!");
            else if (actionInvoked == 1) MessageBox.Show("Database restored!");
        }
    }      
}
