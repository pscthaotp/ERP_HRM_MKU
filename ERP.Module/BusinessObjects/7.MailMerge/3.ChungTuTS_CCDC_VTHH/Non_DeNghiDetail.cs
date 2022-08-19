using DevExpress.ExpressApp.Model;
using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.ChungTuTS_CCDC_VTHH
{
    public class Non_DeNghiDetail
    {
        [Browsable(false)]
        public string Oid { get; set; }

        [DisplayName("Số thứ tự")]
        public string STT { get; set; }

        [DisplayName("Tên tài sản/ dịch vụ")]
        public string TenHangHoa { get; set; }

        [DisplayName("Đặc điểm/ Quy cách")]
        public string DacDiem { get; set; }

        [DisplayName("Đơn vị tính")]
        public string DonViTinh { get; set; }

        [DisplayName("Số lượng")]
        [ModelDefault("DisplayFormat", "N0")]
        public string SoLuong { get; set; }

        [DisplayName("Ghi chú")]
        public string GhiChu{ get; set; }

        [DisplayName("Đơn giá")]      
        public string DonGia { get; set; }

        [DisplayName("Thành tiền")]       
        public string ThanhTien { get; set; }

        [DisplayName("Người sử dụng")]
        public string NguoiSuDung { get; set; }

    }
}
