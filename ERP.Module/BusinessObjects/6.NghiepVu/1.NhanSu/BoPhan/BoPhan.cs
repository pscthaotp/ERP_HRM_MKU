using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.Data.SqlClient;
using DevExpress.Data.Filtering;
using ERP.Module.Enum.NhanSu;
using System.Drawing;
using ERP.Module.Commons;
using DevExpress.Office.Utils;
using System.Collections.Generic;
using ERP.Module.WebAPI.Models;
using System.Net;
using ERP.Module.WebAPI;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using ERP.Module.CauHinhChungs;
using System.Data;

namespace ERP.Module.NghiepVu.NhanSu.BoPhans
{
    [DefaultClassOptions]
    [ImageName("BO_Category")]
    [DefaultProperty("TenBoPhan")]
    [ModelDefault("Caption", "Đơn vị, phòng ban")]
    [ModelDefault("AllowNew", "False")]
    [RuleCombinationOfPropertiesIsUnique("Mã quản lý, Tên bộ phận bị trùng", DefaultContexts.Save, "MaQuanLy;TenBoPhan;CongTy")]
    public class BoPhan : BaseObject, IBoPhan, ITreeNode, ITreeNodeImageProvider, ICongTy
    {
        private decimal _STT;
        private string _MaQuanLy;
        private string _CostCenter;
        private string _MaBoPhan;
        private string _TenBoPhan;
        private LoaiBoPhanEnum _LoaiBoPhan = LoaiBoPhanEnum.PhongBan;
        private BoPhan _BoPhanCha;
        private bool _NgungHoatDong;
        private CongTy _CongTy;
        private CapDonViEnum _CapDonVi;
        private bool _KhongTinhLuong;
        private bool _Create;

        [ModelDefault("Caption", "Số thứ tự")]
        [ModelDefault("EditMask", "N3")]
        [ModelDefault("DisplayFormat", "N3")]
        public decimal STT
        {
            get
            {
                return _STT;
            }
            set
            {
                SetPropertyValue("STT", ref _STT, value);
            }
        }

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Mã phân bổ")]
        public string CostCenter
        {
            get
            {
                return _CostCenter;
            }
            set
            {
                SetPropertyValue("CostCenter", ref _CostCenter, value);
            }
        }

