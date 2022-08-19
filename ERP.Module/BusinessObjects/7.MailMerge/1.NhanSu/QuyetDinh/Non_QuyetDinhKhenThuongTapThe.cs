using System;
using System.Collections.Generic;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhKhenThuongTapThe : Non_QuyetDinhNhanVien
    {
        public Non_QuyetDinhKhenThuongTapThe()
        {
            Master = new List<Non_QuyetDinhKhenThuongTheMaster>();
            Detail = new List<Non_QuyetDinhKhenThuongTapTheDetail>();
        }
    }
}
