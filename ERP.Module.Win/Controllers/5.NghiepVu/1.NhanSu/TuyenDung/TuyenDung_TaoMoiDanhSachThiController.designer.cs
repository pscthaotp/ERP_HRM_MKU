using ERP.Module.NghiepVu.NhanSu.TuyenDung;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.TuyenDung
{
    partial class TuyenDung_TaoMoiDanhSachThiController
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
            this.popupWindowShowAction1.Caption = "Tạo danh sách thi";
            this.popupWindowShowAction1.ConfirmationMessage = null;
            this.popupWindowShowAction1.Id = "TuyenDung_TaoMoiDanhSachThiController";
            this.popupWindowShowAction1.ImageName = "BO_QuyetDinh";
            this.popupWindowShowAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.popupWindowShowAction1.TargetObjectType = typeof(DanhSachThi);
            this.popupWindowShowAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.popupWindowShowAction1.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.popupWindowShowAction1.ToolTip = "Tạo danh sách thi";
            this.popupWindowShowAction1.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.popupWindowShowAction1.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowAction1_CustomizePopupWindowParams);
            this.popupWindowShowAction1.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowAction1_Execute);
            // 
            // TuyenDung_TaoMoiDanhSachThiController
            // 
            this.Activated += new System.EventHandler(this.BienDongAction_Activated);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowAction1;
    }
}
