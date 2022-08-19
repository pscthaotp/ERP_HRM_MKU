using System;

namespace ERP.Module.Enum.Systems
{
    public enum LoaiXacThucEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Email")]
        Email = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("SMS")]
        SMS = 2
    }
}
