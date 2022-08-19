using System;
using DevExpress.Xpo;

namespace ERP.Module.Enum.Systems
{
    public enum LoaiChucNangEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Thư mục quản lý")]
        ThuMucQuanLy = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chức năng")]
        ChucNang = 2
    }
}
