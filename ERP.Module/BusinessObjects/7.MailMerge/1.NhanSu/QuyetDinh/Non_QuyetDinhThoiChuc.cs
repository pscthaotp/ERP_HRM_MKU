using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhThoiChuc : Non_QuyetDinhNhanVien
    {

        [DisplayName("Chức vụ cũ")]
        public string ChucVuCu { get; set; }

        [DisplayName("Chức danh cũ")]
        public string ChucDanhCu { get; set; }

        [DisplayName("PC trách nhiệm cũ")]
        public string PCTrachNhiemCu { get; set; }

        [DisplayName("PC kiêm nhiệm cũ")]
        public string PCKiemNhiemCu { get; set; }

        [DisplayName("Ngày BN chức vụ cũ")]
        public string NgayBNChucVuCu { get; set; }

        //
        [DisplayName("Chức vụ mới")]
        public string ChucVuMoi { get; set; }

        [DisplayName("Chức danh mới")]
        public string ChucDanhMoi { get; set; }

        [DisplayName("PC trách nhiệm mới")]
        public string PCTrachNhiemMoi { get; set; }

        [DisplayName("PC kiêm nhiệm mới")]
        public string PCKiemNhiemMoi { get; set; }

        [DisplayName("Lý do")]
        public string LyDo { get; set; }

    }
}
