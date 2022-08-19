using System;

namespace ERP.Module.Enum.NhanSu
{
    public enum TrangThaiThamGiaBaoHiemEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Đang tham gia")]
        DangThamGia = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giảm tạm thời")]
        GiamTamThoi = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giảm hẳn")]
        GiamHan = 2
    }
}
