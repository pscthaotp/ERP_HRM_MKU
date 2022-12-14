using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using System.ComponentModel;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.DinhBien;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn vị trí để tuyển dụng")]
    public class TuyenDung_ChonViTriTuyenDungItem : BaseObject
    {
        private bool _Chon;
        private string _MaQuanLy;
        private DinhBienChucDanh _DinhBienChucDanh;
        private BoPhan _BoPhan;
        private ChucVu _ChucVu;
        private ChucDanh _ChucDanh;
        private LoaiTuyenDung _LoaiTuyenDung;
        private string _TenViTriTuyenDung;


        [Browsable(false)]
        [ModelDefault("Caption", "Danh sách định biên chức danh")]
        public DinhBienChucDanh DinhBienChucDanh
        {
            get
            {
                return _DinhBienChucDanh;
            }
            set
            {
                SetPropertyValue("DinhBienChucDanh", ref _DinhBienChucDanh, value);
            }
        }

        [ModelDefault("Caption", "Tất cả")]
        [ImmediatePostData]
        public bool Chon
        {
            get
            {
                return _Chon;
            }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }

        [ModelDefault("Caption", "Số thứ tự")]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tên vị trí tuyển dụng")]
        public string TenViTriTuyenDung
        {
            get
            {
                return _TenViTriTuyenDung;
            }
            set
            {
                SetPropertyValue("TenViTriTuyenDung", ref _TenViTriTuyenDung, value);
            }
        }

        [ModelDefault("Caption", "Bộ phận")]
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

        [ModelDefault("Caption", "Loại tuyển dụng")]
        public LoaiTuyenDung LoaiTuyenDung
        {
            get
            {
                return _LoaiTuyenDung;
            }
            set
            {
                SetPropertyValue("LoaiTuyenDung", ref _LoaiTuyenDung, value);
            }
        }

        public TuyenDung_ChonViTriTuyenDungItem(Session session) : base(session) { }
    }
}
