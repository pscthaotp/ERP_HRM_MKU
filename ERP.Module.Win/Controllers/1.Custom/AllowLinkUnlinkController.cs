using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp;
using ERP.Module.HeThong;

namespace ERP.Module.Win.Controllers.Custom
{
    public class AllowLinkUnlinkController : LinkUnlinkController
    {
        protected override DevExpress.ExpressApp.View CreateLinkView()
        {
            if (View.Id == "SecuritySystemRole_Users_ListView")
            {
                IObjectSpace _obs = Application.CreateObjectSpace();
                CollectionSource collectionSource = new CollectionSource(_obs, typeof(SecuritySystemUser_Custom));
                //
                return Application.CreateListView("SecuritySystemUser_Custom_ListView", collectionSource, true);
            }
            else
                return base.CreateLinkView();
        }

        protected override void UpdateActionsState()
        {
            /* Update link/unlink actions */

            LinkAction.BeginUpdate();
            UnlinkAction.BeginUpdate();
            try
            {
                /* Inherited */
                base.UpdateActionsState();

                // Exit when no view availlable
                if (View == null)
                    return;
                //
                //LinkAction.Active["ViewAllowNew"] = false;
                //UnlinkAction.Active["ViewAllowDelete"] = false;
            }
            finally
            {
                LinkAction.EndUpdate();
                UnlinkAction.EndUpdate();
            }
        }
    }
}
