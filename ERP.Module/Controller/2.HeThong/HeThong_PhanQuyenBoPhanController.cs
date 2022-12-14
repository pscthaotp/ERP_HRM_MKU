using System;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using System.Data.SqlClient;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.HeThong;
using ERP.Module.Commons;
//
namespace ERP.Module.Controller.HeThong
{
    public partial class HeThong_PhanQuyenBoPhanController : ViewController<ListView>
    {
        public HeThong_PhanQuyenBoPhanController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetObjectType = typeof(IBoPhan);
        }

        private void HeThong_PhanQuyenBoPhanController_Activated(object sender, EventArgs e)
        {
            ListView listView = View as ListView;
            //
            if (listView != null 
                && !listView.Id.Contains("CongTy")
                && !listView.Id.Contains("SecuritySystemUser_Custom"))
            {
                //Nếu Quản trị Hệ thống thì không cần phân quyền
                //Nếu Quản trị Khối, Quản trị Trường và Bình thường thì phân quyền

                SecuritySystemUser_Custom currentUser = SecuritySystem.CurrentUser as SecuritySystemUser_Custom;
                if (currentUser == null) return;
                //
                if (currentUser.LoaiTaiKhoan != Enum.Systems.LoaiTaiKhoanEnum.QuanTriHeThong)
                {
                    InOperator criteria = new InOperator();
                    bool state = false;
                    //
                    List<string> bpList = Common.Department_GetRoledDepartmentList_BySession(((XPObjectSpace)View.ObjectSpace).Session);
                    foreach (string item in bpList)
                    {
                        criteria.Operands.Add(new OperandValue(item.ToString().ToLower()));
                    }

                    if (listView.ObjectTypeInfo.Type == typeof(BoPhan))
                    {
                        criteria.LeftOperand = new OperandProperty("Oid");
                        state = true;
                    }
                    else if (listView.ObjectTypeInfo.Implements<IBoPhan>())
                    {
                        criteria.LeftOperand = new OperandProperty("BoPhan.Oid");
                        state = true;
                    }
                    //
                    if (state)
                    {
                        listView.CollectionSource.Criteria["PhanQuyenBoPhan"] = criteria;
                    }
                }
            }
        }
    }
}
