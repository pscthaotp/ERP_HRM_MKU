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
    [DefaultProperty("TenNgoaiNgu")]
    [ModelDefault("Caption", "Ngoại ngữ")]
    public class NgoaiNgu : BaseObject
    {
        private string _MaQuanLy;
        private string _TenNgoaiNgu;

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

        [ModelDefault("Caption", "Tên ngoại ngữ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNgoaiNgu
        {
            get
            {
                return _TenNgoaiNgu;
            }
            set
            {
                SetPropertyValue("TenNgoaiNgu", ref _TenNgoaiNgu, value);
            }
        }

        public NgoaiNgu(Session session) : base(session) { }
    }

}
