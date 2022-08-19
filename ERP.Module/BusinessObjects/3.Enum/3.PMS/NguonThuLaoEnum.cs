using System;
using System.Collections.Generic;

//
namespace ERP.Module.Enum.PMS
{
    public enum NguonThuLaoEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Giảng dạy")]
        GiangDay = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("CVHT")]
        CVHT = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("NCKH")]
        NCKH = 2
    }
}
