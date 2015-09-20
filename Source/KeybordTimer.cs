using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace TimingForToby
{
    public class KeybordTimer : TimingDevice
    {
        private Stopwatch timer;

        public KeybordTimer()
        {
            timer = new Stopwatch();
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
                if(timer.IsRunning)
                    MessageBox.Show(timer.Elapsed.ToString(@"hh\:mm\:ss\.ffff"));
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
            timer.Start();
        }
        public override void StopRace()
        {
            timer.Stop();
        }
        public override void ClearTimer()
        {
            timer.Reset();
        }
    }
}
