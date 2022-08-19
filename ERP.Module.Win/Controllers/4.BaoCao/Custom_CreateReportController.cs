using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Configuration;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Reports.Win;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;

namespace ERP.Module.Win.Controllers.BaoCao
{
    public partial class Custom_CreateReportController : ViewController
    {
        private CreateReport _report;
        private IObjectSpace _obs;
        private StoreProcedureReport _obj;

        public Custom_CreateReportController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "ReportCustomView_ListView;ReportCustomView_DetailView";//ReportData_Custom_DetailView;ReportData_Custom_ListView;
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            _report = _obs.CreateObject<CreateReport>();
            //
            e.View = Application.CreateDetailView(_obs, _report);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //
            _obs = Application.CreateObjectSpace();
            _obj = (StoreProcedureReport)_obs.CreateObject(_report.DataType);
            //
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, _obj);
            e.ShowViewParameters.Context = TemplateContext.PopupWindow;
            DialogController ct = new DialogController();
            ct.AcceptAction.Caption = "OK";
            e.ShowViewParameters.Controllers.Add(ct);
            ct.AcceptAction.Execute += AcceptAction_Execute;        
        }

        private void AcceptAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)_obs).Session.DataLayer))
            {
                ReportData_Custom rpt = new ReportData_Custom(uow, _report.DataType);
                rpt.ReportName = _report.TenBaoCao;
                rpt.GroupReport = uow.GetObjectByKey<GroupReport>(_report.GroupReport.Oid);
                rpt.CongTy = uow.GetObjectByKey<CongTy>(_report.CongTy.Oid);
                //
                CustomXafReport custom = new CustomXafReport();
                StoreProcedureReport.Param = _obj;
                custom.ReportName = _report.TenBaoCao;
                custom.DataType = _report.DataType;
                custom.TargetType = _report.TargetType;
                //
                rpt.SaveReport(custom);
                uow.CommitChanges();
                Frame.GetController<WinReportServiceController>().ShowDesigner(rpt);
                //
                View.ObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            }
        }

        private void Custom_CreateReportController_Activated(object sender, EventArgs e)
        {
            //string value = ConfigurationManager.AppSettings["EnableCreateReport"];
            //popupWindowShowAction1.Active["TruyCap"] = value == "True";
            if (Common.TaiKhoanHeThong())
                popupWindowShowAction1.Active["TruyCap"] = true;
            else
                popupWindowShowAction1.Active["TruyCap"] = false;
        }
    }
}
