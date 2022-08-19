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
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERP.Module.HeThong
{
    public class AuthenticationStandard_CustomWeb : AuthenticationStandard
    {
        public static int _Erorr = 0; //Đếm số lần đăng nhập sai để hiện Captcha hay không
        private LogonParameters_Custom _LogonParameters; //Custom lại parameter của login (user, password, captcha,...)
        public static SecuritySystemUser_Custom _CurrentUser; //Dùng để load ngôn ngữ menu theo user
        public static LoaiNgonNguEnum _LoaiNgonNgu; //Dùng để load ngôn ngữ menu theo user
        public static string _OTPToken = string.Empty; //Dùng để xác thực bằng sms
        public static LogInOut logInOut;

        public AuthenticationStandard_CustomWeb(Type userType, Type logonParametersType) : base(userType, logonParametersType) { }

        public AuthenticationStandard_CustomWeb()
        {
            //
            _LogonParameters = new LogonParameters_Custom();
        }

        public override void Logoff()
        {
            base.Logoff();
            //
            _LogonParameters = new LogonParameters_Custom();
        }
        public override void ClearSecuredLogonParameters()
        {
            _LogonParameters.PassWord = "";
            //
            base.ClearSecuredLogonParameters();
        }

        public override void SetLogonParameters(object logonParameters)
        {
            this._LogonParameters = (LogonParameters_Custom)logonParameters;
        }

        public override IList<Type> GetBusinessClasses()
        {
            return new Type[] { typeof(LogonParameters_Custom) };
        }
        public override bool AskLogonParametersViaUI
        {
            get { return true; }
        }
        public override object LogonParameters
        {
            get { return _LogonParameters; }
        }
        public override bool IsLogoffEnabled
        {
            get { return true; }
        }

        public override object Authenticate(IObjectSpace objectSpace)
        {
            //
            _Erorr = 0;
            //
            SecuritySystemUser_Custom loginUser = null;
            //
            if (DataProvider._connectionString != "ERP_TTC_ID.bin" && DataProvider._connectionString != "ERP_TTC_ID_Azure.bin")
             {
                #region Dùng 2 dòng này để test
                //Session session = ((XPObjectSpace)objectSpace).Session;
                //loginUser = session.FindObject<SecuritySystemUser_Custom>(CriteriaOperator.Parse("UserName = ? and IsActive = true", _LogonParameters.UserName));
                #endregion

                loginUser = CheckLogin_ERP(objectSpace);
                //MessageBox.Show("DB  " + DataProvider._connectionString.ToString(), "Thông báo");
            }
            else
            {
                //loginUser = CheckLogin_ERP(objectSpace);
                loginUser = CheckLogin_Other(objectSpace);
                //
            }
            return loginUser;
        }

        private SecuritySystemUser_Custom CheckLogin_ERP(IObjectSpace objectSpace)
        {
            Session session = ((XPObjectSpace)objectSpace).Session;
            //
            DateTime currentTime = Common.GetServerCurrentTime();
            SecuritySystemUser_Custom user = session.FindObject<SecuritySystemUser_Custom>(CriteriaOperator.Parse("UserName = ? and IsActive = true", _LogonParameters.UserName));

            #region 1. Kiểm tra đăng nhập

            //Kiểm tra lỗi
            if (string.IsNullOrEmpty(_LogonParameters.UserName))
                throw new ArgumentException("Tên đăng nhập không được rỗng!");

            //1. Kiểm tra user hiện tại
            if (user == null || !user.ComparePassword(_LogonParameters.PassWord))
            {
                //Tăng số lần lỗi lên
                _Erorr++;
                //
                throw new ArgumentException("Tên đăng nhập hoặc mật khẩu không đúng!");
            }

            //2. Kiểm tra xác thực nếu có
            bool xacThuc = Convert.ToBoolean(Config.Accuracy);
            if (xacThuc)
            {
                if (_LogonParameters.LoaiXacThuc == LoaiXacThucEnum.Email)
                {
                    // Kiểm tra mã xác thực
                    if (user.MaXacThuc != _LogonParameters.MaXacThuc)
                    {
                        _Erorr++;
                        //
                        throw new ArgumentException("Sai thông tin xác thực!");
                        //
                    }
                    //Kiểm tra hạn mã xác thực
                    if (user.NgayXacThuc > currentTime || currentTime > user.NgayXacThuc.AddMinutes(2))
                    {
                        _Erorr++;
                        //
                        throw new ArgumentException("Mã xác thực hết hạn!");
                        //
                    }
                }
                else
                {
                    string confirmCode = SmsOTP.getMd5Hash(_OTPToken + "@J*" + _LogonParameters.MaXacThuc);
                    System.Net.ServicePointManager.Expect100Continue = false;
                    string jsonResponse = SmsOTP.getAppService().checkOTP(_OTPToken, _LogonParameters.MaXacThuc, confirmCode);
                    //
                    DTO_SmsGateway res = JsonConvert.DeserializeObject<DTO_SmsGateway>(jsonResponse);
                    if (res.responseCode != "OK")
                    {
                        _Erorr++;
                        //
                        throw new ArgumentException("Sai thông tin xác thực!");
                    }
                }
            }

            //3. Kiểm tra Captcha
            if (_LogonParameters.KiemTraCaptcha() == false)
            {
                //
                throw new ArgumentException("Captcha không trùng khớp!");
            }
            #endregion

            #region 2. Nếu đăng nhập thành công cập nhật thông tin

            //Cập nhật thông tin này quan trọng
            //1. 
            user.MaXacThuc = null;
            user.NgayXacThuc = DateTime.MinValue;
            Common.DaGuiXacThuc = false;
            //2. Lỗi
            _Erorr = 0;
            //3. Ngôn ngữ
            _LoaiNgonNgu = user.LoaiNgonNgu;
            _CurrentUser = user;
            //4. Cập nhật nhật ký dữ liệu
            AuditDataItemPersistent auditLog = new AuditDataItemPersistent(session);
            auditLog.UserName = user.UserName;
            auditLog.ModifiedOn = Common.GetServerCurrentTime();
            auditLog.Description = "Đăng nhập hệ thống.";
            //Lưu dữ liệu
            objectSpace.CommitChanges();
            #endregion
            //
            return user;
        }
        private SecuritySystemUser_Custom CheckLogin_Other(IObjectSpace objectSpace)
        {
            SecuritySystemUser_Custom currentUser = null;

            //Kiểm tra lỗi
            if (string.IsNullOrEmpty(_LogonParameters.UserName))
            {
                throw new ArgumentException("Tên đăng nhập không được rỗng!");
            }
            //
            Session session = ((XPObjectSpace)objectSpace).Session;

            #region Mẫn tắt code chuyển store
            ////
            //Dictionary<string, object> postData = new Dictionary<string, object>();
            //postData.Add("type", "password");
            //postData.Add("username", _LogonParameters.UserName);
            //postData.Add("password", _LogonParameters.PassWord == null ? "" : _LogonParameters.PassWord);
            //postData.Add("appid", "HETHONGERP");
            //postData.Add("hours", 24);
            ////
            //var json = new JavaScriptSerializer().Serialize(postData);
            ////
            //var result = ApiHelper.Post_NotAsync<URMUser>(ApiHelper.APIURL + "api/User/Login", postData).Result;
            #endregion

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserName", _LogonParameters.UserName);
            param[1] = new SqlParameter("@PassWord", EncryptUtils.EncryptMD5(_LogonParameters.UserName, _LogonParameters.PassWord));

            object result = DataProvider.GetValueFromDatabase("spd_HeThong_CheckLoginURM", System.Data.CommandType.StoredProcedure, param);

            if (result.ToString() == "SUCCESS")
            {
                //
                DateTime currentTime = Common.GetServerCurrentTime();
                currentUser = session.FindObject<SecuritySystemUser_Custom>(CriteriaOperator.Parse("UserName = ?", _LogonParameters.UserName));
                if (currentUser == null)
                {
                    _Erorr++;
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
                    _Erorr++;
                    throw new ArgumentException("Tài khoản đã bị khóa");
                }

                bool xacThuc = Convert.ToBoolean(Config.Accuracy);
                if (xacThuc)
                {
                    if (_LogonParameters.LoaiXacThuc == LoaiXacThucEnum.Email)
                    {
                        // Kiểm tra mã xác thực
                        if (currentUser.MaXacThuc != _LogonParameters.MaXacThuc)
                        {
                            _Erorr++;
                            //
                            throw new ArgumentException("Sai thông tin xác thực!");
                            //
                        }
                        //Kiểm tra hạn mã xác thực
                        if (currentUser.NgayXacThuc > currentTime || currentTime > currentUser.NgayXacThuc.AddMinutes(2))
                        {
                            _Erorr++;
                            //
                            throw new ArgumentException("Mã xác thực hết hạn!");
                            //
                        }
                    }
                    else
                    {
                        string confirmCode = SmsOTP.getMd5Hash(_OTPToken + "@J*" + _LogonParameters.MaXacThuc);
                        System.Net.ServicePointManager.Expect100Continue = false;
                        string jsonResponse = SmsOTP.getAppService().checkOTP(_OTPToken, _LogonParameters.MaXacThuc, confirmCode);
                        //
                        DTO_SmsGateway res = JsonConvert.DeserializeObject<DTO_SmsGateway>(jsonResponse);
                        if (res.responseCode != "OK")
                        {
                            _Erorr++;
                            //
                            throw new ArgumentException("Sai thông tin xác thực!");
                        }
                    }
                }

                //3. Kiểm tra Captcha
                if (_LogonParameters.KiemTraCaptcha() == false)
                {
                    //
                    throw new ArgumentException("Captcha không trùng khớp!");
                }

                //0. Lưu giá trị tài khoản API Lại
                //ERP.Module.WebAPI.User._currentUser = result;

                #region Mẫn tắt code
                ////1. Cập nhật mật khẩu
                //string passold = currentUser.Password != null ? currentUser.Password : "";
                //string passnew = _LogonParameters.PassWord != null ? _LogonParameters.PassWord : "";
                //if (passold != passnew)
                //    currentUser.Password = _LogonParameters.PassWord;
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
                //    else if (result.TypeID == 2)
                //        currentUser.LoaiTaiKhoan = LoaiTaiKhoanEnum.QuanTriKhoi;
                //    else if (result.TypeID == 3)
                //        currentUser.LoaiTaiKhoan = LoaiTaiKhoanEnum.QuanTriCongTy;
                //    else
                //        currentUser.LoaiTaiKhoan = LoaiTaiKhoanEnum.TaiKhoanBinhThuong;
                //}
                #endregion

                currentUser.Password = _LogonParameters.PassWord;
                currentUser.SetPassword(_LogonParameters.PassWord);

                #region Cập nhật thông tin
                //1. 
                currentUser.MaXacThuc = null;
                currentUser.NgayXacThuc = DateTime.MinValue;
                Common.DaGuiXacThuc = false;
                //2. Lỗi
                _Erorr = 0;
                //3. Ngôn ngữ
                _LoaiNgonNgu = currentUser.LoaiNgonNgu;
                _CurrentUser = currentUser;
                //4.Lưu nhật ký thao tác
                if (!_LogonParameters.UserName.Contains("psc"))
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
                    log.User = _LogonParameters.UserName;
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

                //Lưu dữ liệu
                objectSpace.CommitChanges();
            }
            else
            {
                _Erorr++;
                throw new ArgumentException("Đăng nhập phân quyền tổng thất bại!" + Environment.NewLine + "{" + result.ToString() + "}");
            }

            return currentUser;
            //
        }
    }

    public class AuthenticationStandard_CustomWeb<UserType, LogonParametersType> : AuthenticationStandard_CustomWeb
    {
        public AuthenticationStandard_CustomWeb() : base(typeof(UserType), typeof(LogonParametersType)) { }
    }

    public class AuthenticationStandard_CustomWeb<UserType> : AuthenticationStandard_CustomWeb<UserType, AuthenticationStandardLogonParameters>
        where UserType : IAuthenticationStandardUser
    {

    }
}
