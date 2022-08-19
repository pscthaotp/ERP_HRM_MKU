using System;
using DevExpress.Xpo;

namespace ERP.Module.PMS.Enum
{
    public enum LoaiGiangVienEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Cơ hữu")]
        CoHuu=0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thỉnh giảng")]
        ThinhGiang = 1
    }
}
