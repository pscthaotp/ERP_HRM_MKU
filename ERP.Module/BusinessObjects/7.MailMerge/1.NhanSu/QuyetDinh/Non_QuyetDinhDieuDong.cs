using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhDieuDong : Non_QuyetDinhNhanVien
    {

        [DisplayName("Chức danh mới")]
        public string ChucDanhMoi { get; set; }

        [DisplayName("Bộ phận mới")]
        public string BoPhanMoi { get; set; }

        [DisplayName("Chức vụ người ký viết thường")]
        public string ChucVuNguoiKyVietThuong { get; set; }

        [DisplayName("Tên trường/Công ty chuyển đến")]
        public string TenTruongHoacCongTyMoi { get; set; }


    }
}
