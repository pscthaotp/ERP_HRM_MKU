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
using ERP.Module.Enum.Systems;
using ERP.Module.DanhMuc.System;

namespace ERP.Module.Commons
{
    public static partial class Common
    {

        //Dùng khi gửi xác thực
        public static bool DaGuiXacThuc = false;
        //Dùng khi ghi nhận lúc chọn thông tin nhân viên
        public static bool AcessedCategorizedListEditor = false;

        /// Kiểm tra tài khoản hiện tại có phải là Quản trị Hệ thống hay không
        public static bool QuanTriToanHeThong()
        {
            SecuritySystemUser_Custom currentUser = SecuritySystem.CurrentUser as SecuritySystemUser_Custom;
            //
            if (currentUser != null && currentUser.LoaiTaiKhoan == LoaiTaiKhoanEnum.QuanTriHeThong)
            {
                return true;
            }
            return false;
        }

        /// Kiểm tra tài khoản hiện tại có phải là Quản trị Khối hay không
        public static bool QuanTriKhoi()
        {
            SecuritySystemUser_Custom currentUser = SecuritySystem.CurrentUser as SecuritySystemUser_Custom;
            //
            if (currentUser != null && currentUser.LoaiTaiKhoan == LoaiTaiKhoanEnum.QuanTriKhoi)
            {
                return true;
            }
            return false;
        }


        /// Kiểm tra tài khoản hiện tại có phải là Tài khoản Edu
        public static bool TaiKhoanEdu()
        {
            SecuritySystemUser_Custom currentUser = SecuritySystem.CurrentUser as SecuritySystemUser_Custom;
            if (currentUser != null && currentUser.CongTy != null)
            {
                if (currentUser.CongTy.Oid.Equals(Config.KeyTTCEdu))
                    return true;
            }
            return false;
        }
        // Kiểm tra tài khỏn của E-study kh
        public static bool CheckEstudy()
        {
            if ((SecuritySystem.CurrentUser as SecuritySystemUser_Custom) != null && (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).CongTy != null && (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).CongTy.Oid.Equals(Config.KeyEStudy))
            {
                return true;
            }
            return false;
        }

        /// Kiểm tra tài khoản hiện tại có phải là Tài khoản bình thường Not Edu
        public static bool TaiKhoanBinhThuong_NotEdu()
        {
            SecuritySystemUser_Custom currentUser = SecuritySystem.CurrentUser as SecuritySystemUser_Custom;
            if (currentUser != null && currentUser.CongTy != null && currentUser.LoaiTaiKhoan == LoaiTaiKhoanEnum.TaiKhoanBinhThuong)
            {
                if (!currentUser.CongTy.Oid.Equals(Config.KeyTTCEdu))
                    return true;
            }
            return false;
        }

        /// Kiểm tra tài khoản hiện tại có phải là Tài khoản bình thường của Edu
        public static bool TaiKhoanBinhThuong()
        {
            SecuritySystemUser_Custom currentUser = SecuritySystem.CurrentUser as SecuritySystemUser_Custom;
            if (currentUser != null && currentUser.LoaiTaiKhoan == LoaiTaiKhoanEnum.TaiKhoanBinhThuong)
            {
                return true;
            }
            return false;
        }
        /// Kiểm tra tài khoản hiện tại có phải là Tài khoản hệ thống không
        public static bool TaiKhoanHeThong()
        {
            SecuritySystemUser_Custom currentUser = SecuritySystem.CurrentUser as SecuritySystemUser_Custom;
            if (currentUser != null && currentUser.LoaiTaiKhoan == LoaiTaiKhoanEnum.QuanTriHeThong)
            {
                return true;
            }
            return false;
        }
        /// Kiểm tra tài khoản hiện tại có phải là Tài khoản bình thường của Edu
        public static bool TaiKhoanBinhThuong_Edu()
        {
            SecuritySystemUser_Custom currentUser = SecuritySystem.CurrentUser as SecuritySystemUser_Custom;
            if (currentUser != null && currentUser.CongTy != null && currentUser.LoaiTaiKhoan == LoaiTaiKhoanEnum.TaiKhoanBinhThuong)
            {
                if (currentUser.CongTy.Oid.Equals(Config.KeyTTCEdu))
                    return true;
            }
            return false;
        }

