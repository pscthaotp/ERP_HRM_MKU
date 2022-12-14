using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using ERP.Module.HeThong;
//
namespace ERP.Module.Controller.HeThong
{
    public partial class HeThong_PhanQuyenCongTyController : ViewController<ListView>
    {
        public HeThong_PhanQuyenCongTyController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(ICongTy);
        }

        private void HeThong_PhanQuyenCongTyController_Activated(object sender, EventArgs e)
        {
            ListView listView = View as ListView;
            if (listView != null 
                && !listView.Id.Contains("BoPhan")
                && !listView.Id.Contains("SecuritySystemUser_Custom"))
            {
                InOperator criteria = new InOperator();
                List<string> bpList = Common.Department_GetRoledDepartmentList_BySession(((XPObjectSpace)View.ObjectSpace).Session);
                foreach (string item in bpList)
                {
                    // Có thể lấy dư của bộ phận nhưng không sao miễn có trường là được
                    criteria.Operands.Add(new OperandValue(item.ToString().ToLower()));
                }

                //Nếu là công ty thì công ty cao nhất
                GroupOperator go = new GroupOperator(GroupOperatorType.And);
                
                if (listView.ObjectTypeInfo.Type == typeof(CongTy))
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("LoaiBoPhan=0");
                    criteria.LeftOperand = new OperandProperty("Oid");
                    //
                    go.Operands.Add(criteria);
                    go.Operands.Add(filter);
                }
                else if (listView.ObjectTypeInfo.Implements<ICongTy>())
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("CongTy.LoaiBoPhan=0");
                    criteria.LeftOperand = new OperandProperty("CongTy.Oid");
                    //
                    go.Operands.Add(filter);
                    go.Operands.Add(criteria);
                }
                //
                if (go.Operands.Count > 0)
                    listView.CollectionSource.Criteria["PhanQuyenCongTy"] = go;
            }
        }
    }
}
