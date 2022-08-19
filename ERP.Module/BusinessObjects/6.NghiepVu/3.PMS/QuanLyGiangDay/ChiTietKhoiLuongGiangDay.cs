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

namespace ERP.Module.NghiepVu.PMS.QuanLyGiangDay
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Chi tiết khối lượng giảng dạy")]
    public class ChiTietKhoiLuongGiangDay : BaseObject
    {
        private ThongTinKhoiLuongGiangDay _ThongTinKhoiLuongGiangDay;
        private DayOfWeekEnum _Thu;
        private int _TietBatDau;
        private int _TietKetThuc;
        private NgonNguGiangDay _NgonNguGiangDay;
        private decimal _SoTietDay;
        private decimal _GioQuyChuan;
        private string _MaCoSo;
        private string _TenCoSo;
        private string _NgayDay;
        private decimal _HeSo_NgonNgu;
        private decimal _HeSo_GiangVien;
        private decimal _HeSo_LopDong;
        private decimal _HeSo_MonHoc;
        private decimal _HeSo_ThoiGian;
        private decimal _TongHeSo;
        private decimal _GioQuyDoi;

        [Association("ThongTinKhoiLuongGiangDay-ListChiTietKhoiLuongGiangDay")]
        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý khối lượng giảng dạy")]
        public ThongTinKhoiLuongGiangDay ThongTinKhoiLuongGiangDay
        {
            get
            {
                return _ThongTinKhoiLuongGiangDay;
            }
            set
            {
                SetPropertyValue("ThongTinKhoiLuongGiangDay", ref _ThongTinKhoiLuongGiangDay, value);
            }
        }

        [ModelDefault("Caption", "Thứ")]
        public DayOfWeekEnum Thu
        {
            get { return _Thu; }
            set { SetPropertyValue("Thu", ref _Thu, value); }
        }

        [ModelDefault("Caption", "Tiết bắt đầu")]
        public int TietBatDau
        {
            get { return _TietBatDau; }
            set { SetPropertyValue("TietBatDau", ref _TietBatDau, value); }
        }

        [ModelDefault("Caption", "Tiết kết thúc")]
        public int TietKetThuc
        {
            get { return _TietKetThuc; }
            set { SetPropertyValue("TietKetThuc", ref _TietKetThuc, value); }
        }

        [ModelDefault("Caption", "Ngôn ngữ giảng dạy")]
        public NgonNguGiangDay NgonNguGiangDay
        {
            get { return _NgonNguGiangDay; }
            set { SetPropertyValue("NgonNguGiangDay", ref _NgonNguGiangDay, value); }
        }

        [ModelDefault("Caption", "Số tiết thực dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietDay
        {
            get { return _SoTietDay; }
            set { SetPropertyValue("SoTietDay", ref _SoTietDay, value); }
        }


        [ModelDefault("Caption", "Giờ quy chuẩn")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioQuyChuan
        {
            get { return _GioQuyChuan; }
            set { SetPropertyValue("GioQuyChuan", ref _GioQuyChuan, value); }
        }

        [ModelDefault("Caption", "Mã cơ sở")]
        [Size(-1)]
        [ImmediatePostData]
        public string MaCoSo
        {
            get { return _MaCoSo; }
            set { SetPropertyValue("MaCoSo", ref _MaCoSo, value); }
        }

        [ModelDefault("Caption", "Tên cơ sở")]
        [Size(-1)]
        [ImmediatePostData]
        public string TenCoSo
        {
            get { return _TenCoSo; }
            set { SetPropertyValue("TenCoSo", ref _TenCoSo, value); }
        }

        [ModelDefault("Caption", "Ngày dạy")]
        [ImmediatePostData]
        public string NgayDay
        {
            get { return _NgayDay; }
            set { SetPropertyValue("NgayDay", ref _NgayDay, value); }
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

        public ChiTietKhoiLuongGiangDay(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

    }
}
