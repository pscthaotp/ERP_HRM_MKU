using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenSucKhoe")]
    [ModelDefault("Caption", "Sức khỏe")]
    public class SucKhoe : BaseObject
    {
        private string _MaQuanLy;
        private string _TenSucKhoe;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tên sức khỏe")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenSucKhoe
        {
            get
            {
                return _TenSucKhoe;
            }
            set
            {
                SetPropertyValue("TenSucKhoe", ref _TenSucKhoe, value);
            }
        }

        public SucKhoe(Session session) : base(session) { }
    }

}
