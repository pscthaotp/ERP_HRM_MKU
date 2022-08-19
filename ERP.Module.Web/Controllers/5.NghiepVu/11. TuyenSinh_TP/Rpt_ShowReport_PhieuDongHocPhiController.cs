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
using ERP.Module.NghiepVu.TuyenSinh_TP;
using ERP.Module.BaoCao.TuyenSinh;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.BaoCao.TuyenSinh_TP;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.ExpressApp.Web;
using ERP.Module.Web.Controllers.Custom;

namespace ERP.Module.Web.Controllers.BaoCao
{
    public partial class Rpt_ShowReport_PhieuDongHocPhiController : ViewController
    {
        public Rpt_ShowReport_PhieuDongHocPhiController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        private void Rpt_ShowReport_PhieuDongHocPhiController_Activated(object sender, EventArgs e)
        {
            if (View.Id.Equals("HoSoXetTuyen_DetailView"))
            {
                var hs = View.CurrentObject as HoSoXetTuyen;
                if (hs != null && !hs.DaDongHocPhi)
                    simpleAction1.Active["TruyCap"] = true;
                else
                    simpleAction1.Active["TruyCap"] = false;
            }

            else
                simpleAction1.Active["TruyCap"] = false;
        }
        private void simpleAction1_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            if (View != null)
            {
                HoSoXetTuyen hs = View.CurrentObject as HoSoXetTuyen;
                //lấy hồ sơ trên view hiện tại
                if (hs != null)
                {
                    bool isExists = false;
                    object result = string.Empty;
                    int thangNhapHoc = hs.NgayNhapHoc.Month;
                    int namNhapHoc = hs.NgayNhapHoc.Year;

                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("@MaHocSinh", !string.IsNullOrEmpty(hs.MAHOCSINH) ? hs.MAHOCSINH : hs.MaXetTuyen);
                    result = DataProvider.GetValueFromDatabase("sp_crm_KiemTraHocPhi", CommandType.StoredProcedure, param);
                    if (result != null)
                        bool.TryParse(result.ToString(), out isExists);

                    if (isExists && (thangNhapHoc != hs.ThangNhapHocGhiNhan || namNhapHoc != hs.NamNhapHocGhiNhan))
                    {
                        WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Công nợ học phí đã tồn tại ở tháng khác. Vui lòng xóa công nợ cũ trước khi thao tác !!!')");
                    }
                    else
                    {
                        //dùng để bắt giá trị của ComboBox khi không chọn
                        hs.XLHK = "Xếp loại hạnh kiểm";
                        hs.XLTN = "Xếp loại tốt nghiệp";
                        //
                        View.ObjectSpace.CommitChanges();
                        //
                        DevExpress.ExpressApp.DC.ITypeInfo type = ObjectSpace.TypesInfo.FindTypeInfo("ERP.Module.BaoCao.TuyenSinh_TP.BaoCao_TuyenSinh_PhieuDongHocPhi");
                        if (type != null)
                        {
                            //
                            IObjectSpace _obs = Application.CreateObjectSpace();
                            StoreProcedureReport rpt = (StoreProcedureReport)_obs.CreateObject(type.Type);
                            if (hs != null)
                            {
                                //Truyển parameter vào store báo cáo
                                ReportData_Custom report = _obs.FindObject<ReportData_Custom>(CriteriaOperator.Parse("DataTypeName =?", "ERP.Module.BaoCao.TuyenSinh_TP.BaoCao_TuyenSinh_PhieuDongHocPhi"));
                                ((BaoCao_TuyenSinh_PhieuDongHocPhi)rpt).CongTy = _obs.GetObjectByKey<CongTy>(hs.CongTy.Oid);
                                ((BaoCao_TuyenSinh_PhieuDongHocPhi)rpt).HoSoXetTuyen = _obs.GetObjectByKey<HoSoXetTuyen>(hs.Oid);
                                ((BaoCao_TuyenSinh_PhieuDongHocPhi)rpt).StudentId = hs.MAHOCSINH == null ? hs.MaXetTuyen : hs.MAHOCSINH;
                                ((BaoCao_TuyenSinh_PhieuDongHocPhi)rpt).Nam = namNhapHoc;
                                ((BaoCao_TuyenSinh_PhieuDongHocPhi)rpt).Thang = thangNhapHoc;
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
    }
}

