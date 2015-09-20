using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace TimingForToby
{
     public class Filter
     {
          SQLiteConnection connect = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
          SQLiteCommand command = new SQLiteCommand();
          DataGridView data = new DataGridView();
          DataTable filterTable = new DataTable();
          Label filLabel = new Label();
          RaceData raceData = new RaceData();
          string ageString = "(strftime('%Y', 'now') - strftime('%Y', DOB)) - (strftime('%m-%d', 'now') < strftime('%m-%d', DOB)) = ";
          
          public Filter(string resultNum, string age, string gender){
               string query = "select BibID, CAST(Time as varchar(10)) as 'Time', ROWID as 'Position' from RaceResults where "+ ageString + age +" and Gender = @sex order by Position asc";
               filLabel.Text = "Results" + resultNum;
               filLabel.Location = new Point(8,10);
               filLabel.Show();
               connect.Open();
               command.Connection = connect;
               command.CommandText = query;
               var daFilter = new SQLiteDataAdapter(command);
               daFilter.Fill(filterTable);
               data.DataSource = filterTable;
               data.Location = new Point(8, 25);
               data.Size = new Size(350, 185);
               data.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
               data.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
               data.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
               //tabResults.Controls.Add(Results1);
               //tabResults.Controls.Add(resLabel1);
               //Results1.Show();
               
          }
             
             
             
             
             
             
     }
}
