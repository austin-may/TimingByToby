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
        private TobyTimer _timer;
        private List<Keys> _triggerKeys=new List<Keys>();
        public KeyEventHandler keyHandler;

        public KeybordTimer(string keyCode)
        {
            switch(keyCode){
                case "F1":
                    _triggerKeys.Add(Keys.F1);
                    break;
                case "F2":
                    _triggerKeys.Add(Keys.F2);
                    break;
                case "F3":
                    _triggerKeys.Add(Keys.F3);
                    break;
                case "F4":
                    _triggerKeys.Add(Keys.F4);
                    break;
                case "F5":
                    _triggerKeys.Add(Keys.F5);
                    break;
                case "F6":
                    _triggerKeys.Add(Keys.F6);
                    break;
                case "F7":
                    _triggerKeys.Add(Keys.F7);
                    break;
                case "F8":
                    _triggerKeys.Add(Keys.F8);
                    break;
                case "F9":
                    _triggerKeys.Add(Keys.F9);
                    break;
                case "F10":
                    _triggerKeys.Add(Keys.F10);
                    break;
                case "F11":
                    _triggerKeys.Add(Keys.F11);
                    break;
                case "F12":
                    _triggerKeys.Add(Keys.F12);
                    break;
                default:
                    _triggerKeys.AddRange(new[] {Keys.F1,Keys.F2,Keys.F3,Keys.F4,Keys.F5,Keys.F6,Keys.F7,Keys.F8,Keys.F9,Keys.F10,Keys.F11,Keys.F12});
                    break;
            }
            keyHandler = new KeyEventHandler(KeyPressEvent);
        }
        
        private void KeyPressEvent(Object sender, KeyEventArgs e)
        {    
            if (_triggerKeys.Contains(e.KeyData))
            {
                if (_timer!=null)
                {
                    this.RecordTime("DEFAULT", _timer.Elapsed().ToString(@"hh\:mm\:ss\.ffff"));
                }
                else
                    MessageBox.Show("Race Has Not Started");
            }
        }
        
        public override void StartRace()
        {
            if(_timer==null)
                _timer=new TobyTimer();            
        }

        public override void StopRace()
        {
            _timer=null;
        }
        public override void StartRace(TimeSpan ts)
        {
            StartRace();
            OffsetTimer(ts);            
        }
        public override void ClearTimer()
        {
            _timer.Reset();
        }
        public override TimeSpan GetCurrentTime()
        {
            if (_timer == null)
                return new TimeSpan(0, 0, 0);
            return _timer.Elapsed();
        }
        public override void OffsetTimer(TimeSpan ts)
        {
            _timer.OffSetTime(ts);
        }

        public override void Dispose()
        {
            keyHandler = null;
        }
    }
}
