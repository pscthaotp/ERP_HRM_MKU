﻿using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;

namespace ERP.Module.Win.NormalForm.NhanSu
{
    public partial class frmChonCanBo : XtraForm
    {
        private XPCollection<BoPhan> _boPhanDuocPhanQuyenList;
        private XPCollection<BoPhan> _findedDepartmentList;
        private XPCollection<ThongTinNhanVien> _nhanVienList;
        private XPCollection<ChiTietTimKiemNhanVien> _trichDanhSachNhanVienList;
        private ThongTinNhanVien _thongTinNhanVien;
        private Session _session;
        //
        public frmChonCanBo()
        {
            InitializeComponent();
        }

        public frmChonCanBo(Session session)
        {
            InitializeComponent();
            this._session = session;
        }

        private void frmChonCanBo_Load(object sender, EventArgs e)
        {
            if (!Common.QuanTriToanHeThong())
            {
                _boPhanDuocPhanQuyenList = new XPCollection<BoPhan>(_session, new InOperator("Oid", Common.Department_GetRoledDepartmentList_BySession(_session)));
            }
            else
            {
                _boPhanDuocPhanQuyenList = new XPCollection<BoPhan>(_session);
            }

            //
            AddDataNodeOfTreeList(Common.CongTy(_session).Oid);
        }

        private void donViTreeList_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
        }

        private void donViTreeList_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            SetCheckedChildNodes(e.Node, e.Node.CheckState);
            SetCheckedParentNodes(e.Node, e.Node.CheckState);

            //filter nhan vien list
            GetDepartmentList();

            //clear nhan vien treelist
            nhanVienTreeList.Nodes.Clear();

