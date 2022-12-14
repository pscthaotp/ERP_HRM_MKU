using System;
using System.ComponentModel;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Persistent.Validation;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
using DevExpress.ExpressApp.Security.Strategy;
using ERP.Module.DanhMuc.System;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp;
//
namespace ERP.Module.HeThong
{
    [DefaultClassOptions]
    [DefaultProperty("Name")]
    [ImageName("BO_NewContract")]
    [ModelDefault("Caption", "Phân quyền chức năng")]
    [ModelDefault("IsCloneable", "True")]
    public class SecuritySystemRole_Custom : SecuritySystemRole
    {
        private PhanHe _PhanHe;
        private LoaiPhanMenEnum _LoaiPhanMen;

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

        [ModelDefault("Caption", "Phân hệ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public PhanHe PhanHe
        {
            get
            {
                return _PhanHe;
            }
            set
            {
                SetPropertyValue("PhanHe", ref _PhanHe, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chức năng phụ")]
        [Association("SecuritySystemRole_MenuNonPersistent-ListMenuNonPersistent")]
        public XPCollection<SecuritySystemRole_MenuNonPersistent> ListMenuNonPersistent
        {
            get
            {
                return GetCollection<SecuritySystemRole_MenuNonPersistent>("ListMenuNonPersistent");
            }
        }

        public SecuritySystemRole_Custom(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction(); 
            //
            if (Config.TypeApplication.Equals("WebForm"))
            {
                LoaiPhanMen = LoaiPhanMenEnum.Web;
            }
            else
            {
                LoaiPhanMen = LoaiPhanMenEnum.Win;
            }
        }


        //Viết lại hàm này để gắn phân quyền bằng store nhanh hơn
        protected override IEnumerable<DevExpress.ExpressApp.Security.IOperationPermission> GetPermissionsCore()
        {
            //Hiện tại vẫn chưa sửa cho WebApp, sửa cho WinApp chạy thử
            if (!(Config.TypeApplication.Equals("WebForm")))
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@User", Common.SecuritySystemUser_GetCurrentUser().Oid);
                DataTable tb = DataProvider.GetDataTable("spd_HeThong_GetPermissions", System.Data.CommandType.StoredProcedure, p);

                List<IOperationPermission> list = new List<IOperationPermission>();

                foreach (DataRow item in tb.Rows)
                {
                    //Nếu store trả ra có check IsAdmin nghĩa là đc phân vào nhóm quyền IsAdministrator
                    //Khi đó chỉ cần add 1 quyền duy nhất là IsAdministratorPermission rồi break cho nhanh
                    if (Convert.ToBoolean(item["IsAdmin"]))
                    {
                        list.Add(new DevExpress.ExpressApp.Security.IsAdministratorPermission());
                        break;
                    }
                    else
                    {
                        //Phải dùng code này để lấy Type mà k dùng Type.GetType() vì các class của Devexpress GetType không lấy được
                        DevExpress.ExpressApp.DC.ITypeInfo iTypeInfo = XafTypesInfo.Instance.FindTypeInfo(item["TargetType"].ToString());
                        if (iTypeInfo != null)
                        {
                            Type type = iTypeInfo.Type;
                            if (type != null)
                            {
                                if (Convert.ToBoolean(item["AllowRead"]))
                                {
                                    list.Add(new TypeOperationPermission(type, "Read"));
                                }
                                if (Convert.ToBoolean(item["AllowWrite"]))
                                {
                                    list.Add(new TypeOperationPermission(type, "Write"));
                                }
                                if (Convert.ToBoolean(item["AllowCreate"]))
                                {
                                    list.Add(new TypeOperationPermission(type, "Create"));
                                }
                                if (Convert.ToBoolean(item["AllowDelete"]))
                                {
                                    list.Add(new TypeOperationPermission(type, "Delete"));
                                }
                                if (Convert.ToBoolean(item["AllowNavigate"]))
                                {
                                    list.Add(new TypeOperationPermission(type, "Navigate"));
                                }
                            }
                        }
                    }
                }
                //
                return list;
            }
            else
                return base.GetPermissionsCore();
        }
    }
}
