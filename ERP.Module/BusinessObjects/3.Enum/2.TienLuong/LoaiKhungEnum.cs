using System;
using System.Collections.Generic;

//
namespace ERP.Module.Enum.TienLuong
{
    public enum LoaiKhungEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ giữa giờ")]
        NghiGiuaGio = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Không nghỉ giữa giờ")]
        KhongNghiGiuaGio = 1
    }
}
