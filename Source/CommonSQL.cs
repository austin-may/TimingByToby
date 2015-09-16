using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimingForToby
{
    class CommonSQL
    {
        internal static string SQLiteConnection = "Data Source=MyDatabase.sqlite;Version=3;";
        internal static void AddRunner(string FirstName, string LastName, DateTime DOB, string BibID, string Team, string Orginization, string RaceName, string Connection){
            using (var conn = new SQLiteConnection(Connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand())
                {
                        cmd.Connection = conn;
                        cmd.CommandText = "Insert Into Runners(FirstName, LastName, DOB) Values(@FirstName, @LastName, @DOB);";//'"+DOB.ToString("MM/dd/yyyy")+"');";
                        cmd.Parameters.AddWithValue("@FirstName", FirstName);
                        cmd.Parameters.AddWithValue("@LastName", LastName);
                        cmd.Parameters.AddWithValue("@DOB", DOB.ToString("MM/dd/yyyy"));
                        cmd.ExecuteNonQuery();
                        //cmd.Parameters.Add(new SQLiteParameter("@FirstName", SqlDbType.Text) { Value = FirstName });
                        //cmd.Parameters.Add(new SQLiteParameter("@LastName", SqlDbType.Text) { Value = LastName });
                        //cmd.Parameters.Add(new SQLiteParameter("@DOB", DbType.Date) { Value =  });
                        //cmd.Parameters.Add(DOB.ToString("MM/DD/YYYY"));
                        cmd.CommandText = "Insert into RaceRunner(RunnerID, RaceID, BibID, Orginization, Team) Values(" +
                            "(select RunnerID from Runners where FirstName=@FirstName AND LastName=@LastName Limit 1)," +
                            "(select RaceID from Race where Name=@Race Limit 1)," +
                            "@BibID,@Orginization,@Team);";


                        cmd.Parameters.AddWithValue("@Race", RaceName);
                        cmd.Parameters.AddWithValue("@BibID", BibID);
                        cmd.Parameters.AddWithValue("@Team", Team);
                        cmd.Parameters.AddWithValue("@Orginization", Orginization);
                        cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        internal static void AddRunners(string[] FirstName, string[] LastName, DateTime[] DOB, string[] BibID, string[] Team, string[] Orginization, string RaceName, string Connection)
        {
            //big assumption that all arrays are same size or atleast larger than the FirstName array
            using (var conn = new SQLiteConnection(Connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand())
                {
                    for (int i = 0; i < FirstName.Length; i++)
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "Insert Into Runners(FirstName, LastName, DOB) Values(@FirstName, @LastName, @DOB);";//'"+DOB.ToString("MM/dd/yyyy")+"');";
                        cmd.Parameters.AddWithValue("@FirstName", FirstName[i]);
                        cmd.Parameters.AddWithValue("@LastName", LastName[i]);
                        cmd.Parameters.AddWithValue("@DOB", DOB[i].ToString("MM/dd/yyyy"));
                        cmd.ExecuteNonQuery();
                        //cmd.Parameters.Add(new SQLiteParameter("@FirstName", SqlDbType.Text) { Value = FirstName });
                        //cmd.Parameters.Add(new SQLiteParameter("@LastName", SqlDbType.Text) { Value = LastName });
                        //cmd.Parameters.Add(new SQLiteParameter("@DOB", DbType.Date) { Value =  });
                        //cmd.Parameters.Add(DOB.ToString("MM/DD/YYYY"));
                        cmd.CommandText = "Insert into RaceRunner(RunnerID, RaceID, BibID, Orginization, Team) Values(" +
                            "(select RunnerID from Runners where FirstName=@FirstName AND LastName=@LastName Limit 1)," +
                            "(select RaceID from Race where Name=@Race Limit 1)," +
                            "@BibID,@Orginization,@Team);";


                        cmd.Parameters.AddWithValue("@Race", RaceName);
                        cmd.Parameters.AddWithValue("@BibID", BibID[i]);
                        cmd.Parameters.AddWithValue("@Team", Team[i]);
                        cmd.Parameters.AddWithValue("@Orginization", Orginization[i]);
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }
    }
}
