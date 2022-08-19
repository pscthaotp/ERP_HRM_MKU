using ERP.Module.NghiepVu.TienLuong.Luong;
using System;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.Luong
{
    partial class Luong_TinhLuongNhanVienController
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
            this.simpleAction1.Caption = "Tính lương";
            this.simpleAction1.Id = "Luong_TinhLuongNhanVienController";
            this.simpleAction1.ImageName = "BO_Money_Calculator";
            this.simpleAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction1.Tag = null;
            this.simpleAction1.TargetObjectType = typeof(BangLuongNhanVien);
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction1.ToolTip = "Tính lương và phục cấp.";
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // Luong_TinhLuongNhanVienController
            // 
            this.Activated += new EventHandler(this.Luong_TinhLuongNhanVienController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
