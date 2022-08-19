using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhChuyenCongTac : Non_QuyetDinhNhanVien
    {
        [DisplayName("Từ ngày")]
        public string TuNgay { get; set; }

        [DisplayName("Cơ quan mới")]
        public string CoQuanMoi { get; set; }

    }
}
