using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo.Metadata;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;
using ERP.Module.NghiepVu.MaTuDong;
using ERP.Module.Enum.NhanSu;
using ERP.Module.Commons;
using ERP.Module.HeThong;
using ERP.Module.DanhMuc.TienLuong;
using System.Data.SqlClient;
using System.Data;
using ERP.Module.Enum.Systems;
using ERP.Module.CauHinhChungs;
//
namespace ERP.Module.NghiepVu.NhanSu.NhanViens
{
    [ImageName("BO_Resume")]
    [ModelDefault("Caption", "Thông tin nhân viên")]
    [ModelDefault("EditorTypeName", "ERP.Module.Win.Editors.NhanSu.NhanVien.CategorizedListEditor_NhanVien")]

    #region Khóa hồ sơ
    [Appearance("ThongTinNhanVien.HoSo", TargetItems = "*", Enabled = false, Criteria = "KhoaHoSo")]
    [Appearance("ThongTinNhanVien.LuongAndTrinhDo", TargetItems = "NhanVienThongTinLuong.NgachLuong;NhanVienThongTinLuong.NgayBoNhiemNgach;NhanVienThongTinLuong.NgayHuongLuong;NhanVienThongTinLuong.BacLuong;NhanVienThongTinLuong.HeSoLuong;NhanVienThongTinLuong.HSPCChucVu;NhanVienThongTinLuong.NgayHuongHSPCChucVu;NhanVienThongTinLuong.HSPCKhac;NhanVienThongTinLuong.VuotKhung;NhanVienThongTinLuong.HSPCVuotKhung;NhanVienThongTinLuong.ThamNien;NhanVienThongTinLuong.HSPCThamNien;NhanVienThongTinLuong.SoNguoiPhuThuoc;NhanVienThongTinLuong.SoThangGiamTru;NhanVienThongTinLuong.MocNangLuong;NhanVienThongTinLuong.MaSoThue;NhanVienTrinhDo.TrinhDoVanHoa;NhanVienTrinhDo.TrinhDoChuyenMon;NhanVienTrinhDo.ChuyenNganhDaoTao;NhanVienTrinhDo.NganhDaoTao;NhanVienTrinhDo.TruongDaoTao;NhanVienTrinhDo.HinhThucDaoTao;NhanVienTrinhDo.NamTotNghiep;NhanVienTrinhDo.TrinhDoTinHoc;NhanVienTrinhDo.NgoaiNgu;NhanVienTrinhDo.TrinhDoNgoaiNgu", Enabled = false, Criteria = "KhoaHoSo")]
    #endregion

    #region Khóa Mã tập đoàn nếu là hồ sơ được copy
    [Appearance("HoSo.OidBoPhanCha", TargetItems = "MaTapDoan", Enabled = false, Criteria = "OidHoSoCha <> '00000000-0000-0000-0000-000000000000'")]
    #endregion

    #region Ẩn hiện theo nghiệp vụ
    [Appearance("ThongTinNhanVien.AnTaiBoMon", TargetItems = "TaiBoMon", Visibility = ViewItemVisibility.Hide, Criteria = "!GiangDay")]
    [Appearance("ThongTinNhanVien.AnMonHoc", TargetItems = "MonHoc", Visibility = ViewItemVisibility.Hide, Criteria = "!GiangDay")]
    #endregion

    #region Ẩn hiện theo mã trường
    [Appearance("ThongTinNhanVien.AnTruong_ManNon", TargetItems = "HocHam;HocVi;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'E12'")]
    [Appearance("ThongTinNhanVien.AnTruong_Yersin", TargetItems = "ListQuaTrinhGiangDay;NhanVienThongTinLuong.HieuQuaCongViec;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'E12'")]
    //[Appearance("ThongTinNhanVien.AnTruong_KhacYersin", TargetItems = "TinhThinhGiang;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong <> 'E12'")]
    [Appearance("ThongTinNhanVien.AnTruong_MonHoc", TargetItems = "MonHoc", Enabled = false, Criteria = "MaTapDoan Like 'E10%' Or (MaTapDoan Like 'E12%' And MaTapDoan Not Like 'E12-121%')")]//bỏ CĐ và ĐH
    #endregion

