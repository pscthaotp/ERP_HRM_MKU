﻿namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.TienLuong
{
    partial class KhenThuongPhucLoi_ImportCacLoaiKhenThuocPhucLoiController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.popupWindowShowAction1 = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popupWindowShowAction1
            // 
            this.popupWindowShowAction1.AcceptButtonCaption = null;
            this.popupWindowShowAction1.CancelButtonCaption = null;
            this.popupWindowShowAction1.Caption = "Import khen thưởng phúc lợi";
            this.popupWindowShowAction1.ConfirmationMessage = null;
            this.popupWindowShowAction1.Id = "KhenThuongPhucLoi_ImporCacLoaiKhenThuongPhucLoi_Controller";
            this.popupWindowShowAction1.ImageName = "Action_Import";
            this.popupWindowShowAction1.ToolTip = null;
            this.popupWindowShowAction1.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowAction1_CustomizePopupWindowParams);
            this.popupWindowShowAction1.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowAction1_Execute);
            // 
            // KhenThuongPhucLoi_ImportCacLoaiKhenThuocPhucLoiController
            // 
            this.Actions.Add(this.popupWindowShowAction1);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowAction1;
    }
}
