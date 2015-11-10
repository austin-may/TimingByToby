using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;


namespace TimingForToby
{
    public class KeybordTimer : TimingDevice
    {
        private TobyTimer timer;
        private List<Keys> triggerKeys=new List<Keys>();
        private KeyEventHandler keyHandler;

        public KeybordTimer(string keyCode)
        {
            switch(keyCode){
                case "F1":
                    triggerKeys.Add(Keys.F1);
                    break;
                case "F2":
                    triggerKeys.Add(Keys.F2);
                    break;
                case "F3":
                    triggerKeys.Add(Keys.F3);
                    break;
                case "F4":
                    triggerKeys.Add(Keys.F4);
                    break;
                case "F5":
                    triggerKeys.Add(Keys.F5);
                    break;
                case "F6":
                    triggerKeys.Add(Keys.F6);
                    break;
                case "F7":
                    triggerKeys.Add(Keys.F7);
                    break;
                case "F8":
                    triggerKeys.Add(Keys.F8);
                    break;
                case "F9":
                    triggerKeys.Add(Keys.F9);
                    break;
                case "F10":
                    triggerKeys.Add(Keys.F10);
                    break;
                case "F11":
                    triggerKeys.Add(Keys.F11);
                    break;
                case "F12":
                    triggerKeys.Add(Keys.F12);
                    break;
                default:
                    triggerKeys.AddRange(new[] {Keys.F1,Keys.F2,Keys.F3,Keys.F4,Keys.F5,Keys.F6,Keys.F7,Keys.F8,Keys.F9,Keys.F10,Keys.F11,Keys.F12});
                    break;
            }
            keyHandler = new KeyEventHandler(keyPressEvent);
        }
        public KeybordTimer(MainWindow window, string keyCode)
            : this(keyCode)
        {
            // TODO: Complete member initialization         
            window.KeyDown += keyHandler;
        }
        private void keyPressEvent(Object sender, KeyEventArgs e)
        {    
            if (triggerKeys.Contains(e.KeyData))
            {
                if (timer!=null)
                {
                    //MessageBox.Show(timer.Elapsed().ToString(@"hh\:mm\:ss\.ffff"));
                    this.RecordTime("DEFAULT", timer.Elapsed().ToString(@"hh\:mm\:ss\.ffff"));
                }
                else
                    MessageBox.Show("Race Has Not Started");
            }
        }

        
        private void keyEvent()
        {
            DialogResult results2 = MessageBox.Show("event triggered");
        }

        
        public override void StartRace()
        {
            if(timer==null)
                timer=new TobyTimer();            
        }

        public override void StopRace()
        {
            timer=null;
        }
        public override void StartRace(TimeSpan ts)
        {
            StartRace();
            OffsetTimer(ts);            
        }
        public override void ClearTimer()
        {
            timer.Reset();
        }
        public override TimeSpan GetCurrentTime()
        {
            if (timer == null)
                return new TimeSpan(0, 0, 0);
            return timer.Elapsed();
        }
        public override void OffsetTimer(TimeSpan ts)
        {
            timer.OffSetTime(ts);
        }

        public override void Dispose()
        {
            keyHandler = null;
        }
    }
}
