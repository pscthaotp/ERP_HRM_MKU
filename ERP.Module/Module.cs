using System;
using System.Text;
using System.Linq;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Notifications;
using DevExpress.Persistent.Base.General;
using DevExpress.Data.Filtering;
using ERP.Module.Commons;
using DevExpress.XtraBars.Alerter;
using System.Windows.Forms;
using System.Drawing;
using ERP.Module.HeThong;
using ERP.Module.Extends;
using DevExpress.XtraBars.ToastNotifications;
using System.Data.SqlClient;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.ScriptRecorder;
using DevExpress.ExpressApp.ScriptRecorder.Win;

namespace ERP.Module {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppModuleBasetopic.aspx.
    public sealed partial class ERPModule : ModuleBase {
        NotificationsModule notificationsModule;

        public ERPModule() {
            InitializeComponent();
			BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction;
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
        public override void Setup(XafApplication application) {
            base.Setup(application);

            //Viết lại hàm khi login và logout (Lưu ý phải có logoff nếu k sẽ bị chồng hàm, hàm LoggedOn sẽ chạy nhiều lần)
            application.LoggedOn += application_LoggedOn;
            application.LoggedOff += application_LoggedOff;
        }

        public override void CustomizeTypesInfo(ITypesInfo typesInfo) {
            base.CustomizeTypesInfo(typesInfo);
            CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
        }
        void application_LoggedOn(object sender, LogonEventArgs e)
        {
            //Kiểm tra trong ERP.Win.WinApplication xem đã kéo module Notifications hay chưa
            notificationsModule = Application.Modules.FindModule<NotificationsModule>();
            if (notificationsModule != null)
            {
                //Bắt sự kiện khi báo Notification để custom lại
                notificationsModule.NotificationsService.NotificationTriggered += NotificationsService_NotificationTriggered;

                //Xóa class Event dùng trong TKB (vì event đã dùng cho CTKhung, quá nhiều record load k nỗi)
                foreach (var item in notificationsModule.NotificationsService.NotificationsProviders)
                {
                    item.NotificationTypesInfo.RemoveWhere(s => s.DefaultMember != null &&
                                                                s.DefaultMember.Owner.ToString() == "DevExpress.Persistent.BaseImpl.Event");
                }

                //Filter lại Collection theo User hiện tại
                DefaultNotificationsProvider notificationsProvider = notificationsModule.DefaultNotificationsProvider;
                notificationsProvider.CustomizeNotificationCollectionCriteria += notificationsProvider_CustomizeNotificationCollectionCriteria;
            }

        }

        void application_LoggedOff(object sender, EventArgs e)
        {
            //Kiểm tra trong ERP.Win.WinApplication xem đã kéo module Notifications hay chưa
            NotificationsModule notificationsModule = Application.Modules.FindModule<NotificationsModule>();
            if (notificationsModule != null)
            {
                notificationsModule.NotificationsService.NotificationTriggered -= NotificationsService_NotificationTriggered;
            }
            var i = System.Web.HttpContext.Current;
            if (i != null)
            {
                if (DevExpress.ExpressApp.ScriptRecorder.Logger.Instance.Script.ScriptLog != null
                       && AuthenticationStandard_CustomWin.logInOut != null)
                {
                    //Lưu lại ActivitiesLog
                    IObjectSpace obs = Application.CreateObjectSpace();
                    LogInOut log = obs.GetObjectByKey<LogInOut>(AuthenticationStandard_CustomWin.logInOut.Oid);
                    if (log != null)
                    {
                        //Replace password lại
                        log.ActivitiesLog = DateTime.Now.ToString() + " - " + DevExpress.ExpressApp.ScriptRecorder.Logger.Instance.Script.ScriptLog;
                        obs.CommitChanges();
                    }
                }
            }
        }

        void NotificationsService_NotificationTriggered(object sender, NotificationItemsEventArgs e)
        {
            //Duyệt qua các thông báo hiện tại để tạo popup
            //foreach (var item in e.NotificationItems)
            {
                AlertControl alertControl1 = new AlertControl();
                alertControl1.AutoFormDelay = 60000 * 3; //Cho hiển thị 3 phút
                //Tạo nút
                AlertButton btn2 = new AlertButton((Image)global::ERP.Module.Properties.Resources.ring);
                btn2.Hint = "Đã xem";
                //
                alertControl1.Buttons.Add(btn2);
                alertControl1.ButtonClick += alertControl1_ButtonClick; //Nhấp vào nút
                alertControl1.AlertClick += alertControl1_AlertClick; //Nhấp vào form thông báo
                //Show thông báo
                alertControl1.Show(null, "Thông báo", "Bạn có " + e.NotificationItems.Count + " thông báo mới");
            }
            //Tắt code xử lý mặc định của NotificationTriggered
            e.Handled = true;
        }

        void alertControl1_ButtonClick(object sender, AlertButtonClickEventArgs e)
        {
            //Dùng để khi nhấp vào nút đã xem thì tắt thông báo
            ((AlertControl)sender).AlertFormList[0].Close();

            //Gọi store tắt hết tất cả thông báo của user
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SecuritySystemUser", Common.SecuritySystemUser_GetCurrentUser().Oid);
            DataProvider.ExecuteNonQuery("spd_ThongBao_TatThongBaoUser", System.Data.CommandType.StoredProcedure, param);

            //Gọi hàm RefreshNotifications để refesh lại số thông báo
            if (notificationsModule != null)
                notificationsModule.NotificationsService.RefreshNotifications();
        }

        void alertControl1_AlertClick(object sender, AlertClickEventArgs e)
        {
            ((AlertControl)sender).AlertFormList[0].Close();

            IObjectSpace ios = Application.CreateObjectSpace();
            CollectionSource collectionSource = new CollectionSource(ios, typeof(ThongBao));
            collectionSource.Criteria.Add("", CriteriaOperator.Parse("SecuritySystemUser =? and AlarmTime is not null", Common.SecuritySystemUser_GetCurrentUser().Oid));
            
            ShowViewParameters showView = new ShowViewParameters();
            showView.CreatedView = this.Application.CreateListView(this.Application.GetListViewId(typeof(ThongBao)), collectionSource, false);
            showView.TargetWindow = TargetWindow.NewWindow;
            showView.Context = TemplateContext.PopupWindow;
            showView.CreateAllControllers = false;

            this.Application.ShowViewStrategy.ShowView(showView, new ShowViewSource(null, null));
        }

        void notificationsProvider_CustomizeNotificationCollectionCriteria(
            object sender, CustomizeCollectionCriteriaEventArgs e)
        {
            if (e.Type == typeof(ERP.Module.HeThong.ThongBao))
            {
                //Filter thông báo theo tài khoản
                e.Criteria = CriteriaOperator.Parse("SecuritySystemUser =?", Common.SecuritySystemUser_GetCurrentUser().Oid);
            }
        }

    }
}
