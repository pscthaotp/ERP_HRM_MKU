using ERP.Module.HeThong;
using System;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.HeThong
{
    partial class HeThong_ChuanHoaDanhMucController
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
            this.TargetObjectType = typeof(AppDictionaryNormalizationData);            
            // 
            // simpleAction1
            // 
            this.simpleAction1.Caption = "Chuẩn hóa danh mục";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "HeThong_ChuanHoaDanhMucController";
            this.simpleAction1.ImageName = "Action_AddGroup";
            this.simpleAction1.TargetObjectType = typeof(AppDictionaryNormalizationData);
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.simpleAction1.ToolTip = null;
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // BaoMat_ChuanHoaDanhMucController
            // 
            this.Activated += new EventHandler(this.HeThong_ChuanHoaDanhMucController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
