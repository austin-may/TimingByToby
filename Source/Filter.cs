using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
          
          SQLiteCommand command = new SQLiteCommand();
          DataGridView data = new DataGridView();
          Label filLabel = new Label();
          private int RaceID;
         private String connectionSting;
          string ageString = "(strftime('%Y', 'now') - strftime('%Y', DOB)) - (strftime('%m-%d', 'now') < strftime('%m-%d', DOB))";
          private String _Sql;
         public String Name;
          
         public Filter(RaceData raceData){
             RaceID = raceData.RaceID;
             connectionSting = raceData.ConnectionString;
             data.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
             data.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
             data.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
             data.AllowUserToAddRows = false;
         }
          public Filter(RaceData raceData, string name, string query, int width=-1, int height=-1)
              : this(raceData)
          {
              Name = name;
              _Sql = query;
              if (width > 0 && height > 0)
              {
                  data.Width = width;
                  data.Height = height;
              }
              else
                data.AutoSize = true;
          }
          public void LoadDataTable()
          {
              using (var connect = new SQLiteConnection(connectionSting))
              {
                  try
                  {
                      connect.Open();
                      command.Connection = connect;
                      command.CommandText = _Sql;
                      command.Parameters.AddWithValue("@RaceID", RaceID);  
                      var filterTable = new DataTable();
                      var daFilter = new SQLiteDataAdapter(command);
                      daFilter.Fill(filterTable);
                      data.DataSource = filterTable;
                  }
                  catch (Exception e)
                  {
                      MessageBox.Show(e.Message);
                  }
              }
          }
          public DataGridView GetDataTable()
          {
              return data;
          }

          public static List<Filter> BuildFromXML(RaceData raceData, string file, int width = -1, int height = -1)
          {
              var filters=new List<Filter>();
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
                    if (!string.IsNullOrEmpty(sql) && !string.IsNullOrEmpty(name)||(xml.IsEmptyElement && xml.Name=="Filter"))
                    {
                        filters.Add(new Filter(raceData, name, sql, width, height));
                        sql = null;
                        name = null;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
            return filters;
            }
     }
}
