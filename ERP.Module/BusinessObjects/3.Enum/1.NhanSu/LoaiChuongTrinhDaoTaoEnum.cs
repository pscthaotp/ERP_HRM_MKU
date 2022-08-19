using System;
using DevExpress.Xpo;

namespace ERP.Module.Enum.NhanSu
{
    public enum LoaiChuongTrinhDaoTaoEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Dài hạn")]
        DaiHan = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Ngắn hạn")]
        NganHan = 2,
      
    }
}
