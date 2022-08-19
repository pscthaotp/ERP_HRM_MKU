using System;
using System.ComponentModel;
using ERP.Module.NghiepVu.TienLuong.ChungTus;
using ERP.Module.NghiepVu.PMS.QuanLyGiangDay;

namespace ERP.Module.Win.Controllers.NghiepVu.PMS
{
    partial class DongBoKhoiLuongGiangDayController
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
            this.simpleAction1.Caption = "Đồng bộ TKB";
            this.simpleAction1.Id = "DongBoKhoiLuongGiangDayController";
            this.simpleAction1.ImageName = "Action_Reload";
            this.simpleAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction1.Tag = null;
            this.simpleAction1.TargetObjectType = typeof(QuanLyKhoiLuongGiangDay);
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Any;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction1.ToolTip = "Đồng bộ dữ liệu thời khóa biểu.";
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // ChungTu_TinhChuyenKhoanController
            // 
            this.Activated += new EventHandler(this.DongBoKhoiLuongGiangDayController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
