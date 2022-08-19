using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Web;
using System.Web;
using DevExpress.Utils.OAuth.Provider;
//
namespace ERP.Module.Controllers.Web.ExecuteImport.ImportControl.Commons
{
    public partial class Common_XemLoiNhapDuLieuController : ViewController , IHttpHandler
    {
        public Common_XemLoiNhapDuLieuController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            if (View.Id.Equals("ThongTinKhachHang_ListView") || View.Id.Equals("TuVanTuyenSinh_DetailView")
                || View.Id.Equals("TuVanTuyenSinh_Email_DetailView") || View.Id.Equals("ThongBaoNhapHoc_DetailView")
                || View.Id.Equals("ThongBaoNhapHoc_Email_DetailView") || View.Id.Equals("ChamSocKhachHang_ListView")
                || View.Id.Equals("HoSoXetTuyen_ListView")
               )
            {

                WebWindow.CurrentRequestWindow.RegisterStartupScript("", "window.open('Services/Commons/ExportFile_ImportError.ashx');"); 
                //ProcessRequest(HttpContext.Current);
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            //
            string path = HttpContext.Current.Server.MapPath(Config.URLErorrImportExcel);
            string filename = "Erorr_Import.txt";

            try
            {
                //Tải file
                var buffer = Common.WriteDataToByte(path);
                //
                context.Response.ContentType = "application/octet-stream";
                context.Response.AddHeader("content-disposition", "attachment; filename=" + filename + "");
                context.Response.BinaryWrite(buffer);
                context.Response.Flush();
                context.Response.End();
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
                throw ex;
            }
            finally
            {
                context.Response.End();
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        private void Common_XemLoiNhapDuLieuController_Activated(object sender, EventArgs e)
        {
            #region Tuyển sinh
            //DetailView
            if (View.Id.Equals("TuVanTuyenSinh_DetailView"))
            {
                simpleAction1.Active["TruyCap"] = true;
            }
            else if (View.Id.Equals("TuVanTuyenSinh_Email_DetailView"))
            {
                simpleAction1.Active["TruyCap"] = true;
            }
            else if (View.Id.Equals("ThongBaoNhapHoc_DetailView"))
            {
                simpleAction1.Active["TruyCap"] = true;
            }
            else if (View.Id.Equals("ThongBaoNhapHoc_Email_DetailView"))
            {
                simpleAction1.Active["TruyCap"] = true;
            }
            else if (View.Id.Equals("ToChucSuKien_DetailView"))
            {
                simpleAction1.Active["TruyCap"] = true;
            }
            //ListView
            else if (View.Id.Equals("ThongTinKhachHang_ListView"))
            {
                simpleAction1.Active["TruyCap"] = true;
            }
            else if (View.Id.Equals("ChamSocKhachHang_ListView"))
            {
                simpleAction1.Active["TruyCap"] = true;
            }
            else if (View.Id.Equals("HoSoXetTuyen_ListView"))
            {
                simpleAction1.Active["TruyCap"] = true;
            }
            //
            else
            {
                simpleAction1.Active["TruyCap"] = false;
            }
            #endregion
        }
    }
}
