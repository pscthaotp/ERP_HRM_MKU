using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.NhanSu;
using ERP.Module.DanhMuc.TienLuong;

namespace ERP.Module.NghiepVu.TienLuong.Luong
{
    [ImageName("BO_BangLuong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Phân tích Lương công ty")]
    [ModelDefault("AllowEdit","False")]
    public class PhanTichLuongCongTy : BaseObject
    {
        private BangPhanTichLuongNhanVien _BangPhanTichLuongNhanVien;
        private string _DienGiai;
        private string _CostCenter1;
        private string _CostCenter2;
        private decimal _SoTien;
        private CongTruEnum _CongTru;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng phân tích chi phí lương nhân viên")]
        [Association("BangPhanTichLuongNhanVien-ListPhanTichLuongCongTy")]
        public BangPhanTichLuongNhanVien BangPhanTichLuongNhanVien
        {
            get
            {
                return _BangPhanTichLuongNhanVien;
            }
            set
            {
                SetPropertyValue("BangPhanTichLuongNhanVien", ref _BangPhanTichLuongNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Mã nhóm phân bổ")]
        public string CostCenter1
        {
            get
            {
                return _CostCenter1;
            }
            set
            {
                SetPropertyValue("CostCenter1", ref _CostCenter1, value);
            }
        }

        [ModelDefault("Caption", "Mã chi phí tiền lương")]
        public string CostCenter2
        {
            get
            {
                return _CostCenter2;
            }
            set
            {
                SetPropertyValue("CostCenter2", ref _CostCenter2, value);
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

        public PhanTichLuongCongTy(Session session) : base(session) { }
    }
}
