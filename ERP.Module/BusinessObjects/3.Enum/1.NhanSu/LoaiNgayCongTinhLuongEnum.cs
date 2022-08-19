using System;

namespace ERP.Module.Enum.NhanSu
{
    public enum LoaiNgayCongTinhLuongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Không trừ ngày công")]
        KhongTruCong = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Trừ ngày công hưởng lương")]
        TruNgayCongHuongLuong = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Trừ công thực tế")]
        TruNgayCongThucTe = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Trừ công thực tế (chỉ tính nguyên ngày)")]
        TruNgayCongThucTeNguyenNgay = 3       
    }
}
