using System;
using System.Collections.Generic;

//
namespace ERP.Module.Enum.PMS
{
    public enum LoaiGiangVienEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Cơ hữu")]
        CoHuu = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thỉnh giảng")]
        ThinhGiang = 1,
        //[DevExpress.ExpressApp.DC.XafDisplayName("Tất cả")]
        //TatCa = 2,
        //[DevExpress.ExpressApp.DC.XafDisplayName("Nhân viên cán bộ văn phòng")]
        //NhanVienVanPhong = 3
    }
}
