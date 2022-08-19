using System;
using DevExpress.Xpo;

namespace ERP.Module.Enum.NhanSu
{
    public enum LoaiVanBangChungChiEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Văn bằng")]
        VanBang = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chứng chỉ")]
        ChungChi = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Khác")]
        Khac = 3      
    }
}
