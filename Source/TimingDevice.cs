using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimingForToby
{
    abstract class TimingDevice
    {
        private List<Object> listeners=new List<object>();
        public List<Tuple<TimeSpan, String>> buffer= new List<Tuple<TimeSpan,string>>(); 
        public void addListener(Object obj);
        abstract void addToBuffer(TimeSpan time, String bib);
        public void clearBuffer() {
            buffer.Clear();
        }
    }
}
