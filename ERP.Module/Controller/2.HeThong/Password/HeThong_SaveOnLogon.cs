using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Security;
using ERP.Module.HeThong;
using DevExpress.Xpo;
using ERP.Module.WebAPI.Models;
using System.Net;
using ERP.Module.WebAPI;
using System.Runtime.Serialization.Json;
using System.Data.SqlClient;
using ERP.Module.Commons;
using System.Data;

namespace ERP.Module.Controllers.HeThong
{
    public partial class HeThong_SaveOnLogon : ViewController
    {
        IObjectSpace _obs;
        string _NewPassword;

        public HeThong_SaveOnLogon()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "ChangePasswordOnLogonParameters_DetailView;ChangePasswordParameters_DetailView";
        }

        protected override void OnViewControlsCreated()
        {
            _obs = Application.CreateObjectSpace();
            if (View.Id == "ChangePasswordOnLogonParameters_DetailView") //Stick chọn thay đổi mật khẩu khi logon
            {
                SecurityModule SecurityModule = this.Application.Modules.FindModule<SecurityModule>();
                SecurityModule.CustomChangePasswordOnLogon += SecurityModule_CustomChangePasswordOnLogon;
                SecurityModule.ChangePasswordOnLogonAction.ExecuteCompleted += ChangePasswordAction_ExecuteCompleted;
            }
            else if (View.Id == "ChangePasswordParameters_DetailView") //Đổi mật khẩu
            {
                SecurityModule SecurityModule = this.Application.Modules.FindModule<SecurityModule>();
                SecurityModule.CustomUpdateLogonParameters += SecurityModule_CustomUpdateLogonParameters;
            }
        }

        void SecurityModule_CustomUpdateLogonParameters(object sender, CustomUpdateLogonParametersEventArgs e)
        {
            _obs.Refresh(); //Bắt buộc phải có thêm cái refesh nếu k sẽ lỗi
            SecuritySystemUser_Custom user = _obs.GetObjectByKey<SecuritySystemUser_Custom>(Commons.Common.SecuritySystemUser_GetCurrentUser().Oid);
            user.Password = e.NewPassword;
            _obs.CommitChanges();
            
            _obs.Refresh();
            //
        }

        void SecurityModule_CustomChangePasswordOnLogon(object sender, CustomChangePasswordOnLogonEventArgs e)
        {
            _NewPassword = e.LogonPasswordParameters.NewPassword;
        }

        void ChangePasswordAction_ExecuteCompleted(object sender, ActionBaseEventArgs e)
        {
            _obs.Refresh(); //Bắt buộc phải có thêm cái refesh nếu k sẽ lỗi
            SecuritySystemUser_Custom user = _obs.GetObjectByKey<SecuritySystemUser_Custom>(Commons.Common.SecuritySystemUser_GetCurrentUser().Oid);
            user.Password = _NewPassword;
            _obs.CommitChanges();
            //
        }

    }
}
