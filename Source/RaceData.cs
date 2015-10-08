using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace TimingForToby
{
    public class RaceData
    {
        internal string ConnectionString;
        internal string RaceName;
        public readonly int RaceID;
        public Form StartWindow;
        public RaceData(){}
        public RaceData(Form StartScreen, string _RaceName, string _Connection)
        {
            this.StartWindow=StartScreen;
            this.RaceName=_RaceName;
            this.ConnectionString = _Connection;
            using (var conn = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "select RaceID from Race where Name = @RaceName Limit 1;";
                        cmd.Parameters.AddWithValue("@RaceName", RaceName);
                        SQLiteDataReader r = cmd.ExecuteReader();
                        if (r.HasRows)
                        {
                            r.Read();
                            RaceID=r.GetInt32(0);
                            r.Dispose();                            
                        }
                        else
                        {
                            Console.WriteLine("No rows found.");
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}
