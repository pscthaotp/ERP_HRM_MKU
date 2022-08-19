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
using ERP.Module.WebAPI.Models;
using System.Net;
using ERP.Module.WebAPI;
using System.Runtime.Serialization.Json;

namespace ERP.Module.Controllers.HeThong
{
    public partial class HeThong_SaveOnReset : ResetPasswordController
    {
        string _NewPassword;

        public HeThong_SaveOnReset()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            //
            CustomResetPassword += HeThong_SaveOnReset_CustomResetPassword;
            ResetPasswordAction.ExecuteCompleted += ResetPasswordAction_ExecuteCompleted;
        }

        void ResetPasswordAction_ExecuteCompleted(object sender, ActionBaseEventArgs e)
        {
            IObjectSpace obs = Application.CreateObjectSpace();
            SecuritySystemUser_Custom user = obs.GetObjectByKey<SecuritySystemUser_Custom>(((SecuritySystemUser_Custom)View.CurrentObject).Oid);
            user.Password = _NewPassword;
            obs.CommitChanges();
        }

        void HeThong_SaveOnReset_CustomResetPassword(object sender, CustomResetPasswordEventArgs e)
        {
            _NewPassword = e.ResetPasswordParameters.Password;
        }
    }
}
