using System;
using DevExpress.ExpressApp;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Columns;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.HeThong;
using ERP.Module.Extends;
using ERP.Module.Commons;

namespace ERP.Module.Win.Controllers.HeThong
{
    public partial class HeThong_DepartmentRoleTreeListController : ViewController<DetailView>
    {
        private TreeList _treeList;
        private XPCollection<BoPhan> _boPhanList;
        private XPCollection<BoPhan> _searchBoPhanList;
        private SecuritySystemRole_Department _phanQuyenDonVi;
        private string[] _bpDuocPhanQuyenList;
        TextBox _search;

        public HeThong_DepartmentRoleTreeListController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        #region Add node Deparment to TreeList
        private void HeThong_DepartmentRoleTreeListController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;

            if (view != null && view.Id.Equals("SecuritySystemRole_Department_DetailView"))
            {
                _phanQuyenDonVi = view.CurrentObject as SecuritySystemRole_Department;
                //
                ControlViewItem itemRole = ((DetailView)View).FindItem("CustomControl") as ControlViewItem;
                //
                if (itemRole != null)
                {
                    _treeList = itemRole.Control as TreeList;

                    if (_treeList != null)
                    {
                        //Tên bộ phận
                        TreeListColumn colTenBoPhan = new TreeListColumn();
                        colTenBoPhan.Caption = "Tên bộ phận";
                        colTenBoPhan.Visible = true;
                        colTenBoPhan.VisibleIndex = 0;
                        colTenBoPhan.OptionsColumn.AllowEdit = false;

                        //Mã quản lý
                        TreeListColumn colMaQuanLy = new TreeListColumn();
                        colMaQuanLy.Caption = "Mã quản lý";
                        colMaQuanLy.Visible = true;
                        colMaQuanLy.VisibleIndex = 1;
                        colMaQuanLy.Width = 50;
                        colMaQuanLy.OptionsColumn.AllowEdit = false;
                        //
                        _treeList.Columns.AddRange(new TreeListColumn[] { colTenBoPhan, colMaQuanLy });

                        //Set cấu hình cơ bản của cây
                        TreeUtil.InitTreeView(_treeList);
                        //Vì ở đây chỉ xài riêng cho view này
                        _treeList.OptionsView.ShowCheckBoxes = true;

                        //Các sự kiện của cây
                        _treeList.BeforeCheckNode += TreeList_BeforeCheckNode;
                        _treeList.AfterCheckNode += TreeList_AfterCheckNode;
                        _treeList.NodeCellStyle += TreeList_NodeCellStyle;

                        //Thêm dữ liệu vào node của cây
                        AddDataToNode();
                    }
                }

                //
                ControlViewItem itemFind = ((DetailView)View).FindItem("SearchTextBox") as ControlViewItem;
                if (itemFind!= null)
                {
                    _search = itemFind.Control as TextBox;
                    if (_search != null)
                    {
                        _search.KeyDown += KeyDown;
                    }
                }
            }
        }

