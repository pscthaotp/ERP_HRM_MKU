
using ERP.Module.NghiepVu.HocSinh.HoSoNhapHocs;
using System;
using System.ComponentModel;

namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    partial class TuyenSinh_MoTapTinController
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
            this.simpleAction1 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // simpleAction1
            // 
            this.simpleAction1.Caption = "Mở dữ liệu";
            this.simpleAction1.Id = "TuyenSinh_MoTapTinController";
            this.simpleAction1.ImageName = "Action_Report_Object_Inplace_Preview";
            this.simpleAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction1.Tag = null;
            this.simpleAction1.TargetObjectType = typeof(HoSoNhapHoc_TapTin);
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Nested;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.simpleAction1.ToolTip = "Xem chi tiết tập tin đã tải.";
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            this.Activated += new EventHandler(this.TuyenSinh_MoTapTinController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
