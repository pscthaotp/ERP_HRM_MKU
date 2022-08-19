using System;

namespace ERP.Module.Enum.NhanSu
{
    public enum LoaiCongThucLuongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Công thức lương và phụ cấp")]
        CongThucLuongVaPhuCap = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Công thức lương ngoài giờ")]
        CongThucLuongNgoaiGio = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Công thức lương phép hè")]
        CongThucLuongPhepHe = 3
    }
}
