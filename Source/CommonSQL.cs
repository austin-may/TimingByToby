using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.Threading;
using System.IO;

[assembly: InternalsVisibleTo("TimingByTobyTests")]

namespace TimingForToby
{
    public class CommonSQL
    {
        private static string _dbFile="MyDatabase.sqlite";
        internal static string SQLiteConnectionString = "Data Source=MyDatabase.sqlite;Version=3;";
        internal static string backupDB = "Data Source=BackupDatabase.sqlite;Version=3;";
        private static Dictionary<string, int> RaceIdMap = new Dictionary<string, int>();
        public static string filterFolder="Filters";
        public static SQLiteConnection originalDatabase;
        public static SQLiteConnection backupDatabase;
        private static SQLiteConnection _db = new SQLiteConnection(SQLiteConnectionString);
        public static string GetDBFileName()
        {
            return _dbFile;
        }
        public static void BuildIfNotExsistDB()
        {
            try
            {
                //look to see if the db file exsist, if not, create it
                if(File.Exists(_dbFile))
                    return;
                //if that file doesnt exsist
                //force close so we can work
                _db.Close();
                //create db file
                SQLiteConnection.CreateFile(_dbFile);
                //re-establish connection
                _db = new SQLiteConnection(SQLiteConnectionString);
                _db.Open();
                //build db
                using (var cmd = new SQLiteCommand())
                {
                    cmd.Connection = _db;
                    cmd.CommandText =   //Table: Race
                                        "DROP TABLE IF EXISTS Race;"+
                                        "CREATE TABLE Race (RaceID INTEGER PRIMARY KEY AUTOINCREMENT, Name VARCHAR (50) NOT NULL UNIQUE ON CONFLICT FAIL);"+
                                        
                                        //Table: RaceRunner
                                        "DROP TABLE IF EXISTS RaceRunner;"+
                                        "CREATE TABLE RaceRunner (RaceID INTEGER REFERENCES Race (RaceID) ON DELETE CASCADE ON UPDATE CASCADE NOT NULL, BibID VARCHAR (10), RunnerID INTEGER REFERENCES Runners (RunnerID) ON DELETE CASCADE ON UPDATE CASCADE NOT NULL, Orginization VARCHAR (250), Team VARCHAR (250));"+

                                        //Table: Runners
                                        "DROP TABLE IF EXISTS Runners;"+
                                        "CREATE TABLE Runners (RunnerID INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL, FirstName VARCHAR (25) NOT NULL, LastName VARCHAR (25) NOT NULL, DOB DATE NOT NULL, Gender CHAR);"+

                                        //Table: RaceResults
                                        "DROP TABLE IF EXISTS RaceResults;"+
                                        "CREATE TABLE RaceResults (RaceID INTEGER REFERENCES Race (RaceID) ON DELETE CASCADE ON UPDATE CASCADE NOT NULL, BibID VARCHAR (10), Time TIME NOT NULL);";
                    var r = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception sqlError)
            {
                MessageBox.Show(sqlError.Message);
            }
            finally
            {
                _db.Close();
            }
        }
        internal static void AddRunner(string FirstName, string LastName, DateTime DOB, string BibID, string sex, string Team, string Orginization, string RaceName, string Connection){
            BuildIfNotExsistDB();
            int raceID = GetRaceID(RaceName);
            using (var conn = new SQLiteConnection(Connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Insert Into Runners(FirstName, LastName, DOB, Gender) Values(@FirstName, @LastName, @DOB, @Sex);";
                    cmd.Parameters.AddWithValue("@FirstName", FirstName);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@DOB", DOB.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Sex", sex);
                    //check to ensure that value was added, if not, break
                    if(cmd.ExecuteNonQuery()==0)
                    {
                        MessageBox.Show("No Runner added");
                        return;
                    }

                    cmd.CommandText =
                        "Insert or ignore into RaceRunner(RunnerID, RaceID, BibID, Orginization, Team) Values(" +
                        "(select RunnerID from Runners where FirstName=@FirstName AND LastName=@LastName Limit 1)," +
                        "@Race," +
                        "@BibID,@Orginization,@Team);";

                    cmd.Parameters.AddWithValue("@Race", raceID);
                    cmd.Parameters.AddWithValue("@BibID", BibID);
                    cmd.Parameters.AddWithValue("@Team", Team);
                    cmd.Parameters.AddWithValue("@Orginization", Orginization);
                    //check to ensure that value was added, if not, break
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        MessageBox.Show("No Runner added to race");
                        return;
                    }                 
                }
                conn.Close();
            }
        }
        
