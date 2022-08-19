using System;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.Custom
{
    partial class ShowReportOnToolbarController
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
            this.singleChoiceAction1 = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // singleChoiceAction1
            // 
            this.singleChoiceAction1.Caption = "B�o c�o";
            this.singleChoiceAction1.ConfirmationMessage = null;
            this.singleChoiceAction1.Id = "ShowReportOnToolbarController";
            this.singleChoiceAction1.ImageName = "BO_Report";
            this.singleChoiceAction1.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.singleChoiceAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            //this.singleChoiceAction1.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.singleChoiceAction1.ToolTip = null;
            //this.singleChoiceAction1.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.singleChoiceAction1.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.singleChoiceAction1_Execute);
            // 
            // ShowReportOnToolbarController
            // 
            this.Actions.Add(this.singleChoiceAction1);
            this.ViewControlsCreated += new EventHandler(this.ShowReportOnToolbarController_ViewControlsCreated);
            this.Activated += new EventHandler(this.ShowReportOnToolbarController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction singleChoiceAction1;
    }
}
