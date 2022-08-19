using System;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using ERP.Module.Commons;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraEditors;

namespace ERP.Win {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
#if EASYTEST
            DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif
          
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Set cấu hình ngôn ngữ
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Config.Languages);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Config.Languages);
            //
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;
            Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath;
            Tracing.Initialize();
            ERPWindowsFormsApplication winApplication = new ERPWindowsFormsApplication();
            //Gọi chuỗi kết nối
            winApplication.ConnectionString =  DataProvider.GetConnectionString();

            SqlConnection cnn = new SqlConnection(DataProvider.GetConnectionString());
            if (cnn.State == ConnectionState.Closed)
            {
                try
                {
                    cnn.Open();  
                }
                catch (Exception)
                {
                    XtraMessageBox.Show("Không thể kết nối đến máy chủ\nVui lòng liên hệ admin để được hỗ trợ", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return ;
                }
            }

            //Cập nhật databse
            winApplication.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;

            //Code để mở ScriptRecorderController (bắt buộc)
            DevExpress.ExpressApp.ScriptRecorder.ScriptRecorderControllerBase.ScriptRecorderEnabled = true;

            //
            try
         {                               
                //Lấy đường dẫn khi chạy phần mền
                Config.StartupPath = Application.StartupPath;
                
                winApplication.Setup();
                // Đăng ký interface.
                //Register.RegisterAll();
                //
                winApplication.Start();
            }
            catch (Exception e)
            {
                winApplication.HandleException(e);
            }
        }

    }
}

