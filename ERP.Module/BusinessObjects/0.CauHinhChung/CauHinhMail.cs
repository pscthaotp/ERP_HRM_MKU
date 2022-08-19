using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace ERP.Module.CauHinhChungs
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình mail")]
    public class CauHinhMail : BaseObject
    {
        //
        private int _Port;
        private string _Email;
        private string _Password;
        private string _Server;
        private string _Template;
        //
        [ModelDefault("Caption", "Cổng - Mail")]
        public int Port
        {
            get
            {
                return _Port;
            }
            set
            {
                SetPropertyValue("Port", ref _Port, value);
            }
        }

        [ModelDefault("Caption", "Máy chủ - Mail")]
        public string Server
        {
            get
            {
                return _Server;
            }
            set
            {
                SetPropertyValue("Server", ref _Server, value);
            }
        }

        [ModelDefault("Caption", "Tài khoản - Mail")]
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                SetPropertyValue("Email", ref _Email, value);
            }
        }

        [ModelDefault("Caption", "Mật khẩu - Mail")]
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                SetPropertyValue("Password", ref _Password, value);
            }
        }
        [Size(SizeAttribute.Unlimited)]
        [ModelDefault("Caption", "Mẫu")]
        [ModelDefault("PropertyEditorType", "ERP.Module.Web.Editors.Html_NoiDungGuiMail")]
        public string Template
        {
            get
            {
                return _Template;
            }
            set
            {
                SetPropertyValue("Template", ref _Template, value);     
            }
        }
        public CauHinhMail(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
        }
    }

}
