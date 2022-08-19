using DevExpress.Xpo;

namespace ERP.Module.MailMerge.NhanSu.TuyenDung
{
    public class Non_ThuTuyenDung : Non_UngVien
    {
        [DisplayName("Đợt tuyển dụng")]
        public int DotTuyenDung { get; set; }

        [DisplayName("Năm tuyển dụng")]
        public string NamTuyenDung { get; set; }

        [DisplayName("Thời gian thử việc")]
        public string ThoiGianThuViec { get; set; }

        [DisplayName("Phần trăm tính lương")]
        public string PhanTramTinhLuong { get; set; }
    }
}