        private void TreeList_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            //Tô màu tree cho dễ nhìn
            if (e.Node.Checked)
            {
                e.Appearance.BackColor = Color.WhiteSmoke;
                e.Appearance.ForeColor = Color.Brown;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _searchBoPhanList = BoPhanTimDuocList(_search.Text.Trim());
                //
                AddNodeOfTreeList(Commons.Common.CongTy(((XPObjectSpace)View.ObjectSpace).Session).Oid, true);
            }
        }
        private void AddDataToNode()
        {
            _boPhanList = new XPCollection<BoPhan>(((XPObjectSpace)View.ObjectSpace).Session, CriteriaOperator.Parse("NgungHoatDong=?", false));
            //
            AddNodeOfTreeList(Commons.Common.CongTy(((XPObjectSpace)View.ObjectSpace).Session).Oid, false);
        }

        private void TreeList_BeforeCheckNode(object sender, CheckNodeEventArgs e)
        {
            e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
        }

        private void TreeList_AfterCheckNode(object sender, NodeEventArgs e)
        {
            //
            SetCheckedNode(e.Node, e.Node.CheckState);
        }

        private void AddNodeOfTreeList(Guid idRoot, bool search)
        {
            //Danh sách phân quyền đơn vị
            if (_phanQuyenDonVi != null && !String.IsNullOrEmpty(_phanQuyenDonVi.Quyen))
            {
                _bpDuocPhanQuyenList = _phanQuyenDonVi.Quyen.Split(';');
            }

            //Xóa tất cả các node trên cây
            _treeList.Nodes.Clear();
            //
            _treeList.BeginUnboundLoad();
            //
            TreeListNode rootNode = null;

            //Lấy bộ phận root
            BoPhan boPhanRoot = (((XPObjectSpace)View.ObjectSpace).Session).FindObject<BoPhan>(CriteriaOperator.Parse("Oid=?", idRoot));

            if (boPhanRoot != null)
            {
                rootNode = _treeList.AppendNode(new object[] { boPhanRoot.TenBoPhan, boPhanRoot.MaQuanLy }, null);
                //Check vào các node đã phân quyền
                if (_bpDuocPhanQuyenList != null)
                {
                    foreach (string bp in _bpDuocPhanQuyenList)
                    {
                        if (boPhanRoot.Oid.ToString().Equals(bp))
                            rootNode.CheckState = CheckState.Checked;
                    }
                }
                //
                if (!search)
                    AddChildNode(rootNode, boPhanRoot, boPhanRoot.ListBoPhanCon, false);
                else
                    AddChildNode(rootNode, boPhanRoot, _searchBoPhanList, true);
                //
                rootNode.Tag = boPhanRoot;
            }
            //
            _treeList.EndUnboundLoad();
            _treeList.Refresh();

            // Bung tất cả các node ra
            _treeList.ExpandAll();
        }

        private void AddChildNode(TreeListNode rootNode, BoPhan boPhanCha, XPCollection<BoPhan> boPhanConList, bool search)
        {
            if (boPhanConList == null) return;
            //
            foreach (BoPhan item in boPhanConList)
            {
                if (item.BoPhanCha == boPhanCha && !item.NgungHoatDong)
                {
                    TreeListNode node = _treeList.AppendNode(new object[] { item.TenBoPhan, item.MaQuanLy }, rootNode);
                    //Check vào các node đã phân quyền
                    if (_bpDuocPhanQuyenList != null)
                    {
                        foreach (string bp in _bpDuocPhanQuyenList)
                        {
                            if (item.Oid.ToString().Equals(bp))
                                node.CheckState = CheckState.Checked;
                        }
                    }
                    //
                    if (!search)
                        AddChildNode(node, item, item.ListBoPhanCon, false);
                    else
                        AddChildNode(node, item, _searchBoPhanList, true);
                    //
                    node.Tag = item;
                }
            }
        }

        private void SetCheckedNode(TreeListNode node, CheckState check)
        {
            //Checked cả các node cha
            /*if (node.ParentNode != null && check == CheckState.Checked)
                CheckParentNode(node, check);*/

            //Checked or UnChecked tất các các node con
            CheckChildNode(node, check);
        }

        private void CheckParentNode(TreeListNode node, CheckState check)
        {
            node.ParentNode.CheckState = check;
            //Check vào node root
            foreach (TreeListNode item in _treeList.Nodes)
            {
                if (item.ParentNode == null)
                    item.CheckState = CheckState.Checked;
            }
        }

        private static void CheckChildNode(TreeListNode node, CheckState check)
        {

            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState = check;
                CheckChildNode(node.Nodes[i], check);
            }
        }
        private XPCollection<BoPhan> BoPhanTimDuocList(string tenBoPhan)
        {
            XPCollection<BoPhan> boPhanList = null;
            //
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@TenBoPhan", tenBoPhan);
            param[1] = new SqlParameter("@Quyen", Common.System_GetDeparment_Role_ByUser());

            List<Guid> oidDepartmentList = DataProvider.GetGuidList("spd_HeThong_TimBoPhanTheoTen", CommandType.StoredProcedure, param);
            //
            if (oidDepartmentList != null)
            {
                boPhanList = new XPCollection<BoPhan>((((XPObjectSpace)View.ObjectSpace).Session), new InOperator("Oid", oidDepartmentList));
            }
            //
            return boPhanList;
        }
        #endregion

        #region Save SecuritySystemRole_Deparment
        protected override void OnActivated()
        {
            base.OnActivated();
            ObjectSpace.Committing += ObjectSpace_Committing;
        }

        protected override void OnDeactivated()
        {
            ObjectSpace.Committing -= ObjectSpace_Committing;
            base.OnDeactivated();
        }

        void ObjectSpace_Committing(object sender, CancelEventArgs e)
        {
            Process();
        }

        private void Process()
        {
            if (_treeList != null)
            {
                //Mẫn chỉnh sửa ngày 06/03/2018
                //foreach (TreeListNode node in _treeList.Nodes)
                //{
                //    GetChildNode(node);
                //}

                _phanQuyenDonVi.Quyen = "";
                foreach (TreeListNode node in _treeList.GetAllCheckedNodes())
                {
                    _phanQuyenDonVi.Quyen += ((BoPhan)node.Tag).Oid.ToString() + ";";
                }
            }
        }
        private void GetChildNode(TreeListNode node)
        {
            if (node.Checked)
            {
                if (!SearchQuyen(_phanQuyenDonVi.Quyen, ((BoPhan)node.Tag).Oid.ToString()))
                    _phanQuyenDonVi.Quyen += ((BoPhan)node.Tag).Oid.ToString() + ";";
            }
            if (!node.Checked)
            {
                if (SearchQuyen(_phanQuyenDonVi.Quyen, ((BoPhan)node.Tag).Oid.ToString()))
                    _phanQuyenDonVi.Quyen = _phanQuyenDonVi.Quyen.Replace(((BoPhan)node.Tag).Oid.ToString() + ";", String.Empty);
            }

            if (node.HasChildren)
            {
                foreach (TreeListNode item in node.Nodes)
                    GetChildNode(item);
            }
        }
        private bool SearchQuyen(string quyen, string idBoPhan)
        {
            if (!string.IsNullOrEmpty(quyen))
            {
                string[] quyenList = quyen.Split(";".ToCharArray());
                for (int i = 0; i < quyenList.Length; i++)
                    if (quyenList[i] == idBoPhan)
                    {
                        return true;
                    }
            }
            return false;
        }
        #endregion
    }
}
