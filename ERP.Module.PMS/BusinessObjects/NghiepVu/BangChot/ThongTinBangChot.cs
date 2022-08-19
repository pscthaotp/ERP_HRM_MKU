using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.PMS.BusinessObjects;


namespace ERP.Module.PMS.NghiepVu
{

    [ModelDefault("Caption", "Danh sách nhân viên")]
    [Appearance("BangChotThuLao_ThinhGiang", TargetItems = "DonGia", Visibility = ViewItemVisibility.Hide, Criteria = "BangChotThuLao_ThinhGiang.ThinhGiang")]
    //[Appearance("Hide_HeSo", TargetItems = "HeSoCoSo;HeSoLuong;HeSoMonMoi;HeSoGiangDayNgoaiGio", Visibility = ViewItemVisibility.Hide, Criteria = "KhoiLuongGiangDay.ThongTinTruong.TenVietTat <> 'QNU'")]
    [Appearance("ThongTinBangChot_Khoa", TargetItems = "*", Enabled = false, Criteria = "Khoa = 1")]
    [Appearance("ToMauTongTien", TargetItems = "TongTienThanhToan", BackColor = "lightyellow", FontColor = "Black", FontStyle = System.Drawing.FontStyle.Bold)]
    [Appearance("ToMauDonGia_DonGia", TargetItems = "DonGia", BackColor = "palegreen", FontColor = "Black")]
    [Appearance("ToMauDonGia_Thue", TargetItems = "TongTienThanhToanThueTNCN", BackColor = "SandyBrown", FontColor = "Black", FontStyle = System.Drawing.FontStyle.Bold)]
    [Appearance("ToMauTongTienThucLanh", TargetItems = "TongTienThucLanh", BackColor = "lightsteelblue", FontColor = "Black", FontStyle = System.Drawing.FontStyle.Bold)]
    public class ThongTinBangChot : ThongTinChungNhanVien, IBoPhan
    {
        #region key
        private BangChotThuLao _BangChotThuLao;
        [Association("BangChotThuLao-ListThongTinBangChot")]
        [ModelDefault("Caption", "Bảng chốt thông tin giảng dạy")]
        [Browsable(false)]
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

