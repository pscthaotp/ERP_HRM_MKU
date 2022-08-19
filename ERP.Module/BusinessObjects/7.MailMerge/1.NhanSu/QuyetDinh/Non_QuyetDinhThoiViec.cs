using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhThoiViec : Non_QuyetDinhNhanVien
    {
        [DisplayName("Nghỉ việc từ ngày")]
        public string NghiViecTuNgay { get; set; }

        [DisplayName("Nghỉ việc từ ngày (date)")]
        public string NghiViecTuNgayDate { get; set; }

        [DisplayName("Thời hạn bàn giao công việc")]
        public string ThoiHanBanGiaoCongViec { get; set; }

        [DisplayName("Thời hạn bàn giao công việc (date)")]
        public string ThoiHanBanGiaoCongViecDate { get; set; }

        [DisplayName("Lý do")]
        public string LyDo { get; set; }

    }
}