        //Dùng để tạo mã tập đoàn
        [ModelDefault("Caption", "Mã đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaBoPhan
        {
            get
            {
                return _MaBoPhan;
            }
            set
            {
                SetPropertyValue("MaBoPhan", ref _MaBoPhan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tên Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenBoPhan
        {
            get
            {
                return _TenBoPhan;
            }
            set
            {
                SetPropertyValue("TenBoPhan", ref _TenBoPhan, value);
                if (!IsLoading && !String.IsNullOrEmpty(value))
                {
                    AfterChangeTenBoPhan();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Loại đơn vị")]
        //[ModelDefault("AllowEdit", "False")]
        public LoaiBoPhanEnum LoaiBoPhan
        {
            get
            {
                return _LoaiBoPhan;
            }
            set
            {
                SetPropertyValue("LoaiBoPhan", ref _LoaiBoPhan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cấp đơn vị")]
        public CapDonViEnum CapDonVi
        {
            get
            {
                return _CapDonVi;
            }
            set
            {
                SetPropertyValue("CapDonVi", ref _CapDonVi, value);
            }
        }

        //[Browsable(false)]
        [ModelDefault("Caption", "Thuộc Đơn vị")]
        [Association("ParentBoPhan-ChildBoPhan")]
        public BoPhan BoPhanCha
        {
            get
            {
                return _BoPhanCha;
            }
            set
            {
                SetPropertyValue("BoPhanCha", ref _BoPhanCha, value);
            }
        }

        [ModelDefault("Caption", "Trường")]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
            }
        }

        [Association("ParentBoPhan-ChildBoPhan")]
        [ModelDefault("Caption", "Danh sách Đơn vị trực thuộc")]
        [Aggregated]
        public XPCollection<BoPhan> ListBoPhanCon
        {
            get
            {
                //
                XPCollection<BoPhan> boPhanConList = GetCollection<BoPhan>("ListBoPhanCon");
                if (!Common.QuanTriToanHeThong())
                {
                    List<string> listRoled = Common.Department_GetRoledDepartmentList_ByCurrentUser();
                    //
                    CriteriaOperator filter = new InOperator("Oid", listRoled);
                    boPhanConList.Criteria = filter;
                }
                //
                return boPhanConList;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngừng hoạt động")]
        public bool NgungHoatDong
        {
            get
            {
                return _NgungHoatDong;
            }
            set
            {
                SetPropertyValue("NgungHoatDong", ref _NgungHoatDong, value);
                if (!IsLoading)
                {
                    foreach (BoPhan item in this.ListBoPhanCon)
                    {
                        item.NgungHoatDong = NgungHoatDong;
                    }
                }
            }
        }


        [ImmediatePostData]
        [ModelDefault("Caption", "Không tính lương")]
        public bool KhongTinhLuong
        {
            get
            {
                return _KhongTinhLuong;
            }
            set
            {
                SetPropertyValue("KhongTinhLuong", ref _KhongTinhLuong, value);
            }
        }

        #region Đặc biệt Custom cho web 
        [Browsable(false)]
        public Guid ID { get; set; }
        [Browsable(false)]
        public Guid ParentID { get; set; }
        #endregion

        public Image GetImage(out string imageName)
        {
            imageName = "BO_GiaDinh_32x32";
            return ImageLoader.Instance.GetImageInfo(imageName).Image;
        }

        protected virtual void AfterChangeTenBoPhan()
        { }

        //Chỉ dùng để phân quyền
        [NonPersistent]
        [Browsable(false)]
        BoPhan IBoPhan.BoPhan
        {
            get { return this; }
        }

        public BoPhan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            if (LoaiBoPhan != LoaiBoPhanEnum.CongTy)
                CongTy = Commons.Common.CongTy(Session);
            //
            _Create = true;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            //Gọi store cập nhật danh mục bộ phận bên TS
            SqlParameter[] param = new SqlParameter[0];
            DataProvider.ExecuteNonQuery("spd_TSCD_CapNhatDanhMucBoPhanTS", System.Data.CommandType.StoredProcedure, param);

            //
            if (!IsDeleted)
            {
                //Cập nhật cho web
                ID = this.Oid;
                ParentID = this.BoPhanCha != null ? this.BoPhanCha.Oid : Guid.Empty;

                //Cập nhật cho phân quyền tổng
                /*
                CauHinhChung cauHinhChung = Common.CauHinhChung_GetCauHinhChung;
                if (cauHinhChung != null && cauHinhChung.CauHinhXacThuc.DongBoTaiKhoan)
                {
                    //
                    if (_Create)
                        TaoBoPhanChoPhanQuyenTong();
                    else
                        CapNhatBoPhanChoPhanQuyenTong();
                }
               */

                //Cập nhật công ty cho hồ sơ khi thay đổi bộ phận
                /*
                if (CongTy != null && LoaiBoPhan != LoaiBoPhanEnum.Khoi)
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@BoPhan", this.Oid);
                    param[1] = new SqlParameter("@CongTy", CongTy.Oid); 

                    DataProvider.ExecuteNonQuery("spd_BoPhan_CapNhatCongTyChoHoSo", CommandType.StoredProcedure, param);
                }
                */
            }
        }
        [ImmediatePostData]
        IBindingList ITreeNode.Children
        {
            get
            {

                return ListBoPhanCon;
            }
        }

        string ITreeNode.Name
        {
            get { return TenBoPhan; }
        }

        ITreeNode ITreeNode.Parent
        {
            get { return BoPhanCha; }
        }

        async void CapNhatBoPhanChoPhanQuyenTong()
        {
            //1. Lấy bộ phận
            URMUserGroup boPhan = GetUserGroupFromAPI().Find(x => x.ErpID == this.Oid.ToString());
            if (boPhan != null)
            {
                if (this.MaQuanLy != boPhan.UserGroupID || this.TenBoPhan != boPhan.UserGroupName)
                {
                    //1. Cập bộ phận cho phân quyền tổng
                    boPhan.UserGroupName = this.TenBoPhan; // Thay đổi
                    boPhan.UserGroupID = this.MaQuanLy; // Thay đổi
                    //
                    var json = new JavaScriptSerializer().Serialize(boPhan);
                    //
                    var result = await ApiHelper.Post<string>(ApiHelper.APIURL + "api/UserGroup/Update", boPhan);
                    //
                    if (result == "OK")
                    {

                    }
                }
            }
            else //Trương hợp này rất hiếm xảy ra, trừ khi trường hợp trên lỗi
            {
                TaoBoPhanChoPhanQuyenTong();
            }
        }

        async void TaoBoPhanChoPhanQuyenTong()
        {
            List<URMUserGroup> groupList = GetUserGroupFromAPI();

            //1. Lấy bộ phận cha
            URMUserGroup boPhanCha = groupList.Find(x => x.ErpID == this.BoPhanCha.Oid.ToString());
            if (boPhanCha != null)
            {
                URMUserGroup newDepartment = groupList.Find(x => x.ErpID == this.Oid.ToString());
                if (newDepartment == null) //Không có thì tạo mới
                {
                    //1. Tạo bộ phận cho phân quyền tổng
                    newDepartment = new URMUserGroup();
                    newDepartment.ErpID = this.Oid.ToString();
                    newDepartment.ParentID = boPhanCha.ID;
                    newDepartment.UserGroupName = this.TenBoPhan;
                    newDepartment.UserGroupID = this.MaQuanLy;
                    //
                    var json = new JavaScriptSerializer().Serialize(newDepartment);
                    //
                    var result = await ApiHelper.Post<URMUserGroup>(ApiHelper.APIURL + "api/UserGroup/Insert", newDepartment);
                    //
                    if (result.Message == "OK")
                    {

                    }
                }
            }
        }

        private List<URMUserGroup> GetUserGroupFromAPI()
        {
            string apiUrl = ApiHelper.APIURL + "api/UserGroup/Get";
            HttpWebRequest request = WebRequest.CreateHttp(apiUrl);
            request.Headers.Add("Token", ERP.Module.WebAPI.User._currentUser.Token);
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            DataContractJsonSerializer jSerializer = new DataContractJsonSerializer(typeof(List<URMUserGroup>));
            object responseData = jSerializer.ReadObject(response.GetResponseStream());
            List<URMUserGroup> list = responseData as List<URMUserGroup>;
            //
            return list;
        }
    }
}
