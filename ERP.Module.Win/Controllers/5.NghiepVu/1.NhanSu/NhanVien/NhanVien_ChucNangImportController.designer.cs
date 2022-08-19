using ERP.Module.NghiepVu.NhanSu.NhanViens;
using System;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    partial class NhanVien_ChucNangImportController
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
            this.singleChoiceActionList = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // singleChoiceAction1
            // 
            this.singleChoiceActionList.Caption = "Các chức năng khác";
            this.singleChoiceActionList.ConfirmationMessage = null;
            this.singleChoiceActionList.Id = "NhanVien_ChucNangImportController";
            this.singleChoiceActionList.ImageName = "Action_Import";
            this.singleChoiceActionList.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.singleChoiceActionList.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            //this.singleChoiceActionList.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            //this.singleChoiceActionList.TargetObjectType = typeof(ThongTinNhanVien);
            this.singleChoiceActionList.ToolTip = null;
            this.singleChoiceActionList.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.singleChoiceAction1_Execute);
            // 
            // NhanVien_ChucNangImportController
            // 
            this.Actions.Add(this.singleChoiceActionList);
            this.ViewControlsCreated += new EventHandler(this.NhanVien_ChucNangImportController_ViewControlsCreated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction singleChoiceActionList;
    }
}
