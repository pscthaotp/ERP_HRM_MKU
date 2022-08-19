using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.NghiepVu.NhanSu.HopDongs;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    #region 1. Hợp đồng lao động
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Hợp đồng")]
    public class HopDong_ChonHopDongLamViec : OfficeBaseObject
    {
        private QuanLyHopDong _QuanLyHopDong;
        private ChucVuNguoiKy _ChucVuNguoiKy;
        private PhanLoaiNguoiKy _PhanLoaiNguoiKy;
        private ThongTinNhanVien _NguoiKy;

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Quản lý hợp đồng")]
        public QuanLyHopDong QuanLyHopDong
        {
            get
            {
                return _QuanLyHopDong;
            }
            set
            {
                SetPropertyValue("QuanLyHopDong", ref _QuanLyHopDong, value);
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại người ký")]
        public PhanLoaiNguoiKy PhanLoaiNguoiKy
        {
            get
            {
                return _PhanLoaiNguoiKy;
            }
            set
            {
                SetPropertyValue("PhanLoaiNguoiKy", ref _PhanLoaiNguoiKy, value);
                if (!IsLoading && ChucVuNguoiKy != null)
                {
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

        [ModelDefault("Caption", "Người ký")]
        [DataSourceProperty("NguoiKyList")]
        [RuleRequiredField(DefaultContexts.Save)]
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
        //

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NguoiKyList { get; set; }

        public HopDong_ChonHopDongLamViec(Session session) : base(session) { }

        protected override void OnLoaded()
        {
            base.OnLoading();
            //
            UpdateNguoiKyList();
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNguoiKyList();
        }

        private void UpdateNguoiKyList()
        {
            if (NguoiKyList == null)
                NguoiKyList = new XPCollection<ThongTinNhanVien>(Session);

            if (PhanLoaiNguoiKy != null && ChucVuNguoiKy != null)
            NguoiKyList.Criteria = Common.Criteria_HopDong_NguoiKyTenTheoLoaiNguoiKyVaChucVu(PhanLoaiNguoiKy, ChucVuNguoiKy, CongTy);
        }
    }
    #endregion
    
    #region 2. Hợp đồng khoán
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Hợp đồng khoán")]
    public class HopDong_ChonHopDongKhoan : OfficeBaseObject
    {
        private QuanLyHopDong _QuanLyHopDong;
        private ChucVuNguoiKy _ChucVuNguoiKy;
        private PhanLoaiNguoiKy _PhanLoaiNguoiKy;
        private ThongTinNhanVien _NguoiKy;

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Quản lý hợp đồng")]
        public QuanLyHopDong QuanLyHopDong
        {
            get
            {
                return _QuanLyHopDong;
            }
            set
            {
                SetPropertyValue("QuanLyHopDong", ref _QuanLyHopDong, value);
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại người ký")]
        public PhanLoaiNguoiKy PhanLoaiNguoiKy
        {
            get
            {
                return _PhanLoaiNguoiKy;
            }
            set
            {
                SetPropertyValue("PhanLoaiNguoiKy", ref _PhanLoaiNguoiKy, value);
                if (!IsLoading && ChucVuNguoiKy != null)
                {
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

        [ModelDefault("Caption", "Người ký")]
        [DataSourceProperty("NguoiKyList")]
        [RuleRequiredField(DefaultContexts.Save)]
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
        //

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NguoiKyList { get; set; }

        protected override void OnLoaded()
        {
            base.OnLoading();
            //
            UpdateNguoiKyList();
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNguoiKyList();
        }

        private void UpdateNguoiKyList()
        {
            if (NguoiKyList == null)
                NguoiKyList = new XPCollection<ThongTinNhanVien>(Session);

            if (PhanLoaiNguoiKy != null && ChucVuNguoiKy != null)
            NguoiKyList.Criteria = Common.Criteria_HopDong_NguoiKyTenTheoLoaiNguoiKyVaChucVu(PhanLoaiNguoiKy, ChucVuNguoiKy, CongTy);
        }

        public HopDong_ChonHopDongKhoan(Session session) : base(session) { }
    }
    #endregion

    #region 3. Phụ lục hợp đồng
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Hợp đồng")]
    public class HopDong_ChonPhuLucHopDong : OfficeBaseObject
    {
        private QuanLyHopDong _QuanLyHopDong;
        private ChucVuNguoiKy _ChucVuNguoiKy;
        private PhanLoaiNguoiKy _PhanLoaiNguoiKy;
        private ThongTinNhanVien _NguoiKy;

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Quản lý hợp đồng")]
        public QuanLyHopDong QuanLyHopDong
        {
            get
            {
                return _QuanLyHopDong;
            }
            set
            {
                SetPropertyValue("QuanLyHopDong", ref _QuanLyHopDong, value);
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại người ký")]
        public PhanLoaiNguoiKy PhanLoaiNguoiKy
        {
            get
            {
                return _PhanLoaiNguoiKy;
            }
            set
            {
                SetPropertyValue("PhanLoaiNguoiKy", ref _PhanLoaiNguoiKy, value);
                if (!IsLoading && ChucVuNguoiKy != null)
                {
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

        [ModelDefault("Caption", "Người ký")]
        [DataSourceProperty("NguoiKyList")]
        [RuleRequiredField(DefaultContexts.Save)]
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
        //

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NguoiKyList { get; set; }

        public HopDong_ChonPhuLucHopDong(Session session) : base(session) { }

        protected override void OnLoaded()
        {
            base.OnLoading();
            //
            UpdateNguoiKyList();
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNguoiKyList();
        }

        private void UpdateNguoiKyList()
        {
            if (NguoiKyList == null)
                NguoiKyList = new XPCollection<ThongTinNhanVien>(Session);

            if (PhanLoaiNguoiKy != null && ChucVuNguoiKy != null)
                NguoiKyList.Criteria = Common.Criteria_HopDong_NguoiKyTenTheoLoaiNguoiKyVaChucVu(PhanLoaiNguoiKy, ChucVuNguoiKy, CongTy);
        }
    }
    #endregion

}
