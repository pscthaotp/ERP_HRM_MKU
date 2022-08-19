using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using DevExpress.Utils;
using System.Data.SqlClient;
using ERP.Module.Commons;
using ERP.Module.HeThong;
using ERP.Module.Extends;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NonPersistentObjects.HeThong;

namespace ERP.Module.Win.Controllers.HeThong
{
    public partial class HeThong_CopyRoleUserController : ViewController
    {
        ChonSecuritySystemUser _chonUser;
        SecuritySystemUser_Custom user_Old;

        public HeThong_CopyRoleUserController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        private void HeThong_CopyRoleUserController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] =  Common.IsAccessGranted(typeof(SecuritySystemUser_Custom));
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace _obs = Application.CreateObjectSpace();
            _chonUser = _obs.CreateObject<ChonSecuritySystemUser>();
            //
            e.View = Application.CreateDetailView(_obs, _chonUser);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            
            SecuritySystemUser_Custom user_Old = View.CurrentObject as SecuritySystemUser_Custom;
            if (user_Old != null)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@User_Old", user_Old.Oid);
                param[1] = new SqlParameter("@User_New", _chonUser.User.Oid);

                DataProvider.ExecuteNonQuery("spd_HeThong_CopyRoleUser", System.Data.CommandType.StoredProcedure, param);
            }
        }
    }
}
