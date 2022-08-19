using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.TuyenDung
{
   public class Non_UngVien : Non_TuyenDung
    {
        //HoSo
        [DisplayName("Họ tên viết thường")]
        public string HoTenVietThuong { get; set; }

        [DisplayName("Họ tên viết hoa")]
        public string HoTenVietHoa { get; set; }

        [DisplayName("Danh xưng ứng viên viết thường")]
        public string DanhXungVietThuong { get; set; }

        [DisplayName("Danh xưng ứng viên viết hoa")]
        public string DanhXungVietHoa { get; set; }

        [DisplayName("Ngày sinh")]
        public string NgaySinh { get; set; }

        [DisplayName("Ngày sinh (Date)")]
        public string NgaySinhDate { get; set; }

        [DisplayName("Nơi sinh")]
        public string NoiSinh { get; set; }

        [DisplayName("Địa chỉ thường trú")]
        public string DiaChiThuongTru { get; set; }

        [DisplayName("Số điện thoại")]
        public string DienThoaiDiDong { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        //UngVien
        [DisplayName("Số báo danh")]
        public string SoBaoDanh { get; set; }

        [DisplayName("Ngày dự tuyển")]
        public string NgayDuTuyen { get; set; }

        [DisplayName("Ngày dự tuyển (Date)")]
        public string NgayDuTuyenDate { get; set; }

        [DisplayName("Ngày trúng tuyển")]
        public string NgayTrungTuyen { get; set; }

        [DisplayName("Ngày trúng tuyển (Date)")]
        public string NgayTrungTuyenDate { get; set; }

        [DisplayName("Ngày thông báo không trúng tuyển")]
        public string NgayThongBaoKhongTrungTuyen { get; set; }

        [DisplayName("Ngày thông báo không trúng tuyển (Date)")]
        public string NgayThongBaoKhongTrungTuyenDate { get; set; }

        [DisplayName("Vị trí trúng tuyển")]
        public string ViTriTrungTuyen { get; set; }

        [DisplayName("Ngày nhận việc")]
        public string NgayNhanViec { get; set; }

        [DisplayName("Ngày nhận việc (Date)")]
        public string NgayNhanViecDate { get; set; }

        //ThongTinLuong
        [DisplayName("Mã ngạch lương")]
        public string MaNgachLuong { get; set; }

        [DisplayName("Tên ngạch lương")]
        public string TenNgachLuong { get; set; }

        [DisplayName("Bậc lương")]
        public string BacLuong { get; set; }

        [DisplayName("Lương gộp")]
        public string LuongGop { get; set; }

        [DisplayName("Lương chức danh")]
        public string LuongCoBan { get; set; }

        [DisplayName("Lương bổ sung(HQCV)")]
        public string LuongKinhDoanh { get; set; }

        [DisplayName("Pc trách nhiệm ")]
        public string PCTrachNhiem { get; set; }

        [DisplayName("Pc kiêm nhiệm")]
        public string PCKiemNhiem { get; set; }

        [DisplayName("Mốc nâng lương")]
        public string MocNangLuong { get; set; }

        [DisplayName("Ngày hưởng lương")]
        public string NgayHuongLuong { get; set; }

        [DisplayName("Ngày hưởng lương")]
        public string NgayHuongLuongDate { get; set; }

        //NhanVienTrinhDo
        [DisplayName("Trình độ chuyên môn")]
        public string TrinhDoChuyenMon { get; set; }

        [DisplayName("Chuyên ngành đào tạo")]
        public string ChuyenNganhDaoTao { get; set; }

        [DisplayName("Trường đào tạo")]
        public string TruongDaoTao { get; set; }

        [DisplayName("Năm tốt nghiệp")]
        public string NamTotNghiep { get; set; }
    }
}
