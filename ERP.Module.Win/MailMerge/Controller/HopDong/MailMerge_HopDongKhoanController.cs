using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using System.Collections.Generic;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.HopDongs;
using ERP.Module.Win.MailMerge.Prosess.HopDong;
using ERP.Module.Commons;

namespace ERP.Module.Win.MailMerge.Controller.HopDongs
{
    public partial class MailMerge_HopDongKhoanController : ViewController
    {
        private IObjectSpace _obs;
        //
        public MailMerge_HopDongKhoanController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            //
            var hopDongKhoanList = new List<HopDongKhoan>();
            //
            foreach (object item in View.SelectedObjects)
            {
                HopDongKhoan hopDongKhoan = item as HopDongKhoan;
                if (hopDongKhoan != null)
                    hopDongKhoanList.Add(hopDongKhoan);
            }
            //
            if (hopDongKhoanList.Count > 0)
            {              
                //2. Mần non ABI + các trường khác
                //if (hopDongKhoanList[0].QuanLyHopDong.CongTy.Oid.Equals(Config.KeyABI))
                    Prosess_HopDongKhoan.ShowMailMerge_ABI(((XPObjectSpace)View.ObjectSpace), hopDongKhoanList);
            }
            //
        }

        private void MailMerge_HopDongKhoanController_Activated(object sender, EventArgs e)
        {
            //
            simpleAction1.Active["TruyCap"] = Common.IsCreateGranted<HopDongKhoan>();
        }
    }
}
