using System;
using System.Globalization;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Model;
using DevExpress.Web;
using DevExpress.ExpressApp.Xpo;
using System.Drawing;
using ERP.Module.Commons;
using DevExpress.Xpo;
using ERP.Module.HeThong;
using ERP.Module.Extends;
using ERP.Module.Enum.Systems;
using DevExpress.Data.Filtering;
//...
namespace ERP.Module.Web.Editors
{
    [PropertyEditor(typeof(String), false)]
    public class Button_LayXacThuc : ASPxPropertyEditor
    {
        LogonParameters_Custom _logonParameters;
        ASPxButton _layXacThuc = null;
        //
        public Button_LayXacThuc( Type objectType, IModelMemberViewItem info) : base(objectType, info) { }
       
        protected override void SetupControl(WebControl control)
        {
            
        }
        protected override WebControl CreateEditModeControlCore()
        {
            _layXacThuc = RenderHelper.CreateASPxButton();
            //
            _layXacThuc.Text = "Lấy mã";
            _layXacThuc.Visible = true;
            _layXacThuc.Click += LayXacThuc_Click;
            //
            return _layXacThuc;
        }

        void LayXacThuc_Click(object sender, EventArgs e)
        {
            _logonParameters = View.CurrentObject as LogonParameters_Custom;
            //
            if (_logonParameters != null)
            {
                if (_logonParameters.LoaiXacThuc == LoaiXacThucEnum.Email)
                {
                    //
                    sendEmail();
                }
                else
                {
                    //
                    sendSMS();
                }
            }

        }

        void sendEmail()
        {
            if (Common.DaGuiXacThuc == true)
                return;

            //
            IObjectSpace obs = (WebApplication.Instance).CreateObjectSpace();
            Session ses = ((XPObjectSpace)obs).Session;
            SecuritySystemUser_Custom user = ses.FindObject<SecuritySystemUser_Custom>(CriteriaOperator.Parse("UserName =?", _logonParameters.UserName));
            if (user == null || user.ThongTinNhanVien == null)
            {
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Sai thông tin tài khoản!')");
                return;
            }
            else
            {
                #region Gửi email
                string code = (Guid.NewGuid()).ToString().Substring(0, 8);

                //Cập nhật mã xác thực xuống db
                user.MaXacThuc = code;
                user.NgayXacThuc = Common.GetServerCurrentTime();

                //Gửi mã xác thực qua mail 
                if (user.ThongTinNhanVien.Email != null)
                {
                    string emailNhan = user.ThongTinNhanVien.Email;

                    bool success = true;
                    //
                    int port = user.CongTy.CauHinhChung.CauHinhMail.Port;
                    string server = user.CongTy.CauHinhChung.CauHinhMail.Server;
                    string emailgui = user.CongTy.CauHinhChung.CauHinhMail.Email;
                    string passgui = user.CongTy.CauHinhChung.CauHinhMail.Password;
                    //
                    success = MailHelpers.SendMail(port, server, "Mã xác thực", code, string.Empty, emailgui, passgui, emailNhan);
                    //
                    if (!success)
                    {
                        string message = "alert('Gửi mã không thành công.')";
                        WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                    }
                    else
                    {
                        //Cập nhật _logonParameters
                        _logonParameters.LoaiXacThuc = LoaiXacThucEnum.Email;
                        //
                        Common.DaGuiXacThuc = true;
                        //
                        obs.CommitChanges();
                        //
                        string message = "alert('Gửi mã thành công.')";
                        WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                    }
                }
                #endregion
            }
        }

        void sendSMS()
        {
            if (Common.DaGuiXacThuc == true)
                return;
            //
            IObjectSpace obs = (WebApplication.Instance).CreateObjectSpace();
            //
            Session ses = ((XPObjectSpace)obs).Session;
            SecuritySystemUser_Custom user = ses.FindObject<SecuritySystemUser_Custom>(CriteriaOperator.Parse("UserName =?", _logonParameters.UserName));
            if (user == null || user.ThongTinNhanVien == null)
            {
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Sai thông tin tài khoản!')");
                return;
            }
            else
            {
                #region Gửi sms
                //
                if (user.ThongTinNhanVien.DienThoaiDiDong != null)
                {
                    string receiver = user.ThongTinNhanVien.DienThoaiDiDong;
                    bool daGui = SmsOTP.RequestOTP(receiver);

                    //Cập nhật thông tin
                    if (daGui)
                    {
                        _logonParameters.LoaiXacThuc = LoaiXacThucEnum.SMS;
                        //
                        Common.DaGuiXacThuc = true;
                    }
                }
                #endregion
            }
        }
    }
}