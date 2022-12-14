using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.TienLuong.ChamCong;

namespace ERP.Module.NghiepVu.NhanSu.HoSoLuong
{
    [ImageName("BO_BangLuong")]
    [ModelDefault("Caption", "Chọn giá trị")]
    [NonPersistent]
     public class ChonGiaTriLapCongThuc : BaseObject
    {
        //Chỉ dùng để lập công thức
        //======================================================
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Kỳ tính lương")]
        public KyTinhLuong KyTinhLuong { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Hồ sơ tính lương")]
        public ChiTietLuong ChiTietLuong { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Chấm công nhân viên")]
        public CC_ChiTietChamCong CC_ChiTietChamCong { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Chấm công ngoài giờ")]
        public CC_ChiTietCongNgoaiGio CC_ChiTietCongNgoaiGio { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Chấm công khác")]
        public CC_ChiTietCongKhac CC_ChiTietCongKhac { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Tỉ lệ đánh giá")]
        public ChiTietDanhGiaCongViec ChiTietDanhGiaCongViec { get; set; }

        //======================================================

        public ChonGiaTriLapCongThuc(Session session) : base(session) { }
    }
}
