using System;

namespace ERP.Module.Enum.NhanSu
{
    public enum LoaiQuaTrinhEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Lịch sử bản thân")]
        LichSuBanThan = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Diễn biến lương")]
        DienBienLuong = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Quá trình bổ nhiệm")]
        BoNhiem = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Quá trình công tác")]
        CongTac = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Quá trình đào tạo")]
        DaoTao = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Quá trình bồi dưỡng")]
        BoiDuong = 5,
        [DevExpress.ExpressApp.DC.XafDisplayName("Quá trình khen thưởng")]
        KhenThuong = 6,
        [DevExpress.ExpressApp.DC.XafDisplayName("Quá trình kỷ luật")]
        KyLuat = 7,
        [DevExpress.ExpressApp.DC.XafDisplayName("Quá trình điều động")]
        DieuDong = 8,
        [DevExpress.ExpressApp.DC.XafDisplayName("Quá trình bổ nhiệm kiêm nhiệm")]
        BoNhiemKiemNhiem = 9
    }
}
