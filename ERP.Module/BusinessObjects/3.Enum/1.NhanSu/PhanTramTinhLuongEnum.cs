using System;

namespace ERP.Module.Enum.NhanSu
{
    public enum PhanTramTinhLuongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("80%")]
        TamMuoiPhanTram = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("85%")]
        TamNamPhanTram = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("90%")]
        ChinhMuoiPhanTram = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("95%")]
        ChinhNamPhanTram = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("100%")]
        MotTramPhanTram = 5,
    }
}
