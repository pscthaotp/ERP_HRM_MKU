using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.TuyenDung
{
    partial class TuyenDung_ChonUngVienLapQuyetDinhTuyenDungController
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
            this.popupWindowShowAction1.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.popupWindowShowAction1.CancelButtonCaption = null;
            this.popupWindowShowAction1.Caption = "Chọn cán bộ";
            this.popupWindowShowAction1.ConfirmationMessage = null;
            this.popupWindowShowAction1.Id = "TuyenDung_ChonUngVienLapQuyetDinhTuyenDungController";
            this.popupWindowShowAction1.ImageName = "Action_AddEmployee";
            this.popupWindowShowAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.popupWindowShowAction1.TargetObjectType = typeof(ERP.Module.NghiepVu.NhanSu.QuyetDinhs.QuyetDinhTuyenDung);
            this.popupWindowShowAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.popupWindowShowAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.popupWindowShowAction1.ToolTip = "Chọn cán bộ lập QĐ tuyển dụng";
            this.popupWindowShowAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.popupWindowShowAction1.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowAction1_CustomizePopupWindowParams);
            this.popupWindowShowAction1.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowAction1_Execute);
            // 
            // TuyenDung_ChonUngVienLapQuyetDinhTuyenDungController
            // 
            this.Actions.Add(this.popupWindowShowAction1);
            this.Activated += new System.EventHandler(this.ChuyenHoSoTuyenDungAction_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowAction1;

    }
}
