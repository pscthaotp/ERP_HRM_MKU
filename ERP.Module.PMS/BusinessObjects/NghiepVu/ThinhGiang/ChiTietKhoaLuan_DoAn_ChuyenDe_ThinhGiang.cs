using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.PMS.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Chi tiết khóa luận - đồ án - chuyên đề (thỉnh giảng)")]
    public class ChiTietKhoaLuan_DoAn_ChuyenDe_ThinhGiang : BaseObject
    {
        #region key
        private KhoiLuongGiangDay_ThinhGiang _KhoiLuongGiangDay_ThinhGiang;
        [Association("KhoiLuongGiangDay_ThinhGiang-ListChiTietKhoaLuan_DoAn_ChuyenDe_ThinhGiang")]
        [ModelDefault("Caption", "Khối lượng giảng dạy thỉnh giảng")]
        [Browsable(false)]
        public KhoiLuongGiangDay_ThinhGiang KhoiLuongGiangDay_ThinhGiang
        {
            get
            {
                return _KhoiLuongGiangDay_ThinhGiang;
            }
            set
            {
                SetPropertyValue("KhoiLuongGiangDay_ThinhGiang", ref _KhoiLuongGiangDay_ThinhGiang, value);
            }
        }
        #endregion
        private NhanVien _NhanVien;
        private BoPhan _BoPhan;

        private BacDaoTao _BacDaoTao;
        private HeDaoTao_PMS _HeDaoTao;
        private LoaiHocPhan _LoaiHocPhan;
        private NhomMon _NhomMon;
        private NganhHoc _NganhHoc;
        private KhoiNganh _KhoiNganh;

        private string _MaLopHocPhan;
        private string _MaHocPhan;
        private string _TenHocPhan;
        private string _MaDALV;
        private string _TenDALV;
        private string _MaLoaiDALV;
        private string _TenLoaiDALV;
        private string _MaVaiTroHuongDan;
        private string _TenVaiTroHuongDan;
        private DateTime _NgayThucHien;
        
        private decimal _SoLuong;
        private int _SoTinChi;
        private decimal _HeSo; 
        private decimal _GioQuyDoi;

        private bool _TinhThuLao;
        private string _GhiChu;

        //
        [ModelDefault("Caption", "Nhân viên")]
        [Size(-1)]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Bộ phận")]
        [Size(-1)]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        //
        //
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
        [ModelDefault("Caption", "Loại học phần")]
        public LoaiHocPhan LoaiHocPhan
        {
            get { return _LoaiHocPhan; }
            set { SetPropertyValue("LoaiHocPhan", ref _LoaiHocPhan, value); }
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
        //
        [ModelDefault("Caption", "Mã lớp học phần")]
        [Size(-1)]
        public string MaLopHocPhan
        {
            get { return _MaLopHocPhan; }
            set { SetPropertyValue("MaLopHocPhan", ref _MaLopHocPhan, value); }
        }
        [ModelDefault("Caption", "Mã học phần")]
        [Size(-1)]
        public string MaHocPhan
        {
            get { return _MaHocPhan; }
            set { SetPropertyValue("MaHocPhan", ref _MaHocPhan, value); }
        }
        [ModelDefault("Caption", "Tên lớp học phần")]
        [Size(-1)]
        public string TenHocPhan
        {
            get { return _TenHocPhan; }
            set { SetPropertyValue("TenHocPhan", ref _TenHocPhan, value); }
        }

        [ModelDefault("Caption", "Mã đồ án luận văn")]
        [Size(-1)]
        public string MaDALV
        {
            get { return _MaDALV; }
            set { SetPropertyValue("MaDALV", ref _MaDALV, value); }
        }
        [ModelDefault("Caption", "Tên đồ án luận văn")]
        [Size(-1)]
        public string TenDALV
        {
            get { return _TenDALV; }
            set { SetPropertyValue("TenDALV", ref _TenDALV, value); }
        }
        [ModelDefault("Caption", "Mã loại DALV")]
        [Size(-1)]
        public string MaLoaiDALV
        {
            get { return _MaLoaiDALV; }
            set { SetPropertyValue("MaLoaiDALV", ref _MaLoaiDALV, value); }
        }
        [ModelDefault("Caption", "Tên loại DALV")]
        [Size(-1)]
        public string TenLoaiDALV
        {
            get { return _TenLoaiDALV; }
            set { SetPropertyValue("TenLoaiDALV", ref _TenLoaiDALV, value); }
        }
        [ModelDefault("Caption", "Mã vai trò HD")]
        [Size(-1)]
        public string MaVaiTroHuongDan
        {
            get { return _MaVaiTroHuongDan; }
            set { SetPropertyValue("MaVaiTroHuongDan", ref _MaVaiTroHuongDan, value); }
        }
        [ModelDefault("Caption", "Tên vai trò HD")]
        [Size(-1)]
        public string TenVaiTroHuongDan
        {
            get { return _TenVaiTroHuongDan; }
            set { SetPropertyValue("TenVaiTroHuongDan", ref _TenVaiTroHuongDan, value); }
        }
        [ModelDefault("Caption", "Ngày thực hiện")]
        public DateTime NgayThucHien
        {
            get { return _NgayThucHien; }
            set { SetPropertyValue("NgayThucHien", ref _NgayThucHien, value); }
        }
        [ModelDefault("Caption", "Số lượng")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoLuong
        {
            get { return _SoLuong; }
            set { SetPropertyValue("SoLuong", ref _SoLuong, value); }
        }
        [ModelDefault("Caption", "Số tín chỉ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public int SoTinChi
        {
            get { return _SoTinChi; }
            set { SetPropertyValue("SoTinChi", ref _SoTinChi, value); }
        }
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
        [ModelDefault("Caption", "Tính thù lao")]      
        public bool TinhThuLao
        {
            get { return _TinhThuLao; }
            set { SetPropertyValue("TinhThuLao", ref _TinhThuLao, value); }
        }
        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        public ChiTietKhoaLuan_DoAn_ChuyenDe_ThinhGiang(Session session) : base(session) { }
      
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

    }
}
