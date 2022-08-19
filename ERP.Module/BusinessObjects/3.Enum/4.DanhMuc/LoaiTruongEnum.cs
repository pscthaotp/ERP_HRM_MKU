using System;
using System.Collections.Generic;

namespace ERP.Module.Enum.TuyenSinh_PT
{
    public enum LoaiTruongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Chưa chọn")]
        NA = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Mầm non")]
        MN = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Phổ thông")]
        PT = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Cao đẳng - Đại học")]
        DH = 3
    }
}
