using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ERP.Module.MailMerge.QuanLyKho.DonHang
{
    public class Non_ToTrinhMuaHang : IMailMergeBase
    {
        public IList Master { get; set; }
        public IList Master1 { get; set; }
        public IList Detail { get; set; }
        public IList Detail1 { get; set; }

        [Browsable(false)]
        public string Oid { get; set; }

        public Non_ToTrinhMuaHang()
        {
            Master = new List<Non_ToTrinhMuaHangMaster>();
            Detail = new List<Non_ToTrinhMuaHangDetail>();
        }

        [DisplayName("Ngày")]
        public string Ngay { get; set; }

        [DisplayName("Số tờ trình")]
        public string SoToTrinh { get; set; }

        [DisplayName("Nhà cung cấp chọn")]
        public string NhaCungCapChon { get; set; }

        [DisplayName("Công ty")]
        public string CongTy { get; set; }

        [DisplayName("Đơn vị")]
        public string BoPhan { get; set; }
    }
}
