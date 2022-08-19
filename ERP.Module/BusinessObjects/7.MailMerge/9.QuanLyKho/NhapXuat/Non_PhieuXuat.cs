using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ERP.Module.MailMerge.QuanLyKho.NhapXuat
{
    public class Non_PhieuXuat : Non_Phieu
    {
        public Non_PhieuXuat()
        {
            Master = new List<Non_PhieuXuatMaster>();
            Detail = new List<Non_PhieuXuatDetail>();
        }

        [DisplayName("Nhân viên nhận")]
        public string ThongTinNhanVien { get; set; }
    }
}
