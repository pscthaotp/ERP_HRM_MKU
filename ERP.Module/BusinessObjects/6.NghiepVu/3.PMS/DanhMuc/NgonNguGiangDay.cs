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
    [DefaultProperty("TenNgonNgu")]
    [ModelDefault("Caption", "Ngôn ngữ dạy")]
    [RuleCombinationOfPropertiesIsUnique("Mã quản lý, Ngôn ngữ bị trùng", DefaultContexts.Save, "MaQuanLy;TenNgonNgu")]
    public class NgonNguGiangDay : BaseObject
    {
        private string _MaQuanLy;
        private string _TenNgonNgu;
       
      
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
        
        [ModelDefault("Caption", "Tên ngôn ngữ")]
        public string TenNgonNgu
        {
            get
            {
                return _TenNgonNgu;
            }
            set
            {
                SetPropertyValue("TenNgonNgu", ref _TenNgonNgu, value);
            }
        }
        
        public NgonNguGiangDay(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
