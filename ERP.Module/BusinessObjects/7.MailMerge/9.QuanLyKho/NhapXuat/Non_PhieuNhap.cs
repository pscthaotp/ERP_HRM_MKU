using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ERP.Module.MailMerge.QuanLyKho.NhapXuat
{
    public class Non_PhieuNhap : Non_Phieu
    {
        public Non_PhieuNhap()
        {
            Master = new List<Non_PhieuNhapMaster>();
            Detail = new List<Non_PhieuNhapDetail>();
        }

        [DisplayName("Số hóa đơn")]
        public string SoHoaDon { get; set; }
    }
}
