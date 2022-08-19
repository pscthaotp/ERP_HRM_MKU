using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhChamDutHopDong : Non_QuyetDinhNhanVien
    {

        [DisplayName("Số hợp đồng")]
        public string SoHopDong { get; set; }

        [DisplayName("Ngày hợp đồng")]
        public string NgayHopDong { get; set; }

        [DisplayName("Từ ngày")]
        public string TuNgay { get; set; }

        [DisplayName("Từ ngày (date)")]
        public string TuNgayDate { get; set; }

        [DisplayName("Lý do")]
        public string LyDo { get; set; }

        [DisplayName("Tên loại hợp đồng")]
        public string TenLoaiHopDong { get; set; }

        [DisplayName("Ngày ký thỏa thuận")]
        public string NgayKyThoaThuan { get; set; }

        [DisplayName("Ngày ký thỏa thuận (date)")]
        public string NgayKyThoaThuanDate { get; set; }

    }
}
