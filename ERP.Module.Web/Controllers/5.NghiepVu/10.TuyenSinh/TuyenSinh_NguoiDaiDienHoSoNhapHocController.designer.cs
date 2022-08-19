using System;
using System.ComponentModel;
using ERP.Module.NghiepVu.TuyenSinh;

namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    partial class TuyenSinh_NguoiDaiDienHoSoNhapHocController
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
            this.simpleAction1.Caption = "Người đại diện";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "TuyenSinh_NguoiDaiDienHoSoNhapHocController";
            this.simpleAction1.ImageName = "BO_Documents7";
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Nested;
            this.simpleAction1.TargetObjectType = typeof(ERP.Module.NghiepVu.HocSinh.HoSoNhapHocs.GiaDinhTre);
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.simpleAction1.ToolTip = "Chọn người đại diện chính của học sinh.";
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // TuyenSinh_NguoiDaiDienHoSoNhapHocController
            // 
            this.Actions.Add(this.simpleAction1);
            this.Activated += new System.EventHandler(this.TuyenSinh_NguoiDaiDienHoSoNhapHocController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
