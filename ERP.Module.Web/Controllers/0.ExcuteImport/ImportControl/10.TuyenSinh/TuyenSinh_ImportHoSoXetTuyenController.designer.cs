using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.NghiepVu.TuyenSinh_TP;
using System;
using System.ComponentModel;

namespace ERP.Module.Controllers.Web.ExecuteImport.ImportControl.TuyenSinh
{
    partial class TuyenSinh_ImportHoSoXetTuyenController
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
            this.popupWindowShowAction1 = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popupWindowShowAction1
            // 
            this.popupWindowShowAction1.Caption = "Nhập dữ liệu";
            this.popupWindowShowAction1.ImageName = "BO_Extract";
            this.popupWindowShowAction1.Id = "TuyenSinh_ImportHoSoXetTuyenController";
            this.popupWindowShowAction1.TargetObjectType = typeof(HoSoXetTuyen);
            this.popupWindowShowAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.popupWindowShowAction1.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.popupWindowShowAction1.ToolTip = null;
            this.popupWindowShowAction1.ToolTip = "Nhập dữ liệu khách hàng từ tập tin excel.";
            this.popupWindowShowAction1.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.popupWindowShowAction1.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowAction1_CustomizePopupWindowParams);
            this.popupWindowShowAction1.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowAction1_Execute);
            // 
            // TuyenSinh_ImportTuVanTuyenSinhController
            // 
            this.Actions.Add(this.popupWindowShowAction1);
            this.Activated += new System.EventHandler(this.TuyenSinh_ImportHoSoXetTuyenController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowAction1;
    }
}
