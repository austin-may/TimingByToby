using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace TimingForToby
{
    class TobyTimer
    {
        private DateTime _StartTime;
        public TobyTimer()
        {
            _StartTime = DateTime.Now;
        }
        public TobyTimer(DateTime dt)
        {
            _StartTime = dt;
        }
        public void Reset()
        {
            _StartTime = DateTime.Now;
        }
        public TimeSpan Elapsed()
        {
            if (_StartTime == null)
                return new TimeSpan(0, 0, 0);
            return DateTime.Now - _StartTime;
        }
        public void OffSetTime(TimeSpan ts)
        {
            _StartTime = DateTime.Now - ts;
        }

        private static System.Timers.Timer BackupTimer;
        internal static void SetTimer()
        {
            // Create a timer that fires every minute
            BackupTimer = new System.Timers.Timer(60000);
            //Sync lapsed events to timer
            BackupTimer.Elapsed += OnTimedEvent;
            BackupTimer.AutoReset = true;
            BackupTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //MessageBox.Show("Backup just occured at " + 
               //               e.SignalTime);
            CommonSQL.BackupDB();
        }


    }
}
