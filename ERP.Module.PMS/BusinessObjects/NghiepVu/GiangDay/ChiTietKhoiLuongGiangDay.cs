using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.PMS.BusinessObjects;


namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Chi tiết khối lượng giảng dạy")]
    [DefaultProperty("ThongTin")]
    [Appearance("Hide_TinhThuLao", TargetItems = "TinhThuLao", Visibility = ViewItemVisibility.Hide, Criteria = "KhoiLuongGiangDay != null")]
    public class ChiTietKhoiLuongGiangDay : ThongTinKhoiLuongGiangDay
    {
        
        #region key
        private KhoiLuongGiangDay _KhoiLuongGiangDay;
        [Association("KhoiLuongGiangDay-ListChiTiet")]
        [ModelDefault("Caption", "Khối lượng giảng dạy")]
        [Browsable(false)]
        public KhoiLuongGiangDay KhoiLuongGiangDay
        {
            get
            {
                return _KhoiLuongGiangDay;
            }
            set
            {
                SetPropertyValue("KhoiLuongGiangDay", ref _KhoiLuongGiangDay, value);
            }
        }
        private KhoiLuongGiangDay_ThinhGiang _KhoiLuongGiangDay_ThinhGiang;
        [Association("KhoiLuongGiangDay_ThinhGiang-ListChiTietKhoiLuongGiangDay_ThinhGiang")]
        [ModelDefault("Caption", "Khối lượng giảng dạy(thỉnh giảng)")]
        [Browsable(false)]
        public KhoiLuongGiangDay_ThinhGiang KhoiLuongGiangDay_ThinhGiang
        {
            get
            {
                return _KhoiLuongGiangDay_ThinhGiang;
            }
            set
            {
                SetPropertyValue("KhoiLuongGiangDay_ThinhGiang", ref _KhoiLuongGiangDay_ThinhGiang, value);
            }
        }
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        [VisibleInDetailView(false)]
        public String ThongTin
        {
            get
            {
                if (this.NhanVien != null && !string.IsNullOrEmpty(this.TenMonHoc))
                {
                    return String.Format("{0}", this != null ? this.NhanVien.MaNhanVien + " - " + this.NhanVien.HoTen + " - " + this.TenMonHoc : "");
                }
                else
                    return "";
                
            }
        }
        #endregion

        #region Khai báo
        private ChiTietThanhTra _ChiTietThanhTra;
        private bool _TinhThuLao;
        #endregion

        [ModelDefault("Caption", "Thanh tra")]
        [ModelDefault("AllowEdit","False")]
        public ChiTietThanhTra ChiTietThanhTra
        {
            get { return _ChiTietThanhTra; }
            set { SetPropertyValue("ChiTietThanhTra", ref _ChiTietThanhTra, value); }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Tính thù lao")]
        public bool TinhThuLao
        {
            get { return _TinhThuLao; }
            set { SetPropertyValue("TinhThuLao", ref _TinhThuLao, value); }
        }
        public ChiTietKhoiLuongGiangDay(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
       }
    }

}
