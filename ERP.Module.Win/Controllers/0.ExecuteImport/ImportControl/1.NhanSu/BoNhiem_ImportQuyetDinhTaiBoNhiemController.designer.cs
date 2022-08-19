using ERP.Module.NghiepVu.NhanSu.HopDongs;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using System;
using System.ComponentModel;

namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.NhanSu
{
    partial class BoNhiem_ImportQuyetDinhTaiBoNhiemController
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
            this.popupWindowShowAction1 = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popupWindowShowAction1
            // 
            this.popupWindowShowAction1.AcceptButtonCaption = null;
            this.popupWindowShowAction1.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.popupWindowShowAction1.CancelButtonCaption = null;
            this.popupWindowShowAction1.Caption = "Nhập quyết định";
            this.popupWindowShowAction1.ConfirmationMessage = null;
            this.popupWindowShowAction1.Id = "BoNhiem_ImportQuyetDinhTaiBoNhiemController";
            this.popupWindowShowAction1.ImageName = "BO_QuyetDinh";
            this.popupWindowShowAction1.Shortcut = null;
            this.popupWindowShowAction1.Tag = null;
            this.popupWindowShowAction1.TargetObjectsCriteria = null;
            this.popupWindowShowAction1.TargetObjectType = typeof(QuyetDinhTaiBoNhiem);
            this.popupWindowShowAction1.TargetViewId = null;
            this.popupWindowShowAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.popupWindowShowAction1.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.popupWindowShowAction1.ToolTip = "Nhập danh sách nhân viên được tái bổ nhiệm vào hệ thống.";
            this.popupWindowShowAction1.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.popupWindowShowAction1.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowAction1_CustomizePopupWindowParams);
            this.popupWindowShowAction1.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowAction1_Execute);

            // 
            this.TargetObjectType = typeof(QuyetDinhTaiBoNhiem);
            this.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.Activated += new EventHandler(this.BoNhiem_ImportQuyetDinhTaiBoNhiemController_Activated);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowAction1;
    }
}
