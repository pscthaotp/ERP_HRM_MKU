using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ERP.Module.MailMerge.QuanLyKho.NhapXuat
{
    public class Non_Phieu : IMailMergeBase
    {
        public IList Master { get; set; }
        public IList Master1 { get; set; }
        public IList Detail { get; set; }
        public IList Detail1 { get; set; }

        public Non_Phieu()
        {
        }

        [Browsable(false)]
        public string Oid { get; set; }

        [DisplayName("Ngày")]
        public string Ngay { get; set; }

        [DisplayName("Số phiếu")]
        public string SoPhieu { get; set; }

        [DisplayName("Kho")]
        public string Kho { get; set; }

        [DisplayName("Tổng tiền")]
        public string TongTien { get; set; }

        [DisplayName("Tổng tiền bằng chữ")]
        public string TongTienBangChu { get; set; }

        [DisplayName("Logo")]
        public string Logo { get; set; }
    }
}
