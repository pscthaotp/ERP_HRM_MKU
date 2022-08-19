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
using DevExpress.Xpo.DB;
using System.Data.SqlClient;
using System.Data;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.NhanSu.HopDongs
{
    [ImageName("BO_Constract")]
    [ModelDefault("AllowNew", "False")]
    [DefaultProperty("SoHopDong")]
    [ModelDefault("Caption", "Hợp đồng")]
    [Appearance("HopDong.NgoaiCongTy", TargetItems = "NguoiKy", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiCongTy.TenLoaiCongTy like '%khác%'")]
    [Appearance("HopDong.CongTy", TargetItems = "TenNguoiKy", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiCongTy.TenLoaiCongTy not like '%khác%'")]
    public class HopDong : BaseObject,IBoPhan
    {
        //
        private QuanLyHopDong _QuanLyHopDong;
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
        private ThongTinNhanVien _ThongTinNhanVien;
        private ChucDanh _ChucDanh;
        private LoaiHopDong _LoaiHopDong;
        private string _GhiChu;
        //Cơ quan khác
        private string _TenNguoiKy;
        private bool _KhongCoTrongHoSo;
        //
        private LoaiHopDong _LoaiHopDongLuuTru;
        private bool _InThoaThuan;
        //
        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Quản lý hợp đồng")]
        [Association("QuanLyHopDong-ListHopDong")]
        public QuanLyHopDong QuanLyHopDong
        {
            get
            {
                return _QuanLyHopDong;
            }
            set
            {
                SetPropertyValue("QuanLyHopDong", ref _QuanLyHopDong, value);
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
                if (!IsLoading && !string.IsNullOrEmpty(value))
                    AfterGiayToHoSo();
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
                if (!IsLoading && value != DateTime.MinValue)
                {
                    AfterGiayToHoSo();
                }
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
                    if (LoaiCongTy!= null && ( LoaiCongTy.TenLoaiCongTy.Contains("Trường") || LoaiCongTy.TenLoaiCongTy.Contains("Công ty")))
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

        [ModelDefault("Caption", "Tên trường")]
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
        //[RuleRequiredField(DefaultContexts.Save)]
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

        [Size(2000)]
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

        [Size(2000)]
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

        [ModelDefault("Caption",  "Hết hiệu lực")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Không có trong hồ sơ")]
        public bool KhongCoTrongHoSo
        {
            get
            {
                return _KhongCoTrongHoSo;
            }
            set
            {
                SetPropertyValue("KhongCoTrongHoSo", ref _KhongCoTrongHoSo, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bộ phận")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!KhongCoTrongHoSo")]
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
                {               
                    ThongTinNhanVien = null;
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Người lao động")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!KhongCoTrongHoSo")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                {
                    //xử lý khi nhân viên thay đổi
                    AfterNhanVienChanged();
                    //
                    if (BoPhan == null || BoPhan != value.BoPhan)
                        BoPhan = value.BoPhan;
                }
            }
        }

        [ModelDefault("Caption", "Chức danh")]
        public ChucDanh ChucDanh
        {
            get
            {
                return _ChucDanh;
            }
            set
            {
                SetPropertyValue("ChucDanh", ref _ChucDanh, value);
            }
        }

        [ModelDefault("Caption", "Loại hợp đồng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit","False")]
        public LoaiHopDong LoaiHopDong
        {
            get
            {
                return _LoaiHopDong;
            }
            set
            {
                SetPropertyValue("LoaiHopDong", ref _LoaiHopDong, value);
                if (!IsLoading && value != null)
                {
                    AfterLoaiHopDongChanged();
                    //
                }
            }
        }
        
        [ModelDefault("Caption", "In lại thỏa thuận")]
        public bool InThoaThuan
        {
            get
            {
                return _InThoaThuan;
            }
            set
            {
                SetPropertyValue("InThoaThuan", ref _InThoaThuan, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Loại hợp đồng")]
        public LoaiHopDong LoaiHopDongLuuTru
        {
            get
            {
                return _LoaiHopDongLuuTru;
            }
            set
            {
                SetPropertyValue("LoaiHopDongLuuTru", ref _LoaiHopDongLuuTru, value);
            }
        }


        [Browsable(false)]
        public XPCollection<PhanLoaiNguoiKy> PhanLoaiNguoiKyList { get; set; }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NguoiKyList { get; set; }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        public HopDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // 
            if (Common.TaiKhoanEdu())
            {
                LoaiCongTy = Session.FindObject<LoaiCongTy>(CriteriaOperator.Parse("TenLoaiCongTy like ?", "%Công ty%"));
                PhanLoaiNguoiKy = Session.FindObject<PhanLoaiNguoiKy>(CriteriaOperator.Parse("TenPhanLoaiNguoiKy like ? and LoaiCongTy=?", "%đang tại chức%", LoaiCongTy != null ? LoaiCongTy.Oid : Guid.Empty));
                ChucVuNguoiKy = Session.FindObject<ChucVuNguoiKy>(CriteriaOperator.Parse("ChucDanh.TenChucDanh like ?", "Tổng Giám đốc"));             
                NguoiKy = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ChucDanh.Oid = ? and !TinhTrang.DaNghiViec and CongTy.Oid=?", ChucVuNguoiKy != null ? ChucVuNguoiKy.ChucDanh.Oid : Guid.Empty, Common.CongTy(Session) != null ? Common.CongTy(Session).Oid : Guid.Empty));
            }
            else
            {
                LoaiCongTy = Session.FindObject<LoaiCongTy>(CriteriaOperator.Parse("TenLoaiCongTy like ?", "%Trường%"));
                PhanLoaiNguoiKy = Session.FindObject<PhanLoaiNguoiKy>(CriteriaOperator.Parse("TenPhanLoaiNguoiKy like ? and LoaiCongTy=?", "%đang tại chức%", LoaiCongTy != null ? LoaiCongTy.Oid : Guid.Empty));
                ChucVuNguoiKy = Session.FindObject<ChucVuNguoiKy>(CriteriaOperator.Parse("ChucDanh.TenChucDanh like ?", "Hiệu trưởng"));
                NguoiKy = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ChucDanh.Oid = ? and !TinhTrang.DaNghiViec and CongTy.Oid=?", ChucVuNguoiKy != null ? ChucVuNguoiKy.ChucDanh.Oid : Guid.Empty, Common.CongTy(Session) != null ? Common.CongTy(Session).Oid : Guid.Empty));
            }
            NgayKy = Common.GetServerCurrentTime();
            InThoaThuan = true;
            //
            UpdateNhanVienList();
            //AfterLoaiHopDongChanged();
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            NgayKy = NgayKy.Date;
            //
            if (!IsDeleted && !HopDongCu && ThongTinNhanVien != null)
            {
                //Cập nhật hợp đồng hiện tại nhân viên
                UpdateHopDongHienTai();

                //Cập nhật trích yếu hợp đồng
                TaoTrichYeu();
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //           
            UpdateNhanVienList();
            //
            UpdatePhanLoaiNguoiKyList();
            UpdateNguoiKyList();
            AfterLoaiHopDongChanged();
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                //Cập nhật hợp đồng hiện tại nhân viên lấy hợp đồng cũ gần nhất
                if (ThongTinNhanVien != null && ThongTinNhanVien.HopDongHienTai == this)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien =? and Oid != ? && !HopDongCu", ThongTinNhanVien.Oid, this.Oid);
                    XPCollection<HopDong> ListHopDong = new XPCollection<HopDong>(Session, filter);
                    ListHopDong.Sorting.Add(new SortProperty("NgayKy", SortingDirection.Descending));
                    //
                    if(ListHopDong.Count > 0)
                    ThongTinNhanVien.HopDongHienTai = ListHopDong[0];
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
            if (ThongTinNhanVien.HopDongHienTai != null && ThongTinNhanVien.HopDongHienTai != this
                && NgayKy != DateTime.MinValue && ThongTinNhanVien.HopDongHienTai.NgayKy < NgayKy && !HopDongCu)
            { ThongTinNhanVien.HopDongHienTai = this; }
            if (ThongTinNhanVien.HopDongHienTai == null)
            { ThongTinNhanVien.HopDongHienTai = this; }
        }

        //Cập nhật danh sách phân loại người ký
        private void UpdatePhanLoaiNguoiKyList()
        {
            if (PhanLoaiNguoiKyList == null)
                PhanLoaiNguoiKyList = new XPCollection<PhanLoaiNguoiKy>(Session);
            //
            if(LoaiCongTy!=null)
            PhanLoaiNguoiKyList.Criteria = CriteriaOperator.Parse("LoaiCongTy.Oid = ?",LoaiCongTy.Oid);
        }

        //Cập nhật danh sách người ký
        private void UpdateNguoiKyList()
        {
            if (NguoiKyList == null)
                NguoiKyList = new XPCollection<ThongTinNhanVien>(Session);
            if(ChucVuNguoiKy!=null && PhanLoaiNguoiKy != null && QuanLyHopDong != null)
               NguoiKyList.Criteria = Common.Criteria_HopDong_NguoiKyTenTheoLoaiNguoiKyVaChucVu(PhanLoaiNguoiKy, ChucVuNguoiKy, QuanLyHopDong.CongTy);
        }

        /// <summary>
        /// Cập nhật danh sách nhân viên
        /// </summary>
        private void UpdateNhanVienList()
        {
            //
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            //
            if (BoPhan == null)
                NVList.Criteria = new InOperator("Oid", Common.NhanVien_DanhSachNhanVienDuocPhanQuyen());
            else
                NVList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }

        /// <summary>
        /// Tạo số hợp đồng tự động
        /// </summary>
        protected virtual void TaoSoHopDong(){ }

        /// <summary>
        /// Tạo trích yếu cho giấy tờ hồ sơ
        /// </summary>
        protected virtual void TaoTrichYeu() { }

        /// <summary>
        /// Xảy ra sau khi dữ liệu nhân viên thay đổi
        /// </summary>
        protected virtual void AfterNhanVienChanged() { }

        /// <summary>
        /// Xảy ra sau khi dữ liệu nhân viên thay đổi
        /// </summary>
        protected virtual void AfterLoaiHopDongChanged() { }

        //Cập nhật giấy tờ hồ sơ
        protected virtual void AfterGiayToHoSo() { }
    }

}
