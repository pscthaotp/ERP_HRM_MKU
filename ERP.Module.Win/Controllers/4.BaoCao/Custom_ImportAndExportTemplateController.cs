using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using DevExpress.ExpressApp.SystemModule;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraEditors;
using System.Text;
using System.Collections.Generic;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Extends;
using ERP.Module.Commons;

namespace ERP.Module.Win.Controllers.BaoCao
{
    public partial class Custom_ImportAndExportTemplateController : WindowController
    {
        IObjectSpace _obs = null;

        public Custom_ImportAndExportTemplateController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void actReportManager_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            //
            if (e.SelectedChoiceActionItem.Caption == "Export")
                ExportReport(e);
            else
                ImportReport(e);
        }


        private void ImportReport(SingleChoiceActionExecuteEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            //
            ImportReport source = _obs.CreateObject<ImportReport>();
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, source);
            e.ShowViewParameters.Context = TemplateContext.PopupWindow;
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            //
            DialogController ctrl = new DialogController();
            ctrl.AcceptAction.Execute += ImportAction_Execute;
            e.ShowViewParameters.Controllers.Add(ctrl);
        }

        private void ExportReport(SingleChoiceActionExecuteEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            //
            ExportReport source = _obs.CreateObject<ExportReport>();
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, source);
            e.ShowViewParameters.Context = TemplateContext.PopupWindow;
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            //
            DialogController ctrl = new DialogController();
            ctrl.AcceptAction.Execute += ExportAction_Execute;
            e.ShowViewParameters.Controllers.Add(ctrl);
        }

        void ExportAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Report Data|*.report|All file|*.*";
                saveFileDialog.AddExtension = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportReport exportReport = e.CurrentObject as ExportReport;
                    DataTable dt = new DataTable("ReportTemplate", "ERP");
                    //
                    List<string> reports = new List<string>();
                    StringBuilder sReport = new StringBuilder("Những báo cáo sau đã được export:");
                    bool state = false;
                    string query = "SELECT a.Oid, a.ObjectTypeName, a.Content, a.Name, a.IsInplaceReport, b.TargetTypeName, b.GroupReport, c.TenNhom, b.HinhAnh,b.CongTy FROM dbo.ReportData a INNER JOIN dbo.ReportData_Custom b On a.Oid = b.Oid INNER JOIN dbo.GroupReport c On b.GroupReport=c.Oid Where a.GCRecord IS NULL AND b.Oid=@Oid";
                    SqlCommand cmd = new SqlCommand(query, (SqlConnection)exportReport.Session.Connection);
                    cmd.Parameters.Add("@Oid", SqlDbType.Int);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //
                    foreach (var item in exportReport.ReportList)
                    {
                        if (item.Chon)
                        {
                            cmd.Parameters["@Oid"].Value = item.Report.Oid;
                            da.Fill(dt);
                            reports.Add(item.Report.Oid.ToString());
                            sReport.AppendLine(" + " + item.Report.ReportName);
                            state = true;
                        }
                    }
                    //Ghi dữ liệu vào file xml
                    if (state)
                    {
                        //
                       dt.WriteXml(saveFileDialog.FileName, XmlWriteMode.WriteSchema);
                       DialogUtil.ShowInfo(String.Format("Đã xuất {0} báo cáo.\r\n{1}", dt.Rows.Count, sReport));
                    }
                    else
                        DialogUtil.ShowError("Không có báo cáo nào được chọn để xuất.");
                }
            }
        }

        void ImportAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (DialogUtil.ShowYesNo("Bạn thất sự muốn nhập mẫu báo cáo.") == DialogResult.Yes)
            {
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    //
                    dlg.Filter = "Report Data|*.report|All file|*.*";
                    dlg.AddExtension = true;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {    
                        //
                        ImportReport import = e.CurrentObject as ImportReport;
                        DataTable tbl = null;
                        //Dọc dữ liệu từ file đã chọn
                        DataSet ds = new DataSet();
                        ds.ReadXml(dlg.FileName, XmlReadMode.ReadSchema);
                        //
                        if (ds.Tables.Count > 0)
                            tbl = ds.Tables[0];
                        //
                        if (tbl != null)
                        {
                            //
                            IObjectSpace obs = Application.CreateObjectSpace();
                            ReportData_Custom report;
                            GroupReport group;
                            string sReport = "";
                            int rptCount = 0;
                            //
                            SqlConnection cn = (SqlConnection)((XPObjectSpace)Application.CreateObjectSpace()).Session.Connection;
                            SqlCommand cm = new SqlCommand("", cn);
                            //
                            foreach (DataRow row in tbl.Rows)
                            {
                                #region Tạo hoặc lấy nhóm báo cáo
                                if (import.GroupReport == null)
                                {

                                    //Insert group
                                    group = obs.FindObject<GroupReport>(CriteriaOperator.Parse("Oid=?", row["GroupReport"]));
                                    if (group == null)
                                    {
                                        //Tạo nhóm báo cáo mới
                                        cm.CommandText = "INSERT INTO dbo.GroupReport (Oid, TenNhom) Values (@Oid, @TenNhom)";
                                        cm.Parameters.Clear();
                                        cm.Parameters.AddWithValue("@Oid", row["GroupReport"]);
                                        cm.Parameters.AddWithValue("@TenNhom", row["TenNhom"]);
                                        cm.ExecuteNonQuery();
                                    }
                                    //
                                    group = obs.FindObject<GroupReport>(CriteriaOperator.Parse("Oid=?", row["GroupReport"]));
                                }
                                else
                                    group = obs.GetObjectByKey<GroupReport>(import.GroupReport.Oid);
                                #endregion

                                #region Tạo hoặc cập nhật báo cáo
                                if (!import.GhiDe)
                                {
                                    #region 1. Thêm mới

                                    //Insert object type
                                    XPObjectType xpobj = obs.FindObject<XPObjectType>(CriteriaOperator.Parse("TypeName=?", "ERP.Module.BaoCao.Custom.ReportData_Custom"));
                                    if (xpobj == null)
                                    {
                                        cm.CommandText = "INSERT INTO XPObjectType(TypeName,AssemblyName) values(@p1,@p2)";
                                        cm.Parameters.Clear();
                                        cm.Parameters.AddWithValue("@p1", "ERP.Module.BaoCao.Custom.ReportData_Custom");
                                        cm.Parameters.AddWithValue("@p2", "ERP.Module");
                                        cm.ExecuteNonQuery();
                                        //
                                        xpobj = obs.FindObject<XPObjectType>(CriteriaOperator.Parse("TypeName=?", "ERP.Module.BaoCao.Custom.ReportData_Custom"));
                                    }

                                    //Lấy id lớn nhất của reportdata
                                    cm.CommandText = "SELECT MAX(Oid) FROM dbo.ReportData";
                                    cm.Parameters.Clear();
                                    object oid = cm.ExecuteScalar();
                                    //
                                    if (oid != null)
                                    {
                                        //Set identity on
                                        cm.CommandText = string.Format("DBCC CHECKIDENT ('dbo.ReportData', RESEED, {0})", oid);
                                        cm.Parameters.Clear();
                                        cm.ExecuteNonQuery();
                                        //
                                        //Insert reportdata
                                        cm.CommandText = "INSERT INTO dbo.ReportData (ObjectTypeName, Content, Name, IsInplaceReport, ObjectType) Values (@ObjectTypeName, @Content, @Name, @IsInplaceReport, @ObjectType)";
                                        cm.Parameters.Clear();
                                        cm.Parameters.AddWithValue("@Content", row["Content"]);
                                        cm.Parameters.AddWithValue("@IsInplaceReport", row["IsInplaceReport"]);
                                        cm.Parameters.AddWithValue("@Name", row["Name"]);
                                        cm.Parameters.AddWithValue("@ObjectTypeName", row["ObjectTypeName"]);
                                        cm.Parameters.AddWithValue("@ObjectType", xpobj.Oid);
                                        cm.ExecuteNonQuery();

                                        //Set identity on
                                        cm.CommandText = "SELECT MAX(Oid) FROM dbo.ReportData";
                                        cm.Parameters.Clear();
                                        object oidNew = cm.ExecuteScalar();
                                        //
                                        if (oidNew != null)
                                        {
                                            //Insert ReportData_Custom
                                            cm.CommandText = "INSERT INTO dbo.ReportData_Custom (Oid, GroupReport, TargetTypeName,HinhAnh,CongTy) Values (@Oid, @GroupReport, @TargetTypeName,@HinhAnh,@CongTy)";
                                            cm.Parameters.Clear();
                                            cm.Parameters.AddWithValue("@Oid", oidNew);
                                            cm.Parameters.AddWithValue("@GroupReport", group.Oid);
                                            cm.Parameters.AddWithValue("@TargetTypeName", row["TargetTypeName"]);
                                            cm.Parameters.AddWithValue("@HinhAnh", row["HinhAnh"]);
                                            cm.Parameters.AddWithValue("@CongTy", import.CongTy.Oid);
                                            cm.ExecuteNonQuery();
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    #region 2. Ghi đè

                                    //Tìm report id 
                                    report = obs.GetObjectByKey<ReportData_Custom>(row["Oid"]);
                                    if (report != null)
                                    {
                                        // Ghi đè phải cùng công ty
                                        if (import.CongTy.Oid.ToString().ToUpper().Equals(row["CongTy"].ToString().ToUpper()))
                                        {
                                            //update, nếu đã bị xóa thì phục hồi lại (GCRecord=NULL)
                                            cm.CommandText = "UPDATE dbo.ReportData SET Content = @Content, IsInplaceReport = @IsInplaceReport, Name = @Name, ObjectTypeName = @ObjectTypeName, GCRecord=NULL WHERE Oid = @Oid";
                                            cm.Parameters.Clear();
                                            cm.Parameters.AddWithValue("@Content", row["Content"]);
                                            cm.Parameters.AddWithValue("@IsInplaceReport", row["IsInplaceReport"]);
                                            cm.Parameters.AddWithValue("@Name", row["Name"]);
                                            cm.Parameters.AddWithValue("@ObjectTypeName", row["ObjectTypeName"]);
                                            cm.Parameters.AddWithValue("@Oid", row["Oid"]);
                                            cm.ExecuteNonQuery();
                                            //
                                            cm.CommandText = "UPDATE dbo.ReportData_Custom SET TargetTypeName = @TargetTypeName, GroupReport = @GroupReport,HinhAnh = @HinhAnh,CongTy=@CongTy WHERE Oid = @Oid";
                                            cm.Parameters.Clear();
                                            cm.Parameters.AddWithValue("@TargetTypeName", row["TargetTypeName"]);
                                            cm.Parameters.AddWithValue("@GroupReport", group.Oid);
                                            cm.Parameters.AddWithValue("@Oid", row["Oid"]);
                                            cm.Parameters.AddWithValue("@HinhAnh", row["HinhAnh"]);
                                            cm.Parameters.AddWithValue("@CongTy", import.CongTy.Oid);
                                            cm.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            DialogUtil.ShowError("Mẩu báo cáo {" + row["Name"] + "} không thể ghi đè. Khác công ty !!!");
                                        }
                                    }
                                    else
                                    {
                                        //Insert object type
                                        XPObjectType xpobj = obs.FindObject<XPObjectType>(CriteriaOperator.Parse("TypeName=?", "ERP.Module.BaoCao.Custom.ReportData_Custom"));
                                        if (xpobj == null)
                                        {
                                            cm.CommandText = "INSERT INTO XPObjectType(TypeName,AssemblyName) values(@p1,@p2)";
                                            cm.Parameters.Clear();
                                            cm.Parameters.AddWithValue("@p1", "ERP.Module.BaoCao.Custom.ReportData_Custom");
                                            cm.Parameters.AddWithValue("@p2", "ERP.Module");
                                            cm.ExecuteNonQuery();
                                            xpobj = obs.FindObject<XPObjectType>(CriteriaOperator.Parse("TypeName=?", "ERP.Module.BaoCao.Custom.ReportData_Custom"));
                                        }

                                        //Lấy id lớn nhất của reportdata
                                        cm.CommandText = "SELECT MAX(Oid) FROM dbo.ReportData";
                                        cm.Parameters.Clear();
                                        object oid = cm.ExecuteScalar();
                                        //
                                        if (oid != null)
                                        {
                                            cm.CommandText = string.Format("DBCC CHECKIDENT ('dbo.ReportData', RESEED, {0})", oid);
                                            cm.Parameters.Clear();
                                            cm.ExecuteNonQuery();

                                            //Insert reportdata
                                            cm.CommandText = String.Format("INSERT INTO dbo.ReportData (ObjectTypeName, Content, Name, IsInplaceReport, ObjectType) Values (@ObjectTypeName, @Content, @Name, @IsInplaceReport, {0})", xpobj.Oid);
                                            cm.Parameters.Clear();
                                            cm.Parameters.AddWithValue("@Content", row["Content"]);
                                            cm.Parameters.AddWithValue("@IsInplaceReport", row["IsInplaceReport"]);
                                            cm.Parameters.AddWithValue("@Name", row["Name"]);
                                            cm.Parameters.AddWithValue("@ObjectTypeName", row["ObjectTypeName"]);
                                            cm.ExecuteNonQuery();

                                            //Lấy id lớn nhất của reportdata
                                            cm.CommandText = "SELECT MAX(Oid) FROM dbo.ReportData";
                                            cm.Parameters.Clear();
                                            object oidNew = cm.ExecuteScalar();
                                            //
                                            if (oidNew != null)
                                            {
                                                //insert hrmreport
                                                cm.CommandText = "INSERT INTO dbo.ReportData_Custom (Oid, GroupReport, TargetTypeName,HinhAnh,CongTy) Values (@Oid, @GroupReport, @TargetTypeName,@HinhAnh,@CongTy)";
                                                cm.Parameters.Clear();
                                                cm.Parameters.AddWithValue("@Oid", oidNew);
                                                cm.Parameters.AddWithValue("@GroupReport", group.Oid);
                                                cm.Parameters.AddWithValue("@TargetTypeName", row["TargetTypeName"]);
                                                cm.Parameters.AddWithValue("@HinhAnh", row["HinhAnh"]);
                                                cm.Parameters.AddWithValue("@CongTy", import.CongTy.Oid);
                                                cm.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                #endregion

                                //
                                sReport += "\r\n + " + (string)row["Name"];
                                rptCount++;
                            }
                              
                            //Lưu dữ liệu lại
                            obs.CommitChanges();
                            //
                            DialogUtil.ShowInfo(String.Format("Đã nhập {0} báo cáo thành công.\r\nCác báo cáo được cập nhật:{1}", rptCount, sReport));
                        }
                    }
                }
            }
        }
    }
}
