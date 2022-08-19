using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERP.Module.Web.Editors.Other;
using ERP.Module.HeThong;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Web.Controls
{
    public partial class UC_PhanQuyenDonVi : System.Web.UI.UserControl, IXpoSessionAwareControl
    {
        IObjectSpace _obs = null;
        XafApplication _application = WebApplication.Instance;
        string _currentQuyen = string.Empty;
        SecuritySystemRole_Department _currentRole;
        //       

        #region Load
        public void UpdateDataSource(DevExpress.Xpo.Session session)
        {

        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            //Sau khi load form và chỉ gọi 1 lần
            CheckNodeOfTreeList();
        }
        protected override void OnInit(EventArgs e)
        {
            //
            _obs = _application.CreateObjectSpace();
            //
            _currentRole = _obs.GetObjectByKey<SecuritySystemRole_Department>(Common.OidCustomList[0]);
            if (_currentRole != null)
            {
                txtTen.Value = _currentRole.Ten;
                //
                if (_currentRole.Quyen != null)
                    _currentQuyen = _currentRole.Quyen.Replace("-", "");
                //
                if (Common.QuanTriToanCongTy())
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("CongTy=?", Common.SecuritySystemUser_GetCurrentUser().CongTy.Oid);
                    XPCollection<BoPhan> boPhanList = new XPCollection<BoPhan>(((XPObjectSpace)_obs).Session, filter);
                    if (boPhanList != null && boPhanList.Count > 0)
                    {
                        BoPhan_TreeList.DataSource = boPhanList;
                        BoPhan_TreeList.DataBind();
                    }
                }
                if (Common.QuanTriToanHeThong())
                {
                    XPCollection<BoPhan> boPhanList = new XPCollection<BoPhan>(((XPObjectSpace)_obs).Session);
                    if (boPhanList != null && boPhanList.Count > 0)
                    {
                        BoPhan_TreeList.DataSource = boPhanList;
                        BoPhan_TreeList.DataBind();
                    }
                }
            }
        }

        #endregion

        #region Event

        private void CheckNodeOfTreeList()
        {
            if (string.IsNullOrEmpty(_currentQuyen)) return;
            //
            //Check node
            foreach (TreeListNode node in BoPhan_TreeList.Nodes)
            {
                //
                if (node.ChildNodes.Count > 0)
                {
                    //Check node con
                    SetAllNodeTreeList(node);
                }
                //Checked
                if (_currentQuyen.ToUpper().Contains(node.Key.ToString().ToUpper()))
                {
                    node.Selected = true;
                }
            }
            //
        }

        void SetAllNodeTreeList(TreeListNode node)
        {
            foreach (TreeListNode nodeChild in node.ChildNodes)
            {
                if (nodeChild.ChildNodes.Count > 0)
                {
                    // Đệ qui
                    SetAllNodeTreeList(nodeChild);
                }
                //Checked
                if (_currentQuyen.ToUpper().Contains(nodeChild.Key.ToString().ToUpper()))
                {
                    nodeChild.Selected = true;
                }
            }
        }

        protected void btnLuu_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTen.Value.ToString()))
            {
                _currentRole.Ten = txtTen.Value.ToString();
            }
            //Cập nhật quyền
            foreach (TreeListNode node in BoPhan_TreeList.Nodes)
            {
                //Thêm node
                BoPhan bp = node.DataItem as BoPhan;
                if (bp != null)
                {
                    //
                    if (node.Selected)
                    {
                        if (_currentRole.Quyen == null || (!SearchQuyen(_currentRole.Quyen.ToUpper(), bp.Oid.ToString().ToUpper())))
                        {
                            _currentRole.Quyen += bp.Oid.ToString().ToUpper() + ";";
                        }
                    }
                }
                //
                if (node.ChildNodes.Count > 0)
                {
                    GetChildNode(node);
                }

            }
            //Lưu dữ liệu
            _obs.CommitChanges();

            //
            WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Lưu thành công !!!')");
            //
            OnInit(e);
        }

        private void GetChildNode(TreeListNode node)
        {
            foreach (TreeListNode nodeChild in node.ChildNodes)
            {
                BoPhan bp = nodeChild.DataItem as BoPhan;
                if (bp != null)
                {
                    //
                    if (nodeChild.Selected)
                    {
                        if (_currentRole.Quyen == null || (!SearchQuyen(_currentRole.Quyen.ToUpper(), bp.Oid.ToString().ToUpper())))
                        {
                            _currentRole.Quyen += bp.Oid.ToString().ToUpper() + ";";
                        }
                    }
                    else
                    {
                        if (_currentRole.Quyen != null && SearchQuyen(_currentRole.Quyen.ToUpper(), bp.Oid.ToString().ToUpper()))
                        {
                            _currentRole.Quyen = _currentRole.Quyen.ToUpper().Replace(bp.Oid.ToString().ToUpper() + ";", String.Empty);
                        }
                    }
                }

                //Đệ qui
                GetChildNode(nodeChild);
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