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
using ERP.Module.BaoCao.Custom;

namespace ERP.Web.Controls
{
    public partial class UC_PhanQuyenBaoCao : System.Web.UI.UserControl, IXpoSessionAwareControl
    {
        IObjectSpace _obs = null;
        XafApplication _application = WebApplication.Instance;
        string _currentQuyen = string.Empty;
        SecuritySystemRole_Report _currentRole;

        #region Load
        // 
        public void UpdateDataSource(DevExpress.Xpo.Session session)
        {

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
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
            _currentRole = _obs.GetObjectByKey<SecuritySystemRole_Report>(Common.OidCustomList[0]);
            if (_currentRole != null)
            {
                txtTen.Value = _currentRole.Ten;
                //
                if (_currentRole.Quyen != null)
                    _currentQuyen = _currentRole.Quyen.Replace("-", "");
                //
                XPCollection<GroupReport> baoCaoList = _obs.CreateObject<XPCollection<GroupReport>>();
                BaoCao_TreeList.DataSource = baoCaoList;
                BaoCao_TreeList.DataBind();
            }
        }

        #endregion

        #region Event

        private void CheckNodeOfTreeList()
        {
            if (string.IsNullOrEmpty(_currentQuyen)) return;
            //
            //Check node
            foreach (TreeListNode node in BaoCao_TreeList.Nodes)
            {
                //Checked
                if (_currentQuyen.ToUpper().Contains(node.Key.ToString().ToUpper()))
                {
                    node.Selected = true;
                }
            }
            //
        }

        protected void btnLuu_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTen.Value.ToString()))
            {
                _currentRole.Ten = txtTen.Value.ToString();
            }

            //Cập nhật quyền
            foreach (TreeListNode node in BaoCao_TreeList.Nodes)
            {
                //
                UpdateQuyen(node);
            }
            //Lưu dữ liệu
            _obs.CommitChanges();

            //
            WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Lưu thành công !!!')");

            //Load lại data
            OnInit(e);
        }

        private void UpdateQuyen(TreeListNode node)
        {
            //
            GroupReport rp = node.DataItem as GroupReport;
            if (rp != null)
            {
                //
                if (node.Selected)
                {
                    if (_currentRole.Quyen == null || (_currentRole.Quyen != null && !SearchQuyen(_currentRole.Quyen.ToUpper(), rp.Oid.ToString().ToUpper())))
                    {
                        _currentRole.Quyen += rp.Oid.ToString().ToUpper() + ";";
                    }
                }
                else
                {
                    if (_currentRole.Quyen != null && (SearchQuyen(_currentRole.Quyen.ToUpper(), rp.Oid.ToString().ToUpper())))
                    {
                        _currentRole.Quyen = _currentRole.Quyen.ToUpper().Replace(rp.Oid.ToString().ToUpper() + ";", String.Empty);
                    }
                }
            }
        }

        private bool SearchQuyen(string quyen, string idBaoCao)
        {
            if (!string.IsNullOrEmpty(quyen))
            {
                string[] quyenList = quyen.Split(";".ToCharArray());
                for (int i = 0; i < quyenList.Length; i++)
                    if (quyenList[i] == idBaoCao)
                    {
                        return true;
                    }
            }
            return false;
        }
        #endregion
    }
}