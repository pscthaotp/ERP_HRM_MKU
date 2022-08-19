using System;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.TienLuong;
//
namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("Caption", "Quyết định tiền lương chính thức")]
    public class QuyetDinhTienLuongChinhThuc : QuyetDinhCaNhan
    {
        //     
        private NgachLuong _NgachLuongCu;
        private BacLuong _BacLuongCu;
        private decimal _LuongCoBanCu;
        private decimal _LuongKinhDoanhCu;
        private DateTime _NgayHuongLuongCu;
        private decimal _MucHuongCu;
        private NgachLuong _NgachLuongMoi;
        private BacLuong _BacLuongMoi;
        private decimal _LuongCoBanMoi;
        private decimal _LuongKinhDoanhMoi;
        private decimal _MucHuongMoi;

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
                    BacLuongMoi = null;
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
                if (!IsLoading)
                {
                    if (value != null)
                    {
                        LuongCoBanCu = value.LuongCoBan;
                        LuongKinhDoanhCu = value.LuongKinhDoanh;
                    }
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Lương chức danh cũ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        //[ModelDefault("AllowEdit", "False")]
        public decimal LuongCoBanCu
        {
            get
            {
                return _LuongCoBanCu;
            }
            set
            {
                SetPropertyValue("LuongCoBanCu", ref _LuongCoBanCu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Lương bổ sung (HQCV) cũ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        //[ModelDefault("AllowEdit", "False")]
        public decimal LuongKinhDoanhCu
        {
            get
            {
                return _LuongKinhDoanhCu;
            }
            set
            {
                SetPropertyValue("LuongKinhDoanhCu", ref _LuongKinhDoanhCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương cũ")]
        [ModelDefault("AllowEdit", "False")]
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

        [ModelDefault("Caption", "Mức hưởng thử việc cũ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("AllowEdit", "False")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public decimal MucHuongThuViecCu
        {
            get
            {
                return _MucHuongCu;
            }
            set
            {
                SetPropertyValue("MucHuongThuViecCu", ref _MucHuongCu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương mới")]
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
        public BacLuong BacLuongMoi
        {
            get
            {
                return _BacLuongMoi;
            }
            set
            {
                SetPropertyValue("BacLuongMoi", ref _BacLuongMoi, value);
                if (!IsLoading)
                {
                    if (value != null)
                    {
                        LuongCoBanMoi = value.LuongCoBan;
                        LuongKinhDoanhMoi = value.LuongKinhDoanh;
                    }
                }
            }
        }

        [ModelDefault("Caption", "Lương chức danh mới")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal LuongCoBanMoi
        {
            get
            {
                return _LuongCoBanMoi;
            }
            set
            {
                SetPropertyValue("LuongCoBanMoi", ref _LuongCoBanMoi, value);
            }
        }        

        [ModelDefault("Caption", "Lương bổ sung (HQCV) mới")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal LuongKinhDoanhMoi
        {
            get
            {
                return _LuongKinhDoanhMoi;
            }
            set
            {
                SetPropertyValue("LuongKinhDoanhMoi", ref _LuongKinhDoanhMoi, value);
            }
        }        

        [ModelDefault("Caption", "Mức hưởng mới")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal MucHuongThuViecMoi
        {
            get
            {
                return _MucHuongMoi;
            }
            set
            {
                SetPropertyValue("MucHuongThuViecMoi", ref _MucHuongMoi, value);
            }
        }

        public QuyetDinhTienLuongChinhThuc(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhTienLuongChinhThuc;  
            QuyetDinhMoi = true;
        
        }

        protected override void AfterNhanVienChanged()
        {
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
            SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
            using (XPCollection<QuyetDinhTienLuongThuViec> quyetdinh = new XPCollection<QuyetDinhTienLuongThuViec>(Session, filter, sort))
            {
                quyetdinh.TopReturnedObjects = 1;
                //
                if (quyetdinh.Count > 0)
                {
                    MucHuongThuViecCu = quyetdinh[0].MucHuongThuViec;
                }
                else
                {
                    MucHuongThuViecCu = ThongTinNhanVien.NhanVienThongTinLuong.PhanTramTinhLuong;
                }
            }

            NgachLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong;
            BacLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.BacLuong;
            LuongCoBanCu = ThongTinNhanVien.NhanVienThongTinLuong.LuongCoBan;
            LuongKinhDoanhCu = ThongTinNhanVien.NhanVienThongTinLuong.LuongKinhDoanh;
            NgayHuongLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //
                if (QuyetDinhMoi && NgayHieuLuc <= Common.GetServerCurrentTime())
                {
                    //Cập nhất thông tin hồ sơ
                    ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuongMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuongMoi;                 
                    ThongTinNhanVien.NhanVienThongTinLuong.LuongCoBan = LuongCoBanMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.LuongKinhDoanh = LuongKinhDoanhMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHieuLuc;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhanTramTinhLuong = MucHuongThuViecMoi;

                    JobUpdated = true;
                }
                //Quá trình diễn biến lương
                ProcessesHelper.CreateDienBienLuong(Session, this, ThongTinNhanVien, this);
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                //Kiểm tra xem quyết định này có phải mới nhất không
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
                SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                using (XPCollection<QuyetDinhTienLuongChinhThuc> quyetdinh = new XPCollection<QuyetDinhTienLuongChinhThuc>(Session, filter, sort))
                {
                    quyetdinh.TopReturnedObjects = 1;
                    //
                    if (quyetdinh.Count > 0)
                    {
                        if (quyetdinh[0] == this)
                        {
                            ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.LuongCoBan = LuongCoBanCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.LuongKinhDoanh = LuongKinhDoanhCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.PhanTramTinhLuong = MucHuongThuViecCu;
                        }
                    }
                }
                //Xóa quá trình bổ nhiệm
                ProcessesHelper.DeleteQuaTrinhNhanVien<DienBienLuong>(Session, this.Oid, this.ThongTinNhanVien.Oid);
            }
           base.OnDeleting();
        }
    }
}
