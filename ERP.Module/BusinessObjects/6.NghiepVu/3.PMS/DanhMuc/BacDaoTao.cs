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
    [DefaultProperty("TenBacDaoTao")]
    [ModelDefault("Caption", "Bậc đào tạo")]
    [RuleCombinationOfPropertiesIsUnique("Mã quản lý, Tên bậc đào tạo bị trùng", DefaultContexts.Save, "MaQuanLy;TenBacDaoTao")]
    public class BacDaoTao : BaseObject
    {
        private string _MaQuanLy;
        private string _TenBacDaoTao;
       
      
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
        
        [ModelDefault("Caption", "Tên bậc đào tạo")]
        public string TenBacDaoTao
        {
            get
            {
                return _TenBacDaoTao;
            }
            set
            {
                SetPropertyValue("TenBacDaoTao", ref _TenBacDaoTao, value);
            }
        }
        
        public BacDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
