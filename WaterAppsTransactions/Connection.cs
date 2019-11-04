using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;


namespace WaterAppsTransactions
{
    public class Connection
    {

        private NpgsqlConnection _pgconn = null;
        private static NpgsqlCommand cmd = new NpgsqlCommand();
        //private Dataset ds;
        private NpgsqlDataAdapter da;
        private string conStr = "", dbname = "", dbhost = "", fcontent = "", dbusername = "", dbpass = "";
        private string _uname = "", _pword = "";

        public Connection(string username, string password)
        {
            _uname = username;
            _pword = password;
        }


        public Connection() {

            try {

                //
                if(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt"))
                {
                    using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt"))
                    {
                        string line;
                        int i = 1;
                        while ((line = sr.ReadLine()) != null)
                        {
                            switch (i)
                            {
                                case 1: dbname = line.ToString().Trim();
                                    break;
                                case 2: dbhost = line.ToString().Trim();
                                    break;
                                case 3: dbusername = line.ToString().Trim();
                                    break;
                                case 4: dbpass = line.ToString().Trim();
                                    break; 
                            }
                        }

                        conStr = String.Format("server={0};  user id = {1}; password = {2}; database = {3}", dbhost, dbusername, dbpass, dbname );
                    }
                }

            }
            catch (Exception e)
            {
                Library.WriteErrorLog(e);
            }


        }


        public void openConnection() {
               
            if(_pgconn == null || _pgconn.State == ConnectionState.Closed || _pgconn.State == ConnectionState.Broken)
            {
                _pgconn = new NpgsqlConnection(conStr.ToString());
                _pgconn.Open();

                if(_pgconn.State == ConnectionState.Open)
                {
                    MessageBox.Show("Connection Successful");
                }
                else
                {
                    Library.WriteErrorLog("Error establishing database connection");
                }
            }
        }

        public void CloseConnection()
        {
            if (_pgconn.State == ConnectionState.Open || _pgconn.State == ConnectionState.Closed || _pgconn.State == ConnectionState.Broken)
            {
           
                _pgconn.Close();
            }
        }

    }
}
