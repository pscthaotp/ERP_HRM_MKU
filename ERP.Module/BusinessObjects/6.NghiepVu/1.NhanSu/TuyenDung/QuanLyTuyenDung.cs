using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Enum.NhanSu;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.NhanSu.TuyenDung
{
    [DefaultClassOptions]
    [ImageName("group2_32x32")]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Quản lý tuyển dụng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "CongTy;NienDoTaiChinh;DotTuyenDung")]
    [Appearance("QuanLyTuyenDung", TargetItems = "NamHoc", Enabled = false, Criteria = "NamHoc is not null")]
    [Appearance("QuanLyTuyenDung.NgoaiCongTy", TargetItems = "NguoiKy", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiCongTy.TenLoaiCongTy like '%khác%'")]
    [Appearance("QuanLyTuyenDung.CongTy", TargetItems = "TenNguoiKy", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiCongTy.TenLoaiCongTy not like '%khác%'")]

    public class QuanLyTuyenDung : BaseObject, ICongTy
    {
        private TrangThaiTuyenDungEnum _TrangThai;
        private DateTime _NopHoSoDenNgay;
        private DateTime _NopHoSoTuNgay;
        private DateTime _ThucHienDenNgay;
        private DateTime _ThucHienTuNgay;
        private DateTime _DuKienDenNgay;
        private DateTime _DuKienTuNgay;
        //private NamHoc _NamHoc;
        private NienDoTaiChinh _NienDoTaiChinh;
        private int _DotTuyenDung;
        private LoaiCongTy _LoaiCongTy;
        private CongTy _CongTy;
        private PhanLoaiNguoiKy _PhanLoaiNguoiKy;
        private ThongTinNhanVien _NguoiKy;
        private string _TenNguoiKy; // Nếu cơ quan khác
        private ChucVuNguoiKy _ChucVuNguoiKy;

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
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
                if (!IsLoading)
                {
                    NienDoTaiChinh = null;
                    UpdateNienDoTaiChinh();
                }
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
                if (!IsLoading && PhanLoaiNguoiKy != null)
                {
                    ChucVuNguoiKy = null;
                    NguoiKy = null;
                    //
                    UpdateNguoiKyList();
                    NguoiKy = null;
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
                    UpdateNguoiKyList();
                    NguoiKy = null;
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

        [ImmediatePostData]
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

        [ModelDefault("Caption", "Niên độ")]
        [DataSourceProperty("NienDoTaiChinhList")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NienDoTaiChinh NienDoTaiChinh
        {
            get
            {
                return _NienDoTaiChinh;
            }
            set
            {
                SetPropertyValue("NienDoTaiChinh", ref _NienDoTaiChinh, value);
            }
        }

        //[ModelDefault("Caption", "Năm học")]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public NamHoc NamHoc
        //{
        //    get
        //    {
        //        return _NamHoc;
        //    }
        //    set
        //    {
        //        SetPropertyValue("NamHoc", ref _NamHoc, value);
        //    }
        //}

        [ModelDefault("Caption", "Đợt tuyển dụng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleRange("", DefaultContexts.Save, 1, 12)]
        public int DotTuyenDung
        {
            get
            {
                return _DotTuyenDung;
            }
            set
            {
                SetPropertyValue("DotTuyenDung", ref _DotTuyenDung, value);
            }
        }

        [NonCloneable]
        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public string Caption
        {
            get
            {
                if (NienDoTaiChinh != null)
                    return string.Format("{0} đợt {1}", NienDoTaiChinh.TenNienDo, DotTuyenDung);
                else
                    return "";
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        public DateTime DuKienTuNgay
        {
            get
            {
                return _DuKienTuNgay;
            }
            set
            {
                SetPropertyValue("DuKienTuNgay", ref _DuKienTuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DuKienDenNgay
        {
            get
            {
                return _DuKienDenNgay;
            }
            set
            {
                SetPropertyValue("DuKienDenNgay", ref _DuKienDenNgay, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        public DateTime ThucHienTuNgay
        {
            get
            {
                return _ThucHienTuNgay;
            }
            set
            {
                SetPropertyValue("ThucHienTuNgay", ref _ThucHienTuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        public DateTime ThucHienDenNgay
        {
            get
            {
                return _ThucHienDenNgay;
            }
            set
            {
                SetPropertyValue("ThucHienDenNgay", ref _ThucHienDenNgay, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        public DateTime NopHoSoTuNgay
        {
            get
            {
                return _NopHoSoTuNgay;
            }
            set
            {
                SetPropertyValue("NopHoSoTuNgay", ref _NopHoSoTuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        public DateTime NopHoSoDenNgay
        {
            get
            {
                return _NopHoSoDenNgay;
            }
            set
            {
                SetPropertyValue("NopHoSoDenNgay", ref _NopHoSoDenNgay, value);
            }
        }

        [ModelDefault("Caption", "Trạng thái")]
        public TrangThaiTuyenDungEnum TrangThai
        {
            get
            {
                return _TrangThai;
            }
            set
            {
                SetPropertyValue("TrangThai", ref _TrangThai, value);
            }
        }

        [ModelDefault("Caption", "B1. Vị trí tuyển dụng")]
        [Association("QuanLyTuyenDung-ListViTriTuyenDung")]
        public XPCollection<ViTriTuyenDung> ListViTriTuyenDung
        {
            get
            {
                return GetCollection<ViTriTuyenDung>("ListViTriTuyenDung");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "B2. Đăng ký tuyển dụng")]
        [Association("QuanLyTuyenDung-ListDangKyTuyenDung")]
        public XPCollection<DangKyTuyenDung> ListDangKyTuyenDung
        {
            get
            {
                return GetCollection<DangKyTuyenDung>("ListDangKyTuyenDung");
            }
        }


        [ModelDefault("Caption", "B3. Duyệt đăng ký tuyển dụng")]
        [Association("QuanLyTuyenDung-ListNhuCauTuyenDung")]
        public XPCollection<NhuCauTuyenDung> ListNhuCauTuyenDung
        {
            get
            {
                return GetCollection<NhuCauTuyenDung>("ListNhuCauTuyenDung");
            }
        }

        [ModelDefault("Caption", "B4. Hội đồng tuyển dụng")]
        [Association("QuanLyTuyenDung-ListHoiDongTuyenDung")]
        public XPCollection<HoiDongTuyenDung> ListHoiDongTuyenDung
        {
            get
            {
                return GetCollection<HoiDongTuyenDung>("ListHoiDongTuyenDung");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "B5. Danh sách ứng viên")]
        [Association("QuanLyTuyenDung-ListUngVien")]
        public XPCollection<UngVien> ListUngVien
        {
            get
            {
                return GetCollection<UngVien>("ListUngVien");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "B6. Chi tiết tuyển dụng")]
        [Association("QuanLyTuyenDung-ListChiTietTuyenDung")]
        public XPCollection<ChiTietTuyenDung> ListChiTietTuyenDung
        {
            get
            {
                return GetCollection<ChiTietTuyenDung>("ListChiTietTuyenDung");
            }
        }

        [ModelDefault("Caption", "B7. Danh sách trúng tuyển")]
        [Association("QuanLyTuyenDung-ListTrungTuyen")]
        public XPCollection<TrungTuyen> ListTrungTuyen
        {
            get
            {
                return GetCollection<TrungTuyen>("ListTrungTuyen");
            }
        }

        [ModelDefault("Caption", "Danh sách không trúng tuyển")]
        [Association("QuanLyTuyenDung-ListKhongTrungTuyen")]
        public XPCollection<KhongTrungTuyen> ListKhongTrungTuyen
        {
            get
            {
                return GetCollection<KhongTrungTuyen>("ListKhongTrungTuyen");
            }
        }

        [Browsable(false)]
        public XPCollection<NienDoTaiChinh> NienDoTaiChinhList { get; set; }

        [Browsable(false)]
        public XPCollection<PhanLoaiNguoiKy> PhanLoaiNguoiKyList { get; set; }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NguoiKyList { get; set; }

        public QuanLyTuyenDung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DotTuyenDung = 1;

            CongTy = Common.CongTy(Session);
            //
            if (Common.TaiKhoanEdu())
            {
                LoaiCongTy = Session.FindObject<LoaiCongTy>(CriteriaOperator.Parse("TenLoaiCongTy like ?", "%Công ty%"));
                PhanLoaiNguoiKy = Session.FindObject<PhanLoaiNguoiKy>(CriteriaOperator.Parse("TenPhanLoaiNguoiKy like ? and LoaiCongTy=?", "%đang tại chức%", LoaiCongTy != null ? LoaiCongTy.Oid : Guid.Empty));
                ChucVuNguoiKy = Session.FindObject<ChucVuNguoiKy>(CriteriaOperator.Parse("ChucDanh.TenChucDanh like ?", "Giám đốc"));
                NguoiKy = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ChucDanh.Oid = ? and !TinhTrang.DaNghiViec and CongTy=?", ChucVuNguoiKy != null ? ChucVuNguoiKy.ChucDanh.Oid : Guid.Empty, CongTy.Oid));
            }
            else
            {
                LoaiCongTy = Session.FindObject<LoaiCongTy>(CriteriaOperator.Parse("TenLoaiCongTy like ?", "%Trường%"));
                PhanLoaiNguoiKy = Session.FindObject<PhanLoaiNguoiKy>(CriteriaOperator.Parse("TenPhanLoaiNguoiKy like ? and LoaiCongTy=?", "%đang tại chức%", LoaiCongTy != null ? LoaiCongTy.Oid : Guid.Empty));
                ChucVuNguoiKy = Session.FindObject<ChucVuNguoiKy>(CriteriaOperator.Parse("ChucDanh.TenChucDanh like ?", "Hiệu trưởng"));
                NguoiKy = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ChucDanh.Oid = ? and !TinhTrang.DaNghiViec and CongTy.Oid=?", ChucVuNguoiKy != null ? ChucVuNguoiKy.ChucDanh.Oid : Guid.Empty, CongTy.Oid));
            }
            //khoi tao du lieu vi tri tuyen dung       
            //TuyenDungHelper.CreateViTriTuyenDung(Session, this, "03", "Nhân viên", LoaiNhanVienEnum.BienChe);

            UpdateNienDoTaiChinh();
        }

        protected void UpdateNienDoTaiChinh()
        {
            if (NienDoTaiChinhList == null)
                NienDoTaiChinhList = new XPCollection<NienDoTaiChinh>(Session);

            if (CongTy != null)
                NienDoTaiChinhList.Criteria = CriteriaOperator.Parse("CongTy.Oid=?", this.CongTy.Oid);
            else
                NienDoTaiChinhList = null;
        }

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
            if (ChucVuNguoiKy != null)
                NguoiKyList.Criteria = Common.Criteria_HopDong_NguoiKyTenTheoLoaiNguoiKyVaChucVu(PhanLoaiNguoiKy, ChucVuNguoiKy, CongTy);
        }

        protected override void OnDeleting()
        {
            Session.Delete(ListViTriTuyenDung);
            Session.Save(ListViTriTuyenDung);
            Session.Delete(ListDangKyTuyenDung);
            Session.Save(ListDangKyTuyenDung);
            Session.Delete(ListNhuCauTuyenDung);
            Session.Save(ListNhuCauTuyenDung);
            Session.Delete(ListHoiDongTuyenDung);
            Session.Save(ListHoiDongTuyenDung);
            Session.Delete(ListUngVien);
            Session.Save(ListUngVien);
            Session.Delete(ListChiTietTuyenDung);
            Session.Save(ListChiTietTuyenDung);
            Session.Delete(ListTrungTuyen);
            Session.Save(ListTrungTuyen);
            Session.Delete(ListKhongTrungTuyen);
            Session.Save(ListKhongTrungTuyen);
            base.OnDeleting();
        }
    }
}
