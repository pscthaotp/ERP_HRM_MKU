namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    partial class MailMerge_QuyetDinhTienLuongThuViecController
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
            this.simpleAction1.Caption = "In quyết định";
            this.simpleAction1.Category = "View";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "5f3dccd9-fdfb-4dfe-9b06-d65351ed3248";
            this.simpleAction1.ImageName = "Action_Printing_Print";
            this.simpleAction1.TargetObjectType = typeof(ERP.Module.NghiepVu.NhanSu.QuyetDinhs.QuyetDinhTienLuongThuViec);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // MailMerge_QuyetDinhTienLuongThuViecController
            // 
            this.Actions.Add(this.simpleAction1);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
