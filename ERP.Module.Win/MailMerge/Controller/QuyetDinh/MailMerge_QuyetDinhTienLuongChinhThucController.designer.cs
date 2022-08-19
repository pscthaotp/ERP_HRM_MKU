namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    partial class MailMerge_QuyetDinhTienLuongChinhThucController
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
            this.simpleAction1 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // simpleAction1
            // 
            this.simpleAction1.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.simpleAction1.Caption = "In quyết định";
            this.simpleAction1.Category = "View";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "c91b8ca5-e4d0-418b-9586-a0bdc2aa8226";
            this.simpleAction1.ImageName = "Action_Printing_Print";
            this.simpleAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.simpleAction1.TargetObjectType = typeof(ERP.Module.NghiepVu.NhanSu.QuyetDinhs.QuyetDinhTienLuongChinhThuc);
            this.simpleAction1.ToolTip = "Quyết định tiền lương chính thức";
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // MailMerge_QuyetDinhTienLuongChinhThucController
            // 
            this.Actions.Add(this.simpleAction1);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
