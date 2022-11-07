using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.NghiepVu.NhanSu.NhanViens;

namespace ERP.Module.NghiepVu.PMS.NCKH
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Bảo lưu NCKH")]

    public class BaoLuuNCKH : BaseObject
    {
        private QuanLyNCKH _QuanLyNCKH; //
        private NhanVien _NhanVien;//
        private decimal _GioQuyBaoLuu;
        private decimal _GioQuySuDung;

        [ModelDefault("Caption", "Quản lý NCKH")]
        [Association("QuanLyNCKH-ListBaoLuuNCKH")]
        [Browsable(false)]
        public QuanLyNCKH QuanLyNCKH
        {
            get { return _QuanLyNCKH; }
            set
            {
                SetPropertyValue("QuanLyNCKH", ref _QuanLyNCKH, value);
            }
        }


        [ModelDefault("Caption", "Giảng viên")]
        [ModelDefault("AllowEdit", "false")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }       

        [ModelDefault("Caption", "Giờ bảo lưu")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioQuyBaoLuu
        {
            get { return _GioQuyBaoLuu; }
            set { SetPropertyValue("GioQuyBaoLuu", ref _GioQuyBaoLuu, value); }
        }

        [ModelDefault("Caption", "Giờ bảo lưu đã sử dụng")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal GioQuySuDung
        {
            get { return _GioQuySuDung; }
            set { SetPropertyValue("GioQuySuDung", ref _GioQuySuDung, value); }
        }

        public BaoLuuNCKH(Session session)
            : base(session)
        {
        }


    }
}