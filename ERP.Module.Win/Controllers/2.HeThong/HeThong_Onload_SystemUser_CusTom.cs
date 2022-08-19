using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using ERP.Module.HeThong;

namespace ERP.Module.Win.Controllers.NghiepVu.HeThong

{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class HeThong_Onload_SystemUser_CusTom : ViewController
    {
        public HeThong_Onload_SystemUser_CusTom()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(SecuritySystemUser_Custom);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.

            SecuritySystemUser_Custom obj = View.CurrentObject as SecuritySystemUser_Custom;
            if (obj != null)
            {
                obj.onload();
            }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
