using System;
using System.Collections.Generic;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhThanhLapDonViTapThe : Non_QuyetDinh
    {
        public Non_QuyetDinhThanhLapDonViTapThe()
        {
            Master = new List<Non_QuyetDinhThanhLapDonViMaster>();
            Detail = new List<Non_QuyetDinhThanhLapDonViDetail>();
        }

        [System.ComponentModel.DisplayName("Đợt tuyển dụng")]
        public int DotTuyenDung { get; set; }
        [System.ComponentModel.DisplayName("Năm tuyển dụng")]
        public string NamTuyenDung { get; set; }
        [System.ComponentModel.DisplayName("Nhiệm vụ đơn vị mới")]
        public string NhiemVuDonViMoi { get; set; }
        [System.ComponentModel.DisplayName("Nhiệm vụ đơn vị khác")]
        public string NhiemVuDonViKhac { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị mới")]
        public string DonViMoi { get; set; }
    }
}
