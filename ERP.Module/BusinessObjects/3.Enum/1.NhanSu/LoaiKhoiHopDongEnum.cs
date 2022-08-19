using System;

namespace ERP.Module.Enum.NhanSu
{
    public enum LoaiKhoiHopDongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Khối hành chính")]
        KhoiHanhChinh = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Khối giảng viên")]
        KhoiGiangVien = 2
    }
}
