using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBuildHelper
{
   public class TxtLog
    {
        static string logPath = System.Windows.Forms.Application.StartupPath + @"\log.txt";
        public static void Log(string content) {
            
            if (!File.Exists(logPath))
            {
                FileInfo myfile = new FileInfo(logPath);
                FileStream fs = myfile.Create();
                fs.Close();
            }

           string m_time = System.DateTime.Now.ToString();

            StreamWriter sw = File.AppendText(logPath);
            sw.WriteLine(m_time + " " + content);
            sw.Flush();
            sw.Close();
        }

    }
}
