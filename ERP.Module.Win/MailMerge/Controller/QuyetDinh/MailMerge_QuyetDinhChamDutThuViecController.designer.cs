namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    partial class MailMerge_QuyetDinhChamDutThuViecController
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
            this.simpleAction1.Caption = "In thông báo";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "a8ba8c81-006b-4383-bf90-5e31799c800f";
            this.simpleAction1.ImageName = "Action_Printing_Print";
            this.simpleAction1.TargetObjectType = typeof(ERP.Module.NghiepVu.NhanSu.QuyetDinhs.QuyetDinhThongBaoChamDutThuViec);
            this.simpleAction1.ToolTip = null;
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // MailMerge_QuyetDinhChamDutThuViecController
            // 
            this.Actions.Add(this.simpleAction1);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
