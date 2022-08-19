using System;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.Web;
using System.Collections.Generic;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.HeThong;
using ERP.Module.Web.Controllers.HeThong;
using ERP.Module.Enum.Systems;

namespace ERP.Web {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/DevExpressExpressAppWebWebApplicationMembersTopicAll.aspx
    public partial class ERPAspNetApplication : WebApplication {
        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule module2;
        private ERP.Module.ERPModule module3;
        private ERP.Module.Web.ERPAspNetModule module4;
        private DevExpress.ExpressApp.AuditTrail.AuditTrailModule auditTrailModule;
        private DevExpress.ExpressApp.Chart.ChartModule chartModule;
        private DevExpress.ExpressApp.Chart.Web.ChartAspNetModule chartAspNetModule;
        private DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule conditionalAppearanceModule;
        private DevExpress.ExpressApp.FileAttachments.Web.FileAttachmentsAspNetModule fileAttachmentsAspNetModule;
        private DevExpress.ExpressApp.ReportsV2.ReportsModuleV2 reportsModuleV2;
        private DevExpress.ExpressApp.ReportsV2.Web.ReportsAspNetModuleV2 reportsAspNetModuleV2;
        private DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase treeListEditorsModuleBase;
        private DevExpress.ExpressApp.TreeListEditors.Web.TreeListEditorsAspNetModule treeListEditorsAspNetModule;
        private DevExpress.ExpressApp.Validation.ValidationModule validationModule;
        private DevExpress.ExpressApp.Scheduler.SchedulerModuleBase schedulerModuleBase1;
        private DevExpress.ExpressApp.Scheduler.Win.SchedulerWindowsFormsModule schedulerWindowsFormsModule1;
        private DevExpress.ExpressApp.Security.SecurityStrategyComplex securityStrategyComplex1;
        private AuthenticationStandard_CustomWeb authenticationStandard1;
        private DevExpress.ExpressApp.Security.SecurityModule securityModule1;
        private DevExpress.ExpressApp.Reports.Web.ReportsAspNetModule reportsAspNetModule1;
        private DevExpress.ExpressApp.Reports.ReportsModule reportsModule1;
        private DevExpress.ExpressApp.CloneObject.CloneObjectModule cloneObjectModule1;
        private DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule validationAspNetModule;

