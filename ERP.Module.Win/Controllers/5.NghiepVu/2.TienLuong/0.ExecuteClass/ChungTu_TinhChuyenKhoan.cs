using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.Collections.Generic;
using ERP.Module.NghiepVu.TienLuong.ChungTus;
using ERP.Module.Commons;
//
namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.ExecuteClass
{
    public static class ChungTu_TinhChuyenKhoan
    {
        public static string XuLy(IObjectSpace obs, ChungTu obj )
        {
            string message = string.Empty;
            //
            if (obj != null)
            {
                #region 1. Tiến hành tính chuyển khoản

                //
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ChungTu", obj.Oid);
                //
                bool sucess = DataProvider.ExecuteNonQuery_ReturnValue("spd_ChungTu_TinhChuyenKhoanLuongNhanVien", CommandType.StoredProcedure, param);
                //
                if (!sucess)
                {
                    message = "Tính chuyển khoản không thành công !!!";
                    return message;
                }
                #endregion
            }
            //
            return message;
        }
    }
}
