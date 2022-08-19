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
using System.Windows.Forms;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using ERP.Module.Commons;
using ERP.Module.HeThong;
using ERP.NormalizationData;
using DevExpress.ExpressApp.Security;

namespace ERP.Module.Win.Controllers.HeThong
{
    public partial class HeThong_LayMatKhauTruocKhiThayDoi : ObjectViewController<DetailView, ChangePasswordOnLogonParameters>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            Validator.RuleSet.ValidationCompleted += RuleSet_ValidationCompleted;
        }
        void RuleSet_ValidationCompleted(object sender, ValidationCompletedEventArgs e)
        {
            ChangePasswordOnLogonParameters view = View.CurrentObject as ChangePasswordOnLogonParameters;
            if (e.Exception != null)
            {
                e.Exception.ObjectHeaderFormat = "";
            }
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            Validator.RuleSet.ValidationCompleted -= RuleSet_ValidationCompleted;
        }

    }
}
