using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.PMS.BusinessObjects;
using ERP.Module.PMS.DanhMuc;

namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Chi tiết thanh tra giảng dạy")]
    [DefaultProperty("ThongTin")]
    public class ChiTietThanhTra : ThongTinKhoiLuongGiangDay
    {
        #region key
        private QuanLyThanhTra _QuanLyThanhTra;
        [Association("QuanLyThanhTra-ListChiTiet")]
        [ModelDefault("Caption", "Thanh tra giảng dạy")]
        [Browsable(false)]
        public QuanLyThanhTra QuanLyThanhTra
        {
            get
            {
                return _QuanLyThanhTra;
            }
            set
            {
                SetPropertyValue("QuanLyThanhTra", ref _QuanLyThanhTra, value);
            }
        }
        #endregion

        #region KB 
        private string _MaLopSV;
        private string _TenLopSV;
        private DayOfWeek _Thu;
        private int _TietBD;
        private int _TietKT; 
        private string _PhongHoc;
        private DateTime _NgayBD;
        private DateTime _NgayKT;

        private BacDaoTao _BacDaoTao;
        private bool _LopChatLuongCao;

        private decimal _SoTietGhiNhan;
        private DateTime _ThoiDiemThanhLy;
        private string _GhiChuThanhTra;
        private bool _DaThanhTra;
        #endregion
        private bool _XacNhanGV;
        private string _GhiChuGV;


        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        [VisibleInDetailView(false)]
        public string ThongTin
        {
            get
            {
                return String.Format("{0}", this.NhanVien != null ? this.NhanVien.MaNhanVien + " - " + this.NhanVien.HoTen + " - " + this.TenMonHoc : "");
            }
        }
        #region
        [ModelDefault("Caption", "Mã lớp SV")]
        public string MaLopSV
        {
            get { return _MaLopSV; }
            set { SetPropertyValue("MaLopSV", ref _MaLopSV, value); }
        }
        [ModelDefault("Caption", "Tên lớp SV")]
        public string TenLopSV
        {
            get { return _TenLopSV; }
            set { SetPropertyValue("TenLopSV", ref _TenLopSV, value); }
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
        [ModelDefault("Caption", "Phòng học")]
        public string PhongHoc
        {
            get { return _PhongHoc; }
            set { SetPropertyValue("PhongHoc", ref _PhongHoc, value); }
        }

        [ModelDefault("Caption", "Ngày bắt đầu")]
        public DateTime NgayBD
        {
            get { return _NgayBD; }
            set { SetPropertyValue("NgayBD", ref _NgayBD, value); }
        }
        [ModelDefault("Caption", "Ngày kết thúc")]
        public DateTime NgayKT
        {
            get { return _NgayKT; }
            set { SetPropertyValue("NgayKT", ref _NgayKT, value); }
        }
       

        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }
        [ModelDefault("Caption", "Lớp CLC")]
        public bool LopChatLuongCao
        {
            get { return _LopChatLuongCao; }
            set { SetPropertyValue("LopChatLuongCao", ref _LopChatLuongCao, value); }
        }
        #endregion

        #region ThanhTra

        [ModelDefault("Caption", "Số tiết ghi nhận")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietGhiNhan
        {
            get { return _SoTietGhiNhan; }
            set
            {
                SetPropertyValue("SoTietGhiNhan", ref _SoTietGhiNhan, value);
               
            }
        }
        [ModelDefault("Caption", "Thời điểm thanh lý")]
        public DateTime ThoiDiemThanhLy
        {
            get { return _ThoiDiemThanhLy; }
            set
            {
                SetPropertyValue("ThoiDiemThanhLy", ref _ThoiDiemThanhLy, value);
               
            }
        }
        [ModelDefault("Caption", "Ghi chú thanh tra")]
        public string GhiChuThanhTra
        {
            get { return _GhiChuThanhTra; }
            set
            {
                SetPropertyValue("GhiChuThanhTra", ref _GhiChuThanhTra, value);
                
            }
        }
        [ModelDefault("Caption", "Đã thanh tra")]
        [ModelDefault("AllowEdit", "False")]
        public bool DaThanhTra
        {
            get { return _DaThanhTra; }
            set { SetPropertyValue("DaThanhTra", ref _DaThanhTra, value); }
        }
        #endregion
        #region ThanhTra
        [ModelDefault("Caption", "Xác nhận GV")]
        [Browsable(false)]
        [ModelDefault("AllowEdit", "False")]
        public bool XacNhanGV
        {
            get { return _XacNhanGV; }
            set { SetPropertyValue("XacNhanGV", ref _XacNhanGV, value); }
        }

        [ModelDefault("Caption", "Ghi chú GV")]
        [Browsable(false)]
        [ModelDefault("AllowEdit", "False")]
        [Size(-1)]
        public string GhiChuGV
        {
            get { return _GhiChuGV; }
            set
            {
                SetPropertyValue("GhiChuGV", ref _GhiChuGV, value);

            }
        }
        #endregion
        public ChiTietThanhTra(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
