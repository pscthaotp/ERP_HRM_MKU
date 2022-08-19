using System;
using System.Collections.Generic;

//
namespace ERP.Module.Enum.TienLuong
{
    public enum LoaiChuHoaHayChuThuong : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Form chữ hoa")]
        ChuHoa = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Form chữ thường")]
        ChuThuong = 2
    }
}
