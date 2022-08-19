namespace ERP.Module.Web.Controllers._5.NghiepVu._14.XetTuyen
{
    partial class XetTuyen_XetTuyenHoSoController
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
            this.simpleAction1.Caption = "Xét tuyển";
            this.simpleAction1.ConfirmationMessage = "Bạn chắn chắn muốn xét tuyển không?";
            this.simpleAction1.Id = "XetTuyen_XetTuyenHoSoController";
            this.simpleAction1.ImageName = "BO_attendance";
            this.simpleAction1.TargetObjectType = typeof(ERP.Module.NonPersistentObjects.XetTuyen.XetTuyen_DotXetTuyen);
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction1.ToolTip = null;
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // XetTuyen_XetTuyenHoSoController
            // 
            this.Actions.Add(this.simpleAction1);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
