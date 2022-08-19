using System.Collections.Generic;

namespace ERP.Module.MailMerge.NhanSu.TuyenDung
{
    public class Non_YeuCauTuyenDungFull : Non_TuyenDung
    {
        public Non_YeuCauTuyenDungFull()
        {
            Master = new List<Non_YeuCauTuyenDungMaster>();
            Detail = new List<Non_YeuCauTuyenDungDetail>();
        }
    }
}
