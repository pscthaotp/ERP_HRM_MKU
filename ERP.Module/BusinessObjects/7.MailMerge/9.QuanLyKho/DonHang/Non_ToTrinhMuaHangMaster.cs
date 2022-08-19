using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;

namespace ERP.Module.MailMerge.QuanLyKho.DonHang
{
    public class Non_ToTrinhMuaHangMaster
    {
        [Browsable(false)]
        public string Oid { get; set; }

        [DisplayName("Nhà cung cấp 1")]
        public string NhaCungCap1 { get; set; }

        [DisplayName("Nhà cung cấp 2")]
        public string NhaCungCap2 { get; set; }

        [DisplayName("Nhà cung cấp 3")]
        public string NhaCungCap3 { get; set; }

    }
}
