using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimingForToby
{
    public abstract class TimingDevice
    {
        private List<Object> listeners=new List<object>();
        public abstract void StartRace();
        public abstract void StopRace();
        public abstract void ClearTimer();
        public List<Tuple<TimeSpan, String>> buffer= new List<Tuple<TimeSpan,string>>(); 
        //public void addListener(Object obj);
        //public void addToBuffer(TimeSpan time, String bib);
        public void clearBuffer() {
            buffer.Clear();
        }
    }
}
