using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.NghiepVu.NhanSu.DieuKien
{
    [NonPersistent]
    [ModelDefault("Caption", "Hợp đồng hiện tại")]
    public class DieuKien_HopDong : BaseObject
    {
        [ModelDefault("Caption", "Số hợp đồng")]
        public string SoHopDong { get; set; }

        [ModelDefault("Caption", "Ngày ký")]
        public DateTime NgayKy { get; set; }

        [ModelDefault("Caption", "Hình thức hợp đồng")]
        public LoaiHopDong HinhThucHopDong { get; set; }

        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay { get; set; }

        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay { get; set; }
        
        public DieuKien_HopDong(Session session) : base(session) { }
    }

}
