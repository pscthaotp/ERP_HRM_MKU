using System;
using System.Collections.Generic;

//
namespace ERP.Module.Enum.TienLuong
{
    public enum LoaiNhomPhanBoEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Tổng công ty")]
        TongCongTy = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Cao đẳng-Đại học")]
        CaoDangDaiHoc = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Phổ thông")]
        PhoThong = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Mầm non")]
        MamNon = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Ngành")]
        Nganh = 5,
        [DevExpress.ExpressApp.DC.XafDisplayName("Khác")]
        Khac = 6,
    }
}
