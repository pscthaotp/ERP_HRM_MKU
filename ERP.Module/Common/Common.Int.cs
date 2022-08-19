using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.DanhMuc;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Enum.NhanSu;
using ERP.Module.NghiepVu.NhanSu.GiayTo;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.HeThong;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.Commons
{
    public  static partial class Common
    {
        /// <summary>
        /// Tạo bảng phân quyền đơn vị theo user hiện tại
        /// </summary>
        /// <param name="session"></param>
        /// <param name="truong"></param>
        public static int CreateDeparment_Role_ByCurrentUser(Session session)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CongTy", CongTy(session).Oid);
            param[1] = new SqlParameter("@Quyen", System_GetDeparment_Role_ByUser());
            //
            return DataProvider.ExecuteNonQuery("spd_HeThong_TaoPhanQuyenDonVi", CommandType.StoredProcedure, param);
        }
    }
}
