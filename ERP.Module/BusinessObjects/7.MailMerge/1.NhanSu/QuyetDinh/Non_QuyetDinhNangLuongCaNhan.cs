using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhNangLuongCaNhan : Non_QuyetDinhNhanVien
    {
        [DisplayName("Tên ngạch lương mới")]
        public string TenNgachLuongMoi{ get; set; }

        [DisplayName("Mã ngạch lương mới")]
        public string MaNgachLuongMoi { get; set; }

        [DisplayName("Bậc lương mới")]
        public string BacLuongMoi { get; set; }

        [DisplayName("Ngày hưởng lương mới")]
        public string NgayHuongLuongMoi { get; set; }

        [DisplayName("Ngày hưởng lương mới (date)")]
        public string NgayHuongLuongMoiDate { get; set; }

        [DisplayName("Loại nâng lương")]
        public string LoaiNangLuong { get; set; }

        [DisplayName("Lương chức danh mới")]
        public string LuongCoBanMoi { get; set; }

        [DisplayName("Lương bổ sung(HQCV) mới")]
        public string LuongKinhDoanhMoi { get; set; }

        [DisplayName("Pc trách nhiệm mới")]
        public string PCTrachNhiemMoi { get; set; }

        [DisplayName("Pc kiêm nhiệm mới")]
        public string PCKiemNhiemMoi { get; set; }
        //
        [DisplayName("Tổng lương mới")]
        public string TongLuongMoi { get; set; }
    }
}
