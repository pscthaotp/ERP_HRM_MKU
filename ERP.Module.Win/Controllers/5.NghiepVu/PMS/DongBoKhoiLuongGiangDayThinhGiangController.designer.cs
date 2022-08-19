using System;
using System.ComponentModel;
using ERP.Module.NghiepVu.TienLuong.ChungTus;
using ERP.Module.NghiepVu.PMS.QuanLyGiangDay;

namespace ERP.Module.Win.Controllers.NghiepVu.PMS
{
    partial class DongBoKhoiLuongGiangDayThinhGiangController
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
            this.simpleAction1.Caption = "Đồng bộ TKB(TG)";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "DongBoKhoiLuongGiangDayThinhGiangController";
            this.simpleAction1.ImageName = "Action_Reload";
            this.simpleAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction1.TargetObjectType = typeof(ERP.Module.NghiepVu.PMS.QuanLyGiangDay.QuanLyKhoiLuongGiangDay_ThinhGiang);
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction1.ToolTip = "Đồng bộ dữ liệu thời khóa biểu.";
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);           
            // 
            // DongBoKhoiLuongGiangDayThinhGiangController
            // 
            this.Actions.Add(this.simpleAction1);
            this.Activated += new System.EventHandler(this.DongBoKhoiLuongGiangDayThinhGiangController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
