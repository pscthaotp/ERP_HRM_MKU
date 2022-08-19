using System;

namespace ERP.Module.Enum.Systems
{
    public enum LoaiEmailEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Gửi")]
        Gui = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nhận")]
        Nhan = 1
    }
}