    #region Ẩn theo phân loại lương
    //[Appearance("Hide_LuongNgachBac", TargetItems = "NhanVienThongTinLuong.LuongKhoan", Visibility = ViewItemVisibility.Hide, Criteria = "NhanVienThongTinLuong.PhanLoaiLuong=1")]
    [Appearance("Hide_LuongKhoan", TargetItems = "NhanVienThongTinLuong.PhanTramTinhLuong;NhanVienThongTinLuong.LyDoDieuChinh;NhanVienThongTinLuong.MocNangLuongDieuChinh;NhanVienThongTinLuong.NgayBoNhiemNgach;NhanVienThongTinLuong.NgachLuong;NhanVienThongTinLuong.BacLuong;NhanVienThongTinLuong.LuongCoBan;NhanVienThongTinLuong.LuongKinhDoanh;NhanVienThongTinLuong.MocNangLuongLanSau;NhanVienThongTinLuong.LuongGop", Visibility = ViewItemVisibility.Hide, Criteria = "NhanVienThongTinLuong.PhanLoaiLuong=2")]
    #endregion

    #region Ẩn theo loại thuế
    [Appearance("Hide_TinhThueMacDinh", TargetItems = "NhanVienThongTinLuong.PhanTramTinhThue", Visibility = ViewItemVisibility.Hide, Criteria = "NhanVienThongTinLuong.TinhThueMacDinh")]
   #endregion

    public class ThongTinNhanVien : NhanVien
    {
        // 
        private bool _GiangDay = false;
        private BoPhan _TaiBoMon;
        private To _To;
        private bool _KhoaHoSo;
        private LoaiNhanSu _LoaiNhanSu;
        private ChucVu _ChucVu;
        private DateTime _NgayBoNhiemChucVu;       
        //private ChucVu _ChucVuDang;
        private ChucDanh _ChucDanhDang;
        private DateTime _NgayVaoDang;
        //private ChucVu _ChucVuDoan;
        private ChucDanh _ChucDanhDoan;
        private DateTime _NgayVaoDoan;
        private bool _ChamCong;
        private string _IDChamCong;
        private LoaiGioLamViec _LoaiGioLamViec;
        private LoaiChamCongEnum _LoaiChamCong = LoaiChamCongEnum.CongHanhChinh;
        private PhanLoaiNhanSu _PhanLoaiNhanSu;


        //Phục vụ tập đoàn
        private NhomPhanBo _NhomPhanBo;
        private KhoiTapDoan _KhoiTapDoan;
        private DateTime _NgayVaoTapDoan;
        private ChucDanhTapDoan _ChucDanhTapDoan;
        private bool _Create = false;

       

        //ThuHuong _ Ngiệp vụ PMS Yersin : Xác định những giảng viên cơ hữu nhưng tính thù lao như giảng viên thỉnh giảng 
        private bool _TinhThinhGiang;


        [ModelDefault("Caption", "Phân loại nhân sự*")]
        public PhanLoaiNhanSu PhanLoaiNhanSu
        {
            get
            {
                return _PhanLoaiNhanSu;
            }
            set
            {
                SetPropertyValue("PhanLoaiNhanSu", ref _PhanLoaiNhanSu, value);
            }
        }




        //Mon hoc
        private MonHoc _MonHoc;

