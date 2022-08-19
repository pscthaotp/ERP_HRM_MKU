using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.Persistent.Validation;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.NghiepVu.NhanSu.QuaTrinh
{
    //[ImageName("BO_QuaTrinh")]
    [ModelDefault("Caption", "Quá trình điều động")]
    [RuleCombinationOfPropertiesIsUnique("QuaTrinhDieuDong.Identifier", DefaultContexts.Save, "ThongTinNhanVien;SoQuyetDinh")]
    public class QuaTrinhDieuDong : BaseObject
    {
        private QuyetDinh _QuyetDinh;
        private string _SoQuyetDinh;
        private DateTime _NgayQuyetDinh;
        private ThongTinNhanVien _ThongTinNhanVien;
        private CongTy _CongTyCu;
        private CongTy _CongTyMoi;
        private BoPhan _DonViMoi;
        private BoPhan _DonViCu;
        private ChucDanh _ChucDanhCu;
        private ChucDanh _ChucDanhMoi;
        private ChucVu _ChucVuCu;
        private ChucVu _ChucVuMoi;
        private DateTime _NgayDieuChuyen;
        private string _LyDo;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("ThongTinNhanVien-ListQuaTrinhDieuDong")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Quyết định")]
        [Browsable(false)]
        public QuyetDinh QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
                if (!IsLoading && value != null)
                {
                    SoQuyetDinh = value.SoQuyetDinh;
                    NgayQuyetDinh = value.NgayQuyetDinh;
                }
            }
        }

        [ModelDefault("Caption", "Số quyết định")]
        public string SoQuyetDinh
        {
            get
            {
                return _SoQuyetDinh;
            }
            set
            {
                SetPropertyValue("SoQuyetDinh", ref _SoQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày quyết định")]
        public DateTime NgayQuyetDinh
        {
            get
            {
                return _NgayQuyetDinh;
            }
            set
            {
                SetPropertyValue("NgayQuyetDinh", ref _NgayQuyetDinh, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Trường cũ")]
        public CongTy CongTyCu
        {
            get
            {
                return _CongTyCu;
            }
            set
            {
                SetPropertyValue("CongTyCu", ref _CongTyCu, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị cũ")]
        public BoPhan DonViCu
        {
            get
            {
                return _DonViCu;
            }
            set
            {
                SetPropertyValue("DonViCu", ref _DonViCu, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Trường mới")]
        public CongTy CongTyMoi
        {
            get
            {
                return _CongTyMoi;
            }
            set
            {
                SetPropertyValue("CongTyMoi", ref _CongTyMoi, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị mới")]
        public BoPhan DonViMoi
        {
            get
            {
                return _DonViMoi;
            }
            set
            {
                SetPropertyValue("DonViMoi", ref _DonViMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày điều chuyển")]
        public DateTime NgayDieuChuyen
        {
            get
            {
                return _NgayDieuChuyen;
            }
            set
            {
                SetPropertyValue("NgayDieuChuyen", ref _NgayDieuChuyen, value);
            }
        }

        [ModelDefault("Caption", "Chức danh cũ")]
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

        [ModelDefault("Caption", "Chức danh mới")]
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

        [ModelDefault("Caption", "Chức vụ cũ")]
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



        [ModelDefault("Caption", "Chức vụ moi")]
        public ChucVu ChucVuMoi
        {
            get
            {
                return _ChucVuMoi;
            }
            set
            {
                SetPropertyValue("ChucVuMoi", ref _ChucVuMoi, value);
            }
        }


        [Size(1000)]
        [ModelDefault("Caption", "Lý do")]
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }

        public QuaTrinhDieuDong(Session session) : base(session) { }
    }

}
