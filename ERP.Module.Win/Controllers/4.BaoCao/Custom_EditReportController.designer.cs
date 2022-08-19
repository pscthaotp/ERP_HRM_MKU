using ERP.Module.BaoCao.Custom;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.BaoCao
{
    partial class Custom_EditReportController
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
            this.components = new Container();
            this.simpleAction1 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // simpleAction1
            // 
            this.simpleAction1.Caption = "Chỉnh sửa báo cáo";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "Custom_EditReportController";
            this.simpleAction1.ImageName = "MenuBar_Edit";
            this.simpleAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction1.Shortcut = null;
            this.simpleAction1.Tag = null;
            this.simpleAction1.TargetObjectsCriteria = null;
            this.simpleAction1.TargetObjectType = typeof(ReportData_Custom);
            this.simpleAction1.TargetViewId = null;
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.simpleAction1.ToolTip = null;
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            this.Activated += Custom_EditReportController_Activated;
        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
