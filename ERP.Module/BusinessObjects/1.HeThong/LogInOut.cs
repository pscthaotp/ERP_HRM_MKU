using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base.General;

namespace ERP.Module.HeThong
{
    [DefaultClassOptions]
    [ModelDefault("AllowEdit", "False")]
    [ModelDefault("Caption", "Nhật ký thao tác")]
    [ImageName("Nav_HeThong")]
    public class LogInOut : BaseObject
    {
        private string _User;
        private string _IP;
        private string _HostName;
        private DateTime _DateTime;
        private string _Note;
        private string _ActivitiesLog;

        [ModelDefault("Caption", "Tài khoản")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string User
        {
            get
            {
                return _User;
            }
            set
            {
                SetPropertyValue("User", ref _User, value);
            }
        }

        [ModelDefault("Caption", "IP")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string IP
        {
            get
            {
                return _IP;
            }
            set
            {
                SetPropertyValue("IP", ref _IP, value);
            }
        }

        [ModelDefault("Caption", "Tên host")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string HostName
        {
            get
            {
                return _HostName;
            }
            set
            {
                SetPropertyValue("HostName", ref _HostName, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy hh:mm:ss:tt")]
        [ModelDefault("EditMask", "dd/MM/yyyy hh:mm:ss:tt")]
        public DateTime DateTime
        {
            get
            {
                return _DateTime;
            }
            set
            {
                SetPropertyValue("DateTime", ref _DateTime, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string Note
        {
            get
            {
                return _Note;
            }
            set
            {
                SetPropertyValue("Note", ref _Note, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Nhật ký thao tác")]
        public string ActivitiesLog
        {
            get
            {
                return _ActivitiesLog;
            }
            set
            {
                SetPropertyValue("ActivitiesLog", ref _ActivitiesLog, value);
            }
        }

        public LogInOut(Session session)
            : base(session)
        {
        }

    }
}
