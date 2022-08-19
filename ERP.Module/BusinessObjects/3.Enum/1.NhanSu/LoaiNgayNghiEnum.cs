using System;
using DevExpress.Xpo;

namespace ERP.Module.Enum.NhanSu
{
    public enum LoaiNgayNghiEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ lễ, tết")]
        NghiLe = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ bù lễ, tết")]
        NghiBu = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ chế độ công ty")]
        NghiCheDo = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghỉ cuối tuần")]
        NghiCuoiTuan = 3,             
        [DevExpress.ExpressApp.DC.XafDisplayName("Ngày thường")]
        NgayThuong = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Ngày nghỉ đột xuất")]
        NgayNghiDotXuat =5
    }
}
