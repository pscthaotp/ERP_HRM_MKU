using System;

namespace ERP.Module.Enum.NhanSu
{
    public enum LoaiQuyetDinhVaHopDongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Bổ nhiệm")]
        BoNhiem = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tái bổ nhiệm")]
        TaiBoNhiem = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Miễn nhiệm / Thôi nhiệm")]
        MienNhiem = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Luân chuyển")]
        LuanChuyen = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Điều động")]
        DieuDong = 5,
        [DevExpress.ExpressApp.DC.XafDisplayName("Điều chỉnh tiền lương")]
        DieuChinhTienLuong = 6,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chấm dứt hợp đồng lao động")]
        ChamDutHDLD = 7,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tạm hoãn hợp đồng lao động")]
        TamHoanHDLD = 8,
        [DevExpress.ExpressApp.DC.XafDisplayName("Ứng cử nội bộ")]
        UngCuNoiBo = 9,
        [DevExpress.ExpressApp.DC.XafDisplayName("Khác")]
        LoaiKhac = 10,
    }
}