        /// Kiểm tra tài khoản hiện tại có phải là admin Trường hay không
        public static bool QuanTriToanCongTy()
        {
            if ((SecuritySystem.CurrentUser as SecuritySystemUser_Custom).LoaiTaiKhoan == LoaiTaiKhoanEnum.QuanTriCongTy)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Kiểm tra xem có được cấp quyền truy cập không
        /// </summary>
        /// <typeparam name="T">typeof(BaseObject)</typeparam>
        /// <returns></returns>
        public static bool IsAccessGranted(Type type)
        {
            bool result = SecuritySystem.IsGranted(new ClientPermissionRequest(type, null, null, SecurityOperations.Navigate));
            //
            return result;
        }

        /// <summary>
        /// Kiểm tra xem có được cấp quyền xóa không
        /// </summary>
        /// <typeparam name="T">typeof(BaseObject)</typeparam>
        /// <returns></returns>
        public static bool IsDeleteGranted<T>() where T : BaseObject
        {

            return SecuritySystem.IsGranted(new ClientPermissionRequest(typeof(T), null, null, SecurityOperations.Delete));
        }

        /// <summary>
        /// Kiểm tra xem có được cấp quyền sửa không
        /// </summary>
        /// <typeparam name="T">typeof(BaseObject)</typeparam>
        /// <returns></returns>
        public static bool IsWriteGranted<T>() where T : BaseObject
        {

            return SecuritySystem.IsGranted(new ClientPermissionRequest(typeof(T), null, null, SecurityOperations.Write));
        }

        /// <summary>
        /// Kiểm tra xem có được cấp quyền create không
        /// </summary>
        /// <typeparam name="T">typeof(BaseObject)</typeparam>
        /// <returns></returns>
        public static bool IsCreateGranted<T>() where T : BaseObject
        {
            return SecuritySystem.IsGranted(new ClientPermissionRequest(typeof(T), null, null, SecurityOperations.Create));
        }


        /// <summary>
        /// Không hiện nhân viên khi chọn Trường
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool KhongHienNhanVienKhiChonCongTy
        {
            get
            {
                if (CauHinhChung_GetCauHinhChung != null)
                    return CauHinhChung_GetCauHinhChung.CauHinhHoSo.KhongHienNhanVienKhiChonCongTy;
                return false;
            }
        }

        /// Kiểm tra tài khoản hiện tại có quyền duyệt hay không?
        public static bool KiemTraDuyetHeThong(Session sesion, Guid phanHe)
        {
            bool result = false;
            //
            SecuritySystemUser_Custom user = Common.SecuritySystemUser_GetCurrentUser();
            if (user == null) return false;
            //
            if (Common.QuanTriKhoi() || Common.QuanTriToanCongTy())
                return true;
            //
            //
            CriteriaOperator filter = CriteriaOperator.Parse("PhanHe.Oid=? and SecuritySystemUser.Oid=?", phanHe, user.Oid);

            InOperator filter1 = new InOperator("CongTy", user.SecuritySystemRole_Department.Quyen.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
            SecuritySystemRole_Accept duyet = sesion.FindObject<SecuritySystemRole_Accept>(BinaryOperator.And(filter, filter1));
            if (duyet != null)
            {
                result = true;
            }

            //
            return result;
        }


        /// Kiểm tra tài khoản hiện tại có quyền duyệt hay không?
        public static bool KiemTraPhanQuyenDuyet(Session sesion, Guid phanHe, Guid congTy, Guid user)
        {
            bool result = false;
            //
            if (user == null) return false;
            //
            if (Common.QuanTriToanHeThong())
                return true;
            //
            //
            CriteriaOperator filter = CriteriaOperator.Parse("PhanHe = ? and SecuritySystemUser = ? and CongTy = ?", phanHe, user, congTy);
            SecuritySystemRole_Accept duyet = sesion.FindObject<SecuritySystemRole_Accept>(filter);
            if (duyet != null)
            {
                result = true;
            }

            //
            return result;
        }

        /// <summary>
        /// Kiểm phân hệ theo tài khoản
        /// </summary>
        /// <returns></returns>
        public static bool KiemTraPhanHeCuaTaiKhoan(Session sesion, Guid phanHe)
        {
            SecuritySystemUser_Custom currentUser = sesion.GetObjectByKey<SecuritySystemUser_Custom>((SecuritySystem.CurrentUser as SecuritySystemUser_Custom).Oid);
            if (currentUser == null) return false;
            if (currentUser.LoaiTaiKhoan == Enum.Systems.LoaiTaiKhoanEnum.QuanTriHeThong
                || currentUser.LoaiTaiKhoan == Enum.Systems.LoaiTaiKhoanEnum.QuanTriCongTy
                || currentUser.LoaiTaiKhoan == Enum.Systems.LoaiTaiKhoanEnum.QuanTriKhoi
               )
                return true;
            //

            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            InOperator filter1 = new InOperator("Oid", currentUser.Roles);
            CriteriaOperator filter2 = null;
            if (phanHe.ToString() != "00000000-0000-0000-0000-000000000007")
            {
                filter2 = CriteriaOperator.Parse("PhanHe.Oid=?", phanHe);
            }
            go.Operands.Add(filter1);
            go.Operands.Add(filter2);
            //
            XPCollection<SecuritySystemRole_Custom> roleList = new XPCollection<SecuritySystemRole_Custom>(sesion, go);
            if (roleList != null && roleList.Count > 0)
            {
                //
                return true;
            }
            //
            return false;
        }

        /// <summary>
        /// Kiểm tra quyền chức năng phụ of User
        /// </summary>
        /// <returns></returns>
        public static bool CheckRolesBySecuritySystemRole_NonPersitent(Session sesion, Guid phanHe, string appMenu)
        {
            //
            SecuritySystemUser_Custom currentUser = sesion.GetObjectByKey<SecuritySystemUser_Custom>((SecuritySystem.CurrentUser as SecuritySystemUser_Custom).Oid);
            if (currentUser != null)
            {
                GroupOperator go = new GroupOperator(GroupOperatorType.And);
                InOperator filter1 = new InOperator("Oid", currentUser.Roles);
                CriteriaOperator filter2 = CriteriaOperator.Parse("PhanHe.Oid=?", phanHe);
                //
                go.Operands.Add(filter1);
                go.Operands.Add(filter2);
                XPCollection<SecuritySystemRole_Custom> roleList = new XPCollection<SecuritySystemRole_Custom>(sesion, go);
                if (roleList != null)
                {
                    foreach (var item in roleList)
                    {
                        foreach (var appMenuRole in item.ListMenuNonPersistent)
                        {
                            if (appMenuRole.AppMenu.TenChucNang.ToUpper().Equals(appMenu.ToUpper()))
                                return true;
                        }
                    }
                }
            }

            //
            return false;
        }
    }
}
