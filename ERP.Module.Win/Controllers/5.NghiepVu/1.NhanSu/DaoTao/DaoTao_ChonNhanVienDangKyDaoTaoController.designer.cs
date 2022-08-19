﻿using ERP.Module.NghiepVu.NhanSu.DaoTao;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.DaoTao
{
    partial class DaoTao_ChonNhanVienDangKyDaoTaoController
    {
        /// <summary>
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
            // simpleAction
            // 
            this.simpleAction1.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.simpleAction1.Caption = "Chọn nhân viên";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "DaoTao_ChonNhanVienDangKyDaoTaoController";
            this.simpleAction1.ImageName = "Action_AddEmployee";
            this.simpleAction1.Shortcut = null;
            this.simpleAction1.Tag = null;
            this.simpleAction1.TargetObjectsCriteria = null;           
            this.simpleAction1.TargetViewId = null;
            this.simpleAction1.ToolTip = "Chọn nhân viên đăng ký đào tạo";
            this.simpleAction1.TargetObjectType = typeof(DangKyDaoTao);          
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction_Execute);
            // 
            // DaoTao_DuyetDaoTaoController
            // 
            this.Activated += new System.EventHandler(this.DaoTao_ChonNhanVienDangKyDaoTaoController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