        //the inserting of every runner that happens on an asynchronous thread
        public static Task ProcessRunners(string[] FirstName, string[] LastName, DateTime[] DOB, char[] Genders, string[] BibID, string[] Team, string[] Orginization, string RaceName, string Connection, IProgress<ProgressReport> progress)
        {
            BuildIfNotExsistDB();
            int raceID = GetRaceID(RaceName);
            int index = 1;
            //big assumption that all arrays are same size or atleast larger than the FirstName array
            int totalProcess = FirstName.Length;
            var progessReport = new ProgressReport();
            return Task.Run(() =>
            {
                using (var conn = new SQLiteConnection(Connection))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand())
                    {
                        for (int i = 0; i < totalProcess; i++)
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "Insert Into Runners(FirstName, LastName, DOB, Gender) Values(@FirstName, @LastName, @DOB, @Sex);";
                            cmd.Parameters.AddWithValue("@FirstName", FirstName[i]);
                            cmd.Parameters.AddWithValue("@LastName", LastName[i]);
                            cmd.Parameters.AddWithValue("@DOB", DOB[i].ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@Sex", Genders[i]);
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "Insert into RaceRunner(RunnerID, RaceID, BibID, Orginization, Team) Values(" +
                                "(select RunnerID from Runners where FirstName=@FirstName AND LastName=@LastName Limit 1)," +
                                "@Race," +
                                "@BibID,@Orginization,@Team);";

                            cmd.Parameters.AddWithValue("@Race", raceID);
                            cmd.Parameters.AddWithValue("@BibID", BibID[i]);
                            cmd.Parameters.AddWithValue("@Team", Team[i]);
                            cmd.Parameters.AddWithValue("@Orginization", Orginization[i]);
                            cmd.ExecuteNonQuery();
                            progessReport.PercentComplete = index++ * 100 / totalProcess;
                            progress.Report(progessReport);
                        }
                    }
                    conn.Close();
                }
            });            
        }        

        internal static void BackupDB()
        {
            originalDatabase = new SQLiteConnection(SQLiteConnectionString);
            backupDatabase = new SQLiteConnection(backupDB);
            originalDatabase.Open();
            backupDatabase.Open();
            originalDatabase.BackupDatabase(backupDatabase, "main", "main", -1, null, -1);
            originalDatabase.Close();
            backupDatabase.Close();
        }
        
        internal static void UpdateTimingBib(int raceID, string oldBib, string time, string newBib)
        {
            BuildIfNotExsistDB();
            using (var conn = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "update raceresults set BibID=@newBib where RaceID=@RaceID AND BibID=@oldBib AND Time=@time;";
                        cmd.Parameters.AddWithValue("@RaceID", raceID);
                        cmd.Parameters.AddWithValue("@oldBib", oldBib);
                        cmd.Parameters.AddWithValue("@newBib", newBib);
                        cmd.Parameters.AddWithValue("@time", time);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception sqlError)
                {
                    MessageBox.Show(sqlError.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        internal static void UpdateTimingTime(int raceID, string bib, string oldTime, string newTime)
        {
            BuildIfNotExsistDB();
            using (var conn = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "update raceresults set Time=@newTime where RaceID=@RaceID AND BibID=@Bib AND Time=@oldTime;";
                        cmd.Parameters.AddWithValue("@RaceID", raceID);
                        cmd.Parameters.AddWithValue("@Bib", bib);
                        cmd.Parameters.AddWithValue("@newTime", newTime);
                        cmd.Parameters.AddWithValue("@oldTime", oldTime);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception sqlError)
                {
                    MessageBox.Show(sqlError.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        //returns -1 if no race is found
        public static int GetRaceID(string raceName){
            BuildIfNotExsistDB();
            int raceID;
            if (RaceIdMap.TryGetValue(raceName, out raceID))
            {
                return raceID;
            }
            //becouse if it wasnt found before, RaceID is now Default int (0)
            raceID = -1;
            using (var conn = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "select RaceID from Race where Name = @RaceName Limit 1;";
                        cmd.Parameters.AddWithValue("@RaceName", raceName);
                        SQLiteDataReader r = cmd.ExecuteReader();
                        if (r.HasRows)
                        {
                            r.Read();
                            raceID=r.GetInt32(0);
                            RaceIdMap.Add(raceName, raceID);
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
                return raceID;
            }
        }
        public static List<string> FindBadBibs(int raceID)
        {
            BuildIfNotExsistDB();
            var badBibs = new List<string>();
            using (var conn = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand())
                    {
                        //get list of all results in table that have multiple BibID entries
                        cmd.Connection = conn;
                        cmd.CommandText = "select BibID from(select count(BibID) as count, BibID from RaceResults where RaceID=@RaceID Group by (BibID)) as dictionary where dictionary.count>1;";
                        cmd.Parameters.AddWithValue("@RaceID", raceID+"");

                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            badBibs.Add(reader[0].ToString());
                        }
                        reader.Dispose();
                    }
                }
                catch (Exception sqlError)
                {
                    MessageBox.Show(sqlError.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return badBibs;
        }

        internal static void AddTimeAndBib(int raceID, string bib, string time)
        {
            BuildIfNotExsistDB();
            using (var conn = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "insert into raceresults(RaceID, BibID, Time) values(@RaceID, @Bib, @Time);";
                        cmd.Parameters.AddWithValue("@RaceID", raceID);
                        cmd.Parameters.AddWithValue("@Bib", bib);
                        cmd.Parameters.AddWithValue("@Time", time);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception sqlError)
                {
                    MessageBox.Show(sqlError.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        internal static void DelTimingRow(int raceID, string time)
        {
            BuildIfNotExsistDB();
            using (var conn = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "delete from raceresults where RaceID=@RaceID AND Time=@time;";
                        cmd.Parameters.AddWithValue("@RaceID", raceID);
                        cmd.Parameters.AddWithValue("@time", time);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception sqlError)
                {
                    MessageBox.Show(sqlError.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        //reuturn runner ID based on name and dob. return -1 if none is found
        internal static int GetRunnerID(string firstName, string LastName, DateTime dob)
        {
            BuildIfNotExsistDB();
            int runnerID = -1;
            bool alreadyOpen = _db.State != System.Data.ConnectionState.Closed;
                try
                {
                    if(!alreadyOpen)
                        _db.Open();
                    using (var cmd = new SQLiteCommand())
                    {
                        cmd.Connection = _db;
                        cmd.CommandText = "select RunnerID from runners where FirstName=@firstName and LastName=@lastName and DOB=@dob;";
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@lastName", LastName);
                        cmd.Parameters.AddWithValue("@dob", dob.ToString("yyyy-MM-dd"));

                        SQLiteDataReader r = cmd.ExecuteReader();
                        if (r.HasRows)
                        {
                            r.Read();
                            runnerID = r.GetInt32(0);
                            r.Dispose();
                        }
                        else
                        {
                            Console.WriteLine("No rows found.");
                        }
                    }
                }
                catch (Exception sqlError)
                {
                    MessageBox.Show(sqlError.Message);
                }
                finally
                {
                    if(!alreadyOpen)
                        _db.Close();
                }
            return runnerID;
        }
        internal static void DelRunner(string firstName, string LastName, DateTime dob, int raceID)
        {
            BuildIfNotExsistDB();
            bool alreadyOpen = _db.State != System.Data.ConnectionState.Closed;
            try
            {
                if (!alreadyOpen)
                    _db.Open();
                int id = GetRunnerID(firstName, LastName, dob);
                using (var cmd = new SQLiteCommand())
                {
                    cmd.Connection = _db;
                    cmd.CommandText = "delete from RaceRunner where RunnerID=@id and RaceID=@RaceID;";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@RaceID", raceID);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception sqlError)
            {
                MessageBox.Show(sqlError.Message);
            }
            finally
            {
                if (!alreadyOpen)
                    _db.Close();
            }
        }
        internal static bool BibExist(string bibID, int raceID)
        {
            BuildIfNotExsistDB();
            bool results = false;
            bool alreadyOpen = _db.State != System.Data.ConnectionState.Closed;
            try
            {
                if (!alreadyOpen)
                    _db.Open();
                using (var cmd = new SQLiteCommand())
                {
                    cmd.Connection = _db;
                    cmd.CommandText = "select * from RaceRunner where BibID=@id and RaceID=@RaceID;";
                    cmd.Parameters.AddWithValue("@id", bibID);
                    cmd.Parameters.AddWithValue("@RaceID", raceID);

                    var r=cmd.ExecuteReader();
                    results= r.HasRows;
                    if (results)
                    {
                        //burn through results to prevent db lock
                        while (r.Read()) { }
                    }
                }
            }
            catch (Exception sqlError)
            {
                MessageBox.Show(sqlError.Message);
            }
            finally
            {
                if (!alreadyOpen)
                    _db.Close();
            }
            //if we get here then ya, it doesnt exsist
            return results;
        }
        //same as above except we want to check if the bib exsist excluding runner
        internal static bool BibExistOutsideRunner(string bibID, int raceID, int runnerID)
        {
            BuildIfNotExsistDB();
            bool results = false;
            bool alreadyOpen = _db.State != System.Data.ConnectionState.Closed;
            try
            {
                if (!alreadyOpen)
                    _db.Open();
                using (var cmd = new SQLiteCommand())
                {
                    cmd.Connection = _db;
                    cmd.CommandText = "select * from RaceRunner where BibID=@id and RaceID=@RaceID and RunnerID<>@runnerID;";
                    cmd.Parameters.AddWithValue("@id", bibID);
                    cmd.Parameters.AddWithValue("@RaceID", raceID);
                    cmd.Parameters.AddWithValue("@runnerID", runnerID);

                    var r = cmd.ExecuteReader();
                    results = r.HasRows;
                    if (results)
                    {
                        //burn through results to prevent db lock
                        while (r.Read()) { }
                    }
                }
            }
            catch (Exception sqlError)
            {
                MessageBox.Show(sqlError.Message);
            }
            finally
            {
                if (!alreadyOpen)
                    _db.Close();
            }
            //if we get here then ya, it doesnt exsist
            return results;
        }
        internal static void UpdateRunner(int runnerId, string FirstName, string LastName, DateTime DOB, string BibID, string sex, string Team, string Orginization, string RaceName, string Connection)
        {
            BuildIfNotExsistDB();
            int raceID = GetRaceID(RaceName);
            using (var conn = new SQLiteConnection(Connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Update Runners set FirstName=@FirstName, LastName=@LastName, DOB=@DOB, Gender=@Sex where RunnerID=@runnerId;";
                    cmd.Parameters.AddWithValue("@FirstName", FirstName);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@DOB", DOB.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Sex", sex);
                    cmd.Parameters.AddWithValue("@runnerId", runnerId);
                    //check to ensure that value was added, if not, break
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        MessageBox.Show("No Runner changed");
                        return;
                    }

                    cmd.CommandText =
                        "Update RaceRunner set BibID=@BibID, Orginization=@Orginization, Team=@Team where RunnerID=@runnerId";

                    cmd.Parameters.AddWithValue("@Race", raceID);
                    cmd.Parameters.AddWithValue("@BibID", BibID);
                    cmd.Parameters.AddWithValue("@Team", Team);
                    cmd.Parameters.AddWithValue("@Orginization", Orginization);
                    //check to ensure that value was added, if not, break
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        MessageBox.Show("Runner race data not updated");
                        return;
                    }
                }
                conn.Close();
            }
        }        
    }
}
