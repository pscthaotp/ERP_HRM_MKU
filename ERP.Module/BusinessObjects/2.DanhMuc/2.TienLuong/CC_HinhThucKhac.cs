using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.BaseImpl;

namespace ERP.Module.DanhMuc.TienLuong
{
    [DefaultClassOptions]
    [ImageName("BO_QuanLyChamCong")]
    [ModelDefault("Caption", "Hình thức chấm công nửa ngày")]
    [RuleCriteria("DoUuTien > 0", CustomMessageTemplate = "Độ ưu tiên phải lớn hơn 0", SkipNullOrEmptyValues = false)]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "HinhThucSang;HinhThucChieu")]
    public class CC_HinhThucKhac : BaseObject
    {
        //
        private CC_HinhThucNghi _HinhThucSang;
        private CC_HinhThucNghi _HinhThucChieu;
        private string _MaQuanLy;
        //private string _TenHinhThucNghi;
        private decimal _DoUuTien;
        //

        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Hình thức sáng")]
        public CC_HinhThucNghi HinhThucSang
        {
            get
            {
                return _HinhThucSang;
            }
            set
            {
                SetPropertyValue("HinhThucSang", ref _HinhThucSang, value);
            }
        }

        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Hình thức chiều")]
        public CC_HinhThucNghi HinhThucChieu
        {
            get
            {
                return _HinhThucChieu;
            }
            set
            {
                SetPropertyValue("HinhThucChieu", ref _HinhThucChieu, value);
            }
        }

        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Mã quản lý")]
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

        //[Size(200)]
        //[RuleRequiredField("", DefaultContexts.Save)]
        //[ModelDefault("Caption", "Tên hình thức nghỉ")]
        //public string TenHinhThucNghi
        //{
        //    get
        //    {
        //        return _TenHinhThucNghi;
        //    }
        //    set
        //    {
        //        SetPropertyValue("TenHinhThucNghi", ref _TenHinhThucNghi, value);
        //    }
        //}

        [ModelDefault("Caption", "Độ ưu tiên")]
        [ModelDefault("Editmask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal DoUuTien
        {
            get
            {
                return _DoUuTien;
            }
            set
            {
                SetPropertyValue("DoUuTien", ref _DoUuTien, value);
            }
        }

        public CC_HinhThucKhac(Session session) : base(session) { }
    }

}
