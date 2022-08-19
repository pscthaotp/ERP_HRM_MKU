using System;
using System.Collections.Generic;

namespace ERP.Module.Enum.NhanSu
{
    public enum CongTruEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Cộng")]
        Cong = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Trừ")]
        Tru = 2
    }
}
