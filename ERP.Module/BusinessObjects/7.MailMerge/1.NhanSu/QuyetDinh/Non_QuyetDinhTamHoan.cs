using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhTamHoan : Non_QuyetDinhNhanVien
    {   

        [DisplayName("Ngày hết tạm hoãn")]
        public string NgayHetTamHoan { get; set; }

        [DisplayName("Số tháng")]
        public string SoThang { get; set; }

        [DisplayName("Số năm")]
        public string SoNam { get; set; }


    }
}
