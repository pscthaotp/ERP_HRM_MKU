using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.HopDong
{
    public class Non_HopDongKhoan : Non_HopDong
    {
        [DisplayName("Loại hợp đồng khoán")]
        public string LoaiHopDongKhoan { get; set; }

        [DisplayName("Lương khoán")]
        public string LuongKhoan { get; set; }

        [DisplayName("Ngày hưởng lương")]
        public string NgayHuongLuong { get; set; }

        [DisplayName("Ngày hưởng lương (date)")]
        public string NgayHuongLuongDate { get; set; }

        [DisplayName("Từ ngày")]
        public string TuNgay { get; set; }

        [DisplayName("Từ ngày (date)")]
        public string TuNgayDate { get; set; }

        [DisplayName("Đến ngày")]
        public string DenNgay { get; set; }

        [DisplayName("Đến ngày (date)")]
        public string DenNgayDate { get; set; }

        [DisplayName("Số tháng")]
        public string SoThang { get; set; }

        [DisplayName("Số ngày")]
        public string SoNgay { get; set; }

    }
}
