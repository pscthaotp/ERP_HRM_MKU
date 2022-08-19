using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.NghiepVu.NhanSu.QuaTrinh
{
    //[ImageName("BO_QuaTrinh")]
    [ModelDefault("Caption", "Quá trình bổ nhiệm chức vụ kiêm nhiệm")]
    [RuleCombinationOfPropertiesIsUnique("QuaTrinhBoNhiemKiemNhiem.Identifier", DefaultContexts.Save, "ThongTinNhanVien;SoQuyetDinh")]
    public class QuaTrinhBoNhiemKiemNhiem : BaseObject
    {
        private string _SoQuyetDinh;
        private DateTime _NgayQuyetDinh;
        private QuyetDinh _QuyetDinh;
        private string _TuNam;
        private string _DenNam;
        private ThongTinNhanVien _ThongTinNhanVien;
        private ChucVu _ChucVu;
        private decimal _HSPCChucVu;
        private DateTime _NgayHuongChucVu;

        //
        [Browsable(false)]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("ThongTinNhanVien-ListQuaTrinhBoNhiemKiemNhiem")]
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

        [ModelDefault("Caption", "")]
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

        [ModelDefault("Caption", "Từ năm")]
        public string TuNam
        {
            get
            {
                return _TuNam;
            }
            set
            {
                SetPropertyValue("TuNam", ref _TuNam, value);
            }
        }

        [ModelDefault("Caption", "Đến năm")]
        public string DenNam
        {
            get
            {
                return _DenNam;
            }
            set
            {
                SetPropertyValue("DenNam", ref _DenNam, value);
            }
        }

        [ModelDefault("Caption", "Quyết Định")]
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

        [ModelDefault("Caption", "Chức vụ")]
        public ChucVu ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
            }
        }

        [ModelDefault("Caption", "HS chức vụ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChucVu
        {
            get
            {
                return _HSPCChucVu;
            }
            set
            {
                SetPropertyValue("HSPCChucVu", ref _HSPCChucVu, value);
            }
        }

        [ModelDefault("Caption", "Ngày chức vụ")]
        public DateTime NgayHuongChucVu
        {
            get
            {
                return _NgayHuongChucVu;
            }
            set
            {
                SetPropertyValue("NgayHuongHeSo", ref _NgayHuongChucVu, value);
            }
        }

        public QuaTrinhBoNhiemKiemNhiem(Session session) : base(session) { }

    }

}
