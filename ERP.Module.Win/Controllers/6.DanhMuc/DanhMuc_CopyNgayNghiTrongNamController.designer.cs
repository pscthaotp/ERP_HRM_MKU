using ERP.Module.DanhMuc.TienLuong;
using System;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.DanhMuc
{
    partial class DanhMuc_CopyNgayNghiTrongNamController
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
            this.popupWindowShowAction1.Caption = "Copy";
            this.popupWindowShowAction1.Id = "DanhMuc_CopyNgayNghiTrongNamController";
            this.popupWindowShowAction1.ImageName = "BO_ChiTietLuong";
            this.popupWindowShowAction1.TargetObjectType = typeof(CC_NgayNghiTrongNam);
            this.popupWindowShowAction1.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.popupWindowShowAction1.ToolTip = "Copy Ngày nghỉ trong năm";
            this.popupWindowShowAction1.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowAction1_CustomizePopupWindowParams);
            this.popupWindowShowAction1.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowAction1_Execute);
            // 
            // DanhMuc_CopyNgayNghiTrongNamController
            // 
            this.Activated += new EventHandler(this.DanhMuc_CopyNgayNghiTrongNamController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowAction1;

    }
}
