using System;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using DevExpress.XtraTreeList.Columns;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraTreeList.Nodes;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.ComponentModel;
using ERP.Module.BaoCao.Custom;
using ERP.Module.HeThong;
using ERP.Module.Extends;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;

namespace ERP.Module.Win.Controllers.HeThong
{
    public partial class HeThong_ReportRoleTreeListController : ViewController<DetailView>
    {
        TreeList _treeList;
        XPCollection<GroupReport> _groupReportList;
        SecuritySystemRole_Report _phanQuyenBaoCao;
        string[] _baoCaoDaPhanQuyenList;
        bool _checkGroup = false;
        TextBox _searchTextBox;
        bool _search = false;

        public HeThong_ReportRoleTreeListController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        #region Add node to TreeList
        private void HeThong_ReportRoleTreeListController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;

            if (view != null && view.Id.Equals("SecuritySystemRole_Report_DetailView"))
            {
                _phanQuyenBaoCao = view.CurrentObject as SecuritySystemRole_Report;
                //
                ControlViewItem item = ((DetailView)View).FindItem("CustomControl") as ControlViewItem;
                //
                if (item != null)
                {
                    _treeList = item.Control as TreeList;

                    if (_treeList != null)
                    {
                        TreeListColumn colTenBaoCao = new TreeListColumn();
                        colTenBaoCao.Caption = "Tên báo cáo";
                        colTenBaoCao.Visible = true;
                        colTenBaoCao.VisibleIndex = 0;
                        colTenBaoCao.OptionsColumn.AllowEdit = false;
                        //
                        _treeList.Columns.AddRange(new TreeListColumn[] { colTenBaoCao });

                        //Set cấu hình cơ bản của cây
                        TreeUtil.InitTreeView(_treeList);
                        //Vì ở đây chỉ xài riêng cho view này
                        _treeList.OptionsView.ShowCheckBoxes = true;

                        //Các sự kiện của cây
                        _treeList.BeforeCheckNode += TreeList_BeforeCheckNode;
                        _treeList.AfterCheckNode += TreeList_AfterCheckNode;
                        _treeList.NodeCellStyle += TreeList_NodeCellStyle;

                        //Thêm dữ liệu vào từng node của cây
                        AddDataToNode();
                    }
                }

                //
                ControlViewItem itemFind = ((DetailView)View).FindItem("SearchTextBox") as ControlViewItem;
                if (itemFind != null)
                {
                    _searchTextBox = itemFind.Control as TextBox;
                    if (_search != null)
                    {
                        _searchTextBox.KeyDown += KeyDown;
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
                //
                _search = true;
                //
                AddDataToNode();
            }
        }

        private void AddDataToNode()
        {
            //Lấy danh sách các nhóm báo cáo
            _groupReportList = new XPCollection<GroupReport>(((XPObjectSpace)View.ObjectSpace).Session);
            //
            AddNodeOfTreeList();
        }

        private void AddNodeOfTreeList()
        {
            //Danh sach phân quyền
            if (_phanQuyenBaoCao != null && !String.IsNullOrEmpty(_phanQuyenBaoCao.Quyen))
            {
                _baoCaoDaPhanQuyenList = _phanQuyenBaoCao.Quyen.Split(';');
            }

            //Xóa tất cả các node có sẵn
            _treeList.Nodes.Clear();
            //
            _treeList.BeginUnboundLoad();

            //Danh sách công ty
            XPCollection<CongTy> congTyList = new XPCollection<CongTy>(((XPObjectSpace)View.ObjectSpace).Session);
            CriteriaOperator filter = CriteriaOperator.Parse("Oid=?", Config.KeyTTCEdu);
            congTyList.Criteria = filter;
            //
            foreach (var itemCongTy in congTyList)
            {
                //Khởi tạo node root
                TreeListNode rootNode = _treeList.AppendNode(new object[] { itemCongTy.TenBoPhan }, null);

                foreach (GroupReport item in _groupReportList)
                {
                    _checkGroup = false;
                    XPCollection<ReportData_Custom> reportList = null;

                    //Danh sách báo cáo theo nhóm
                    if (!_search || string.IsNullOrEmpty(_searchTextBox.Text.Trim()))
                    {
                        reportList = new XPCollection<ReportData_Custom>(((XPObjectSpace)View.ObjectSpace).Session, CriteriaOperator.Parse("GroupReport.Oid=? and CongTy.Oid=?", item.Oid, itemCongTy.Oid));
                    }
                    else
                    {
                        string bien = "%" + _searchTextBox.Text.Trim() + "%";

                        CriteriaOperator criteria = CriteriaOperator.Parse("GroupReport.Oid =? And Name like ?  and CongTy.Oid=? ", item.Oid, bien, itemCongTy.Oid);
                        reportList = new XPCollection<ReportData_Custom>(((XPObjectSpace)View.ObjectSpace).Session, criteria);
                    }

                    if (reportList != null && reportList.Count > 0)
                    {
                        TreeListNode node = _treeList.AppendNode(new object[] { item.TenNhom }, rootNode);

                        //Thêm báo cáo vào cây
                        AddChildNode(node, reportList);

                        //Check vào group
                        if (_checkGroup)
                        {
                            node.CheckState = CheckState.Checked;
                        }

                        //
                        node.Tag = item;
                    }
                }
            }

            _treeList.EndUnboundLoad();

            //Bung tất cả các node ra
            _treeList.ExpandAll();
        }

        private void AddChildNode(TreeListNode rootNode, XPCollection<ReportData_Custom> reportList)
        {
            foreach (ReportData_Custom item in reportList)
            {
                TreeListNode node = _treeList.AppendNode(new object[] { item.ReportName }, rootNode);

                //Check vào các node đã phân quyền
                if (_baoCaoDaPhanQuyenList != null)
                {
                    foreach (string bp in _baoCaoDaPhanQuyenList)
                    {
                        if (item.Oid.ToString() == bp)
                        {
                            node.CheckState = CheckState.Checked;
                            //
                            _checkGroup = true;
                        }
                    }
                }
                //
                node.Tag = item;
            }
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

        private void SetCheckedNode(TreeListNode node, CheckState check)
        {
            /*
            //Checked cả các node cha
            if (node.ParentNode != null && check == CheckState.Checked)
                CheckParentNode(node, check); */

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

        #region Save SecuritySystemRole_Report
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

                _phanQuyenBaoCao.Quyen = "";
                foreach (TreeListNode node in _treeList.GetAllCheckedNodes())
                {
                    if(node.Tag is ReportData_Custom)
                        _phanQuyenBaoCao.Quyen += ((ReportData_Custom)node.Tag).Oid.ToString() + ";";
                }
            }
        }

        private void GetChildNode(TreeListNode node)
        {
            if (node.Checked && node.Tag is ReportData_Custom)
            {
                if (!SearchQuyen(_phanQuyenBaoCao.Quyen, ((ReportData_Custom)node.Tag).Oid.ToString()))
                    _phanQuyenBaoCao.Quyen += ((ReportData_Custom)node.Tag).Oid.ToString() + ";";
            }
            if (!node.Checked && node.Tag is ReportData_Custom)
            {
                if (SearchQuyen(_phanQuyenBaoCao.Quyen, ((ReportData_Custom)node.Tag).Oid.ToString()))
                    _phanQuyenBaoCao.Quyen = _phanQuyenBaoCao.Quyen.Replace(((ReportData_Custom)node.Tag).Oid.ToString() + ";", String.Empty);
            }

            if (node.HasChildren)
            {
                foreach (TreeListNode item in node.Nodes)
                    GetChildNode(item);
            }
        }

        private bool SearchQuyen(string quyen, string idReport)
        {
            if (!string.IsNullOrEmpty(quyen))
            {
                string[] quyenList = quyen.Split(";".ToCharArray());
                for (int i = 0; i < quyenList.Length; i++)
                    if (quyenList[i] == idReport)
                    {
                        return true;
                    }
            }
            return false;
        }
        #endregion
    }
}
