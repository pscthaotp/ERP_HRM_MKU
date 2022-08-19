using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
//
namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinh : IMailMergeBase
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

        [DisplayName("Căn cứ")]
        public string CanCu { get; set; }

        [DisplayName("Về việc")]
        public string NoiDung { get; set; }

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

        [DisplayName("Số lượng cán bộ")]
        public string SoLuongCanBo { get; set; }

        [DisplayName("Nơi Nhận")]
        public string NoiNhan { get; set; }

        [DisplayName("Tỉnh Thành")]
        public string TinhThanh { get; set; }

        [DisplayName("Danh xưng công ty hoặc trường")]
        public string DanhXungCongTyHoacTruong { get; set; }

        [DisplayName("Ban tổng giám đốc hoặc ban giám hiệu")]
        public string BanTongGiamDocHoacBanGiamHieu { get; set; }

        [DisplayName("Số doanh nghiệp")]
        public string SoDoanhNghiep { get; set; }

        [DisplayName("Sở KH&ĐT")]
        public string SoKHDT { get; set; }

        [DisplayName("Tên niên độ viết thường")]
        public string TenNienDoVietThuong { get; set; }

        [DisplayName("Tên niên độ viết hoa")]
        public string TenNienDoVietHoa { get; set; }

        [DisplayName("Tên bộ phận phụ trách nhân sự")]
        public string TenBoPhanNhanSu { get; set; }

        public IList Master { get; set; }
        public IList Master1 { get; set; }
        public IList Detail { get; set; }
        public IList Detail1 { get; set; }
    }

}
