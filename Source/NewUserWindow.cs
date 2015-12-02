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
        //blank new runner window
        public NewUserWindow(RaceData _raceData)
        {
            raceData = _raceData;
            InitializeComponent();
        }
        //blank new runner window
        public NewUserWindow(RaceData _raceData, MainWindow _parent): this(_raceData)
        {
            this.parent = _parent;
        }
        //pre-pop with runner data and get runner ID (this will preform an update)
        public NewUserWindow(RaceData _raceData, MainWindow _parent, string firstName, string lastName, DateTime dob, string bibID, char sex, string team, string org)
            : this(_raceData, _parent)
        {
            textBoxFirstName.Text = firstName;
            textBoxLastName.Text = lastName;
            dateTimePicker1.Value = dob;
            textBoxBibId.Text = bibID;
            textBoxTeam.Text = team;
            textBoxOrginization.Text=org;
            tbGender.Text = sex+"";
            _RunnerID=CommonSQL.GetRunnerID(firstName, lastName, dob);
        }
        //on done click. try to add runner or update runner if the runner already exsists
        private void btnDone_Click(object sender, EventArgs e)
        {
            if (textBoxFirstName.Text.Trim() == "" || textBoxLastName.Text.Trim() == "" || textBoxBibId.Text.Trim() == "")
            {
                MessageBox.Show("Can not add: First Name, Last Name, and Bib can not be empty");
            }
            else if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("This user has not been born yet! Unborn children can not race");
            }
            else
            {
                //if we already know the runner
                if (_RunnerID > 0)
                    updateRunner();
                else
                    addRunner();
            }
        }
        //add a runner to the db
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
        //updates an exsisting runners info based on runnerID
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
