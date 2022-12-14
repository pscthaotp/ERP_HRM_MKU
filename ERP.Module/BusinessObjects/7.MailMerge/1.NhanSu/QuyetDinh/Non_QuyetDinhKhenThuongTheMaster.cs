using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhKhenThuongTheMaster : Non_MergeItem
    {
        [DisplayName("Đơn vị chủ quản")]
        public string DonViChuQuan { get; set; }

        [DisplayName("Tên Trường viết hoa")]
        public string TenCongTyVietHoa { get; set; }

        [DisplayName("Tên Trường viết thường")]
        public string TenCongTyVietThuong { get; set; }

        [DisplayName("Chức vụ người ký")]
        public string ChucVuNguoiKy { get; set; }

        [DisplayName("Người ký")]
        public string TenNguoiKy { get; set; }

        [DisplayName("Số quyết định")]
        public string SoQuyetDinh { get; set; }

        [DisplayName("Ngày quyết định")]
        public string NgayQuyetDinh { get; set; }

        [DisplayName("Ngày hiệu lực")]
        public string NgayHieuLuc { get; set; }

        [DisplayName("Tên niên độ viết thường")]
        public string TenNienDoVietThuong { get; set; }

        [DisplayName("Tên niên độ viết hoa")]
        public string TenNienDoVietHoa { get; set; }
    }
}
