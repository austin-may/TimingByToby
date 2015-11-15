using System;
using TimingForToby;
using System.Windows.Forms;
using System.Data.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TimingByTobyTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestKeyboard()
        {
            KeybordTimer keyboard = new KeybordTimer(Keys.F1.ToString());
            keyboard.StartRace();
        }

        [TestMethod]
        public void TestImportingRunners()
        {
            DateTime time = DateTime.Now;
            CommonSQL.AddRunner("4343", "2323", time, "hey", "female", "ok", "ok", "Hahaha", "Data Source=MyDatabase.sqlite;Version=3;");
            CommonSQL.AddRunner("Bob", "Martin", time, "345", "male", "ok", "ok", "Race 3", "Data Source=MyDatabase.sqlite;Version=3;");
            RaceData rd = new RaceData(null, "Sample Data", "Data Source=MyDatabase.sqlite;Version=3;");
            MainWindow main = new MainWindow(rd);
            main.OnTime();
        }
    }
}
