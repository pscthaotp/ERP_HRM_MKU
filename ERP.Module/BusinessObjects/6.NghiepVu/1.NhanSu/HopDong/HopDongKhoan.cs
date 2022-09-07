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
using ERP.Module.NghiepVu.MaTuDong;
using ERP.Module.Enum.NhanSu;
using ERP.Module.Commons;
using DevExpress.Xpo.DB;
//
namespace ERP.Module.NghiepVu.NhanSu.HopDongs
{
    [ImageName("BO_Contract")]
    [DefaultProperty("SoHopDong")]
    [ModelDefault("Caption", "Hợp đồng khoán")]
    [Appearance("HopDongLamViec.CoTrongHoSo", TargetItems = "ThongTinNhanVien;BoPhan", Visibility = ViewItemVisibility.Hide, Criteria = "KhongCoTrongHoSo")]
    //
    [Appearance("HopDongLamViec.KhongCoTrongHoSo", TargetItems = "HoTen;DiaChiThuongTru", Visibility = ViewItemVisibility.Hide, Criteria = "!KhongCoTrongHoSo")]
    public class HopDongKhoan : HopDong
    {
        //
        private LoaiHopDongKhoanEnum _LoaiHopDongKhoan = LoaiHopDongKhoanEnum.Gross;
        private HinhThucHopDong _HinhThucHopDong;
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        //Điều khoản hợp đồng
        private decimal _LuongKhoan;
        private DateTime _NgayHuongLuong;
        private decimal _PhuCapTienXang;
        private decimal _PhuCapTienAn;
        private decimal _TienBHXH;
        private decimal _PhuCapChucVu;
        private decimal _PhuCapHocVi;
        private decimal _PhuCapHieuQuaCongViec;
        private decimal _PhuCapKiemNhiem;
        private decimal _PhuCapDienThoai;
        private decimal _TongLuong;
        //
        private string _HoTen;
        private string _DiaChiThuongTru;

        // lưu vết cũ
        private decimal _LuongKhoanCu;
        private HopDong _ThongTinHopDongCu;
        private DateTime _NgayHuongLuongCu;
        private decimal _PhuCapTienXangCu;
        private decimal _PhuCapTienAnCu;
        private decimal _TienBHXHCu;
        private decimal _PhuCapChucVuCu;
        private decimal _PhuCapHocViCu;
        private decimal _PhuCapHieuQuaCongViecCu;
        private decimal _PhuCapKiemNhiemCu;
        private decimal _PhuCapDienThoaiCu;

