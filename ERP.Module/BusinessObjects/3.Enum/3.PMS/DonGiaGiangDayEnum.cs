using System;
using System.Collections.Generic;

//
namespace ERP.Module.Enum.PMS
{
    public enum DonGiaGiangDayEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Đơn giá vượt định mức")]
        DonGiaVuot = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đơn giá trong định mức")]
        DonGiaTrong = 2,
    }
}
