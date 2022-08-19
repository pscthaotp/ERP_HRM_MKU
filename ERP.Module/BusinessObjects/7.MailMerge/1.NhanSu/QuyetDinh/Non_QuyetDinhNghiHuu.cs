using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhNghiHuu : Non_QuyetDinhNhanVien
    {
        [DisplayName("Nghỉ việc từ ngày")]
        public string NghiViecTuNgay { get; set; }

        [DisplayName("Nghỉ việc từ ngày (date)")]
        public string NghiViecTuNgayDate { get; set; }

    }
}
