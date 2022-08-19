using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhCacVanDeNhanSu : Non_QuyetDinhNhanVien
    {
        [DisplayName("Đơn vị mới")]
        public string DonViMoi { get; set; }

        [DisplayName("Trường hoặc Công ty mới")]
        public string TruongCongTyMoi { get; set; }

        [DisplayName("Chức vụ cũ")]
        public string ChucVuCu { get; set; }

        [DisplayName("Chức danh cũ")]
        public string ChucDanhCu { get; set; }

        [DisplayName("Chức vụ mới")]
        public string ChucVuMoi { get; set; }

        [DisplayName("Chức danh mới")]
        public string ChucDanhMoi { get; set; }

        [DisplayName("Nội dung đề xuất")]
        public string NoiDungDeXuat { get; set; }

        [DisplayName("Tổng tiền lương mới")]
        public string TongLuongMoi { get; set; }

        [DisplayName("Tổng tiền lương củ")]
        public string TongLuongCu { get; set; }


        [DisplayName("Tổng tiền bổ sung khác mới")]
        public string TongLuongBoSungKhacMoi { get; set; }

        [DisplayName("Tổng tiền bổ sung khác cũ")]
        public string TongLuongBoSungKhacCu { get; set; }


        [DisplayName("Chế độ mới")]
        public string CheDoMoi{ get; set; }

        [DisplayName("Chế độ cũ")]
        public string CheDoCu { get; set; }

        
    }
}
