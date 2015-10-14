using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimingForToby
{
    public abstract class TimingDevice
    {
        private int _RaceID=-1;
        private List<Object> listeners=new List<object>();
        public abstract void StartRace();
        public abstract void StopRace();
        public abstract void ClearTimer();
        public void SetRaceID(int id) { _RaceID=id;}
        public List<Tuple<TimeSpan, String>> buffer= new List<Tuple<TimeSpan,string>>();
        public void addListener(ITimerListener obs) { listeners.Add(obs); }
        public void Removeistener(ITimerListener obs) {
            try
            {
                listeners.Remove(obs);
            }catch(Exception e){}
        }
        public void ClearListener(ITimerListener obs) { listeners.Clear(); }
        //public void addToBuffer(TimeSpan time, String bib);
        public void clearBuffer() {
            buffer.Clear();
        }
        public void RecordTime(string bib, string time)
        {
            if (_RaceID < 0)
            {
                throw new Exception("No Race set!");
            }
            CommonSQL.AddTimeAndBib(_RaceID, bib, time);
            foreach(ITimerListener obs in listeners){
                obs.OnTime();
            }
        }
    }
}
