using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;

namespace ERP.Module.NonPersistentObjects.HeThong
{
    [NonPersistent]
    [DefaultProperty("TenCongTy")]
    public class CongTyView : BaseObject
    {
        private string _TenCongTy;
        private Guid _OidCongTy;

        [Browsable(false)]
        public Guid OidCongTy
        {
            get
            {
                return _OidCongTy;
            }
            set
            {
                SetPropertyValue("OidCongTy", ref _OidCongTy, value);
            }
        }
        [ModelDefault("Caption", "Trường")]
        public string TenCongTy
        {
            get
            {
                return _TenCongTy;
            }
            set
            {
                SetPropertyValue("TenCongTy", ref _TenCongTy, value);
            }
        }

        public CongTyView(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}