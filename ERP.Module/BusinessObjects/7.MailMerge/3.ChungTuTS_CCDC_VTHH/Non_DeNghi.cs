using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using ERP.Module.MailMerge.ChungTuTS_CCDC_VTHH;
using System.Drawing;
//
namespace ERP.Module.MailMerge
{
    public class Non_DeNghi : IMailMergeBase
    {
        [Browsable(false)]
        public string Oid { get; set; }

        [DisplayName("Tên Trường viết hoa")]
        public string TenCongTyVietHoa { get; set; }

        [DisplayName("Tên Trường viết thường")]
        public string TenCongTyVietThuong { get; set; }

        [DisplayName("Số quyết định")]
        public string SoQuyetDinh { get; set; }

        [DisplayName("Ngày quyết định")]
        public string NgayQuyetDinh { get; set; }

        [DisplayName("Đơn vị yêu cầu")]
        public string DonViChuQuan { get; set; }

        [DisplayName("Thời gian đáp ứng")]
        public string ThoiGianDapUng { get; set; }

        [DisplayName("Tên tài sản")]
        public string TenHangHoa { get; set; }

        [DisplayName("Mô tả chi tiết (đính kèm file)")]
        public string FileMoTaChiTiet { get; set; }

        [DisplayName("Số đề nghị")]
        public string SoDeNghi { get; set; }

        [DisplayName("Ngày lập")]
        public string NgayLap { get; set; }

        [DisplayName("Mục đích sử dụng")]
        public string MucDichSuDung { get; set; }

        [DisplayName("Tổng số lượng")]
        public string TongSoLuong { get; set; }

        [DisplayName("Tổng số tiền")]
        public string TongTien { get; set; }

        [DisplayName("Tiêu đề")]
        public string TieuDe { get; set; }

        [DisplayName("Tiêu đề viết hoa")]
        public string TieuDeVietHoa { get; set; }    
      
        //
        [DisplayName("Logo")]
        public string Logo { get; set; }
        //


        public IList Master { get; set; }
        public IList Detail { get; set; }

        public IList Master1 { get; set; }
        public IList Detail1 { get; set; }
    }

}
