using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;
using DevExpress.Xpo.DB;

//
namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định nâng ngạch lương")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhNangNgach;ThongTinNhanVien")]
    public class ChiTietQuyetDinhNangNgach : BaseObject
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private QuyetDinhNangNgach _QuyetDinhNangNgach;
        private BacLuong _BacLuongMoi;
        private decimal _HeSoLuongMoi;
        private DateTime _NgayBoNhiemNgachMoi;
        private DateTime _NgayHuongLuongMoi;
        private NgachLuong _NgachLuongMoi;
        //
        private DateTime _NgayBoNhiemNgachCu;
        private DateTime _NgayHuongLuongCu;
        private NgachLuong _NgachLuongCu;
        private BacLuong _BacLuongCu;
        private decimal _HeSoLuongCu;
        private DateTime _MocNangLuongCu;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định nâng ngạch")]
        [Association("QuyetDinhNangNgach-ListChiTietQuyetDinhNangNgach")]
        public QuyetDinhNangNgach QuyetDinhNangNgach
        {
            get
            {
                return _QuyetDinhNangNgach;
            }
            set
            {
                SetPropertyValue("QuyetDinhNangNgach", ref _QuyetDinhNangNgach, value);
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
                    if (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    AfterNhanVienChanged();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương cũ")]
        public NgachLuong NgachLuongCu
        {
            get
            {
                return _NgachLuongCu;
            }
            set
            {
                SetPropertyValue("NgachLuongCu", ref _NgachLuongCu, value);
                if (!IsLoading)
                {
                    BacLuongCu = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương cũ")]
        [DataSourceProperty("NgachLuongCu.ListBacLuong")]
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

        [ModelDefault("Caption", "Hệ số lương cũ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
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

        [ModelDefault("Caption", "Ngày bổ nhiệm ngạch cũ")]
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

        [ModelDefault("Caption", "Ngày hưởng lương cũ")]
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

        [ModelDefault("Caption", "Mốc nâng lương cũ")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương mới")]
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
        [ModelDefault("Caption", "Bậc lương mới")]
        [DataSourceProperty("NgachLuongMoi.ListBacLuong")]
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

        [ModelDefault("Caption", "Hệ số lương mới")]
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

        [ModelDefault("Caption", "Ngày bổ nhiệm ngạch mới")]
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


        [ModelDefault("Caption", "Ngày hưởng lương mới")]
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

        public ChiTietQuyetDinhNangNgach(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNhanVienList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!IsLoading && !IsDeleted
                && QuyetDinhNangNgach != null
                && !QuyetDinhNangNgach.IsDirty)
                QuyetDinhNangNgach.IsDirty = true;
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

        private void AfterNhanVienChanged()
        {
            NgachLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong;
            BacLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.BacLuong;
            NgayBoNhiemNgachCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach;
            NgayHuongLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong;
            MocNangLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh != DateTime.MinValue ? ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh : ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongLanSau;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                if (QuyetDinhNangNgach.QuyetDinhMoi)
                {
                    //Cập nhật thông tin lương
                    ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuongMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuongMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach = NgayBoNhiemNgachMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuongMoi;
                }

                //1. Cập nhật diễn biến lương trước đó
                ProcessesHelper.UpdateDienBienLuong(Session, QuyetDinhNangNgach, ThongTinNhanVien, NgayHuongLuongMoi, true);
          
                //2. Tạo diễn biến lương mới
                ProcessesHelper.CreateDienBienLuong(Session, QuyetDinhNangNgach, ThongTinNhanVien, this);
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                if (QuyetDinhNangNgach.QuyetDinhMoi)
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
                            (dblList.Count == 1 && dblList[0].QuyetDinh == QuyetDinhNangNgach))
                        {
                            ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach = NgayBoNhiemNgachCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuongCu;
                        }
                    }
                }

                //Cập đến ngày của diễn biến lương và đến năm của quá trình tham gia bảo hiểm trước đó = null
                ProcessesHelper.UpdateDienBienLuong(Session, QuyetDinhNangNgach, ThongTinNhanVien, DateTime.MinValue, false);

                //Xóa diễn biến lương
                ProcessesHelper.DeleteQuaTrinh<DienBienLuong>(Session, CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", QuyetDinhNangNgach, ThongTinNhanVien));
            }

            base.OnDeleting();
        }
    }

}
