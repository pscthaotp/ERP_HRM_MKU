using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using DevExpress.Utils;
using System.Data.SqlClient;
using DevExpress.Web;
using DevExpress.ExpressApp.Web;
using ERP.Module.Commons;
using ERP.Module.HeThong;

namespace ERP.Module.Web.Controllers.HeThong
{
    public partial class HeTHong_EditReportRoleTreeListController : ViewController
    {
        IObjectSpace _obs = null;
        WebApplication _application = WebApplication.Instance;
        //
        public HeTHong_EditReportRoleTreeListController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        private void HeTHong_EditReportRoleTreeListController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<SecuritySystemRole_Report>();
        }
        private void simpleAction1_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            _obs = _application.CreateObjectSpace();
            //
            if (View.SelectedObjects[0] != null)
            {
                Guid oid = (View.SelectedObjects[0] as SecuritySystemRole_Report).Oid;
                //
                if (oid != null)
                {
                    Common.OidCustomList = new List<Guid>();
                    //
                    if (!Common.OidCustomList.Contains(oid))
                    {
                        Common.OidCustomList.Add(oid);
                    }
                    //
                    e.ShowViewParameters.CreatedView = Application.CreateDashboardView(_obs, "SecuritySystemRole_Report_DashboardView", true);
                    e.ShowViewParameters.Context = TemplateContext.View;
                    e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                }
            }
        }
    }
}
