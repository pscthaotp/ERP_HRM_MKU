using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Security;
using DevExpress.Utils;
using System.Windows.Forms;
using System.Data;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Security.Strategy;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.HeThong;

namespace ERP.Module.Win.Controllers.HeThong
{
    public partial class HeThong_ChucNangPhuController : ViewController
    {
        ChonChucNangPhu _chonChucNangPhu = null;
        IObjectSpace _obs = null;
        SecuritySystemRole_Custom _securitySystemRole = null;

        public HeThong_ChucNangPhuController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void HeThong_ChucNangPhuController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsAccessGranted(typeof(SecuritySystemRole_Custom));
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventArgs e)
        {
            //
            _securitySystemRole = View.CurrentObject as SecuritySystemRole_Custom;
            //
            if (_securitySystemRole != null)
            {
                _obs = Application.CreateObjectSpace();
                //
                _chonChucNangPhu = _obs.CreateObject<ChonChucNangPhu>();
                e.View = Application.CreateDetailView(_obs, _chonChucNangPhu);
            }
        }

        private void popupWindowShowAction1_Execute(object sender, DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventArgs e)
        {
            if (_chonChucNangPhu != null && _chonChucNangPhu.ChucNangList.Count > 0)
            {
                Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                //
                foreach (var item in _chonChucNangPhu.ChucNangList)
                {
                    if (item.Chon && item.AppMenu != null)
                    {
                        SecuritySystemRole_MenuNonPersistent chucNangPhu = new SecuritySystemRole_MenuNonPersistent(session);
                        chucNangPhu.AppMenu = session.GetObjectByKey<AppMenu>(item.AppMenu.Oid);
                        chucNangPhu.SecuritySystemRole = _securitySystemRole;
                        //
                        if (chucNangPhu.AppMenu != null)
                        {
                            _securitySystemRole.ListMenuNonPersistent.Add(chucNangPhu);
                        }
                    }
                }
            }
        }
    }
}
