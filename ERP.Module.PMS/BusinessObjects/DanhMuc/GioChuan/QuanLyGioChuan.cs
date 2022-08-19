using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.PMS.BusinessObjects;

namespace ERP.Module.PMS.GioChuan
{
    //[DefaultProperty("Caption")]
    //[ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Quản lý giờ chuẩn")]
     public class QuanLyGioChuan : ThongTinChungPMS
    {
        private decimal _GioChuanGiangDay_MacDinh;
        private decimal _GioChuanNCKH_MacDinh;
        private decimal _GioChuanKhac_MacDinh;


        [ModelDefault("Caption", "Giờ giảng dạy(Mặc định)")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        [VisibleInListView(false)]
        public decimal GioChuanGiangDay_MacDinh
        {
            get { return _GioChuanGiangDay_MacDinh; }
            set { SetPropertyValue("GioChuanGiangDay_MacDinh", ref _GioChuanGiangDay_MacDinh, value); }
        }

        [ModelDefault("Caption", "Giờ NCKH(Mặc định)")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        [VisibleInListView(false)]
        public decimal GioChuanNCKH_MacDinh
        {
            get { return _GioChuanNCKH_MacDinh; }
            set { SetPropertyValue("GioChuanNCKH_MacDinh", ref _GioChuanNCKH_MacDinh, value); }
        }
        [ModelDefault("Caption", "Giờ khác(Mặc định)")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        [VisibleInListView(false)]
        public decimal GioChuanKhac_MacDinh
        {
            get { return _GioChuanKhac_MacDinh; }
            set { SetPropertyValue("GioChuanKhac_MacDinh", ref _GioChuanKhac_MacDinh, value); }
        }

        [Aggregated]
        [Association("QuanLyGioChuan-ListDinhMucChucVu")]
        [ModelDefault("Caption", "Định mức chức vụ")]
        public XPCollection<DinhMucChucVu> ListDinhMucChucVu
        {
            get
            {
                return GetCollection<DinhMucChucVu>("ListDinhMucChucVu");
            }
        }//dinhmucgiochuan

        [Aggregated]
        [Browsable(false)]
        [Association("QuanLyGioChuan-ListDinhMuc_NghienCuuKhoaHoc")]
        [ModelDefault("Caption", "Định mức NCKH")]
        public XPCollection<DinhMuc_NghienCuuKhoaHoc> ListDinhMuc_NghienCuuKhoaHoc
        {
            get
            {
                return GetCollection<DinhMuc_NghienCuuKhoaHoc>("ListDinhMuc_NghienCuuKhoaHoc");
            }
        }//dinhmucgiochuan

        [Aggregated]
        [Association("QuanLyGioChuan-ListDinhMucChucVuNhanVien")]
        [ModelDefault("Caption", "Định mức chức vụ nhân viên")]
        public XPCollection<DinhMucChucVu_NhanVien> ListDinhMucChucVuNhanVien
        {
            get
            {
                return GetCollection<DinhMucChucVu_NhanVien>("ListDinhMucChucVuNhanVien");
            }
        }//dinhmucgiochuan

        //[Aggregated]
        //[Association("QuanLyGioChuan-ListDinhMucGiamTruNhanVien")]
        //[ModelDefault("Caption", "Định mức giảm trừ nhân viên")]
        //public XPCollection<DinhMucGiamTru_NhanVien> ListDinhMucGiamTruNhanVien
        //{
        //    get
        //    {
        //        return GetCollection<DinhMucGiamTru_NhanVien>("ListDinhMucGiamTruNhanVien");
        //    }
        //}
        public QuanLyGioChuan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction(); 
        }
    }
}