using ERP.Module.HeThong;
using System;
using System.ComponentModel;

namespace ERP.Module.Web.Controllers.HeThong
{
    partial class HeThong_ChangePassController
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
            this.simpleAction1.Caption = "Đổi mật khẩu";
            this.simpleAction1.Id = "HeThong_ChangePassController";
            this.simpleAction1.ImageName = "BO_TienIch";
            this.simpleAction1.Tag = null;
            this.simpleAction1.TargetObjectType = typeof(SecuritySystemUser_Custom);
            this.simpleAction1.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.simpleAction1.ToolTip = "Cho phép đổi mật khẩu khi đăng nhập.";
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
