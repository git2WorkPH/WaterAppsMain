using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WaterAppsServices
{
    class ErrorLogging
    {

        public static void WriteErrorLog(Exception ex)
        {

            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine("\n" + DateTime.Now.ToString() + " : " + ex.StackTrace.ToString().Trim() + " : " + ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch
            {

            }
        }

        public static void WriteErrorLog(string message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine("\n" + DateTime.Now.ToString() + " : " + message.Trim());
                sw.Flush();
                sw.Close();
            }
            catch
            {

            }
        }
    }
}
