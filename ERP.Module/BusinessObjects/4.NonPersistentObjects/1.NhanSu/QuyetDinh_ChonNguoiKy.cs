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
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Quyết định")]
    public class QuyetDinh_ChonNguoiKy : OfficeBaseObject
    {
        private ChucVuNguoiKy _ChucVuNguoiKy;
        private ThongTinNhanVien _NguoiKy;
        private LoaiCongTy _LoaiCongTy;
        private PhanLoaiNguoiKy _PhanLoaiNguoiKy;
        //
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
                    NguoiKy = null;
                }
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


        [Browsable(false)]
        public XPCollection<PhanLoaiNguoiKy> PhanLoaiNguoiKyList { get; set; }

        public QuyetDinh_ChonNguoiKy(Session session) : base(session) { }

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
            CongTy = Common.CongTy(Session);
            //
            if (Common.TaiKhoanEdu())
            {
                LoaiCongTy = Session.FindObject<LoaiCongTy>(CriteriaOperator.Parse("TenLoaiCongTy like ?", "%Công ty%"));
                PhanLoaiNguoiKy = Session.FindObject<PhanLoaiNguoiKy>(CriteriaOperator.Parse("TenPhanLoaiNguoiKy like ? and LoaiCongTy=?", "%đang tại chức%", LoaiCongTy != null ? LoaiCongTy.Oid : Guid.Empty));
                ChucVuNguoiKy = Session.FindObject<ChucVuNguoiKy>(CriteriaOperator.Parse("ChucDanh.TenChucDanh like ?", "Tổng Giám đốc"));
                NguoiKy = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ChucDanh.Oid = ? and !TinhTrang.DaNghiViec and CongTy=?", ChucVuNguoiKy != null ? ChucVuNguoiKy.ChucDanh.Oid : Guid.Empty, CongTy!= null ? CongTy.Oid : Guid.Empty));
            }
            else
            {
                LoaiCongTy = Session.FindObject<LoaiCongTy>(CriteriaOperator.Parse("TenLoaiCongTy like ?", "%Trường%"));
                PhanLoaiNguoiKy = Session.FindObject<PhanLoaiNguoiKy>(CriteriaOperator.Parse("TenPhanLoaiNguoiKy like ? and LoaiCongTy=?", "%đang tại chức%", LoaiCongTy != null ? LoaiCongTy.Oid : Guid.Empty));
                ChucVuNguoiKy = Session.FindObject<ChucVuNguoiKy>(CriteriaOperator.Parse("ChucDanh.TenChucDanh like ?", "Hiệu trưởng"));
                NguoiKy = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ChucDanh.Oid = ? and !TinhTrang.DaNghiViec and CongTy.Oid=?", ChucVuNguoiKy != null ? ChucVuNguoiKy.ChucDanh.Oid : Guid.Empty, CongTy != null ? CongTy.Oid : Guid.Empty));
            }
            //
            UpdateNguoiKyList();
        }

        private void UpdateNguoiKyList()
        {
            if (NguoiKyList == null)
                NguoiKyList = new XPCollection<ThongTinNhanVien>(Session);
            //NguoiKyList.Criteria = CriteriaOperator.Parse("CongTy = ? and TinhTrang.TenTinhTrang like '%đang làm việc%'", Common.SecuritySystemUser_GetCurrentUser().CongTy.Oid);

            if (PhanLoaiNguoiKy != null && ChucVuNguoiKy != null)
                NguoiKyList.Criteria = Common.Criteria_HopDong_NguoiKyTenTheoLoaiNguoiKyVaChucVu(PhanLoaiNguoiKy, ChucVuNguoiKy, CongTy);
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
    }
   

}
