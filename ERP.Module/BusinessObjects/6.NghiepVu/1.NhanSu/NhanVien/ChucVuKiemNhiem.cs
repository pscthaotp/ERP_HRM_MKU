using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;

namespace ERP.Module.NghiepVu.NhanSu.NhanViens
{
    [ImageName("BO_NgoaiNguKhac")]
    [ModelDefault("Caption", "Chức vụ kiêm nhiệm")]
    public class ChucVuKiemNhiem : BaseObject
    {
        private NhanVien _NhanVien;
        private ChucVu _ChucVu;
        private ChucDanh _ChucDanh;
        private DateTime _NgayBoNhiem;
        private DateTime _NgayHetNhiemKy;
        private BoPhan _BoPhan;
        private decimal _PhuCapKiemNhiem;
        private bool _DaMienNhiem;
        private QuyetDinh _QuyetDinh;

        //Lưu vết
        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định")]
        public QuyetDinh QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("NhanVien-ListChucVuKiemNhiem")]
        public NhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }

        [ModelDefault("Caption", "Bộ phận")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucVu ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
                if (!IsLoading && value != null)
                    CapNhatChucDanh();
            }
        }

        [ModelDefault("Caption", "Chức danh")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("CDList")]
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

        [ModelDefault("Caption", "Ngày bổ nhiệm")]
        public DateTime NgayBoNhiem
        {
            get
            {
                return _NgayBoNhiem;
            }
            set
            {
                SetPropertyValue("NgayBoNhiem", ref _NgayBoNhiem, value);
            }
        }

        [ModelDefault("Caption", "Ngày hết hạn nhiệm kỳ")]
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

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Phụ cấp kiêm nhiệm")]
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

        [ModelDefault("Caption", "Đã miễn nhiệm")]
        public bool DaMienNhiem
        {
            get
            {
                return _DaMienNhiem;
            }
            set
            {
                SetPropertyValue("DaMienNhiem", ref _DaMienNhiem, value);
            }
        }

        [Browsable(false)]
        public XPCollection<ChucDanh> CDList { get; set; }

        public ChucVuKiemNhiem(Session session) : base(session) { }

        public void CapNhatChucDanh()
        {
            if (CDList == null)
                CDList = new XPCollection<ChucDanh>(Session);
            //          
            if (ChucVu != null)
                CDList.Filter = CriteriaOperator.Parse("ChucVu.TenChucVu like '%Kiêm nhiệm%'");
        }
    }

}

