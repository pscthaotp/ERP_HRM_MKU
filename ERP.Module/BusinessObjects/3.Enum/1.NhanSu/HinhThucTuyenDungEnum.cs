using System;
using DevExpress.Xpo;

namespace ERP.Module.Enum.NhanSu
{
    public enum HinhThucTuyenDungEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Xét tuyển")]
        XetTuyen = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thi tuyển")]
        ThiTuyen = 1
    }
}
