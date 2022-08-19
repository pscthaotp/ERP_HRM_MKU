using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhNangLuongTapTheDetail : Non_MergeItem
    {
        [DisplayName("Số thứ tự")]
        public string STT { get; set; }

        [DisplayName("Họ tên")]
        public string HoTen { get; set; }

        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }

        //

        [DisplayName("Mã ngạch lương")]
        public string MaNgachLuong { get; set; }

        [DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }

        [DisplayName("Bậc lương cũ")]
        public string BacLuongCu { get; set; }

        [DisplayName("Hệ số lương cũ")]
        public string HeSoLuongCu { get; set; }

        [DisplayName("Vượt khung cũ")]
        public string VuotKhungCu { get; set; }

        [DisplayName("Ngày hưởng lương cũ")]
        public string NgayHuongLuongCu { get; set; }

        [DisplayName("Mốc nâng lương cũ")]
        public string MocNangLuongCu { get; set; }

        [DisplayName("Lương chức danh cũ")]
        public string LuongCoBanCu { get; set; }

        [DisplayName("Lương bổ sung(HQCV) cũ")]
        public string LuongKinhDoanhCu { get; set; }

        //
        [DisplayName("Bậc lương mới")]
        public string BacLuongMoi { get; set; }

        [DisplayName("Hệ số lương mới")]
        public string HeSoLuongMoi { get; set; }

        [DisplayName("Vượt khung mới")]
        public string VuotKhungMoi { get; set; }

        [DisplayName("Ngày hưởng lương mới")]
        public string NgayHuongLuongMoi { get; set; }

        [DisplayName("Lương chức danh mới")]
        public string LuongCoBanMoi { get; set; }

        [DisplayName("Lương bổ sung(HQCV) mới")]
        public string LuongKinhDoanhMoi { get; set; }
        //
    }
}
