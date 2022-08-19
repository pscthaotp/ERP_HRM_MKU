using System;
using DevExpress.Xpo;

namespace ERP.Module.Enum.NhanSu
{
    public enum LoaiQuanHeEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Quan hệ gia đình")]
        GiaDinh = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Quan hệ thân thuộc")]
        ThanToc = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Quan hệ xã hội")]
        XaHoi = 2
    }
}
