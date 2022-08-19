﻿using System;
using DevExpress.Xpo;

namespace ERP.Module.PMS.Enum
{
    public enum ChamHuongDanEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Chấm 1")]
        ChamMot = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chấm 2")]
        ChamHai = 1
    }
}
