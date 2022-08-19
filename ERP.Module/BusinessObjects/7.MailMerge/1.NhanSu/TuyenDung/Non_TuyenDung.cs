using System.Collections;
using System.ComponentModel;
//
namespace ERP.Module.MailMerge.NhanSu.TuyenDung
{
    public class Non_TuyenDung : IMailMergeBase
    {
        [Browsable(false)]
        public string Oid { get; set; }

        [DisplayName("Đơn vị chủ quản")]
        public string DonViChuQuan { get; set; }

        [DisplayName("Tên Côngty/Trường viết hoa")]
        public string TenCongTyVietHoa { get; set; }

        [DisplayName("Tên Côngty/Trường viết thường")]
        public string TenCongTyVietThuong { get; set; }

        [DisplayName("Địa chỉ Côngty/Trường")]
        public string DiaChiCongTyHoacTruong { get; set; }

        [DisplayName("Số điện thoại Côngty/Trường")]
        public string SoDienThoaiCongTyHoacTruong { get; set; }

        [DisplayName("Số Fax Côngty/Trường")]
        public string SoFaxCongTyHoacTruong { get; set; }

        [DisplayName("Email Côngty/Trường")]
        public string EmailCongTyHoacTruong { get; set; }

        [DisplayName("Website Côngty/Trường")]
        public string WebsiteCongTyHoacTruong { get; set; }

        [DisplayName("Số quyết định")]
        public string SoQuyetDinh { get; set; }

        [DisplayName("Ngày quyết định")]
        public string NgayQuyetDinh { get; set; }

        [DisplayName("Ngày quyết định (date)")]
        public string NgayQuyetDinhDate { get; set; }

        [DisplayName("Ngày hiệu lực")]
        public string NgayHieuLuc { get; set; }

        [DisplayName("Ngày hiệu lực (date)")]
        public string NgayHieuLucDate { get; set; }

        [DisplayName("Niên độ")]
        public string NienDo { get; set; }

        [DisplayName("Đợt tuyển dụng")]
        public string DotTuyenDung { get; set; }

        [DisplayName("Chức vụ người ký hoa")]
        public string ChucVuNguoiKy { get; set; }

        [DisplayName("Chức vụ người ký viết thường")]
        public string ChucVuNguoiKyVietThuong { get; set; }

        [DisplayName("Người ký viết thường")]
        public string TenNguoiKyVietThuong { get; set; }

        [DisplayName("Người ký viết hoa")]
        public string TenNguoiKyVietHoa { get; set; }

        [DisplayName("Danh xưng người ký viết thường")]
        public string DanhXungNguoiKyVietThuong { get; set; }

        [DisplayName("Danh xưng người ký viết hoa")]
        public string DanhXungNguoiKyVietHoa { get; set; }

        [DisplayName("Trạng thái tuyển dụng")]
        public string TrangThaiTuyenDung { get; set; }

        [DisplayName("Nơi Nhận")]
        public string NoiNhan { get; set; }

        [DisplayName("Tỉnh Thành")]
        public string TinhThanh { get; set; }

        [DisplayName("Danh xưng công ty hoặc trường")]
        public string DanhXungCongTyHoacTruong { get; set; }

        [DisplayName("Ban tổng giám đốc hoặc ban giám hiệu")]
        public string BanTongGiamDocHoacBanGiamHieu { get; set; }

        [DisplayName("Thời gian dự kiến từ ngày (Date)")]
        public string ThoiGianDuKienTuNgayDate { get; set; }

        [DisplayName("Thời gian dự kiến đến ngày (Date)")]
        public string ThoiGianDuKienDenNgayDate { get; set; }

        [DisplayName("Thời hạn nộp hồ sơ từ ngày (Date)")]
        public string ThoiHanNopHoSoTuNgayDate { get; set; }

        [DisplayName("Thời hạn nộp hồ sơ đến ngày (Date)")]
        public string ThoiHanNopHoSoDenNgayDate { get; set; }

        [DisplayName("Thời gian thực hiện từ ngày (Date)")]
        public string ThoiGianThucHienTuNgayDate { get; set; }

        [DisplayName("Thời gian thực hiện đến ngày (Date)")]
        public string ThoiGianThucHienDenNgayDate { get; set; }

        public IList Master { get; set; }
        public IList Master1 { get; set; }
        public IList Detail { get; set; }
        public IList Detail1 { get; set; }
    }
}
