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
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace ERP.Module.NghiepVu.NhanSu.QuaTrinh
{
    // [ImageName("BO_QuaTrinh")]
    [ModelDefault("Caption", "Quá trình bổ nhiệm chức vụ")]
    [RuleCombinationOfPropertiesIsUnique("QuaTrinhBoNhiem.Identifier", DefaultContexts.Save, "ThongTinNhanVien;SoQuyetDinh")]
    public class QuaTrinhBoNhiem : BaseObject
    {
        private string _SoQuyetDinh;
        private DateTime _NgayQuyetDinh;
        private QuyetDinh _QuyetDinh;
        private string _TuNam;
        private string _DenNam;
        private ThongTinNhanVien _ThongTinNhanVien;
        private ChucVu _ChucVu;
        private ChucDanh _ChucDanh;
        private decimal _PhuCapKiemNhiem;
        private decimal _PhuCapTrachNhiem;
        private DateTime _NgayBoNhiemChucVu;
        private string _GhiChu;

        //
        [Browsable(false)]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("ThongTinNhanVien-ListQuaTrinhBoNhiem")]
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

        [ModelDefault("Caption", "Chức danh")]
        public ChucDanh ChucDanh
        {
            get
            {
                return _ChucDanh;
            }
            set
            {
                SetPropertyValue("ChucDanh", ref _ChucDanh, value);
            }
        }


        [Appearance("PhuCapKiemNhiem", Visibility = ViewItemVisibility.Hide)]
        [ModelDefault("Caption", "PC kiêm nhiệm")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapKiemNhiem
        {
            get
            {
                return _PhuCapKiemNhiem;
            }
            set
            {
                SetPropertyValue("PhuCapKiemNhiem", ref _PhuCapKiemNhiem, value);
            }
        }


        [Appearance("PhuCapTrachNhiem", Visibility = ViewItemVisibility.Hide)]
        [ModelDefault("Caption", "PC trách nhiệm")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTrachNhiem
        {
            get
            {
                return _PhuCapTrachNhiem;
            }
            set
            {
                SetPropertyValue("PhuCapTrachNhiem", ref _PhuCapTrachNhiem, value);
            }
        }

        [ModelDefault("Caption", "Ngày BN chức vụ")]
        public DateTime NgayBoNhiemChucVu
        {
            get
            {
                return _NgayBoNhiemChucVu;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemChucVu", ref _NgayBoNhiemChucVu, value);
            }
        }
       
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public QuaTrinhBoNhiem(Session session) : base(session) { }

    }

}
