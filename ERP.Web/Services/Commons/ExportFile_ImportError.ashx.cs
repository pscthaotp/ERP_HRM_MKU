using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using ERP.Module.Commons;

namespace ERP.Web.Services.Commons
{
    public class ExportFile_ImportError : IHttpHandler
    {
        //
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
            catch(Exception ex)
            {

            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}