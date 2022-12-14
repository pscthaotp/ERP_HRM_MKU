using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Security.Strategy;
using ERP.Module.HeThong;
using ERP.Module.Extends;

namespace ERP.Module.Win.Controllers.HeThong
{
    public partial class HeThong_XemThongTinUserController : ViewController
    {
        public HeThong_XemThongTinUserController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Session session = ((XPObjectSpace)View.ObjectSpace).Session;
            //
           SecuritySystemUser user = View.CurrentObject as SecuritySystemUser;
            if (user != null)
            {
                SecuritySystemUser_Custom user_Custom = session.GetObjectByKey<SecuritySystemUser_Custom>(user.Oid);
                if (user_Custom != null && user_Custom.ThongTinNhanVien != null)
                    DialogUtil.ShowInfo(String.Concat("Tài khoản ", user_Custom.UserName, " - ", user_Custom.ThongTinNhanVien.HoTen," - ", user_Custom.BoPhan.TenBoPhan, " - ", user_Custom.CongTy.TenBoPhan));
                else
                    DialogUtil.ShowInfo("Tài khoản quản trị hoặc Tài khoản vô danh !");
            }            
        }

        private void HeThong_XemThongTinUserController_Activated(object sender, EventArgs e)
        {
            if (!Common.TaiKhoanBinhThuong())
            {
                this.simpleAction1.Active["TruyCap"] = true;
            }
            else
            {
                this.simpleAction1.Active["TruyCap"] = false;
            }
        }
    }
}
