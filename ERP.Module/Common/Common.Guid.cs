using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.CauHinhChungs;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Enum.NhanSu;
using ERP.Module.HeThong;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace ERP.Module.Commons
{
    public  static partial class Common
    {
        //Chỉ dùng cho website
        public static List<Guid> OidCustomList = new List<Guid>();

        //Dùng chơi lầy hồ
        public static int ID_KHOI_Filter = 0;
        public static int ID_KHOI_ThiLai_Filter = 0;
        //Chỉ dùng cho website
        public static List<Guid[]> OidCustomListArray = new List<Guid[]>();
        /// <summary>
        /// Lấy danh sách nhân viên được phân quyền theo user
        /// </summary>
        /// <param name="session"></param>
        /// <param name="boPhan"></param>
        /// <returns></returns>
        public static List<Guid> NhanVien_DanhSachNhanVienDuocPhanQuyen()
        {
            //
            String quyenList = System_GetDeparment_Role_ByUser();
            //
            List<Guid> guidList = DataProvider.GetGuidList("spd_NhanVien_DanhSachNhanVienDuocPhanQuyen", CommandType.StoredProcedure, new SqlParameter("@Quyen", quyenList));
            //
            return guidList;
        }

        /// <summary>
        /// Lấy danh sách nhân viên nghỉ việc được phân quyền theo user
        /// </summary>
        /// <param name="session"></param>
        /// <param name="boPhan"></param>
        /// <returns></returns>
        public static List<Guid> NhanVien_DanhSachNhanVienNghiViecDuocPhanQuyen()
        {
            //
            String quyenList = System_GetDeparment_Role_ByUser();
            //
            List<Guid> guidList = DataProvider.GetGuidList("spd_NhanVien_DanhSachNhanVienNghiViecDuocPhanQuyen", CommandType.StoredProcedure, new SqlParameter("@Quyen", quyenList));
            //
            return guidList;
        }
        
        

    }
}
