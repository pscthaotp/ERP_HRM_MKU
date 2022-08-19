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

namespace ERP.Module.Win.Controllers.HeThong
{
    public partial class HeThong_ChangePassController : ViewController
    {
        
        public HeThong_ChangePassController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        private void HeThong_ChangePassController_Activated(object sender, EventArgs e)
        {
                simpleAction1.Active["TruyCap"] = Common.IsAccessGranted(typeof(SecuritySystemUser_Custom));
        }
        private void simpleAction1_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        { 
            if (View != null)
            {
                SecuritySystemUser_Custom nguoiDungHienTai = View.ObjectSpace.GetObjectByKey<SecuritySystemUser_Custom>(Common.SecuritySystemUser_GetCurrentUser().Oid);
                if (nguoiDungHienTai != null)
                {
                    nguoiDungHienTai.ChangePasswordOnFirstLogon = true;
                    View.ObjectSpace.CommitChanges();
                    //
                    DialogUtil.ShowInfo("Gởi yêu cầu đổi mật khẩu tài khoản " + @"""" + nguoiDungHienTai.UserName.ToString()  +@""""  +" thành công!");
                }
            }
        }
    }
}
