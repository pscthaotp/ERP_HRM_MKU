using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using ERP.Module.HeThong;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;

namespace ERP.Module.Win.Controllers._2.HeThong
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class HeThong_Onload_Controller : ViewController
    {

        IObjectSpace _obs = null;
        Session _Session;
        public HeThong_Onload_Controller()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.

            TargetViewId = "AppMenu_DetailView";
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
            _obs = Application.CreateObjectSpace();
            _Session = ((XPObjectSpace)_obs).Session;
            DetailView _DetailView = View as DetailView;
            if (_DetailView != null)
            {
                if (_DetailView.ToString().Contains("AppMenu_DetailView"))
                {
                    AppMenu app = View.CurrentObject as AppMenu;
                    if (app != null)
                    {
                        app.UpdateAppMenuParentList();
                    }
                }
            }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
