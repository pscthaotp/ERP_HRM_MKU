using ERP.Module.NonPersistentObjects.NhanSu;
using System;
using System.ComponentModel;

namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.NhanSu
{
    partial class NhanVien_ImportQuanHeGiaDinhController
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
            this.simpleAction1.Caption = "Thực thi";
            this.simpleAction1.Id = "NhanVien_ImportQuanHeGiaDinhController";
            this.simpleAction1.ImageName = "BO_Lock";
            this.simpleAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction1.Tag = null;
            this.simpleAction1.TargetObjectType = typeof(NhanVien_ChonNhanVien);
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.ToolTip = "Tiến hành nhập dữ liệu từ tập tin excel";
            this.simpleAction1.TypeOfView = null;
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            this.Activated += new EventHandler(this.NhanVien_ImportQuanHeGiaDinhController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
