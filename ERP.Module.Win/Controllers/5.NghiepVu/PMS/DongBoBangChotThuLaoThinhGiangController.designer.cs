using System;
using System.ComponentModel;
using ERP.Module.NghiepVu.TienLuong.ChungTus;
using ERP.Module.NghiepVu.PMS.QuanLyGiangDay;
using ERP.Module.NghiepVu.PMS.BangChotThuLao;

namespace ERP.Module.Win.Controllers.NghiepVu.PMS
{
    partial class DongBoBangChotThuLaoThinhGiangController
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
            this.simpleAction2 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // simpleAction1
            // 
            this.simpleAction1.Caption = "Chốt thù lao";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "DongBoBangChotThuLaoTGController";
            this.simpleAction1.ImageName = "TemplatesV2Images.BO_Opportunity";
            this.simpleAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction1.TargetObjectType = typeof(ERP.Module.NghiepVu.PMS.BangChotThuLao.BangChotThuLao_ThinhGiang);
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction1.ToolTip = "Chốt thù lao giảng dạy.";
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // simpleAction2
            // 
            this.simpleAction2.Caption = "Mở khóa bảng chốt";
            this.simpleAction2.ConfirmationMessage = null;
            this.simpleAction2.Id = "MoKhoaBangChotThuLaoTGController";
            this.simpleAction2.ImageName = "Action_Security_ChangePassword";
            this.simpleAction2.ToolTip = "Mở khóa thù lao giảng dạy.";
            this.simpleAction2.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction2.TargetObjectType = typeof(ERP.Module.NghiepVu.PMS.BangChotThuLao.BangChotThuLao_ThinhGiang);
            this.simpleAction2.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction2.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction2.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction2_Execute);
            // 
            // DongBoBangChotThuLaoController
            // 
            this.Actions.Add(this.simpleAction1);
            this.Actions.Add(this.simpleAction2);
            this.Activated += new System.EventHandler(this.DongBoBangChotThuLaoThinhGiangController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction2;
    }
}
