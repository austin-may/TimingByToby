using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimingForToby
{
    public partial class NewUserWindow : Form
    {
        private RaceData raceData;
        private MainWindow parent;
        //if the user exsist already, we want their ID
        private int _RunnerID=-1;
        public NewUserWindow(RaceData _raceData)
        {
            raceData = _raceData;
            InitializeComponent();
        }
        public NewUserWindow(RaceData _raceData, MainWindow _parent): this(_raceData)
        {
            this.parent = _parent;
        }

        public NewUserWindow(RaceData _raceData, MainWindow _parent, string firstName, string lastName, DateTime dob, string bibID, string team, string org)
            : this(_raceData, _parent)
        {
            textBoxFirstName.Text = firstName;
            textBoxLastName.Text = lastName;
            dateTimePicker1.Value = dob;
            textBoxBibId.Text = bibID;
            textBoxTeam.Text = team;
            textBoxOrginization.Text=org;
            _RunnerID=CommonSQL.GetRunnerID(firstName, lastName, dob);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (textBoxFirstName.Text.Trim() == "" || textBoxLastName.Text.Trim() == "" || textBoxBibId.Text.Trim() == "")
            {
                MessageBox.Show("Can not add: First Name, Last Name, and Bib can not be empty");
            }
            //if we already know the runner
            if (_RunnerID > 0)
                updateRunner();
            else
                addRunner();
        }
        private void addRunner()
        {
            if (!CommonSQL.BibExist(textBoxBibId.Text, raceData.RaceID))
            {
                CommonSQL.AddRunner(textBoxFirstName.Text, textBoxLastName.Text, Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString()), textBoxBibId.Text, tbGender.Text, textBoxTeam.Text, textBoxOrginization.Text, raceData.raceName, raceData.connectionString);
                if (parent != null)
                    parent.Reload();
                CommonSQL.BackupDB();
                this.Close();
            }
            else
            {
                MessageBox.Show("Can not add: Duplicate bib");
            }
        }
        private void updateRunner()
        {
            if (!CommonSQL.BibExistOutsideRunner(textBoxBibId.Text, raceData.RaceID, _RunnerID))
            {
                CommonSQL.UpdateRunner(_RunnerID, textBoxFirstName.Text, textBoxLastName.Text, Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString()), textBoxBibId.Text, tbGender.Text, textBoxTeam.Text, textBoxOrginization.Text, raceData.raceName, raceData.connectionString);
                if (parent != null)
                    parent.Reload();
                CommonSQL.BackupDB();
                this.Close();
            }
            else
            {
                MessageBox.Show("Can not add: Duplicate bib");
            }
        }
    }
}
