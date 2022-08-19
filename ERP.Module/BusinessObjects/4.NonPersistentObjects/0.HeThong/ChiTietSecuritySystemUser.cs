using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.Systems;
using ERP.Module.HeThong;
using DevExpress.Data.Filtering;

namespace ERP.Module.NonPersistentObjects.HeThong
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chi tiết tài khoản")]
    public class ChiTietSecuritySystemUser : BaseObject
    {
        private bool _Chon;
        private SecuritySystemUser_Custom _User;

        [ModelDefault("Caption", "Chọn")]
        [ImmediatePostData]
        public bool Chon
        {
            get
            {
                return _Chon;
            }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }

        [ModelDefault("Caption", "Tài khoản")]
        [ImmediatePostData]
        public SecuritySystemUser_Custom User
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

        public ChiTietSecuritySystemUser(Session session) : base(session) { }

    }

}
