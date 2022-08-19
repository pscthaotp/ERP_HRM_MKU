using System;
using System.Collections.Generic;

//
namespace ERP.Module.Enum.TienLuong
{
    public enum LoaiChiEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Lương - phụ cấp")]
        LuongVaPhuCap,
        [DevExpress.ExpressApp.DC.XafDisplayName("Ngoài giờ")]
        NgoaiGio,
        [DevExpress.ExpressApp.DC.XafDisplayName("Khấu trừ lương")]
        KhauTruLuong,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thu nhập khác")]
        ThuNhapKhac,
        [DevExpress.ExpressApp.DC.XafDisplayName("Khen thưởng - phúc lợi")]
        KhenThuong
    }
}
