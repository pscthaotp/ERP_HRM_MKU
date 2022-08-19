using ERP.Module.BaoCao.Custom;
using System;
using System.ComponentModel;

namespace ERP.Module.Web.Controllers.BaoCao
{
    partial class Rpt_ShowReport_PhieuDongHocPhiController
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
            this.simpleAction1.Caption = "In phiếu đóng phí";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "Rpt_ShowReport_PhieuDongHocPhiController";
            this.simpleAction1.ImageName = "MenuBar_PrintPreview";
            this.simpleAction1.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction1.ToolTip = "In phiếu đóng phí";
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // Rpt_ShowReport_PhieuDongHocPhiController
            // 
            this.Actions.Add(this.simpleAction1);
            this.Activated += new System.EventHandler(this.Rpt_ShowReport_PhieuDongHocPhiController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
