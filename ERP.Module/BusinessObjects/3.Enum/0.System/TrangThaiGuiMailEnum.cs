using System;

namespace ERP.Module.Enum.Systems
{
    public enum TrangThaiGuiMailEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Đã gửi mail tư vấn tuyển sinh")]
        DaGuiMailTuVanTuyenSinh = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đã gửi mail thông báo nhập học")]
        DaGuiMailThongBaoNhapHoc = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đã gửi mail chăm sóc khách hàng")]
        DaGuiMailChamSocKhachHang = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đã gửi mail Tổ chức sự kiện")]
        DaGuiMailToChucSuKien = 4
    }
}
