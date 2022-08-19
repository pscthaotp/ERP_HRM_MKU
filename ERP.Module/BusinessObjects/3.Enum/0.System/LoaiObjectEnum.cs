using System;

namespace ERP.Module.Enum.Systems
{
    public enum LoaiObjectEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Persistent")]
        Persistent = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("NonPersistent")]
        NonPersistent = 1
    }
}
