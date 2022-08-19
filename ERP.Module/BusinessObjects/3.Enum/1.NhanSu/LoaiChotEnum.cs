using System;

namespace ERP.Module.Enum.NhanSu
{
    public enum LoaiChotEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Chuyển dữ liệu đợt 1 sang đợt 2")]
        CopyDot1_Dot2 = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lấy dữ liệu mới nhất trong hồ sơ")]
        GetNewData = 3
    }
}
