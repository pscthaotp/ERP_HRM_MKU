using ERP.Module.MailMerge.NhanSu.QuyetDinh;
using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.QuanLyKho.DonHang
{
    public class Non_ToTrinhMuaHangDetail
    {

        [Browsable(false)]
        public string Oid { get; set; }

        [DisplayName("Số thứ tự")]
        public string STT { get; set; }

        [DisplayName("Tên hàng hóa")]
        public string TenHangHoa { get; set; }

        [DisplayName("Đơn vị tính")]
        public string DonViTinh { get; set; }

        [DisplayName("Số lượng")]
        public string SoLuong { get; set; }

        [DisplayName("Đơn giá 1")]
        public string DonGia1 { get; set; }

        [DisplayName("Thành tiền 1")]
        public string ThanhTien1 { get; set; }

        [DisplayName("Đơn giá 2")]
        public string DonGia2 { get; set; }

        [DisplayName("Thành tiền 2")]
        public string ThanhTien2 { get; set; }

        [DisplayName("Đơn giá 3")]
        public string DonGia3 { get; set; }

        [DisplayName("Thành tiền 3")]
        public string ThanhTien3 { get; set; }
    }
}
