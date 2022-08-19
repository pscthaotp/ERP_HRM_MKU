using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.ExpressApp.Model;
using ERP.Module.PMS.DanhMuc;
using DevExpress.Persistent.Base;

namespace ERP.Module.PMS
{
    [ModelDefault("Caption", "Thông tin khối lượng giảng dạy")]
    public class ThongTinKhoiLuongGiangDay : ThongTinChungNhanVien
    {

        #region KB Môn học
        private string _MaMonHoc;
        private string _TenMonHoc;

        private string _MaLopHocPhan;
        private string _LopHocPhan;

        private string _MaLopSV; 
        private string _TenLopSV;

        private string _MaKhoaQLMonHoc;
        private string _TenKhoaQLMonHoc;

        private string _LopHPGhep;

        private LoaiHocPhan _LoaiHocPhan;
        private BacDaoTao _BacDaoTao;
        private HeDaoTao_PMS _HeDaoTao;
        private NhomMon _NhomMon; 
        private NganhHoc _NganhHoc;
        private KhoiNganh _KhoiNganh; 
        //private NhomNganh _NhomNganh; Hiện tại không sử dụng _ ThuHuong sửa 
        
        private int _SoLuongSV;
        private decimal _SoTinChi;
        private decimal _SoTietThucDay;
        private DayOfWeek _Thu;
        private int _TietBD;
        private int _TietKT; 
        
        private CoSoGiangDay _CoSoGiangDay;
        //Dịa diểm giảng dạy dùng để tính Đơn giá khoảng cách 
        private TinhThanh _DiaDiemGiangDay; 

        private NgonNguGiangDay _NgonNguGiangDay;
        private bool _LopNgoaiGio;
        private bool _GiangDayTheoDuAn;
        private bool _LopCLC;
        private string _TenPhong;

        private DateTime _NgayDay;
        
        #endregion

        #region KB Hệ số
        private decimal _HeSo_LyThuyet_ThucHanh;
        private decimal _HeSo_GiangDayNgoaiGio;
        private decimal _HeSo_LopDong;
        private decimal _HeSo_KhoiNganh;
        private decimal _HeSo_NgonNgu; 
        private decimal _HeSo_BacDaoTao;
        private decimal _HeSo_CoVanHocTap;
        private decimal _HeSo_GiangDayDuAn;
        #endregion

        #region Quy đổi
        private decimal _TongHeSo;
        private decimal _GioQuyDoi;
        private decimal _TongGioQuyDoi;
        #endregion

        private string _GhiChu;
        private bool _TinhThanhToan2;


        private bool _IsKhoan;
        private decimal _SoTienKhoan;
        #region Môn học
        [ModelDefault("Caption", "Mã môn học")]
        [Size(500)]
        public string MaMonHoc
        {
            get { return _MaMonHoc; }
            set { SetPropertyValue("MaMonHoc", ref _MaMonHoc, value); }
        }
        [ModelDefault("Caption", "Tên môn học")]
        [Size(500)]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }

