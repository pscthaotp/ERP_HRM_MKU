using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using ERP.Module.MailMerge.ChungTuTS_CCDC_VTHH;
//
namespace ERP.Module.MailMerge
{
    public class Non_DeNghiMaster
    {

        [Browsable(false)]
        public string Oid { get; set; }

        [DisplayName("STT")]
        public string STT { get; set; }

        [DisplayName("Tên tài sản/ dịch vụ")]
        public string TaiSan { get; set; }

        [DisplayName("Đặc điểm/ Quy cách")]
        public string DacDiem { get; set; }

        [DisplayName("Đơn vị tính")]
        public string DonViTinh{ get; set; }

        [DisplayName("Số lượng")]
        public string SoLuong{ get; set; }

        [DisplayName("Ghi Chú")]
        public int GhiChu { get; set; }       
      
    }

}
