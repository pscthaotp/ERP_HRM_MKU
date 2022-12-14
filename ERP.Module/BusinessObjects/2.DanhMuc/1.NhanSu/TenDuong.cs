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
    [DefaultProperty("TenDuongDi")]
    [ModelDefault("Caption", "Tên đường")]
    public class TenDuong : BaseObject
    {
        private string _MaQuanLy;
        private string _TenDuongDi;

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

        [ModelDefault("Caption", "Tên đường")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenDuongDi
        {
            get
            {
                return _TenDuongDi;
            }
            set
            {
                SetPropertyValue("TenDuongDi", ref _TenDuongDi, value);
            }
        }

       
        public TenDuong(Session session) : base(session) { }
    }

}
