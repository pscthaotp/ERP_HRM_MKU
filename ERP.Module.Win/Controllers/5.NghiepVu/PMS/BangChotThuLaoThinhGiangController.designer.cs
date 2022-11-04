using System;
using System.ComponentModel;
using ERP.Module.NghiepVu.TienLuong.ChungTus;
using ERP.Module.NghiepVu.PMS.QuanLyGiangDay;
using ERP.Module.NghiepVu.PMS.BangChotThuLao;

namespace ERP.Module.Win.Controllers.NghiepVu.PMS
{
    partial class BangChotThuLaoThinhGiangController
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
            this.simpleAction2 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.popupTinhThuLao = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // simpleAction2
            // 
            this.simpleAction2.Caption = "Mở khóa bảng chốt";
            this.simpleAction2.ConfirmationMessage = null;
            this.simpleAction2.Id = "MoKhoaBangChotThuLaoTGController";
            this.simpleAction2.ImageName = "Action_Security_ChangePassword";
            this.simpleAction2.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction2.TargetObjectType = typeof(ERP.Module.NghiepVu.PMS.BangChotThuLao.BangChotThuLao_ThinhGiang);
            this.simpleAction2.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction2.ToolTip = "Mở khóa thù lao giảng dạy.";
            this.simpleAction2.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction2.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction2_Execute);
            // 
            // popupTinhThuLao
            // 
            this.popupTinhThuLao.AcceptButtonCaption = null;
            this.popupTinhThuLao.CancelButtonCaption = null;
            this.popupTinhThuLao.Caption = "Tính thù lao";
            this.popupTinhThuLao.ConfirmationMessage = null;
            this.popupTinhThuLao.Id = "popTinhThuLaoThinhGiang_Controller";
            this.popupTinhThuLao.ImageName = "BO_Money1";
            this.popupTinhThuLao.ToolTip = "Tính thù lao";
            this.popupTinhThuLao.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupTinhThuLao_CustomizePopupWindowParams);
            this.popupTinhThuLao.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupTinhThuLao_Execute);
            // 
            // BangChotThuLaoThinhGiangController
            // 
            this.Actions.Add(this.simpleAction2);
            this.Actions.Add(this.popupTinhThuLao);
            this.Activated += new System.EventHandler(this.DongBoBangChotThuLaoThinhGiangController_Activated);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction2;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupTinhThuLao;
    }
}
