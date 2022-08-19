using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.Data.Filtering;
using ERP.Module.Enum.PMS;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.PMS.HeSo;
using ERP.Module.NghiepVu.PMS.DanhMuc;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace ERP.Module.NghiepVu.PMS.BangChotThuLao
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Thông tin bảng chốt thù lao")]
    [Appearance("ThongTinBangChotThuLao_Hide", TargetItems = "GioCQThieu;DinhMuc;TongDinhMuc;GioCVHT;DinhMucNCKH;GioNCKHKyTruoc;SoGioNCKHThucHien;SoGioNCKHConThieu;TongSoGioChuaVuot", Visibility = ViewItemVisibility.Hide, Criteria = "BangChotThuLao = null")]
    public class ThongTinBangChotThuLao : BaseObject
    {
        private BangChotThuLao _BangChotThuLao;
        private BangChotThuLao_ThinhGiang _BangChotThuLao_ThinhGiang;
        private NhanVien _NhanVien;
        private decimal _DinhMuc;
        private decimal _TongDinhMuc;
        private decimal _GioCVHT;
        private decimal _GioCQThieu;

        private decimal _DinhMucNCKH;
        private decimal _GioNCKHKyTruoc;
        private decimal _SoGioNCKHThucHien;
        private decimal _SoGioNCKHConThieu;

        private decimal _TongSoGioChuaVuot;
        private decimal _TongSoGioDaVuot;
        private decimal _ThanhTien;
        private decimal _ThueTNCN;
        private decimal _ThucLanh;

        [Association("BangChotThuLao-ListThongTinBangChotThuLao")]
        [Browsable(false)]
        [ModelDefault("Caption", "Bảng chốt thù lao")]
        public BangChotThuLao BangChotThuLao
        {
            get
            {
                return _BangChotThuLao;
            }
            set
            {
                SetPropertyValue("BangChotThuLao", ref _BangChotThuLao, value);
            }
        }

        [Association("BangChotThuLao_ThinhGiang-ListThongTinBangChotThuLao")]
        [Browsable(false)]
        [ModelDefault("Caption", "Bảng chốt thù lao(Thỉnh giảng)")]
        public BangChotThuLao_ThinhGiang BangChotThuLao_ThinhGiang
        {
            get
            {
                return _BangChotThuLao_ThinhGiang;
            }
            set
            {
                SetPropertyValue("BangChotThuLao_ThinhGiang", ref _BangChotThuLao_ThinhGiang, value);
            }
        }

        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }

        [ModelDefault("Caption", "Định mức")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DinhMuc
        {
            get { return _DinhMuc; }
            set { SetPropertyValue("DinhMuc", ref _DinhMuc, value); }
        }

        [ModelDefault("Caption", "Định mức NCKH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DinhMucNCKH
        {
            get { return _DinhMucNCKH; }
            set { SetPropertyValue("DinhMucNCKH", ref _DinhMucNCKH, value); }
        }

        [ModelDefault("Caption", "Giờ CVHT")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioCVHT
        {
            get { return _GioCVHT; }
            set { SetPropertyValue("GioCVHT", ref _GioCVHT, value); }
        }

        [ModelDefault("Caption", "Tổng định mức")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongDinhMuc
        {
            get { return _TongDinhMuc; }
            set { SetPropertyValue("TongDinhMuc", ref _TongDinhMuc, value); }
        }

        [ModelDefault("Caption", "Giờ chính quy thiếu")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioCQThieu
        {
            get { return _GioCQThieu; }
            set { SetPropertyValue("GioCQThieu", ref _GioCQThieu, value); }
        }

        [ModelDefault("Caption", "Giờ NCKH kỳ trước")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioNCKHKyTruoc
        {
            get { return _GioNCKHKyTruoc; }
            set { SetPropertyValue("GioNCKHKyTruoc", ref _GioNCKHKyTruoc, value); }
        }

        [ModelDefault("Caption", "Giờ NCKH thực hiện")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioNCKHThucHien
        {
            get { return _SoGioNCKHThucHien; }
            set { SetPropertyValue("SoGioNCKHThucHien", ref _SoGioNCKHThucHien, value); }
        }

        [ModelDefault("Caption", "Giờ NCKH còn thiếu")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioNCKHConThieu
        {
            get { return _SoGioNCKHConThieu; }
            set { SetPropertyValue("SoGioNCKHConThieu", ref _SoGioNCKHConThieu, value); }
        }

        [ModelDefault("Caption", "Giờ chưa vượt")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongSoGioChuaVuot
        {
            get { return _TongSoGioChuaVuot; }
            set { SetPropertyValue("TongSoGioChuaVuot", ref _TongSoGioChuaVuot, value); }
        }

        [ModelDefault("Caption", "Giờ đã vượt")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongSoGioDaVuot
        {
            get { return _TongSoGioDaVuot; }
            set { SetPropertyValue("TongSoGioDaVuot", ref _TongSoGioDaVuot, value); }
        }

        [ModelDefault("Caption", "Thành tiền")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ThanhTien
        {
            get { return _ThanhTien; }
            set { SetPropertyValue("ThanhTien", ref _ThanhTien, value); }
        }

        [ModelDefault("Caption", "Thuế TNCN")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ThueTNCN
        {
            get { return _ThueTNCN; }
            set { SetPropertyValue("ThueTNCN", ref _ThueTNCN, value); }
        }

        [ModelDefault("Caption", "Thực lãnh")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ThucLanh
        {
            get { return _ThucLanh; }
            set { SetPropertyValue("ThucLanh", ref _ThucLanh, value); }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết bảng chốt")]
        [Association("ThongTinBangChotThuLao-ListChiTietBangChotThuLao")]
        public XPCollection<ChiTietBangChotThuLao> ListChiTietBangChotThuLao
        {
            get
            {
                return GetCollection<ChiTietBangChotThuLao>("ListChiTietBangChotThuLao");
            }
        }
        public ThongTinBangChotThuLao(Session session)
            : base(session) {}

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

    }
}
