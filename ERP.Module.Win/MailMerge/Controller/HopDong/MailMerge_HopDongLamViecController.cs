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
    public partial class MailMerge_HopDongLamViecController : ViewController
    {
        private IObjectSpace _obs;
        //
        public MailMerge_HopDongLamViecController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            //
            var hopDongLamViecList = new List<HopDongLamViec>();
            //
            foreach (object item in View.SelectedObjects)
            {
                HopDongLamViec hopDongLamViec = item as HopDongLamViec;
                if (hopDongLamViec != null)
                    hopDongLamViecList.Add(hopDongLamViec);
            }
            //
            if (hopDongLamViecList.Count > 0)
            {
                Prosess_HopDongLamViec.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), hopDongLamViecList);               
            }
            //
        }

        private void MailMerge_HopDongLamViecController_Activated(object sender, EventArgs e)
        {
            //
            simpleAction1.Active["TruyCap"] = Common.IsCreateGranted<HopDongLamViec>();
        }
    }
}
