using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.NghiepVu.NhanSu.NhanViens;

namespace ERP.Module.NghiepVu.PMS.QuanLyKhaoThi
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Chi tiết ra đề")]

    public class ChiTietRaDe : BaseObject
    {
        private QuanLyKhaoThi _QuanLyKhaoThi;
        private string _LopHocPhan;
        private string _LopSinhVien;
        private string _MonHoc;                     
        private NhanVien _NhanVien;
        private decimal _HeSoRaDe;
        private decimal _SoDe;
        private string _NamHoc;
        private string _HocKy;
        private decimal _GioQuyDoi;

        [ModelDefault("Caption", "Quản lý khảo thí")]
        [Association("QuanLyKhaoThi-ListChiTietRaDe")]
        [Browsable(false)]
        public QuanLyKhaoThi QuanLyKhaoThi
        {
            get { return _QuanLyKhaoThi; }
            set
            {
                SetPropertyValue("QuanLyKhaoThi", ref _QuanLyKhaoThi, value);
            }
        }


        [ModelDefault("Caption", "Lớp học phần")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set
            {
                SetPropertyValue("LopHocPhan", ref _LopHocPhan, value);
            }
        }

        [ModelDefault("Caption", "Lớp sinh viên")]
        public string LopSinhVien
        {
            get { return _LopSinhVien; }
            set
            {
                SetPropertyValue("LopSinhVien", ref _LopSinhVien, value);
            }
        }

        [ModelDefault("Caption", "Môn học")]
        public string MonHoc
        {
            get { return _MonHoc; }
            set
            {
                SetPropertyValue("MonHoc", ref _MonHoc, value);
            }
        }
       
        [ModelDefault("Caption", "Giảng viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }           

        [ModelDefault("Caption", "Hế số ra đề")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoRaDe
        {
            get { return _HeSoRaDe; }
            set
            {
                SetPropertyValue("HeSoRaDe", ref _HeSoRaDe, value);
            }
        }

        [ModelDefault("Caption", "Số đề")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoDe
        {
            get { return _SoDe; }
            set
            {
                SetPropertyValue("SoDe", ref _SoDe, value);
            }
        }

        [ModelDefault("Caption", "Năm học")]
        public string NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        [ModelDefault("Caption", "Học kỳ")]
        public string HocKy
        {
            get { return _HocKy; }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
            }
        }

        [ModelDefault("Caption", "Giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioQuyDoi
        {
            get { return _GioQuyDoi; }
            set
            {
                SetPropertyValue("GioQuyDoi", ref _GioQuyDoi, value);
            }
        }

        public ChiTietRaDe(Session session)
            : base(session)
        {
        }


    }
}