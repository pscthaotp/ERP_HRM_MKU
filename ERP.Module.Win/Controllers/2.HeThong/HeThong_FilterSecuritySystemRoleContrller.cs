using System;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.HeThong;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.Security.Strategy.PermissionMatrix;
using System.Collections.Generic;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Win.Controllers.HeThong
{
    public partial class HeThong_FilterSecuritySystemRoleContrller : ViewController<ListView>
    {
        public HeThong_FilterSecuritySystemRoleContrller()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void HeThong_FilterSecuritySystemRoleContrller_Activated(object sender, EventArgs e)
        {
            ListView listView = View as ListView;

            //
            if (listView.Id.Equals("SecuritySystemTypePermissionsObjectOwner_TypePermissionMatrix_ListView"))
            {
                if (Common.QuanTriToanHeThong())
                    return;
                //
                Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                //
                SecuritySystemUser_Custom user = Common.SecuritySystemUser_GetCurrentUser();
                List<Guid> roleGroupList = new List<Guid>();
                foreach (var item in user.Roles)
                {
                    roleGroupList.Add(item.Oid);
                }
                //
                if (roleGroupList.Count > 0)
                {
                    GroupOperator go = new GroupOperator(GroupOperatorType.And);
                    InOperator filter1 = new InOperator("Owner", roleGroupList);
                    CriteriaOperator filter2 = CriteriaOperator.Parse("AllowNavigate");
                    //
                    go.Operands.Add(filter1);
                    go.Operands.Add(filter2);
                    //
                    XPCollection<SecuritySystemTypePermissionObject> functionList = new XPCollection<SecuritySystemTypePermissionObject>(session, go);
                    //
                    InOperator criteria = new InOperator();
                    foreach (var item in functionList)
                    {
                        if(item.TargetType != null)
                            criteria.Operands.Add(new OperandValue(item.TargetType.Name));
                    }
                    //
                    criteria.LeftOperand = new OperandProperty("TargetType.Name");
                    //
                    listView.CollectionSource.Criteria["PhanQuyenChucNang"] = criteria;
                }
            }
            //
            if (listView.Id.Equals("SecuritySystemRole_Custom_ListView")
                || listView.Id.Equals("SecuritySystemRole_Custom_ListView"))
            {
                if (Common.QuanTriToanHeThong())
                    return;
                //
                Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                CongTy congTy = Common.CongTy(session);
                //
                CriteriaOperator filter = "";
                filter = CriteriaOperator.Parse("Name not like ? and Name not like ? and Name not like ?", "%nhân sự%", "%tiền lương%", "%hệ thống%");
                //
                listView.CollectionSource.Criteria["PhanQuyenChucNang"] = filter;
            }
        }
    }
}
