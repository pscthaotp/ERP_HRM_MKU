using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.Xpo.DB;
using System.Data.SqlClient;
using ERP.Module.NghiepVu.MaTuDong;
using ERP.Module.Enum.NhanSu;
using ERP.Module.DanhMuc.PMS;
//
namespace ERP.Module.NghiepVu.NhanSu.HopDongs
{
    [ImageName("BO_Contract")]
    [DefaultProperty("SoHopDong")]
    [ModelDefault("Caption", "Hợp đồng thỉnh giảng")]
    [Appearance("HopDong.NgoaiCongTy", TargetItems = "NguoiKy", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiCongTy.TenLoaiCongTy like '%khác%'")]
    [Appearance("HopDong.CongTy", TargetItems = "TenNguoiKy", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiCongTy.TenLoaiCongTy not like '%khác%'")]
    public class HopDongThinhGiang : BaseObject
    {
        private QuanLyHopDongThinhGiang _QuanLyHopDongThinhGiang;
        private string _SoHopDong;
        private DateTime _NgayKy;
        private LoaiCongTy _LoaiCongTy;
        private string _TenCongTy;
        private PhanLoaiNguoiKy _PhanLoaiNguoiKy;
        private ThongTinNhanVien _NguoiKy;
        private ChucVuNguoiKy _ChucVuNguoiKy;
        private string _CanCu;
        private bool _HopDongCu;
        private BoPhan _BoPhan;
        private string _NoiLamViec;
        private GiangVienThinhGiang _GiangVienThinhGiang;
        private string _GhiChu;
        private string _TenNguoiKy;

        //Chi tiết hợp đồng

        private string _MaSoThue;
        private string _TenTaiKhoan;
        private string _SoTaiKhoan;
        private NganHang _NganHang;

        //

        private LopHocPhan _LopHocPhan;
        private decimal _SoTinChi;
        private decimal _SoTiet; //bao gồm tiết Lý thuyết và Thực hành 
        private decimal _SoBaiCham;
        private string _HuongDanDoAnTotNghiep;
        private string _MaLopSV;
        private string _TenLopSV;
        private decimal _SiSo;
        private decimal _ThuLaoGiangDay; 
        private decimal _ThucLanhThuLao;
        private decimal _SoLuotDiLai; 
        private decimal _DonGiaKhoangCach; 
        private decimal _ChiPhiTienXe; 
        private decimal _ChiPhiTienAn; 
        private string _ViTriHienTai;//Hiện tại chưa dùng 
        private bool _NgoaiDaLat;
        private bool _TrongDaLat;
        private bool _LoaiHocPhan_LT;
        private bool _LoaiHocPhan_TH;
        private bool _LoaiDoAnMonHoc;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Quản lý hợp đồng")]
        [Association("QuanLyHopDongThinhGiang-ListHopDong")]
        public QuanLyHopDongThinhGiang QuanLyHopDongThinhGiang
        {
            get
            {
                return _QuanLyHopDongThinhGiang;
            }
            set
            {
                SetPropertyValue("QuanLyHopDongThinhGiang", ref _QuanLyHopDongThinhGiang, value);
                if (!IsLoading && value != null)
                {
                    TaoSoHopDong();
                }
            }
        }

        [ModelDefault("Caption", "Số hợp đồng")]
        public string SoHopDong
        {
            get
            {
                return _SoHopDong;
            }
            set
            {
                SetPropertyValue("SoHopDong", ref _SoHopDong, value);
            }
        }

        [ModelDefault("Caption", "Ngày ký")]
        public DateTime NgayKy
        {
            get
            {
                return _NgayKy;
            }
            set
            {
                SetPropertyValue("NgayKy", ref _NgayKy, value);
            }
        }

        [ModelDefault("Caption", "Loại cơ quan")]
        public LoaiCongTy LoaiCongTy
        {
            get
            {
                return _LoaiCongTy;
            }
            set
            {
                SetPropertyValue("LoaiCongTy", ref _LoaiCongTy, value);

                if (!IsLoading)
                {
                    UpdatePhanLoaiNguoiKyList();
                    ChucVuNguoiKy = null;
                    PhanLoaiNguoiKy = null;
                    //
                    if (LoaiCongTy != null && (LoaiCongTy.TenLoaiCongTy.Contains("Trường") || LoaiCongTy.TenLoaiCongTy.Contains("Công ty")))
                    {
                        TenCongTy = Common.CongTy(Session).TenBoPhan;
                    }
                    else
                    {
                        TenCongTy = string.Empty;
                        NguoiKy = null;
                    }
                }
            }
        }

        [ModelDefault("Caption", "Tên Trường")]
        public string TenCongTy
        {
            get
            {
                return _TenCongTy;
            }
            set
            {
                SetPropertyValue("TenCongTy", ref _TenCongTy, value);
            }
        }

        [ModelDefault("Caption", "Phân loại người ký")]
        [DataSourceProperty("PhanLoaiNguoiKyList")]
        public PhanLoaiNguoiKy PhanLoaiNguoiKy
        {
            get
            {
                return _PhanLoaiNguoiKy;
            }
            set
            {
                SetPropertyValue("PhanLoaiNguoiKy", ref _PhanLoaiNguoiKy, value);
                if (!IsLoading)
                {
                    ChucVuNguoiKy = null;
                    NguoiKy = null;
                    //
                    UpdateNguoiKyList();
                }
            }
        }


        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ người ký")]
        public ChucVuNguoiKy ChucVuNguoiKy
        {
            get
            {
                return _ChucVuNguoiKy;
            }
            set
            {
                SetPropertyValue("ChucVuNguoiKy", ref _ChucVuNguoiKy, value);
                if (!IsLoading)
                {
                    NguoiKy = null;
                    //
                    UpdateNguoiKyList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Người ký")]
        [DataSourceProperty("NguoiKyList")]
        public ThongTinNhanVien NguoiKy
        {
            get
            {
                return _NguoiKy;
            }
            set
            {
                SetPropertyValue("NguoiKy", ref _NguoiKy, value);
            }
        }

        [ModelDefault("Caption", "Tên người ký")]
        public string TenNguoiKy
        {
            get
            {
                return _TenNguoiKy;
            }
            set
            {
                SetPropertyValue("TenNguoiKy", ref _TenNguoiKy, value);
            }
        }

        [ModelDefault("Caption", "Căn cứ")]
        public string CanCu
        {
            get
            {
                return _CanCu;
            }
            set
            {
                SetPropertyValue("CanCu", ref _CanCu, value);
            }
        }

        [Size(1000)]
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

        [ModelDefault("Caption", "Hết hiệu lực")]
        public bool HopDongCu
        {
            get
            {
                return _HopDongCu;
            }
            set
            {
                SetPropertyValue("HopDongCu", ref _HopDongCu, value);
            }
        }

        [ModelDefault("Caption", "Bộ phận")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Nơi làm việc")]
        public string NoiLamViec
        {
            get
            {
                return _NoiLamViec;
            }
            set
            {
                SetPropertyValue("NoiLamViec", ref _NoiLamViec, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Người lao động")]
        [RuleRequiredField(DefaultContexts.Save)]
        public GiangVienThinhGiang GiangVienThinhGiang
        {
            get
            {
                return _GiangVienThinhGiang;
            }
            set
            {
                SetPropertyValue("GiangVienThinhGiang", ref _GiangVienThinhGiang, value);
                if (!IsLoading && value != null)
                {
                    //xử lý khi giảng viên thay đổi
                    AfterGiangVienChanged();
                    //
                    if (BoPhan == null || BoPhan != value.BoPhan)
                        BoPhan = value.BoPhan;
                }
            }
        }

        #region Chi tiết hợp đồng

        [ModelDefault("Caption", "Mã số thuế")]
        public string MaSoThue
        {
            get
            {
                return _MaSoThue;
            }
            set
            {
                SetPropertyValue("MaSoThue", ref _MaSoThue, value);
            }
        }

        [ModelDefault("Caption", "Tên tài khoản")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public string TenTaiKhoan
        {
            get
            {
                return _TenTaiKhoan;
            }
            set
            {
                SetPropertyValue("TenTaiKhoan", ref _TenTaiKhoan, value);
            }
        }

        [ModelDefault("Caption", "Số tài khoản")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public string SoTaiKhoan
        {
            get
            {
                return _SoTaiKhoan;
            }
            set
            {
                SetPropertyValue("SoTaiKhoan", ref _SoTaiKhoan, value);
            }
        }

        [ModelDefault("Caption", "Ngân hàng")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public NganHang NganHang
        {
            get
            {
                return _NganHang;
            }
            set
            {
                SetPropertyValue("NganHang", ref _NganHang, value);
            }
        }

        //

        [ModelDefault("Caption", "Lớp học phần")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LopHocPhan LopHocPhan
        {
            get
            {
                return _LopHocPhan;
            }
            set
            {
                SetPropertyValue("LopHocPhan", ref _LopHocPhan, value);
            }
        }

        [ModelDefault("Caption", "Số tín chỉ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTinChi
        {
            get
            {
                return _SoTinChi;
            }
            set
            {
                SetPropertyValue("SoTinChi", ref _SoTinChi, value);
            }
        }

        [ModelDefault("Caption", "Số tiết")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTiet
        {
            get
            {
                return _SoTiet;
            }
            set
            {
                SetPropertyValue("SoTiet", ref _SoTiet, value);
            }
        }

        [ModelDefault("Caption", "Số bài chấm")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoBaiCham
        {
            get
            {
                return _SoBaiCham;
            }
            set
            {
                SetPropertyValue("SoBaiCham", ref _SoBaiCham, value);
            }
        }

        [ModelDefault("Caption", "Hướng dẫn đồ án tốt nghiệp")]
        [Size(-1)]
        public string HuongDanDoAnTotNghiep
        {
            get
            {
                return _HuongDanDoAnTotNghiep;
            }
            set
            {
                SetPropertyValue("HuongDanDoAnTotNghiep", ref _HuongDanDoAnTotNghiep, value);
            }
        }

        [ModelDefault("Caption", "Mã lớp SV")]
        [Size(-1)]
        public string MaLopSV
        {
            get { return _MaLopSV; }
            set 
            {
                SetPropertyValue("MaLopSV", ref _MaLopSV, value);
            }
        }

        [ModelDefault("Caption", "Tên lớp SV")]
        [Size(-1)]
        public string TenLopSV
        {
            get { return _TenLopSV; }
            set
            {
                SetPropertyValue("TenLopSV", ref _TenLopSV, value);
            }
        }

        [ModelDefault("Caption", "Sỉ số")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SiSo
        {
            get
            {
                return _SiSo;
            }
            set
            {
                SetPropertyValue("SiSo", ref _SiSo, value);
            }
        }

        [ModelDefault("Caption", "Thù lao giảng dạy")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal ThuLaoGiangDay
        {
            get
            {
                return _ThuLaoGiangDay;
            }
            set
            {
                SetPropertyValue("ThuLaoGiangDay", ref _ThuLaoGiangDay, value);
            }
        }

        [ModelDefault("Caption", "Thực lãnh thù lao")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal ThucLanhThuLao
        {
            get
            {
                return _ThucLanhThuLao;
            }
            set
            {
                SetPropertyValue("ThucLanhThuLao", ref _ThucLanhThuLao, value);
            }
        }

        [ModelDefault("Caption", "Số lượt đi lại")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoLuotDiLai
        {
            get
            {
                return _SoLuotDiLai;
            }
            set
            {
                SetPropertyValue("SoLuotDiLai", ref _SoLuotDiLai, value);
            }
        }
        
        [ModelDefault("Caption", "Đơn giá khoảng cách")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal DonGiaKhoangCach
        {
            get
            {
                return _DonGiaKhoangCach;
            }
            set
            {
                SetPropertyValue("DonGiaKhoangCach", ref _DonGiaKhoangCach, value);
            }
        }

        [ModelDefault("Caption", "Chi phí tiền xe")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal ChiPhiTienXe
        {
            get
            {
                return _ChiPhiTienXe;
            }
            set
            {
                SetPropertyValue("ChiPhiTienXe", ref _ChiPhiTienXe, value);
            }
        }

        [ModelDefault("Caption", "Chi phí tiền ăn")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal ChiPhiTienAn
        {
            get
            {
                return _ChiPhiTienAn;
            }
            set
            {
                SetPropertyValue("ChiPhiTienAn", ref _ChiPhiTienAn, value);
            }
        }

        [ModelDefault("Caption", "Vị trí hiện tại")]
        [Size(-1)]
        public string ViTriHienTai
        {
            get { return _ViTriHienTai; }
            set 
            {
                SetPropertyValue("ViTriHienTai", ref _ViTriHienTai, value);
            }
        }

        [ModelDefault("Caption", "Trong Đà Lạt")]
        [ImmediatePostData]
        public bool TrongDaLat
        {
            get
            {
                return _TrongDaLat;
            }
            set
            {
                SetPropertyValue("TrongDaLat", ref _TrongDaLat, value);
                if (!IsLoading && value == true)
                    NgoaiDaLat = false;
            }
        }

        [ModelDefault("Caption", "Ngoài Đà Lạt")]
        [ImmediatePostData]
        public bool NgoaiDaLat
        {
            get
            {
                return _NgoaiDaLat;
            }
            set
            {
                SetPropertyValue("NgoaiDaLat", ref _NgoaiDaLat, value);
                if (!IsLoading && value == true)
                    TrongDaLat = false;
            }
        }


        [ModelDefault("Caption", "Lý thuyết")]
        [ImmediatePostData]
        public bool LoaiHocPhan_LT
        {
            get
            {
                return _LoaiHocPhan_LT;
            }
            set
            {
                SetPropertyValue("LoaiHocPhan_LT", ref _LoaiHocPhan_LT, value);
                if (!IsLoading && value == true)
                {
                    LoaiHocPhan_TH = false;
                    LoaiDoAnMonHoc = false;
                }
                    
            }
        }

        [ModelDefault("Caption", "Thực hành")]
        [ImmediatePostData]
        public bool LoaiHocPhan_TH
        {
            get
            {
                return _LoaiHocPhan_TH;

            }
            set
            {
                SetPropertyValue("LoaiHocPhan_TH", ref _LoaiHocPhan_TH, value);
                if (!IsLoading && value == true)
                {
                    LoaiHocPhan_LT = false;
                    LoaiDoAnMonHoc = false;
                }
                    
            }
        }

        [ModelDefault("Caption", "Đồ án môn học")]
        [ImmediatePostData]
        public bool LoaiDoAnMonHoc
        {
            get
            {
                return _LoaiDoAnMonHoc;

            }
            set
            {
                SetPropertyValue("LoaiDoAnMonHoc", ref _LoaiDoAnMonHoc, value);
                if (!IsLoading && value == true)
                {
                    LoaiHocPhan_LT = false;
                    LoaiHocPhan_TH = false;
                }

            }
        }
        #endregion

        [Browsable(false)]
        public XPCollection<PhanLoaiNguoiKy> PhanLoaiNguoiKyList { get; set; }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NguoiKyList { get; set; }

        [Browsable(false)]
        public XPCollection<ChucVu> ChucVuList { get; set; }

        public HopDongThinhGiang(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // 
            if (Common.TaiKhoanEdu())
            {
                LoaiCongTy = Session.FindObject<LoaiCongTy>(CriteriaOperator.Parse("TenLoaiCongTy like ?", "%Công ty%"));
                PhanLoaiNguoiKy = Session.FindObject<PhanLoaiNguoiKy>(CriteriaOperator.Parse("TenPhanLoaiNguoiKy like ? and LoaiCongTy=?", "%đang tại chức%", LoaiCongTy != null ? LoaiCongTy.Oid : Guid.Empty));
                ChucVuNguoiKy = Session.FindObject<ChucVuNguoiKy>(CriteriaOperator.Parse("ChucVu.TenChucVu like ?", "Giám đốc"));
                //NguoiKy = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ChucDanh.Oid = ? and !TinhTrang.DaNghiViec and CongTy=?", ChucVuNguoiKy != null ? ChucVuNguoiKy.ChucDanh.Oid : Guid.Empty, QuanLyHopDongThinhGiang.CongTy.Oid));
            }
            else
            {
                LoaiCongTy = Session.FindObject<LoaiCongTy>(CriteriaOperator.Parse("TenLoaiCongTy like ?", "%Trường%"));
                PhanLoaiNguoiKy = Session.FindObject<PhanLoaiNguoiKy>(CriteriaOperator.Parse("TenPhanLoaiNguoiKy like ? and LoaiCongTy=?", "%đang tại chức%", LoaiCongTy != null ? LoaiCongTy.Oid : Guid.Empty));
                ChucVuNguoiKy = Session.FindObject<ChucVuNguoiKy>(CriteriaOperator.Parse("ChucVu.TenChucVu like ?", "Hiệu trưởng"));
                NguoiKy = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ChucVu.Oid = ? and !TinhTrang.DaNghiViec and CongTy=?", ChucVuNguoiKy != null ? ChucVuNguoiKy.ChucVu.Oid : Guid.Empty, QuanLyHopDongThinhGiang.CongTy.Oid));
            }
            NgayKy = Common.GetServerCurrentTime();
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            //
            if (!IsDeleted && !HopDongCu && GiangVienThinhGiang != null)
            {
                //Cập nhật hợp đồng hiện tại giảng viên
                UpdateHopDongHienTai();
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdatePhanLoaiNguoiKyList();
            UpdateNguoiKyList();
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                //Cập nhật hợp đồng hiện tại giảng viên lấy hợp đồng cũ gần nhất
                if (GiangVienThinhGiang != null && GiangVienThinhGiang.HopDongThinhGiang == this)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("GiangVienThinhGiang =? and Oid != ? && !HopDongCu", GiangVienThinhGiang.Oid, this.Oid);
                    XPCollection<HopDongThinhGiang> ListHopDong = new XPCollection<HopDongThinhGiang>(Session, filter);
                    ListHopDong.Sorting.Add(new SortProperty("NgayKy", SortingDirection.Descending));
                    //
                    if (ListHopDong.Count > 0)
                        GiangVienThinhGiang.HopDongThinhGiang = ListHopDong[0];
                }
                //
            }
            //
            base.OnDeleting();
        }

        /// <summary>
        /// Cập nhật hợp đồng mới nhất
        /// </summary>
        private void UpdateHopDongHienTai()
        {
            if (GiangVienThinhGiang.HopDongThinhGiang != null && GiangVienThinhGiang.HopDongThinhGiang != this
                && NgayKy != DateTime.MinValue && GiangVienThinhGiang.HopDongThinhGiang.NgayKy < NgayKy && !HopDongCu)
            { GiangVienThinhGiang.HopDongThinhGiang = this; }
            if (GiangVienThinhGiang.HopDongThinhGiang == null)
            { GiangVienThinhGiang.HopDongThinhGiang = this; }
        }

        //Cập nhật danh sách phân loại người ký
        private void UpdatePhanLoaiNguoiKyList()
        {
            if (PhanLoaiNguoiKyList == null)
                PhanLoaiNguoiKyList = new XPCollection<PhanLoaiNguoiKy>(Session);
            //
            if (LoaiCongTy != null)
                PhanLoaiNguoiKyList.Criteria = CriteriaOperator.Parse("LoaiCongTy.Oid = ?", LoaiCongTy.Oid);
        }

        //Cập nhật danh sách người ký
        private void UpdateNguoiKyList()
        {
            if (NguoiKyList == null)
                NguoiKyList = new XPCollection<ThongTinNhanVien>(Session);
           // if (ChucVuNguoiKy != null && PhanLoaiNguoiKy != null)
              // NguoiKyList.Criteria = Common.Criteria_HopDong_NguoiKyTenTheoLoaiNguoiKyVaChucVu(PhanLoaiNguoiKy, ChucVuNguoiKy, QuanLyHopDongThinhGiang.CongTy);
        }

        private void TaoSoHopDong()
        {
            if (QuanLyHopDongThinhGiang != null)
            {
                SqlParameter param = new SqlParameter("@QuanLyHopDong", QuanLyHopDongThinhGiang.Oid);
                //
                SoHopDong = ManageKeyFactory.ManageKey(ManageKeyEnum.SoHopDongThinhGiang, param);
            }
        }

        private void AfterGiangVienChanged()
        {
            if (GiangVienThinhGiang != null)
            {
                MaSoThue = GiangVienThinhGiang.NhanVienThongTinLuong.MaSoThue;
                CriteriaOperator filter = CriteriaOperator.Parse("TaiKhoanChinh=1 and NhanVien.Oid=?",GiangVienThinhGiang.Oid);
                XPCollection<TaiKhoanNganHang> tkList = new XPCollection<TaiKhoanNganHang>(Session,filter);
                if (tkList.Count > 0)
                {
                    SoTaiKhoan = tkList[0].SoTaiKhoan;
                    NganHang = Session.GetObjectByKey<NganHang>(tkList[0].NganHang.Oid);
                }
            }
        }
    }

}
