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
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Đối tượng")]
    public class AppObject : BaseObject
    {
        private string _KeyObject;
        private string _Caption;

        [ModelDefault("Caption", "Key")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string KeyObject
        {
            get
            {
                return _KeyObject;
            }
            set
            {
                SetPropertyValue("KeyObject", ref _KeyObject, value);
            }
        }

        [ModelDefault("Caption", "Caption")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Caption
        {
            get
            {
                return _Caption;
            }
            set
            {
                SetPropertyValue("Caption", ref _Caption, value);
            }
        }

        public AppObject(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
