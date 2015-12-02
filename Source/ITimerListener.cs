using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimingForToby
{
    public interface ITimerListener
    {
        //to be called when a runner crosses the finish
        void OnTime();
    }
}
