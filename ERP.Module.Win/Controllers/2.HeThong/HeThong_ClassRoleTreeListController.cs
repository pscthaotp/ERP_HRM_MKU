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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.HeThong;
using ERP.Module.Extends;
using ERP.Module.Commons;
using DevExpress.Xpo.DB;

namespace ERP.Module.Win.Controllers.HeThong
{
    public partial class HeThong_ClassRoleTreeListController : ViewController<DetailView>
    {
        private TreeList _treeList;
        private SecuritySystemRole_Class _phanQuyenLop;
        private string[] _lopDuocPhanQuyenList;

        public HeThong_ClassRoleTreeListController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        #region Add node Deparment to TreeList
        private void HeThong_ClassRoleTreeListController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;

            if (view != null && view.Id.Equals("SecuritySystemRole_Class_DetailView"))
            {
                _phanQuyenLop = view.CurrentObject as SecuritySystemRole_Class;
                //
                ControlViewItem itemRole = ((DetailView)View).FindItem("CustomControl") as ControlViewItem;
                //
                if (itemRole != null)
                {
                    _treeList = itemRole.Control as TreeList;

                    if (_treeList != null)
                    {
                        //Tên bộ phận
                        TreeListColumn colTenLop = new TreeListColumn();
                        colTenLop.Caption = "Tên lớp";
                        colTenLop.Visible = true;
                        colTenLop.VisibleIndex = 0;
                        colTenLop.OptionsColumn.AllowEdit = false;

                        TreeListColumn colTenHDT = new TreeListColumn();
                        colTenHDT.Caption = "Hệ đào tạo";
                        colTenHDT.Visible = true;
                        colTenHDT.VisibleIndex = 0;
                        colTenHDT.OptionsColumn.AllowEdit = false;

                        //Mã quản lý
                        TreeListColumn colMaQuanLy = new TreeListColumn();
                        colMaQuanLy.Caption = "Mã lớp";
                        colMaQuanLy.Visible = true;
                        colMaQuanLy.VisibleIndex = 1;
                        colMaQuanLy.Width = 50;
                        colMaQuanLy.OptionsColumn.AllowEdit = false;
                        //
                        _treeList.Columns.AddRange(new TreeListColumn[] { colTenLop, colTenHDT, colMaQuanLy });

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

        private void AddDataToNode()
        {
            //_lopList = new XPCollection<Lop>(((XPObjectSpace)View.ObjectSpace).Session, CriteriaOperator.Parse("NgungHoatDong=?", false));
            ////
            //AddNodeOfTreeList();
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

        void _treeList_StartSorting(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //_treeList.BeginSort();
            //_treeList.Columns["HeDaoTao"].SortOrder = SortOrder.Ascending;
            //_treeList.Columns["TenLop"].SortOrder = SortOrder.Descending;
            //_treeList.EndSort();
            _treeList.Columns["HeDaoTao"].SortOrder = SortOrder.Ascending;
            _treeList.Columns["TenLop"].SortOrder = SortOrder.Ascending;

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
