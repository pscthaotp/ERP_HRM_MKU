using System;
using System.ComponentModel;
using ERP.Module.NghiepVu.TuyenSinh;

namespace ERP.Module.Controllers.Web.ExecuteImport.ImportControl.Commons
{
    partial class Common_XemLoiNhapDuLieuController
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
            this.simpleAction1.Caption = "Xem lỗi";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "Common_XemLoiNhapDuLieuController";
            this.simpleAction1.ImageName = "BO_Documents7";
            this.simpleAction1.ToolTip = "Xem lỗi khi nhập dữ liệu từ tập tin excel.";
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // Common_XemLoiNhapDuLieuController
            // 
            this.Actions.Add(this.simpleAction1);
            this.Activated += new System.EventHandler(this.Common_XemLoiNhapDuLieuController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