        //
        [Browsable(false)]
        [ModelDefault("Caption", "Khóa hồ sơ")]
        public bool KhoaHoSo
        {
            get
            {
                return _KhoaHoSo;
            }
            set
            {
                SetPropertyValue("KhoaHoSo", ref _KhoaHoSo, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Giảng dạy")]
        public bool GiangDay
        {
            get
            {
                return _GiangDay;
            }
            set
            {
                SetPropertyValue("GiangDay", ref _GiangDay, value);
            }
        }

        [ModelDefault("Caption", "Tại Khoa/Bộ môn")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "GiangDay")]
        public BoPhan TaiBoMon
        {
            get
            {
                return _TaiBoMon;
            }
            set
            {
                SetPropertyValue("TaiBoMon", ref _TaiBoMon, value);
            }
        }

        [ModelDefault("Caption", "Tổ")]       
        public To To
        {
            get
            {
                return _To;
            }
            set
            {
                SetPropertyValue("To", ref _To, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Môn")]
        public MonHoc MonHoc
        {
            get
            {
                return _MonHoc;
            }
            set
            {
                SetPropertyValue("MonHoc", ref _MonHoc, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Loại nhân sự")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiNhanSu LoaiNhanSu
        {
            get
            {
                return _LoaiNhanSu;
            }
            set
            {
                SetPropertyValue("LoaiNhanSu", ref _LoaiNhanSu, value);
                if (!IsLoading && value != null)
                {
                    if (value.TenLoaiNhanSu.Contains("Giảng viên") || value.TenLoaiNhanSu.Contains("Giáo viên"))
                        GiangDay = true;
                    else
                        GiangDay = false;
                }
            }
        }

        [ModelDefault("Caption", "Loại giờ làm việc")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiGioLamViec LoaiGioLamViec
        {
            get
            {
                return _LoaiGioLamViec;
            }
            set
            {
                SetPropertyValue("LoaiGioLamViec", ref _LoaiGioLamViec, value);
            }
        }


        [ModelDefault("Caption", "Loại chấm công")]
        [Browsable(false)]
        public LoaiChamCongEnum LoaiChamCong
        {
            get
            {
                return _LoaiChamCong;
            }
            set
            {
                SetPropertyValue("LoaiChamCong", ref _LoaiChamCong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ")]
        [DataSourceProperty("CVList")]//Nguyen
        public ChucVu ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);

                //if (!IsLoading)
                //{
                //    CapNhatChucDanh(value);
                //}
            }
        }

        //Nguyen
        [Browsable(false)]
        public XPCollection<ChucVu> CVList { get; set; }

        public void CapNhatChucVu()
        {
            if (CVList == null)
                CVList = new XPCollection<ChucVu>(Session, false);
            else
                CVList.Reload();
            //
            CriteriaOperator filter = CriteriaOperator.Parse("isnull(KhongConHieuLuc,0) = 0 ");
            XPCollection<ChucVu> listChucVu = new XPCollection<ChucVu>(Session, filter);
            foreach (ChucVu item in listChucVu)
            {
                CVList.Add(item);
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm chức vụ")]
        public DateTime NgayBoNhiemChucVu
        {
            get
            {
                return _NgayBoNhiemChucVu;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemChucVu", ref _NgayBoNhiemChucVu, value);
            }
        }     

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức danh Đảng")]        
        [DataSourceProperty("CDDangList")]
        public ChucDanh ChucDanhDang
        {
            get
            {
                return _ChucDanhDang;
            }
            set
            {
                SetPropertyValue("ChucDanhDang", ref _ChucDanhDang, value);               
            }
        }

        [ModelDefault("Caption", "Ngày vào Đảng")]
        public DateTime NgayVaoDang
        {
            get
            {
                return _NgayVaoDang;
            }
            set
            {
                SetPropertyValue("NgayVaoDang", ref _NgayVaoDang, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức danh Đoàn")]
        [DataSourceProperty("CDDoanList")]
        public ChucDanh ChucDanhDoan
        {
            get
            {
                return _ChucDanhDoan;
            }
            set
            {
                SetPropertyValue("ChucDanhDoan", ref _ChucDanhDoan, value);               
            }
        }

        [ModelDefault("Caption", "Ngày vào Đoàn")]
        public DateTime NgayVaoDoan
        {
            get
            {
                return _NgayVaoDoan;
            }
            set
            {
                SetPropertyValue("NgayVaoDoan", ref _NgayVaoDoan, value);
            }
        }

        [ModelDefault("Caption", "Chấm công")]
        public bool ChamCong
        {
            get
            {
                return _ChamCong;
            }
            set
            {
                SetPropertyValue("ChamCong", ref _ChamCong, value);
            }
        }

        [ModelDefault("Caption", "Id chấm công")]
        public string IDChamCong
        {
            get
            {
                return _IDChamCong;
            }
            set
            {
                SetPropertyValue("IDChamCong", ref _IDChamCong, value);
            }
        }
        
        [ModelDefault("Caption", "Khối tập đoàn")]
        public KhoiTapDoan KhoiTapDoan
        {
            get
            {
                return _KhoiTapDoan;
            }
            set
            {
                SetPropertyValue("KhoiTapDoan", ref _KhoiTapDoan, value);
            }
        }

        [ModelDefault("Caption", "Chức danh tập đoàn")]
        public ChucDanhTapDoan ChucDanhTapDoan
        {
            get
            {
                return _ChucDanhTapDoan;
            }
            set
            {
                SetPropertyValue("ChucDanhTapDoan", ref _ChucDanhTapDoan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày vào tập đoàn")]
        public DateTime NgayVaoTapDoan
        {
            get
            {
                return _NgayVaoTapDoan;
            }
            set
            {
                SetPropertyValue("NgayVaoTapDoan", ref _NgayVaoTapDoan, value);
            }
        }

        [ModelDefault("Caption", "Nhóm phân bổ")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public NhomPhanBo NhomPhanBo
        {
            get
            {
                return _NhomPhanBo;
            }
            set
            {
                SetPropertyValue("NhomPhanBo", ref _NhomPhanBo, value);
            }
        }

        [ModelDefault("Caption", "Tính thỉnh giảng")]
        public bool TinhThinhGiang
        {
            get
            {
                return _TinhThinhGiang;
            }
            set
            {
                SetPropertyValue("TinhThinhGiang", ref _TinhThinhGiang, value);
            }
        }
        

        #region Danh sách quá trình
        [Aggregated]
        [Association("ThongTinNhanVien-ListLichSuBanThan")]
        [ModelDefault("Caption", "Lịch sử bản thân")]
        public XPCollection<LichSuBanThan> ListLichSuBanThan
        {
            get
            {
                return GetCollection<LichSuBanThan>("ListLichSuBanThan");
            }
        }

        [Aggregated]
        [Association("ThongTinNhanVien-ListDienBienLuong")]
        [ModelDefault("Caption", "Diễn biến lương")]
        public XPCollection<DienBienLuong> ListDienBienLuong
        {
            get
            {
                return GetCollection<DienBienLuong>("ListDienBienLuong");
            }
        }

        [Aggregated]
        [Association("ThongTinNhanVien-ListQuaTrinhBoNhiem")]
        [ModelDefault("Caption", "Quá trình bổ nhiệm")]
        public XPCollection<QuaTrinhBoNhiem> ListQuaTrinhBoNhiem
        {
            get
            {
                return GetCollection<QuaTrinhBoNhiem>("ListQuaTrinhBoNhiem");
            }
        }

        [Aggregated]
        [Association("ThongTinNhanVien-ListQuaTrinhBoNhiemKiemNhiem")]
        [ModelDefault("Caption", "Quá trình bổ nhiệm kiêm nhiệm")]
        public XPCollection<QuaTrinhBoNhiemKiemNhiem> ListQuaTrinhBoNhiemKiemNhiem
        {
            get
            {
                return GetCollection<QuaTrinhBoNhiemKiemNhiem>("ListQuaTrinhBoNhiemKiemNhiem");
            }
        }

        [Aggregated]
        [Association("ThongTinNhanVien-ListQuaTrinhCongTac")]
        [ModelDefault("Caption", "Quá trình công tác")]
        public XPCollection<QuaTrinhCongTac> ListQuaTrinhCongTac
        {
            get
            {
                return GetCollection<QuaTrinhCongTac>("ListQuaTrinhCongTac");
            }
        }

        [Aggregated]
        [Association("ThongTinNhanVien-ListQuaTrinhDieuDong")]
        [ModelDefault("Caption", "Quá trình điều động")]
        public XPCollection<QuaTrinhDieuDong> ListQuaTrinhDieuDong
        {
            get
            {
                return GetCollection<QuaTrinhDieuDong>("ListQuaTrinhDieuDong");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Hồ sơ bảo hiểm")]
        [Association("ThongTinNhanVien-ListHoSoBaoHiem")]
        public XPCollection<HoSoBaoHiem> ListHoSoBaoHiem
        {
            get
            {
                return GetCollection<HoSoBaoHiem>("ListHoSoBaoHiem");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Quá trình khen thưởng")]
        [Association("ThongTinNhanVien-ListQuaTrinhKhenThuong")]
        public XPCollection<QuaTrinhKhenThuong> ListQuaTrinhKhenThuong
        {
            get
            {
                return GetCollection<QuaTrinhKhenThuong>("ListQuaTrinhKhenThuong");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Quá trình kỷ luật")]
        [Association("ThongTinNhanVien-ListQuaTrinhKyLuat")]
        public XPCollection<QuaTrinhKyLuat> ListQuaTrinhKyLuat
        {
            get
            {
                return GetCollection<QuaTrinhKyLuat>("ListQuaTrinhKyLuat");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Quá trình đào tạo")]
        [Association("ThongTinNhanVien-ListQuaTrinhDaoTao")]
        public XPCollection<QuaTrinhDaoTao> ListQuaTrinhDaoTao
        {
            get
            {
                return GetCollection<QuaTrinhDaoTao>("ListQuaTrinhDaoTao");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Quá trình nghiên cứu khoa học")]
        [Association("ThongTinNhanVien-ListQuaTrinhNghienCuuKhoaHoc")]
        public XPCollection<QuaTrinhNghienCuuKhoaHoc> ListQuaTrinhNghienCuuKhoaHoc
        {
            get
            {
                return GetCollection<QuaTrinhNghienCuuKhoaHoc>("ListQuaTrinhNghienCuuKhoaHoc");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Quá trình tham gia lực lượng vũ trang")]
        [Association("ThongTinNhanVien-ListQuaTrinhThamGiaLucLuongVuTrang")]
        public XPCollection<QuaTrinhThamGiaLucLuongVuTrang> ListQuaTrinhThamGiaLucLuongVuTrang
        {
            get
            {
                return GetCollection<QuaTrinhThamGiaLucLuongVuTrang>("ListQuaTrinhThamGiaLucLuongVuTrang");
            }
        }
        #endregion

        public ThongTinNhanVien(Session session) : base(session) { }


        [NonPersistent]
        [Browsable(false)]
        public string MaTruong { get; set; }        

        [Browsable(false)]
        public XPCollection<ChucDanh> CDDangList { get; set; }

        [Browsable(false)]
        public XPCollection<ChucDanh> CDDoanList { get; set; }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //Tạo loại giấy tờ hồ sơ
            Common.CreateLoaiGiayToHoSo(Session, this);

            //Loại hồ sơ
            LoaiHoSo = LoaiHoSoEnum.NhanVien;

            // Mã trường
            CongTy congTy = Common.CongTy(Session);
            if (congTy != null)
            {
                CongTy = congTy;
                MaTruong = congTy.MaQuanLy;
            }

            // Tạo nhân sự hiển thị tạm lên form
            if (string.IsNullOrEmpty(MaNhanVien))
                MaNhanVien = ManageKeyFactory.ManageKey(ManageKeyEnum.MaNhanVien);

            if(CongTy.CauHinhChung != null)
            {
                if(CongTy.CauHinhChung.CauHinhHoSo != null)
                {
                    if(CongTy.CauHinhChung.CauHinhHoSo.LoaiGioLamViec != null)
                    {
                        LoaiGioLamViec = CongTy.CauHinhChung.CauHinhHoSo.LoaiGioLamViec; //Session.FindObject<LoaiGioLamViec>(CriteriaOperator.Parse("SoGio=8"));
                    }
                }

            }

            _Create = true;      
        }
        public void OnLoad()
        {
            // Mã trường
            CongTy congTy = Common.CongTy(Session);
            if (congTy != null)
                MaTruong = congTy.MaQuanLy;
            //
            //CapNhatChucDanh(ChucVu);
            CapNhatChucDanhDang();
            CapNhatChucDanhDoan();
        }

        //protected override void OnLoaded()
        //{
        //    base.OnLoaded();
        //    CapNhatChucVu();
        //}

        //protected override void OnLoaded()
        //{
        //    base.OnLoaded();

        //    // Mã trường
        //    CongTy congTy = Common.CongTy(Session);
        //    if (congTy != null)
        //        MaTruong = congTy.MaQuanLy;
        //    //
        //    CapNhatChucDanh();
        //}
        
        protected override void OnSaving()
        {
            base.OnSaving();
            if (!IsDeleted)
            {
                //1. Tạo nhân sự (Trường họp sau khi tạo mới form mà có người lưu mã trước thì cập nhật mã form này lại)
                if (_Create == true && OidHoSoCha == Guid.Empty)
                {
                    MaNhanVien = ManageKeyFactory.ManageKey(ManageKeyEnum.MaNhanVien);
                    _Create = false;
                }

                //2. Tạo mã tập đoàn (Mã trường + Mã phòng + CMND)
                if (string.IsNullOrEmpty(MaTapDoan))
                    MaTapDoan = string.Format("{0}-{1}-{2}", CongTy.MaBoPhan != null ? CongTy.MaBoPhan.ToUpper() : string.Empty, BoPhan != null ? BoPhan.MaBoPhan.ToUpper() : string.Empty, CMND != null ? CMND.ToUpper() : string.Empty);

                //3. Xử lý tài khoản sử dụng
                //if(TinhTrang != null)
                //   CapNhatThongTinTaiKhoan();

                CongTy = BoPhan.CongTy;
            }
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            //
            //Cập nhật tài khoản web
            if (OidHoSoCha == Guid.Empty)
            {
                //CapNhatTaiKhoanWebHRM_WebManNon();
                //CapNhatTaiKhoanWinERP();
                //DongBoCoHuuSIS();
            }
        }

        public void CapNhatChucDanhDoan()
        {
            if (CDDoanList == null)
                CDDoanList = new XPCollection<ChucDanh>(Session);
            //          
            //if (nhanVien != null && nhanVien.ChucVu != null)
                CDDoanList.Filter = CriteriaOperator.Parse("ChucVu.TenChucVu like '%Đoàn%'");
        }

        public void CapNhatChucDanhDang()
        {
            if (CDDangList == null)
                CDDangList = new XPCollection<ChucDanh>(Session);
            //          
            //if (nhanVien != null && nhanVien.ChucVu != null)
                CDDangList.Filter = CriteriaOperator.Parse("ChucVu.TenChucVu like '%Đảng%'");
        }

        //void CapNhatThongTinTaiKhoan()
        //{
        //    CauHinhChung cauHinhChung = Common.CauHinhChung_GetCauHinhChung;
        //    //
        //    if (OidHoSoCha == Guid.Empty && cauHinhChung != null && cauHinhChung.CauHinhXacThuc.DongBoTaiKhoan) //Chỉ tạo tài khoản ở tài khoản cha
        //    {
        //        //1. Cập nhật tài khoản - ERP
        //        CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", this.Oid);
        //        SecuritySystemUser_Custom taiKhoan = Session.FindObject<SecuritySystemUser_Custom>(filter);
        //        if (taiKhoan == null)
        //        {
        //            taiKhoan = new SecuritySystemUser_Custom(Session);
        //            taiKhoan.UserName = this.MaNhanVien;
        //            taiKhoan.Password = this.MaNhanVien;
        //            taiKhoan.CongTy = this.BoPhan.CongTy;//Lưu ý set công ty trước nếu hok bộ phận = null
        //            taiKhoan.BoPhan = this.BoPhan;
        //            taiKhoan.ThongTinNhanVien = this;
        //            taiKhoan.IsActive = true;
        //            taiKhoan.LoaiTaiKhoan = LoaiTaiKhoanEnum.TaiKhoanBinhThuong;
        //            //
        //            Session.Save(taiKhoan);
        //        }
        //        else
        //        {
        //            if (TinhTrang.DaNghiViec)
        //            {
        //                taiKhoan.IsActive = false;
        //            }
        //            else
        //            {
        //                taiKhoan.IsActive = true;
        //            }
        //        }
        //    }
        //}

        public void CapNhatTaiKhoanWebHRM_WebManNon()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@NhanVien", Oid);
            //
            DataProvider.ExecuteNonQuery("spd_HeThong_TaoTaiKhoanWeb", CommandType.StoredProcedure, parameter);
            //
        }

        public void CapNhatTaiKhoanWinERP()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@NhanVien", Oid);
            //
            DataProvider.ExecuteNonQuery("spd_HeThong_TaoTaiKhoanWin", CommandType.StoredProcedure, parameter);
            //
        }

        public void DongBoCoHuuSIS()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@NhanVien", Oid);
            //
            DataProvider.ExecuteNonQuery("spd_HeThong_DongBoCoHuuSIS", CommandType.StoredProcedure, parameter);
            //
        }       

        protected override void AfterNgayVaoTruongChanged()
        {            
            NgayVaoTapDoan = NgayVaoCongTy;
        }

        protected override void AfterBoPhanChanged()
        {
            if (BoPhan != null)
            {
                string tenBoPhan = BoPhan.TenBoPhan.Replace("Phòng ","").Replace("Bộ phận ", "").Replace("BP.","");
                CriteriaOperator filter = CriteriaOperator.Parse("TenNhomPhanBo like '%" + tenBoPhan + "%'");
                NhomPhanBo = Session.FindObject<NhomPhanBo>(filter);
            }
        }
    }
}
