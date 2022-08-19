using DevExpress.ExpressApp;
using ERP.Module.HeThong;
using System;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.HeThong
{
    partial class HeThong_CopyRoleUserController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();
            this.popupWindowShowAction1 = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popupWindowShowAction1
            // 
            this.popupWindowShowAction1.Caption = "Sao chép quyền";
            this.popupWindowShowAction1.ToolTip = "Sao chép quyền cho tài khoản khác";
            this.popupWindowShowAction1.ConfirmationMessage = null;
            this.popupWindowShowAction1.Id = "HeThong_CopyRoleUserController";
            this.popupWindowShowAction1.ImageName = "Action_UpdateProcess";
            this.popupWindowShowAction1.TargetViewType = ViewType.DetailView;
            this.popupWindowShowAction1.TargetViewNesting = Nesting.Root;
            this.popupWindowShowAction1.Category = "Edit";
            this.popupWindowShowAction1.TargetObjectType = typeof(SecuritySystemUser_Custom);
            this.popupWindowShowAction1.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowAction1_CustomizePopupWindowParams);
            this.popupWindowShowAction1.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowAction1_Execute);

            this.Activated += new EventHandler(this.HeThong_CopyRoleUserController_Activated);
        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowAction1;
    }
}
