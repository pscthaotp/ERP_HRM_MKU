using ERP.Module.BaoCao.Custom;
using System;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.BaoCao
{
    partial class Custom_CreateReportController
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
            this.popupWindowShowAction1.AcceptButtonCaption = null;
            this.popupWindowShowAction1.CancelButtonCaption = null;
            this.popupWindowShowAction1.Caption = "Thêm mới";
            this.popupWindowShowAction1.Category = "ObjectsCreation";
            this.popupWindowShowAction1.ConfirmationMessage = null;
            this.popupWindowShowAction1.Id = "CustomCreateReportController";
            this.popupWindowShowAction1.ImageName = "Action_New";
            this.popupWindowShowAction1.Shortcut = null;
            this.popupWindowShowAction1.Tag = null;
            this.popupWindowShowAction1.TargetObjectsCriteria = null;
            //this.popupWindowShowAction1.TargetObjectType = typeof(ReportData_Custom);
            //this.popupWindowShowAction1.TargetViewId = null;
            this.popupWindowShowAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.popupWindowShowAction1.ToolTip = null;
            this.popupWindowShowAction1.TypeOfView = null;
            this.popupWindowShowAction1.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowAction1_CustomizePopupWindowParams);
            this.popupWindowShowAction1.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowAction1_Execute);
            // 
            // CustomCreateReportController
            // 
            this.Activated += new EventHandler(this.Custom_CreateReportController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowAction1;

    }
}
