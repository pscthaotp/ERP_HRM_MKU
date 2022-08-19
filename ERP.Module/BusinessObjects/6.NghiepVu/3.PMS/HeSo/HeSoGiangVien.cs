using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.Data.Filtering;
using ERP.Module.Enum.PMS;
using ERP.Module.NghiepVu.PMS.QuanLy;
using ERP.Module.NghiepVu.NhanSu.NhanViens;

namespace ERP.Module.NghiepVu.PMS.HeSo
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Hệ số giảng viên")]
    public class HeSoGiangVien : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        private NhanVien _NhanVien;
        private ChucDanh _ChucDanh;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private HocHam _HocHam;
        private int _SoThamNien;
        private int _DenThamNien;
        private LoaiGiangVienEnum? _LoaiGiangVien;
        private decimal _HeSo;

        [Association("QuanLyHeSo-ListHeSoGiangVien")]
        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý hệ số")]
        public QuanLyHeSo QuanLyHeSo
        {
            get
            {
                return _QuanLyHeSo;
            }
            set
            {
                SetPropertyValue("QuanLyHeSo", ref _QuanLyHeSo, value);
            }
        }

        [ModelDefault("Caption", "Giảng viên")]
        [ModelDefault("AllowEdit", "false")]
        [Browsable(false)]
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

        [ModelDefault("Caption", "Chức danh")]
        //[ModelDefault("AllowEdit", "false")]
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


        [ModelDefault("Caption", "Học vị")]
        //[ModelDefault("AllowEdit", "false")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get
            {
                return _TrinhDoChuyenMon;
            }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value);
            }
        }

        [ModelDefault("Caption", "Học hàm")]
        //[ModelDefault("AllowEdit", "false")]
        public HocHam HocHam
        {
            get
            {
                return _HocHam;
            }
            set
            {
                SetPropertyValue("HocHam", ref _HocHam, value);
            }
        }

        [ModelDefault("Caption", "Từ thâm niên")]
        //[ModelDefault("AllowEdit", "false")]
        public int ThamNien
        {
            get
            {
                return _SoThamNien;
            }
            set
            {
                SetPropertyValue("SoThamNien", ref _SoThamNien, value);
            }
        }

        [ModelDefault("Caption", "Đến thâm niên")]
        //[ModelDefault("AllowEdit", "false")]
        public int DenThamNien
        {
            get
            {
                return _DenThamNien;
            }
            set
            {
                SetPropertyValue("DenThamNien", ref _DenThamNien, value);
            }
        }


        [ModelDefault("Caption", "Loại giảng viên")]
        //[ModelDefault("AllowEdit", "false")]
        public LoaiGiangVienEnum? LoaiGiangVien
        {
            get { return _LoaiGiangVien; }
            set
            {
                SetPropertyValue("LoaiGiangVien", ref _LoaiGiangVien, value);
            }
        }


        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo
        {
            get { return _HeSo; }
            set { SetPropertyValue("HeSo", ref _HeSo, value); }
        }

        public HeSoGiangVien(Session session)
            : base(session)
        {
        }


    }
}