using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenHocHam")]
    [ModelDefault("Caption", "Học hàm")]
    public class HocHam : BaseObject
    {
        private string _MaQuanLy;
        private string _TenHocHam;
        private decimal _CapDo;
        
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

        [ModelDefault("Caption", "Tên học hàm")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenHocHam
        {
            get
            {
                return _TenHocHam;
            }
            set
            {
                SetPropertyValue("TenHocHam", ref _TenHocHam, value);
            }
        }

        [ModelDefault("Caption", "Cấp độ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal CapDo
        {
            get
            {
                return _CapDo;
            }
            set
            {
                SetPropertyValue("CapDo", ref _CapDo, value);
            }
        }

        public HocHam(Session session) : base(session) { }
    }

}
