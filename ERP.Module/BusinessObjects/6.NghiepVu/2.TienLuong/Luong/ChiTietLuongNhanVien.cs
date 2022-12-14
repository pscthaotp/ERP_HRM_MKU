using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.Enum.NhanSu;
//
namespace ERP.Module.NghiepVu.TienLuong.Luong
{
    [DefaultProperty("DienGiai")]
    [ImageName("BO_ChiTietLuong")]
    [ModelDefault("Caption", "Chi tiết lương cán bộ")]
    [ModelDefault("AllowEdit", "False")]
    public class ChiTietLuongNhanVien : BaseObject
    {
        private LuongNhanVien _LuongNhanVien;
        private string _DienGiai;
        private string _MaChiTiet;
        private string _CostCenter;
        private decimal _TienLuong;
        private decimal _TongNgayCong;
        private decimal _SoTien;
        private decimal _SoTienChiuThue;
        private CongTruEnum _CongTru;
        private string _CongThucTinhSoTien;
        private string _CongThucTinhTNCT;
        private string _CongThucTinhBangChu;
        private string _GhiChu;

        [Browsable(false)]
        [ModelDefault("Caption", "Lương nhân viên")]
        [Association("LuongNhanVien-ListChiTietLuongNhanVien")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public LuongNhanVien LuongNhanVien
        {
            get
            {
                return _LuongNhanVien;
            }
            set
            {
                SetPropertyValue("LuongNhanVien", ref _LuongNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Mã chi tiết")]       
        public string MaChiTiet
        {
            get
            {
                return _MaChiTiet;
            }
            set
            {
                SetPropertyValue("MaChiTiet", ref _MaChiTiet, value);
            }
        }

        [ModelDefault("Caption", "Mã phân bổ")]        
        public string CostCenter
        {
            get
            {
                return _CostCenter;
            }
            set
            {
                SetPropertyValue("CostCenter", ref _CostCenter, value);
            }
        }

        [ModelDefault("Caption", "Diễn giải")]
        public string DienGiai
        {
            get
            {
                return _DienGiai;
            }
            set
            {
                SetPropertyValue("DienGiai", ref _DienGiai, value);
            }
        }

        [ModelDefault("Caption", "Tiền lương")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TienLuong
        {
            get
            {
                return _TienLuong;
            }
            set
            {
                SetPropertyValue("TienLuong", ref _TienLuong, value);
            }
        }

        [ModelDefault("Caption", "Tổng ngày công")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal TongNgayCong
        {
            get
            {
                return _TongNgayCong;
            }
            set
            {
                SetPropertyValue("TongNgayCong", ref _TongNgayCong, value);
            }
        }

        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTien
        {
            get
            {
                return _SoTien;
            }
            set
            {
                SetPropertyValue("SoTien", ref _SoTien, value);
            }
        }

        [ModelDefault("Caption", "Số tiền chịu thuế")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTienChiuThue
        {
            get
            {
                return _SoTienChiuThue;
            }
            set
            {
                SetPropertyValue("SoTienChiuThue", ref _SoTienChiuThue, value);
            }
        }
        
        [ModelDefault("Caption", "Cộng/Trừ")]
        public CongTruEnum CongTru
        {
            get
            {
                return _CongTru;
            }
            set
            {
                SetPropertyValue("CongTru", ref _CongTru, value);
            }
        }

        [Size(500)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        [Size(-1)]
        [Browsable(false)]
        [ModelDefault("Caption", "Công thức tính số tiền")]
        public string CongThucTinhSoTien
        {
            get
            {
                return _CongThucTinhSoTien;
            }
            set
            {
                SetPropertyValue("CongThucTinhSoTien", ref _CongThucTinhSoTien, value);
            }
        }

        [Size(-1)]
        [Browsable(false)]
        [ModelDefault("Caption", "Công thức tính bằng chữ")]
        public string CongThucTinhBangChu
        {
            get
            {
                return _CongThucTinhBangChu;
            }
            set
            {
                SetPropertyValue("CongThucTinhBangChu", ref _CongThucTinhBangChu, value);
            }
        }   

        [Size(-1)]
        [Browsable(false)]
        [ModelDefault("Caption", "Công thức tính TNCT")]
        public string CongThucTinhTNCT
        {
            get
            {
                return _CongThucTinhTNCT;
            }
            set
            {
                SetPropertyValue("CongThucTinhTNCT", ref _CongThucTinhTNCT, value);
            }
        }

        public ChiTietLuongNhanVien(Session session) : base(session) { }
    }

}
