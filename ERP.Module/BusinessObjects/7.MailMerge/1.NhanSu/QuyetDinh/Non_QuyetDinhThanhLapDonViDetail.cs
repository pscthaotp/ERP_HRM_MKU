using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhThanhLapDonViDetail : Non_MergeItem
    {
        [DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Giới tính")]
        public string GioiTinh { get; set; }
        [DisplayName("Năm sinh")]
        public string NamSinh { get; set; }
        [DisplayName("Ngày sinh (date)")]
        public string NgaySinhDate { get; set; }
        [DisplayName("Quê quán")]
        public string QueQuan { get; set; }
        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }


        [DisplayName("Chức vụ")]
        public string ChucVu { get; set; }

    }
}
