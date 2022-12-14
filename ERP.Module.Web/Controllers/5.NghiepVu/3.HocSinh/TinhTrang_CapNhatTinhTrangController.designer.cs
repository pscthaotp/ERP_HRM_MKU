
using ERP.Module.NonPersistentObjects.HocSinh;

namespace ERP.Module.Web.Controllers.NghiepVu.HocSinh
{
    partial class TinhTrang_CapNhatTinhTrangController
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
            this.simpleAction1.Caption = "Thực thi";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "TinhTrang_CapNhatTinhTrangController";
            this.simpleAction1.ImageName = "BO_HinhThucKyLuat";
            this.simpleAction1.TargetObjectType = typeof(TinhTrang_CapNhatTinhTrang);
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction1.ToolTip = "Cập nhật tình trạng học sinh và thực hiện các nghiệp vụ kèm theo";
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // TinhTrang_CapNhatTinhTrangController
            // 
            this.Actions.Add(this.simpleAction1);
            this.Activated += new System.EventHandler(this.TinhTrang_CapNhatTinhTrangController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
