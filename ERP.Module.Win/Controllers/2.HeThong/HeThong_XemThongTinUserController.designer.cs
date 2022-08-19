using DevExpress.ExpressApp.Security.Strategy;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.HeThong
{
    partial class HeThong_XemThongTinUserController
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
            this.components = new System.ComponentModel.Container();
            this.TargetObjectType = typeof(SecuritySystemUser);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.simpleAction1 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // simpleAction1
            // 
            this.simpleAction1.Caption = "Xem thông tin user";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "HeThong_XemThongTinUserController";
            this.simpleAction1.ImageName = "BO_User";
            this.simpleAction1.TargetObjectType = typeof(SecuritySystemUser);
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Nested;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.simpleAction1.ToolTip = "Xem thông tin user người dùng.";          
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // HeThong_XemNhatKyDuLieuContrller
            // 
            this.Actions.Add(this.simpleAction1);
            this.Activated += new System.EventHandler(this.HeThong_XemThongTinUserController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
