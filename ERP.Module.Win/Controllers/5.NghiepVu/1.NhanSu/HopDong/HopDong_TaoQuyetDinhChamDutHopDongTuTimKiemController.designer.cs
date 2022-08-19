﻿using ERP.Module.NghiepVu.NhanSu.HopDongs;
using System;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.HopDongs
{
    partial class HopDong_TaoQuyetDinhChamDutHopDongTuTimKiemController
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
            this.TargetObjectType = typeof(DanhSachHetHanHopDong);
            this.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction1 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // popupWindowShowAction1
            // 
            this.simpleAction1.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.simpleAction1.Caption = "Lập quyết định chấm dứt hợp đồng";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "HopDong_TaoQuyetDinhChamDutHopDongTuTimKiemController";
            this.simpleAction1.ImageName = "BO_QuyetDinh";
            this.simpleAction1.TargetObjectType = typeof(DanhSachHetHanHopDong);
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction1.ToolTip = "Lập quyết định chấm dứt hợp đồng cho một nhân viên được chọn";
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // HopDong_TaoHopDongTuTimKiemController            // 

            this.Activated += new System.EventHandler(this.HopDong_TaoHopDongTuTimKiemController_Activated);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
