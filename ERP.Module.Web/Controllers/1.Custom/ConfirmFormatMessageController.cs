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
using ERP.Module.NghiepVu.TuyenSinh_TP;
using DevExpress.Web;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.Commons;

namespace ERP.Module.Web.Controllers.Custom
{
    public partial class ConfirmFormatMessageController : ObjectViewController
    {
        private ModificationsController controller;
        protected override void OnActivated()
        {
            base.OnActivated();
            var congTy = Common.SecuritySystemUser_GetCurrentUser().CongTy;
            if (congTy != null && congTy.Oid.Equals(Config.KeyTanPhu))
            {
                if (View.Id.Equals("ThongTinKhachHang_DetailView"))
                {
                    // Perform various tasks depending on the target View.
                    controller = Frame.GetController<ModificationsController>();
                    SetConfirmationMessage(View.CurrentObject as ThongTinKhachHang);
                    View.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
                }
                else
                    controller = null;
            }
        }
        void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            if (e.Object is ThongTinKhachHang)
            {
                SetConfirmationMessage((ThongTinKhachHang)e.Object);
            }
        }


        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.

            base.OnDeactivated();

            RemoveConfirmationMessage();
            View.ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged;
        }
        public void SetConfirmationMessage(ThongTinKhachHang theProject)
        {
            bool yourBusinessCondition = theProject != null;
            string message = yourBusinessCondition ? string.Format("Bạn có chắc chắn muốn lưu dữ liệu.") : null;
            if (controller != null)
            {
                controller.SaveAndCloseAction.ConfirmationMessage = message;
                controller.SaveAction.ConfirmationMessage = message;
                controller.SaveAndNewAction.ConfirmationMessage = message;
            }
        }
        public void RemoveConfirmationMessage()
        {
            string message = null;
            if (controller != null)
            {
                controller.SaveAndCloseAction.ConfirmationMessage = message;
                controller.SaveAction.ConfirmationMessage = message;
                controller.SaveAndNewAction.ConfirmationMessage = message;
            }
        }
    }
}
