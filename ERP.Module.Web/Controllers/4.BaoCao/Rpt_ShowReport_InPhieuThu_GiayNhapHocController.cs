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
using ERP.Module.NghiepVu.TuyenSinh_TP;
using ERP.Module.NonPersistentObjects.TuyenSinh_TP;
using ERP.Module.BaoCao.Custom;
using ERP.Module.BaoCao.TuyenSinh_TP;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.ExpressApp.Reports;
using ERP.Module.Commons;

namespace ERP.Module.Web.Controllers._4.BaoCao
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Rpt_ShowReport_InPhieuThu_GiayNhapHocController : ViewController
    {
        private HoSoXetTuyen _hoSoXetTuyen;
        TuyenSinhPT_InPhieuThu_GiayNhapHoc _Nam_Thang;
        IObjectSpace _obs;
        public Rpt_ShowReport_InPhieuThu_GiayNhapHocController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            RegisterActions(components);
        }
        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            #region 1. Hồ sơ xét tuyển
            if (View.Id.Equals("HoSoXetTuyen_DetailView"))
            {
                _hoSoXetTuyen = View.CurrentObject as HoSoXetTuyen;
                if (_hoSoXetTuyen != null)
                {
                    {
                        _obs = Application.CreateObjectSpace();
                        //
                        _Nam_Thang = _obs.CreateObject<TuyenSinhPT_InPhieuThu_GiayNhapHoc>();
                        DetailView view = Application.CreateDetailView(_obs, _Nam_Thang);
                        view.ViewEditMode = ViewEditMode.Edit;
                        e.View = view;
                        _Nam_Thang.Nam = DateTime.Today.Year;
                        _Nam_Thang.Thang = DateTime.Today.Month;
                    }
                }
            }
            #endregion
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (View != null)
            {
                _hoSoXetTuyen = View.CurrentObject as HoSoXetTuyen;
                _obs = Application.CreateObjectSpace();
                DevExpress.ExpressApp.DC.ITypeInfo type = ObjectSpace.TypesInfo.FindTypeInfo("ERP.Module.BaoCao.TuyenSinh_TP.BaoCao_TuyenSinh_PhieuDongHocPhi");
                if (type != null)
                {
                    StoreProcedureReport rpt = (StoreProcedureReport)_obs.CreateObject(type.Type);
                    if (_hoSoXetTuyen != null)
                    {
                        ReportData_Custom report = _obs.FindObject<ReportData_Custom>(CriteriaOperator.Parse("DataTypeName =?", "ERP.Module.BaoCao.TuyenSinh_TP.BaoCao_TuyenSinh_PhieuDongHocPhi"));
                        ((BaoCao_TuyenSinh_PhieuDongHocPhi)rpt).CongTy = _obs.GetObjectByKey<CongTy>(_hoSoXetTuyen.CongTy.Oid);
                        ((BaoCao_TuyenSinh_PhieuDongHocPhi)rpt).HoSoXetTuyen = _obs.GetObjectByKey<HoSoXetTuyen>(_hoSoXetTuyen.Oid);
                        ((BaoCao_TuyenSinh_PhieuDongHocPhi)rpt).StudentId = _hoSoXetTuyen.MaXetTuyen;
                        ((BaoCao_TuyenSinh_PhieuDongHocPhi)rpt).Nam = _Nam_Thang.Nam;
                        ((BaoCao_TuyenSinh_PhieuDongHocPhi)rpt).Thang = _Nam_Thang.Thang;
                        ((BaoCao_TuyenSinh_PhieuDongHocPhi)rpt).UpdateStaff = Common.SecuritySystemUser_GetCurrentUser().UserName;
                        StoreProcedureReport.Param = rpt;
                        Frame.GetController<ReportServiceController>().ShowPreview(report);
                        //Lệnh khai báo máy in
                        //var reportPrintTool = new DevExpress.XtraReports.UI.ReportPrintTool(report.LoadReport(_obs));
                        //Lệnh in
                        //reportPrintTool.Print();
                    }
                }
            }
        }
    }
}

