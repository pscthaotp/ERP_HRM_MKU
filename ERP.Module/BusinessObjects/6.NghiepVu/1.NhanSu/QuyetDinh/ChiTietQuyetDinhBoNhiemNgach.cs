using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;
using DevExpress.Xpo.DB;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định bổ nhiệm ngạch")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhBoNhiemNgach;ThongTinNhanVien")]
    public class ChiTietQuyetDinhBoNhiemNgach : BaseObject
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private QuyetDinhBoNhiemNgach _QuyetDinhBoNhiemNgach;
        //
        private DateTime _NgayBoNhiemNgachCu;
        private DateTime _NgayHuongLuongCu;
        private NgachLuong _NgachLuongCu;
        private BacLuong _BacLuongCu;
        private decimal _HeSoLuongCu;
        private DateTime _MocNangLuongCu;
        //
        private DateTime _NgayBoNhiemNgachMoi;
        private DateTime _NgayHuongLuongMoi;
        private NgachLuong _NgachLuongMoi;
        private BacLuong _BacLuongMoi;
        private decimal _HeSoLuongMoi;

        [Browsable(false)]
        [Association("QuyetDinhBoNhiemNgach-ListChiTietQuyetDinhBoNhiemNgach")]
        public QuyetDinhBoNhiemNgach QuyetDinhBoNhiemNgach
        {
            get
            {
                return _QuyetDinhBoNhiemNgach;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoNhiemNgach", ref _QuyetDinhBoNhiemNgach, value);
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
                    //
                    if (BoPhan == null
                        || (BoPhan != null && value.BoPhan.Oid != BoPhan.Oid))
                        BoPhan = value.BoPhan;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NgachLuong NgachLuongMoi
        {
            get
            {
                return _NgachLuongMoi;
            }
            set
            {
                SetPropertyValue("NgachLuongMoi", ref _NgachLuongMoi, value);
                if (!IsLoading)
                {
                    BacLuongMoi = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương")]
        [DataSourceProperty("NgachLuong.ListBacLuong")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BacLuong BacLuongMoi
        {
            get
            {
                return _BacLuongMoi;
            }
            set
            {
                SetPropertyValue("BacLuongMoi", ref _BacLuongMoi, value);
            }
        }

        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoLuongMoi
        {
            get
            {
                return _HeSoLuongMoi;
            }
            set
            {
                SetPropertyValue("HeSoLuongMoi", ref _HeSoLuongMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm ngạch")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayBoNhiemNgachMoi
        {
            get
            {
                return _NgayBoNhiemNgachMoi;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemNgachMoi", ref _NgayBoNhiemNgachMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHuongLuongMoi
        {
            get
            {
                return _NgayHuongLuongMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongLuongMoi", ref _NgayHuongLuongMoi, value);
            }
        }

        //

        [Browsable(false)]
        public NgachLuong NgachLuongCu
        {
            get
            {
                return _NgachLuongCu;
            }
            set
            {
                SetPropertyValue("NgachLuongCu", ref _NgachLuongCu, value);
            }
        }

        [Browsable(false)]
        public BacLuong BacLuongCu
        {
            get
            {
                return _BacLuongCu;
            }
            set
            {
                SetPropertyValue("BacLuongCu", ref _BacLuongCu, value);
            }
        }

        [Browsable(false)]
        public decimal HeSoLuongCu
        {
            get
            {
                return _HeSoLuongCu;
            }
            set
            {
                SetPropertyValue("HeSoLuongCu", ref _HeSoLuongCu, value);
            }
        }

        [Browsable(false)]
        public DateTime NgayBoNhiemNgachCu
        {
            get
            {
                return _NgayBoNhiemNgachCu;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemNgachCu", ref _NgayBoNhiemNgachCu, value);
            }
        }

        [Browsable(false)]
        public DateTime NgayHuongLuongCu
        {
            get
            {
                return _NgayHuongLuongCu;
            }
            set
            {
                SetPropertyValue("NgayHuongLuongCu", ref _NgayHuongLuongCu, value);
            }
        }

        [Browsable(false)]
        public DateTime MocNangLuongCu
        {
            get
            {
                return _MocNangLuongCu;
            }
            set
            {
                SetPropertyValue("MocNangLuongCu", ref _MocNangLuongCu, value);
            }
        }

        public ChiTietQuyetDinhBoNhiemNgach(Session session) : base(session) { }
        private void AfterNhanVienChanged()
        {
            NgachLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong;
            BacLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.BacLuong;
            NgayBoNhiemNgachCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach;
            NgayHuongLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong;
            MocNangLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh != DateTime.MinValue ? ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh : ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongLanSau;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNhanVienList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (QuyetDinhBoNhiemNgach != null
                && !IsLoading
                && !QuyetDinhBoNhiemNgach.IsDirty)
                QuyetDinhBoNhiemNgach.IsDirty = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateNhanVienList();
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

        protected override void OnSaving()
        {
            base.OnSaving();
            //
            if (!IsDeleted && Oid != Guid.Empty)
            {
                if (QuyetDinhBoNhiemNgach.QuyetDinhMoi)
                {
                    //1. Cập nhật thông tin lương
                    ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuongMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuongMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach = NgayBoNhiemNgachMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuongMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongLanSau = MocNangLuongCu;
                }

                //2. Cập nhật diễn biến lương của quyết định trước đó
                ProcessesHelper.UpdateDienBienLuong(Session, QuyetDinhBoNhiemNgach, ThongTinNhanVien, NgayHuongLuongMoi, true);

                //3. Tạo diễn biến lương
                ProcessesHelper.CreateDienBienLuong(Session, QuyetDinhBoNhiemNgach, ThongTinNhanVien, this);
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                //1.
                //Lưu ý: Kiểm tra diễn biến lương mới nhất lấy quyết định ra kiểm tra với quyết định hiện tại
                //Tránh trường hợp nếu xóa quyết định này mà đã lập quyết định khác mới hơn
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay>?", ThongTinNhanVien, NgayHuongLuongMoi);
                SortProperty sort = new SortProperty("TuNgay", SortingDirection.Descending);
                using (XPCollection<DienBienLuong> dblList = new XPCollection<DienBienLuong>(Session, filter, sort))
                {
                    dblList.TopReturnedObjects = 1;
                    //
                    if (dblList.Count == 0 ||
                        (dblList.Count == 1 && dblList[0].QuyetDinh == QuyetDinhBoNhiemNgach))
                    {
                        ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuongCu;
                        ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuongCu;
                        ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuongCu;
                        ThongTinNhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach = NgayBoNhiemNgachCu;
                    }
                }

                //2. Cập nhật diễn biến lương của quyết định trước đó
                ProcessesHelper.UpdateDienBienLuong(Session, QuyetDinhBoNhiemNgach, ThongTinNhanVien, DateTime.MinValue, false);

                //3. Xóa diễn biến lương
                ProcessesHelper.DeleteQuaTrinh<DienBienLuong>(Session, CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", QuyetDinhBoNhiemNgach.Oid, ThongTinNhanVien.Oid));

            }
            base.OnDeleting();
        }
    }

}
