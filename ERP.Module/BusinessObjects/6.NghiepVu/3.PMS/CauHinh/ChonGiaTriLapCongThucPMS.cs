using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.Data.Filtering;
using ERP.Module.Enum.PMS;
using ERP.Module.NghiepVu.PMS.QuanLy;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.PMS.DanhMuc;
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