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
using DevExpress.Persistent.BaseImpl;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.HopDongs;
using ERP.Module.Enum.NhanSu;
using System.Drawing;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.Extends;
//
namespace ERP.Module.NghiepVu.NhanSu.NhanViens
{
    [ImageName("BO_Resume")]
    [ModelDefault("Caption", "Nhân viên")]
    [Appearance("ThongTinNhanVien.AnNghiViec", TargetItems = "NgayNghiViec", Visibility = ViewItemVisibility.Hide, Criteria = "!TinhTrang.DaNghiViec")]
    [Appearance("ThongTinNhanVien.AnThaiSan", TargetItems = "NgayNghiThaiSan;NgayHetHanNghiThaiSan", Visibility = ViewItemVisibility.Hide, Criteria = "TinhTrang.TenTinhTrang not like 'Nghỉ thai sản'")]
    [Appearance("ThongTinNhanVien.AnNghiKhongLuong", TargetItems = "NgayNghiKhongLuong;NgayHetHanNghiKhongLuong", Visibility = ViewItemVisibility.Hide, Criteria = "TinhTrang.TenTinhTrang not like 'Nghỉ không lương'")]
    [RuleCombinationOfPropertiesIsUnique("NhanVien.Unique1", DefaultContexts.Save, "CongTy;CMND;LoaiHoSo", "Nhân viên đã tồn tại trong Công ty/Trường. Vui lòng kiểm tra lại !")]
    [RuleCombinationOfPropertiesIsUnique("NhanVien.Unique2", DefaultContexts.Save, "CongTy;SoHoChieu;LoaiHoSo", "Nhân viên đã tồn tại trong Công ty/Trường. Vui lòng kiểm tra lại !")]
    public class NhanVien : HoSo, IBoPhan, ICategorizedItem,ICongTy
    {
        private DateTime _NgayTuyenDung;
        private DateTime _NgayVaoCongTy;
        private DateTime _ThamNienLamViec;
        private BoPhan _BoPhan;
        private CongTy _CongTy;
        //
        private ChucDanh _ChucDanh;
        private CongViec _CongViecHienNay;
        private LoaiHopDong _LoaiHopDong;
        private HopDong _HopDongHienTai;
        private string _DonViTuyenDung;
        private string _CongViecTruocTuyenDung;
        //
        private NhanVienThongTinLuong _NhanVienThongTinLuong;
        private NhanVienTrinhDo _NhanVienTrinhDo;
        private TinhTrang _TinhTrang;
        private DateTime _NgayNghiViec;
        private DateTime _NgayNghiThaiSan;
        private DateTime _NgayHetHanNghiThaiSan;
        private DateTime _NgayNghiKhongLuong;
        private DateTime _NgayHetHanNghiKhongLuong;
        private string _URLHinh;
        //
        //Them mới
        private int _SoThangLamViec;

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading && value != null)
                    AfterBoPhanChanged();
            }
        }
      
        [ModelDefault("Caption", "Ngày tuyển dụng")]
        public DateTime NgayTuyenDung
        {
            get
            {
                return _NgayTuyenDung;
            }
            set
            {
                SetPropertyValue("NgayTuyenDung", ref _NgayTuyenDung, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày vào Trường*")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "LoaiHoSo = 0 OR LoaiHoSo is null")]
        public DateTime NgayVaoCongTy
        {
            get
            {
                return _NgayVaoCongTy;
            }
            set
            {
                SetPropertyValue("NgayVaoCongTy", ref _NgayVaoCongTy, value);
                if (!IsLoading && value != DateTime.MinValue)
                {
                    ThamNienLamViec = value;
                    SoThangLamViec = Common.GetMonthNumber(value, DateTime.Now); 
                    AfterNgayVaoTruongChanged();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Thâm niên làm việc")]
        //[RuleRequiredField(DefaultContexts.Save, TargetCriteria = "LoaiHoSo = 0 OR LoaiHoSo is null")]
        public DateTime ThamNienLamViec
        {
            get
            {
                return _ThamNienLamViec;
            }
            set
            {
                SetPropertyValue("ThamNienLamViec", ref _ThamNienLamViec, value);                
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Số tháng làm việc")]
        public int SoThangLamViec
        {
            get
            {
                return _SoThangLamViec;
            }
            set
            {
                SetPropertyValue("SoThangLamViec", ref _SoThangLamViec, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức danh*")]
        //[RuleRequiredField(DefaultContexts.Save, TargetCriteria = "LoaiHoSo = 0 OR LoaiHoSo is null")]
        //[DataSourceProperty("CDList")]
        public ChucDanh ChucDanh
        {
            get
            {
                return _ChucDanh;
            }
            set
            {
                SetPropertyValue("ChucDanh", ref _ChucDanh, value);
                if (!IsLoading)
                {
                    if (value != null)
                    {
                        CriteriaOperator filter = CriteriaOperator.Parse("CapBac=? && CongTy.Oid = ?",value.CapBac,CongTy != null ? CongTy.Oid : Guid.Empty);
                        NgachLuong ngachLuong = Session.FindObject<NgachLuong>(filter);
                        if (ngachLuong != null)
                        {
                            this.NhanVienThongTinLuong.NgachLuong = ngachLuong;
                        }
                    }
                    else
                    {
                        this.NhanVienThongTinLuong.NgachLuong = null;
                    }                 
                }
            }
        }

        [ModelDefault("Caption", "Công việc hiện nay")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public CongViec CongViecHienNay
        {
            get
            {
                return _CongViecHienNay;
            }
            set
            {
                SetPropertyValue("CongViecHienNay", ref _CongViecHienNay, value);
            }
        }
        
        [ModelDefault("Caption", "Loại hợp đồng")]
        public LoaiHopDong LoaiHopDong
        {
            get
            {
                return _LoaiHopDong;
            }
            set
            {
                SetPropertyValue("LoaiHopDong", ref _LoaiHopDong, value);
                if (!IsLoading)
                {
                    if (LoaiHopDong.TenLoaiHopDong.ToLower().Contains("thử việc") ||
                        LoaiHopDong.TenLoaiHopDong.ToLower().Contains("tập sự"))
                    {
                        NhanVienThongTinLuong.KhongDongBHXH = true;
                        NhanVienThongTinLuong.KhongDongBHYT = true;
                        NhanVienThongTinLuong.KhongDongBHTN = true;
                        NhanVienThongTinLuong.KhongDongCongDoan = true;
                        NhanVienThongTinLuong.PhanTramTinhLuong = 85;
                    }
                    else
                    {
                        NhanVienThongTinLuong.KhongDongBHXH = false;
                        NhanVienThongTinLuong.KhongDongBHYT = false;
                        NhanVienThongTinLuong.KhongDongBHTN = false;
                        NhanVienThongTinLuong.KhongDongCongDoan = false;
                        NhanVienThongTinLuong.PhanTramTinhLuong = 100;
                    }
                }
            }
        }

        [ModelDefault("Caption", "Hợp đồng hiện tại")]
        [DataSourceProperty("ListHopDong")]
        public HopDong HopDongHienTai
        {
            get
            {
                return _HopDongHienTai;
            }
            set
            {
                SetPropertyValue("HopDongHienTai", ref _HopDongHienTai, value);
                if (!IsLoading && value != null && value.LoaiHopDong != null)
                {
                    LoaiHopDong = HopDongHienTai.LoaiHopDong;
                }
            }
        }

        [ModelDefault("Caption", "Đơn vị tuyển dụng")]
        public string DonViTuyenDung
        {
            get
            {
                return _DonViTuyenDung;
            }
            set
            {
                SetPropertyValue("DonViTuyenDung", ref _DonViTuyenDung, value);
            }
        }

        [ModelDefault("Caption", "Công việc trước tuyển dụng")]
        public string CongViecTruocTuyenDung
        {
            get
            {
                return _CongViecTruocTuyenDung;
            }
            set
            {
                SetPropertyValue("CongViecTruocTuyenDung", ref _CongViecTruocTuyenDung, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Hồ sơ lương")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public NhanVienThongTinLuong NhanVienThongTinLuong
        {
            get
            {
                return _NhanVienThongTinLuong;
            }
            set
            {
                SetPropertyValue("NhanVienThongTinLuong", ref _NhanVienThongTinLuong, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Thông tin trình độ")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public NhanVienTrinhDo NhanVienTrinhDo
        {
            get
            {
                return _NhanVienTrinhDo;
            }
            set
            {
                SetPropertyValue("NhanVienTrinhDo", ref _NhanVienTrinhDo, value);               
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tình trạng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public TinhTrang TinhTrang
        {
            get
            {
                return _TinhTrang;
            }
            set
            {
                SetPropertyValue("TinhTrang", ref _TinhTrang, value);
            }
        }

        //
        [Browsable(false)]
        public string URLHinh
        {
            get
            {
                return _URLHinh;
            }
            set
            {
                SetPropertyValue("URLHinh", ref _URLHinh, value);
            }
        }

        [NonPersistent]
        [Delayed]
        [ModelDefault("Caption", "Hình ảnh")]
        [Size(SizeAttribute.Unlimited)]
        [ValueConverter(typeof(ImageValueConverter))]
        public Image HinhAnh
        {
            get
            {
                //Tải hình từ server
                Image image = UploadImage.DownLoadImageFromServer(URLHinh,1);
                //
                return image;
            }
            set
            {
                //
                if (!IsLoading)
                {
                    if (string.IsNullOrEmpty(MaNhanVien))
                    {
                        DialogUtil.ShowError("Vui lòng lưu dữ liệu trước khi tải hình !!!");
                        value = null;
                        //
                        return;
                    }
                    //
                    string urlHinh = MaNhanVien + ".png";
                    if (UploadImage.UploadImageToServer(value, urlHinh,1) && value != null)
                    {
                        URLHinh = urlHinh;
                        // 
                    }
                    else
                    {
                        value = null;
                        URLHinh = "";
                    }
                }
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Thông tin Trường")]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày nghỉ việc")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TinhTrang.TenTinhTrang like '%Nghỉ việc%'")]
        public DateTime NgayNghiViec
        {
            get
            {
                return _NgayNghiViec;
            }
            set
            {
                SetPropertyValue("NgayNghiViec", ref _NgayNghiViec, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày nghỉ thai sản")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TinhTrang.TenTinhTrang like '%Nghỉ thai sản%'")]
        public DateTime NgayNghiThaiSan
        {
            get
            {
                return _NgayNghiThaiSan;
            }
            set
            {
                SetPropertyValue("NgayNghiThaiSan", ref _NgayNghiThaiSan, value);
            }
        }
        
        [ModelDefault("Caption", "Ngày hết hạn nghỉ thai sản")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TinhTrang.TenTinhTrang like '%Nghỉ thai sản%'")]
        public DateTime NgayHetHanNghiThaiSan
        {
            get
            {
                return _NgayHetHanNghiThaiSan;
            }
            set
            {
                SetPropertyValue("NgayHetHanNghiThaiSan", ref _NgayHetHanNghiThaiSan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày nghỉ không lương")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TinhTrang.TenTinhTrang like '%Nghỉ không lương%'")]
        public DateTime NgayNghiKhongLuong
        {
            get
            {
                return _NgayNghiKhongLuong;
            }
            set
            {
                SetPropertyValue("NgayNghiKhongLuong", ref _NgayNghiKhongLuong, value);
            }
        }

        [ModelDefault("Caption", "Ngày hết hạn nghỉ không lương")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TinhTrang.TenTinhTrang like '%Nghỉ không lương%'")]
        public DateTime NgayHetHanNghiKhongLuong
        {
            get
            {
                return _NgayHetHanNghiKhongLuong;
            }
            set
            {
                SetPropertyValue("NgayHetHanNghiKhongLuong", ref _NgayHetHanNghiKhongLuong, value);
            }
        }

        #region Danh sách
        [Aggregated]
        [ModelDefault("Caption", "Tài khoản ngân hàng")]
        [Association("NhanVien-ListTaiKhoanNganHang")]
        public XPCollection<TaiKhoanNganHang> ListTaiKhoanNganHang
        {
            get
            {
                return GetCollection<TaiKhoanNganHang>("ListTaiKhoanNganHang");
            }
        }

        [Aggregated]
        [ModelDefault("AllowNew", "False")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Hợp đồng lao động")]
        public XPCollection<HopDong> ListHopDong
        {
            get
            {
                return new XPCollection<HopDong>(Session, CriteriaOperator.Parse("ThongTinNhanVien=?", Oid));
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách kiêm nhiệm")]
        [Association("NhanVien-ListChucVuKiemNhiem")]
        public XPCollection<ChucVuKiemNhiem> ListChucVuKiemNhiem
        {
            get
            {
                return GetCollection<ChucVuKiemNhiem>("ListChucVuKiemNhiem");
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Quan hệ gia đình")]
        [Association("NhanVien-ListQuanHeGiaDinh")]
        public XPCollection<QuanHeGiaDinh> ListQuanHeGiaDinh
        {
            get
            {
                return GetCollection<QuanHeGiaDinh>("ListQuanHeGiaDinh");
            }
        }

        [Aggregated]
        [Association("NhanVien-ListGiamTruGiaCanh")]
        [ModelDefault("Caption", "Giảm trừ gia cảnh")]
        public XPCollection<GiamTruGiaCanh> ListGiamTruGiaCanh
        {
            get
            {
                return GetCollection<GiamTruGiaCanh>("ListGiamTruGiaCanh");
            }
        }
        #endregion

        #region Custom treeList
        ITreeNode ICategorizedItem.Category
        {
            get
            {
                return BoPhan;
            }
            set
            {
                BoPhan = (BoPhan)value;
            }
        }

        [Browsable(false)]
        [NonPersistent()]
        public BoPhan Category
        {
            get
            {
                return BoPhan;
            }
            set
            {
                BoPhan = value;
            }
        }
        #endregion

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            NhanVienTrinhDo = new NhanVienTrinhDo(Session);
            NhanVienThongTinLuong = new NhanVienThongTinLuong(Session);
            TinhTrang = Session.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Đang làm việc"));
            CongTy = Common.CongTy(Session);
        }

       public void OnLoad()
        {
            if (this.NhanVienThongTinLuong == null)
                NhanVienThongTinLuong = new NhanVienThongTinLuong(Session);
            if (this.NhanVienTrinhDo == null)
                NhanVienTrinhDo = new NhanVienTrinhDo(Session);
        }

        //protected override void OnLoaded()
        //{
        //    base.OnLoaded();
        //    //
        //    if(this.NhanVienThongTinLuong == null)
        //        NhanVienThongTinLuong = new NhanVienThongTinLuong(Session);
        //    if(this.NhanVienTrinhDo == null)
        //        NhanVienTrinhDo = new NhanVienTrinhDo(Session);
        //}

        public NhanVien(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ChucDanh> CDList { get; set; }

        public void CapNhatChucDanh(ChucVu cv)
        {
            if (CDList == null)
                CDList = new XPCollection<ChucDanh>(Session, false);
            else
                CDList.Reload();

            CriteriaOperator filter = CriteriaOperator.Parse("isnull(KhongConHieuLuc,0) = 0 and ChucVu = ?", cv.Oid);
            XPCollection<ChucDanh> listChucDanh = new XPCollection<ChucDanh>(Session, filter);

            //
            foreach (ChucDanh item in listChucDanh)
            {
                CDList.Add(item);
            }
            //CDList.Filter = CriteriaOperator.Parse("ChucVu.Oid=?", nhanVien.ChucVu.Oid);//old
        }

        protected virtual void AfterNgayVaoTruongChanged()
        {
            ThamNienLamViec = NgayVaoCongTy;
        }

        protected virtual void AfterBoPhanChanged()
        {           
        }
    }

}
