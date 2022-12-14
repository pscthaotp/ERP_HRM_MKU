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
    [DefaultProperty("TenHeDaoTao")]
    [ModelDefault("Caption", "Hệ đào tạo")]
    [RuleCombinationOfPropertiesIsUnique("Mã quản lý, Tên hệ đào tạo bị trùng", DefaultContexts.Save, "MaQuanLy;TenHeDaoTao")]
    public class HeDaoTao : BaseObject
    {
        private string _MaQuanLy;
        private string _TenHeDaoTao;
       
      
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
        
        [ModelDefault("Caption", "Tên hệ đào tạo")]
        public string TenHeDaoTao
        {
            get
            {
                return _TenHeDaoTao;
            }
            set
            {
                SetPropertyValue("TenHeDaoTao", ref _TenHeDaoTao, value);
            }
        }
        
        public HeDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
