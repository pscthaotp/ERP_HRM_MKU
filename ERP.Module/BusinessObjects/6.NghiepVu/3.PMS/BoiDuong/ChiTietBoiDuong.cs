using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.PMS.DanhMuc;

namespace ERP.Module.NghiepVu.PMS.BoiDuong
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Chi tiết bồi dưỡng")]

    public class ChiTietBoiDuong : BaseObject
    {
        private QuanLyBoiDuong _QuanLyBoiDuong;
        private NhanVien _NhanVien;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;      
        private BoPhan _BoPhan;
        private string _MaQuanLy;
        private string _CongViec;
        private string _ChiTiet;
        private string _DienGiai;
        private DonViTinh _DonViTinh;
        private decimal _GioQuyDoiChuan;
        private string _GhiChu;
        private bool _TinhThuLao;

        [ModelDefault("Caption", "Quản lý bồi dưỡng")]
        [Association("QuanLyBoiDuong-ListChiTietBoiDuong")]
        [Browsable(false)]
        public QuanLyBoiDuong QuanLyBoiDuong
        {
            get { return _QuanLyBoiDuong; }
            set
            {
                SetPropertyValue("QuanLyBoiDuong", ref _QuanLyBoiDuong, value);
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
        
        [ModelDefault("Caption", "Trình độ chuyên môn")]
        [ModelDefault("AllowEdit", "false")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get { return _TrinhDoChuyenMon; }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value);
            }
        }


        [ModelDefault("Caption", "Bộ phận")]
        [ModelDefault("AllowEdit", "false")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }


        [ModelDefault("Caption", "Mã quản lý")]    
        [ModelDefault("AllowEdit", "false")]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }
        
        [ModelDefault("Caption", "Công việc")]
        [Size(-1)]
        [ModelDefault("AllowEdit", "false")]
        public string CongViec
        {
            get { return _CongViec; }
            set
            {
                SetPropertyValue("CongViec", ref _CongViec, value);
            }
        }
        
        [ModelDefault("Caption", "Chi tiết")]
        [Size(-1)]
        [ModelDefault("AllowEdit", "false")]
        public string ChiTiet
        {
            get { return _ChiTiet; }
            set
            {
                SetPropertyValue("ChiTiet", ref _ChiTiet, value);
            }
        }


        [ModelDefault("Caption", "Diễn giải")]
        [Size(-1)]
        [ModelDefault("AllowEdit", "false")]
        public string DienGiai
        {
            get { return _DienGiai; }
            set
            {
                SetPropertyValue("DienGiai", ref _DienGiai, value);
            }
        }


        [ModelDefault("Caption", "Đơn vị tính")]
        [ModelDefault("AllowEdit", "false")]
        public DonViTinh DonViTinh
        {
            get { return _DonViTinh; }
            set
            {
                SetPropertyValue("DonViTinh", ref _DonViTinh, value);
            }
        }
        
        [ModelDefault("Caption", "Quy đổi giờ chuẩn")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioQuyDoiChuan
        {
            get { return _GioQuyDoiChuan; }
            set { SetPropertyValue("GioQuyDoiChuan", ref _GioQuyDoiChuan, value); }
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

        [ModelDefault("Caption", "Tính thù lao")]
        public bool TinhThuLao
        {
            get { return _TinhThuLao; }
            set
            {
                SetPropertyValue("TinhThuLao", ref _TinhThuLao, value);
            }
        }
        
        public ChiTietBoiDuong(Session session)
            : base(session)
        {
        }


    }
}