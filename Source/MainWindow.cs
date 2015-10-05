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
    public partial class MainWindow : Form
    {
        private TimingDevice timingDevice;
        private RaceData raceData;
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
                resultTable.Controls.Add(filters[i].GetDataTable(), i, 1);
            }
            /*inital test filters
            this.resultTable.ColumnCount = 5;
            string overall = "select BibID, CAST(Time as varchar(10)) as 'Time', ROWID as 'Position' from RaceResults";
            var filter1 = new Filter("Overall", overall, 350, (int)(resultTable.Height * .9));
            var filter2 = new Filter("Filter1", overall, 350, (int)(resultTable.Height * .9));
            var filter3 = new Filter("Filter2", overall, 350, (int)(resultTable.Height * .9));
            var filter4 = new Filter("Filter3", overall, 350, (int)(resultTable.Height * .9));

            resultTable.Controls.Add(new Label { Text = filter1.Name, Anchor = AnchorStyles.None }, 0, 0);
            resultTable.Controls.Add(new Label { Text = filter2.Name, Anchor = AnchorStyles.None }, 1, 0);
            resultTable.Controls.Add(new Label { Text = filter3.Name, Anchor = AnchorStyles.None }, 2, 0);
            resultTable.Controls.Add(new Label { Text = filter4.Name, Anchor = AnchorStyles.None }, 3, 0);

            resultTable.Controls.Add(filter1.GetDataTable(), 0, 1);
            resultTable.Controls.Add(filter2.GetDataTable(), 1, 1);
            resultTable.Controls.Add(filter3.GetDataTable(), 2, 1);
            resultTable.Controls.Add(filter4.GetDataTable(), 3, 1);
             * */
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            var runners = new DataTable();
            var results = new DataTable();
            var timing = new DataTable();
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
                        cmd.CommandText = "select FirstName, LastName, BibId, CAST(DOB as varchar(10)) as DOB from RaceRunner rr join Runners r where rr.RunnerID=r.RunnerID and rr.RaceID=(select RaceID from Race where Name = @RaceName Limit 1);";
                        cmd.Parameters.AddWithValue("@RaceName", raceData.RaceName);
                        var daRunners = new SQLiteDataAdapter(cmd);
                        daRunners.Fill(runners);

                        cmd.CommandText = "select  ( SELECT COUNT(*) + 1  FROM  RaceResults where time< r.time) as Position, BibID, CAST(Time as varchar(10)) as Time from RaceResults r where r.RaceID=(select RaceID from Race where Name = @RaceName Limit 1) order by Time";
                        var daTimer = new SQLiteDataAdapter(cmd);
                        daTimer.Fill(timing);
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
            }

            

            dataGridRunners.AllowUserToAddRows = false;
            //dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dgv_CellEndEdit);
            //dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dgv_CellValidating);
            dataGridRunners.DataSource = runners;
            dataGridTiming.DataSource = timing;
            
/*
* The following code will be rolled into a method which is part of a "Filter" object.
* Using this, we can create filters and add them to the form dynamically.
*/

             /*Results 1
            Label resLabel1 = new Label();
            resLabel1.Text = "Result 1";
             resLabel1.Location = new Point(8,10);
             resLabel1.Show();
             DataGridView Results1 = new DataGridView();
             var filter1 = new DataTable();
             string r1 = "select BibID, CAST(Time as varchar(10)) as 'Time', ROWID as 'Position' from RaceResults where BibID % 2 = 0 order by Position asc";
             using (var connect = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
             {
                 connect.Open();
                 SQLiteCommand command = new SQLiteCommand();
                 using (command)
                 {
                     command.Connection = connect;
                     command.CommandText = r1;
                     var daFilter1 = new SQLiteDataAdapter(command);
                     daFilter1.Fill(filter1);
                     Results1.DataSource = filter1;
                     Results1.Location = new Point(8, 25);
                     Results1.Size = new Size(350, 185);
                     Results1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
                     Results1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                     Results1.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
                     tabResults.Controls.Add(Results1);
                     tabResults.Controls.Add(resLabel1);
                     Results1.Show();

                     //Results 2
                     DataGridView Results2 = new DataGridView();
                     Label resLabel2 = new Label();
                     resLabel2.Text = "Result 2";
                     resLabel2.Location = new Point(8, 215);
                     resLabel2.Visible = true;
                     var filter2 = new DataTable();
                     string r2 = "select BibID, CAST(Time as varchar(10)) as 'Time', ROWID as 'Position' from RaceResults where BibID % 2 != 0 order by Position asc";
                     //connect.Open();
                     SQLiteCommand command2 = new SQLiteCommand();
                     command2.Connection = connect;
                     command2.CommandText = r2;
                     var daFilter2 = new SQLiteDataAdapter(command2);
                     daFilter2.Fill(filter2);
                     Results2.DataSource = filter2;
                     Results2.Location = new Point(8, 235);
                     Results2.Size = new Size(350, 185);
                     Results2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
                     Results2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                     Results2.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
                     tabResults.Controls.Add(Results2);
                     tabResults.Controls.Add(resLabel2);
                     Results2.Show();
                 }
                 connect.Close();
             }
             */
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
                timingDevice = new KeybordTimer(this);
            }
        }

        private void MainWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            DialogResult results = MessageBox.Show("Key Selected");
        }
        private void StartRace(object sender, EventArgs e)
        {
            if (timingDevice == null) {
                RadioButton rb = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r=>r.Checked);
                switch(rb.Name)
                {
                    case "radioButtonKB":
                        timingDevice = new KeybordTimer(this);
                        break;
                    case "radioButtonTM":
                        timingDevice = new KeybordTimer();
                        break;
                    default:
                        timingDevice = new KeybordTimer(this);
                        break;
                }
            }
            //note! this is different frrom else, we want this to run so long as not null (should be based on above)
            if (timingDevice != null)
                timingDevice.StartRace();
        }
        private void StopRace(object sender, EventArgs e)
        {
            if(timingDevice!=null)
                timingDevice.StopRace();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            List<Filter> filters = new List<Filter>();
            if(e.NewValue==CheckState.Checked)
            {
                foreach (string filter in checkedListBox1.CheckedItems)
                {
                    filters.Add(Filter.BuildFromXML(CommonSQL.filterFolder + "\\" + filter + ".xml", 350, (int)(resultTable.Height * .9)));
                }
                filters.Add(Filter.BuildFromXML(CommonSQL.filterFolder + "\\" + checkedListBox1.Items[e.Index].ToString() + ".xml", 350, (int)(resultTable.Height * .9)));
            }
            else//item was checked and is now being unchecked
            {
                foreach (string filter in checkedListBox1.CheckedItems)
                {
                    if(filter!=checkedListBox1.Items[e.Index])
                        filters.Add(Filter.BuildFromXML(CommonSQL.filterFolder + "\\" + filter + ".xml", 350, (int)(resultTable.Height * .9)));
                }
            }
            //build filters
            buildResults(filters);
        }
    }
}
