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
    [ModelDefault("Caption", "Quyết định miễn nhiệm")]
    [Appearance("Hide", TargetItems = "NgayHetNhiemKy",Visibility = ViewItemVisibility.Hide, Criteria = "ChucDanhMoi is null and ChucVuMoi is null")]
    public class QuyetDinhMienNhiem : QuyetDinhCaNhan
    {
        //
        private ChucVu _ChucVuCu;
        private ChucDanh _ChucDanhCu;
        private BoPhan _BoPhanMoi;
        private ChucVu _ChucVuMoi;
        private ChucDanh _ChucDanhMoi;
        private DateTime _NgayBoNhiemCu;
        private DateTime _NgayHetNhiemKyMoi;


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


 
        [ModelDefault("Caption", "Ngày bổ nhiệm cũ")]
        [ModelDefault("AllowEdit", "False")]
        public DateTime NgayBoNhiemCu
        {
            get
            {
                return _NgayBoNhiemCu;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemCu", ref _NgayBoNhiemCu, value);
            }
        }


        [ModelDefault("Caption", "Ngày hết nhiệm kỳ")]
        public DateTime NgayHetNhiemKy
        {
            get
            {
                return _NgayHetNhiemKyMoi;
            }
            set
            {
                SetPropertyValue("NgayHetNhiemKy", ref _NgayHetNhiemKyMoi, value);
            }
        }

        public QuyetDinhMienNhiem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhMienNhiem;
            //
            QuyetDinhMoi = true;
            //
        }

        protected override void AfterNhanVienChanged()
        {
            BoPhanMoi = ThongTinNhanVien.BoPhan;
            ChucVuCu = ThongTinNhanVien.ChucVu;
            ChucDanhCu = ThongTinNhanVien.ChucDanh;
            NgayBoNhiemCu = ThongTinNhanVien.NgayBoNhiemChucVu;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {  
                //
                if (QuyetDinhMoi && NgayHieuLuc <= Common.GetServerCurrentTime())
                {
                    //Cập nhất thông tin nhân viên
                    ThongTinNhanVien.ChucVu = ChucVuMoi;
                    ThongTinNhanVien.ChucDanh = ChucDanhMoi;
                    ThongTinNhanVien.NgayBoNhiemChucVu = NgayHieuLuc;

                    JobUpdated = true;
                }
                
                ProcessesHelper.CreateQuaTrinhBoNhiem(Session, this, ChucVuMoi,ChucDanhMoi, ThongTinNhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem,ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTrachNhiem, NgayHieuLuc,NgayHetNhiemKy,"miễn nhiệm");
            } 
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                if (QuyetDinhMoi)
                {
                    //Kiểm tra xem quyết định này có phải mới nhất không
                    CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
                    SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                    using (XPCollection<QuyetDinhMienNhiem> quyetdinh = new XPCollection<QuyetDinhMienNhiem>(Session, filter, sort))
                    {
                        quyetdinh.TopReturnedObjects = 1;
                        //
                        if (quyetdinh.Count > 0)
                        {
                            if (quyetdinh[0] == this)
                            {
                                ThongTinNhanVien.ChucVu = ChucVuCu;
                                ThongTinNhanVien.ChucDanh = ChucDanhCu;
                                ThongTinNhanVien.NgayBoNhiemChucVu = NgayBoNhiemCu;
                            }
                        }
                    }
                }

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
