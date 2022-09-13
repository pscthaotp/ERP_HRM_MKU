using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.Text;
using System.Data.SqlClient;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.MaTuDong;
using ERP.Module.Enum.NhanSu;
using ERP.Module.Commons;
//
namespace ERP.Module.NghiepVu.NhanSu.HopDongs
{
    [ImageName("BO_Contract")]
    [DefaultProperty("SoHopDong")]
    [ModelDefault("Caption", "Hợp đồng lao động")]
    [Appearance("HopDongLamViec.HideHopDongLamViec", TargetItems = "HopDongLaoDong;KhoanThayDoi;DieuThayDoi;NoiDungDieuKhoanThayDoi;DieuKhoanThayDoi", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiHopDong != 'Phụ lục hợp đồng'")]
    [Appearance("HopDongLamViec.BaoHiem", TargetItems = "KhongDongBH_CD", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiHopDong.TenLoaiHopDong like '%thử việc%'")]
    public class HopDongLamViec : HopDong
    {
        //
        private LoaiKhoiHopDongEnum _LoaiKhoiHopDong = LoaiKhoiHopDongEnum.KhoiHanhChinh;
        private Decimal _PhanTramTinhLuong;
        private bool _KhongDongBHXH= false;
        private bool _KhongDongBHYT = false;
        private bool _KhongDongBHTN = false;
        private bool _KhongDongCD = false;
        private HinhThucHopDong _HinhThucHopDong;
        private DateTime _DenNgay;
        private DateTime _TuNgay; 
        //Điều khoản hợp đồng
        private decimal _LuongCoBan;
        private decimal _LuongKinhDoanh;
        private BacLuong _BacLuong;
        private NgachLuong _NgachLuong;
        private DateTime _NgayHuongLuong;
        private decimal _PhuCapTienXang;
        private decimal _PhuCapTienAn;
        private decimal _PhuCapDienThoai;
        private decimal _PhuCapChucVu;
        private decimal _TongLuong;
        private string _NoiDungDieuKhoanThayDoi;
        private string _DieuThayDoi;
        private string _KhoanThayDoi;
        private string _DieuKhoanThayDoi;
        private HopDongLamViec _HopDongLaoDong;

        //dữ liệu cũ
        private decimal _PhanTramTinhLuongCu;
        private decimal _LuongCoBanCu;
        private decimal _LuongKinhDoanhCu;
        private NgachLuong _NgachLuongCu;
        private BacLuong _BacLuongCu;
        private DateTime _NgayHuongLuongCu;
        private string _LuongDoanhNghiepCu;
        private decimal _PhuCapChucVuCu;
        private decimal _PhuCapTienXangCu;
        private decimal _PhuCapTienAnCu;
        private decimal _PhuCapDienThoaiCu;
        private decimal _PhuCapDongPhucCu;
        private decimal _PhuCapKhacCu;
        private decimal _PhuCapTrachNhiemCu;
        private decimal _LuongHopDongCu;
        private LoaiHopDong _LoaiHopDongCu;
        private HopDong _ThongTinHopDongCu;
        //        
        [ModelDefault("Caption", "Khối")]
        [Browsable(false)]
        public LoaiKhoiHopDongEnum LoaiKhoiHopDong
        {
            get
            {
                return _LoaiKhoiHopDong;
            }
            set
            {
                SetPropertyValue("LoaiKhoiHopDong", ref _LoaiKhoiHopDong, value);
            }
        }

        [ModelDefault("Caption", "Không đóng BHXH")]
        public bool KhongDongBHXH
        {
            get
            {
                return _KhongDongBHXH;
            }
            set
            {
                SetPropertyValue("KhongDongBHXH", ref _KhongDongBHXH, value);
            }
        }

        [ModelDefault("Caption", "Không đóng BHYT")]
        public bool KhongDongBHYT
        {
            get
            {
                return _KhongDongBHYT;
            }
            set
            {
                SetPropertyValue("KhongDongBHYT", ref _KhongDongBHYT, value);
            }
        }

        [ModelDefault("Caption", "Không đóng BHTN")]
        public bool KhongDongBHTN
        {
            get
            {
                return _KhongDongBHTN;
            }
            set
            {
                SetPropertyValue("KhongDongBHTN", ref _KhongDongBHTN, value);
            }
        }

        [ModelDefault("Caption", "Không đóng CĐ")]
        public bool KhongDongCD
        {
            get
            {
                return _KhongDongCD;
            }
            set
            {
                SetPropertyValue("KhongDongCD", ref _KhongDongCD, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Hình thức hợp đồng")]
        [DataSourceProperty("HTHDList")]
   //     [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "LoaiHopDong != 'Phụ lục hợp đồng'")]
        public HinhThucHopDong HinhThucHopDong
        {
            get
            {
                return _HinhThucHopDong;
            }
            set
            {
                SetPropertyValue("HinhThucHopDong", ref _HinhThucHopDong, value);
                if (!IsLoading && value != null && TuNgay != DateTime.MinValue && HinhThucHopDong.CoThoiHan)
                    DenNgay = TuNgay.AddMonths(value.SoThang).AddDays(-1);
                else
                    DenNgay = DateTime.MinValue;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "HinhThucHopDong.CoThoiHan")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading && TuNgay != DateTime.MinValue)
                {
                    if (HinhThucHopDong != null && HinhThucHopDong.CoThoiHan)
                        DenNgay = TuNgay.AddMonths(HinhThucHopDong.SoThang).AddDays(-1);
                    else
                        DenNgay = DateTime.MinValue;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Lương chức danh")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LuongCoBan
        {
            get
            {
                return _LuongCoBan;
            }
            set
            {
                SetPropertyValue("LuongCoBan", ref _LuongCoBan, value);
            }
        }

        [ModelDefault("Caption", "Lương bổ sung (HQCV)")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LuongKinhDoanh
        {
            get
            {
                return _LuongKinhDoanh;
            }
            set
            {
                SetPropertyValue("LuongKinhDoanh", ref _LuongKinhDoanh, value);
            }
        }
        [NonPersistent]
        [ModelDefault("Caption", "Lương gộp")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LuongGop
        {
            get
            {
                return LuongCoBan + LuongKinhDoanh;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương")]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
                if (!IsLoading)
                {
                    BacLuong = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương")]
        [DataSourceProperty("NgachLuong.ListBacLuong")]
        public BacLuong BacLuong
        {
            get
            {
                return _BacLuong;
            }
            set
            {
                SetPropertyValue("BacLuong", ref _BacLuong, value);
                if (!IsLoading)
                {
                    if (value != null)
                    {
                        LuongCoBan = value.LuongCoBan;
                        LuongKinhDoanh = value.LuongKinhDoanh;
                    }
                }
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime NgayHuongLuong
        {
            get
            {
                return _NgayHuongLuong;
            }
            set
            {
                SetPropertyValue("NgayHuongLuong", ref _NgayHuongLuong, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp tiền ăn")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTienAn
        {
            get
            {
                return _PhuCapTienAn;
            }
            set
            {
                SetPropertyValue("PhuCapTienAn", ref _PhuCapTienAn, value);
            }
        }
        [ModelDefault("Caption", "Phụ cấp tiền điện thoại")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapDienThoai
        {
            get
            {
                return _PhuCapDienThoai;
            }
            set
            {
                SetPropertyValue("PhuCapDienThoai", ref _PhuCapDienThoai, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp tiền xăng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTienXang
        {
            get
            {
                return _PhuCapTienXang;
            }
            set
            {
                SetPropertyValue("PhuCapTienXang", ref _PhuCapTienXang, value);
            }
        }
        [ModelDefault("Caption", "Phụ cấp chức vụ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapChucVu
        {
            get
            {
                return _PhuCapChucVu;
            }
            set
            {
                SetPropertyValue("PhuCapChucVu", ref _PhuCapChucVu, value);
            }
        }
        [NonPersistent]
        [ModelDefault("Caption", "Tổng lương")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongLuong
        {
            get
            {
                return (LuongCoBan*PhanTramTinhLuong/100) + LuongKinhDoanh + PhuCapTienAn + PhuCapDienThoai + PhuCapTienXang;
            }
        }

        [ImmediatePostData]
        [Size(-1)]
        [ModelDefault("Caption", "Nội dung thay đổi")]        
        public string NoiDungDieuKhoanThayDoi
        {
            get
            {
                return _NoiDungDieuKhoanThayDoi;
            }
            set
            {
                SetPropertyValue("NoiDungDieuKhoanThayDoi", ref _NoiDungDieuKhoanThayDoi, value);
            }
        }
       
        [ModelDefault("Caption", "Điều thay đổi")]
        public string DieuThayDoi
        {
            get
            {
                return _DieuThayDoi;
            }
            set
            {
                SetPropertyValue("DieuThayDoi", ref _DieuThayDoi, value);
            }
        }
       
        [ModelDefault("Caption", "Khoản thay đổi")]
        public string KhoanThayDoi
        {
            get
            {
                return _KhoanThayDoi;
            }
            set
            {
                SetPropertyValue("KhoanThayDoi", ref _KhoanThayDoi, value);
            }
        }
     
        [ModelDefault("Caption", "Điều khoản thay đổi")]
        public string DieuKhoanThayDoi
        {
            get
            {
                return _DieuKhoanThayDoi;
            }
            set
            {
                SetPropertyValue("DieuKhoanThayDoi", ref _DieuKhoanThayDoi, value);              
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Hợp đồng lao động")]
        [DataSourceProperty("HTLVList")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "LoaiHopDong like 'Phụ lục hợp đồng'")]
        public HopDongLamViec HopDongLaoDong
        {
            get
            {
                return _HopDongLaoDong;
            }
            set
            {
                SetPropertyValue("HopDongLaoDong", ref _HopDongLaoDong, value);
                if (!IsLoading && value != null)
                {
                    HinhThucHopDong = value.HinhThucHopDong;
                    TuNgay = value.TuNgay;
                    DenNgay = value.DenNgay;
                }
            }
        }


        [ModelDefault("Caption", "Mức hưởng lương")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public Decimal PhanTramTinhLuong
        {
            get
            {
                return _PhanTramTinhLuong;
            }
            set
            {
                SetPropertyValue("PhanTramTinhLuong", ref _PhanTramTinhLuong, value);
            }
        }
        #region Lưu vết cũ
        [Browsable(false)]
        [ModelDefault("Caption", "mức hưởng lương cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhanTramTinhLuongCu
        {
            get
            {
                return _PhanTramTinhLuongCu;
            }
            set
            {
                SetPropertyValue("PhanTramTinhLuongCu", ref _PhanTramTinhLuongCu, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Lương chức danh cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LuongCoBanCu
        {
            get
            {
                return _LuongCoBanCu;
            }
            set
            {
                SetPropertyValue("LuongCoBanCu", ref _LuongCoBanCu, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Lương bổ sung (HQCV) cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LuongKinhDoanhCu
        {
            get
            {
                return _LuongKinhDoanhCu;
            }
            set
            {
                SetPropertyValue("LuongKinhDoanhCu", ref _LuongKinhDoanhCu, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Ngạch lương cũ")]
        public NgachLuong NgachLuongCu
        {
            get
            {
                return _NgachLuongCu;
            }
            set
            {
                SetPropertyValue("NgachLuongCu", ref _NgachLuongCu, value);

            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Bậc lương cũ")]
        public BacLuong BacLuongCu
        {
            get
            {
                return _BacLuongCu;
            }
            set
            {
                SetPropertyValue("BacLuongCu", ref _BacLuongCu, value);

            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Ngày hưởng lương cũ")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime NgayHuongLuongCu
        {
            get
            {
                return _NgayHuongLuongCu;
            }
            set
            {
                SetPropertyValue("NgayHuongLuongCu", ref _NgayHuongLuongCu, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Phụ cấp chức vụ cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapChucVuCu
        {
            get
            {
                return _PhuCapChucVuCu;
            }
            set
            {
                SetPropertyValue("PhuCapChucVuCu", ref _PhuCapChucVuCu, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Phụ cấp tiền ăn cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTienAnCu
        {
            get
            {
                return _PhuCapTienAnCu;
            }
            set
            {
                SetPropertyValue("PhuCapTienAnCu", ref _PhuCapTienAnCu, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Phụ cấp điện thoại cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapDienThoaiCu
        {
            get
            {
                return _PhuCapDienThoaiCu;
            }
            set
            {
                SetPropertyValue("PhuCapDienThoaiCu", ref _PhuCapDienThoaiCu, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Phụ cấp tiền xăng cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTienXangCu
        {
            get
            {
                return _PhuCapTienXangCu;
            }
            set
            {
                SetPropertyValue("PhuCapTienXangCu", ref _PhuCapTienXangCu, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Phụ cấp đồng phục cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapDongPhucCu
        {
            get
            {
                return _PhuCapDongPhucCu;
            }
            set
            {
                SetPropertyValue("PhuCapDongPhucCu", ref _PhuCapDongPhucCu, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Phụ cấp trách nhiệm cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTrachNhiemCu
        {
            get
            {
                return _PhuCapTrachNhiemCu;
            }
            set
            {
                SetPropertyValue("PhuCapTrachNhiemCu", ref _PhuCapTrachNhiemCu, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Phụ cấp khác cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapKhacCu
        {
            get
            {
                return _PhuCapKhacCu;
            }
            set
            {
                SetPropertyValue("PhuCapKhacCu", ref _PhuCapKhacCu, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Lương hợp đồng cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LuongHopDongCu
        {
            get
            {
                return _LuongHopDongCu;
            }
            set
            {
                SetPropertyValue("LuongHopDongCu", ref _LuongHopDongCu, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Loại hợp đồng cũ")]
        public LoaiHopDong LoaiHopDongCu
        {
            get
            {
                return _LoaiHopDongCu;
            }
            set
            {
                SetPropertyValue("LoaiHopDongCu", ref _LoaiHopDongCu, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Hợp đồng cũ")]
        public HopDong ThongTinHopDongCu
        {
            get
            {
                return _ThongTinHopDongCu;
            }
            set
            {
                SetPropertyValue("ThongTinHopDongCu", ref _ThongTinHopDongCu, value);
            }
        }
        #endregion

        [Browsable(false)]
        public XPCollection<HinhThucHopDong> HTHDList { get; set; }

        [Browsable(false)]
        public XPCollection<HopDongLamViec> HTLVList { get; set; }

        public HopDongLamViec(Session session) : base(session) { }

        protected override void TaoSoHopDong()
        {
            if (QuanLyHopDong != null)
            {
                SqlParameter param = new SqlParameter("@QuanLyHopDong", QuanLyHopDong.Oid);
                //
                if (LoaiHopDong != null && SoHopDong == null)
                {
                    if (LoaiHopDong.TenLoaiHopDong.Contains("Hợp đồng lao động"))
                        SoHopDong = ManageKeyFactory.ManageKeyCompany(ManageKeyEnum.SoHopDongLamViec, QuanLyHopDong.CongTy, param);
                    else if (LoaiHopDong.TenLoaiHopDong.Contains("Hợp đồng thử việc"))
                        SoHopDong = ManageKeyFactory.ManageKeyCompany(ManageKeyEnum.SoHopDongThuViec, QuanLyHopDong.CongTy, param);
                    else if (LoaiHopDong.TenLoaiHopDong.Contains("Phụ lục hợp đồng"))
                        SoHopDong = ManageKeyFactory.ManageKeyCompany(ManageKeyEnum.SoPhuLucHopDong, QuanLyHopDong.CongTy, param);
                    else
                        SoHopDong = string.Empty;
                }
            }
        }

        protected override void AfterNhanVienChanged()
        {
            if (ThongTinNhanVien != null)
            {
                //
                ChucDanh = ThongTinNhanVien.ChucDanh;
                NgachLuong = ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong;
                BacLuong = ThongTinNhanVien.NhanVienThongTinLuong.BacLuong;
                NgayHuongLuong = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong;
                LuongCoBan = ThongTinNhanVien.NhanVienThongTinLuong.LuongCoBan;
                LuongKinhDoanh = ThongTinNhanVien.NhanVienThongTinLuong.LuongKinhDoanh;
                PhuCapTienAn = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienAn;
                PhuCapTienXang = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienXang;
                PhuCapDienThoai = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapDienThoai;
                PhuCapChucVu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapChucVu;
                LoaiHopDongLuuTru = ThongTinNhanVien.LoaiHopDong;
                //
                KhongDongBHXH = ThongTinNhanVien.NhanVienThongTinLuong.KhongDongBHXH;
                KhongDongBHYT = ThongTinNhanVien.NhanVienThongTinLuong.KhongDongBHYT;
                KhongDongBHTN = ThongTinNhanVien.NhanVienThongTinLuong.KhongDongBHTN;
                KhongDongCD = ThongTinNhanVien.NhanVienThongTinLuong.KhongDongCongDoan;

                // dữ liệu cũ mới thêm
                LuongCoBanCu = ThongTinNhanVien.NhanVienThongTinLuong.LuongCoBan;
                LuongKinhDoanhCu = ThongTinNhanVien.NhanVienThongTinLuong.LuongKinhDoanh;
                NgachLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong;
                BacLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.BacLuong;
                NgayHuongLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong;
                PhanTramTinhLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.PhanTramTinhLuong;
                PhuCapTienAnCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienAn;
                PhuCapDienThoaiCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapDienThoai;
                PhuCapTienXangCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienXang;
                PhuCapTrachNhiemCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTrachNhiem;
                LuongHopDongCu = ThongTinNhanVien.NhanVienThongTinLuong.LuongKhoan;
                LoaiHopDongCu = ThongTinNhanVien.LoaiHopDong;
                ThongTinHopDongCu = ThongTinNhanVien.HopDongHienTai;
                PhuCapChucVuCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapChucVu;

                if (HTLVList == null)
                    HTLVList = new XPCollection<HopDongLamViec>(Session);
                //
                if (ThongTinNhanVien != null)
                    HTLVList.Criteria = CriteriaOperator.Parse("ThongTinNhanVien.Oid = ? and HopDongCu = false", this.ThongTinNhanVien.Oid);
            }
        }
        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //
                if (NgayKy <= Common.GetServerCurrentTime())
                {
                    //Cập nhất thông tin hồ sơ
                    //Lương theo ngạch bậc
                    ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuong;
                    ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuong;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhanTramTinhLuong = PhanTramTinhLuong;
                    ThongTinNhanVien.NhanVienThongTinLuong.LuongCoBan = LuongCoBan;
                    ThongTinNhanVien.NhanVienThongTinLuong.LuongKinhDoanh = LuongKinhDoanh;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuong;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienAn = PhuCapTienAn;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienXang = PhuCapTienXang;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhuCapDienThoai = PhuCapDienThoai;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhuCapChucVu = PhuCapChucVu;
                    ThongTinNhanVien.LoaiHopDong = LoaiHopDong;
                    ThongTinNhanVien.HopDongHienTai = this;
                    ///JobUpdated = true;

                }
            }
        }
        protected override void OnDeleting()
        {
            if (!IsSaving)
            {


                //Cập nhất thông tin hồ sơ
                //Lương theo ngạch bậc
                ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuongCu;
                ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuongCu;
                ThongTinNhanVien.NhanVienThongTinLuong.PhanTramTinhLuong = PhanTramTinhLuongCu;
                ThongTinNhanVien.NhanVienThongTinLuong.LuongCoBan = LuongCoBanCu;
                ThongTinNhanVien.NhanVienThongTinLuong.LuongKinhDoanh = LuongKinhDoanhCu;
                ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuongCu;
                ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienAn = PhuCapTienAnCu;
                ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienXang = PhuCapTienXangCu;
                ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTrachNhiem = PhuCapTrachNhiemCu;
                ThongTinNhanVien.NhanVienThongTinLuong.PhuCapDienThoai = PhuCapDienThoaiCu;
                ThongTinNhanVien.NhanVienThongTinLuong.PhuCapChucVu = PhuCapChucVuCu;
                if (LoaiHopDongCu != null)
                {
                    ThongTinNhanVien.LoaiHopDong = LoaiHopDongCu;
                }
                //else
                //{
                //    ThongTinNhanVien.LoaiHopDong =null;
                //}
                if (ThongTinHopDongCu != null)
                {
                    ThongTinNhanVien.HopDongHienTai = ThongTinHopDongCu;
                }
                //else
                //{
                //    ThongTinNhanVien.HopDongHienTai = null;
                //}


                //Lương theo hợp đồng
                ThongTinNhanVien.NhanVienThongTinLuong.LuongKhoan = LuongHopDongCu;
            }
            base.OnDeleting();
        }

        protected override void AfterLoaiHopDongChanged()
        {
            if(HTHDList ==null)
               HTHDList = new XPCollection<HinhThucHopDong>(Session);
            //
            if(this.LoaiHopDong!=null)
            HTHDList.Criteria = CriteriaOperator.Parse("LoaiHopDong.Oid=?",this.LoaiHopDong.Oid);
            //
            ///HinhThucHopDong = null;
        }

        protected override void AfterGiayToHoSo()
        {
            base.AfterNhanVienChanged();
        }
      
    }

}
