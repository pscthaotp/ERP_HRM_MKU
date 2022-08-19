using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhLuanChuyen : Non_QuyetDinhNhanVien
    {
        [DisplayName("Chức vụ cũ")]
        public string ChucVuCu { get; set; }

        [DisplayName("Chức vụ mới")]
        public string ChucVuMoi { get; set; }

        [DisplayName("Số tháng")]
        public string SoThang { get; set; }

        [DisplayName("Số năm")]
        public string SoNam { get; set; }

        [DisplayName("Tên trường/Công ty chuyển đến")]
        public string TenTruongHoacCongTyMoi { get; set; }

        [DisplayName("Bộ phận cũ")]
        public string BoPhanCu { get; set; }

        [DisplayName("Bộ phận mới")]
        public string BoPhanMoi { get; set; }

        [DisplayName("Chức danh mới")]
        public string ChucDanhMoi { get; set; }

    }
}
