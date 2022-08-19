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
using DevExpress.Xpo;
using ERP.Module.NghiepVu.TuyenSinh_TP;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using ERP.Module.Commons;
using System.Data;
using DevExpress.ExpressApp.Web;
using ERP.Module.NghiepVu.XetTuyen;
using ERP.Module.NonPersistentObjects.XetTuyen;

namespace ERP.Module.Web.Controllers._5.NghiepVu._14.XetTuyen
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class XetTuyen_XetTuyenHoSoController : ViewController
    {
        public XetTuyen_XetTuyenHoSoController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var current = View.CurrentObject as XetTuyen_DotXetTuyen;
            var dotXetTuyen = current.DotXetTuyen;
            Session session = ((XPObjectSpace)View.ObjectSpace).Session;
            int slMonXetTuyen = 0;
            var succes = "";
            if (current != null)
            {
                if (current.ListDanhSachXetTuyen != null && current.ListDanhSachXetTuyen.Count > 0)
                {
                    int slhs = 0;
                    int slhsmiss = 0;
                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = new SqlParameter("@CongTy", Config.KeyTanPhu);
                    param[1] = new SqlParameter("@NamHoc", current.NamHoc != null ? current.NamHoc.Oid : Guid.Empty);
                    if (current.TatCa)
                        param[2] = new SqlParameter("@ID_KHOI", 999);
                    else
                        param[2] = new SqlParameter("@ID_KHOI", current.ID_KHOI);
                    //param[1] = new SqlParameter("@DenNgay", DenNgay);
                    using (DataSet ds = DataProvider.GetDataSetWithParam("spd_XetTuyen_XetTuyenHoSo", CommandType.StoredProcedure, param))
                    {
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            foreach (DataTable table in ds.Tables)
                            {
                                if (table.TableName.Equals("Table"))
                                {
                                    foreach (DataRow row in table.Rows)
                                    {
                                        HoSoXetTuyen hsxt = session.GetObjectByKey<HoSoXetTuyen>( new Guid(row["Oid"].ToString()));
                                        if(hsxt!=null)
                                        {
                                            hsxt.DaXetTuyen =  true;
                                            hsxt.DotXetTuyen= current.DotXetTuyen;
                                            hsxt.NgayXetTuyen= DateTime.Now;
                                            hsxt.TinhTrangHoSo= Enum.TuyenSinh_PT.TinhTrangHoSoEnum.KhongTrungTuyen;
                                        }
                                    }             
                                    slhsmiss = table.Rows.Count;
                                }
                                else if (table.TableName.Equals("Table1"))
                                {

                                    foreach (DataRow row in table.Rows)
                                    {
                                        HoSoXetTuyen hsxt = session.GetObjectByKey<HoSoXetTuyen>(new Guid(row["Oid"].ToString()));
                                        if (hsxt != null)
                                        {
                                            hsxt.DaXetTuyen = true;
                                            hsxt.DotXetTuyen = current.DotXetTuyen;
                                            hsxt.NgayXetTuyen = DateTime.Now;
                                            hsxt.TinhTrangHoSo = Enum.TuyenSinh_PT.TinhTrangHoSoEnum.TrungTuyen;
                                        }
                                    }
                                    slhs = table.Rows.Count;
                                }

                            }
                            try
                            {
                                if (current.TatCa)
                                {
                                    foreach (var item in current.DotXetTuyen.ListXetTuyenTheoKhoi)
                                    {
                                        item.DaXetTuyen = true;
                                    }
                                }
                                else
                                {
                                    DotXetTuyen_Khoi dxt = session.FindObject<DotXetTuyen_Khoi>(CriteriaOperator.Parse("DotXetTuyen = ? and ID_KHOI = ?", current.DotXetTuyen, current.ID_KHOI));
                                    if (dxt != null)
                                        dxt.DaXetTuyen = true;
                                }
                                session.CommitTransaction();
                                string message = "alert('Có '" + slhs + " hồ sơ trúng tuyển và "+ slhs+" hồ sơ không trúng tuyển.)";
                               
                                
                                WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                            }
                            catch (Exception ex)
                            {
                                WebWindow.CurrentRequestWindow.RegisterStartupScript("", "Lỗi hệ thống. Vui lòng kiểm tra lại.");
                            }
                        }
                        else
                        {
                            string message = "alert('Không có hồ sơ nào trúng tuyển.')";
                            WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                        }
                    }
                }
            }
        }
    }
}
