using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.Systems;
using ERP.Module.HeThong;
using DevExpress.Data.Filtering;
using ERP.Module.Commons;

namespace ERP.Module.NonPersistentObjects.HeThong
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Quản lý nhật ký thao tác")]
    public class ActivitiesLogManager : BaseObject
    {
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private SecuritySystemUser_Custom _User;

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if(!IsLoading && value != DateTime.MinValue)
                {
                    LoadData();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                if (!IsLoading && value != DateTime.MinValue)
                {
                    LoadData();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tài khoản")]
        public SecuritySystemUser_Custom User
        {
            get
            {
                return _User;
            }
            set
            {
                SetPropertyValue("User", ref _User, value);
                if (!IsLoading)
                {
                    LoadData();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Danh sách chi tiết")]
        public XPCollection<LogInOut> LogList { get; set; }


        public ActivitiesLogManager(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            DenNgay = Common.GetServerCurrentTime();
            TuNgay = DenNgay.AddDays(-7);
        }

        private void LoadData()
        {
            CriteriaOperator filter = null;
            CriteriaOperator filter1 = null;

            if (TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
            {
                filter = CriteriaOperator.Parse("? <= DateTime && DateTime <= ?", TuNgay, DenNgay);
                if(User != null)
                    filter1 = CriteriaOperator.Parse("User =?", User.UserName);

                LogList = new XPCollection<LogInOut>(Session, GroupOperator.And(filter,filter1));
                OnChanged("LogList");
            }
        }

    }

}
