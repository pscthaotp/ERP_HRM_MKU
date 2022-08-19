namespace ERP.Module.Win.Controllers.NghiepVu.BaoCao
{
    partial class Show_Design_Report_Controller
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
            this.popShowDesign = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popShowDesign
            // 
            this.popShowDesign.AcceptButtonCaption = null;
            this.popShowDesign.CancelButtonCaption = null;
            this.popShowDesign.Caption = "Design Report";
            this.popShowDesign.ConfirmationMessage = null;
            this.popShowDesign.Id = "popShow";
            this.popShowDesign.ImageName = "Action_Report_ShowDesigner";
            this.popShowDesign.ToolTip = null;
            this.popShowDesign.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popShow_CustomizePopupWindowParams);
            this.popShowDesign.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popShow_Execute);
            // 
            // ShowReport_Controller
            // 
            this.Activated+=Show_Design_Report_Controller_Activated;
            this.Actions.Add(this.popShowDesign);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popShowDesign;
    }
}
