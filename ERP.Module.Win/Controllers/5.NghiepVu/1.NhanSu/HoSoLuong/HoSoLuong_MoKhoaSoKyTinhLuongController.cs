using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.TienLuong;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.HoSoLuong
{
    public partial class HoSoLuong_MoKhoaSoKyTinhLuongController : ViewController
    {
        public HoSoLuong_MoKhoaSoKyTinhLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            KyTinhLuong kyTinhLuong = View.CurrentObject as KyTinhLuong;
            if(kyTinhLuong != null)
            {
                kyTinhLuong.KhoaSo = false;
                View.ObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            }
        }

        private void HoSoLuong_MoKhoaSoKyTinhLuongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.SecuritySystemUser_GetCurrentUser().MoKhoaSoLuong;
        }
    }
}
