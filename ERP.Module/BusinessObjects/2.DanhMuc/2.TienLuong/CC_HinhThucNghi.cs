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
    [DefaultProperty("TenHinhThucNghi")]
    [ImageName("BO_QuanLyChamCong")]
    [ModelDefault("Caption", "Hình thức chấm công trọn ngày")]
    public class CC_HinhThucNghi : BaseObject
    {
        //
        private string _MaQuanLy;
        private string _TenHinhThucNghi;
        private string _KyHieu;
        private decimal _GiaTri;
        private int _SoNgayToiDa;
        private decimal _DoUuTien;
        private bool _HinhThucNghi;

        //
        [ModelDefault("AllowEdit", "False")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Mã quản lý")]
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

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Tên hình thức nghỉ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string TenHinhThucNghi
        {
            get
            {
                return _TenHinhThucNghi;
            }
            set
            {
                SetPropertyValue("TenHinhThucNghi", ref _TenHinhThucNghi, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Ký hiệu")]
        public string KyHieu
        {
            get
            {
                return _KyHieu;
            }
            set
            {
                SetPropertyValue("KyHieu", ref _KyHieu, value);
            }
        }
        
        [ModelDefault("Caption", "Giá trị")]
        [ModelDefault("Editmask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal GiaTri
        {
            get
            {
                return _GiaTri;
            }
            set
            {
                SetPropertyValue("GiaTri", ref _GiaTri, value);
            }
        }

        [ModelDefault("Caption", "Số ngày tối đa")]
        public int SoNgayToiDa
        {
            get
            {
                return _SoNgayToiDa;
            }
            set
            {
                SetPropertyValue("SoNgayToiDa", ref _SoNgayToiDa, value);
            }
        }

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

        [ModelDefault("Caption", "Hình thức nghỉ")]
        public bool HinhThucNghi
        {
            get
            {
                return _HinhThucNghi;
            }
            set
            {
                SetPropertyValue("HinhThucNghi", ref _HinhThucNghi, value);
            }
        }

        public CC_HinhThucNghi(Session session) : base(session) { }
    }

}
