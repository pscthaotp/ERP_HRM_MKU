using System;
using DevExpress.Xpo;

namespace ERP.Module.Enum.NhanSu
{
    public enum LoaiBoPhanEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Trường;Công ty")]
        CongTy = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Khối")]
        Khoi = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đơn vị; Phòng ban; Khoa")]
        PhongBan = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Bộ môn/Tổ")]
        BoMon = 3
    }
}
