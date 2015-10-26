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

        public KeybordTimer()
        {
        }
        public KeybordTimer(MainWindow window)
            : this()
        {
            // TODO: Complete member initialization         
            window.KeyPress += new KeyPressEventHandler(this.keyPressEvent);
        }
        private void keyPressEvent(Object sender, KeyPressEventArgs e)
        {    
            if (e.KeyChar == ' ')
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
    }
}
