using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.PMS.DanhMuc;
using ERP.Module.NghiepVu.PMS.CVHT;

namespace ERP.Module.NghiepVu.PMS.NCKH
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Chi tiết NCKH")]

    public class ChiTietCVHT : BaseObject
    {
        private QuanLyCVHT _QuanLyCVHT; //
        private NhanVien _NhanVien;//  
        private string _TenLopPhuTrach;
        private int _SoLuongSV;    
        private string _GhiChu;
        private bool _Import;

        [ModelDefault("Caption", "Quản lý CVHT")]
        [Association("QuanLyCVHT-ListChiTietCVHT")]
        [Browsable(false)]
        public QuanLyCVHT QuanLyCVHT
        {
            get { return _QuanLyCVHT; }
            set
            {
                SetPropertyValue("QuanLyCVHT", ref _QuanLyCVHT, value);
            }
        }


        [ModelDefault("Caption", "Giảng viên")]
        [ModelDefault("AllowEdit", "false")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }

        [ModelDefault("Caption", "Tên lớp phụ trách")]
        [Size(-1)]
        public string TenLopPhuTrach
        {
            get { return _TenLopPhuTrach; }
            set
            {
                SetPropertyValue("TenLopPhuTrach", ref _TenLopPhuTrach, value);
            }
        }

        [ModelDefault("Caption", "Số lượng sinh viên")]
        public int SoLuongSV
        {
            get { return _SoLuongSV; }
            set
            {
                SetPropertyValue("SoLuongSV", ref _SoLuongSV, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        [ModelDefault("Caption", "Import")]
        [Browsable(false)]
        public bool Import
        {
            get { return _Import; }
            set
            {
                SetPropertyValue("Import", ref _Import, value);
            }
        }

        public ChiTietCVHT(Session session)
            : base(session)
        {
        }


    }
}