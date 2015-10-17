using System;
using TimingForToby;
using System.Windows.Forms;
using System.Data.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TimingByTobyTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestKeyboard()
        {
            KeybordTimer keyboard = new KeybordTimer();
            keyboard.StartRace();
        }

        [TestMethod]
        public void TestImportingRunners()
        {
            DateTime time = DateTime.Now;
            CommonSQL.AddRunner("4343", "2323", time, "hey", "ok", "ok", "Hahaha", "ok");
            CommonSQL.AddRunner("Bob", "Martin", time, "345", "ok", "ok", "Race 3", "huh");
            MainWindow main = new MainWindow();
            main.OnTime();
        }
    }
}
