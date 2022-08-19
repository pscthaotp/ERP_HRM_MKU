using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhMienNhiem : Non_QuyetDinhNhanVien
    {
        [DisplayName("Chức vụ cũ")]
        public string ChucVuCu { get; set; }

        [DisplayName("Chức danh cũ")]
        public string ChucDanhCu { get; set; }

        [DisplayName("Ngày miễn nhiệm")]
        public string NgayMienNhiem { get; set; }

    }
}
