using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ERP.Module.MailMerge.QuanLyKho.DonHang
{
    public class Non_DonHangMua : IMailMergeBase
    {
        public IList Master { get; set; }
        public IList Master1 { get; set; }
        public IList Detail { get; set; }
        public IList Detail1 { get; set; }

        [Browsable(false)]
        public string Oid { get; set; }

        public Non_DonHangMua()
        {
            Master = new List<Non_DonHangMuaMaster>();
            Detail = new List<Non_DonHangMuaDetail>();
        }

        [DisplayName("Logo")]
        public string Logo { get; set; }

        [DisplayName("Ngày")]
        public string Ngay { get; set; }

        [DisplayName("Số đơn hàng")]
        public string SoDonHang { get; set; }

        [DisplayName("Tổng tiền")]
        public string TongTien { get; set; }

        [DisplayName("Tổng tiền bằng chữ")]
        public string TongTienBangChu { get; set; }

        //
        [DisplayName("Bên bán")]
        public string BenBan { get; set; }

        [DisplayName("(Bên bán) tài khoản")]
        public string BenBan_TaiKhoan { get; set; }

        [DisplayName("(Bên bán) địa chỉ")]
        public string BenBan_DiaChi { get; set; }

        [DisplayName("(Bên bán) điện thoại")]
        public string BenBan_DienThoai { get; set; }

        [DisplayName("(Bên bán) fax")]
        public string BenBan_Fax { get; set; }

        [DisplayName("(Bên bán) người đại diện")]
        public string BenBan_NguoiDaiDien { get; set; }

        [DisplayName("(Bên bán) chức vụ đại diện")]
        public string BenBan_ChucVuDaiDien { get; set; }

        //
        [DisplayName("Bên mua")]
        public string BenMua { get; set; }

        [DisplayName("(Bên mua) mã số thuế")]
        public string BenMua_MST { get; set; }

        [DisplayName("(Bên mua) địa chỉ")]
        public string BenMua_DiaChi { get; set; }

        [DisplayName("(Bên mua) điện thoại")]
        public string BenMua_DienThoai { get; set; }

        [DisplayName("(Bên mua) fax")]
        public string BenMua_Fax { get; set; }

        [DisplayName("(Bên mua) người đại diện")]
        public string BenMua_NguoiDaiDien { get; set; }

        [DisplayName("(Bên mua) chức vụ đại diện")]
        public string BenMua_ChucVuDaiDien { get; set; }
    }
}
