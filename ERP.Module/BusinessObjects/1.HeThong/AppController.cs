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
    [ModelDefault("Caption", "Nút thao tác")]
    public class AppController : BaseObject
    {
        private string _Key;
        private string _Caption;
        private string _AppObject;
        private string _ObjectCaption;

        [Browsable(false)]
        public string Key
        {
            get
            {
                return _Key;
            }
            set
            {
                SetPropertyValue("Key", ref _Key, value);
            }
        }

        [Browsable(false)]
        public string AppObject
        {
            get
            {
                return _AppObject;
            }
            set
            {
                SetPropertyValue("AppObject", ref _AppObject, value);
            }
        }

        [ModelDefault("Caption", "Tên nút thao tác")]
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

        [ModelDefault("Caption", "Đối tượng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string ObjectCaption
        {
            get
            {
                return _ObjectCaption;
            }
            set
            {
                SetPropertyValue("ObjectCaption", ref _ObjectCaption, value);
            }
        }


        public AppController(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
