using System;

namespace ERP.Module.Enum.Systems
{
    public enum LoaiCustomEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Nhân viên đang làm việc")]
        DangLamViec = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nhân viên đã nghỉ việc")]
        DaNghiViec = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nhân viên không nhận việc")]
        KhongNhanViec = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giảng viên thỉnh giảng")]
        GiangVienThinhGiang = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Category 1")]
        Category1 = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Category 2")]
        Category2 = 5,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giảng viên thỉnh giảng đã nghỉ việc")]
        GiangVienThinhGiangDaNghiViec = 6,
    }
}
