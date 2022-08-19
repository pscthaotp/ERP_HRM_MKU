using System;
using DevExpress.Xpo;

namespace ERP.Module.Enum.NhanSu
{
    public enum HinhThucThiEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Phỏng vấn")]
        PhongVan = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Làm bài thi")]
        LamBaiThi = 2
    }
}