        [ModelDefault("Caption", "Mã học phần")]
        [Size(500)]
        public string MaLopHocPhan
        {
            get { return _MaLopHocPhan; }
            set { SetPropertyValue("MaLopHocPhan", ref _MaLopHocPhan, value); }
        }
        [ModelDefault("Caption", "Tên lớp học phần")]
        [Size(500)]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }

        [ModelDefault("Caption", "Mã lớp SV")]
        [Size(500)]
        public string MaLopSV
        {
            get { return _MaLopSV; }
            set { SetPropertyValue("MaLopSV", ref _MaLopSV, value); }
        }
        [ModelDefault("Caption", "Mã khoa Qly Môn học")]
        [Size(500)]
        public string MaKhoaQLMonHoc
        {
            get { return _MaKhoaQLMonHoc; }
            set { SetPropertyValue("MaKhoaQLMonHoc", ref _MaKhoaQLMonHoc, value); }
        }

        [ModelDefault("Caption", "Tên khoa Qly Môn học")]
        [Size(500)]
        public string TenKhoaQLMonHoc
        {
            get { return _TenKhoaQLMonHoc; }
            set { SetPropertyValue("TenKhoaQLMonHoc", ref _TenKhoaQLMonHoc, value); }
        }
        [ModelDefault("Caption", "Tên lớp SV")]
        [Size(-1)]
        public string TenLopSV
        {
            get { return _TenLopSV; }
            set { SetPropertyValue("TenLopSV", ref _TenLopSV, value); }
        }

        [ModelDefault("Caption", "Lớp HP Ghép")]
        [Size(500)]
        public string LopHPGhep
        {
            get { return _LopHPGhep; }
            set { SetPropertyValue("LopHPGhep", ref _LopHPGhep, value); }
        }

        [ModelDefault("Caption", "Loại học phần")]
        public LoaiHocPhan LoaiHocPhan
        {
            get { return _LoaiHocPhan; }
            set { SetPropertyValue("LoaiHocPhan", ref _LoaiHocPhan, value); }
        }
        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }
        [ModelDefault("Caption", "Hệ đào tạo")]
        public HeDaoTao_PMS HeDaoTao
        {
            get { return _HeDaoTao; }
            set { SetPropertyValue("HeDaoTao", ref _HeDaoTao, value); }
        }
        [ModelDefault("Caption", "Nhóm môn")]
        public NhomMon NhomMon
        {
            get { return _NhomMon; }
            set { SetPropertyValue("NhomMon", ref _NhomMon, value); }
        }
        [ModelDefault("Caption", "Ngành học")]
        public NganhHoc NganhHoc
        {
            get { return _NganhHoc; }
            set { SetPropertyValue("NganhHoc", ref _NganhHoc, value); }
        }
        [ModelDefault("Caption", "Khối ngành")]
        public KhoiNganh KhoiNganh
        {
            get { return _KhoiNganh; }
            set { SetPropertyValue("KhoiNganh", ref _KhoiNganh, value); }
        }
        //[ModelDefault("Caption", "Nhóm ngành")]
        //public NhomNganh NhomNganh
        //{
        //    get { return _NhomNganh; }
        //    set { SetPropertyValue("NhomNganh", ref _NhomNganh, value); }
        //}
        [ModelDefault("Caption", "Số lượng SV")]
        public int SoLuongSV
        {
            get { return _SoLuongSV; }
            set { SetPropertyValue("SoLuongSV", ref _SoLuongSV, value); }
        }
        [ModelDefault("Caption", "Số tín chỉ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTinChi
        {
            get { return _SoTinChi; }
            set { SetPropertyValue("SoTinChi", ref _SoTinChi, value); }
        }
        [ModelDefault("Caption", "Số tiết thực dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietThucDay
        {
            get { return _SoTietThucDay; }
            set { SetPropertyValue("SoTietThucDay", ref _SoTietThucDay, value); }
        }
        [ModelDefault("Caption", "Thứ (giảng dạy)")]
        public DayOfWeek Thu
        {
            get { return _Thu; }
            set { SetPropertyValue("Thu", ref _Thu, value); }
        }
        [ModelDefault("Caption", "Tiết bắt đầu")]
        public int TietBD
        {
            get { return _TietBD; }
            set { SetPropertyValue("TietBD", ref _TietBD, value); }
        }
        [ModelDefault("Caption", "Tiết kết thúc")]
        public int TietKT
        {
            get { return _TietKT; }
            set { SetPropertyValue("TietKT", ref _TietKT, value); }
        }
       
        [ModelDefault("Caption", "Cơ sở giảng dạy")]
        public CoSoGiangDay CoSoGiangDay
        {
            get { return _CoSoGiangDay; }
            set { SetPropertyValue("CoSoGiangDay", ref _CoSoGiangDay, value); }
        }

        [ModelDefault("Caption", "Địa điểm giảng dạy")]
        public TinhThanh DiaDiemGiangDay
        {
            get { return _DiaDiemGiangDay; }
            set { SetPropertyValue("DiaDiemGiangDay", ref _DiaDiemGiangDay, value); }
        }

        [ModelDefault("Caption", "Ngôn ngữ giảng dạy")]
        public NgonNguGiangDay NgonNguGiangDay
        {
            get { return _NgonNguGiangDay; }
            set { SetPropertyValue("NgonNguGiangDay", ref _NgonNguGiangDay, value); }
        }

        [ModelDefault("Caption", "Lớp Ngoài Giờ")]
        public bool LopNgoaiGio
        {
            get { return _LopNgoaiGio; }
            set { SetPropertyValue("LopNgoaiGio", ref _LopNgoaiGio, value); }
        }

        [ModelDefault("Caption", "Lớp CLC")]
        public bool LopCLC
        {
            get { return _LopCLC; }
            set { SetPropertyValue("LopCLC", ref _LopCLC, value); }
        }

        [ModelDefault("Caption", "Phòng học")]
        public string TenPhong
        {
            get { return _TenPhong; }
            set { SetPropertyValue("TenPhong", ref _TenPhong, value); }
        }



        [ModelDefault("Caption", "Giảng dạy theo dự án")]
        public bool GiangDayTheoDuAn
        {
            get { return _GiangDayTheoDuAn; }
            set { SetPropertyValue("GiangDayTheoDuAn", ref _GiangDayTheoDuAn, value); }
        }

        [ModelDefault("Caption", "Ngày dạy")]
        public DateTime NgayDay
        {
            get { return _NgayDay; }
            set { SetPropertyValue("NgayDay", ref _NgayDay, value); }
        }
        #endregion

        #region Hệ số
        [ModelDefault("Caption", "HS LT/TH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_LyThuyet_ThucHanh
        {
            get { return _HeSo_LyThuyet_ThucHanh; }
            set { SetPropertyValue("HeSo_LyThuyet_ThucHanh", ref _HeSo_LyThuyet_ThucHanh, value); }
        }
        [ModelDefault("Caption", "HS giảng dạy ngoài giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_GiangDayNgoaiGio
        {
            get { return _HeSo_GiangDayNgoaiGio; }
            set { SetPropertyValue("HeSo_GiangDayNgoaiGio", ref _HeSo_GiangDayNgoaiGio, value); }
        }
        [ModelDefault("Caption", "HS lớp đông")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_LopDong
        {
            get { return _HeSo_LopDong; }
            set { SetPropertyValue("HeSo_LopDong", ref _HeSo_LopDong, value); }
        }
        [ModelDefault("Caption", "HS khối ngành")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_KhoiNganh
        {
            get { return _HeSo_KhoiNganh; }
            set { SetPropertyValue("HeSo_KhoiNganh", ref _HeSo_KhoiNganh, value); }
        }
        [ModelDefault("Caption", "HS ngôn ngữ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_NgonNgu
        {
            get { return _HeSo_NgonNgu; }
            set { SetPropertyValue("HeSo_NgonNgu", ref _HeSo_NgonNgu, value); }
        }
        [ModelDefault("Caption", "HS bậc đào tạo")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_BacDaoTao
        {
            get { return _HeSo_BacDaoTao; }
            set { SetPropertyValue("HeSo_BacDaoTao", ref _HeSo_BacDaoTao, value); }
        }
        [ModelDefault("Caption", "HS CVHT")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_CoVanHocTap
        {
            get { return _HeSo_CoVanHocTap; }
            set { SetPropertyValue("HeSo_CoVanHocTap", ref _HeSo_CoVanHocTap, value); }
        }
        [ModelDefault("Caption", "HS giảng dạy dự án")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_GiangDayDuAn
        {
            get { return _HeSo_GiangDayDuAn; }
            set { SetPropertyValue("HeSo_GiangDayDuAn", ref _HeSo_GiangDayDuAn, value); }
        }
        #endregion

        #region QuyDoi
        [ModelDefault("Caption", "Tổng hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongHeSo
        {
            get { return _TongHeSo; }
            set { SetPropertyValue("TongHeSo", ref _TongHeSo, value); }
        }

        [ModelDefault("Caption", "Giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioQuyDoi
        {
            get { return _GioQuyDoi; }
            set { SetPropertyValue("GioQuyDoi", ref _GioQuyDoi, value); }
        }

        [ModelDefault("Caption", "Tổng giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioQuyDoi
        {
            get { return _TongGioQuyDoi; }
            set { SetPropertyValue("TongGioQuyDoi", ref _TongGioQuyDoi, value); }
        }
        #endregion

        [ModelDefault("Caption", "Ghi chú")]
        [Size(1000)]
        [VisibleInListView(false)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }                  

        [ModelDefault("Caption", "Tính thù lao")]
        public bool TinhThanhToan2
        {
            get { return _TinhThanhToan2; }
            set { SetPropertyValue("TinhThanhToan2", ref _TinhThanhToan2, value); }
        }

        [ModelDefault("Caption", "Khoán")]
        public bool IsKhoan
        {
            get { return _IsKhoan; }
            set { SetPropertyValue("IsKhoan", ref _IsKhoan, value); }
        }

        [ModelDefault("Caption", "Số tiền khoán")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTienKhoan
        {
            get { return _SoTienKhoan; }
            set { SetPropertyValue("SoTienKhoan", ref _SoTienKhoan, value); }
        }
        public ThongTinKhoiLuongGiangDay(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}