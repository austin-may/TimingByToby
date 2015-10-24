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
        public NewUserWindow(RaceData _raceData)
        {
            raceData = _raceData;
            InitializeComponent();
        }
        public NewUserWindow(RaceData _raceData, MainWindow _parent): this(_raceData)
        {
            this.parent = _parent;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            CommonSQL.AddRunner(textBoxFirstName.Text, textBoxLastName.Text, Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString()), textBoxBibId.Text, textBoxTeam.Text, textBoxOrginization.Text, raceData.RaceName, raceData.ConnectionString);
            if (parent != null)
                parent.reload();
            CommonSQL.BackupDB();
            this.Close();
        }

        private void NewUserWindow_Load(object sender, EventArgs e)
        {

        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
