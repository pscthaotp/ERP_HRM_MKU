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
//
namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("Caption", "Quyết định tái bổ nhiệm")]
    public class QuyetDinhTaiBoNhiem : QuyetDinhCaNhan
    {
        //
        private ChucVu _ChucVuCu;
        private ChucDanh _ChucDanhCu;
        private decimal _PhuCapKiemNhiemCu;
        private decimal _PhuCapTrachNhiemCu;
        private DateTime _NgayBNChucVuCu;
        //
        private QuyetDinhBoNhiem _QuyetDinhBoNhiem;
        private DateTime _NgayHetNhiemKy;

        [ModelDefault("Caption", "Chức vụ cũ")]
        //[ModelDefault("AllowEdit", "False")]
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
        //[ModelDefault("AllowEdit", "False")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Quyết định bổ nhiệm")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("QuyetDinhBoNhiemList", DataSourcePropertyIsNullMode.SelectAll)]
        public QuyetDinhBoNhiem QuyetDinhBoNhiem
        {
            get
            {
                return _QuyetDinhBoNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoNhiem", ref _QuyetDinhBoNhiem, value);
                if (!IsLoading && value != null)
                {
                    ChucVuCu = QuyetDinhBoNhiem.ChucVuMoi;
                    ChucDanhCu = QuyetDinhBoNhiem.ChucDanhMoi;
                    PhuCapKiemNhiemCu = QuyetDinhBoNhiem.PhuCapKiemNhiemMoi;
                    PhuCapTrachNhiemCu = QuyetDinhBoNhiem.PhuCapTrachNhiemMoi;
                    NgayBNChucVuCu = QuyetDinhBoNhiem.NgayHieuLuc;
                }
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

        [Browsable(false)]
        public XPCollection<QuyetDinhBoNhiem> QuyetDinhBoNhiemList { get; set; }

        public QuyetDinhTaiBoNhiem(Session session) : base(session) { }

        private void UpdateHDList()
        {
            if (QuyetDinhBoNhiemList == null)
                QuyetDinhBoNhiemList = new XPCollection<QuyetDinhBoNhiem>(Session);
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien.Oid=?", ThongTinNhanVien != null ? ThongTinNhanVien.Oid : Guid.Empty);
            SortingCollection sort = new SortingCollection();
            sort.Add(new SortProperty("NgayHieuLuc", SortingDirection.Descending));
            QuyetDinhBoNhiemList.Filter = filter;
            QuyetDinhBoNhiemList.Sorting = sort;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhTaiBoNhiem;
            //
            QuyetDinhMoi = true;
        }

        protected override void AfterNhanVienChanged()
        {
            UpdateHDList();
            ChucVuCu = ThongTinNhanVien.ChucVu;
            ChucDanhCu = ThongTinNhanVien.ChucDanh;
            PhuCapKiemNhiemCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem;
            PhuCapTrachNhiemCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTrachNhiem;
            NgayBNChucVuCu = ThongTinNhanVien.NgayBoNhiemChucVu;
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
                    ThongTinNhanVien.NgayBoNhiemChucVu = NgayHieuLuc;

                    JobUpdated = true;
                }

                //Quá trình bổ nhiệm chức vụ
                ProcessesHelper.CreateQuaTrinhBoNhiem(Session, this, ChucVuCu, ChucDanhCu, PhuCapKiemNhiemCu, PhuCapTrachNhiemCu, NgayHieuLuc, NgayHetNhiemKy, "tái bổ nhiệm");
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                //Kiểm tra xem quyết định này có phải mới nhất không
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
                SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                using (XPCollection<QuyetDinhTaiBoNhiem> quyetdinh = new XPCollection<QuyetDinhTaiBoNhiem>(Session, filter, sort))
                {
                    quyetdinh.TopReturnedObjects = 1;
                    //
                    if (quyetdinh.Count > 0)
                    {
                        if (quyetdinh[0] == this)
                        {
                            ThongTinNhanVien.NgayBoNhiemChucVu = NgayBNChucVuCu;
                        }
                    }
                }

                //Xóa quá trình bổ nhiệm
                ProcessesHelper.DeleteQuaTrinhNhanVien<QuaTrinhBoNhiem>(Session, this.Oid, this.ThongTinNhanVien.Oid);
            }

            base.OnDeleting();
        }
    }
}
