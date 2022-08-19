using DevExpress.Xpo;

namespace ERP.Module.MailMerge.NhanSu.TuyenDung
{
    public class Non_DanhGiaPhongVan : Non_UngVien
    {
        [DisplayName("Trình độ học vấn")]
        public string TrinhDoHocVan { get; set; }

        [DisplayName("Kiến thức chuyên môn")]
        public string KienThucChuyenMon { get; set; }

        [DisplayName("Kinh nghiệm công việc")]
        public string KinhNghiemCongViec { get; set; }

        [DisplayName("Nguồn hồ sơ")]
        public string NguonHoSo { get; set; }

        [DisplayName("IQ")]
        public string IQ { get; set; }

        [DisplayName("EQ")]
        public string EQ { get; set; }

        [DisplayName("Tin học văn phòng")]
        public string TinHocVanPhong { get; set; }

        [DisplayName("Ngoại ngữ")]
        public string NgoaiNgu { get; set; }

        [DisplayName("Chuyên môn")]
        public string ChuyenMon { get; set; }

        [DisplayName("Kỹ năng đọc hiểu")]
        public string KyNangDocHieu { get; set; }
    }
}
