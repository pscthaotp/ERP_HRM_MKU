using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.HopDong
{
    public class Non_HopDongKhoan : Non_HopDong
    {
        [DisplayName("Loại hợp đồng khoán")]
        public string LoaiHopDongKhoan { get; set; }
        [DisplayName("Hình thức hợp đồng")]
        public string HinhThucHopDong { get; set; }

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
        [DisplayName("PC tiền xăng")]
        public string PCTienXang { get; set; }
        [DisplayName("Tổng lương")]
        public string TongLuong { get; set; }
        [DisplayName("PC tiền ăn")]
        public string PCTienAn { get; set; }
        [DisplayName("PC tiền BHXH")]
        public string PCTienBHXH { get; set; }
        [DisplayName("PC chức vụ")]
        public string PCChucVu { get; set; }
        [DisplayName("PC học vị")]
        public string PCHocVi { get; set; }
        [DisplayName("PC hiệu quả CV")]
        public string PCHieuQuaCV { get; set; }
        [DisplayName("PC kiêm nhiệm")]
        public string PCKiemNhiem { get; set; }
        [DisplayName("PC điện thoại")]
        public string PCDienThoai { get; set; }

    }
}
