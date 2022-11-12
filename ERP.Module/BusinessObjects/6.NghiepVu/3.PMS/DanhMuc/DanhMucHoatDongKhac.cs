using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.PMS.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_Category")]
    [DefaultProperty("TenLoaiHoatDong")]
    [ModelDefault("Caption", "Loại hoạt động")]
    [RuleCombinationOfPropertiesIsUnique("Mã quản lý, Tên loại hoạt động bị trùng", DefaultContexts.Save, "MaQuanLy;TenLoaiHoatDong")]
    public class DanhMucHoatDongKhac : BaseObject
    {
        private string _MaQuanLy;
        private string _TenLoaiHoatDong;
            
        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }
        
        [ModelDefault("Caption", "Tên loại hoạt động")]
        public string TenLoaiHoatDong
        {
            get
            {
                return _TenLoaiHoatDong;
            }
            set
            {
                SetPropertyValue("TenLoaiHoatDong", ref _TenLoaiHoatDong, value);
            }
        }
        
        public DanhMucHoatDongKhac(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
