using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.Data.Filtering;
using ERP.Module.Enum.PMS;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.PMS.HeSo;
using ERP.Module.NghiepVu.PMS.DanhMuc;
using System;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace ERP.Module.NghiepVu.PMS.BangChotThuLao
{

    [DefaultClassOptions]
    [ModelDefault("Caption", "Chi tiết bảng chốt thù lao")]
    public class ChiTietBangChotThuLao : BaseObject
    {
        private ThongTinBangChotThuLao _ThongTinBangChotThuLao;
        private NguonThuLaoEnum _NguonThuLaoEnum;
        private LoaiHocPhan _LoaiHocPhan;
        private string _MaHocPhan;
        private string _TenHocPhan;
        private string _LopHocPhan;
        private decimal _SoTietDay;
        private decimal _HeSo_NgonNgu;
        private decimal _HeSo_GiangVien;
        private decimal _HeSo_LopDong;
        private decimal _HeSo_MonHoc;
        private decimal _HeSo_ThoiGian;
        private decimal _TongHeSo;
        private decimal _GioQuyDoi;
        private decimal _DonGia;
        private decimal _ThanhTien;
        private Guid _OidChiTiet;
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;

        [Association("ThongTinBangChotThuLao-ListChiTietBangChotThuLao")]
        [Browsable(false)]
        [ModelDefault("Caption", "Thông tin bảng chốt thù lao")]
        public ThongTinBangChotThuLao ThongTinBangChotThuLao
        {
            get
            {
                return _ThongTinBangChotThuLao;
            }
            set
            {
                SetPropertyValue("ThongTinBangChotThuLao", ref _ThongTinBangChotThuLao, value);
            }
        }

        [ModelDefault("Caption", "Nguồn thù lao")]
        [ImmediatePostData]
        public NguonThuLaoEnum NguonThuLaoEnum
        {
            get { return _NguonThuLaoEnum; }
            set { SetPropertyValue("NguonThuLaoEnum", ref _NguonThuLaoEnum, value); }
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

        [ModelDefault("Caption", "Số tiết thực dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietDay
        {
            get { return _SoTietDay; }
            set { SetPropertyValue("SoTietDay", ref _SoTietDay, value); }
        }

        [ModelDefault("Caption", "Hệ số ngôn ngữ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_NgonNgu
        {
            get { return _HeSo_NgonNgu; }
            set { SetPropertyValue("HeSo_NgonNgu", ref _HeSo_NgonNgu, value); }
        }

        [ModelDefault("Caption", "Hệ số giảng viên")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_GiangVien
        {
            get { return _HeSo_GiangVien; }
            set { SetPropertyValue("HeSo_GiangVien", ref _HeSo_GiangVien, value); }
        }

        [ModelDefault("Caption", "Hệ số lớp đông")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_LopDong
        {
            get { return _HeSo_LopDong; }
            set { SetPropertyValue("HeSo_LopDong", ref _HeSo_LopDong, value); }
        }

        [ModelDefault("Caption", "Hệ số môn học")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_MonHoc
        {
            get { return _HeSo_MonHoc; }
            set { SetPropertyValue("HeSo_MonHoc", ref _HeSo_MonHoc, value); }
        }

        [ModelDefault("Caption", "Hệ số thời gian")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_ThoiGian
        {
            get { return _HeSo_ThoiGian; }
            set { SetPropertyValue("HeSo_ThoiGian", ref _HeSo_ThoiGian, value); }
        }

        [ModelDefault("Caption", "Hệ số lương")]
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

        [ModelDefault("Caption", "Đơn giá")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DonGia
        {
            get { return _DonGia; }
            set { SetPropertyValue("DonGia", ref _DonGia, value); }
        }

        [ModelDefault("Caption", "Thành tiền")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ThanhTien
        {
            get { return _ThanhTien; }
            set { SetPropertyValue("ThanhTien", ref _ThanhTien, value); }
        }

        [ModelDefault("Caption", "Oid chi tiết")]
        [Browsable(false)]
        public Guid OidChiTiet
        {
            get { return _OidChiTiet; }
            set { SetPropertyValue("OidChiTiet", ref _OidChiTiet, value); }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }

        [ModelDefault("Caption", "Heej đào tạo")]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set { SetPropertyValue("HeDaoTao", ref _HeDaoTao, value); }
        }

        public ChiTietBangChotThuLao(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

    }
}
