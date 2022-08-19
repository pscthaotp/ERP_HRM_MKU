using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ERP.Module.MailMerge.QuanLyKho.HopDongMuaBan
{
    public class Non_HopDongMuaBan : IMailMergeBase
    {
        [Browsable(false)]
        public string Oid { get; set; }

        [DisplayName("Ngày")]
        public string Ngay { get; set; }

        [DisplayName("Số hợp đồng")]
        public string SoHopDong { get; set; }

        [DisplayName("Bên bán")]
        public string BenBan { get; set; }

        [DisplayName("Người đại diện")]
        public string NguoiDaiDien { get; set; }

        [DisplayName("Chức vụ")]
        public string ChucVu { get; set; }

        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [DisplayName("Điện thoại")]
        public string DienThoai { get; set; }

        [DisplayName("Số tk")]
        public string SoTk { get; set; }

        [DisplayName("MST")]
        public string MST { get; set; }

        //
        public IList Master { get; set; }
        public IList Master1 { get; set; }
        public IList Detail { get; set; }
        public IList Detail1 { get; set; }
    }
}
