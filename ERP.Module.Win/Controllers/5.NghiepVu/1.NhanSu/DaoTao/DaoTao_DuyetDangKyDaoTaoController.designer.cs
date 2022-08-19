﻿using ERP.Module.NghiepVu.NhanSu.DaoTao;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.DaoTao
{
    partial class DaoTao_DuyetDangKyDaoTaoController
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
            components = new System.ComponentModel.Container();
            this.popupWindowShowAction1 = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popupWindowShowAction1
            // 
            this.popupWindowShowAction1.AcceptButtonCaption = "Duyệt";
            this.popupWindowShowAction1.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.popupWindowShowAction1.CancelButtonCaption = null;
            this.popupWindowShowAction1.Caption = "Duyệt đăng ký đào tạo";
            this.popupWindowShowAction1.ConfirmationMessage = null;
            this.popupWindowShowAction1.Id = "DaoTao_DuyetDangKyDaoTaoController";
            this.popupWindowShowAction1.ImageName = "Action_Import";
            this.popupWindowShowAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.popupWindowShowAction1.Shortcut = null;
            this.popupWindowShowAction1.Tag = null;
            this.popupWindowShowAction1.TargetObjectsCriteria = null;
            this.popupWindowShowAction1.TargetObjectType = typeof(DangKyDaoTao);
            this.popupWindowShowAction1.TargetViewId = null;
            //this.popupWindowShowAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.popupWindowShowAction1.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.popupWindowShowAction1.ToolTip = "Duyệt đăng ký đào tạo";
            this.popupWindowShowAction1.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.popupWindowShowAction1.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowAction1_CustomizePopupWindowParams);
            this.popupWindowShowAction1.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowAction1_Execute);
            // 
            // DanhGia_CopyBangQuyDoiThangController
            // 
            this.Activated += new System.EventHandler(this.DaoTao_ChonNhanVienDuyetDaoTaoController_Activated);
        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowAction1;
    }
}
