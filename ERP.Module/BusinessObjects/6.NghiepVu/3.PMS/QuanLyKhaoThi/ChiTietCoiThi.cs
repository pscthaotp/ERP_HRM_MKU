using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using System;

namespace ERP.Module.NghiepVu.PMS.QuanLyKhaoThi
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Chi tiết chấm bài")]

    public class ChiTietCoiThi : BaseObject
    {
        private QuanLyKhaoThi _QuanLyKhaoThi;
        private string _LopHocPhan;
        private string _LopSinhVien;
        private string _MonHoc;
        private DateTime _NgayThi;
        private string _GioThi;
        private decimal _ThoiGianThi;
        private string _PhongThi;
        private NhanVien _NhanVien;
        private string _VaiTroCoiThi;
        private decimal _HeSo;
        private decimal _HeSoHinhThucThi;
        private string _NamHoc;
        private string _HocKy;
        private decimal _GioQuyDoi;       


        [ModelDefault("Caption", "Quản lý khảo thí")]
        [Association("QuanLyKhaoThi-ListChiTietCoiThi")]
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

        [ModelDefault("Caption", "Ngày thi")]
        public DateTime NgayThi
        {
            get { return _NgayThi; }
            set
            {
                SetPropertyValue("NgayThi", ref _NgayThi, value);
            }
        }

        [ModelDefault("Caption", "Giờ thi")]
        public string GioThi
        {
            get { return _GioThi; }
            set
            {
                SetPropertyValue("GioThi", ref _GioThi, value);
            }
        }

        [ModelDefault("Caption", "Thời gian thi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ThoiGianThi
        {
            get { return _ThoiGianThi; }
            set
            {
                SetPropertyValue("ThoiGianThi", ref _ThoiGianThi, value);
            }
        }

        [ModelDefault("Caption", "Phòng thi")]
        public string PhongThi
        {
            get { return _PhongThi; }
            set
            {
                SetPropertyValue("PhongThi", ref _PhongThi, value);
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

        [ModelDefault("Caption", "Vai trò coi thi")]
        public string VaiTroCoiThi
        {
            get { return _VaiTroCoiThi; }
            set
            {
                SetPropertyValue("VaiTroCoiThi", ref _VaiTroCoiThi, value);
            }
        }


        [ModelDefault("Caption", "Hế số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo
        {
            get { return _HeSo; }
            set
            {
                SetPropertyValue("HeSo", ref _HeSo, value);
            }
        }

        [ModelDefault("Caption", "Hế số hình thức thi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoHinhThucThi
        {
            get { return _HeSoHinhThucThi; }
            set
            {
                SetPropertyValue("HeSoHinhThucThi", ref _HeSoHinhThucThi, value);
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

        public ChiTietCoiThi(Session session)
            : base(session)
        {
        }


    }
}