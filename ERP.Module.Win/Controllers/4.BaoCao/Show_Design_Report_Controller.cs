using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Windows.Forms;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NonPersistentObjects.ReportCustom;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Reports.Win;
using DevExpress.XtraEditors;

namespace ERP.Module.Win.Controllers.NghiepVu.BaoCao
{
    public partial class Show_Design_Report_Controller : ViewController
    {
       
        Session ses;
        IObjectSpace obs;
        ReportData_Custom rptDesign; 
        object source;
        public Show_Design_Report_Controller()
        {
            InitializeComponent();
            TargetViewId = "ReportCustomView_ListView;ReportCustomView_DetailView;ReportCustomView_ReportViewList_ListView;ReportCustomView_ReportViewList_DetailView";
        }
        private void Show_Design_Report_Controller_Activated(object sender, EventArgs e)
        {
            if (Common.TaiKhoanHeThong())
                popShowDesign.Active["TruyCap"] = true;
            else
                popShowDesign.Active["TruyCap"] = false;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            ListViewProcessCurrentObjectController processCurrentObjectController = Frame.GetController<ListViewProcessCurrentObjectController>();
            if (processCurrentObjectController != null)
            {
                processCurrentObjectController.ProcessCurrentObjectAction.Execute += ProcessCurrentObjectAction_Execute;
            }
        }

        void ProcessCurrentObjectAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            ses = ((XPObjectSpace)obs).Session;

            if (
                    View.Id == "ReportCustomView_ReportViewList_DetailView"
                    || View.Id == "ReportCustomView_ReportViewList_ListView"
                     || View.Id == "ReportCustomView_ListView"
                    || View.Id == "ReportCustomView_DetailView")
            {
                //Lấy kiểu đổi tượng (type) của detailview muốn mở dựa vào cột ClassName
                ReportViewGroup rptOID = e.CurrentObject as ReportViewGroup;

                ReportData_Custom rpt = ses.FindObject<ReportData_Custom>(CriteriaOperator.Parse("Oid =?", rptOID.ID));
                if (rpt != null)
                {
                    try
                    {
                        string[] arrListStr = rpt.DataTypeName.ToString().Split('.');
                        string classname = arrListStr[arrListStr.Count() - 1];
                        Type type = DevExpress.Persistent.Base.ReflectionHelper.GetType(classname);
                        if (type != null && type.BaseType == typeof(StoreProcedureReport))
                        {
                            object source = obs.CreateObject(type);
                            //
                            e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, source);
                            e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                            var ctrl = new DevExpress.ExpressApp.SystemModule.DialogController();
                            e.ShowViewParameters.Controllers.Add(ctrl);
                            ctrl.AcceptAction.Execute += (obj, ea) =>
                            {
                                ea.Action.Controller.Frame.View.ObjectSpace.CommitChanges();
                                if (ea.CurrentObject.GetType().ToString().Contains("ERP.Module.Report.HocPhi"))
                                {
                                    StoreProcedureReport.Param = (StoreProcedureReport)Common.CopyAll(((XPObjectSpace)ObjectSpace).Session, source);
                                }
                                else
                                {
                                    StoreProcedureReport.Param = Common.Copy<StoreProcedureReport>(((XPObjectSpace)ObjectSpace).Session, source as StoreProcedureReport);
                                }
                                Frame.GetController<WinReportServiceController>().ShowPreview(rpt);
                            };
                            ctrl.CanCloseWindow = true;
                        }
                    }
                    catch (Exception exx)
                    {
                        XtraMessageBox.Show(exx.Message, "Thông báo!");
                    }                    
                }
            }
        }

        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }


        private void popShow_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {

            StoreProcedureReport.Param = source as StoreProcedureReport;
            Frame.GetController<WinReportServiceController>().ShowDesigner(rptDesign);
        }

        private void popShow_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var i = View.SelectedObjects;
            obs = Application.CreateObjectSpace();
            ses = ((XPObjectSpace)obs).Session;
            ReportCustomView rptCusTom = View.CurrentObject as ReportCustomView;
            if (rptCusTom != null)
            {

                 rptDesign = ses.FindObject<ReportData_Custom>(CriteriaOperator.Parse("Oid =?", rptCusTom.OidReport));

                 string[] arrListStr = rptDesign.DataTypeName.ToString().Split('.');
                string classname = arrListStr[arrListStr.Count() - 1];
                Type type = DevExpress.Persistent.Base.ReflectionHelper.GetType(classname);
                if (type != null)
                {
                     source = obs.CreateObject(type);
                    e.View = Application.CreateDetailView(obs, source);
                }
            }
        }
    }
}
