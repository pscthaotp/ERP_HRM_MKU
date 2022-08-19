﻿using ERP.Module.NghiepVu.NhanSu.HoSoLuong;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.HoSoLuong
{
    partial class HoSoLuong_CopyCongThucLuongController
    {
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
            this.popupWindowShowAction1 = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popupWindowShowAction1
            // 
            this.popupWindowShowAction1.AcceptButtonCaption = null;
            this.popupWindowShowAction1.CancelButtonCaption = null;
            this.popupWindowShowAction1.Caption = "Copy công thức";
            this.popupWindowShowAction1.ConfirmationMessage = null;
            this.popupWindowShowAction1.Id = "HoSoLuong_CopyCongThucLuongController";
            this.popupWindowShowAction1.ImageName = "BO_NangLuong";
            this.popupWindowShowAction1.TargetObjectType = typeof(CongThucTinhLuong);
            this.popupWindowShowAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.popupWindowShowAction1.ToolTip = "Copy công thức tính lương và phụ cấp";
            this.popupWindowShowAction1.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.popupWindowShowAction1.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowAction1_CustomizePopupWindowParams);
            this.popupWindowShowAction1.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowAction1_Execute);
            // 
            // HoSo_ImportController
            // 
            this.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.Activated += new System.EventHandler(this.HoSoLuong_CopyCongThucLuongController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowAction1;

    }
}
