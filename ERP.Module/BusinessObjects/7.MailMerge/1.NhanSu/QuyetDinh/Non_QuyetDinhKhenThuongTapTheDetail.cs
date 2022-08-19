using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhKhenThuongTapTheDetail : Non_MergeItem
    {
        [DisplayName("Số thứ tự")]
        public string STT { get; set; }

        [DisplayName("Mã tập đoàn")]
        public string MaTapDoan { get; set; }

        [DisplayName("Mã nhân viên")]
        public string MaNhanVien { get; set; }

        [DisplayName("Họ tên")]
        public string HoTen { get; set; }

        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }

        [DisplayName("Chức danh")]
        public string ChucDanh { get; set; }

        [DisplayName("Ngày vào cty")]
        public string NgayVaoCT { get; set; }

        [DisplayName("Ngày vào tập đoàn")]
        public string NgayVaoTD { get; set; }
     
        [DisplayName("Danh hiệu khen thưởng")]
        public string DanhHieuKhenThuong { get; set; }

        [DisplayName("Ngày khen thưởng")]
        public string NgayKhenThuong { get; set; }

        //
    }
}