        //      
        [ModelDefault("Caption", "Loại khoán")]
        public LoaiHopDongKhoanEnum LoaiHopDongKhoan
        {
            get
            {
                return _LoaiHopDongKhoan;
            }
            set
            {
                SetPropertyValue("LoaiHopDongKhoan", ref _LoaiHopDongKhoan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Hình thức hợp đồng")]
        [DataSourceProperty("HTHDList")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "HinhThucHopDong.CoThoiHan")]
        public HinhThucHopDong HinhThucHopDong
        {
            get
            {
                return _HinhThucHopDong;
            }
            set
            {
                SetPropertyValue("HinhThucHopDong", ref _HinhThucHopDong, value);
                if (!IsLoading && value != null && TuNgay != DateTime.MinValue)
                    DenNgay = TuNgay.AddMonths(value.SoThang);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tên người lao động")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "KhongCoTrongHoSo")]
        public string HoTen
        {
            get
            {
                return _HoTen;
            }
            set
            {
                SetPropertyValue("HoTen", ref _HoTen, value);
            }
        }

        [ModelDefault("Caption", "Địa chỉ thường trú")]
        public string DiaChiThuongTru
        {
            get
            {
                return _DiaChiThuongTru;
            }
            set
            {
                SetPropertyValue("DiaChiThuongTru", ref _DiaChiThuongTru, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
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
                    if(HinhThucHopDong != null)
                    DenNgay = TuNgay.AddMonths(HinhThucHopDong.SoThang);
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

        [ModelDefault("Caption", "Lương khoán")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LuongKhoan
        {
            get
            {
                return _LuongKhoan;
            }
            set
            {
                SetPropertyValue("LuongKhoan", ref _LuongKhoan, value);
            }
        }


        [ModelDefault("Caption", "Ngày hưởng lương")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
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
        [ModelDefault("Caption", "Phụ cấp điện thoại")]
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
        [NonPersistent]
        [ModelDefault("Caption", "Tổng lương")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongLuong
        {
            get
            {
                return LuongKhoan + PhuCapTienAn + PhuCapTienXang + PhuCapKiemNhiem + PhuCapHocVi + PhuCapHieuQuaCongViec + PhuCapChucVu + TienBHXH + PhuCapDienThoai;
            }
        }
        [ModelDefault("Caption", "BHXH")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TienBHXH
        {
            get
            {
                return _TienBHXH;
            }
            set
            {
                SetPropertyValue("TienBHXH", ref _TienBHXH, value);
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
        [ModelDefault("Caption", "Phụ cấp ưu đãi(học vị)")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapHocVi
        {
            get
            {
                return _PhuCapHocVi;
            }
            set
            {
                SetPropertyValue("PhuCapHocVi", ref _PhuCapHocVi, value);
            }
        }
        [ModelDefault("Caption", "Phụ cấp ưu đãi(hiệu quả CV)")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapHieuQuaCongViec
        {
            get
            {
                return _PhuCapHieuQuaCongViec;
            }
            set
            {
                SetPropertyValue("PhuCapHieuQuaCongViec", ref _PhuCapHieuQuaCongViec, value);
            }
        }
        [ModelDefault("Caption", "Phụ cấp kiêm nhiệm")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapKiemNhiem
        {
            get
            {
                return _PhuCapKiemNhiem;
            }
            set
            {
                SetPropertyValue("PhuCapKiemNhiem", ref _PhuCapKiemNhiem, value);
            }
        }
        #region Lưu vết cũ
        [Browsable(false)]
        [ModelDefault("Caption", "Lương khoán cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LuongKhoanCu
        {
            get
            {
                return _LuongKhoanCu;
            }
            set
            {
                SetPropertyValue("LuongKhoanCu", ref _LuongKhoanCu, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Thông tin hợp đồng cũ")]
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
        [Browsable(false)]
        [ModelDefault("Caption", "Ngày hưởng lương cũ")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        //[RuleRequiredField(DefaultContexts.Save)]
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
        [ModelDefault("Caption", "BHXH cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TienBHXHCu
        {
            get
            {
                return _TienBHXHCu;
            }
            set
            {
                SetPropertyValue("TienBHXHCu", ref _TienBHXHCu, value);
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
        [ModelDefault("Caption", "Phụ cấp ưu đãi(học vị) cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapHocViCu
        {
            get
            {
                return _PhuCapHocViCu;
            }
            set
            {
                SetPropertyValue("PhuCapHocViCu", ref _PhuCapHocViCu, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Phụ cấp ưu đãi(hiệu quả CV) cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapHieuQuaCongViecCu
        {
            get
            {
                return _PhuCapHieuQuaCongViecCu;
            }
            set
            {
                SetPropertyValue("PhuCapHieuQuaCongViecCu", ref _PhuCapHieuQuaCongViecCu, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Phụ cấp kiêm nhiệm cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapKiemNhiemCu
        {
            get
            {
                return _PhuCapKiemNhiemCu;
            }
            set
            {
                SetPropertyValue("PhuCapKiemNhiemCu", ref _PhuCapKiemNhiemCu, value);
            }
        }
        #endregion



        [Browsable(false)]
        public XPCollection<HinhThucHopDong> HTHDList { get; set; }

        public HopDongKhoan(Session session) : base(session) { }
        
        protected override void TaoSoHopDong()
        {
            if (QuanLyHopDong != null)
            {
                SqlParameter param = new SqlParameter("@QuanLyHopDong", QuanLyHopDong.Oid);
                //
                SoHopDong = ManageKeyFactory.ManageKeyCompany(ManageKeyEnum.SoHopDongKhoan, QuanLyHopDong.CongTy, param);
            }
        }

        protected override void AfterNhanVienChanged()
        {
            if (ThongTinNhanVien != null)
            {
                //
                NgayHuongLuong = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong;
                LuongKhoan = ThongTinNhanVien.NhanVienThongTinLuong.LuongKhoan;
                LoaiHopDongLuuTru = ThongTinNhanVien.LoaiHopDong;
                PhuCapChucVu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapChucVu;
                PhuCapDienThoai = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapDienThoai;
                PhuCapTienAn = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienAn;
                PhuCapTienXang = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienXang;
                PhuCapHocVi = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapHocVi;
                PhuCapHieuQuaCongViec = ThongTinNhanVien.NhanVienThongTinLuong.HieuQuaCongViec;
                TienBHXH  = ThongTinNhanVien.NhanVienThongTinLuong.TienBHXH;

                // thông tin cũ
                ThongTinHopDongCu = ThongTinNhanVien.HopDongHienTai;
                NgayHuongLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong;
                LuongKhoanCu = ThongTinNhanVien.NhanVienThongTinLuong.LuongKhoan;
                PhuCapChucVuCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapChucVu;
                PhuCapDienThoaiCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapDienThoai;
                PhuCapTienAnCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienAn;
                PhuCapTienXangCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienXang;
                PhuCapHocViCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapHocVi;
                PhuCapHieuQuaCongViecCu = ThongTinNhanVien.NhanVienThongTinLuong.HieuQuaCongViec;
                TienBHXHCu = ThongTinNhanVien.NhanVienThongTinLuong.TienBHXH;
                PhuCapKiemNhiemCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem;
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
                    ThongTinNhanVien.NhanVienThongTinLuong.LuongKhoan = LuongKhoan;
                    ThongTinNhanVien.LoaiHopDong = LoaiHopDong;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhanLoaiLuong = PhanLoaiLuongEnum.LuongKhoan;
                    ThongTinNhanVien.HopDongHienTai = this;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuong;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhuCapChucVu = PhuCapChucVu;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhuCapDienThoai = PhuCapDienThoai;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhuCapHocVi = PhuCapHocVi;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem = PhuCapKiemNhiem;
                    ThongTinNhanVien.NhanVienThongTinLuong.HieuQuaCongViec = PhuCapHieuQuaCongViec;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienXang = PhuCapTienXang;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienAn = PhuCapTienAn;
                    ThongTinNhanVien.NhanVienThongTinLuong.TienBHXH = TienBHXH;

                }
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                //Kiểm tra xem hợp đồng này có phải mới nhất không
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
                SortProperty sort = new SortProperty("NgayKy", SortingDirection.Descending);
                using (XPCollection<HopDong> hopdong = new XPCollection<HopDong>(Session, filter, sort))
                {
                    hopdong.TopReturnedObjects = 1;
                    //
                    if (hopdong.Count > 0)
                    {
                        if (hopdong[0] == this)
                        {
                            //Cập nhất thông tin hồ sơ
                            //Lương theo hợp đồng
                            ThongTinNhanVien.NhanVienThongTinLuong.LuongKhoan = LuongKhoanCu;
                            ThongTinNhanVien.LoaiHopDong = LoaiHopDongLuuTru;
                            ThongTinNhanVien.NhanVienThongTinLuong.PhanLoaiLuong = PhanLoaiLuongEnum.LuongKhoan;
                            ThongTinNhanVien.HopDongHienTai = ThongTinHopDongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.PhuCapChucVu = PhuCapChucVuCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.PhuCapDienThoai = PhuCapDienThoaiCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.PhuCapHocVi = PhuCapHocViCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem = PhuCapKiemNhiemCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.HieuQuaCongViec = PhuCapHieuQuaCongViecCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienXang = PhuCapTienXangCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienAn = PhuCapTienAnCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.TienBHXH = TienBHXHCu;
                        }
                    }
                    else
                    {
                        //Cập nhất thông tin hồ sơ bằng rỗng hết
                        //Lương theo hợp đồng
                        ThongTinNhanVien.NhanVienThongTinLuong.LuongKhoan = 0;
                        ThongTinNhanVien.LoaiHopDong = null;
                        ThongTinNhanVien.NhanVienThongTinLuong.PhanLoaiLuong = PhanLoaiLuongEnum.LuongNgachBacGross;
                        ThongTinNhanVien.HopDongHienTai = null;
                        ThongTinNhanVien.NhanVienThongTinLuong.PhuCapChucVu = 0;
                        ThongTinNhanVien.NhanVienThongTinLuong.PhuCapDienThoai = 0;
                        ThongTinNhanVien.NhanVienThongTinLuong.PhuCapHocVi = 0;
                        ThongTinNhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem = 0;
                        ThongTinNhanVien.NhanVienThongTinLuong.HieuQuaCongViec = 0;
                        ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienXang = 0;
                        ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienAn = 0;
                        ThongTinNhanVien.NhanVienThongTinLuong.TienBHXH = 0;
                    }

                }
            }

            base.OnDeleting();
        }

        protected override void AfterLoaiHopDongChanged()
        {
            if(HTHDList ==null)
               HTHDList = new XPCollection<HinhThucHopDong>(Session);
            //
            if(this.LoaiHopDong!=null)
                HTHDList.Criteria = CriteriaOperator.Parse("LoaiHopDong.Oid=?", this.LoaiHopDong.Oid);
            //
            //HinhThucHopDong = null;
        }
    }

}
