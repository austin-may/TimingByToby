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
        private SerialPort com;
        private static readonly string START_TX = "START OF RETRANSMIT";
        private static readonly string END_TX = "END OF RETRANSMIT";
        private bool retransmit = false;
        private List<String> recalled = new List<string>();
        private String currentString = "";
        private Dictionary<string, string> raceMap = new Dictionary<string, string>();

        public TimeMachineTimer() { }
        public TimeMachineTimer(String port)
        {
            ConnectToPort(port);
        }
        private void ConnectToPort(string comPort)
        {
            if (com != null)
            { com.Close(); com.Dispose(); }
            com = new SerialPort();

            // Allow the user to set the appropriate properties.
            com.PortName = comPort;
            com.BaudRate = 9600;
            com.Parity = Parity.None;
            com.DataBits = 8;
            com.StopBits = StopBits.One;
            com.Handshake = Handshake.None;

            // Set the read/write timeouts
            com.ReadTimeout = 500;
            com.WriteTimeout = 500;
            try
            {
                com.Open();
                com.DataReceived += new SerialDataReceivedEventHandler(responseHandler);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                com.Close(); com.Dispose();
            }
        }
        private void responseHandler(object sender, SerialDataReceivedEventArgs args)
        {
            ParseCom(com.ReadExisting());
        }

        private void ParseCom(string text)
        {            
            foreach (char c in text.ToCharArray())
            {
                // build the line
                currentString += c;
                //IF WE HIT THE END OF THE LINE
                if (currentString.Contains("\r\n"))
                {
                    currentString = currentString.Replace("\r\n", "");
                    recalled.Add(currentString);
                    if (currentString.Contains(START_TX))
                        retransmit = true;
                    else if (currentString.Contains(END_TX))
                        retransmit = false;
                    else if (retransmit)
                        ProcessRecalledData(currentString);
                    else
                        ProcessPassedData(currentString);
                    currentString = "";
                }
                //start of line
                else if (IsSpecialChar(c))
                {
                    currentString = "";
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
                if(!raceMap.ContainsKey(time))
                    raceMap.Add(time, bibString);
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
                if (!raceMap.ContainsKey(time))
                {
                    raceMap.Add(time, bibString);
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
    }
}
