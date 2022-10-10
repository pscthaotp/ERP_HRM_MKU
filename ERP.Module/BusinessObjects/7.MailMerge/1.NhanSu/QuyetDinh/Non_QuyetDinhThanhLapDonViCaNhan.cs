using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhThanhLapDonViCaNhan : Non_QuyetDinhNhanVien
    {
        [DisplayName("Đơn vị mới")]
        public string DonViMoi { get; set; }
        [DisplayName("Chức năng đơn vị mới")]
        public string ChucNangDonViMoi { get; set; }
        [DisplayName("Nhiệm vụ đơn vị mới")]
        public string NhiemVuDonViMoi { get; set; }
        [DisplayName("Đơn vị tách")]
        public string DonViTach { get; set; }
        [DisplayName("Tên tiếng Anh đơn vị mới")]
        public string TenTiengAnhDonViMoi { get; set; }

    }
}