        private BangChotThuLao_ThinhGiang _BangChotThuLao_ThinhGiang;
        [Association("BangChotThuLao_ThinhGiang-ListThongTinBangChotThuLao")]
        [ModelDefault("Caption", "Bảng chốt thông tin giảng dạy(thỉnh giảng)")]
        [Browsable(false)]
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

        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                return String.Format("{0} - {1}", NhanVien != null ? NhanVien.HoTen : "", BangChotThuLao != null ? BangChotThuLao.Caption : "");
            }
        }
        #endregion

        #region Khai báo
        private bool _Khoa;
        private decimal _GioNghiaVu;
        private decimal _DinhMucNCKH;
        private decimal _DinhMucHDKhac;
        //
        private decimal _TongGioRaDe;
        private decimal _TongGioCoiThi;
        private decimal _TongGioChamBai;
        //
        private decimal _TongGioGiangDay;
        private decimal _TongGioNCKH;
        private decimal _TongGioHDKhac;
        //
        private decimal _TongGioGDSauKhiTruDM;
        private decimal _TongGioNCKHSauKhiTruDM;
        private decimal _TongGioHDKhacSauKhiTruDM;
        //
        private decimal _GioVuotGiangDay;
        private decimal _GioVuotNCKH;
        private decimal _GioVuotHDKhac;
        //
        //private decimal _SoLuotDiLai;
        //private decimal _DonGiaDiLai;
        //private decimal _TongTienDiLai;
        //
        private decimal _TongSoLuotDiLai;
        private decimal _TongTienDiLai;
        //
        private decimal _DonGia; 
        private decimal _TongTienThanhToan;
        private decimal _TongTienThanhToanThueTNCN;
        private decimal _TongTienThucLanh;
        //

        #endregion

        #region 
        [ModelDefault("Caption", "Khóa")]
        [ModelDefault("AllowEdit", "False")]
        [VisibleInListView(false)]
        [ModelDefault("AllowEdit", "false")]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }
        [ModelDefault("Caption", "Giờ nghĩa vụ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioNghiaVu
        {
            get { return _GioNghiaVu; }
            set { SetPropertyValue("GioNghiaVu", ref _GioNghiaVu, value); }
        }
        [ModelDefault("Caption", "Định mức NCKH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal DinhMucNCKH
        {
            get { return _DinhMucNCKH; }
            set { SetPropertyValue("DinhMucNCKH", ref _DinhMucNCKH, value); }
        }
        [ModelDefault("Caption", "Định mức HĐ khác")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal DinhMucHDKhac
        {
            get { return _DinhMucHDKhac; }
            set { SetPropertyValue("DinhMucHDKhac", ref _DinhMucHDKhac, value); }
        }

        //

        [ModelDefault("Caption", "Tổng giờ ra đề")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGioRaDe
        {
            get { return _TongGioRaDe; }
            set { SetPropertyValue("TongGioRaDe", ref _TongGioRaDe, value); }
        }
        [ModelDefault("Caption", "Tổng giờ coi thi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGioCoiThi
        {
            get { return _TongGioCoiThi; }
            set { SetPropertyValue("TongGioCoiThi", ref _TongGioCoiThi, value); }
        }
        [ModelDefault("Caption", "Tổng giờ chấm bài")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGioChamBai
        {
            get { return _TongGioChamBai; }
            set { SetPropertyValue("TongGioChamBai", ref _TongGioChamBai, value); }
        }

        //
        [ModelDefault("Caption", "Tổng giờ giảng dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGioGiangDay
        {
            get { return _TongGioGiangDay; }
            set { SetPropertyValue("TongGioGiangDay", ref _TongGioGiangDay, value); }
        }
        [ModelDefault("Caption", "Tổng giờ NCKH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGioNCKH
        {
            get { return _TongGioNCKH; }
            set { SetPropertyValue("TongGioNCKH", ref _TongGioNCKH, value); }
        }
        [ModelDefault("Caption", "Tổng giờ HĐ khác")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGioHDKhac
        {
            get { return _TongGioHDKhac; }
            set { SetPropertyValue("TongGioHDKhac", ref _TongGioHDKhac, value); }
        }

        //

        [ModelDefault("Caption", "Tổng giờ GD sau khi trừ ĐM")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGioGDSauKhiTruDM
        {
            get { return _TongGioGDSauKhiTruDM; }
            set { SetPropertyValue("TongGioGDSauKhiTruDM", ref _TongGioGDSauKhiTruDM, value); }
        }

        [ModelDefault("Caption", "Tổng giờ NCKH sau khi trừ ĐM")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGioNCKHSauKhiTruDM
        {
            get { return _TongGioNCKHSauKhiTruDM; }
            set { SetPropertyValue("TongGioNCKHSauKhiTruDM", ref _TongGioNCKHSauKhiTruDM, value); }
        }
        [ModelDefault("Caption", "Tổng giờ HĐ khác sau khi trừ ĐM")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGioHDKhacSauKhiTruDM
        {
            get { return _TongGioHDKhacSauKhiTruDM; }
            set { SetPropertyValue("TongGioHDKhacSauKhiTruDM", ref _TongGioHDKhacSauKhiTruDM, value); }
        }

        //

        [ModelDefault("Caption", "Giờ vượt Giảng dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioVuotGiangDay
        {
            get { return _GioVuotGiangDay; }
            set { SetPropertyValue("GioVuotGiangDay", ref _GioVuotGiangDay, value); }
        }
        [ModelDefault("Caption", "Giờ vượt HĐ khác")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioVuotHDKhac
        {
            get { return _GioVuotHDKhac; }
            set { SetPropertyValue("GioVuotHDKhac", ref _GioVuotHDKhac, value); }
        }
        [ModelDefault("Caption", "Giờ vượt NCKH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioVuotNCKH
        {
            get { return _GioVuotNCKH; }
            set { SetPropertyValue("GioVuotNCKH", ref _GioVuotNCKH, value); }
        }

        //ThuHuong tắt chuyển cái này qua ChiTietBangChotThuLaoGiangDay
        //[ModelDefault("Caption", "Số lượt đi lại")]
        //[ModelDefault("DisplayFormat", "N0")]
        //[ModelDefault("EditMask", "N0")]
        //[ModelDefault("AllowEdit", "false")]
        //public decimal SoLuotDiLai
        //{
        //    get { return _SoLuotDiLai; }
        //    set { SetPropertyValue("SoLuotDiLai", ref _SoLuotDiLai, value); }
        //}
        //[ModelDefault("Caption", "Đơn giá đi lại")]
        //[ModelDefault("DisplayFormat", "N2")]
        //[ModelDefault("EditMask", "N2")]
        //[ModelDefault("AllowEdit", "false")]
        //public decimal DonGiaDiLai
        //{
        //    get { return _DonGiaDiLai; }
        //    set { SetPropertyValue("DonGiaDiLai", ref _DonGiaDiLai, value); }
        //}
        //[ModelDefault("Caption", "Đơn giá")]
        //[ModelDefault("DisplayFormat", "N2")]
        //[ModelDefault("EditMask", "N2")]
        //[ModelDefault("AllowEdit", "false")]
        //public decimal TongTienDiLai
        //{
        //    get { return _TongTienDiLai; }
        //    set { SetPropertyValue("TongTienDiLai", ref _TongTienDiLai, value); }
        //}
       
        //
        [ModelDefault("Caption", "Tổng số lượt đi lại")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongSoLuotDiLai
        {
            get { return _TongSoLuotDiLai; }
            set { SetPropertyValue("TongSoLuotDiLai", ref _TongSoLuotDiLai, value); }
        }
        [ModelDefault("Caption", "Tổng tiền đi lại")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongTienDiLai
        {
            get { return _TongTienDiLai; }
            set { SetPropertyValue("TongTienDiLai", ref _TongTienDiLai, value); }
        }
        //

        [ModelDefault("Caption", "Đơn giá")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("AllowEdit", "false")]
        public decimal DonGia
        {
            get { return _DonGia; }
            set { SetPropertyValue("DonGia", ref _DonGia, value); }
        }
        [ModelDefault("Caption", "Tổng tiền thanh toán")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongTienThanhToan
        {
            get { return _TongTienThanhToan; }
            set { SetPropertyValue("TongTienThanhToan", ref _TongTienThanhToan, value); }
        }
        [ModelDefault("Caption", "Tổng tiền thanh toán (Thuế TNCN)")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongTienThanhToanThueTNCN
        {
            get { return _TongTienThanhToanThueTNCN; }
            set { SetPropertyValue("TongTienThanhToanThueTNCN", ref _TongTienThanhToanThueTNCN, value); }
        }
        [ModelDefault("Caption", "Tổng tiền thực lãnh")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongTienThucLanh
        {
            get { return _TongTienThucLanh; }
            set { SetPropertyValue("TongTienThucLanh", ref _TongTienThucLanh, value); }
        }
        #endregion

        [Aggregated]
        [Association("ThongTinBangChot-ListChiTietBangChot")]
        [ModelDefault("Caption", "Chi tiết")] 
        public XPCollection<ChiTietBangChotThuLaoGiangDay> ListChiTietBangChot
        {
            get
            {
                return GetCollection<ChiTietBangChotThuLaoGiangDay>("ListChiTietBangChot");
            }
        }

        public ThongTinBangChot(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}