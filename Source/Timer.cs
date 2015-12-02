using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace TimingForToby
{
    class TobyTimer
    {

        private DateTime _StartTime;
        private static Stack timestampStack = new Stack();
        public TobyTimer()
        {
            _StartTime = DateTime.Now;
        }
        public TobyTimer(DateTime dt)
        {
            _StartTime = dt;
        }
        //reset clock
        public void Reset()
        {
            _StartTime = DateTime.Now;
        }
        //time pased from start
        public TimeSpan Elapsed()
        {
            if (_StartTime == null)
                return new TimeSpan(0, 0, 0);
            return DateTime.Now - _StartTime;
        }
        //offset clock by set ammount of time
        public void OffSetTime(TimeSpan ts)
        {
            _StartTime = DateTime.Now - ts;
        }
        //create a backup but only if +60 seconds since last backup
        internal static void BackupAfter60Seconds()
        {
            TimeSpan rightNow = DateTime.Now.TimeOfDay;
            //if a backup hasn't been created yet create one
            if (timestampStack.Count == 0)
            {
                TimeStampedBackup(false);
            }
            else
            {
                TimeSpan lastBackup = (TimeSpan)timestampStack.Pop();
                if ((rightNow.Seconds - lastBackup.Seconds) > 60)
                {
                    TimeStampedBackup(false);
                }
            }
            
        }

        //performs the backup
        internal static void TimeStampedBackup(bool isClosing)
        {
            string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string sourcePath = Directory.GetCurrentDirectory();
            string destinationPath = AppDataPath + "\\TimingForToby\\AutoBackups";
            string sourceFileName = @"MyDatabase.sqlite";
            //There's no colon in the time because as you might know that's not allowed in file names :(
            string timestamp = string.Format("{0:MM-dd-yyyy hh-mm tt}", DateTime.Now);
            timestampStack.Push(DateTime.Now.TimeOfDay);
            string destinationFileName = "";
            //determines if the auto backup is being called because of a close event or because of a period.
            if (isClosing == false)
            {
                destinationFileName = "Auto Backup " + timestamp + ".sqlite";
            }
            else
            {
                destinationFileName = "Last Close " + timestamp + ".sqlite";
            }
            string destinationFile = Path.Combine(destinationPath, destinationFileName);

            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }
            //enseure that we dont fill the HD
            long megaByte = 1000000;
            CommonSQL.DelToSize(destinationPath, megaByte*250);
            File.Copy(sourceFileName, destinationFile, true);
        }
    }
        
        
}
