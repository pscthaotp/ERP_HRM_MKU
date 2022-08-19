using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.Systems;
using ERP.Module.Commons;

namespace ERP.Module.HeThong
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("Title")]
    [ModelDefault("Caption", "Quản lý gửi mail")]
    public class MailManager : BaseObject, ILoaiPhanMen, ISecuritySystemUser_Custom
    {
        private string _Title;
        private string _Contents;
        private string _ReceiverEmail;
        private DateTime _ReceiverDate;
        private DateTime _SendDate;
        private string _SendEmail;
        private string _SendPass;
        private SecuritySystemUser_Custom _SecuritySystemUser;
        private TrangThaiGuiMailEnum _TrangThaiGuiMail;
        private Guid _KeyMask;
        private LoaiPhanMenEnum _LoaiPhanMen;
        private LoaiEmailEnum _LoaiEmail = LoaiEmailEnum.Gui;

        //
        [NonPersistent]
        [ModelDefault("Caption", "Năm")]
        public string Nam { get; set; }

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Loại phần mềm")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiPhanMenEnum LoaiPhanMen
        {
            get
            {
                return _LoaiPhanMen;
            }
            set
            {
                SetPropertyValue("LoaiPhanMen", ref _LoaiPhanMen, value);
            }
        }

        [ModelDefault("Caption", "Loại Email")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiEmailEnum LoaiEmail
        {
            get
            {
                return _LoaiEmail;
            }
            set
            {
                SetPropertyValue("LoaiEmail", ref _LoaiEmail, value);
            }
        }

        [ModelDefault("Caption", "Tiêu đề")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                SetPropertyValue("Title", ref _Title, value);
            }
        }

        [ModelDefault("Caption", "Nội dung")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "ERP.Module.Web.Editors.Html_NoiDungGuiMail")]
        public string Contents
        {
            get
            {
                return _Contents;
            }
            set
            {
                SetPropertyValue("Contents", ref _Contents, value);
            }
        }

        [ModelDefault("Caption", "Ngày gửi")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime SendDate
        {
            get
            {
                return _SendDate;
            }
            set
            {
                SetPropertyValue("SendDate", ref _SendDate, value);
                if (value != DateTime.MinValue)
                {
                    Nam = value.Year.ToString();
                }
            }
        }

        [ModelDefault("Caption", "Email gửi")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string SendEmail
        {
            get
            {
                return _SendEmail;
            }
            set
            {
                SetPropertyValue("SendEmail", ref _SendEmail, value);
            }
        }

        [ModelDefault("Caption", "Pass gửi")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Browsable(false)]
        public string SendPass
        {
            get
            {
                return _SendPass;
            }
            set
            {
                SetPropertyValue("SendPass", ref _SendPass, value);
            }
        }

        [ModelDefault("Caption", "Email nhận")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string ReceiverEmail
        {
            get
            {
                return _ReceiverEmail;
            }
            set
            {
                SetPropertyValue("ReceiverEmail", ref _ReceiverEmail, value);
            }
        }

        [ModelDefault("Caption", "Ngày nhận")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime ReceiverDate
        {
            get
            {
                return _ReceiverDate;
            }
            set
            {
                SetPropertyValue("ReceiverDate", ref _ReceiverDate, value);
                if (value != DateTime.MinValue)
                {
                    Nam = value.Year.ToString();
                }
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Người gửi")]
        [RuleRequiredField(DefaultContexts.Save)]
        public SecuritySystemUser_Custom SecuritySystemUser
        {
            get
            {
                return _SecuritySystemUser;
            }
            set
            {
                SetPropertyValue("SecuritySystemUser", ref _SecuritySystemUser, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Trạng thái gửi mail")]
        public TrangThaiGuiMailEnum TrangThaiGuiMail
        {
            get
            {
                return _TrangThaiGuiMail;
            }
            set
            {
                SetPropertyValue("TrangThaiGuiMail", ref _TrangThaiGuiMail, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Lưu vết")]
        public Guid KeyMask
        {
            get
            {
                return _KeyMask;
            }
            set
            {
                SetPropertyValue("KeyMask", ref _KeyMask, value);
            }
        }

        public MailManager(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            SendDate = Common.GetServerCurrentTime();
            SecuritySystemUser = Common.SecuritySystemUser_GetCurrentUser(Session);
            //
            if (Config.TypeApplication.Equals("WebForm"))
            {
                LoaiPhanMen = LoaiPhanMenEnum.Web;
            }
            else
            {
                LoaiPhanMen = LoaiPhanMenEnum.Win;
            }
        }
    }
}
