using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.HopDong
{
    public class Non_HopDongLamViec : Non_HopDong
    {
        [DisplayName("Hình thức hợp đồng")]
        public string HinhThucHopDong { get; set; }

        [DisplayName("Mã ngạch")]
        public string MaNgach { get; set; }

        [DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }

        [DisplayName("Bậc lương")]
        public string BacLuong { get; set; }

        [DisplayName("Lương chức danh")]
        public string LuongCoBan { get; set; }
        [DisplayName("Lương chức danh(Thuần)")]
        public string LuongCoBanThuan { get; set; }
        [DisplayName("Lương bổ sung")]
        public string LuongBoSung { get; set; }
        [DisplayName("Lương gộp")]
        public string LuongGop { get; set; }

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

        [DisplayName("Số ngày")]
        public string SoNgay { get; set; }

        [DisplayName("Số tháng")]
        public string SoThang { get; set; }

        [DisplayName("Phụ cấp tiền ăn")]
        public string PhuCapTienAn { get; set; }

        [DisplayName("Phụ cấp tiền xăng")]
        public string PhuCapTienXang { get; set; }
        [DisplayName("Phụ cấp tiền điện thoại")]
        public string PhuCapDienThoai { get; set; }
        [DisplayName("Phụ cấp chức vụ")]
        public string PhuCapChucVu { get; set; }
        [DisplayName("Tổng lương")]
        public string TongLuong { get; set; }

        [DisplayName("Tỉnh Thành")]
        public string TinhThanh { get; set; }

        [DisplayName("Danh xưng công ty hoặc trường")]
        public string DanhXungCongTyHoacTruong { get; set; }

        [DisplayName("Ngày quyết định")]
        public string NgayQuyetDinh { get; set; }

        [DisplayName("Số điện thoại NK")]
        public string SoDienThoaiNK { get; set; }

        [DisplayName("Giới tính NLD")]
        public string GioTinhNLD { get; set; }

        [DisplayName("Điều thay đổi")]
        public string DieuThayDoi { get; set; }

        [DisplayName("Khoản thay đổi")]
        public string KhoanThayDoi { get; set; }

        [DisplayName("Nội dung điều khoản thay đổi")]
        public string NoiDungDieuKhanThayDoi { get; set; }

        [DisplayName("Số hợp đồng (phụ lục hợp đồng cần lấy)")]
        public string SoHopDongLaoDong { get; set; }

        [DisplayName("Ngày ký hợp đồng (phụ lục hợp đồng cần lấy)")]
        public string NgayKyHopDongLaoDongNV { get; set; }

        [DisplayName("Điều khoản thay đổi")]
        public string DieuKhoanThayDoi { get; set; }
    }
}
