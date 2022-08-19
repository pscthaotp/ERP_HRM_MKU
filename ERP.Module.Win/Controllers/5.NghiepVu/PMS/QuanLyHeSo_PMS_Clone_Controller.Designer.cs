namespace ERP.Module.Win.Controllers.PMS
{
    partial class QuanLyHeSo_PMS_Clone_Controller
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
            this.btnpop = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // btnpop
            // 
            this.btnpop.AcceptButtonCaption = null;
            this.btnpop.CancelButtonCaption = null;
            this.btnpop.Caption = "Copy";
            this.btnpop.ConfirmationMessage = null;
            this.btnpop.Id = "QuanLyHeSo_PMS_Clone_Controller";
            this.btnpop.ImageName = "Action_CloneMerge_Clone_Object";
            this.btnpop.ToolTip = "Clone Quản lý hệ số PMS";
            this.btnpop.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.btnpop_CustomizePopupWindowParams);
            this.btnpop.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.btnpop_Execute);
            // 
            // CauHinhQuyDoiPMS_PMS_Clone_Controller
            // 
            this.Actions.Add(this.btnpop);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction btnpop;
    }
}
