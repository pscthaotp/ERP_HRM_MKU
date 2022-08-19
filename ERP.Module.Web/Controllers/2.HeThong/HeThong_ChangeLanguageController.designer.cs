using System.ComponentModel;
//
namespace ERP.Module.Web.Controllers.HeThong
{
    partial class HeThong_ChangeLanguageController
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
            this.ChooseLanguage = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.ChooseFormattingCulture = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // ChooseLanguage
            // 
            this.ChooseLanguage.Caption = "Choose Language";
            this.ChooseLanguage.Category = "Search";
            this.ChooseLanguage.ConfirmationMessage = null;
            this.ChooseLanguage.Id = "ChooseLanguage";
            this.ChooseLanguage.ToolTip = null;
            this.ChooseLanguage.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.ChooseLanguage_Execute);
            // 
            // ChooseFormattingCulture
            // 
            this.ChooseFormattingCulture.Caption = "Choose Formatting Culture";
            this.ChooseFormattingCulture.Category = "Options";
            this.ChooseFormattingCulture.ConfirmationMessage = null;
            this.ChooseFormattingCulture.Id = "ChooseFormattingCulture";
            this.ChooseFormattingCulture.ToolTip = null;
            this.ChooseFormattingCulture.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.ChooseFormattingCulture_Execute);
            // 
            // HeThong_ChangeLanguageController
            // 
            this.Actions.Add(this.ChooseLanguage);
            this.Actions.Add(this.ChooseFormattingCulture);
            this.TargetWindowType = DevExpress.ExpressApp.WindowType.Main;

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction ChooseLanguage;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction ChooseFormattingCulture;
    }
}
