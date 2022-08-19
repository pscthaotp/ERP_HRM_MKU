using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using System;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    partial class HoSo_CopyHoSoNhanVienController
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
            this.popupWindowShowAction1.Caption = "Copy Hồ sơ";
            this.popupWindowShowAction1.Id = "HoSo_CopyHoSoNhanVienController";
            this.popupWindowShowAction1.ImageName = "BO_ChiTietLuong";
            this.popupWindowShowAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            //this.popupWindowShowAction1.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            //this.popupWindowShowAction1.TargetObjectType = typeof(ThongTinNhanVien);
            this.popupWindowShowAction1.ToolTip = "Copy Hồ sơ nhân viên";
            this.popupWindowShowAction1.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowAction1_CustomizePopupWindowParams);
            this.popupWindowShowAction1.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowAction1_Execute);
            // 
            // HoSo_CopyHoSoNhanVienController
            // 
            this.Activated += new EventHandler(this.HoSo_CopyHoSoNhanVienController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowAction1;

    }
}
