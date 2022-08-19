using ERP.Module.HeThong;
namespace ERP.Win {
    partial class ERPWindowsFormsApplication {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.authenticationStandard_CustomWin2 = new ERP.Module.HeThong.AuthenticationStandard_CustomWin();
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule();
            this.module3 = new ERP.Module.ERPModule();
            this.module4 = new ERP.Module.Win.ERPWindowsFormsModule();
            this.auditTrailModule = new DevExpress.ExpressApp.AuditTrail.AuditTrailModule();
            this.chartModule = new DevExpress.ExpressApp.Chart.ChartModule();
            this.chartWindowsFormsModule = new DevExpress.ExpressApp.Chart.Win.ChartWindowsFormsModule();
            this.conditionalAppearanceModule = new DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule();
            this.fileAttachmentsWindowsFormsModule = new DevExpress.ExpressApp.FileAttachments.Win.FileAttachmentsWindowsFormsModule();
            this.treeListEditorsModuleBase = new DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase();
            this.treeListEditorsWindowsFormsModule = new DevExpress.ExpressApp.TreeListEditors.Win.TreeListEditorsWindowsFormsModule();
            this.validationModule = new DevExpress.ExpressApp.Validation.ValidationModule();
            this.validationWindowsFormsModule = new DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule();
            this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
            this.reportsModule1 = new DevExpress.ExpressApp.Reports.ReportsModule();
            this.reportsWindowsFormsModule1 = new DevExpress.ExpressApp.Reports.Win.ReportsWindowsFormsModule();
            this.schedulerWindowsFormsModule1 = new DevExpress.ExpressApp.Scheduler.Win.SchedulerWindowsFormsModule();
            this.schedulerModuleBase1 = new DevExpress.ExpressApp.Scheduler.SchedulerModuleBase();
            this.cloneObjectModule1 = new DevExpress.ExpressApp.CloneObject.CloneObjectModule();
            this.pivotGridModule1 = new DevExpress.ExpressApp.PivotGrid.PivotGridModule();
            this.pivotGridWindowsFormsModule1 = new DevExpress.ExpressApp.PivotGrid.Win.PivotGridWindowsFormsModule();
            this.notificationsWindowsFormsModule1 = new DevExpress.ExpressApp.Notifications.Win.NotificationsWindowsFormsModule();
            this.notificationsModule1 = new DevExpress.ExpressApp.Notifications.NotificationsModule();
            this.scriptRecorderWindowsFormsModule1 = new DevExpress.ExpressApp.ScriptRecorder.Win.ScriptRecorderWindowsFormsModule();
            this.scriptRecorderModuleBase1 = new DevExpress.ExpressApp.ScriptRecorder.ScriptRecorderModuleBase();
            this.securityStrategyComplex1 = new DevExpress.ExpressApp.Security.SecurityStrategyComplex();
            this.reportsModuleV21 = new DevExpress.ExpressApp.ReportsV2.ReportsModuleV2();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // authenticationStandard_CustomWin2
            // 
            this.authenticationStandard_CustomWin2.LogonParametersType = typeof(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters);
            // 
            // auditTrailModule
            // 
            this.auditTrailModule.AuditDataItemPersistentType = typeof(DevExpress.Persistent.BaseImpl.AuditDataItemPersistent);
            // 
            // validationModule
            // 
            this.validationModule.AllowValidationDetailsAccess = true;
            this.validationModule.IgnoreWarningAndInformationRules = false;
            // 
            // reportsModule1
            // 
            this.reportsModule1.EnableInplaceReports = true;
            this.reportsModule1.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.ReportData);
            // 
            // notificationsModule1
            // 
            this.notificationsModule1.CanAccessPostponedItems = false;
            this.notificationsModule1.NotificationsRefreshInterval = System.TimeSpan.Parse("00:05:00");
            this.notificationsModule1.NotificationsStartDelay = System.TimeSpan.Parse("00:00:05");
            this.notificationsModule1.ShowDismissAllAction = false;
            this.notificationsModule1.ShowNotificationsWindow = true;
            this.notificationsModule1.ShowRefreshAction = false;
            // 
            // securityStrategyComplex1
            // 
            this.securityStrategyComplex1.Authentication = this.authenticationStandard_CustomWin2;
            this.securityStrategyComplex1.RoleType = typeof(ERP.Module.HeThong.SecuritySystemRole_Custom);
            this.securityStrategyComplex1.UserType = typeof(ERP.Module.HeThong.SecuritySystemUser_Custom);
            // 
            // reportsModuleV21
            // 
            this.reportsModuleV21.EnableInplaceReports = true;
            this.reportsModuleV21.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.ReportDataV2);
            // 
            // ERPWindowsFormsApplication
            // 
            this.ApplicationName = "ERP";
            this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.auditTrailModule);
            this.Modules.Add(this.chartModule);
            this.Modules.Add(this.conditionalAppearanceModule);
            this.Modules.Add(this.treeListEditorsModuleBase);
            this.Modules.Add(this.validationModule);
            this.Modules.Add(this.schedulerModuleBase1);
            this.Modules.Add(this.schedulerWindowsFormsModule1);
            this.Modules.Add(this.module3);
            this.Modules.Add(this.chartWindowsFormsModule);
            this.Modules.Add(this.fileAttachmentsWindowsFormsModule);
            this.Modules.Add(this.treeListEditorsWindowsFormsModule);
            this.Modules.Add(this.validationWindowsFormsModule);
            this.Modules.Add(this.cloneObjectModule1);
            this.Modules.Add(this.notificationsModule1);
            this.Modules.Add(this.pivotGridModule1);
            this.Modules.Add(this.reportsModuleV21);
            this.Modules.Add(this.module4);
            this.Modules.Add(this.securityModule1);
            this.Modules.Add(this.reportsModule1);
            this.Modules.Add(this.reportsWindowsFormsModule1);
            this.Modules.Add(this.pivotGridWindowsFormsModule1);
            this.Modules.Add(this.notificationsWindowsFormsModule1);
            this.Modules.Add(this.scriptRecorderModuleBase1);
            this.Modules.Add(this.scriptRecorderWindowsFormsModule1);
            this.Security = this.securityStrategyComplex1;
            this.UseOldTemplates = false;
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.ERPWindowsFormsApplication_DatabaseVersionMismatch);
            this.CustomizeFormattingCulture += new System.EventHandler<DevExpress.ExpressApp.CustomizeFormattingCultureEventArgs>(this.ERPWindowsFormsApplication_CustomizeFormattingCulture);
            this.CustomizeTemplate += new System.EventHandler<DevExpress.ExpressApp.CustomizeTemplateEventArgs>(this.ERPWindowsFormsApplication_CustomizeTemplate);
            this.ViewCreated += new System.EventHandler<DevExpress.ExpressApp.ViewCreatedEventArgs>(this.ERPWindowsFormsApplication_ViewCreated);
            this.CustomizeLanguagesList += new System.EventHandler<DevExpress.ExpressApp.CustomizeLanguagesListEventArgs>(this.ERPWindowsFormsApplication_CustomizeLanguagesList);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule module2;
        private ERP.Module.ERPModule module3;
        private ERP.Module.Win.ERPWindowsFormsModule module4;
        private DevExpress.ExpressApp.AuditTrail.AuditTrailModule auditTrailModule;
        private DevExpress.ExpressApp.Chart.ChartModule chartModule;
        private DevExpress.ExpressApp.Chart.Win.ChartWindowsFormsModule chartWindowsFormsModule;
        private DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule conditionalAppearanceModule;
        private DevExpress.ExpressApp.FileAttachments.Win.FileAttachmentsWindowsFormsModule fileAttachmentsWindowsFormsModule;
        private DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase treeListEditorsModuleBase;
        private DevExpress.ExpressApp.TreeListEditors.Win.TreeListEditorsWindowsFormsModule treeListEditorsWindowsFormsModule;
        private DevExpress.ExpressApp.Validation.ValidationModule validationModule;
        private DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule validationWindowsFormsModule;
        private DevExpress.ExpressApp.Security.SecurityModule securityModule1;
        private DevExpress.ExpressApp.Reports.ReportsModule reportsModule1;
        private DevExpress.ExpressApp.Reports.Win.ReportsWindowsFormsModule reportsWindowsFormsModule1;
        private DevExpress.ExpressApp.Scheduler.Win.SchedulerWindowsFormsModule schedulerWindowsFormsModule1;
        private DevExpress.ExpressApp.Scheduler.SchedulerModuleBase schedulerModuleBase1;
        private DevExpress.ExpressApp.CloneObject.CloneObjectModule cloneObjectModule1;
        private DevExpress.ExpressApp.PivotGrid.PivotGridModule pivotGridModule1;
        private DevExpress.ExpressApp.PivotGrid.Win.PivotGridWindowsFormsModule pivotGridWindowsFormsModule1;
        private Module.HeThong.AuthenticationStandard_CustomWin authenticationStandard_CustomWin2;
        private DevExpress.ExpressApp.Notifications.Win.NotificationsWindowsFormsModule notificationsWindowsFormsModule1;
        private DevExpress.ExpressApp.Notifications.NotificationsModule notificationsModule1;
        private DevExpress.ExpressApp.ScriptRecorder.Win.ScriptRecorderWindowsFormsModule scriptRecorderWindowsFormsModule1;
        private DevExpress.ExpressApp.ScriptRecorder.ScriptRecorderModuleBase scriptRecorderModuleBase1;
        private DevExpress.ExpressApp.Security.SecurityStrategyComplex securityStrategyComplex1;
        private DevExpress.ExpressApp.ReportsV2.ReportsModuleV2 reportsModuleV21;
    }
}
