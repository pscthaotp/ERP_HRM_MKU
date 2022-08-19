namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    partial class MailMerge_QuyetDinhBietPhaiController
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
            this.simpleAction2 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.TargetObjectType = typeof(ERP.Module.NghiepVu.NhanSu.QuyetDinhs.QuyetDinhBietPhai);
            // 
            // simpleAction2
            // 
            this.simpleAction2.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.simpleAction2.Caption = "In quyết định";
            this.simpleAction2.Category = "View";
            this.simpleAction2.ConfirmationMessage = null;
            this.simpleAction2.Id = "7bacee4a-9840-4761-adbf-70a7bae7b48a";
            this.simpleAction2.ImageName = "Action_Printing_Print";
            this.simpleAction2.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.simpleAction2.TargetObjectType = typeof(ERP.Module.NghiepVu.NhanSu.QuyetDinhs.QuyetDinhBietPhai);
            this.simpleAction2.ToolTip = "In quyết định biệt phái";
            this.simpleAction2.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction2_Execute);
            // 
            // MailMerge_QuyetDinhBietPhaiController
            // 
            this.Actions.Add(this.simpleAction2);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction2;
    }
}
