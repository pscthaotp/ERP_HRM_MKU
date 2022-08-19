using System;
using DevExpress.Xpo;

namespace ERP.Module.Enum.Systems
{
    public enum LoaiOfficeEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Office 2003")]
        Office2003 = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Office 2010")]
        Office2010 = 2
    }
}
