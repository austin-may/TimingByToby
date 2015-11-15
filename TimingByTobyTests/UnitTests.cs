﻿using System;
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
            TimeSpan ts = new TimeSpan();
            KeybordTimer keyboard = new KeybordTimer("F1");
            Assert.IsNotNull(keyboard.GetCurrentTime());
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
        public void TestImportingRunners()
        {
            //DateTime time = DateTime.Now;
            //CommonSQL.AddRunner("4343", "2323", time, "f", "hey", "ok", "ok", "Hahaha", "ok");
            //CommonSQL.AddRunner("Bob", "Martin", time, "male", "345", "ok", "ok", "Race 3", "huh");
            //MainWindow main = new MainWindow();
            //main.OnTime();
            //CommonSQL.FindBadBibs(2);
        }

        [TestMethod]
        public void TestFilters()
        {
            RaceData racedata = new RaceData();
            Filter filter = new Filter(racedata);
            filter.LoadDataTable();
            Filter.BuildFromXML(racedata, "testFile", 5, 5);
        }

    }
}
