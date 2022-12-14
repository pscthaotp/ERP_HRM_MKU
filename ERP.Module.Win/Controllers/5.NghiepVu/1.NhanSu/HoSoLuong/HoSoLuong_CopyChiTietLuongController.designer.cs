using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using System;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.HoSoLuong
{
    partial class HoSoLuong_CopyChiTietLuongController
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
            this.popupWindowShowAction1.Caption = "Copy chi tiết lương";
            this.popupWindowShowAction1.ConfirmationMessage = null;
            this.popupWindowShowAction1.Id = "HoSoLuong_CopyChiTietLuongController";
            this.popupWindowShowAction1.ImageName = "BO_ChiTietLuong";
            this.popupWindowShowAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.popupWindowShowAction1.TargetObjectsCriteria = null;
            this.popupWindowShowAction1.TargetObjectType = typeof(HoSoTinhLuong);
            this.popupWindowShowAction1.TargetViewId = null;
            this.popupWindowShowAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.popupWindowShowAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.popupWindowShowAction1.ToolTip = "Copy chi tiết lương.";
            this.popupWindowShowAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.popupWindowShowAction1.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowAction1_CustomizePopupWindowParams);
            this.popupWindowShowAction1.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowAction1_Execute);
            // 
            // HoSoLuong_CopyChiTietLuongController
            // 
            this.Activated += new EventHandler(this.HoSoLuong_CopyChiTietLuongController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowAction1;

    }
}
