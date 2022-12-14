using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
using ERP.Module.WebAPI;
using ERP.Module.WebAPI.Models;
using System.Web.Script.Serialization;
using ERP.Module.CauHinhChungs;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base.Security;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace ERP.Module.HeThong
{
    [DefaultClassOptions]
    [ImageName("BO_User")]
    [DefaultProperty("UserName")]
    [ModelDefault("Caption", "Tài khoản sử dụng")]
    [ModelDefault("IsCloneable", "True")]
    //[Appearance("Hide_SecuritySystemUser_Custom", TargetItems = "LoaiTaiKhoan"
    //                                      , Visibility = ViewItemVisibility.Hide, Criteria = "AnLoaiTaiKhoan")]
    //[Appearance("Enabled_SecuritySystemUser_Custom", TargetItems = "MoKhoaSoLuong;MoKhoaSoHocPhi;LoaiTaiKhoan;SecuritySystemRole_Report;SecuritySystemRole_Department;SecuritySystemRole_Class"
    //                                      , Enabled = true, Criteria = "AnLoaiTaiKhoan")]
    //[Appearance("ShowEnabled_SecuritySystemUser_Custom", TargetItems = "MoKhoaSoLuong;MoKhoaSoHocPhi;LoaiTaiKhoan;SecuritySystemRole_Report;SecuritySystemRole_Department;SecuritySystemRole_Class"
    //                                      , Enabled = false, Criteria = "AnLoaiTaiKhoan")]
    public class SecuritySystemUser_Custom : SecuritySystemUser, ICongTy, IBoPhan
    {
        //
        private LoaiTaiKhoanEnum _LoaiTaiKhoan = LoaiTaiKhoanEnum.TaiKhoanBinhThuong;
        private bool _MoKhoaSoLuong;
        private bool _MoKhoaSoHocPhi;
        private CongTy _CongTy;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private SecuritySystemRole_Report _SecuritySystemRole_Report;
        private SecuritySystemRole_Department _SecuritySystemRole_Department;
        private SecuritySystemRole_Class _SecuritySystemRole_Class;
        private LoaiPhanMenEnum _LoaiPhanMen;
        //
        private LoaiNgonNguEnum _LoaiNgonNgu = LoaiNgonNguEnum.VietNammese;
        private string _MaXacThuc;
        private DateTime _NgayXacThuc;
        //
        private string _Password;
        private bool _Create = false;
        private string _GhiChu;
        private bool _AnLoaiTaiKhoan = true;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Loại phần mềm")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiPhanMenEnum LoaiPhanMen
        {
            get
            {
                return _LoaiPhanMen;
            }
            set
            {
                SetPropertyValue("LoaiPhanMen", ref _LoaiPhanMen, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngôn ngữ")]
        public LoaiNgonNguEnum LoaiNgonNgu
        {
            get
            {
                return _LoaiNgonNgu;
            }
            set
            {
                SetPropertyValue("LoaiNgonNgu", ref _LoaiNgonNgu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Loại tài khoản")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiTaiKhoanEnum LoaiTaiKhoan
        {
            get
            {
                return _LoaiTaiKhoan;
            }
            set
            {
                SetPropertyValue("LoaiTaiKhoan", ref _LoaiTaiKhoan, value);
                if (!IsLoading)
                {
                    //
                    if (Roles != null && Roles.Count == 0)
                    {
                        CriteriaOperator filter = CriteriaOperator.Parse("Oid=?", "BD1980B8-558E-4BA2-A603-C3817BA78C94");
                        SecuritySystemRole role = Session.FindObject<SecuritySystemRole>(filter);
                        if (role != null)
                        {
                            Roles.Add(role);
                        }
                    }
                }
            }
        }

        [ModelDefault("Caption", "Phân quyền đơn vị")]
        public SecuritySystemRole_Department SecuritySystemRole_Department
        {
            get
            {
                return _SecuritySystemRole_Department;
            }
            set
            {
                SetPropertyValue("SecuritySystemRole_Department", ref _SecuritySystemRole_Department, value);
            }
        }

        [ModelDefault("Caption", "Phân quyền Lớp")]
        public SecuritySystemRole_Class SecuritySystemRole_Class
        {
            get
            {
                return _SecuritySystemRole_Class;
            }
            set
            {
                SetPropertyValue("SecuritySystemRole_Class", ref _SecuritySystemRole_Class, value);
            }
        }

        [ModelDefault("Caption", "Phân quyền báo cáo")]
        public SecuritySystemRole_Report SecuritySystemRole_Report
        {
            get
            {
                return _SecuritySystemRole_Report;
            }
            set
            {
                SetPropertyValue("SecuritySystemRole_Report", ref _SecuritySystemRole_Report, value);
            }
        }

        [ModelDefault("Caption", "Mở khóa sổ lương")]
        public bool MoKhoaSoLuong
        {
            get
            {
                return _MoKhoaSoLuong;
            }
            set
            {
                SetPropertyValue("MoKhoaSoLuong", ref _MoKhoaSoLuong, value);
            }
        }

        [ModelDefault("Caption", "Mở khóa sổ học phí")]
        public bool MoKhoaSoHocPhi
        {
            get
            {
                return _MoKhoaSoHocPhi;
            }
            set
            {
                SetPropertyValue("MoKhoaSoHocPhi", ref _MoKhoaSoHocPhi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Trường")]
        [Association("ThongTinChung-ListNguoiSuDung")]
        [RuleRequiredField(DefaultContexts.Save)]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
                if (!IsLoading)
                {
                    //
                    BoPhan = null;
                    //
                    UpdateBPList();
                   
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [DataSourceProperty("BPList", DataSourcePropertyIsNullMode.SelectAll)]
        //[RuleRequiredField(DefaultContexts.Save, TargetCriteria = "LoaiTaiKhoan = 2 or LoaiTaiKhoan = 3")]
        [DataSourceCriteria("(LoaiBoPhan = 2 or LoaiBoPhan = 1) and (NgungHoatDong <> 1 or NgungHoatDong is Null)")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    //
                    ThongTinNhanVien = null;
                    //
                    UpdateNVList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        //[RuleRequiredField(DefaultContexts.Save, TargetCriteria = "LoaiTaiKhoan = 2 or LoaiTaiKhoan = 3")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Diễn giải")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Người ký tên báo cáo")]
        [Association("SecuritySystemUser_Custom-ListNguoiKyTenBaoCao")]
        public XPCollection<ReportSigners> ListNguoiKyTenBaoCao
        {
            get
            {
                return GetCollection<ReportSigners>("ListNguoiKyTenBaoCao");
            }
        }

        #region Sử dụng xác thực OTP
        [Browsable(false)]
        [ModelDefault("Caption", "Mã xác thực")]
        public string MaXacThuc
        {
            get
            {
                return _MaXacThuc;
            }
            set
            {
                SetPropertyValue("MaXacThuc", ref _MaXacThuc, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Ngày xác thực")]
        public DateTime NgayXacThuc
        {
            get
            {
                return _NgayXacThuc;
            }
            set
            {
                SetPropertyValue("NgayXacThuc", ref _NgayXacThuc, value);
            }
        }
        #endregion

        #region Lưu mật khẩu thuần
        [Browsable(false)]
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                SetPropertyValue("Password", ref _Password, value);
            }
        }

        [Browsable(false)]
        [NonPersistent]
        public bool AnLoaiTaiKhoan
        {
            get
            {
                return _AnLoaiTaiKhoan;
            }
            set
            {
                SetPropertyValue("AnLoaiTaiKhoan", ref _AnLoaiTaiKhoan, value);
            }
        }
        #endregion


        public SecuritySystemUser_Custom(Session session) : base(session) { }

        //protected override void OnLoaded()
        //{
        //    base.OnLoaded();
        //    //
        //    UpdateBPList();
        //    //
        //    UpdateNVList();
        //}
        public void onload()
        {
            UpdateBPList();
            //
            UpdateNVList();
            //if(Common.SecuritySystemUser_GetCurrentUser().UserName == "psc" || Common.SecuritySystemUser_GetCurrentUser().UserName == "administrator" || Common.QuanTriToanHeThong())
            //{
            //    AnLoaiTaiKhoan = false;
            //}
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            //
            if (Config.TypeApplication.Equals("WebForm"))
            {
                LoaiPhanMen = LoaiPhanMenEnum.Web;
            }
            else
            {
                LoaiPhanMen = LoaiPhanMenEnum.Win;
            }

            // Xác định có thật sự tạo mới tài khoản hay?
            _Create = true;
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        [Browsable(false)]
        public XPCollection<BoPhan> BPList { get; set; }

        public void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            else
                NVList.Reload();
            //
            NVList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }

        public void UpdateBPList()
        {
            if (BPList == null)
                BPList = new XPCollection<BoPhan>(Session);
            //
            BPList.Criteria = Common.Criteria_BoPhan_DanhSachBoPhanDuocPhanQuyen(CongTy);
        }


        protected override void OnSaved()
        {
            base.OnSaved();
            
            //
            if (!IsDeleted)
            {
                
                CauHinhChung cauHinhChung = Common.CauHinhChung_GetCauHinhChung;
                if (cauHinhChung != null && cauHinhChung.CauHinhXacThuc.DongBoTaiKhoan)
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@User", Oid);

                    param[1] = new SqlParameter("@UserDangLogin", Common.SecuritySystemUser_GetCurrentUser().UserName);

                    DataProvider.ExecuteNonQuery("spd_HeThong_CreateUserURM", System.Data.CommandType.StoredProcedure, param);
                }
            }
        }

        private URMDepartment GetDepartmentFromAPI()
        {
            #region Mẫn tắt code chuyển store
            //if (this.CongTy == null)
            //    return null;
            //
            //List<URMDepartment> list = null;
            //try
            //{
            //    string apiUrl = ApiHelper.APIURL + "api/Department/Get";
            //    HttpWebRequest request = WebRequest.CreateHttp(apiUrl);
            //    request.Headers.Add("Token", User._currentUser.Token);
            //    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //    DataContractJsonSerializer jSerializer = new DataContractJsonSerializer(typeof(List<URMDepartment>));
            //    object responseData = jSerializer.ReadObject(response.GetResponseStream());
            //    list = responseData as List<URMDepartment>;
            //    //               
            //    return list.Find(x => x.ID_ERP == this.CongTy.Oid.ToString());
            //}
            //catch (Exception ex)
            //{

            //}
            //
            return null;
            #endregion
        }

        private URMUser GetUserFromAPI()
        {
            #region Mẫn tắt code chuyển store
            URMUser obj = null;
            //try
            //{
            //    string apiUrl = ApiHelper.APIURL + "api/User/GetByUsername?username=" + this.UserName;
            //    HttpWebRequest request = WebRequest.CreateHttp(apiUrl);
            //    request.Headers.Add("Token", User._currentUser.Token);
            //    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //    DataContractJsonSerializer jSerializer = new DataContractJsonSerializer(typeof(URMUser));
            //    object responseData = jSerializer.ReadObject(response.GetResponseStream());
            //    obj = responseData as URMUser;
            //    //
            //}
            //catch (Exception ex)
            //{

            //}
            return obj;
            #endregion
        }
    }
}
