using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Data.SqlClient;
using ERP.Module.Commons;
//
namespace ERP.Module.DanhMuc.TienLuong
{
    [DefaultClassOptions]
    [DefaultProperty("Name")]
    [ImageName("BO_KyTinhLuong")]
    [ModelDefault("Caption", "Nhóm tài khoản")]
    public class WebGroup : BaseObject
    {
        private string _Name;
        
        [ModelDefault("Caption", "Tên nhóm")]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                SetPropertyValue("Name", ref _Name, value);
            }
        }

        public WebGroup(Session session) :base(session) { }

    }

}
