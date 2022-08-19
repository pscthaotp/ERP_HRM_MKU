using System;
using System.Collections.Generic;

//
namespace ERP.Module.Enum.TienLuong
{
    public enum LoaiCongKhacEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Giữ trẻ ngày thứ 7")]
        GiuTreThuBay  = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giữ trẻ ngoài giờ")]
        GiuTreNgoaiGio = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lớp phát triển ngôn ngữ")]
        LopPhatTrienNgonNgu = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chương trình tiếng anh")]
        ChuongTrinhTiengAnh = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Ngoại khóa Gokid")]
        NgoaiKhoaGokid = 5,
        [DevExpress.ExpressApp.DC.XafDisplayName("Công quản lý ngoài giờ")]
        CongQuanLyNgoaiGio = 6
    }
}
