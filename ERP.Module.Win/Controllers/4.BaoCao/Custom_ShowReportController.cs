using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;

namespace ERP.Module.Win.Controllers.BaoCao
{
    public partial class Custom_ShowReportController : DevExpress.ExpressApp.Reports.Win.ReportsController
    {
        private SimpleActionExecuteEventArgs action;
        //
        public Custom_ShowReportController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void ShowReportPreview(SimpleActionExecuteEventArgs args)
        {
            string dataTypeName = (args.CurrentObject as ReportData_Custom).DataTypeName;
            //
            Type type = DevExpress.Persistent.Base.ReflectionHelper.GetType(dataTypeName);
            if (type != null && type.BaseType == typeof(StoreProcedureReport))
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                object source = obs.CreateObject(type);
                //
                args.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, source);
                args.ShowViewParameters.Context = TemplateContext.PopupWindow;
                args.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                var ctrl = new DevExpress.ExpressApp.SystemModule.DialogController();
                args.ShowViewParameters.Controllers.Add(ctrl);
                ctrl.AcceptAction.Execute += Preview_Execute;
                ctrl.CanCloseWindow = true;
                action = args;
            }
            else
                base.ShowReportPreview(args);
        }

        protected override void ShowReportDesigner(SimpleActionExecuteEventArgs args)
        {
            string dataTypeName = (args.CurrentObject as ReportData_Custom).DataTypeName;
            Type type = DevExpress.Persistent.Base.ReflectionHelper.GetType(dataTypeName);
            //
            if (type != null && type.BaseType == typeof(StoreProcedureReport))
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                object source = obs.CreateObject(type);
                //
                args.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, source);
                args.ShowViewParameters.Context = TemplateContext.PopupWindow;
                args.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                var ctrl = new DevExpress.ExpressApp.SystemModule.DialogController();
                args.ShowViewParameters.Controllers.Add(ctrl);
                ctrl.AcceptAction.Execute += Design_Execute;
                ctrl.CanCloseWindow = true;
                action = args;
            }
            else
                base.ShowReportDesigner(args);
        }

        void Design_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            e.Action.Controller.Frame.View.ObjectSpace.CommitChanges();
            StoreProcedureReport.Param = e.CurrentObject as StoreProcedureReport;
            base.ShowReportDesigner(action);
        }

        void Preview_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            e.Action.Controller.Frame.View.ObjectSpace.CommitChanges();
            if (e.CurrentObject.GetType().ToString().Contains("ERP.Module.Report.HocPhi"))
            {
                StoreProcedureReport.Param = (StoreProcedureReport)Common.CopyAll(((XPObjectSpace)ObjectSpace).Session, e.CurrentObject);
            }
            else
            {
                StoreProcedureReport.Param = Common.Copy<StoreProcedureReport>(((XPObjectSpace)ObjectSpace).Session, e.CurrentObject as StoreProcedureReport);
            }
            //
            base.ShowReportPreview(action);
        }
    }
}
