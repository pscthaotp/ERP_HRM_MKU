using System;
using DevExpress.Xpo;

namespace ERP.Module.Enum.NhanSu
{
    public enum LoaiQuocGiaEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Trong nước")]
        TrongNuoc = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Ngoài nước")]
        NgoaiNuoc = 0
    };
}
