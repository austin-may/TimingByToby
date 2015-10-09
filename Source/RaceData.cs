using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace TimingForToby
{
    public class RaceData
    {
        internal string ConnectionString;
        internal string RaceName;
        public readonly int RaceID;
        public Form StartWindow;
        public RaceData(){}
        public RaceData(Form StartScreen, string _RaceName, string _Connection)
        {
            this.StartWindow=StartScreen;
            this.RaceName=_RaceName;
            this.ConnectionString = _Connection;
            RaceID = CommonSQL.GetRaceID(_RaceName);
        }

    }
}
