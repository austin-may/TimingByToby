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
using System.IO.Ports;
using System.Timers;
using Excel = Microsoft.Office.Interop.Excel;

namespace TimingForToby
{
    public partial class MainWindow : Form, ITimerListener
    {
        private TimingDevice TimingDevice;
        private RaceData raceData;
        private List<Filter> filters = new List<Filter>();
        private bool TimingCellBeingEdited = false;
        private System.Timers.Timer ClockRefreshTimer = new System.Timers.Timer(500);
        public MainWindow()
        {
            InitializeComponent();            
        }
        public MainWindow(RaceData data)
        {
            raceData=data;
            InitializeComponent();
            //becouse the default is to start with the Keyboard Timer
            //set clock to inital default of 0
            SetClock(new TimeSpan(0,0,0));
            panelClock.Visible = true;
        }
        //builds and populates the results table
        private void buildResults(List<Filter> filters)
        {
            resultTable.Controls.Clear();
            int filterCount = filters.Count;
            resultTable.ColumnCount = filterCount;
            for (int i = 0; i < filterCount;i++)
            {
                resultTable.Controls.Add(new Label { Text = filters[i].Name, Anchor = AnchorStyles.None }, i, 0);
                filters[i].LoadDataTable();
                resultTable.Controls.Add(filters[i].GetDataTable(), i, 1);
            }
        }
        //builds and populates the timing table
        private void TimingTableLoad()
        {
            if (InvokeRequired)
            {
                MethodInvoker method = new MethodInvoker(TimingTableLoad);
                Invoke(method);
                return;
            }
            //if the cells in the timing table are not currently being edited
            if (!TimingCellBeingEdited)
            {
            using (var conn = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand())
                    {
                        var timing = new DataTable();
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = "select  ( SELECT COUNT(*) + 1  FROM  RaceResults where time< r.time and RaceID=@RaceID) as Position, BibID, CAST(Time as varchar(10)) as Time from RaceResults r where r.RaceID=@RaceID order by Time";
                        cmd.Parameters.AddWithValue("@RaceID", raceData.RaceID);
                        var daTimer = new SQLiteDataAdapter(cmd);
                        daTimer.Fill(timing);
                        dataGridTiming.DataSource = timing;
                        dataGridTiming.AllowUserToAddRows = false;
                            dataGridTiming.Columns[0].ReadOnly = true;
                    }
                }
                catch (Exception e) { MessageBox.Show(this, e.Message); }
                finally
                {
                    conn.Close();
                }
            }
        }
        }
        private void BuildRunnersTable(){
            var runners = new DataTable();
            this.dataGridRunners.AllowUserToAddRows = true;
            using (var conn = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "select FirstName, LastName, BibId, CAST(DOB as varchar(10)) as DOB from RaceRunner rr join Runners r where rr.RunnerID=r.RunnerID and rr.RaceID=@RaceID;";
                        cmd.Parameters.AddWithValue("@RaceID", raceData.RaceID);
                        var daRunners = new SQLiteDataAdapter(cmd);
                        daRunners.Fill(runners);
                    }

                }
                catch (Exception sqlError)
                {
                    MessageBox.Show(sqlError.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            dataGridRunners.AllowUserToAddRows = false;
            dataGridRunners.DataSource = runners;
        }
        private void BuildFilters()
        {
            //look in the Filters folder (should be in the same directory, grab all xml files and list them in the checkbox.
            try
            {
                checkedListBox1.Items.Clear();
                foreach (string file in Directory.GetFiles(CommonSQL.filterFolder, "*.XML"))
                {
                    int index = file.LastIndexOf('\\');
                    //the length is -5 (1 for general overflow, 4 for ".xml"
                    checkedListBox1.Items.Add(file.Substring(index + 1, file.Length - index - 5));
                }
            }
            catch (Exception filterError)
            {
                MessageBox.Show(filterError.Message);
            }
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            BuildFilters();
            BuildRunnersTable();
            TimingTableLoad();            
        }

        public void reload()
        {
            MainWindow_Load(null, null);
        }
        //adding user button
        private void btnAddRunner_Click(object sender, EventArgs e)
        {
            var user = new NewUserWindow(raceData, this);
            user.Show();
        }

        private void mainMenueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (raceData.StartWindow != null)
            {
                raceData.StartWindow.Show();
                this.Dispose();
            }
        }
        //temp... this is to ensure that the parent window (StartScreen) is also closed
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (raceData.StartWindow != null)
            {
                this.Dispose();
                raceData.StartWindow.Close();
            }
        }
        //creates and assigns keybord timer when the kebord radio button is selected
        private void radioButtonKB_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonKB.Checked)
            {
                this.SetTimingDevice(new KeybordTimer(this));
            }
        }
        //what happens when the start race button is pressed
        private void StartRace(object sender, EventArgs e)
        {
            //ensure we have a timing devices, one is set by default
            if (TimingDevice == null)
            {
                RadioButton rb = gbTimerOptions.Controls.OfType<RadioButton>().FirstOrDefault(r=>r.Checked);
                if (rb != null) {
                switch(rb.Name)
                {
                    case "radioButtonKB":
                        this.SetTimingDevice(new KeybordTimer(this));
                        break;
                    case "radioButtonTM":
                            if (comPortComboBox.SelectedItem != null)
                                ValidateTimeMachine();
                        break;
                    default:
                        TimingDevice = new KeybordTimer(this);
                        break;
                }
            }
            }
            //note! this is different from else, we want this to run so long as not null (should be based on above)
            if (TimingDevice != null)
            {
                TimingDevice.StartRace(GetClockTime());
                ClockEditable(false);
        }
            //the clock should run for timers (except for the time machine becouse it is an external clock and we cant pull this data...)
            if(!radioButtonTM.Checked)
            {
                //this should refresh the clock that the user sees
                ClockRefreshTimer.Elapsed += new ElapsedEventHandler(delegate { SetClock(TimingDevice.GetCurrentTime()); });
                ClockRefreshTimer.Enabled = true;
            }
            //disable Start buton, enable stop
            btnStartRace.Enabled = false;
            btnEndRace.Enabled = true;
        }
        private void StopRace(object sender, EventArgs e)
        {
            if(TimingDevice!=null)
                TimingDevice.StopRace();
            ClockEditable(true);
            ClockRefreshTimer.Enabled=false;

            //disable Start buton, enable stop
            btnStartRace.Enabled = true;
            btnEndRace.Enabled = false;
        }
        //this handles the filters.  finds the related file and builds filter
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            filters.Clear();
            if(e.NewValue==CheckState.Checked)
            {
                foreach (string filter in checkedListBox1.CheckedItems)
                {
                    filters.AddRange(Filter.BuildFromXML(raceData, CommonSQL.filterFolder + "\\" + filter + ".xml", 350, (int)(resultTable.Height * .9)));
                }
                filters.AddRange(Filter.BuildFromXML(raceData, CommonSQL.filterFolder + "\\" + checkedListBox1.Items[e.Index].ToString() + ".xml", 350, (int)(resultTable.Height * .9)));
            }
            else//item was checked and is now being unchecked
            {
                foreach (string filter in checkedListBox1.CheckedItems)
                {
                    if(filter!=checkedListBox1.Items[e.Index])
                        filters.AddRange(Filter.BuildFromXML(raceData, CommonSQL.filterFolder + "\\" + filter + ".xml", 350, (int)(resultTable.Height * .9)));
                }
            }
            //build filters
            buildResults(filters);
        }
        //highlight duplicated bibs
        private void HighlightTimingErrors()
        {
            //find bad data in the table
            var test = CommonSQL.FindBadBibs(raceData.RaceID);
            //look through visable cells
            if (test.Count > 0)
            {
                for (int rowIndex = 0; rowIndex <= dataGridTiming.RowCount-1; rowIndex++)
                {
                    var cells = dataGridTiming.Rows[rowIndex].Cells;
                    foreach (DataGridViewCell cell in cells)
                    {
                        if (test.Contains(cell.Value.ToString()))
                        {
                            cell.Style.BackColor = Color.Red;
                        }
                    }
                }
            }

        }
        //runs after a cell has been updated in the timing table
        private void TimingTableCellChange(object sender, DataGridViewCellEventArgs e)
        {
            TimingTableLoad();
            buildResults(filters);
            HighlightTimingErrors();
        }
        //validateds and updates changes in the timing table
        private void TimingTableCellChanging(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var oldValue = dataGridTiming[e.ColumnIndex, e.RowIndex].Value.ToString();
            var newValue = e.FormattedValue.ToString();
            TimingCellBeingEdited = false;
            if (oldValue != newValue)
            {
               if (e.ColumnIndex == 1)//if we are changing the bib
               { CommonSQL.UpdateTimingBib(raceData.RaceID, oldValue, dataGridTiming[2, e.RowIndex].Value.ToString(), newValue); }
               else if (e.ColumnIndex == 2)//if we are changing the time
               { CommonSQL.UpdateTimingTime(raceData.RaceID, dataGridTiming[1, e.RowIndex].Value.ToString(), oldValue, newValue); }
               else
               {
                 MessageBox.Show("That value can not be edited");
               }            
            }
        }


        delegate void OnTimeCallBack();
        //to be called everytime an time is added (timing Device action)
        public void OnTime()
        {
            //cross threading non-sense
            if (dataGridTiming.InvokeRequired)
            {
                try
                {
                    OnTimeCallBack d = new OnTimeCallBack(OnTime);
                    this.Invoke(d);
                }
                catch (ObjectDisposedException ode) { }//saw this when app was closed without ending race first
                catch (Exception e) { MessageBox.Show(e.Message); }
            }
            TimingTableLoad();
            HighlightTimingErrors();
            dataGridTiming.FirstDisplayedScrollingRowIndex = dataGridTiming.RowCount - 1;
        }
        //takes timing device and sets it to be able to be used
        private void SetTimingDevice(TimingDevice timeDevice)
        {
            //set TimingDevice
            this.TimingDevice = timeDevice;
            //set RaceID so the device knows what race to update in DB
            TimingDevice.SetRaceID(raceData.RaceID);
            //listen for change to update Table
            TimingDevice.addListener(this);
            //if we are using a timer with internal clock, report the time
            if (!(TimingDevice is TimeMachineTimer))
            {
                panelClock.Visible = true;
        }
            else
                panelClock.Visible = false;
        }

        private void ComDropDown(object sender, EventArgs e)
        {
            //get Com ports and populate select box
            comPortComboBox.Items.Clear();
            comPortComboBox.Items.AddRange(SerialPort.GetPortNames());
        }
        //the time machine can and should only be used if the com port is also selected
        private void radioButtonTM_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTM.Checked)
            {
                ValidateTimeMachine();
                //the start and end race buttons are basicaly meaningless....
                btnStartRace.Enabled = false;
                btnEndRace.Enabled = false;
            }
        }
        private void ValidateTimeMachine()
        {
            if (comPortComboBox.SelectedItem == null || comPortComboBox.SelectedItem.ToString() == "")
            {
                radioButtonTM.Checked = false;
                MessageBox.Show("No COM Port Selected");
            }
            else
            {
                this.SetTimingDevice(new TimeMachineTimer(comPortComboBox.SelectedItem.ToString()));
            }
        }
        //there is a cell in the timeing table that is being changed, toggle flag
        private void CellBeingEdited(object sender, EventArgs e)
        {
            TimingCellBeingEdited = true;
        }
        //sets clock editability
        public void ClockEditable(bool edit)
        {
            textBoxHours.Enabled = edit;
            textBoxMin.Enabled = edit;
            textBoxSeconds.Enabled = edit;
        }

        delegate void ClockCallBack(TimeSpan ts);
        public void SetClock(TimeSpan ts)
        {
            //cross threading non-sense
            if (textBoxHours.InvokeRequired || textBoxMin.InvokeRequired || textBoxSeconds.InvokeRequired)
            {
                try
                {
                    ClockCallBack d = new ClockCallBack(SetClock);
                    this.Invoke(d, new object[] { ts });
                }
                catch (ObjectDisposedException ode) { }//saw this when app was closed without ending race first
                catch (Exception e) { MessageBox.Show(e.Message); }
            }
            else
            {
                textBoxHours.Text = ts.Hours.ToString();
                textBoxMin.Text = ts.Minutes.ToString();
                textBoxSeconds.Text = ts.Seconds.ToString();
            }
        }
        //returns value of clock as timespan, 0 if improper value
        public TimeSpan GetClockTime()
        {
            string errorString="";
            int hh,mm,ss;
            if (!int.TryParse(textBoxHours.Text, out hh))
                errorString += "Hours not proper value\n";
            if (!int.TryParse(textBoxMin.Text, out mm))
                errorString += "Hours not proper value\n";
            if (!int.TryParse(textBoxSeconds.Text, out ss))
                errorString += "Hours not proper value\n";
            if(errorString!="")
            {
                MessageBox.Show(errorString);
                return TimeSpan.Zero;
            }
            return new TimeSpan(hh, mm, ss);
        }

        private void ExportResults(object sender, EventArgs e)
        {
            if (filters.Count == 0)
            {
                MessageBox.Show("No Results to export");
            }
            else
            {
                //save dialog
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel File|*.xlsx";//type is excel
                saveFileDialog.FileName = "Results_" + raceData.RaceName;//default name of file
                DialogResult result = saveFileDialog.ShowDialog(); // Show the dialog.
                var xlApp = new Microsoft.Office.Interop.Excel.Application();

                try {
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;
                    xlWorkBook = xlApp.Workbooks.Add(misValue);
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    //itterate over all filters
                    for (int f=0; f<filters.Count;f++)
                    {
                        Filter filter = filters[f];
                        xlWorkSheet.Cells[1, f*3+2] = filter.Name;
                        var table = filter.GetDataTable();
                        for (int i = 0; i < table.ColumnCount; i++)
                        {
                            //print out column name
                            xlWorkSheet.Cells[2, f * 3 + i + 1] = table.Columns[i].Name;
                        }
                        //print out each row of content
                        for (int r = 0; r < table.Rows.Count; r++)
                        {
                            for (int c = 0; c < table.ColumnCount; c++)
                            {
                                xlWorkSheet.Cells[r + 3, f * 3 + c + 1] = table[c, r].Value.ToString();
                            }
                        }
                    }
                    xlWorkBook.SaveAs(saveFileDialog.FileName);
                    xlWorkBook.Close();
                    releaseObject(xlWorkSheet);
                    releaseObject(xlWorkBook);
                    releaseObject(xlApp);
                    MessageBox.Show("Excel file created");
                }
                catch (Exception er)
                {
                    //for now, only show the error message if in Debug
#if DEBUG
                        MessageBox.Show(er.Message+"\n");
#else
                        MessageBox.Show("An error occured and the Excel file could not be saved");
#endif
                }
            }
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //Creates a filterbuilder window
            NewFilterBuilder FilterWin = new NewFilterBuilder();
            //Checks that the "Create Filter" button was pressed on the filterbuilder window and then adds that filter to the list
            if (FilterWin.ShowDialog(this) == DialogResult.OK)
            {
                checkedListBox1.Items.Add(FilterWin.FilterName);
            }
        }
    }
}
