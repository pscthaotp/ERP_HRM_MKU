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
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using System.ComponentModel;
//
namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("Caption", "Quyết định biệt phái")]
    public class QuyetDinhBietPhai : QuyetDinhCaNhan
    {
        //
        private ChucVu _ChucVuCu;
        private ChucDanh _ChucDanhCu;
        private ChucVu _ChucVuMoi;
        private ChucDanh _ChucDanhMoi;
        private BoPhan _BoPhanMoi;

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

        [ModelDefault("Caption", "Đơn vị mới")]
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

        public QuyetDinhBietPhai(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhBietPhai;
            //
            QuyetDinhMoi = true;
            //
        }

        protected override void AfterNhanVienChanged()
        {
            if (ThongTinNhanVien != null)
            {
                ChucVuCu = ThongTinNhanVien.ChucVu;
                ChucDanhCu = ThongTinNhanVien.ChucDanh;
                //NgayBNChucVuCu = ThongTinNhanVien.NgayBoNhiemChucVu;
            }           
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //
                if (QuyetDinhMoi && ChucVuCu != ChucVuMoi && NgayHieuLuc <= Common.GetServerCurrentTime())
                {
                    //Cập nhất thông tin hồ sơ
                    ThongTinNhanVien.ChucVu = ChucVuMoi;
                    ThongTinNhanVien.ChucDanh = ChucDanhMoi;
                    ThongTinNhanVien.NgayBoNhiemChucVu = NgayHieuLuc;
                    ThongTinNhanVien.BoPhan = BoPhanMoi;

                    JobUpdated = true;
                }

                //Quá trình bổ nhiệm chức vụ
                ProcessesHelper.CreateQuaTrinhBietPhai(Session, this);
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                //Kiểm tra xem quyết định này có phải mới nhất không
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
                SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                using (XPCollection<QuyetDinhBietPhai> quyetdinh = new XPCollection<QuyetDinhBietPhai>(Session, filter, sort))
                {
                    quyetdinh.TopReturnedObjects = 1;
                    //
                    if (quyetdinh.Count > 0)
                    {
                        if (quyetdinh[0] == this)
                        {
                            //ThongTinNhanVien.NgayBoNhiemChucVu = NgayBNChucVuCu;
                            ThongTinNhanVien.ChucVu = ChucVuCu;
                            ThongTinNhanVien.ChucDanh = ChucDanhCu;
                            ThongTinNhanVien.BoPhan = BoPhan;
                        }
                    }
                }

                //Xóa quá trình điều động
                ProcessesHelper.DeleteQuaTrinhNhanVien<QuaTrinhDieuDong>(Session, this.Oid, this.ThongTinNhanVien.Oid);
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
