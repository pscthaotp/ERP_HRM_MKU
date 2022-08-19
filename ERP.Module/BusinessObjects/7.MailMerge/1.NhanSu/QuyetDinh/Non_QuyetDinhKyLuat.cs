using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhKyLuat : Non_QuyetDinhNhanVien
    {
        [DisplayName("Số biên bản")]
        public string SoBienBan { get; set; }

        [DisplayName("Ngày lập biên bản")]
        public string NgayLapBienBan { get; set; }

        [DisplayName("Hình thức kỷ luật")]
        public string HinhThucKyLuat { get; set; }

        [DisplayName("Từ ngày")]
        public string TuNgay { get; set; }

        [DisplayName("Đến ngày")]
        public string DenNgay { get; set; }

        [DisplayName("Từ ngày (date)")]
        public string TuNgayDate { get; set; }

        [DisplayName("Đến ngày (date)")]
        public string DenNgayDate { get; set; }    
    }
}
