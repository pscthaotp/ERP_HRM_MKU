using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Actions;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;

namespace ERP.Module.Win.Controllers.BaoCao
{
    public partial class Custom_EditReportController : ViewController
    {
        public Custom_EditReportController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "ReportData_Custom_DetailView;ReportData_Custom_ListView";
        }

       private void Custom_EditReportController_Activated(object sender, System.EventArgs e)
        {
            if (Common.TaiKhoanHeThong())
                simpleAction1.Active["TruyCap"] = true;
            else
                simpleAction1.Active["TruyCap"] = false;
        }
        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ReportData_Custom obj = View.CurrentObject as ReportData_Custom;
            if (obj != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                obj = obs.GetObjectByKey<ReportData_Custom>(obj.Oid);
                e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, obj);
                e.ShowViewParameters.TargetWindow = TargetWindow.Default;
            }
        }
    }
}
