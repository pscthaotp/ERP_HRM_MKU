using System;
using System.Collections.Generic;

//
namespace ERP.Module.Enum.PMS
{
    public enum DayOfWeekEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Chủ nhật")]
        Sunday = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thứ 2")]
        Monday = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thứ 3")]
        Tuesday = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thứ 4")]
        Wednesday = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thứ 5")]
        Thursday = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thứ 6")]
        Friday = 5,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thứ 7")]
        Saturday = 6,
        [DevExpress.ExpressApp.DC.XafDisplayName("")]
        KhongCoGiaTri = 7
    }
}
