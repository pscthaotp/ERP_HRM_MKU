using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.NghiepVu.NhanSu.Helper;

namespace ERP.Module.NghiepVu.NhanSu.TuyenDung
{
    //[ImageName("BO_Resume")]
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [ModelDefault("Caption", "Hội đồng tuyển dụng")]
    [DefaultProperty("ThongTinNhanVien")]
    public class HoiDongTuyenDung : BaseObject, IBoPhan
    {
        // Fields...
        private QuyetDinhThanhLapHoiDongTuyenDung _QuyetDinhThanhLapHoiDongTuyenDung;
        private QuanLyTuyenDung _QuanLyTuyenDung;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private ChucVu _ChucVu;
        private ChucDanhHoiDong _ChucDanh;
        private string _VaiTroDamNhiem;
        
        
        [Browsable(false)]
        [Association("QuanLyTuyenDung-ListHoiDongTuyenDung")]
        public QuanLyTuyenDung QuanLyTuyenDung
        {
            get
            {
                return _QuanLyTuyenDung;
            }
            set
            {
                SetPropertyValue("QuanLyTuyenDung", ref _QuanLyTuyenDung, value);
            }
        }

        [Browsable(false)]
        [Association("QuyetDinhThanhLapHoiDongTuyenDung-ListHoiDongTuyenDung")]
        public QuyetDinhThanhLapHoiDongTuyenDung QuyetDinhThanhLapHoiDongTuyenDung
        {
            get
            {
                return _QuyetDinhThanhLapHoiDongTuyenDung;
            }
            set
            {
                SetPropertyValue("QuyetDinhThanhLapHoiDongTuyenDung", ref _QuyetDinhThanhLapHoiDongTuyenDung, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
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
                if (!IsLoading && value != null)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    ChucVu = value.ChucVu;
                }
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        public ChucVu ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
            }
        }

        [ModelDefault("Caption", "Nhiệm vụ")]
        public ChucDanhHoiDong ChucDanh
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

        [Size(500)]
        [ModelDefault("Caption", "Vai trò đảm nhiệm")]
        public string VaiTroDamNhiem
        {
            get
            {
                return _VaiTroDamNhiem;
            }
            set
            {
                SetPropertyValue("VaiTroDamNhiem", ref _VaiTroDamNhiem, value);
            }
        }

        public HoiDongTuyenDung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            UpdateNhanVienList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            if (!IsLoading && this.QuyetDinhThanhLapHoiDongTuyenDung != null)
            {
                this.QuyetDinhThanhLapHoiDongTuyenDung.IsDirty = true;
            }
            if (!IsDeleted && (Oid == Guid.Empty || Session is NestedUnitOfWork))
            {
                //4. qua trinh di cong tac
                ProcessesHelper.CreateQuaTrinhCongTac(Session, ThongTinNhanVien, this.QuyetDinhThanhLapHoiDongTuyenDung);
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                //4. delete quá trình đi cong tac
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and QuyetDinh=?",
                        ThongTinNhanVien, this.QuyetDinhThanhLapHoiDongTuyenDung);
                ProcessesHelper.DeleteQuaTrinh<QuaTrinhCongTac>(Session, filter);            
            }
            base.OnDeleting();
        }
    }

}
