using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using ERP.Module.Commons;

namespace ERP.Web.Services.TuyenSinh
{
    public class Download_HoSoNhapHoc : IHttpHandler
    {
        //
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request["oidtaptin"] == null) return;
            string oidTapTin = context.Request["oidtaptin"].ToString();

            //
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Oid", oidTapTin);
            using (DataTable dt = DataProvider.GetDataTable("spd_TuyenSinh_DownLoadHoSoNhapHoc", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    string filepath = dt.Rows[0]["DuongDanTapTin"].ToString();
                    string filename = dt.Rows[0]["TenTapTin"].ToString();
                    //
                    FileStream MyFileStream = new FileStream(filepath, FileMode.Open);
                    long FileSize = MyFileStream.Length;
                    byte[] Buffer = new byte[(int)FileSize];
                    MyFileStream.Read(Buffer, 0, (int)MyFileStream.Length);
                    MyFileStream.Close();
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.AddHeader("content-disposition", "attachment; filename=" + filename + "");
                    context.Response.BinaryWrite(Buffer);
                    context.Response.Flush();
                    context.Response.End();
                }
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