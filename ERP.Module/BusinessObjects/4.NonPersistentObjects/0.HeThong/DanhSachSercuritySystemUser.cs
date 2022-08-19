using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.Systems;
using ERP.Module.HeThong;
using DevExpress.Data.Filtering;
using System.ComponentModel;

namespace ERP.Module.NonPersistentObjects.HeThong
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn tài khoản")]
    public class DanhSachSercuritySystemUser : BaseObject
    {
        private string _UserOld;

        [Browsable(false)]
        public string UserOld
        {
            get
            {
                return _UserOld;
            }
            set
            {
                SetPropertyValue("UserOld", ref _UserOld, value);
            }
        }

        //
        [ModelDefault("Caption", "Danh sách tài khoản")]
        public XPCollection<ChiTietSecuritySystemUser> UserList { get; set; }

        public DanhSachSercuritySystemUser(Session session) : base(session) { }

        public void LoadData()
        {
            UserList = new XPCollection<ChiTietSecuritySystemUser>(Session, false);
            //
            XPCollection<SecuritySystemUser_Custom> userList = new XPCollection<SecuritySystemUser_Custom>(Session);
            //
            foreach (var item in userList)
            {
                ChiTietSecuritySystemUser user = new ChiTietSecuritySystemUser(Session);
                user.User = item;
                if (UserOld.Contains(item.UserName + ";"))
                {
                    user.Chon = true;
                }
                UserList.Add(user);
            }
        }
    }

}
