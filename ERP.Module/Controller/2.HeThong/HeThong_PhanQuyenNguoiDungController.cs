using System;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using System.Data.SqlClient;
using ERP.Module.HeThong;
using ERP.Module.Commons;
using ERP.Module.CauHinhChungs;
//
namespace ERP.Module.Controller.HeThong
{
    public partial class HeThong_PhanQuyenNguoiDungController : ViewController<ListView>
    {
        public HeThong_PhanQuyenNguoiDungController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewType = ViewType.ListView;
        }

        private void HeThong_PhanQuyenNguoiDungController_Activated(object sender, EventArgs e)
        {
            ListView listView = View as ListView;
            //
            if (listView != null
                && !listView.Id.Equals("RuleSetValidationResultItem_ByTarget_ListView") // Lỗi
                && !listView.Id.Equals("ContextValidationResult_ListView")) // Lỗi
            {
                SecuritySystemUser_Custom currentUser = Common.SecuritySystemUser_GetCurrentUser();
                if (currentUser == null) return;
                CauHinhChung cauHinhChung = Common.CauHinhChung_GetCauHinhChung;

                //Nếu là tài khoản sử dụng thì lúc nào cũng phân quyền data
                //Nếu các bảng khác thì tùy vào cấu hình
                if (cauHinhChung != null && (cauHinhChung.CauHinhXacThuc != null && cauHinhChung.CauHinhXacThuc.PhanQuyenTaiKhoan)
                                             || listView.Id.Equals("SecuritySystemUser_Custom_ListView")
                                             || listView.Id.Equals("PhanQuyenDuyetCTK_ChiTietPhanQuyenDuyetCTK_ListView")
                                             || listView.Id.Equals("SecuritySystemUser_Custom_LookupListView"))
                {
                    GroupOperator go = new GroupOperator(GroupOperatorType.And);

                    if (listView.Id.Equals("SecuritySystemUser_Custom_ListView")
                        || listView.Id.Equals("SecuritySystemUser_Custom_LookupListView"))
                    {
                        //Ẩn tài khoản PSC đi
                        CriteriaOperator filter = CriteriaOperator.Parse("UserName not like ?", "PSC");
                        //go.Operands.Add(filter);
                    }

                    #region 1.Tài khoản bình Thường của Trường
                    if (Common.TaiKhoanBinhThuong_NotEdu())
                    {
                        if (listView.ObjectTypeInfo.Implements<ISecuritySystemUser_Custom>())
                        {
                            CriteriaOperator criteria = CriteriaOperator.Parse("SecuritySystemUser.Oid=?", currentUser.Oid);
                            //
                            go.Operands.Add(criteria);
                        }

                        if (listView.ObjectTypeInfo.Type == typeof(SecuritySystemUser_Custom))
                        {
                            CriteriaOperator criteria = CriteriaOperator.Parse("Oid=?", currentUser.Oid);
                            //
                            go.Operands.Add(criteria);
                        }
                    }
                    #endregion

                    #region 2.Tài khoản bình Thường của Edu
                    if (Common.TaiKhoanBinhThuong_Edu())
                    {
                        //
                        if (listView.ObjectTypeInfo.Type == typeof(SecuritySystemUser_Custom))
                        {
                            CriteriaOperator criteria1 = CriteriaOperator.Parse("Oid=?", currentUser.Oid);
                            //
                            go.Operands.Add(criteria1);
                        }
                        //
                        if (listView.ObjectTypeInfo.Implements<ISecuritySystemUser_Custom>())
                        {
                            InOperator criteria = new InOperator();
                            List<string> bpList = Common.Department_GetRoledDepartmentList_BySession(((XPObjectSpace)View.ObjectSpace).Session);
                            foreach (string item in bpList)
                            {
                                // Có thể lấy dư của bộ phận nhưng không sao miễn có trường là được
                                criteria.Operands.Add(new OperandValue(item.ToString().ToLower()));
                            }
                            criteria.LeftOperand = new OperandProperty("SecuritySystemUser.CongTy.Oid");
                            //
                            go.Operands.Add(criteria);
                        }
                    }
                    #endregion

                    #region 3.Tài khoản Quản trị công Ty
                    if (Common.QuanTriToanCongTy()
                        || Common.QuanTriKhoi())
                    {
                        //
                        GroupOperator go1 = new GroupOperator(GroupOperatorType.Or);
                        InOperator criteria = new InOperator();
                        InOperator criteria2 = new InOperator();
                        List<string> bpList = Common.Department_GetRoledDepartmentList_BySession(((XPObjectSpace)View.ObjectSpace).Session);
                        foreach (string item in bpList)
                        {
                            //Không lấy công ty là edu
                            //Lý do: khi phân quyền đơn vị tài khoản của edu chọn luôn key của edu ...(Liên hệ mẫn)
                            if (item != Config.KeyTTCEdu.ToString())
                            {
                                // Có thể lấy dư của bộ phận nhưng không sao miễn có trường là được
                                criteria.Operands.Add(new OperandValue(item.ToString().ToLower()));
                                criteria2.Operands.Add(new OperandValue(item.ToString().ToLower()));
                            }
                        }

                        //
                        if (listView.ObjectTypeInfo.Type == typeof(SecuritySystemUser_Custom))
                        {
                            CriteriaOperator criteria1 = CriteriaOperator.Parse("Oid=?", currentUser.Oid);
                            criteria.LeftOperand = new OperandProperty("CongTy.Oid");
                            criteria2.LeftOperand = new OperandProperty("BoPhan.Oid");
                            //
                            go1.Operands.Add(criteria1);
                            go1.Operands.Add(criteria);
                            go1.Operands.Add(criteria2);
                            //
                            go.Operands.Add(go1);
                        }
                        
                        //
                        if (listView.ObjectTypeInfo.Implements<ISecuritySystemUser_Custom>())
                        {
                            // Fix lỗi không ảnh hưởng đến phân quyền đã viết
                            if (listView.Id.Equals("TuVanTuyenSinh_ListChiTietTuVanTuyenSinh_ListView"))
                                criteria.LeftOperand = new OperandProperty("CongTy.Oid");
                            else
                                criteria.LeftOperand = new OperandProperty("SecuritySystemUser.CongTy.Oid");
                            //
                            go.Operands.Add(criteria);
                        }
                    }
                    #endregion
                    //
                    if (go.Operands.Count > 0)
                        listView.CollectionSource.Criteria["PhanQuyenNguoiDung"] = go;
                }
            }
        }
    }
}
