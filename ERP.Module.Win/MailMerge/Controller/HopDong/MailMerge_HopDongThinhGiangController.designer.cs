using ERP.Module.NghiepVu.NhanSu.HopDongs;
using System;
using System.ComponentModel;

namespace ERP.Module.Win.MailMerge.Controller.HopDongs
{
    partial class MailMerge_HopDongThinhGiangController
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
            this.simpleAction1.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.simpleAction1.Caption = "In HĐ TG";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "MailMerge_HopDongThinhGiangController";
            this.simpleAction1.ImageName = "Action_Printing_Print";
            this.simpleAction1.TargetObjectType = typeof(ERP.Module.NghiepVu.NhanSu.HopDongs.HopDongThinhGiang);
            this.simpleAction1.ToolTip = "In HĐ Thỉnh giảng";
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // MailMerge_HopDongThinhGiangController
            // 
            this.Actions.Add(this.simpleAction1);
            this.Activated += new System.EventHandler(this.MailMerge_HopDongThinhGiangController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
