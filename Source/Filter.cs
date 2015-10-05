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
using System.Xml.Serialization;
using System.Xml;

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
         public String Name;
          
         public Filter(){

         }
         public Filter(string name, string age, string gender, int width = -1, int height = -1)
             : this()
         {
               string query = "select BibID, CAST(Time as varchar(10)) as 'Time', ROWID as 'Position' from RaceResults where "+ ageString + age +" and Gender = "+gender+" order by Position asc";
               connect.Open();
               command.Connection = connect;
               command.CommandText = query;
               var daFilter = new SQLiteDataAdapter(command);
               daFilter.Fill(filterTable);
               data.DataSource = filterTable;
               data.AutoSize = true;
               data.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
               data.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
               data.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
               Name = name;
               //tabResults.Controls.Add(Results1);
               //tabResults.Controls.Add(resLabel1);
               //Results1.Show();               
          }
          public Filter(string name, string query, int width=-1, int height=-1)
              : this()
          {
              connect.Open();
              command.Connection = connect;
              command.CommandText = query;
              var daFilter = new SQLiteDataAdapter(command);
              daFilter.Fill(filterTable);
              data.DataSource = filterTable;
              if (width > 0 && height > 0)
              {
                  data.Width = width;
                  data.Height = height;
              }
              else
                data.AutoSize = true;
              data.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
              data.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
              data.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
              Name = name;
              //tabResults.Controls.Add(Results1);
              //tabResults.Controls.Add(resLabel1);
              //Results1.Show();               
          }
          public DataGridView GetDataTable()
          {
              return data;
          }

          public static Filter BuildFromXML(string file, int width = -1, int height = -1)
          {
            string name=null, sql=null;
            var xml = new XmlTextReader(file);
            try
            {
                while (xml.Read())
                {
                    if (xml.IsStartElement())
                    {
                        if (xml.Name == "Name")
                        {
                            xml.Read();
                            name = xml.Value;
                        }
                        else if (xml.Name == "SQL")
                        {
                            xml.Read();
                            sql = xml.Value;
                        }
                    }
                    if (!string.IsNullOrEmpty(sql) && !string.IsNullOrEmpty(name))
                    {
                        return new Filter(name, sql, width, height);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
            return null;
            }
     }
}
