using System;
using DevExpress.Xpo;

namespace ERP.Module.Enum.NhanSu
{
    public enum LyDoChamDutHopDongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Theo nguyện vọng")]
        TheoNguyenVong = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Luân chuyển nội bộ")]
        LuanChuyen = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lý do khác")]
        Khac = 2,
    }
}
