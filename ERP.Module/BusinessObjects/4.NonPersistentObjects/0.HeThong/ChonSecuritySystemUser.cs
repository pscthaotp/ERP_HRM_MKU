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
    [ModelDefault("Caption", "Chọn dữ liệu")]
    public class ChonSecuritySystemUser : BaseObject
    {
        private SecuritySystemUser_Custom _User;

        //
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


        public ChonSecuritySystemUser(Session session) : base(session) { }

    }

}
