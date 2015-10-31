using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TimingForToby
{
    public class LogFile
    {     
        internal static void WriteToErrorLog(string message)
        {
            File.AppendAllText(@"..\..\ErrorLog.txt", message);
        }
        
        
    }
}
