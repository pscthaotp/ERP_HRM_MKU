using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Reports;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Reports.Win;
using ERP.Module.Commons;
using ERP.Module.BaoCao.Custom;
//
namespace ERP.Module.Win.Controllers.Custom
{
    public partial class ShowReportOnToolbarController : ViewController
    {
        private XPCollection<ReportData_Custom> _reportList;
        private ReportData_Custom _report;
        private IObjectSpace _obs;

        public ShowReportOnToolbarController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ShowReportOnToolbarController_Activated(object sender, EventArgs e)
        {
            if (View.Id.Contains("NhanSuCustomView_DetailView")
                || View.Id.Contains("ThinhGiangCustomView_DetailView"))
            {
                Type type = null;
                if(View.Id.Contains("NhanSuCustomView_DetailView"))
                    type = typeof(ERP.Module.NghiepVu.NhanSu.NhanViens.ThongTinNhanVien);
                else if (View.Id.Contains("ThinhGiangCustomView_DetailView"))
                    type = typeof(ERP.Module.NghiepVu.NhanSu.NhanViens.GiangVienThinhGiang);

                SortProperty sort = new SortProperty("ReportName", DevExpress.Xpo.DB.SortingDirection.Ascending);
                //
                string quyenBaoCao = Common.System_Report_Role_ByUser();
                if (!Common.QuanTriToanHeThong() && !string.IsNullOrEmpty(quyenBaoCao))
                {
                    List<Int32> phanQuyenBaoCaoList = new List<int>();
                    string[] quyenList = quyenBaoCao.Split(';');
                    foreach (var item in quyenList)
                    {
                        if (!string.IsNullOrEmpty(item))
                            phanQuyenBaoCaoList.Add(Convert.ToInt32(item));
                    }
                    //
                    GroupOperator go = new GroupOperator(GroupOperatorType.And);
                    CriteriaOperator filter = CriteriaOperator.Parse("TargetTypeName=? ", type.FullName);
                    go.Operands.Add(filter);
                    go.Operands.Add(new InOperator("Oid", phanQuyenBaoCaoList));
                    //
                    _reportList = new XPCollection<ReportData_Custom>(((XPObjectSpace)View.ObjectSpace).Session, go, sort);
                    //
                }
                else
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("TargetTypeName=? ", type.FullName);
                    _reportList = new XPCollection<ReportData_Custom>(((XPObjectSpace)View.ObjectSpace).Session, filter, sort);
                }

                if (_reportList.Count > 0)
                    singleChoiceAction1.Active["ByMainForm"] = true;
                else
                    singleChoiceAction1.Active["ByMainForm"] = false;

            }
            else if (View.ObjectTypeInfo != null && !View.Id.Contains("DetailView")  && !View.Id.Contains("Validation"))
            {
                Type type = View.ObjectTypeInfo.Type;
                if (type != null && !(View.ObjectSpace is DevExpress.ExpressApp.NonPersistentObjectSpace))
                {
                    //
                    SortProperty sort = new SortProperty("ReportName", DevExpress.Xpo.DB.SortingDirection.Ascending);
                    //
                    string quyenBaoCao = Common.System_Report_Role_ByUser();
                    if (!Common.QuanTriToanHeThong() && !string.IsNullOrEmpty(quyenBaoCao))
                    {
                        List<Int32> phanQuyenBaoCaoList = new List<int>();
                        string[] quyenList = quyenBaoCao.Split(';');
                        foreach (var item in quyenList)
                        {
                            if (!string.IsNullOrEmpty(item))
                                phanQuyenBaoCaoList.Add(Convert.ToInt32(item));
                        }
                        //
                        GroupOperator go = new GroupOperator(GroupOperatorType.And);
                        CriteriaOperator filter = CriteriaOperator.Parse("TargetTypeName=? ", type.FullName);
                        go.Operands.Add(filter);
                        go.Operands.Add(new InOperator("Oid", phanQuyenBaoCaoList));            
                        //
                        _reportList = new XPCollection<ReportData_Custom>(((XPObjectSpace)View.ObjectSpace).Session, go, sort);
                        //
                    }
                    else
                    {
                        CriteriaOperator filter = CriteriaOperator.Parse("TargetTypeName=? ", type.FullName);
                        _reportList = new XPCollection<ReportData_Custom>(((XPObjectSpace)View.ObjectSpace).Session, filter, sort);
                    }

                    if (_reportList.Count > 0)
                        singleChoiceAction1.Active["ByMainForm"] = true;
                    else
                        singleChoiceAction1.Active["ByMainForm"] = false;
                }
                else
                {
                    singleChoiceAction1.Active["ByMainForm"] = false;
                }
            }
        }

        private void ShowReportOnToolbarController_ViewControlsCreated(object sender, EventArgs e)
        {
            if (_reportList != null)
            {
                singleChoiceAction1.Items.Clear();
                ChoiceActionItem subItem;
                foreach (ReportData_Custom item in _reportList)
                {
                    subItem = new ChoiceActionItem();
                    subItem.Id = item.Oid.ToString();
                    subItem.Caption = item.ReportName;
                    subItem.ImageName = "Action_Report_Object_Inplace_Preview";
                    singleChoiceAction1.Items.Add(subItem);
                }
            }
        }

        private void singleChoiceAction1_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            foreach (ReportData_Custom item in _reportList)
            {
                if (item.Oid.ToString() == e.SelectedChoiceActionItem.Id)
                {
                    //Xu ly execute report o day   
                    DevExpress.ExpressApp.DC.ITypeInfo type = ObjectSpace.TypesInfo.FindTypeInfo(item.DataTypeName);
                    if (type != null)
                    {
                        _obs = Application.CreateObjectSpace();
                        StoreProcedureReport obj = (StoreProcedureReport)_obs.CreateObject(type.Type);
                        if (obj != null)
                        {
                            _report = item;
                            //StoreProcedureReport.Param = obj;
                            e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, obj);
                            e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;

                            var ctrl = new DevExpress.ExpressApp.SystemModule.DialogController();
                            e.ShowViewParameters.Controllers.Add(ctrl);
                            ctrl.AcceptAction.Execute += AcceptAction_Execute;
                            ctrl.AcceptAction.Caption = "Đồng ý";
                        }
                        break;
                    }
                }
            }
        }

        private void AcceptAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (_report != null)
            {
                StoreProcedureReport.Param = ((StoreProcedureReport)e.CurrentObject);
                Frame.GetController<ReportServiceController>().ShowPreview(_report);
            }
        }
    }
}
