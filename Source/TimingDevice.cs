using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimingForToby
{
    public abstract class TimingDevice: IDisposable
    {
        private int _raceID=-1;
        private List<Object> _listeners=new List<object>();
        public abstract void StartRace();
        public abstract void StartRace(TimeSpan ts);
        public abstract void StopRace();
        public abstract void ClearTimer();
        public abstract TimeSpan GetCurrentTime();
        public abstract void OffsetTimer(TimeSpan ts);
        public void SetRaceID(int id) { _raceID=id;}
        public List<Tuple<TimeSpan, String>> buffer= new List<Tuple<TimeSpan,string>>();
        public void AddListener(ITimerListener obs) { _listeners.Add(obs); }
        public void Removeistener(ITimerListener obs) {
            try
            {
                _listeners.Remove(obs);
            }catch(Exception e){}
        }
        public void ClearListener(ITimerListener obs) { _listeners.Clear(); }
        //public void addToBuffer(TimeSpan time, String bib);
        public void ClearBuffer() {
            buffer.Clear();
        }
        public void RecordTime(string bib, string time)
        {
            if (_raceID < 0)
            {
                throw new Exception("No Race set!");
            }
            CommonSQL.AddTimeAndBib(_raceID, bib, time);
            foreach(ITimerListener obs in _listeners){
                obs.OnTime();
            }
        }
        public abstract void Dispose();
    }
}
