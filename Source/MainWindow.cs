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

namespace TimingForToby
{
    public partial class MainWindow : Form, ITimerListener
    {
        private TimingDevice TimingDevice;
        private RaceData raceData;
        private List<Filter> filters = new List<Filter>();
        public MainWindow()
        {
            InitializeComponent();            
        }
        public MainWindow(RaceData data)
        {
            raceData=data;
            InitializeComponent();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
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
        private void TimingTableLoad()
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
                    }
                }
                catch (Exception e) { MessageBox.Show(this, e.Message); }
                finally
                {
                    conn.Close();
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

                         
                        //Testing results
                        cmd.CommandText = "select BibID, CAST(Time as varchar(10)) as 'Time', ROWID as 'Position' from RaceResults";
                        var daResults = new SQLiteDataAdapter(cmd);
                        daResults.Fill(results);
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

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (raceData.StartWindow != null)
            {
                this.Dispose();
                raceData.StartWindow.Close();
            }
        }

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
                        TimingDevice = new KeybordTimer();
                        break;
                    default:
                        TimingDevice = new KeybordTimer(this);
                        break;
                }
            }
            //note! this is different frrom else, we want this to run so long as not null (should be based on above)
            if (TimingDevice != null)
                TimingDevice.StartRace();
        }
        private void StopRace(object sender, EventArgs e)
        {
            if(TimingDevice!=null)
                TimingDevice.StopRace();
        }

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
        private void HilightTimingErrors()
        {
            //find bad data in the table
            var test = CommonSQL.FindBadBibs(raceData.RaceID);
            //look through visable cells
            if (test.Count > 0)
            {
                var vivibleRowsCount = dataGridTiming.DisplayedRowCount(true);
                var firstDisplayedRowIndex = dataGridTiming.FirstDisplayedCell.RowIndex;
                var lastvibileRowIndex = (firstDisplayedRowIndex + vivibleRowsCount) - 1;
                for (int rowIndex = firstDisplayedRowIndex; rowIndex <= lastvibileRowIndex; rowIndex++)
                {
                    var cells = dataGridTiming.Rows[rowIndex].Cells;
                    foreach (DataGridViewCell cell in cells)
                    {
                        if (cell.Displayed && test.Contains(cell.Value.ToString()))
                        {
                            cell.Style.BackColor = Color.Red;
                        }
                    }
                }
            }

        }

        private void TimingTableCellChange(object sender, DataGridViewCellEventArgs e)
        {
            TimingTableLoad();
            buildResults(filters);
            HilightTimingErrors();
        }

        private void TimingTableCellChanging(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var oldValue = dataGridTiming[e.ColumnIndex, e.RowIndex].Value.ToString();
            var newValue = e.FormattedValue.ToString();
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


        public void OnTime()
        {
            TimingTableLoad();
            HilightTimingErrors();
            dataGridTiming.FirstDisplayedScrollingRowIndex = dataGridTiming.RowCount - 1;
        }

        private void SetTimingDevice(TimingDevice timeDevice)
        {
            //set TimingDevice
            this.TimingDevice = timeDevice;
            //set RaceID so the device knows what race to update in DB
            TimingDevice.SetRaceID(raceData.RaceID);
            //listen for change to update Table
            TimingDevice.addListener(this);
        }



    }
}
