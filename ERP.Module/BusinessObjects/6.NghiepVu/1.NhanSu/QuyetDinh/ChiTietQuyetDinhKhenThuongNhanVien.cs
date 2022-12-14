using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;
using DevExpress.Xpo.DB;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định khen thưởng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhKhenThuong;ThongTinNhanVien")]
    public class ChiTietQuyetDinhKhenThuongNhanVien : BaseObject, IBoPhan
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private DanhHieuKhenThuong _DanhHieuKhenThuong;
        private string _LyDo;
        private QuyetDinhKhenThuong _QuyetDinhKhenThuong;
        //

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định khen thưởng")]
        [Association("QuyetDinhKhenThuong-ListChiTietQuyetDinhKhenThuongNhanVien")]
        public QuyetDinhKhenThuong QuyetDinhKhenThuong
        {
            get
            {
                return _QuyetDinhKhenThuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhKhenThuong", ref _QuyetDinhKhenThuong, value);
                //if (!IsLoading)
                //{
                //    if (value != null)
                //        NgayHuongLuongMoi = value.NgayHieuLuc;
                //}
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
                if (!IsLoading)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Cán bộ")]
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
                    AfterNhanVienChanged();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Danh hiệu")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public DanhHieuKhenThuong DanhHieuKhenThuong
        {
            get
            {
                return _DanhHieuKhenThuong;
            }
            set
            {
                SetPropertyValue("DanhHieuKhenThuong", ref _DanhHieuKhenThuong, value);
                //if (!IsLoading && value != null)
                //    UpdateDeNghiKhenThuong();
            }
        }

        [Size(500)]
        [ModelDefault("Caption", "Lý do")]
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }

        public ChiTietQuyetDinhKhenThuongNhanVien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNhanVienList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateNhanVienList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!IsLoading && !IsDeleted
                && QuyetDinhKhenThuong != null
                && !QuyetDinhKhenThuong.IsDirty)
                QuyetDinhKhenThuong.IsDirty = true;
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

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

        private void AfterNhanVienChanged()
        {
            BoPhan = ThongTinNhanVien.BoPhan;           
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            //
            if (!IsDeleted && Oid != Guid.Empty && Session is NestedUnitOfWork)
            {
                //Tạo quá trình khen thưởng khi lưu quyết định
                //if (!QuyetDinhKhenThuong.IsDirty)
                ProcessesHelper.CreateQuaTrinhKhenThuong(Session, ThongTinNhanVien, QuyetDinhKhenThuong, this);
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                //Xóa quá trình khen thưởng
                ProcessesHelper.DeleteQuaTrinhNhanVien<QuaTrinhKhenThuong>(Session, this.QuyetDinhKhenThuong.Oid, this.ThongTinNhanVien.Oid);
            }

            base.OnDeleting();
        }
    }

}
