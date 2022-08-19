using System;
using DevExpress.Xpo;

namespace ERP.Module.Enum.ThongBao
{
    public enum DanhMucThongBaoEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Thông báo học sinh")]
        ThongBaoHocSinh = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thông báo nhân sự")]
        ThongBaoNhanSu = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thông báo tuyển sinh")]
        ThongBaoTuyenSinh = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thông báo học vụ")]
        ThongBaoHocVu = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Thông báo tiếng anh")]
        ThongBaoTiengAnh = 4
    }
}
