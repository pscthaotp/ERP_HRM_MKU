using System;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.TienLuong;
//
namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("Caption", "Quyết định tiền lương thử việc")]
    public class QuyetDinhTienLuongThuViec : QuyetDinhCaNhan
    {       
        private decimal _LuongCoBanCu;
        private decimal _LuongKinhDoanhCu;
        private NgachLuong _NgachLuongMoi;
        private BacLuong _BacLuongMoi;
        private decimal _LuongCoBanMoi;
        private decimal _LuongKinhDoanhMoi;
        private decimal _MucHuong;

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

        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Lương chức danh cũ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField(DefaultContexts.Save)]        
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
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
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


        [ImmediatePostData]
        [ModelDefault("Caption", "Lương chức danh mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Lương bổ sung (HQCV) mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
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


        [ModelDefault("Caption", "Mức hưởng thử việc")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal MucHuongThuViec
        {
            get
            {
                return _MucHuong;
            }
            set
            {
                SetPropertyValue("MucHuongThuViec", ref _MucHuong, value);
            }
        }

        public QuyetDinhTienLuongThuViec(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            QuyetDinhMoi = true;
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhTienLuongThuViec;
            //
        }

        protected override void AfterNhanVienChanged()
        {
            MucHuongThuViec = ThongTinNhanVien.NhanVienThongTinLuong.PhanTramTinhLuong;
            LuongCoBanCu = ThongTinNhanVien.NhanVienThongTinLuong.LuongCoBan;
            LuongKinhDoanhCu = ThongTinNhanVien.NhanVienThongTinLuong.LuongKinhDoanh;
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
                    ThongTinNhanVien.NhanVienThongTinLuong.PhanTramTinhLuong = MucHuongThuViec;

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
                using (XPCollection<QuyetDinhTienLuongThuViec> quyetdinh = new XPCollection<QuyetDinhTienLuongThuViec>(Session, filter, sort))
                {
                    quyetdinh.TopReturnedObjects = 1;
                    //
                    if (quyetdinh.Count > 0)
                    {
                        if (quyetdinh[0] == this)
                        {
                            ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = null;
                            ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = null;
                            //ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = DateTime.MinValue;
                            ThongTinNhanVien.NhanVienThongTinLuong.LuongCoBan = LuongCoBanCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.LuongKinhDoanh = LuongKinhDoanhCu;
                            //ThongTinNhanVien.NhanVienThongTinLuong.PhanTramTinhLuong = 0;
                        }
                    }
                }

                //Xóa diễn biến lương
                ProcessesHelper.DeleteQuaTrinhNhanVien<DienBienLuong>(Session, this.Oid, this.ThongTinNhanVien.Oid);
            }

            base.OnDeleting();
        }
    }
}
