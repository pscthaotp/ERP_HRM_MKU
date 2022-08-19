using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Actions;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;

namespace ERP.Module.Win.Controllers.BaoCao
{
    public partial class Custom_EditReportController_NewVersion : ViewController
    {
        public Custom_EditReportController_NewVersion()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "ReportCustomView_ListView;ReportCustomView_DetailView";//ReportData_Custom_DetailView;ReportData_Custom_ListView;
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ERP.Module.NonPersistentObjects.ReportCustom.ReportCustomView rpt = View.CurrentObject as ERP.Module.NonPersistentObjects.ReportCustom.ReportCustomView;
            //ReportData_Custom obj = View.CurrentObject as ReportData_Custom;
            ReportData_Custom obj = null;
            if (rpt != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                obj = obs.GetObjectByKey<ReportData_Custom>(rpt.OidReport);
                e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, obj);
                e.ShowViewParameters.TargetWindow = TargetWindow.Default;
            }
        }
        private void Custom_EditReportController_NewVersion_Activated(object sender, System.EventArgs e)
        {
            if (Common.TaiKhoanHeThong())
                simpleAction1.Active["TruyCap"] = true;
            else
                simpleAction1.Active["TruyCap"] = false;
        }
    }
}
