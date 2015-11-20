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
        public static readonly string PATH = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)+"\\"+ Path.GetFileName(@"ErrorLog.txt");
        internal static void WriteToErrorLog(string message)
        {
            File.AppendAllText(@"ErrorLog.txt", message);
        }
        
        
    }
}
