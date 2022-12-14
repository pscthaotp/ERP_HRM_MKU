using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using System.Collections.Generic;

using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Commons;
using ERP.Module.Enum.NhanSu;
//
namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("Caption", "Quyết định")]
    [DefaultProperty("SoQuyetDinh")]
    [Appearance("QuyetDinh.NgoaiCongTy", TargetItems = "NguoiKy", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiCongTy.TenLoaiCongTy like '%khác%'")]
    [Appearance("QuyetDinh.CongTy", TargetItems = "TenNguoiKy", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiCongTy.TenLoaiCongTy not like '%khác%'")]
    public class QuyetDinh : BaseObject,ICongTy
    {
        private LoaiQuyetDinhEnum _LoaiQuyetDinh = LoaiQuyetDinhEnum.HieuTruong;
        private LoaiCongTy _LoaiCongTy;
        private string _TenCongTy;
        private PhanLoaiNguoiKy _PhanLoaiNguoiKy;
        private ThongTinNhanVien _NguoiKy;
        private string _TenNguoiKy; // Nếu cơ quan khác
        private ChucVuNguoiKy _ChucVuNguoiKy;
        //
        private string _SoQuyetDinh;
        private DateTime _NgayQuyetDinh;
        private DateTime _NgayHieuLuc;
        private string _NoiDung;
        private string _CanCu;
        private string _NoiNhan;
        private CongTy _CongTy;
        private bool _QuyetDinhMoi;

        //Lưu vết
        private DateTime _CreateDate;

        private bool _IsDirty;

        //
        [ModelDefault("Caption", "Loại quyết định")]
        public LoaiQuyetDinhEnum LoaiQuyetDinh
        {
            get
            {
                return _LoaiQuyetDinh;
            }
            set
            {
                SetPropertyValue("LoaiQuyetDinh", ref _LoaiQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Quyết định mới")]
        public bool QuyetDinhMoi
        {
            get
            {
                return _QuyetDinhMoi;
            }
            set
            {
                SetPropertyValue("QuyetDinhMoi", ref _QuyetDinhMoi, value);
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
                        TenCongTy = CongTy.TenBoPhan;
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Số quyết định")]
        public string SoQuyetDinh
        {
            get
            {
                return _SoQuyetDinh;
            }
            set
            {
                SetPropertyValue("SoQuyetDinh", ref _SoQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày quyết định")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayQuyetDinh
        {
            get
            {
                return _NgayQuyetDinh;
            }
            set
            {
                SetPropertyValue("NgayQuyetDinh", ref _NgayQuyetDinh, value);
                //if (!IsLoading && value != DateTime.MinValue)
                //    NgayHieuLuc = NgayQuyetDinh;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày hiệu lực")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHieuLuc
        {
            get
            {
                return _NgayHieuLuc;
            }
            set
            {
                SetPropertyValue("NgayHieuLuc", ref _NgayHieuLuc, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Nội dung")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string NoiDung
        {
            get
            {
                return _NoiDung;
            }
            set
            {
                SetPropertyValue("NoiDung", ref _NoiDung, value);
            }
        }

        [ModelDefault("Caption", "Căn cứ")]
        [Size(2000)]
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



        [ModelDefault("Caption", "Nơi nhận")]
        [Size(200)]
        public string NoiNhan
        {
            get
            {
                return _NoiNhan;
            }
            set
            {
                SetPropertyValue("NoiNhan", ref _NoiNhan, value);
            }
        }

        [ImmediatePostData]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [ModelDefault("Caption", "Trường")]
        //[ModelDefault("AllowEdit", "false")]
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
                    if (LoaiCongTy != null && (LoaiCongTy.TenLoaiCongTy.Contains("Trường") || LoaiCongTy.TenLoaiCongTy.Contains("Công ty")))
                    {
                        TenCongTy = CongTy.TenBoPhan;
                    }
                    else
                    {
                        TenCongTy = string.Empty;
                        NguoiKy = null;
                    }
                }
            }
        }

        //Lưu vết ngày lập quyết định
        [Browsable(false)]
        public DateTime CreateDate
        {
            get
            {
                return _CreateDate;
            }
            set
            {
                SetPropertyValue("CreateDate", ref _CreateDate, value);
            }
        }

        //Dùng lưu vết dữ liệu thay đổi
        [Browsable(false)]
        public bool IsDirty
        {
            get
            {
                return _IsDirty;
            }
            set
            {
                SetPropertyValue("IsDirty", ref _IsDirty, value);
            }
        }

        [Browsable(false)]
        public XPCollection<PhanLoaiNguoiKy> PhanLoaiNguoiKyList { get; set; }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NguoiKyList { get; set; }

        [Browsable(false)]
        public XPCollection<NhanVien> NVList { get; set; }

        
        public QuyetDinh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            //
            if (Common.TaiKhoanEdu())
            {
                LoaiCongTy = Session.FindObject<LoaiCongTy>(CriteriaOperator.Parse("TenLoaiCongTy like ?", "%Công ty%"));
                PhanLoaiNguoiKy = Session.FindObject<PhanLoaiNguoiKy>(CriteriaOperator.Parse("TenPhanLoaiNguoiKy like ? and LoaiCongTy=?", "%đang tại chức%", LoaiCongTy != null ? LoaiCongTy.Oid : Guid.Empty));
                ChucVuNguoiKy = Session.FindObject<ChucVuNguoiKy>(CriteriaOperator.Parse("ChucDanh.TenChucDanh like ?", "Tổng Giám đốc"));
                NguoiKy = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ChucDanh.Oid = ? and !TinhTrang.DaNghiViec and CongTy=?", ChucVuNguoiKy != null ? ChucVuNguoiKy.ChucDanh.Oid : Guid.Empty, CongTy.Oid));
            }
            else
            {
                LoaiCongTy = Session.FindObject<LoaiCongTy>(CriteriaOperator.Parse("TenLoaiCongTy like ?", "%Trường%"));
                PhanLoaiNguoiKy = Session.FindObject<PhanLoaiNguoiKy>(CriteriaOperator.Parse("TenPhanLoaiNguoiKy like ? and LoaiCongTy=?", "%đang tại chức%", LoaiCongTy != null ? LoaiCongTy.Oid : Guid.Empty));
                ChucVuNguoiKy = Session.FindObject<ChucVuNguoiKy>(CriteriaOperator.Parse("ChucDanh.TenChucDanh like ?", "Hiệu trưởng"));
                NguoiKy = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ChucDanh.Oid = ? and !TinhTrang.DaNghiViec and CongTy.Oid=?", ChucVuNguoiKy != null ? ChucVuNguoiKy.ChucDanh.Oid : Guid.Empty, CongTy.Oid));
            }
            NgayQuyetDinh = Common.GetServerCurrentTime();
            NgayHieuLuc = Common.GetServerCurrentTime();
            //
            CreateDate = Common.GetServerCurrentTime();
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
            if (ChucVuNguoiKy != null)
                NguoiKyList.Criteria = Common.Criteria_HopDong_NguoiKyTenTheoLoaiNguoiKyVaChucVu(PhanLoaiNguoiKy, ChucVuNguoiKy, CongTy);
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            //
            //1. Trả lại trạng thái lưu vết dữ liệu thay đổi
            this.IsDirty = false;
        }
    }
}

