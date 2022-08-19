using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhTienLuongThuViec : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Lương Cơ Bản")]
        public string LuongCoBan { get; set; }
        [System.ComponentModel.DisplayName("Lương Bổ Sung(HQCV)")]
        public string LuongKinhDoanh { get; set; }
        //[System.ComponentModel.DisplayName("Lương Cơ Bản Cũ")]
        //public string LuongCoBanCu { get; set; }
        //[System.ComponentModel.DisplayName("Lương Bổ Sung(HQCV) Cũ")]
        //public string LuongKinhDoanhCu { get; set; }
        [System.ComponentModel.DisplayName("Tổng Lương")]
        public string TongLuong { get; set; }

    }
}
