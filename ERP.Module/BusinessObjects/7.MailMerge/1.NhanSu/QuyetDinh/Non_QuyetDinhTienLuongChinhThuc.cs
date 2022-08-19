using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhTienLuongChinhThuc : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Lương Cơ Bản Mới")]
        public string LuongCoBanMoi { get; set; }
        [System.ComponentModel.DisplayName("Lương Bổ Sung(HQCV) Mới")]
        public string LuongKinhDoanhMoi { get; set; }
        [System.ComponentModel.DisplayName("Lương Cơ Bản Cũ")]
        public string LuongCoBanCu { get; set; }
        [System.ComponentModel.DisplayName("Lương Bổ Sung(HQCV) Cũ")]
        public string LuongKinhDoanhCu { get; set; }
        [System.ComponentModel.DisplayName("Tổng Lương Mới")]
        public string TongLuongMoi { get; set; }
        [System.ComponentModel.DisplayName("Mức Hưởng Thử Việc")]
        public float MucHuongThuViec { get; set; }
    }
}
