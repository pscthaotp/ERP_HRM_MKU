using System;
using System.Collections.Generic;

namespace ERP.Module.Enum.NhanSu
{
    public enum DanhGiaEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Không đánh giá")]
        Khong = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Xuất sắc")]
        XuatSac = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giỏi")]
        Gioi = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Khá")]
        Kha = 3
    }
}
