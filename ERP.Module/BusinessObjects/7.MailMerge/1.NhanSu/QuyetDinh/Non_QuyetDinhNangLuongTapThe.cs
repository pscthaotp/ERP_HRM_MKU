using System;
using System.Collections.Generic;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhNangLuongTapThe : Non_QuyetDinhNhanVien
    {
        public Non_QuyetDinhNangLuongTapThe()
        {
            Master = new List<Non_QuyetDinhNangLuongTapTheMaster>();
            Detail = new List<Non_QuyetDinhNangLuongTapTheDetail>();
        }
    }
}
