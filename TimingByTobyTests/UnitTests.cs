using System;
using TimingForToby;
using System.Windows.Forms;
using System.Data.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace TimingByTobyTests
{
    [TestClass]
    public class UnitTests
    {

        [TestMethod]
        public void TestKeyboard()
        {
            TimeSpan ts = new TimeSpan();
            KeybordTimer keyboard = new KeybordTimer("F1");
            Assert.IsNotNull(keyboard.GetCurrentTime());
        }

        [TestMethod]
        public void TestImportingRunners()
        {
            //DateTime time = DateTime.Now;
            //CommonSQL.AddRunner("4343", "2323", time, "hey", "female", "ok", "ok", "Hahaha", "Data Source=MyDatabase.sqlite;Version=3;");
            //CommonSQL.AddRunner("Bob", "Martin", time, "345", "male", "ok", "ok", "Race 3", "Data Source=MyDatabase.sqlite;Version=3;");
            //RaceData rd = new RaceData(null, "Sample Data", "Data Source=MyDatabase.sqlite;Version=3;");
            //MainWindow main = new MainWindow(rd);
            //main.OnTime();
        }

        [TestMethod]
        public void TestTobyTimer()
        {
            //Test public constructor with no parameters
            TobyTimer tobyTimer = new TobyTimer();
            Assert.IsNotNull(tobyTimer.Elapsed());
            //Test public constructor when taking in a datetime parameter
            DateTime dt = new DateTime();
            TobyTimer tobyTimer1 = new TobyTimer(dt);
            Assert.IsNotNull(tobyTimer1.Elapsed());
        }


        [TestMethod]
        public void TestFilters()
        {
            RaceData racedata = new RaceData();
            Filter filter = new Filter(racedata);
            filter.LoadDataTable();
            Filter.BuildFromXML(racedata, "testFile", 5, 5);
        }
        [TestMethod]
        public void TestDBExsist()
        {
            CommonSQL.BuildIfNotExsistDB();
        }

        [TestMethod]
        public void TestDBNotExsist()
        {
            File.Delete(CommonSQL.GetDBFileName());
            CommonSQL.BuildIfNotExsistDB();
            File.Exists(CommonSQL.GetDBFileName());
        }
    }
}
