using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using ERP.Module.DanhMuc.NhanSu;

//
namespace ERP.Module.NghiepVu.NhanSu.DieuKien
{
    [NonPersistent]
    [DefaultProperty("HoTen")]
    [ModelDefault("Caption", "Điều kiện tổng hợp")]
    public class DieuKienTongHop : BaseObject
    {
        [ModelDefault("Caption", "Hồ sơ")]
        public DieuKien_HoSo HoSo { get; set; }

        [ModelDefault("Caption", "Nhân viên")]
        public DieuKien_NhanVien NhanVien { get; set; }

        [ModelDefault("Caption", "Chức vụ")]
        public ChucVu ChucVu { get; set; }

        [ModelDefault("Caption", "Chức danh")]
        public ChucDanh ChucDanh { get; set; }

        [ModelDefault("Caption", "Loại hợp đồng")]
        public LoaiHopDong LoaiHopDong { get; set; }

        [ModelDefault("Caption", "Loại nhân sự")]
        public LoaiNhanSu LoaiNhanSu { get; set; }

        [ModelDefault("Caption", "Thông tin lương")]
        public DieuKien_ThongTinLuong NhanVienThongTinLuong { get; set; }

        [ModelDefault("Caption", "Trình độ nhân viên")]
        public DieuKien_TrinhDo NhanVienTrinhDo { get; set; }

        [ModelDefault("Caption", "Hợp đồng lao động")]
        public DieuKien_HopDong HopDong { get; set; }
      
        public DieuKienTongHop(Session session) : base(session) { }
    }

}
