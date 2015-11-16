using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace TimingForToby
{
    class TimeMachineTimer: TimingDevice
    {
        private SerialPort _com;
        private static readonly string START_TX = "START OF RETRANSMIT";
        private static readonly string END_TX = "END OF RETRANSMIT";
        private bool _retransmit = false;
        private List<String> _recalled = new List<string>();
        private String _currentString = "";
        private Dictionary<string, string> _raceMap = new Dictionary<string, string>();

        public TimeMachineTimer() { }
        public TimeMachineTimer(String port)
        {
            ConnectToPort(port);
        }
        private void ConnectToPort(string comPort)
        {
            if (_com != null)
            { _com.Close(); _com.Dispose(); }
            _com = new SerialPort();

            // Allow the user to set the appropriate properties.
            _com.PortName = comPort;
            _com.BaudRate = 9600;
            _com.Parity = Parity.None;
            _com.DataBits = 8;
            _com.StopBits = StopBits.One;
            _com.Handshake = Handshake.None;

            // Set the read/write timeouts
            _com.ReadTimeout = 500;
            _com.WriteTimeout = 500;
            try
            {
                _com.Open();
                _com.DataReceived += new SerialDataReceivedEventHandler(responseHandler);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                _com.Close(); _com.Dispose();
            }
        }
        private void responseHandler(object sender, SerialDataReceivedEventArgs args)
        {
            ParseCom(_com.ReadExisting());
        }

        private void ParseCom(string text)
        {            
            foreach (char c in text.ToCharArray())
            {
                // build the line
                _currentString += c;
                //IF WE HIT THE END OF THE LINE
                if (_currentString.Contains("\r\n"))
                {
                    _currentString = _currentString.Replace("\r\n", "");
                    _recalled.Add(_currentString);
                    if (_currentString.Contains(START_TX))
                        _retransmit = true;
                    else if (_currentString.Contains(END_TX))
                        _retransmit = false;
                    else if (_retransmit)
                        ProcessRecalledData(_currentString);
                    else
                        ProcessPassedData(_currentString);
                    _currentString = "";
                }
                //start of line
                else if (IsSpecialChar(c))
                {
                    _currentString = "";
                }
            }
        }
        private void ProcessPassedData(string text)
        {
            var test = text.Split(' ');
            if (test.Length > 3)
            {
                string bibString = "DEFAULT";
                var time = test[3];
                if (test.Length > 6)
                    bibString = test[6];
                if(!_raceMap.ContainsKey(time))
                    _raceMap.Add(time, bibString);
                this.RecordTime(bibString, time);
            }
        }
        private void ProcessRecalledData(string text)
        {
            var test = text.Split(' ');
            if (test.Length > 3)
            {
                string bibString = "DEFAULT";
                var time = test[3];
                if (test.Length > 6)
                    bibString = test[6];
                if (!_raceMap.ContainsKey(time))
                {
                    _raceMap.Add(time, bibString);
                    this.RecordTime(bibString, time);
                }
            }
        }
        private bool IsSpecialChar(char c)
        {
            return (c == 1 || c == 4 || c == 20 || c == 23);
        }
        //I know, this is kinda bad, but I didnt figure out how to send commands so is has to do for now
        public override void StartRace()
        {
        }

        public override void StopRace()
        {
        }

        public override void ClearTimer()
        {
        }

        public override void OffsetTimer(TimeSpan ts)
        {
        }

        public override void StartRace(TimeSpan ts)
        {
            StartRace();
        }

        public override TimeSpan GetCurrentTime()
        {
            return TimeSpan.Zero;
        }

        public override void Dispose()
        {
            _com.Close();
            _com.Dispose();
        }
    }
}
