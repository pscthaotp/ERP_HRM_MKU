using ERP.Module.MailMerge.NhanSu.QuyetDinh;
using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.QuanLyKho.DonHang
{
    public class Non_DonHangMuaDetail
    {

        [Browsable(false)]
        public string Oid { get; set; }

        [DisplayName("Số thứ tự")]
        public string STT { get; set; }

        [DisplayName("Kho")]
        public string Kho { get; set; }

        [DisplayName("Tên hàng hóa")]
        public string TenHangHoa { get; set; }

        [DisplayName("Đơn vị tính")]
        public string DonViTinh { get; set; }

        [DisplayName("Đặc điểm")]
        public string DacDiem { get; set; }

        [DisplayName("Số lượng")]
        public string SoLuong { get; set; }

        [DisplayName("Đơn giá")]
        public string DonGia { get; set; }

        [DisplayName("Thành tiền")]
        public string ThanhTien { get; set; }
    }
}
