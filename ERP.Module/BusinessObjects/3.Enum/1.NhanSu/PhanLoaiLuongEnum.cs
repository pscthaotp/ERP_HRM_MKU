using System;

namespace ERP.Module.Enum.NhanSu
{
    public enum PhanLoaiLuongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương ngạch bậc (gross)")]
        LuongNgachBacGross = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương khoán")]
        LuongKhoan = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương ngạch bậc (net)")]
        LuongNgachBacNet = 3
    }
}
