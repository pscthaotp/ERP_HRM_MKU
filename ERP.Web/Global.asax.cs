using System;
using System.Configuration;
using System.Web.Configuration;
using System.Web;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Web;
using DevExpress.Web;
using ERP.Module.Commons;
using ERP.Module.DatabaseUpdate;
using DevExpress.ExpressApp.Xpo;

namespace ERP.Web
{
    public class Global : System.Web.HttpApplication
    {

        private static DataStoreProxyProvider _provider;
        public Global()
        {
            InitializeComponent();
        }
        protected void Application_Start(Object sender, EventArgs e)
        {
            ASPxWebControl.CallbackError += new EventHandler(Application_Error);
            WebApplication.EnableMultipleBrowserTabsSupport = true;
#if EASYTEST
            DevExpress.ExpressApp.Web.TestScripts.TestScriptsManager.EasyTestEnabled = true;
#endif
        }
        protected void Session_Start(Object sender, EventArgs e)
        {
            Tracing.Initialize();
            WebApplication.SetInstance(Session, new ERPAspNetApplication());
            DevExpress.ExpressApp.Web.Templates.DefaultVerticalTemplateContentNew.ClearSizeLimit();
            //WebApplication.Instance.SwitchToNewStyle();

            //Cấu hình kết nối nhiều db
            /*
            WebApplication.Instance.CreateCustomObjectSpaceProvider += new EventHandler<CreateCustomObjectSpaceProviderEventArgs>(application_CreateCustomObjectSpaceProvider);
            WebApplication.Instance.CustomCheckCompatibility += new EventHandler<CustomCheckCompatibilityEventArgs>(application_CustomCheckCompatibility);
            */

            //Lấy chuỗi kết nối
            string connectionString = DataProvider.GetConnectionString();
            if (!string.IsNullOrEmpty(connectionString))
                WebApplication.Instance.ConnectionString = connectionString;
            //
#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
                WebApplication.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#endif
            if (System.Diagnostics.Debugger.IsAttached && WebApplication.Instance.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema)
            {
                WebApplication.Instance.DatabaseUpdateMode = DatabaseUpdateMode.Never;
            }
            WebApplication.Instance.Setup();
            WebApplication.Instance.Start();
        }
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
        }
        protected void Application_EndRequest(Object sender, EventArgs e)
        {
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            ErrorHandling.Instance.ProcessApplicationError();
        }
        protected void Session_End(Object sender, EventArgs e)
        {
            WebApplication.LogOff(Session);
            WebApplication.DisposeInstance(Session);
        }
        protected void Application_End(Object sender, EventArgs e)
        {
        }
        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion

        static void application_CreateCustomObjectSpaceProvider(object sender, CreateCustomObjectSpaceProviderEventArgs e)
        {
            if (_provider == null)
            {
                _provider = new DataStoreProxyProvider();
            }
            e.ObjectSpaceProvider = new XPObjectSpaceProvider(_provider);
        }

        static void application_CustomCheckCompatibility(object sender, CustomCheckCompatibilityEventArgs e)
        {
            if (_provider != null && !_provider.IsInitialized)
            {
                string connectionStringDefault = DataProvider.GetConnectionString();
                string connectionStringAccountsFee = DataProvider.GetConnectionStringOrther("SIS");
                string connectionStringSIS = string.Empty;//DataProvider.GetConnectionStringOrther("SIS_CIS");
                //
                if (!String.IsNullOrEmpty(connectionStringDefault) && !String.IsNullOrEmpty(connectionStringAccountsFee) && !String.IsNullOrEmpty(connectionStringSIS))
                {
                    _provider.Initialize(((XPObjectSpaceProvider)e.ObjectSpaceProvider).XPDictionary,
                        connectionStringDefault,
                        connectionStringAccountsFee,
                        connectionStringSIS);
                }
                if (!String.IsNullOrEmpty(connectionStringDefault) && !String.IsNullOrEmpty(connectionStringAccountsFee))
                {
                    _provider.Initialize(((XPObjectSpaceProvider)e.ObjectSpaceProvider).XPDictionary,
                        connectionStringDefault,
                        connectionStringAccountsFee);
                }
                else
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Không lấy được chuỗi kết nối!!!')");
                }
            }
        }
    }
}
