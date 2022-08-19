using System;
using DevExpress.Xpo;

namespace ERP.Module.Enum.Systems
{
    public enum LoaiTaiKhoanEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Quản trị Hệ thống")]
        QuanTriHeThong = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Quản trị Khối")]
        QuanTriKhoi = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Quản trị Trường")]
        QuanTriCongTy = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tài khoản Bình thường")]
        TaiKhoanBinhThuong = 4
    }
}
