using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using ERP.Module.Enum.NhanSu;
using ERP.Module.Enum.Systems;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ERP.Module.Commons
{
    public static class DataProvider
    {
        public static readonly string _connectionString;

        //string computerName = System.Environment.UserName;
        public static  string myPath = @"C:\\ConfigERP";// @"C:\\Users\\" + System.Environment.UserName + "\\AppData\\Local\\Apps\\2.0\\ConfigERP";
        public static string link = "";

        static DataProvider()
        {
            _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        /// <summary>
        /// Get DataTable from excel file
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable GetDataTableFromExcel(string excelFilePath, string tableName, LoaiOfficeEnum loaiOffice)
        {

            OleDbDataReader abc= System.Data.OleDb.OleDbEnumerator.GetRootEnumerator();
            string connectionString = string.Empty;
            if (loaiOffice == LoaiOfficeEnum.Office2003)
            {
                connectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", excelFilePath);
            }
            else
            {
                //Excel 2007-2010 Workbook (.xlsx)  
                connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0", excelFilePath);

                //connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0;", excelFilePath);
                //Excel 2007-2010 Macro-enabled workbook (.xlsm)  
                //connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0 Macro;HDR=YES;", excelFilePath);
            }
            //
            using (OleDbConnection cnn = new OleDbConnection(connectionString))
            {
                //
                //cnn.CreateCommand().CommandTimeout = 5000;]
                var a = cnn.ConnectionTimeout;
                string query = String.Format("Select * from {0}", tableName);
                using (OleDbDataAdapter da = new OleDbDataAdapter(query, cnn))
                {
                    da.SelectCommand.CommandTimeout = 10000;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        /// Get DataTable from SQL Server
        /// </summary>
        /// <param name="session"></param>
        /// <param name="query"></param>
        /// <param name="type"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataTable GetDataTableFromSQL(SqlConnection cnn, string query, CommandType type, params SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = GetCommand(query, type, param);
            cmd.Connection = cnn;
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }
            return dt;
        }

        /// <summary>
        /// Get DataTable from SQL Server
        /// </summary>
        /// <param name="session"></param>
        /// <param name="query"></param>
        /// <param name="type"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string query, CommandType type, params SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            try
            {
               
                using (SqlCommand cmd = GetCommand(query, type, param))
                {
                    cmd.Connection = GetConnection();
                    cmd.CommandTimeout = 5000;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    return dt;
                }
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        public static DataSet GetDataSetWithParam(string sql, CommandType type, SqlParameter[] param)
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd =GetCommand(sql, type, param))
            {
                cmd.Connection = GetConnection();
                cmd.CommandTimeout = 5000;
                //cmd.CommandType = type;
                //foreach (var item in param)
                //{
                //    cmd.Parameters.AddWithValue(item.ParameterName, item.Value);
                //}
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds);
                }
                cmd.ExecuteReader();
                return ds;
            }
        }
        /// <summary>
        /// Get DataTable from SQL Server
        /// </summary>
        /// <param name="session"></param>
        /// <param name="query"></param>
        /// <param name="type"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataTable GetDataTableWithDatabase(string query, CommandType type, string nameDatabase,params SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            try
            {

                using (SqlCommand cmd = GetCommand(query, type, param))
                {
                    cmd.Connection = GetConnection(nameDatabase);
                    cmd.CommandTimeout = 5000;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    return dt;
                }
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        /// <summary>
        /// Execute Non Query
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static DataTable ExecuteNonQueryReturnTable(SqlConnection cnn, SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = cnn;
                cmd.CommandTimeout = 5000;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        /// <summary>
        /// Get DataTable from SQL Server
        /// </summary>
        /// <param name="session"></param>
        /// <param name="query"></param>
        /// <param name="type"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(SqlCommand cmd)
        {
            DataSet ds = new DataSet();
            if (cmd != null)
            {
                try
                {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        cmd.Connection = GetConnection();
                        cmd.CommandTimeout = 5000;
                        da.Fill(ds);
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Lỗi xử lý dữ liệu báo cáo từ store : {0}\r\n{1}", cmd.CommandText, ex.Message));
                }
            }

            return ds;
        }

        /// <summary>
        /// Execute Non Query
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlConnection cnn, SqlCommand cmd)
        {
            try
            {
                cmd.Connection = cnn;
                cmd.CommandTimeout = 5000;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        /// <summary>
        /// Execute Non Query
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string query, CommandType type, params SqlParameter[] args)
        {   
            try
            {
                SqlCommand cmd = GetCommand(query, type, args);
                cmd.Connection = GetConnection();
                cmd.CommandTimeout = 5000;         
                //
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
       

        /// <summary>
        /// Execute Non Query
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static bool ExecuteNonQuery_ReturnValue(string query, CommandType type, params SqlParameter[] args)
        {
            bool value = false;
            try
            {
                //Khởi tạo param trả về
                SqlParameter prmReturnValue = new SqlParameter("@ReturnValue", SqlDbType.Bit, 10);
                prmReturnValue.Direction = ParameterDirection.Output;

                //
                SqlCommand cmd = GetCommand(query, type, args);
                cmd.Parameters.Add(prmReturnValue);
                cmd.Connection = GetConnection();
                cmd.CommandTimeout = 5000;         

                //
                cmd.ExecuteNonQuery();
                //
                value = Convert.ToBoolean(prmReturnValue.Value);
            }
            catch (Exception ex)
            {
                return value;
            }
            //
            return value;
        }

        /// <summary>
        /// Get sql command
        /// </summary>
        /// <param name="query"></param>
        /// <param name="type"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static SqlCommand GetCommand(string query, CommandType type, params SqlParameter[] param)
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.CommandType = type;
                cmd.CommandTimeout = 5000;         

                if (param != null)
                    cmd.Parameters.AddRange(param);

                return cmd;
            }
        }

        /// <summary>
        /// Get sql connection
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {
            string connectionString = GetConnectionString();
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                if (cnn.State != ConnectionState.Open)
                    cnn.Open();
                return cnn;
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể kết nối đến máy chủ. Vui lòng liên hệ admin để được hỗ trợ", "Thông báo");
                return null;
            }
        }
        /// <summary>
        /// Get sql connection
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection(string nameDatabase)
        {
            string connectionString = GetConnectionStringOrther(nameDatabase);
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                if (cnn.State != ConnectionState.Open)
                    cnn.Open();
                return cnn;
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể kết nối đến máy chủ. Vui lòng liên hệ admin để được hỗ trợ", "Thông báo");
                return null;
            }
        }
        public static string System_GetConnectionString()
        {
            string connectionString = "";
            //string path = "";

            string Username = "",
                    Password = "";
            string url = "";
            url += _connectionString;
            WebClient client = new WebClient();
            client.Credentials = new NetworkCredential(Username, Password);

            client.Dispose();

            #region Download thử
            try
            {
                //client.DownloadFile(url, myPath); //Download file mới//Trường hợp không kết nối FTP được vẫn chạy tiếp vs file trên local

                #region Xử lý file cũ 
                if (Directory.Exists(myPath) == false)
                {
                    Directory.CreateDirectory(myPath+"\\");
                }
                else
                {
                    File.Delete(myPath + "\\" + _connectionString);//Xóa file cũ
                }
                #endregion
                 link = myPath + "\\" + _connectionString;

                #region Download
                try
                {
                    client.DownloadFile(url, link); //Download file mới//Trường hợp không kết nối FTP được vẫn chạy tiếp vs file trên local
                }
                catch (Exception)
                {
                    link = "";
                }
                #endregion
            }
            catch (Exception)
            {

            }
            #endregion
            return connectionString;
        }

        /// <summary>
        /// Lấy chuỗi kết nối
        /// </summary>
        /// <param name="filename">đường dẫn</param>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            bool KetQuaMoi = false;
            if (string.IsNullOrEmpty(_connectionString)) return string.Empty;

            // Lấy chuỗi kết nối đầy đủ
            string connectionStringFull = String.Empty;

            if (_connectionString == "PSC_HRM.bin")
                connectionStringFull = System_GetConnectionString();
            //
            if (Config.TypeApplication.Equals("WinForm"))
            {
                if (_connectionString == "PSC_HRM.bin" && connectionStringFull!="")
                {
                    KetQuaMoi = false;
                    try
                    {
                        connectionStringFull = String.Format(link);
                    }
                    catch (Exception)
                    {
                        connectionStringFull = "";
                    }
                    if (connectionStringFull == "")
                    {
                        string StartupPath = Application.StartupPath.Replace("\\", "\\\\");
                        connectionStringFull = String.Format(@"{0}\Configs\{1}\{2}", StartupPath, Config.CompanyKey, _connectionString);
                    }
                }
                else
                {
                    KetQuaMoi = false;
                    connectionStringFull = String.Format(@"{0}\Configs\{1}\{2}", Application.StartupPath, Config.CompanyKey, _connectionString);
                }
            }
            else
            {
                // if (connectionStringFull == "")
                KetQuaMoi = false;
                connectionStringFull = String.Format(@"{0}Configs\{1}\{2}", AppDomain.CurrentDomain.BaseDirectory, Config.CompanyKey, _connectionString);
            }
            //
            string connectionString = "";
            if (!KetQuaMoi)
            {
                try
                {
                    using (FileStream stream = File.Open(connectionStringFull, FileMode.Open, FileAccess.Read, FileShare.None))
                    {

                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            string temp = reader.ReadString();

                            if (!String.IsNullOrEmpty(temp))
                            {
                                connectionString = EncryptUtils.Decrypt(temp, Config.DecryptKey, true);
                                return connectionString;
                            }
                            return string.Empty;
                        }
                    }
                }
                catch (Exception)
                {
                    //connectionStringFull = String.Format(@"{0}\Configs\{1}\{2}", Application.StartupPath, Config.CompanyKey, _connectionString);
                    //using (FileStream stream = File.Open(connectionStringFull, FileMode.Open, FileAccess.Read, FileShare.None))
                    //{

                    //    using (BinaryReader reader = new BinaryReader(stream))
                    //    {
                    //        string temp = reader.ReadString();

                    //        if (!String.IsNullOrEmpty(temp))
                    //        {
                    //            connectionString = EncryptUtils.Decrypt(temp, Config.DecryptKey, true);
                    //            return connectionString;
                    //        }
                    //        return string.Empty;
                    //    }
                    //}
                }
            }
            else
                connectionString = EncryptUtils.Decrypt(connectionStringFull, Config.DecryptKey, true);
            return connectionString;
        }
        /// <summary>
        /// Lấy chuỗi kết nối
        /// </summary>
        /// <param name="filename">đường dẫn</param>
        /// <returns></returns>
        public static string GetConnectionStringOrther(string nameDatabase)
        {
            string connectionString = string.Empty;
            //Học phí
            if (nameDatabase.Equals("AccountsFee"))
                connectionString = ConfigurationManager.AppSettings["ConnectionString_AccountsFee"];
            else if(nameDatabase.Equals("SIS")) // Học sinh
                connectionString = ConfigurationManager.AppSettings["ConnectionString_SIS"];


            // Lấy chuỗi kết nối đầy đủ
            string connectionStringFull = String.Empty;
            //
            if (Config.TypeApplication.Equals("WinForm"))
            {
                connectionStringFull = String.Format(@"{0}\Configs\{1}\{2}", Application.StartupPath, Config.CompanyKey, connectionString);
            }
            else
            {
                connectionStringFull = String.Format(@"{0}Configs\{1}\{2}", AppDomain.CurrentDomain.BaseDirectory, Config.CompanyKey, connectionString);
            }
            //
            using (FileStream stream = File.Open(connectionStringFull, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    string temp = reader.ReadString();

                    if (!String.IsNullOrEmpty(temp))
                    {
                        String connectionStringDecrypt = EncryptUtils.Decrypt(temp, Config.DecryptKey, true);
                        //
                        return connectionStringDecrypt;
                    }
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Lấy list oid from database
        /// </summary>
        /// <param name="filename">đường dẫn</param>
        /// <returns></returns>
        public static List<Guid> GetGuidList(string query, CommandType type, params SqlParameter[] param)
        {
            //
            List<Guid> guidList = new List<Guid>();
            //
            DataTable dt = new DataTable();
            //
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.CommandType = type;
                if (param != null)
                    cmd.Parameters.AddRange(param);
                //
                cmd.Connection = GetConnection();
                //
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                    //
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                            guidList.Add(new Guid(dr["Oid"].ToString()));
                    }
                }
            }
            //
            return guidList;
        }

        /// <summary>
        /// Get value from SQL Server
        /// </summary>
        /// <param name="session"></param>
        /// <param name="query"></param>
        /// <param name="type"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object GetValueFromDatabase(string query, CommandType type, params SqlParameter[] param)
        {
            using (SqlCommand cmd = GetCommand(query, type, param))
            {
                cmd.CommandTimeout = 10000;
                cmd.Connection = GetConnection();
                object obj = cmd.ExecuteScalar();
                return obj;
            }
        }

        public static string GetConnectionString(string path)
        {
            FileStream stream = File.Open(String.Format(@"{0}Configs\{1}\{2}", AppDomain.CurrentDomain.BaseDirectory, Config.CompanyKey, path), FileMode.Open, FileAccess.Read, FileShare.None);
            BinaryReader reader = new BinaryReader(stream);

            string temp = reader.ReadString();
            reader.Close();
            stream.Close();
            stream.Dispose();

            if (!String.IsNullOrEmpty(temp))
            {
                return EncryptUtils.Decrypt(temp, Config.DecryptKey, true);
            }
            return "";
        }


    }
}
