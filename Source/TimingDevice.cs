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
        //to be called when race starts
        public abstract void StartRace();
        //off set race clock
        public abstract void StartRace(TimeSpan ts);
        //end the race
        public abstract void StopRace();
        //reset time
        public abstract void ClearTimer();
        //what is the current value of clock
        public abstract TimeSpan GetCurrentTime();
        //offset the time
        public abstract void OffsetTimer(TimeSpan ts);
        //important to set what race to record data in db too
        public void SetRaceID(int id) { _raceID=id;}
        public List<Tuple<TimeSpan, String>> buffer= new List<Tuple<TimeSpan,string>>();
        //add a listener to be called when event happens
        public void AddListener(ITimerListener obs) { _listeners.Add(obs); }
        //remove a particular listener from call list
        public void Removeistener(ITimerListener obs) {
            try
            {
                _listeners.Remove(obs);
            }catch(Exception e){}
        }
        //remove listener from report list
        public void ClearListener(ITimerListener obs) { _listeners.Clear(); }
        //public void addToBuffer(TimeSpan time, String bib);
        public void ClearBuffer() {
            buffer.Clear();
        }
        //record bib and time to db
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
