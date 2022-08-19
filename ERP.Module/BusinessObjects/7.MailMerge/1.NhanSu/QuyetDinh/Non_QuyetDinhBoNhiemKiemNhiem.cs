using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhBoNhiemKiemNhiem : Non_QuyetDinhNhanVien
    {
        [DisplayName("Chức vụ mới")]
        public string ChucVuMoi { get; set; }

        [DisplayName("Hệ số chức vụ mới")]
        public string HSPCChucVuMoi { get; set; }

        [DisplayName("Ngày hưởng hệ số mới")]
        public string NgayHuongHeSoMoi { get; set; }

        [DisplayName("Ngày hết nhiệm kỳ")]
        public string NgayHetNhiemKy { get; set; }

    }
}
