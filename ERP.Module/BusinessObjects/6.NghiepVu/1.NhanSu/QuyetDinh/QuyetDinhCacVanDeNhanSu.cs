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
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Enum.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.ComponentModel;
//
namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("Caption", "Quyết định các vấn đề nhân sự")]
    [Appearance("Hide", TargetItems = "LuongCoBanMoi;LuongCoBanCu;LuongKinhDoanhMoi;LuongKinhDoanhCu;LuongBoSungKhacCu;LuongBoSungKhacMoi",
     Visibility = ViewItemVisibility.Hide, Criteria = "LoaiQuyetDinhVaHopDong != 'DieuChinhTienLuong'")]
    public class QuyetDinhCacVanDeNhanSu : QuyetDinhCaNhan
    {

        private BoPhan _BoPhanMoi;
        private ChucVu _ChucVuCu;
        private ChucDanh _ChucDanhCu;
        private ChucVu _ChucVuMoi;
        private ChucDanh _ChucDanhMoi;
        private decimal _LuongCoBanCu;
        private decimal _LuongKinhDoanhCu;
        private decimal _LuongCoBanMoi;
        private decimal _LuongKinhDoanhMoi;
        private decimal _LuongBoSungKhacCu;
        private decimal _LuongBoSungKhacMoi;
        private string _CheDoCu;
        private string _CheDoMoi;

        private LoaiQuyetDinhVaHopDongEnum _LoaiQuyetDinhVaHopDong;


        [ModelDefault("Caption", "Nội dung đề xuất")]
        public LoaiQuyetDinhVaHopDongEnum LoaiQuyetDinhVaHopDong
        {
            get
            {
                return _LoaiQuyetDinhVaHopDong;
            }
            set
            {
                SetPropertyValue("LoaiQuyetDinhVaHopDong", ref _LoaiQuyetDinhVaHopDong, value);
            }
        }


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
     

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ mới")]
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

  

   

        [ModelDefault("Caption", "Lương chức danh cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("AllowEdit", "False")]
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

        [ModelDefault("Caption", "Lương bổ sung (HQCV) cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("AllowEdit", "False")]
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


        [ModelDefault("Caption", "Lương chức danh mới")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
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
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
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



        [ModelDefault("Caption", "Lương bổ sung khác cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LuongBoSungKhacCu
        {
            get
            {
                return _LuongBoSungKhacCu;
            }
            set
            {
                SetPropertyValue("LuongBoSungKhacCu", ref _LuongBoSungKhacCu, value);
            }
        }

        [ModelDefault("Caption", "Lương bổ sung khác mới")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LuongBoSungKhacMoi
        {
            get
            {
                return _LuongBoSungKhacMoi;
            }
            set
            {
                SetPropertyValue("LuongBoSungKhacCu", ref _LuongBoSungKhacMoi, value);
            }
        }


        [ModelDefault("Caption", "Chế độ cũ")]
        [Size(200)]
        public string CheDoCu
        {
            get
            {
                return _CheDoCu;
            }
            set
            {
                SetPropertyValue("CheDoCu", ref _CheDoCu, value);
            }
        }

        [ModelDefault("Caption", "Chế độ mới")]
        [Size(200)]
        public string CheDoMoi
        {
            get
            {
                return _CheDoMoi;
            }
            set
            {
                SetPropertyValue("CheDoMoi", ref _CheDoMoi, value);
            }
        }


        public QuyetDinhCacVanDeNhanSu(Session session) : base(session) { }

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
            ChucVuCu = ThongTinNhanVien.ChucVu;
            ChucDanhCu = ThongTinNhanVien.ChucDanh;
            LuongCoBanCu = ThongTinNhanVien.NhanVienThongTinLuong.LuongCoBan;
            LuongKinhDoanhCu = ThongTinNhanVien.NhanVienThongTinLuong.LuongKinhDoanh;           
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                JobUpdated = true;
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {              
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
