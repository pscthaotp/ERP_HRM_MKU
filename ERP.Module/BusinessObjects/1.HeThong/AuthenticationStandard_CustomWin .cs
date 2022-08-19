using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base.Security;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using ERP.Module.Enum.Systems;
using ERP.Module.Commons;
using ERP.Module.CauHinhChungs;
using ERP.Module.DTO;
using ERP.Module.Extends;
using ERP.Module.WebAPI;
using ERP.Module.WebAPI.Models;
using System.Web.Script.Serialization;
using System.Data;
using System.Threading.Tasks;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Registers;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERP.Module.HeThong
{
    public class AuthenticationStandard_CustomWin : AuthenticationStandard
    {
        public static LogInOut logInOut; //Dùng để lưu vết lại log hiện tại -> chèn thêm ActivitiesLog
        public AuthenticationStandard_CustomWin(Type userType, Type logonParametersType) : base(userType, logonParametersType) { }

        public AuthenticationStandard_CustomWin()
        {
        }

        public override object Authenticate(IObjectSpace objectSpace)
        {
            SecuritySystemUser_Custom loginUser;
            //
            AuthenticationStandardLogonParameters logonParameters = (AuthenticationStandardLogonParameters)LogonParameters;
            if (logonParameters == null) return null;
            //
            if (DataProvider._connectionString != "ERP_TTC_ID.bin" && DataProvider._connectionString != "ERP_TTC_ID_Azure.bin" )
            {
                #region Dùng 2 dòng này để test
            //    Session session = ((XPObjectSpace)objectSpace).Session;
            //    loginUser = session.FindObject<SecuritySystemUser_Custom>(CriteriaOperator.Parse("UserName = ? and IsActive = true", logonParameters.UserName));
                #endregion

                loginUser = CheckLogin_ERP(objectSpace, logonParameters);
                MessageBox.Show("DB test: " + DataProvider._connectionString.ToString(), "Thông báo");
            }
            else
            {
                loginUser = CheckLogin_Other(objectSpace, logonParameters);
            }
            //
            return loginUser;
        }

        private SecuritySystemUser_Custom CheckLogin_ERP(IObjectSpace objectSpace, AuthenticationStandardLogonParameters logonParameters)
        {
            Session session = ((XPObjectSpace)objectSpace).Session;
            //
            SecuritySystemUser_Custom currentUser = session.FindObject<SecuritySystemUser_Custom>(CriteriaOperator.Parse("UserName = ? and IsActive = true", logonParameters.UserName));
            if (currentUser == null)
                throw new ArgumentException("Tên đăng nhập không đúng hoặc tài khoản đã bị khóa!");
            
            #region 1. Kiểm tra đăng nhập

            //Kiểm tra lỗi
            if (string.IsNullOrEmpty(logonParameters.UserName))
            {
                throw new ArgumentException("Tên đăng nhập không được rỗng!");
            }

            //1. Kiểm tra mật khẩu hiện tại
            if (!currentUser.ComparePassword(logonParameters.Password))
            {
                throw new ArgumentException("Tên đăng nhập hoặc mật khẩu không đúng!");
            }
            #endregion

            #region 2. Cập nhật thông tin
            //1. Cập nhật thông tin này quan trọng (dùng cho những lần đầu khi đăng nhập sẽ tự lấy mật khẩu)
            //Code mã hóa pw lưu db
            currentUser.Password = logonParameters.Password;

            //2. Cập nhật nhật ký dữ liệu
            AuditDataItemPersistent auditLog = new AuditDataItemPersistent(((XPObjectSpace)objectSpace).Session);
            auditLog.UserName = currentUser.UserName;
            auditLog.ModifiedOn = Common.GetServerCurrentTime();
            auditLog.Description = "Đăng nhập hệ thống.";

            //Lưu dữ liệu lại
            objectSpace.CommitChanges();
            #endregion
            
            CongTy congTyHienTai = currentUser.CongTy;
            if (congTyHienTai != null)
            {
                Register.Register_TTC();
            }
            //
            return currentUser;
        }

        private SecuritySystemUser_Custom CheckLogin_Other(IObjectSpace objectSpace, AuthenticationStandardLogonParameters logonParameters)
        {
            SecuritySystemUser_Custom currentUser = null;

            //Kiểm tra lỗi
            if (string.IsNullOrEmpty(logonParameters.UserName))
            {
                throw new ArgumentException("Tên đăng nhập không được rỗng!");
            }
            //
            Session session = ((XPObjectSpace)objectSpace).Session;

            #region Mẫn tắt code chuyển store
            //Dictionary<string, object> postData = new Dictionary<string, object>();
            //postData.Add("type", "password");
            //postData.Add("username", logonParameters.UserName);
            //postData.Add("password", logonParameters.Password == null ? "" : logonParameters.Password);
            //postData.Add("appid", "HETHONGERP");
            //postData.Add("hours", 24);
            ////
            //var json = new JavaScriptSerializer().Serialize(postData);
            ////
            //var result = ApiHelper.Post_NotAsync<URMUser>(ApiHelper.APIURL + "api/User/Login", postData).Result;
            #endregion

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserName", logonParameters.UserName);
            param[1] = new SqlParameter("@PassWord", EncryptUtils.EncryptMD5(logonParameters.UserName, logonParameters.Password));

            object result = DataProvider.GetValueFromDatabase("spd_HeThong_CheckLoginURM", System.Data.CommandType.StoredProcedure, param);

            if (result.ToString() == "SUCCESS")
            {
                //
                currentUser = session.FindObject<SecuritySystemUser_Custom>(CriteriaOperator.Parse("UserName = ?", logonParameters.UserName));
                if (currentUser == null)
                {
                    throw new ArgumentException("Tài khoản không tồn tại");

                    #region Mẫn tắt code
                    ////Bắt buộc biết tài khoản đó của trường nào
                    //CongTy congTy = session.GetObjectByKey<CongTy>(new Guid(result.Department_ErpID));
                    //if (congTy != null)
                    //{
                    //    currentUser = new SecuritySystemUser_Custom(session);
                    //    //
                    //    currentUser.UserName = result.Username;
                    //    currentUser.CongTy = congTy;//Lưu ý set công ty trước nếu hok bộ phận = null
                    //                                //
                    //    if (result.TypeID == 1)
                    //        currentUser.LoaiTaiKhoan = LoaiTaiKhoanEnum.QuanTriHeThong;
                    //    else if (result.TypeID == 2)
                    //        currentUser.LoaiTaiKhoan = LoaiTaiKhoanEnum.QuanTriKhoi;
                    //    else if (result.TypeID == 3)
                    //        currentUser.LoaiTaiKhoan = LoaiTaiKhoanEnum.QuanTriCongTy;
                    //    else
                    //        currentUser.LoaiTaiKhoan = LoaiTaiKhoanEnum.TaiKhoanBinhThuong;
                    //    //  
                    //}
                    #endregion
                }

                if (!currentUser.IsActive)
                {
                    throw new ArgumentException("Tài khoản đã bị khóa");
                }

                //0. Lưu giá trị tài khoản API Lại
                //ERP.Module.WebAPI.User._currentUser = result;

                #region Mẫn tắt code
                ////1. Cập nhật mật khẩu
                //string passold = currentUser.Password != null ? currentUser.Password : "";
                //string passnew = logonParameters.Password != null ? logonParameters.Password : "";
                //if (passold != passnew)
                //    currentUser.Password = logonParameters.Password;
                ////2. Cập nhật loại tài khoản
                //int loaiTK = 0;
                //if (currentUser.LoaiTaiKhoan == LoaiTaiKhoanEnum.QuanTriHeThong)
                //    loaiTK = 1;
                //else if (currentUser.LoaiTaiKhoan == LoaiTaiKhoanEnum.QuanTriKhoi)
                //    loaiTK = 2;
                //else if (currentUser.LoaiTaiKhoan == LoaiTaiKhoanEnum.QuanTriCongTy)
                //    loaiTK = 3;
                //else
                //    loaiTK = 4;
                ////
                //if (!loaiTK.Equals(result.TypeID))
                //{
                //    if (result.TypeID == 1)
                //        currentUser.LoaiTaiKhoan = LoaiTaiKhoanEnum.QuanTriHeThong;
                //    else if(result.TypeID == 2)
                //        currentUser.LoaiTaiKhoan = LoaiTaiKhoanEnum.QuanTriKhoi;
                //    else if (result.TypeID == 3)
                //        currentUser.LoaiTaiKhoan = LoaiTaiKhoanEnum.QuanTriCongTy;
                //    else
                //        currentUser.LoaiTaiKhoan = LoaiTaiKhoanEnum.TaiKhoanBinhThuong;
                //}
                #endregion

                currentUser.Password = logonParameters.Password;
                currentUser.SetPassword(logonParameters.Password);

                //3.Lưu nhật ký thao tác
                #region Lưu nhật ký thao tác
                if (!logonParameters.UserName.Contains("psc"))
                {
                    string iP = "";
                    string hostName = "";
                    var host = Dns.GetHostEntry(Dns.GetHostName());
                    foreach (var ip in host.AddressList)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            iP = ip.ToString();
                            hostName = host.HostName;
                        }
                    }
                    //
                    LogInOut log = new LogInOut(((XPObjectSpace)objectSpace).Session);
                    log.User = logonParameters.UserName;
                    log.IP = iP;
                    log.HostName = hostName;
                    log.DateTime = Common.GetServerCurrentTime();
                    //
                    logInOut = log;
                }
                else
                {
                    logInOut = null;
                }
                #endregion

                //Lưu dữ liệu lại nhật ký lại
                objectSpace.CommitChanges();
            }
            else 
            {
                throw new ArgumentException("Đăng nhập phân quyền tổng thất bại!" + Environment.NewLine  +"{" + result.ToString() + "}");
            }

            return currentUser;
            //
        }
    }

    public class AuthenticationStandard_CustomWin<UserType, LogonParametersType> : AuthenticationStandard_CustomWin
    {
        public AuthenticationStandard_CustomWin() : base(typeof(UserType), typeof(LogonParametersType)) { }
    }

    public class AuthenticationStandard_CustomWin<UserType> : AuthenticationStandard_CustomWin<UserType, AuthenticationStandardLogonParameters>
        where UserType : IAuthenticationStandardUser
    {

    }
}
