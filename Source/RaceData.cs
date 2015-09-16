using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

/*
 * YAPT (Yet Another Push Test)
 * This is a test of the git push function.
 * Only comments were added.
 * Had this been a real push, actual MEANINGFUL changes would have been made.
 * Again, this is only a test.
 */

namespace TimingForToby
{
    public class RaceData
    {
        internal string ConnectionString;
        internal string RaceName;
        public Form StartWindow;
        public RaceData(){}
        public RaceData(Form StartScreen, string _RaceName, string _Connection)
        {
            this.StartWindow=StartScreen;
            this.RaceName=_RaceName;
            this.ConnectionString = _Connection;
        }
    }
}
