using ERP.Module.NghiepVu.TienLuong.ChungTus;
using System;
using System.ComponentModel;

namespace ERP.Module.Win.Controller.BaoCao
{
    partial class BaoCao_ExportToWorkBookBaoCaoTienLuongController
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
            this.simpleAction1 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // simpleAction1
            // 
            this.simpleAction1.Caption = "Xuất workbook hồ sơ lương";
            this.simpleAction1.Category = "View";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "BaoCao_ExportToWorkBookBaoCaoTienLuongController";
            this.simpleAction1.ImageName = "BO_QuyetDinh";
            this.simpleAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction1.TargetObjectType = typeof(ChungTu);
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.Any;
            this.simpleAction1.ToolTip = "Xuất workbook hồ sơ lương";           
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // BaoCao_ExportToWorkBookBaoCaoTienLuongController
            // 
            this.Actions.Add(this.simpleAction1);
            this.Activated += new System.EventHandler(this.BaoCao_ExportToWorkBookBaoCaoTienLuongController_Activated);
        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;

    }
}
