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

namespace TimingForToby
{
    public partial class NewRaceWindow : Form
    {
        public NewRaceWindow()
        {
            InitializeComponent();
            lableError.ForeColor = System.Drawing.Color.Red;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            int rowsAffected=0;
            using (var conn = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "insert into Race (Name) values('" + this.textBox1.Text + "');";
                    try {
                        rowsAffected=cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException sqlE)
                    {
                        if(sqlE.Message.Contains("UNIQUE"))
                        {
                            lableError.Text="Error: Names Must be unique";
                        }
                    }
                }
                conn.Close();
            }
            if(rowsAffected>0)
                this.Close();
        }

        private void NewRaceWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
