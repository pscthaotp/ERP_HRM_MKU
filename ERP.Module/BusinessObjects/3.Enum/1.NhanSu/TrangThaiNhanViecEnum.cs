using System;

namespace ERP.Module.Enum.NhanSu
{
    public enum TrangThaiNhanViecEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Đang chờ phản hồi")]
        ChoPhanHoi = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nhận việc")]
        NhanViec = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Không nhận việc")]
        KhongNhanViec = 2
    }
}
