using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhTaiBoNhiem : Non_QuyetDinhNhanVien
    {
        [DisplayName("Chức vụ cũ")]
        public string ChucVuCu { get; set; }

        [DisplayName("Chức danh cũ")]
        public string ChucDanhCu { get; set; }

        [DisplayName("PC kiêm nhiệm cũ")]
        public string PhuCapKiemNhiemCu { get; set; }

        [DisplayName("Ngày bổ nhiệm cũ")]
        public string NgayBoNhiemCu { get; set; }

        [DisplayName("Chức vụ mới")]
        public string ChucVuMoi { get; set; }

        [DisplayName("Chức danh mới")]
        public string ChucDanhMoi { get; set; }

        [DisplayName("PC kiêm nhiệm mới")]
        public string PhuCapKiemNhiemMoi { get; set; }

        [DisplayName("Ngày hết nhiệm kỳ")]
        public string NgayHetNhiemKy { get; set; }

        [DisplayName("Số tháng")]
        public string SoThang { get; set; }

        [DisplayName("Số năm")]
        public string SoNam { get; set; }

    }
}
