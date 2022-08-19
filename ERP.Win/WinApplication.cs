using System;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win;
using System.Collections.Generic;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Editors;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;

namespace ERP.Win {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/DevExpressExpressAppWinWinApplicationMembersTopicAll.aspx
    public partial class ERPWindowsFormsApplication : WinApplication {
        public ERPWindowsFormsApplication() {
            InitializeComponent();
            LinkNewObjectToParentImmediately = false;
        }
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            args.ObjectSpaceProviders.Add(new XPObjectSpaceProvider(args.ConnectionString, args.Connection, false));
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));

        }
        private void ERPWindowsFormsApplication_CustomizeLanguagesList(object sender, CustomizeLanguagesListEventArgs e) {
            string userLanguageName = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            if(userLanguageName != "en-US" && e.Languages.IndexOf(userLanguageName) == -1) {
                e.Languages.Add(userLanguageName);
            }
        }
        private void ERPWindowsFormsApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
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
        
        private void ERPWindowsFormsApplication_ViewCreated(object sender, ViewCreatedEventArgs e)
        {

            if (e.View is DetailView && e.View.Id.Equals("AuthenticationStandardLogonParameters_DetailView"))
            {
                //Nhớ tạo một StaticImage trên model thì ở đây mới tìm thấy
                StaticImage item = ((DetailView)e.View).FindItem("LogoBaner") as StaticImage;
                if (item == null) return;
                //
                string companyKey = Config.CompanyKey;

                if (companyKey.Equals("TTC"))
                {
                    item.ImageName = "BO_LogoIGC";
                }
                else
                {
                    item.ImageName = "BO_LogoDefault";
                }
            }
        }

        private void ERPWindowsFormsApplication_CustomizeTemplate(object sender, CustomizeTemplateEventArgs e)
        {

            if (!DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName.Equals(Config.SkinName))
            {
                //Đăng ký giao điện mặc định của phần mền
                DevExpress.UserSkins.BonusSkins.Register();
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = Config.SkinName;
            }
        }

        private void ERPWindowsFormsApplication_CustomizeFormattingCulture(object sender, CustomizeFormattingCultureEventArgs e)
        {

            //Format ngày
            e.FormattingCulture.DateTimeFormat.DateSeparator = "/";
            e.FormattingCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            e.FormattingCulture.DateTimeFormat.LongDatePattern = "dddd, 'Ngày' dd 'tháng' MM 'năm' yyyy";

            //Format thứ, ngày tháng năm
            e.FormattingCulture.DateTimeFormat.AbbreviatedDayNames = new string[] { "CN", "T2", "T3", "T4", "T5", "T6", "T7" };
            e.FormattingCulture.DateTimeFormat.ShortestDayNames = new string[] { "CN", "T2", "T3", "T4", "T5", "T6", "T7" };
            e.FormattingCulture.DateTimeFormat.DayNames = new string[] { "Chủ Nhật", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
            e.FormattingCulture.DateTimeFormat.AbbreviatedMonthGenitiveNames = new string[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12", "" };
            e.FormattingCulture.DateTimeFormat.AbbreviatedMonthNames = e.FormattingCulture.DateTimeFormat.AbbreviatedMonthGenitiveNames;
            e.FormattingCulture.DateTimeFormat.MonthGenitiveNames = e.FormattingCulture.DateTimeFormat.AbbreviatedMonthNames;
            e.FormattingCulture.DateTimeFormat.MonthGenitiveNames = e.FormattingCulture.DateTimeFormat.AbbreviatedMonthNames;
            e.FormattingCulture.DateTimeFormat.MonthNames = e.FormattingCulture.DateTimeFormat.AbbreviatedMonthNames;
            e.FormattingCulture.DateTimeFormat.YearMonthPattern = "MMMM / 'năm' yyyy";

            //Format số
            e.FormattingCulture.NumberFormat.NumberDecimalDigits = 0;
            //e.FormattingCulture.NumberFormat.NumberDecimalSeparator = ",";
            e.FormattingCulture.NumberFormat.NumberDecimalSeparator = ",";
            e.FormattingCulture.NumberFormat.NumberGroupSeparator = ".";
            e.FormattingCulture.NumberFormat.CurrencyDecimalDigits = 0;
            e.FormattingCulture.NumberFormat.CurrencyDecimalSeparator = ",";
            e.FormattingCulture.NumberFormat.CurrencyGroupSeparator = ".";
            e.FormattingCulture.NumberFormat.CurrencySymbol = ",";
        }

    }
}
