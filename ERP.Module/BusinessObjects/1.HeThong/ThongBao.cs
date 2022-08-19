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
using ERP.Module.Enum.NhanSu;
using DevExpress.Persistent.Base.General;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
using ERP.Module.DanhMuc.System;
using DevExpress.ExpressApp;
using DevExpress.XtraBars.Alerter;
using DevExpress.LookAndFeel;
using ERP.Module.DanhMuc.UseNotification;

namespace ERP.Module.HeThong
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Thông báo")]
    [Appearance("ThongBao.Enabled", TargetItems = "*", Enabled = false)]
    [ImageName("Nav_HeThong")]
    public class ThongBao : BaseObject, ISupportNotifications, ISecuritySystemUser_Custom
    {
        private string _NotificationMessage;
        private DateTime? _Date;
        private TimeSpan _Time;
        private DateTime? _AlarmTime;
        private bool _IsPostponed;
        //
        private string _KeyObject;
        private Guid _Guid;
        private SecuritySystemUser_Custom _SecuritySystemUser;
        private QuanLyThongBao _QuanLyThongBao;

        [ModelDefault("Caption", "Ghi chú")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string NotificationMessage
        {
            get
            {
                return _NotificationMessage;
            }
            set
            {
                SetPropertyValue("NotificationMessage", ref _NotificationMessage, value);
            }
        }

        [ModelDefault("Caption", "Ngày")]
        public DateTime? Date
        {
            get
            {
                return _Date;
            }
            set
            {
                SetPropertyValue("Date", ref _Date, value);
                if(!IsLoading && Date != DateTime.MinValue && Date != null)
                {
                    AlarmTime = Date.Value.Date.Add(Time);
                }
            }
        }

        [ModelDefault("Caption", "Giờ")]
        public TimeSpan Time
        {
            get
            {
                return _Time;
            }
            set
            {
                SetPropertyValue("Time", ref _Time, value);
                if (!IsLoading && Date != DateTime.MinValue && Date != null)
                {
                    AlarmTime = Date.Value.Date.Add(Time);
                }
            }
        }

        [ModelDefault("Editmask", "dd/MM/yyyy hh:mm tt")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy hh:mm tt")]
        [ModelDefault("AllowEdit", "False")]
        public DateTime? AlarmTime
        {
            get
            {
                return _AlarmTime;
            }
            set
            {
                SetPropertyValue("AlarmTime", ref _AlarmTime, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Xong")]
        public bool IsPostponed
        {
            get
            {
                return _IsPostponed;
            }
            set
            {
                SetPropertyValue("IsPostponed", ref _IsPostponed, value);
            }
        }

        [Browsable(false)]
        public string KeyObject
        {
            get
            {
                return _KeyObject;
            }
            set
            {
                SetPropertyValue("KeyObject", ref _KeyObject, value);
            }
        }

        [Browsable(false)]
        public Guid Guid
        {
            get
            {
                return _Guid;
            }
            set
            {
                SetPropertyValue("Guid", ref _Guid, value);
            }
        }

        [ModelDefault("Caption", "Tài khoản nhận")]
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

        [ModelDefault("Caption", "Quản lý thông báo")]
        public QuanLyThongBao QuanLyThongBao
        {
            get
            {
                return _QuanLyThongBao;
            }
            set
            {
                SetPropertyValue("QuanLyThongBao", ref _QuanLyThongBao, value);
            }
        }

        [Browsable(false)]
        public int Id { get; private set; }

        #region ISupportNotifications members

        [Browsable(false)]
        public object UniqueId
        {
            get { return Id; }
        }
        #endregion

        public ThongBao(Session session) : base(session) { }

        protected override void OnSaving()
        {
            base.OnSaving();
        }
    }
}