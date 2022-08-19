using System;
using System.Collections.Generic;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhTuyenDungTapThe : Non_QuyetDinh
    {
        public Non_QuyetDinhTuyenDungTapThe()
        {
            Master = new List<Non_QuyetDinhTuyenDungMaster>();
            Detail = new List<Non_QuyetDinhTuyenDungDetail>();
        }

        [System.ComponentModel.DisplayName("Đợt tuyển dụng")]
        public int DotTuyenDung { get; set; }
        [System.ComponentModel.DisplayName("Năm tuyển dụng")]
        public string NamTuyenDung { get; set; }
    }
}
