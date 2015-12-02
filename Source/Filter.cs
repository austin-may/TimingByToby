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
          private int _raceID;
         private String _connectionSting;
          string ageString = "(strftime('%Y', 'now') - strftime('%Y', DOB)) - (strftime('%m-%d', 'now') < strftime('%m-%d', DOB))";
          private String _Sql;
         public String name;
          
         public Filter(RaceData raceData){
             _raceID = raceData.RaceID;
             _connectionSting = raceData.connectionString;
             data.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
             data.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
             data.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
             data.AllowUserToAddRows = false;
         }
          public Filter(RaceData raceData, string name, string query, int width=-1, int height=-1)
              : this(raceData)
          {
              this.name = name;
              _Sql = query;
              if (width > 0 && height > 0)
              {
                  data.Width = width;
                  data.Height = height;
              }
              else
                data.AutoSize = true;
          }
         //pop filter with data 
         public void LoadDataTable()
          {
              using (var connect = new SQLiteConnection(_connectionSting))
              {
                  try
                  {
                      connect.Open();
                      command.Connection = connect;
                      command.CommandText = _Sql;
                      command.Parameters.AddWithValue("@RaceID", _raceID);  
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
         //return table containing data
          public DataGridView GetDataTable()
          {
              return data;
          }
         //construct filter based on xml params
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
