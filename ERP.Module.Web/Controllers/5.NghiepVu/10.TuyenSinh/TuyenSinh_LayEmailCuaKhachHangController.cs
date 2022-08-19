using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Web;
using ERP.Module.Enum.TuyenSinh;
using ERP.Module.HeThong;
using DevExpress.Xpo;
using ERP.Module.Extends;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_LayEmailCuaKhachHangController : ViewController
    {
        public TuyenSinh_LayEmailCuaKhachHangController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            Session session = ((XPObjectSpace)View.ObjectSpace).Session;
            //
            string emailnhan = Common.CauHinhChung_GetCauHinhChung.CauHinhMail.Email;
            string passnhan = Common.CauHinhChung_GetCauHinhChung.CauHinhMail.Password;
            string host = Common.CauHinhChung_GetCauHinhChung.CauHinhMail.Server;
            //
            if (!string.IsNullOrEmpty(emailnhan) && !string.IsNullOrEmpty(passnhan)
                && !string.IsNullOrEmpty(host))
            {
               bool sucess = MailHelpers.DownloadEmailsUsingPOP3(session, host, emailnhan, passnhan);
                if (sucess)
                {
                    string message = "alert('Thành công.')";
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                    //
                    View.Refresh();
                }
                else
                {
                    string message = "alert('Thất bại.')";
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                }
            }
            else
            {
                string message = "alert('Kiểm tra Host, Mail, Pass.')";
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
            }
        }

        private void TuyenSinh_LayEmailCuaKhachHangController_Activated(object sender, EventArgs e)
        {
            //
            simpleAction1.Active["TruyCap"] = Commons.Common.IsWriteGranted<MailManager>();
        }
    }
}
