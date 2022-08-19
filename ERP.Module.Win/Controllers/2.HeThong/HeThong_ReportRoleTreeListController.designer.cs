using ERP.Module.HeThong;
using System;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.HeThong
{
    partial class HeThong_ReportRoleTreeListController
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
            // 
            this.components = new Container();
            this.TargetObjectType = typeof(SecuritySystemRole_Report);
            this.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.ViewControlsCreated += new EventHandler(this.HeThong_ReportRoleTreeListController_ViewControlsCreated);

        }

        #endregion
    }
}
