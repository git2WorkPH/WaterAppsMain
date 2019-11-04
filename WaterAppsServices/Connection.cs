using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WaterAppsServices
{
    class Connection
    {
        private string dbconn;
        private string strUser = "", strPwd = "";

        Connection()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory+"//config.ini"))
            {
                dbconn = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "//config.ini");


            }

        }


    }


}
