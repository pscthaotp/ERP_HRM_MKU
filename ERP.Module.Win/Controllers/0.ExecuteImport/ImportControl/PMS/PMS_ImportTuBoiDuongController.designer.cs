using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.NghiepVu.PMS.CVHT;
using System;
using System.ComponentModel;

namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.PMS
{
    partial class PMS_ImportTuBoiDuongController
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
            this.components = new System.ComponentModel.Container();
            this.popupWindowShowAction2 = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popupWindowShowAction2
            // 
            this.popupWindowShowAction2.AcceptButtonCaption = null;
            this.popupWindowShowAction2.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.popupWindowShowAction2.CancelButtonCaption = null;
            this.popupWindowShowAction2.Caption = "Import Tự bồi dưỡng";
            this.popupWindowShowAction2.ConfirmationMessage = null;
            this.popupWindowShowAction2.Id = "PMS_ImportTuBoiDuongController";
            this.popupWindowShowAction2.ImageName = "Action_Import";
            this.popupWindowShowAction2.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.popupWindowShowAction2.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.popupWindowShowAction2.ToolTip = "Nhập dữ liệu Tự bồi dưỡng từ tập tin excel";
            this.popupWindowShowAction2.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.popupWindowShowAction2.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowAction_CustomizePopupWindowParams);
            this.popupWindowShowAction2.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowAction_Execute);
            // 
            // PMS_ImportTuBoiDuongController
            // 
            this.Actions.Add(this.popupWindowShowAction2);
            this.Activated += new System.EventHandler(this.PMS_ImportTuBoiDuongController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowAction1;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowAction2;
    }
}
