using DevExpress.ExpressApp;
using System;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    partial class NhanVien_ThemNhanVienController
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
            #region #simpleAction1
            this.simpleAction1 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // simpleAction1
            // 
            this.simpleAction1.Id = "NhanVien_ThemNhanVienController";
            this.simpleAction1.Caption = "Thêm mới";
            this.simpleAction1.ToolTip = "Thêm mới nhân viên";
            this.simpleAction1.ImageName = "Action_New";
            this.simpleAction1.Category = "ObjectsCreation";
            //this.simpleAction1.TargetViewType = ViewType.ListView;
            this.simpleAction1.TargetViewNesting = Nesting.Root;
            //this.simpleAction1.TargetObjectType = typeof(ERP.Module.NghiepVu.NhanSu.NhanViens.NhanVien);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            this.Activated += NhanVien_ThemNhanVienController_Activated;
            // 
            // Kho_CapNhatKhoController
            // 
            this.Actions.Add(this.simpleAction1);
            #endregion

            #region #simpleAction2
            this.simpleAction2 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // simpleAction1
            // 
            this.simpleAction2.Id = "NhanVien_XoaNhanVienController";
            this.simpleAction2.Caption = "Xóa";
            this.simpleAction2.ToolTip = "Xóa nhân viên";
            this.simpleAction2.ImageName = "Action_Delete";
            this.simpleAction2.Category = "Edit";
            //this.simpleAction1.TargetViewType = ViewType.ListView;
            this.simpleAction1.TargetViewNesting = Nesting.Root;
            //this.simpleAction1.TargetObjectType = typeof(ERP.Module.NghiepVu.NhanSu.NhanViens.NhanVien);
            this.simpleAction2.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction2_Execute);
            this.Activated += NhanVien_ThemNhanVienController_Activated;
            // 
            // Kho_CapNhatKhoController
            // 
            this.Actions.Add(this.simpleAction2);
            #endregion
        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction2;
    }
}
