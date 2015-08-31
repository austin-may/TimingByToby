using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

/*
 * YAPT (Yet Another Push Test)
 * This is a test of the git push function.
 * Only comments were added.
 * Had this been a real push, actual MEANINGFUL changes would have been made.
 * Again, this is only a test.
 */ 

namespace WindowsFormsApplication1
{
    class Class1
    {

        private SQLiteConnection connection;
        public Class1()
        {
            SQLiteConnection.CreateFile("MyDatabase.sqlite");
        }
        public void connect()
        {
            connection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            connection.Open();
        }
        public void disconnect()
        {
            if (connection != null)
                connection.Close();
        }
        public static void test()
        {
            var data = new Class1();
            data.connect();
            string sql = "create table Runners (firstname varchar(20), lastname varchar(20), age int)";
            var command = new SQLiteCommand(sql, data.connection);
            command = new SQLiteCommand("insert into Runners Values('Thomas', 'Jones', 21)", data.connection);
            data.disconnect();
        }
    }
}
