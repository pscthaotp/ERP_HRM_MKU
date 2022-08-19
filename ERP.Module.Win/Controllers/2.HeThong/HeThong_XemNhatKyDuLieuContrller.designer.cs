using ERP.Module.HeThong;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.HeThong
{
    partial class HeThong_XemNhatKyDuLieuContrller
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
            this.simpleAction1 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // simpleAction1
            // 
            this.simpleAction1.Caption = "Xem nhật ký";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "HeThong_XemNhatKyDuLieuContrller";
            this.simpleAction1.ImageName = "Action_UpdateProcess";
            this.simpleAction1.TargetObjectType = typeof(ERP.Module.HeThong.SecuritySystemUser_Custom);
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.simpleAction1.ToolTip = "Xem nhật ký chỉnh sửa dữ liệu của hệ thống.";
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // HeThong_XemNhatKyDuLieuContrller
            // 
            this.Actions.Add(this.simpleAction1);
            this.Activated += new System.EventHandler(this.HeThong_XemNhatKyDuLieuContrller_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
