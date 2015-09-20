using System;
using TimingForToby;
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
    }
}
