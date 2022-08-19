using System;
using DevExpress.Xpo;

namespace ERP.Module.Enum.NhanSu
{
    public enum TinhTrangEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Còn sống")]
        ConSong = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đã mất")]
        DaMat = 1
    }
}
