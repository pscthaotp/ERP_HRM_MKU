using System;
using System.Collections.Generic;

namespace ERP.Module.Enum.NhanSu
{
    public enum LyDoNghiEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Thôi việc, nghỉ hưu")]
        ThoiViecNghiHuu = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ thai sản")]
        ThaiSan = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ ốm")]
        OmDau = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ không hưởng lương")]
        NghiKhongLuong = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thuyên chuyển công tác")]
        ThuyenChuyen = 4
    }
}
