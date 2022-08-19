using System;

namespace ERP.Module.Enum.NhanSu
{
    public enum LoaiChamCongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Công hành chính")]
        CongHanhChinh = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Công quản nhiệm")]
        CongQuanNhiem = 2
    }
}
