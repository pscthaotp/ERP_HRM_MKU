﻿using ERP.Module.DanhMuc.TienLuong;
using System;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.HoSoLuong
{
    partial class HoSoLuong_MoKhoaSoKyTinhLuongController
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
            this.simpleAction1.Caption = "Mở khóa sổ";
            this.simpleAction1.Id = "HoSoLuong_MoKhoaSoKyTinhLuongController";
            this.simpleAction1.ImageName = "BO_Lock";
            this.simpleAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction1.Tag = null;
            this.simpleAction1.TargetObjectType = typeof(KyTinhLuong);
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.ToolTip = "Mở khóa sổ kỳ tính lương";
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // KhoaHoSoAction
            // 
            this.Activated += new EventHandler(this.HoSoLuong_MoKhoaSoKyTinhLuongController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
