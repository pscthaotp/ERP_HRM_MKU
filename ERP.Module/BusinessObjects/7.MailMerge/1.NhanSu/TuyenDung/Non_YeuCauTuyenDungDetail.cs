using System.Collections;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.TuyenDung
{
    public class Non_YeuCauTuyenDungDetail
    {
        [Browsable(false)]
        public string Oid { get; set; }

        [DisplayName("Số thứ tự")]
        public string STT { get; set; }

        [DisplayName("Đơn vị yêu cầu")]
        public string DonViYeuCau { get; set; }

        [DisplayName("Vị trí cần tuyển")]
        public string ViTriTuyenDung { get; set; }

        [DisplayName("Số lượng cần tuyển")]
        public string SoLuongTuyen { get; set; }

        [DisplayName("Mức lương dự kiến")]
        public string LuongDuKien { get; set; }

        [DisplayName("Thời gian vào làm việc dự kiến")]
        public string ThoiGianLamViecDuKien { get; set; }

        [DisplayName("Thời gian vào làm việc dự kiến (Date)")]
        public string ThoiGianLamViecDuKienDate { get; set; }

        [DisplayName("Số lượng tuyển dụng trong định biên")]
        public string SoLuongTrongDinhBien { get; set; }

        [DisplayName("Số lượng tuyển dụng ngoài định biên")]
        public string SoLuongNgoaiDinhBien { get; set; }

        [DisplayName("Trong kế hoạch (định biên)")]
        public string TrongKeHoach { get; set; }

        [DisplayName("Ngoài kế hoạch (định biên)")]
        public string NgoaiKeHoach { get; set; }

        [DisplayName("Thay thế (Lý do)")]
        public string ThayThe { get; set; }

        [DisplayName("Dự án mới (Lý do)")]
        public string DuAnMoi { get; set; }
    }
}
