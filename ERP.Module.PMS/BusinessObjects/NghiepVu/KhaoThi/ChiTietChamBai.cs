using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.PMS.DanhMuc;
using ERP.Module.PMS.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Chi tiết chấm bài")]
    public class ChiTietChamBai : BaseObject
    {
        #region key
        private QuanLyKhaoThi _QuanLyKhaoThi;
        [Association("QuanLyKhaoThi-ListChiTietChamBai")]
        [ModelDefault("Caption", "Quản lý khảo thí - chấm bài")]
        [Browsable(false)]
        public QuanLyKhaoThi QuanLyKhaoThi
        {
            get
            {
                return _QuanLyKhaoThi;
            }
            set
            {
                SetPropertyValue("QuanLyKhaoThi", ref _QuanLyKhaoThi, value);
            }
        }
        #endregion
        private HocKy _HocKy;
        private NhanVien _NhanVien;
        private BoPhan _BoPhan;
        private LoaiGiangVienEnum? _LoaiGiangVien;
        //
        private LoaiHocPhan _LoaiHocPhan;
        private BacDaoTao _BacDaoTao;
        private HeDaoTao_PMS _HeDaoTao;
        //
        private decimal _SoTinChi;
        private string _MaHocPhan;
        private string _TenHocPhan;
        private string _LopHocPhan;
        private string _TenLopSV;
        //
        private string _KyThi;
        private int _LanThi;
        private string _DotThi;
        private string _MaHinhThucThi;
        private string _TenHinhThucThi;
        //
        private string _KhoaNganh;
        private string _Nganh;
        private int _SiSo;
        //
        private DateTime _NgayThi;
        private string _GioThi;
        private string _PhongThi;
        private decimal _ThoiGianThi;
        private decimal _SoBaiLan1;
        private decimal _SoBaiLan2;

        private string _GhiChu;
        private bool _TinhThuLao;
        // 
        private decimal _HeSo;
        private decimal _GioQuyDoi;

        //
        [ModelDefault("Caption", "Học kỳ")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }

        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Bộ phận")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }

        [ModelDefault("Caption", "Loại giảng viên")]
        public LoaiGiangVienEnum? LoaiGiangVien
        {
            get { return _LoaiGiangVien; }
            set { SetPropertyValue("LoaiGiangVien", ref _LoaiGiangVien, value); }
        }
        //
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
        //
        [ModelDefault("Caption", "Số tín chỉ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTinChi
        {
            get { return _SoTinChi; }
            set { SetPropertyValue("SoTinChi", ref _SoTinChi, value); }
        }

        [ModelDefault("Caption", "Mã học phần")]
        [Size(-1)]
        public string MaHocPhan
        {
            get { return _MaHocPhan; }
            set { SetPropertyValue("MaHocPhan", ref _MaHocPhan, value); }
        }

        [ModelDefault("Caption", "Tên học phần")]
        [Size(-1)]
        public string TenHocPhan
        {
            get { return _TenHocPhan; }
            set { SetPropertyValue("TenHocPhan", ref _TenHocPhan, value); }
        }

        [ModelDefault("Caption", "Lớp học phần")]
        [Size(-1)]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }
        
        [ModelDefault("Caption", "Tên lớp SV")]
        public string TenLopSV
        {
            get { return _TenLopSV; }
            set { SetPropertyValue("TenLopSV", ref _TenLopSV, value); }
        }
       //
        [ModelDefault("Caption", "Kỳ thi")]
        public string KyThi
        {
            get { return _KyThi; }
            set { SetPropertyValue("KyThi", ref _KyThi, value); }
        }
        
        [ModelDefault("Caption", "Lần thi")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public int LanThi
        {
            get { return _LanThi; }
            set { SetPropertyValue("LanThi", ref _LanThi, value); }
        }

        [ModelDefault("Caption", "Đợt thi")]
        public string DotThi
        {
            get { return _DotThi; }
            set { SetPropertyValue("DotThi", ref _DotThi, value); }
        }

        [ModelDefault("Caption", "Mã hình thức thi")]
        [Size(-1)]
        public string MaHinhThucThi
        {
            get { return _MaHinhThucThi; }
            set { SetPropertyValue("MaHinhThucThi", ref _MaHinhThucThi, value); }
        }
        [ModelDefault("Caption", "Tên hình thức thi")]
        [Size(-1)]
        public string TenHinhThucThi
        {
            get { return _TenHinhThucThi; }
            set { SetPropertyValue("TenHinhThucThi", ref _TenHinhThucThi, value); }
        }
        //
        [ModelDefault("Caption", "Khóa ngành")]
        public string KhoaNganh
        {
            get { return _KhoaNganh; }
            set { SetPropertyValue("KhoaNganh", ref _KhoaNganh, value); }
        }

        [ModelDefault("Caption", "Ngành")]
        public string Nganh
        {
            get { return _Nganh; }
            set { SetPropertyValue("Nganh", ref _Nganh, value); }
        }

        [ModelDefault("Caption", "Sỉ số")]
        public int SiSo
        {
            get { return _SiSo; }
            set { SetPropertyValue("SiSo", ref _SiSo, value); }
        }
        
        [ModelDefault("Caption", "Ngày thi")]
        public DateTime NgayThi
        {
            get { return _NgayThi; }
            set { SetPropertyValue("NgayThi", ref _NgayThi, value); }
        }

        [ModelDefault("Caption", "Giờ thi")]
        public string GioThi
        {
            get { return _GioThi; }
            set { SetPropertyValue("GioThi", ref _GioThi, value); }
        }

        [ModelDefault("Caption", "Phòng thi")]
        public string PhongThi
        {
            get { return _PhongThi; }
            set { SetPropertyValue("PhongThi", ref _PhongThi, value); }
        }

        [ModelDefault("Caption", "Thời gian thi(phút)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ThoiGianThi
        {
            get { return _ThoiGianThi; }
            set { SetPropertyValue("ThoiGianThi", ref _ThoiGianThi, value); }
        }

        [ModelDefault("Caption", "Số bài(Lần 1)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoBaiLan1
        {
            get { return _SoBaiLan1; }
            set { SetPropertyValue("SoBaiLan1", ref _SoBaiLan1, value); }
        }
        [ModelDefault("Caption", "Số bài(Lần 2)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoBaiLan2
        {
            get { return _SoBaiLan2; }
            set { SetPropertyValue("SoBaiLan2", ref _SoBaiLan2, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Tính thù lao")]
        public bool TinhThuLao
        {
            get { return _TinhThuLao; }
            set { SetPropertyValue("TinhThuLao", ref _TinhThuLao, value); }
        }
        //

        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo
        {
            get { return _HeSo; }
            set { SetPropertyValue("HeSo", ref _HeSo, value); }
        }

        [ModelDefault("Caption", "Giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioQuyDoi
        {
            get { return _GioQuyDoi; }
            set { SetPropertyValue("GioQuyDoi", ref _GioQuyDoi, value); }
        }


        public ChiTietChamBai(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction(); 
        }
    }
}
