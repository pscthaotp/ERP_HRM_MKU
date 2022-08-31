
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.Commons
{
    public  static partial class Common
    {
        #region Nhân sự
        /// <summary>
        /// Lấy điều kiện tìm kiếm bộ phận theo Trường
        /// </summary>
        /// <param name="session"></param>
        /// <param name="boPhan"></param>
        /// <returns></returns>
        public static GroupOperator Criteria_BoPhan_DanhSachBoPhanDuocPhanQuyen(CongTy congTy)
        {
            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            CriteriaOperator filter1 = null;
            InOperator filter2 = null;
            //
            if (!QuanTriToanHeThong()
                || !QuanTriKhoi())
            {
                if (congTy != null)
                    filter1 = CriteriaOperator.Parse("CongTy=? and LoaiBoPhan=2", congTy.Oid);
                else
                    filter1 = CriteriaOperator.Parse("CongTy=? and LoaiBoPhan=2", Guid.Empty);
                //
                filter2 = new InOperator("Oid", Common.Department_GetRoledDepartmentList_ByCurrentUser());
                //
                go.Operands.Add(filter1);
                go.Operands.Add(filter2);
            }
            else
            {
                if (congTy != null)
                    filter1 = CriteriaOperator.Parse("CongTy=? and LoaiBoPhan=2", congTy.Oid);
                else
                    filter1 = CriteriaOperator.Parse("CongTy=? and LoaiBoPhan=2", Guid.Empty);
                //
                go.Operands.Add(filter1);
                //
            }

            return go;
        }

        /// <summary>
        /// Lấy điều kiện tìm kiếm nhân viên theo bộ phận
        /// </summary>
        /// <param name="session"></param>
        /// <param name="boPhan"></param>
        /// <returns></returns>
        public static CriteriaOperator Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan boPhan)
        {
            CriteriaOperator filter = null;
            //
            if(boPhan != null)
               //filter = CriteriaOperator.Parse("!TinhTrang.DaNghiViec and BoPhan=?", boPhan.Oid);//Nguyen tam bo
                filter = CriteriaOperator.Parse("BoPhan=?", boPhan.Oid);//Nguyen
            else
               //filter = CriteriaOperator.Parse("!TinhTrang.DaNghiViec and BoPhan=?", Guid.Empty);//Nguyen tam bo
                filter = CriteriaOperator.Parse("BoPhan=?", Guid.Empty);//Nguyen
            //
            return filter;
        }

        /// <summary>
        /// Lấy điều kiện tìm người ký tên
        /// </summary>
        /// <param name="phanLoai"></param>
        /// <param name="chucVu"></param>
        /// <returns></returns>
        public static CriteriaOperator Criteria_HopDong_NguoiKyTenTheoLoaiNguoiKyVaChucVu(PhanLoaiNguoiKy phanLoai, ChucVuNguoiKy chucVu, CongTy congTy)
        {
            CriteriaOperator filter;
            if (phanLoai.TenPhanLoaiNguoiKy.Contains("đang tại chức"))
                filter = CriteriaOperator.Parse("ChucVu.Oid=?", chucVu.ChucVu != null ? chucVu.ChucVu.Oid : Guid.Empty);
            else if (phanLoai.TenPhanLoaiNguoiKy.Contains("không tại chức"))
            {
                List<Guid> guidList = DataProvider.GetGuidList("spd_HopDong_ChucVuDaQuaCuaNguoiDaKhongTaiChuc", CommandType.StoredProcedure, new SqlParameter("@ChucVu", chucVu.ChucVu != null ? chucVu.ChucVu.Oid : Guid.Empty));
                //
                filter = new InOperator("Oid", guidList);
            }
            else
                filter = CriteriaOperator.Parse("ChucVu.Oid=?", chucVu.ChucVu != null ? chucVu.ChucVu.Oid : Guid.Empty);
            //
            return filter;
        }
        #endregion     
    }
}
