using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using ERP.Module.HeThong;

namespace ERP.Module.Win.Controllers.HeThong
{
    public partial class HeThong_SetupDetailViewNonpersistentController : ViewController
    {
        private ListViewProcessCurrentObjectController processCurrentObjectController;

        public HeThong_SetupDetailViewNonpersistentController()
        {
            TargetObjectType = typeof(INonpersistentController);
        }

        private void processCurrentObjectController_CustomProcessSelectedItem(object sender, CustomizeShowViewParametersEventArgs e)
        {
            DetailView view = Application.CreateDetailView(ObjectSpace, View.CurrentObject, false);
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            e.ShowViewParameters.CreatedView = view;
            //
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            //
            processCurrentObjectController =Frame.GetController<ListViewProcessCurrentObjectController>();
            if (processCurrentObjectController != null)
            {
                processCurrentObjectController.CustomizeShowViewParameters += processCurrentObjectController_CustomProcessSelectedItem;
            }
        }

        protected override void OnDeactivated()
        {
            if (processCurrentObjectController != null)
            {
                processCurrentObjectController.CustomizeShowViewParameters -= processCurrentObjectController_CustomProcessSelectedItem;
                TargetObjectType = null;
            }
            base.OnDeactivated();
        }
    }
}