            //add nv to nvtreelist
            AddDonVi(null, _findedDepartmentList);
            nhanVienTreeList.ExpandAll();
        }

        private void nhanVienTreeList_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            if (e.Node.Tag is BoPhan)
            {
                e.Node.Nodes.Clear();
                XPCollection<ThongTinNhanVien> nhanVienList = new XPCollection<ThongTinNhanVien>(_session, CriteriaOperator.Parse("BoPhan=?", ((BoPhan)e.Node.Tag).Oid));
                AddNhanVien(e.Node, nhanVienList);
            }
        }

        private void nhanVienTreeList_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
        }

        private void nhanVienTreeList_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            SetCheckedChildNodes(e.Node, e.Node.CheckState);
            SetCheckedParentNodes(e.Node, e.Node.CheckState);
        }

        private void AddDonVi(TreeListNode parent, XPCollection<BoPhan> bpList)
        {
            nhanVienTreeList.BeginUnboundLoad();
            //
            foreach (BoPhan item in bpList)
            {
                    TreeListNode node = nhanVienTreeList.AppendNode(new object[] { item.TenBoPhan }, parent);
                    node.Tag = item;
                    node.HasChildren = true;
            }
            //
            nhanVienTreeList.EndUnboundLoad();
        }

        private void AddNhanVien(TreeListNode parent, XPCollection<ThongTinNhanVien> nvList)
        {
            nhanVienTreeList.BeginUnboundLoad();
            //
            foreach (ThongTinNhanVien item in nvList)
            {
                if (item.TinhTrang != null && !item.TinhTrang.DaNghiViec)
                {
                    TreeListNode node = nhanVienTreeList.AppendNode(new object[] { item.HoTen }, parent);
                    node.Tag = item;
                }
            }
            //
            nhanVienTreeList.EndUnboundLoad();
        }

        private void AddDataNodeOfTreeList(Guid idRoot)
        {
            donViTreeList.BeginUnboundLoad();
            //
            TreeListNode rootNode = null;

            //Lấy bộ phận root
            BoPhan boPhanRoot = _session.FindObject<BoPhan>(CriteriaOperator.Parse("Oid=?", idRoot));
            //
            if (boPhanRoot != null)
            {
                rootNode = donViTreeList.AppendNode(new object[] { boPhanRoot.TenBoPhan, boPhanRoot.MaQuanLy }, null);
                rootNode.Tag = boPhanRoot;

                //Thêm các bộ phận con vào cây
                AddChildNode(rootNode, boPhanRoot, boPhanRoot.ListBoPhanCon);
            }
            //
            donViTreeList.EndUnboundLoad();
            //
            donViTreeList.ExpandAll();
        }

        private void AddChildNode(TreeListNode rootNode, BoPhan boPhanCha, XPCollection<BoPhan> boPhanConList)
        {
            foreach (BoPhan item in boPhanConList)
            {
                if (item.BoPhanCha == boPhanCha && !item.NgungHoatDong)
                {  
                    //Duyệt qua các bộ phận được phân quyền
                    foreach (BoPhan bp in _boPhanDuocPhanQuyenList)
                    {
                        if (item.Oid == bp.Oid)
                        {
                            TreeListNode node = donViTreeList.AppendNode(new object[] { item.TenBoPhan, item.MaQuanLy }, rootNode);
                            //Gọi đệ qui
                            AddChildNode(node, item, item.ListBoPhanCon);
                            //
                            node.Tag = item;
                        }
                    }
                }
            }
        }

        private static void SetCheckedChildNodes(TreeListNode node, CheckState check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState = check;
                SetCheckedChildNodes(node.Nodes[i], check);
            }
        }

        private static void SetCheckedParentNodes(TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                bool b = false;
                CheckState state;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    state = (CheckState)node.ParentNode.Nodes[i].CheckState;
                    if (!check.Equals(state))
                    {
                        b = !b;
                        break;
                    }
                }
                node.ParentNode.CheckState = b ? CheckState.Indeterminate : check;
                SetCheckedParentNodes(node.ParentNode, check);
            }
        }

        private void GetDepartmentList()
        {
            _findedDepartmentList = new XPCollection<BoPhan>(_session, false);
            //
            GetChildDepartmentList(donViTreeList.Nodes);
        }

        private void GetChildDepartmentList(TreeListNodes nodes)
        {
            foreach (TreeListNode node in nodes)
            {
                if (node.Checked)
                {
                    BoPhan boPhan = (BoPhan)node.Tag;
                    //
                    _findedDepartmentList.Add(boPhan);
                }
                else if (node.CheckState == CheckState.Unchecked)
                    continue;

                if (node.Nodes.Count > 0)
                    GetChildDepartmentList(node.Nodes);
            }
        }

        public XPCollection<ChiTietTimKiemNhanVien> GetStaffList()
        {
            _trichDanhSachNhanVienList = new XPCollection<ChiTietTimKiemNhanVien>(_session, false);
            //
            FindStaffByDepartmentNodes(nhanVienTreeList.Nodes);
            //
            return _trichDanhSachNhanVienList;
        }

        private void FindStaffByDepartmentNodes(TreeListNodes nodes)
        {
            foreach (TreeListNode node in nodes)
            {
                if (node.Tag is ThongTinNhanVien && node.CheckState == CheckState.Checked)
                {
                    _thongTinNhanVien = _session.GetObjectByKey<ThongTinNhanVien>(((ThongTinNhanVien)node.Tag).Oid);
                    if (_thongTinNhanVien != null)
                    {
                        ChiTietTimKiemNhanVien chiTiet = new ChiTietTimKiemNhanVien(_session);
                        chiTiet.NhanVien = _thongTinNhanVien;
                        _trichDanhSachNhanVienList.Add(chiTiet);
                    }
                }
                else if (node.CheckState == CheckState.Unchecked && node.Tag is ThongTinNhanVien)
                {
                    continue;
                }

                FindStaffByDepartmentNodes(node.Nodes);
            }
        }


        private void miCheckAll_Click(object sender, EventArgs e)
        {
            NodeState1(nhanVienTreeList.Nodes, CheckState.Checked);
        }

        private void miCheckSelected_Click(object sender, EventArgs e)
        {
            NodeState(nhanVienTreeList.Nodes, CheckState.Checked);
        }

        private void miUncheckAll_Click(object sender, EventArgs e)
        {
            NodeState1(nhanVienTreeList.Nodes, CheckState.Unchecked);
        }

        private void miUncheckSelected_Click(object sender, EventArgs e)
        {
            NodeState(nhanVienTreeList.Nodes, CheckState.Unchecked);
        }

        private static void NodeState(TreeListNodes nodes, CheckState state)
        {
            foreach (TreeListNode node in nodes)
            {
                if (node.Selected)
                {
                    node.CheckState = state;
                }
                if (node.Nodes.Count > 0)
                    NodeState(node.Nodes, state);
            }
        }

        private static void NodeState1(TreeListNodes nodes, CheckState state)
        {
            foreach (TreeListNode node in nodes)
            {
                node.CheckState = state;
                if (node.Nodes.Count > 0)
                    NodeState1(node.Nodes, state);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}