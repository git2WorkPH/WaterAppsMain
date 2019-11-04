using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAppsTransactions
{
    public class Library
    {

        public static void WriteErrorLog(Exception e)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFiles.txt", true);
                sw.WriteLine("\n" + DateTime.Now.ToString() + " : " + e.StackTrace.ToString().Trim() + " : " + e.Message.ToString().Trim());
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
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFiles.txt", true);
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
