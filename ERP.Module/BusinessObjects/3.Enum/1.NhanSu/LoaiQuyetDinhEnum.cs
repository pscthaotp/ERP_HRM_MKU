using System;

namespace ERP.Module.Enum.NhanSu
{
    public enum LoaiQuyetDinhEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Hiệu trưởng ký")]
        HieuTruong = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hội đồng quản trị ký")]
        HoiDongQuanTri = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tổng giám đốc ký")]
        TongGiamDoc = 3
    }
}
