﻿using ERP.Module.NghiepVu.NhanSu.TuyenDung;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.TuyenDung
{
    partial class TuyenDung_CapNhatHinhThucTuyenDungController
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
            this.simpleAction1.Caption = "Cập nhật hình thức TD";
            this.simpleAction1.ConfirmationMessage = "Bạn có muốn cập nhật hình thức tuyển dụng không?";
            this.simpleAction1.Id = "TuyenDung_CapNhatHinhThucTuyenDungController";
            this.simpleAction1.ImageName = "BO_List1";
            this.simpleAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction1.Tag = null;
            this.simpleAction1.TargetObjectType = typeof(VongTuyenDung);
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction1.ToolTip = "Cập nhật hình thức tuyển dụng";
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // TuyenDung_TinhDiemTrungBinhController
            // 
            this.Activated += new System.EventHandler(this.TuyenDung_CapNhatHinhThucTuyenDungController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