        public ERPAspNetApplication() {
            InitializeComponent();
            LinkNewObjectToParentImmediately = false;
            DevExpress.ExpressApp.Web.Editors.ASPx.ASPxGridListEditor.AllowFilterControlHierarchy = true;
            DevExpress.ExpressApp.Web.Editors.ASPx.ASPxGridListEditor.MaxFilterControlHierarchyDepth = 3;
            DevExpress.ExpressApp.Web.Editors.ASPx.ASPxCriteriaPropertyEditor.AllowFilterControlHierarchyDefault = true;
            DevExpress.ExpressApp.Web.Editors.ASPx.ASPxCriteriaPropertyEditor.MaxHierarchyDepthDefault = 3;
        }
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            args.ObjectSpaceProvider = new XPObjectSpaceProvider(GetDataStoreProvider(args.ConnectionString, args.Connection), true);
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
            // fix lỗi changed by another 
            //https://supportcenter.devexpress.com/ticket/details/t605267/the-object-you-are-trying-to-save-was-changed-by-another-user-please-refresh-data
            args.ObjectSpaceProviders.Add(new CustomXPObjectSpaceProvider(args.ConnectionString, args.Connection));
        }
        private IXpoDataStoreProvider GetDataStoreProvider(string connectionString, System.Data.IDbConnection connection) {
            System.Web.HttpApplicationState application = (System.Web.HttpContext.Current != null) ? System.Web.HttpContext.Current.Application : null;
            IXpoDataStoreProvider dataStoreProvider = null;
            if(application != null && application["DataStoreProvider"] != null) {
                dataStoreProvider = application["DataStoreProvider"] as IXpoDataStoreProvider;
            }
            else {
                if(!String.IsNullOrEmpty(connectionString)) {
                    connectionString = DevExpress.Xpo.XpoDefault.GetConnectionPoolString(connectionString);
                    dataStoreProvider = new ConnectionStringDataStoreProvider(connectionString, true);
                }
                else if(connection != null) {
                    dataStoreProvider = new ConnectionDataStoreProvider(connection);
                }
                if(application != null) {
                    application["DataStoreProvider"] = dataStoreProvider;
                }
            }
			return dataStoreProvider;
        }
        private void ERPAspNetApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            if(System.Diagnostics.Debugger.IsAttached) {
                e.Updater.Update();
                e.Handled = true;
            }
            else {
				string message = "The application cannot connect to the specified database, " +
					"because the database doesn't exist, its version is older " +
					"than that of the application or its schema does not match " +
					"the ORM data model structure. To avoid this error, use one " +
					"of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

                if(e.CompatibilityError != null && e.CompatibilityError.Exception != null) {
                    message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
                }
                throw new InvalidOperationException(message);
            }
#endif
        }
        private void InitializeComponent() {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule();
            this.module3 = new ERP.Module.ERPModule();
            this.module4 = new ERP.Module.Web.ERPAspNetModule();
            this.auditTrailModule = new DevExpress.ExpressApp.AuditTrail.AuditTrailModule();
            this.chartModule = new DevExpress.ExpressApp.Chart.ChartModule();
            this.chartAspNetModule = new DevExpress.ExpressApp.Chart.Web.ChartAspNetModule();
            this.conditionalAppearanceModule = new DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule();
            this.fileAttachmentsAspNetModule = new DevExpress.ExpressApp.FileAttachments.Web.FileAttachmentsAspNetModule();
            this.reportsModuleV2 = new DevExpress.ExpressApp.ReportsV2.ReportsModuleV2();
            this.reportsAspNetModuleV2 = new DevExpress.ExpressApp.ReportsV2.Web.ReportsAspNetModuleV2();
            this.treeListEditorsModuleBase = new DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase();
            this.treeListEditorsAspNetModule = new DevExpress.ExpressApp.TreeListEditors.Web.TreeListEditorsAspNetModule();
            this.validationModule = new DevExpress.ExpressApp.Validation.ValidationModule();
            this.validationAspNetModule = new DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule();
            this.schedulerModuleBase1 = new DevExpress.ExpressApp.Scheduler.SchedulerModuleBase();
            this.schedulerWindowsFormsModule1 = new DevExpress.ExpressApp.Scheduler.Win.SchedulerWindowsFormsModule();
            this.securityStrategyComplex1 = new DevExpress.ExpressApp.Security.SecurityStrategyComplex();
            this.authenticationStandard1 = new ERP.Module.HeThong.AuthenticationStandard_CustomWeb();
            this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
            this.reportsAspNetModule1 = new DevExpress.ExpressApp.Reports.Web.ReportsAspNetModule();
            this.reportsModule1 = new DevExpress.ExpressApp.Reports.ReportsModule();
            this.cloneObjectModule1 = new DevExpress.ExpressApp.CloneObject.CloneObjectModule();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // auditTrailModule
            // 
            this.auditTrailModule.AuditDataItemPersistentType = typeof(DevExpress.Persistent.BaseImpl.AuditDataItemPersistent);
            // 
            // reportsModuleV2
            // 
            this.reportsModuleV2.EnableInplaceReports = true;
            this.reportsModuleV2.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.ReportDataV2);
            this.reportsModuleV2.ReportStoreMode = DevExpress.ExpressApp.ReportsV2.ReportStoreModes.XML;
            // 
            // reportsAspNetModuleV2
            // 
            this.reportsAspNetModuleV2.ReportViewerType = DevExpress.ExpressApp.ReportsV2.Web.ReportViewerTypes.HTML5;
            // 
            // validationModule
            // 
            this.validationModule.AllowValidationDetailsAccess = true;
            this.validationModule.IgnoreWarningAndInformationRules = false;
            // 
            // securityStrategyComplex1
            // 
            this.securityStrategyComplex1.Authentication = this.authenticationStandard1;
            this.securityStrategyComplex1.RoleType = typeof(DevExpress.ExpressApp.Security.Strategy.SecuritySystemRole);
            this.securityStrategyComplex1.UserType = typeof(DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser);
            // 
            // authenticationStandard1
            // 
            this.authenticationStandard1.LogonParametersType = typeof(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters);
            // 
            // reportsModule1
            // 
            this.reportsModule1.EnableInplaceReports = true;
            this.reportsModule1.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.ReportData);
            // 
            // ERPAspNetApplication
            // 
            this.ApplicationName = "Tuyển sinh - Chăm sóc khách hàng";
            this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
            this.MaxLogonAttemptCount = 10;
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
            this.Modules.Add(this.chartAspNetModule);
            this.Modules.Add(this.fileAttachmentsAspNetModule);
            this.Modules.Add(this.reportsModuleV2);
            this.Modules.Add(this.reportsAspNetModuleV2);
            this.Modules.Add(this.treeListEditorsAspNetModule);
            this.Modules.Add(this.validationAspNetModule);
            this.Modules.Add(this.module4);
            this.Modules.Add(this.securityModule1);
            this.Modules.Add(this.reportsModule1);
            this.Modules.Add(this.reportsAspNetModule1);
            this.Modules.Add(this.cloneObjectModule1);
            this.Security = this.securityStrategyComplex1;
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.ERPAspNetApplication_DatabaseVersionMismatch);
            this.CustomizeLanguage += new System.EventHandler<DevExpress.ExpressApp.CustomizeLanguageEventArgs>(this.SetUserLanguageAspNetApplication_CustomizeLanguage);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            //

            //Code này để khi load form Logon thì nó sẽ gọi controller
            this.CreateCustomLogonWindowControllers += application_CreateCustomLogonWindowControllers;

            //Lấy ngôn ngữ theo user hiện tại
            this.CustomizeLanguage += new EventHandler<DevExpress.ExpressApp.CustomizeLanguageEventArgs>(SetUserLanguageAspNetApplication_CustomizeLanguage);
        }
        
        void application_CreateCustomLogonWindowControllers(object sender, CreateCustomLogonWindowControllersEventArgs e)
        {
            e.Controllers.Add(((XafApplication)sender).CreateController<HeThong_LogonController>());
        }
        void SetUserLanguageAspNetApplication_CustomizeLanguage(object sender, DevExpress.ExpressApp.CustomizeLanguageEventArgs e)
        {
            if (Security.User != null)
            {
                if (AuthenticationStandard_CustomWeb._LoaiNgonNgu == LoaiNgonNguEnum.English)
                    e.LanguageName = "en-EN";
                else if (AuthenticationStandard_CustomWeb._LoaiNgonNgu == LoaiNgonNguEnum.VietNammese)
                    e.LanguageName = "vi-VN";
            }
        }
    }
}
