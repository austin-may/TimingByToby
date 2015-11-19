using System;
using TimingForToby;
using System.Windows.Forms;
using System.Data.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;

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
        public void TestGetRaceID()
        {
            //Return raceID of an existing race
            Assert.AreEqual(1, CommonSQL.GetRaceID("Sample Race"));
            //RaceID of -1 is returned when a race does is passed that does not exist
            Assert.AreEqual(-1, CommonSQL.GetRaceID("RaceNonExistent"));
        }

        [TestMethod]
        public void TestFindBadBibs()
        {
            //Test it's adding just the dictionary key to a bad bib
            CollectionAssert.AllItemsAreUnique(CommonSQL.FindBadBibs(1));
            
        }

        [TestMethod]
        public void TestTobyTimer()
        {
            //Test public constructor with no parameters
            TobyTimer tobyTimer = new TobyTimer();
            //Test it returns some form of a timespan
            Assert.IsNotNull(tobyTimer.Elapsed());
            //Test public constructor when taking in a datetime parameter
            DateTime dt = DateTime.Now;
            TobyTimer tobyTimer1 = new TobyTimer(dt);
            Assert.IsNotNull(tobyTimer1.Elapsed());
            
        }

        [TestMethod]
        public void TestMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            //Test the exception case when clock is at zero
            Assert.AreEqual(TimeSpan.Zero, mainWindow.GetClockTime());
            //Clock is a GUI element so that will be BB tested
        }


        [TestMethod]
        public void TestFilters()
        {
            StartScreen ss = new StartScreen();
            RaceData racedata = new RaceData(ss, "RaceUnitTest", "Data Source=MyDatabase.sqlite;Version=3;");
            Filter filter = new Filter(racedata);
            //Test it returns filter
            var RealFilter = new List<Filter>();
            var TestFilter = new List<Filter>();
            RealFilter = Filter.BuildFromXML(racedata, "OveralFilter.xml", 5, 5);
            TestFilter = Filter.BuildFromXML(racedata, "OveralFilter.xml", 5, 5);
            //Test that two different instances of the same filter are not equal
            //and as a result do not get overwritten
            CollectionAssert.AreNotEqual(RealFilter, TestFilter);
            //Test the catch exception
            Assert.IsNull(Filter.BuildFromXML(racedata, "NoFilter.xml", 5, 5));
            //Test there is data in the datatable
            Assert.IsNotNull(filter.GetDataTable());
        }

        [TestMethod]
        public void TestDBExsist()
        {
            //Will only pass if database is not present
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
