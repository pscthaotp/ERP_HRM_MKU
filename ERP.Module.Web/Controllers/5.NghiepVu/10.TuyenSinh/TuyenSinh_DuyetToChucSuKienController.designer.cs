namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    partial class TuyenSinh_DuyetToChucSuKienController
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
            this.simpleAction1.Caption = "Duyệt";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "TuyenSinh_DuyetToChucSuKienController";
            this.simpleAction1.ImageName = "BO_LapQD";
            this.simpleAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.simpleAction1.TargetObjectType = typeof(ERP.Module.NonPersistentObjects.TuyenSinh.ChiTietDuyetToChucSuKien);
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Nested;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.simpleAction1.ToolTip = null;
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // TuyenSinh_DuyetToChucSuKienController
            // 
            this.Actions.Add(this.simpleAction1);
            this.Activated += new System.EventHandler(this.TuyenSinh_DuyetToChucSuKienController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
