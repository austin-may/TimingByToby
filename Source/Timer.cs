using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimingForToby
{
    class TobyTimer
    {
        private DateTime _StartTime;
        public TobyTimer()
        {
            _StartTime = DateTime.Now;
        }
        public TobyTimer(DateTime dt)
        {
            _StartTime = dt;
        }
        public void Reset()
        {
            _StartTime = DateTime.Now;
        }
        public TimeSpan Elapsed()
        {
            if (_StartTime == null)
                return new TimeSpan(0, 0, 0);
            return DateTime.Now - _StartTime;
        }
        public void OffSetTime(TimeSpan ts)
        {
            _StartTime = DateTime.Now - ts;
        }
    }
}
