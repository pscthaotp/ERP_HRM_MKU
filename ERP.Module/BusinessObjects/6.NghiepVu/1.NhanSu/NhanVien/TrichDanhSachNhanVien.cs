using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
//
namespace ERP.Module.NghiepVu.NhanSu.NhanViens
{
    [NonPersistent]
    [ImageName("BO_Extract")]
    [ModelDefault("Caption", "Trích danh sách cán bộ")]
    public class TrichDanhSachNhanVien : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<ChiTietTimKiemNhanVien> ListChiTietTrichDanhSachNhanVien { get; set; }

        public TrichDanhSachNhanVien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            ListChiTietTrichDanhSachNhanVien = new XPCollection<ChiTietTimKiemNhanVien>(Session, false);
        }
    }

}
