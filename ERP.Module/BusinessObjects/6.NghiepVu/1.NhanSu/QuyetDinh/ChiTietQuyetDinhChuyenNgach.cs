using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định chuyển ngạch")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhChuyenNgach;ThongTinNhanVien")]
    public class ChiTietQuyetDinhChuyenNgach : BaseObject
    {
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private QuyetDinhChuyenNgach _QuyetDinhChuyenNgach;
        //     
        private NgachLuong _NgachLuongCu;
        private BacLuong _BacLuongCu;
        private decimal _HeSoLuongCu;
        private int _VuotKhungCu; 
        private DateTime _NgayHuongLuongCu;
        private DateTime _MocNangLuongCu;
        //
        private NgachLuong _NgachLuongMoi;
        private BacLuong _BacLuongMoi;
        private decimal _HeSoLuongMoi;
        private int _VuotKhungMoi; 
        private DateTime _NgayHuongLuongMoi;
        
        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định chuyển ngạch")]
        [Association("QuyetDinhChuyenNgach-ListChiTietQuyetDinhChuyenNgach")]
        public QuyetDinhChuyenNgach QuyetDinhChuyenNgach
        {
            get
            {
                return _QuyetDinhChuyenNgach;
            }
            set
            {
                SetPropertyValue("QuyetDinhChuyenNgach", ref _QuyetDinhChuyenNgach, value);
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
                    //
                    AfterNhanVienChanged();
                    //
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
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

        [ModelDefault("Caption", "% vuợt khung cũ")]
        public int VuotKhungCu
        {
            get
            {
                return _VuotKhungCu;
            }
            set
            {
                SetPropertyValue("VuotKhungCu", ref _VuotKhungCu, value);
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

        [ModelDefault("Caption", "% vuợt khung mới")]
        public int VuotKhungMoi
        {
            get
            {
                return _VuotKhungMoi;
            }
            set
            {
                SetPropertyValue("VuotKhungMoi", ref _VuotKhungMoi, value);
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


        public ChiTietQuyetDinhChuyenNgach(Session session) : base(session) { }

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
            NgayHuongLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted
                && Oid == Guid.Empty)
            {
                //1. Cập nhật hồ sơ
                if (QuyetDinhChuyenNgach.QuyetDinhMoi)
                {
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach = NgayHuongLuongMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuongMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuongMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuongMoi;
                }

                //2. Cập nhật diễn biến lương của quyết định trước đó
                ProcessesHelper.UpdateDienBienLuong(Session, QuyetDinhChuyenNgach, ThongTinNhanVien, NgayHuongLuongMoi,true);

                //3. Tạo diễn biến lương mới
                ProcessesHelper.CreateDienBienLuong(Session, QuyetDinhChuyenNgach, ThongTinNhanVien, this);
            }
        }

        protected override void OnDeleting()
        {

            if (!IsSaving && QuyetDinhChuyenNgach.QuyetDinhMoi)
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
                        (dblList.Count == 1 && dblList[0].QuyetDinh == QuyetDinhChuyenNgach))
                    {
                        ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuongCu;
                        ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuongCu;
                        ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuongCu;
                        ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung = VuotKhungCu;
                    }
                }
            }

            //2. Cập nhật diễn biến lương của quyết định trước đó
            ProcessesHelper.UpdateDienBienLuong(Session, QuyetDinhChuyenNgach, ThongTinNhanVien, DateTime.MinValue, false);

            //3. Xóa diễn biến lương
            ProcessesHelper.DeleteQuaTrinh<DienBienLuong>(Session, CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", QuyetDinhChuyenNgach, ThongTinNhanVien));

            base.OnDeleting();
        }
    }

}
