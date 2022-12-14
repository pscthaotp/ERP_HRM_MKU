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
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.ComponentModel;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
//
namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("Caption", "Quyết định bổ nhiệm")]
    public class QuyetDinhBoNhiem : QuyetDinhCaNhan
    {
        //
        private ChucVu _ChucVuCu;
        private ChucDanh _ChucDanhCu;
        private decimal _PhuCapKiemNhiemCu;
        private decimal _PhuCapTrachNhiemCu;
        private DateTime _NgayBNChucVuCu;
        //
        private BoPhan _BoPhanMoi;
        private ChucVu _ChucVuMoi;
        private ChucDanh _ChucDanhMoi;
        private decimal _PhuCapKiemNhiemMoi;
        private decimal _PhuCapTrachNhiemMoi;
        private DateTime _NgayHetNhiemKy;

        [ModelDefault("Caption", "Chức vụ cũ")]
        [ModelDefault("AllowEdit", "False")]
        public ChucVu ChucVuCu
        {
            get
            {
                return _ChucVuCu;
            }
            set
            {
                SetPropertyValue("ChucVuCu", ref _ChucVuCu, value);
            }
        }

        [ModelDefault("Caption", "Chức danh cũ")]
        [ModelDefault("AllowEdit", "False")]
        public ChucDanh ChucDanhCu
        {
            get
            {
                return _ChucDanhCu;
            }
            set
            {
                SetPropertyValue("ChucDanhCu", ref _ChucDanhCu, value);
            }
        }


        [Appearance("PhuCapKiemNhiemCu", Visibility = ViewItemVisibility.Hide)]
        [ModelDefault("Caption", "PC kiêm nhiệm cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("AllowEdit", "False")]
        public decimal PhuCapKiemNhiemCu
        {
            get
            {
                return _PhuCapKiemNhiemCu;
            }
            set
            {
                SetPropertyValue("PhuCapKiemNhiemCu", ref _PhuCapKiemNhiemCu, value);
            }
        }


        [Appearance("PhuCapTrachNhiemCu", Visibility = ViewItemVisibility.Hide)]
        [ModelDefault("Caption", "PC trách nhiệm cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("AllowEdit", "False")]
        public decimal PhuCapTrachNhiemCu
        {
            get
            {
                return _PhuCapTrachNhiemCu;
            }
            set
            {
                SetPropertyValue("PhuCapTrachNhiemCu", ref _PhuCapTrachNhiemCu, value);
            }
        }


        [ModelDefault("Caption", "Ngày bổ nhiệm cũ")]
        [ModelDefault("AllowEdit", "False")]
        public DateTime NgayBNChucVuCu
        {
            get
            {
                return _NgayBNChucVuCu;
            }
            set
            {
                SetPropertyValue("NgayBNChucVuCu", ref _NgayBNChucVuCu, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhanMoi
        {
            get
            {
                return _BoPhanMoi;
            }
            set
            {
                SetPropertyValue("BoPhanMoi", ref _BoPhanMoi, value);              
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucVu ChucVuMoi
        {
            get
            {
                return _ChucVuMoi;
            }
            set
            {
                SetPropertyValue("ChucVuMoi", ref _ChucVuMoi, value);
                if (!IsLoading)
                {
                    ChucDanhMoi = null;
                    CapNhatChucDanh();
                }
            }
        }

        [ModelDefault("Caption", "Chức danh mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("CDList")]
        public ChucDanh ChucDanhMoi
        {
            get
            {
                return _ChucDanhMoi;
            }
            set
            {
                SetPropertyValue("ChucDanhMoi", ref _ChucDanhMoi, value);
            }
        }

        [Appearance("PhuCapKiemNhiemMoi", Visibility = ViewItemVisibility.Hide)]
        [ModelDefault("Caption", "PC kiêm nhiệm mới")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapKiemNhiemMoi
        {
            get
            {
                return _PhuCapKiemNhiemMoi;
            }
            set
            {
                SetPropertyValue("PhuCapKiemNhiemMoi", ref _PhuCapKiemNhiemMoi, value);
            }
        }

        [Appearance("PhuCapTrachNhiemMoi", Visibility = ViewItemVisibility.Hide)]
        [ModelDefault("Caption", "PC trách nhiệm mới")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTrachNhiemMoi
        {
            get
            {
                return _PhuCapTrachNhiemMoi;
            }
            set
            {
                SetPropertyValue("PhuCapTrachNhiemMoi", ref _PhuCapTrachNhiemMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày hết nhiệm kỳ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHetNhiemKy
        {
            get
            {
                return _NgayHetNhiemKy;
            }
            set
            {
                SetPropertyValue("NgayHetNhiemKy", ref _NgayHetNhiemKy, value);
            }
        }

        public QuyetDinhBoNhiem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhBoNhiem;
            //
            QuyetDinhMoi = true;
            //
        }

        protected override void AfterNhanVienChanged()
        {
            BoPhanMoi = ThongTinNhanVien.BoPhan;
            ChucVuCu = ThongTinNhanVien.ChucVu;
            ChucDanhCu = ThongTinNhanVien.ChucDanh;
            PhuCapKiemNhiemCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem;
            PhuCapTrachNhiemCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTrachNhiem;
            PhuCapKiemNhiemMoi = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem;
            PhuCapTrachNhiemMoi = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTrachNhiem;
            NgayBNChucVuCu = ThongTinNhanVien.NgayBoNhiemChucVu;
        }        

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //
                if (QuyetDinhMoi && ChucDanhCu != ChucDanhMoi && NgayHieuLuc <= Common.GetServerCurrentTime())
                {
                    //Cập nhất thông tin hồ sơ
                    ThongTinNhanVien.ChucVu = ChucVuMoi;
                    ThongTinNhanVien.ChucDanh = ChucDanhMoi;
                    ThongTinNhanVien.NgayBoNhiemChucVu = NgayHieuLuc;

                    JobUpdated = true;
                }

                //Quá trình bổ nhiệm chức vụ
                ProcessesHelper.CreateQuaTrinhBoNhiem(Session,this,ChucVuMoi,ChucDanhMoi,PhuCapKiemNhiemMoi,PhuCapTrachNhiemMoi,NgayHieuLuc,NgayHetNhiemKy,String.Empty);
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                //Kiểm tra xem quyết định này có phải mới nhất không
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
                SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                using (XPCollection<QuyetDinhBoNhiem> quyetdinh = new XPCollection<QuyetDinhBoNhiem>(Session, filter, sort))
                {
                    quyetdinh.TopReturnedObjects = 1;
                    //
                    if (quyetdinh.Count > 0)
                    {
                        if (quyetdinh[0] == this)
                        {
                            ThongTinNhanVien.NgayBoNhiemChucVu = NgayBNChucVuCu;
                            ThongTinNhanVien.ChucVu = ChucVuCu;
                            ThongTinNhanVien.ChucDanh = ChucDanhCu;
                        }
                    }
                }

                //Xóa quá trình bổ nhiệm
                ProcessesHelper.DeleteQuaTrinhNhanVien<QuaTrinhBoNhiem>(Session, this.Oid, this.ThongTinNhanVien.Oid);
            }

            base.OnDeleting();
        }

        [Browsable(false)]
        public XPCollection<ChucDanh> CDList { get; set; }

        public void CapNhatChucDanh()
        {
            if (CDList == null)
                CDList = new XPCollection<ChucDanh>(Session);
            //            
            if (ChucVuMoi != null)
                CDList.Filter = CriteriaOperator.Parse("ChucVu.Oid=?", ChucVuMoi.Oid);
        }
    }
}
