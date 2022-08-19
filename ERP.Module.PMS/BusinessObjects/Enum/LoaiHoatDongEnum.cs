using System;
using DevExpress.Xpo;

namespace ERP.Module.PMS.Enum
{
    public enum LoaiHoatDongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Đào tạo chính quy")]
        DaoTaoChinhQuy = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Bồi dưỡng thường xuyên")]
        BoiDuongThuongXuyen = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("HĐ chấm bài")]
        ChamBai = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đào tạo sau Đại Học")]
        SauDaiHoc = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tạp chí - Bài báo")]
        TapChiBaiBao = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Các HĐ khác")]
        CacHoatDongKhac = 5,
        [DevExpress.ExpressApp.DC.XafDisplayName("Kê khai sau giảng")]
        KeKhaiSauGiang =6,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghiên cứu khoa học")]
        NghienCuuKhoaHoc = 7,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giảng dạy")]
        GiangDay = 8,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nghiên cứu khoa học(đề cương chi tiết)")]
        NghienCuuKhoaHoc_DCCT = 9,
        [DevExpress.ExpressApp.DC.XafDisplayName("HĐ Ra đề")]
        RaDe = 10,
        [DevExpress.ExpressApp.DC.XafDisplayName("HĐ Coi thi")]
        CoiThi = 11,
    }
}
