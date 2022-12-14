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
namespace ERP.Module.Web.Controllers.NghiepVu.NhanSu
{
    public partial class NhanVien_PhanQuyenBoPhanController : ViewController
    {
        public NhanVien_PhanQuyenBoPhanController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(IBoPhan);
        }

        private void NhanVien_PhanQuyenBoPhanController_Activated(object sender, EventArgs e)
        {
            ListView listView = View as ListView;
            //
            if (listView != null && listView.Id.Equals("NhanVienDangLamViec_ListView"))
            {
                SecuritySystemUser_Custom currentUser = SecuritySystem.CurrentUser as SecuritySystemUser_Custom;
                InOperator criteria = new InOperator();
                bool state = false;
                //

                if (listView.Editor is CategorizedListEditor_NhanVien)
                {
                    #region Cây thông tin nhân viên
                    CategorizedListEditor_NhanVien editor = listView.Editor as CategorizedListEditor_NhanVien;
                    listView = editor.CategoriesListView;
                    if (listView != null)
                    {
                        if (currentUser != null
                            &&  !Common.QuanTriToanHeThong()
                            &&  currentUser.SecuritySystemRole_Department != null
                            &&  ( listView.ObjectTypeInfo.Type == typeof(BoPhan) ||
                                  listView.ObjectTypeInfo.Implements<IBoPhan>())
                           )
                        {
                            //
                            List<string> bpList = Common.Department_GetRoledDepartmentList_BySession(((XPObjectSpace)View.ObjectSpace).Session);
                            foreach (string item in bpList)
                            {
                                criteria.Operands.Add(new OperandValue(item));
                            }

                            if (listView.ObjectTypeInfo.Type == typeof(BoPhan))
                            {
                                criteria.LeftOperand = new OperandProperty("Oid");
                                state = true;
                            }
                            else if (listView.ObjectTypeInfo.Implements<BoPhan>() && listView.ObjectTypeInfo.Type != typeof(CongTy))
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
                    //
                #endregion
                }
            }
        }
    }
}
