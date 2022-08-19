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
using DevExpress.ExpressApp.Reports;
using ERP.Module.Commons;
using ERP.Module.BaoCao.Custom;

namespace ERP.Module.Web.Controllers.BaoCao
{
    public partial class Rpt_ShowReportController : ViewController
    {
        public Rpt_ShowReportController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        private void Rpt_ShowReportController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<StoreProcedureReport>();
        }
        private void simpleAction1_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        { 
            if (View != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("DataTypeName=?", View.ObjectTypeInfo.ToString());
                ReportData_Custom reportData = View.ObjectSpace.FindObject<ReportData_Custom>(filter);
                if (reportData != null)
                {
                    //Lưu vết lấy data
                    StoreProcedureReport.Param = View.CurrentObject as StoreProcedureReport;

                    //Mở form xem báo cáo
                    e.Action.Controller.Frame.GetController<ReportServiceController>().ShowPreview(typeof(ReportData_Custom), reportData.Oid);
                }
            }
        }
    }
}
