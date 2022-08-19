using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using ERP.Module.PMS.CauHinh.HeSo;

namespace ERP.Module.PMS.NghiepVu
{
    [ImageName("BO_BangLuong")]
    [ModelDefault("Caption", "Chọn giá trị lập công thức(PMS)")]
    [NonPersistent]

    public class ChonGiaTriLapCongThucPMS : BaseObject
    {
        //Chỉ dùng để lập công thức
        //======================================================
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Hệ số lớp đông")]
        public HeSoLopDong HeSoLopDong { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Hệ số chức danh")]
        public HeSoChucDanh HeSoChucDanh { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Hệ số ngoài giờ")]
        public HeSoGiangDay_NgoaiGio HeSoGiangDay_NgoaiGio { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Hệ số cơ sở")]
        public HeSoCoSo HeSoCoSo { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Hệ số Tín chỉ")]
        public HeSoTinChi HeSoTinChi { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Khối lượng giảng dạy")]
        public ChiTietKhoiLuongGiangDay KhoiLuongGiangDay { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Cấu hình quy đổi PMS")]
        public CauHinhQuyDoiPMS CauHinhQuyDoiPMS { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Thông tin bảng chốt")]
        public ThongTinBangChot ThongTinBangChot { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Chi tiết thông tin bảng chốt")]
        public ChiTietBangChotThuLaoGiangDay ChiTietBangChotThuLaoGiangDay { get; set; }

        public ChonGiaTriLapCongThucPMS(Session session) : base(session) { }
    }
}
