using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Npgsql;
using System.Data;

namespace WaterAppsServices
{
    class Connection
    {
        private NpgsqlConnection _sqlCon = null;
        private static NpgsqlCommand cmd = new NpgsqlCommand();
        private DataSet ds;
        private NpgsqlDataAdapter da;
        private string conStr = "", dbname = "", dbhost = "", fcontent = "", dbusername = "", dbpass = "";


        public Connection()
        {
            try
            {

                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt"))
                {

                    fcontent = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt");
                    using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\config.txt"))
                    {
                        string line;
                        int i = 1;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (i == 1)
                            {
                                dbname = line.ToString().Trim();
                            }
                            else if (i == 2)
                            {
                                dbhost = line.ToString().Trim();
                            }
                            else if (i == 3)
                            {
                                dbusername = line.ToString().Trim();
                            }
                            else if (i == 4)
                            {
                                dbpass = line.ToString().Trim();
                            }
                            i++;
                        }
                    }
                }


                EncryptDecryptEngine _objED = new EncryptDecryptEngine();

                conStr = String.Format("server={0};user id={1}; password={2};" +
                              "database=" + _objED.Decrypt(dbname.ToString().Trim(), true) + "; pooling=false", "" + _objED.Decrypt(dbhost.ToString().Trim(), true) + "",
                          "" + _objED.Decrypt(dbusername.ToString().Trim(), true) + "", "" + _objED.Decrypt(dbpass.ToString().Trim(), true) + "");

            }
            catch (Exception pcex)
            {
                ErrorLogging.WriteErrorLog(pcex);
            }

        }

        public void openConnection()
        {
            if (_sqlCon == null || _sqlCon.State == ConnectionState.Closed || _sqlCon.State == ConnectionState.Broken)
            {

                _sqlCon = new NpgsqlConnection(conStr.ToString());
                _sqlCon.Open();
            }
        }

        public void closeConnection()
        {
            if (_sqlCon.State == ConnectionState.Open || _sqlCon.State == ConnectionState.Broken || _sqlCon.State == ConnectionState.Closed)
            {
                _sqlCon.Close();
            }
        }

        public DataSet GetDataSet(String cmdText, CommandType cmdType, NpgsqlParameter[] parameters)
        {
            ds = null;
            cmd = null;
            da = null;
            try
            {
                openConnection();
                using (_sqlCon)
                {
                    using (cmd = new NpgsqlCommand(cmdText, _sqlCon))
                    {
                        cmd.CommandType = cmdType;
                        if (parameters != null)
                        {
                            foreach (NpgsqlParameter parameter in parameters)
                            {
                                if (null != parameter) cmd.Parameters.Add(parameter);
                            }
                        }
                        using (da = new NpgsqlDataAdapter(cmd))
                        {
                            ds = new DataSet();
                            da.Fill(ds);

                            return ds;
                        }
                    }
                }
            }
            catch (Exception pcex)
            {
                ErrorLogging.WriteErrorLog(pcex);
                return null;
            }
            finally
            {
                if (ds != null) { ds.Dispose(); }
                if (da != null) { da.Dispose(); }
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
                closeConnection();
            }
        }

        public int ExecuteNonQuery(String cmdText, CommandType cmdType, NpgsqlParameter[] parameters)
        {
            cmd = null;
            try
            {
                openConnection();
                using (_sqlCon)
                {
                    cmd = new NpgsqlCommand(cmdText, _sqlCon);
                    cmd.CommandType = cmdType;
                    if (parameters != null)
                    {
                        foreach (NpgsqlParameter parameter in parameters)
                        {
                            if (null != parameter) cmd.Parameters.Add(parameter);
                        }
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception pcex)
            {
                ErrorLogging.WriteErrorLog(pcex);
                return 0;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
                closeConnection();
            }
        }

        public object ExecuteScalar(String cmdText, CommandType cmdType, NpgsqlParameter[] parameters)
        {
            cmd = null;
            try
            {
                openConnection();
                using (_sqlCon)
                {
                    cmd = new NpgsqlCommand(cmdText, _sqlCon);
                    cmd.CommandType = cmdType;

                    if (parameters != null)
                    {
                        foreach (NpgsqlParameter parameter in parameters)
                        {
                            if (null != parameter) cmd.Parameters.Add(parameter);
                        }
                    }
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception pcex)
            {
                ErrorLogging.WriteErrorLog(pcex);
                return 0;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
                closeConnection();
            }
        }
    }
}
