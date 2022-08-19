using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhKhenThuongCaNhan : Non_QuyetDinhNhanVien
    {
        [DisplayName("Danh hiệu khen thưởng")]
        public string DanhHieuKhenThuong{ get; set; }

        [DisplayName("Ngày khen thưởng")]
        public string NgayKhenThuong { get; set; }

        [DisplayName("Ngày khen thưởng (date)")]
        public string NgayKhenThuongDate { get; set; }
    }
}
