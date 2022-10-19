using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using ERP.Module.NghiepVu.PMS.QuanLyGiangDay;

namespace ERP.Module.NghiepVu.PMS.CauHinh
{
    [ImageName("BO_BangLuong")]
    [ModelDefault("Caption", "Chọn giá trị PMS")]
    [NonPersistent]

    public class ChonGiaTriLapCongThucPMS : BaseObject
    {
        //Chỉ dùng để lập công thức
        //======================================================

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Chi tiết khối lượng giảng dạy")]
        public ChiTietKhoiLuongGiangDay ChiTietKhoiLuongGiangDay { get; set; }


        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Thông tin khối lượng giảng dạy")]
        public ThongTinKhoiLuongGiangDay ThongTinKhoiLuongGiangDay { get; set; }

        
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Cấu hình quy đổi PMS")]
        public CauHinhQuyDoiPMS CauHinhQuyDoiPMS { get; set; }

        public ChonGiaTriLapCongThucPMS(Session session)
            : base(session)
        {
        }


    }
}