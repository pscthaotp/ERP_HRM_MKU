using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.NhanSu;
//
namespace ERP.Module.NghiepVu.NhanSu.DieuKien
{
    [NonPersistent]
    [ModelDefault("Caption", "Điều kiện nhân viên")]
    public class DieuKien_NhanVien : BaseObject
    {        

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan { get; set; }

        [ModelDefault("Caption", "Công việc hiện nay")]
        public CongViec CongViecHienNay { get; set; }

        [ModelDefault("Caption", "Ngày vào Trường")]
        public DateTime NgayVaoCongTy { get; set; }

        [ModelDefault("Caption", "Tình trạng")]
        public TinhTrang TinhTrang { get; set; }

        [ModelDefault("Caption", "Ngày nghỉ việc")]
        public DateTime NgayNghiViec { get; set; }

        [ModelDefault("Caption", "Ngày nghỉ thai sản")]
        public DateTime NgayNghiThaiSan { get; set; }
        [ModelDefault("Caption", "Phân loại nhân sự")]
        public PhanLoaiNhanSu PhanLoaiNhanSu { get; set; }

        public DieuKien_NhanVien(Session session) : base(session) { }
    }

}
