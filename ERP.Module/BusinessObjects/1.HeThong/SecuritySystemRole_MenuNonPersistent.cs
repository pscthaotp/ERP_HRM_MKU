using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Xpo.Metadata;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.BaseImpl;

namespace ERP.Module.HeThong
{
    [ImageName("BO_Category")]
    [DefaultProperty("AppMenu")]
    [ModelDefault("Caption", "Chức năng phụ")]
    [RuleCombinationOfPropertiesIsUnique("SecuritySystemRole_MenuNonPersistent", DefaultContexts.Save, "SecuritySystemRole;AppMenu")]
    public class SecuritySystemRole_MenuNonPersistent : BaseObject
    {
        private SecuritySystemRole_Custom _SecuritySystemRole;
        private AppMenu _AppMenu;


        [Browsable(false)]
        [ModelDefault("Caption", "Phân quyền chức năng")]
        [Association("SecuritySystemRole_MenuNonPersistent-ListMenuNonPersistent")]
        public SecuritySystemRole_Custom SecuritySystemRole
        {
            get
            {
                return _SecuritySystemRole;
            }
            set
            {
                SetPropertyValue("SecuritySystemRole", ref _SecuritySystemRole, value);
            }
        }

        [ModelDefault("Caption", "Tên chức năng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("LoaiObject = 1")]
        public AppMenu AppMenu
        {
            get
            {
                return _AppMenu;
            }
            set
            {
                SetPropertyValue("AppMenu", ref _AppMenu, value);
            }
        }

        public SecuritySystemRole_MenuNonPersistent(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
