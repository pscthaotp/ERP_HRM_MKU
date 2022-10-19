using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.PMS.DanhMuc;
using ERP.Module.NghiepVu.NhanSu.NhanViens;

namespace ERP.Module.NghiepVu.PMS.QuanLyGiangDay
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Thông tin khối lượng giảng dạy")]
    public class ThongTinKhoiLuongGiangDay : BaseObject
    {
        private QuanLyKhoiLuongGiangDay _QuanLyKhoiLuongGiangDay;
        private QuanLyKhoiLuongGiangDay_ThinhGiang _QuanLyKhoiLuongGiangDay_ThinhGiang;
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;
        private NhanVien _NhanVien;
        private string _MaHocPhan;
        private string _TenHocPhan;
        private string _MaLopSinhVien;
        private string _TenLopSinhVien;
        private string _NamHoc;
        private string _HocKy;
        private int _SoTinChi;
        private LoaiHocPhan _LoaiHocPhan;
        private string _LopHocPhan;
        private int _SiSo;
        private BoPhan _BoPhanQuanLy;
        private NhomMonHoc _NhomMonHoc;
        private decimal _SoTietDay;
        private decimal _GioQuyDoi;
        private string _MaBMQL;
        private string _TenBMQL;

        [Association("QuanLyKhoiLuongGiangDay-ListThongTinKhoiLuongGiangDay")]
        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý khối lượng giảng dạy")]
        public QuanLyKhoiLuongGiangDay QuanLyKhoiLuongGiangDay
        {
            get
            {
                return _QuanLyKhoiLuongGiangDay;
            }
            set
            {
                SetPropertyValue("QuanLyKhoiLuongGiangDay", ref _QuanLyKhoiLuongGiangDay, value);
            }
        }

        [Association("QuanLyKhoiLuongGiangDay_ThinhGiang-ListThongTinKhoiLuongGiangDay")]
        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý khối lượng giảng dạy(Thỉnh giảng)")]
        public QuanLyKhoiLuongGiangDay_ThinhGiang QuanLyKhoiLuongGiangDay_ThinhGiang
        {
            get
            {
                return _QuanLyKhoiLuongGiangDay_ThinhGiang;
            }
            set
            {
                SetPropertyValue("QuanLyKhoiLuongGiangDay_ThinhGiang", ref _QuanLyKhoiLuongGiangDay_ThinhGiang, value);
            }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        [ImmediatePostData]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        [ImmediatePostData]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set { SetPropertyValue("HeDaoTao", ref _HeDaoTao, value); }
        }

        [ModelDefault("Caption", "Nhân viên")]
        [ImmediatePostData]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
            
        [ModelDefault("Caption", "Mã học phần")]
        [Size(-1)]
        [ImmediatePostData]
        public string MaHocPhan
        {
            get { return _MaHocPhan; }
            set { SetPropertyValue("MaHocPhan", ref _MaHocPhan, value); }
        }

        [ModelDefault("Caption", "Tên học phần")]
        [Size(-1)]
        [ImmediatePostData]
        public string TenHocPhan
        {
            get { return _TenHocPhan; }
            set { SetPropertyValue("TenHocPhan", ref _TenHocPhan, value); }
        }

        [ModelDefault("Caption", "Mã lớp sinh viên")]
        [Size(-1)]
        [ImmediatePostData]
        public string MaLopSinhVien
        {
            get { return _MaLopSinhVien; }
            set { SetPropertyValue("MaLopSinhVien", ref _MaLopSinhVien, value); }
        }

        [ModelDefault("Caption", "Tên lớp sinh viên")]
        [Size(-1)]
        [ImmediatePostData]
        public string TenLopSinhVien
        {
            get { return _TenLopSinhVien; }
            set { SetPropertyValue("TenLopSinhVien", ref _TenLopSinhVien, value); }
        }

        [ModelDefault("Caption", "Năm học")]
        [Browsable(false)]
        [ImmediatePostData]
        public string NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }

        [ModelDefault("Caption", "Học kỳ")]
        [Browsable(false)]
        [ImmediatePostData]
        public string HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }

        [ModelDefault("Caption", "Số tín chỉ")]
        [ImmediatePostData]
        public int SoTinChi
        {
            get { return _SoTinChi; }
            set { SetPropertyValue("SoTinChi", ref _SoTinChi, value); }
        }

        [ModelDefault("Caption", "Loại học phần")]
        [ImmediatePostData]
        public LoaiHocPhan LoaiHocPhan
        {
            get { return _LoaiHocPhan; }
            set { SetPropertyValue("LoaiHocPhan", ref _LoaiHocPhan, value); }
        }

        [ModelDefault("Caption", "Lớp học phần")]
        [Size(-1)]
        [ImmediatePostData]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }

        [ModelDefault("Caption", "Sỉ số")]
        [ImmediatePostData]
        public int SiSo
        {
            get { return _SiSo; }
            set { SetPropertyValue("SiSo", ref _SiSo, value); }
        }

        [ModelDefault("Caption", "Bộ phận quản lý")]
        [ImmediatePostData]
        public BoPhan BoPhanQuanLy
        {
            get { return _BoPhanQuanLy; }
            set { SetPropertyValue("BoPhanQuanLy", ref _BoPhanQuanLy, value); }
        }

        [ModelDefault("Caption", "Nhóm môn học")]
        [ImmediatePostData]
        public NhomMonHoc NhomMonHoc
        {
            get { return _NhomMonHoc; }
            set { SetPropertyValue("NhomMonHoc", ref _NhomMonHoc, value); }
        }

        [ModelDefault("Caption", "Số tiết thực dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietDay
        {
            get { return _SoTietDay; }
            set { SetPropertyValue("SoTietDay", ref _SoTietDay, value); }
        }

        [ModelDefault("Caption", "Giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioQuyDoi
        {
            get { return _GioQuyDoi; }
            set { SetPropertyValue("GioQuyDoi", ref _GioQuyDoi, value); }
        }

        [ModelDefault("Caption", "Mã BMQL")]
        [Browsable(false)]
        [ImmediatePostData]
        public string MaBMQL
        {
            get { return _MaBMQL; }
            set { SetPropertyValue("MaBMQL", ref _MaBMQL, value); }
        }

        [ModelDefault("Caption", "tÊN bmql")]
        [Browsable(false)]
        [ImmediatePostData]
        public string TenBMQL
        {
            get { return _TenBMQL; }
            set { SetPropertyValue("TenBMQL", ref _TenBMQL, value); }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết khối lượng")]
        [Association("ThongTinKhoiLuongGiangDay-ListChiTietKhoiLuongGiangDay")]
        public XPCollection<ChiTietKhoiLuongGiangDay> ListChiTietKhoiLuongGiangDay
        {
            get
            {
                return GetCollection<ChiTietKhoiLuongGiangDay>("ListChiTietKhoiLuongGiangDay");
            }
        }
        public ThongTinKhoiLuongGiangDay(Session session)
            : base(session) {}

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

    }
}
