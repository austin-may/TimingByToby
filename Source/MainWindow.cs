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

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                        cmd.CommandText = "select  ( SELECT COUNT(*) + 1  FROM  RaceResults where time< r.time) as Position, BibID, CAST(Time as varchar(10)) as Time from RaceResults r where r.RaceID=@RaceID order by Time";
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
        private void MainWindow_Load(object sender, EventArgs e)
        {
            var runners = new DataTable();
            var results = new DataTable();
            this.dataGridRunners.AllowUserToAddRows = true;
            //look in the Filters folder (should be in the same directory, grab all xml files and list them in the checkbox.
            try {
                checkedListBox1.Items.Clear();
                foreach (string file in Directory.GetFiles(CommonSQL.filterFolder, "*.XML"))
                {
                    int index=file.LastIndexOf('\\');
                    //the length is -5 (1 for general overflow, 4 for ".xml"
                    checkedListBox1.Items.Add(file.Substring(index+1,file.Length-index-5));
                }
            }
            catch (Exception filterError) {
                MessageBox.Show(filterError.Message);
            }
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
                TimingTableLoad();
            }
            dataGridRunners.AllowUserToAddRows = false;
            dataGridRunners.DataSource = runners;
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

        private void MainWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            DialogResult results = MessageBox.Show("Key Selected");   
        }
        private void StartRace(object sender, EventArgs e)
        {
            if (TimingDevice == null) {
                RadioButton rb = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r=>r.Checked);
                switch(rb.Name)
                {
                    case "radioButtonKB":
                        this.SetTimingDevice(new KeybordTimer(this));
                        break;
                    case "radioButtonTM":
                        if(comPortComboBox.SelectedItem!=null)
                            MessageBox.Show("Create Time Machine Timer");
                        break;
                    default:
                        TimingDevice = new KeybordTimer(this);
                        break;
                }
            }
            //note! this is different frrom else, we want this to run so long as not null (should be based on above)
            if (TimingDevice != null)
            {
                TimingDevice.StartRace(GetClockTime());
                ClockEditable(false);
            }
            KeybordTimer keybord = TimingDevice as KeybordTimer;
            if(keybord!=null)
            {
                //this should refresh the clock that the user sees
                ClockRefreshTimer.Elapsed += new ElapsedEventHandler(delegate { SetClock(keybord.GetCurrentTime()); });
                ClockRefreshTimer.Enabled = true;
            }
        }
        private void StopRace(object sender, EventArgs e)
        {
            if(TimingDevice!=null)
                TimingDevice.StopRace();
            ClockEditable(true);
            ClockRefreshTimer.Enabled=false;
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
        private void HilightTimingErrors()
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
            HilightTimingErrors();
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

        //to be called everytime an time is added (timing Device action)
        public void OnTime()
        {
            TimingTableLoad();
            dataGridTiming.FirstDisplayedScrollingRowIndex = dataGridTiming.RowCount - 1;
            HilightTimingErrors();
            //dataGridTiming.FirstDisplayedScrollingRowIndex = dataGridTiming.RowCount - 1;
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
            //if we are using the keyboard, report the time
            var keybord=timeDevice as KeybordTimer;
            if (keybord!=null)
            {
                panelClock.Visible = true;
        }
            else
                panelClock.Visible = false;
        }

        private void SelectCom(object sender, EventArgs e)
        {

        }

        private void ComDropDown(object sender, EventArgs e)
        {
            PopulateCom();
        }
        //get Com ports and populate select box
        private void PopulateCom()
        {
            comPortComboBox.Items.Clear();
            comPortComboBox.Items.AddRange(SerialPort.GetPortNames());
        }
        //the time machine can and should only be used if the com port is also selected
        private void ValidateTimeMachine(object sender, EventArgs e)
        {
            if (radioButtonTM.Checked)
            {
                if (comPortComboBox.SelectedItem==null || comPortComboBox.SelectedItem.ToString() == "")
                {
                    radioButtonTM.Checked = false;
                    MessageBox.Show("No COM Port Selected");
                }
                else
                {
                    this.SetTimingDevice(new TimeMachineTimer(comPortComboBox.SelectedItem.ToString()));
                }
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
                ClockCallBack d = new ClockCallBack(SetClock);
                this.Invoke(d, new object[] { ts });
            }
            textBoxHours.Text = ts.Hours.ToString();
            textBoxMin.Text = ts.Minutes.ToString();
            textBoxSeconds.Text = ts.Seconds.ToString();
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

    }
}
