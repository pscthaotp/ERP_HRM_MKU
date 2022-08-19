using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.Web;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using ERP.Module.HeThong;
using ERP.Module.Extends;
using ERP.Module.Enum.Systems;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Web;

namespace ERP.Module.Web.Controllers.HeThong
{
    public partial class HeThong_LogonController : ViewController
    {
        LogonParameters_Custom _logonParameters;

        public HeThong_LogonController()
        {
            InitializeComponent();
            RegisterActions(components);
            //
            TargetObjectType = typeof(LogonParameters_Custom);
        }

        protected override void OnViewControlsCreated()
        {
            DetailView view = View as DetailView;
            //
            if (view != null)
            {
                //Lấy view hiện tại
                _logonParameters = View.CurrentObject as LogonParameters_Custom;

                if (_logonParameters != null)
                {
                    //Tìm control Captcha đã tạo trong model
                    ControlViewItem captcha = ((DetailView)View).FindItem("Captcha") as ControlViewItem;
                    if (captcha != null)
                    {
                        //Khởi tạo định dạng Captcha
                        if (captcha.Control != null)
                        {
                            Captcha_ControlCreated(captcha, EventArgs.Empty);
                        }
                        else
                        {
                            captcha.ControlCreated += Captcha_ControlCreated;
                        }
                    }
                }
            }
        }

        void Captcha_ControlCreated(object sender, EventArgs e)
        {
            ASPxCaptcha captcha = ((ControlViewItem)sender).Control as ASPxCaptcha;
            captcha.TextBox.LabelText = "Mã Captcha";
            captcha.RefreshButton.Text = "Đổi hình";
            captcha.ValidationSettings.ErrorText = "";
            //Nếu nhập sai pass 3 lần thì mới hiện captcha
            //Lưu ý: đã sửa lại số lần login_max mặc định của dev lại thành 10 ở HRM4.Web.WebApplication dòng 121
            if (AuthenticationStandard_CustomWeb._Erorr > 1)
            {
                captcha.Visible = true;
                LogonParameters_Custom._Captcha = captcha;
            }
            else
            {
                captcha.Visible = false;
                LogonParameters_Custom._Captcha = null;
            }
        }
    }
}
