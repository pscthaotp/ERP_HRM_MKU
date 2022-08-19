using System;
using System.ComponentModel;

namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    partial class TuyenSinh_ChonTreToChucThiController
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
            this.popupWindowShowAction1.AcceptButtonCaption = null;
            this.popupWindowShowAction1.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.popupWindowShowAction1.CancelButtonCaption = null;
            this.popupWindowShowAction1.Model.Index = 2;

            this.popupWindowShowAction1.Caption = "Chọn học sinh";
            this.popupWindowShowAction1.ConfirmationMessage = null;
            this.popupWindowShowAction1.Id = "TuyenSinh_ChonTreToChucThiController";
            this.popupWindowShowAction1.ImageName = "Action_Import";
            this.popupWindowShowAction1.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.popupWindowShowAction1.TargetObjectType = typeof(ERP.Module.NghiepVu.TuyenSinh.ToChucThi);
            this.popupWindowShowAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.popupWindowShowAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.popupWindowShowAction1.ToolTip = "Chọn học sinh từ dữ liệu đã lưu trữ";
            this.popupWindowShowAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.popupWindowShowAction1.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowAction1_CustomizePopupWindowParams);
            this.popupWindowShowAction1.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowAction1_Execute);
            // 
            // TuyenSinh_ChonTreToChucThiController
            // 
            this.Actions.Add(this.popupWindowShowAction1);
            this.Activated += new System.EventHandler(this.TuyenSinh_ChonTreToChucThiController_Activated);
              
        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowAction1;

    }
}
