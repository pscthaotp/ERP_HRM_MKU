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
    public partial class MailMerge_HopDongThinhGiangController : ViewController
    {
        private IObjectSpace _obs;
        //
        public MailMerge_HopDongThinhGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "HopDongThinhGiang_DetailView";
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            //
            var hopDongList = new List<HopDongThinhGiang>();
            //
            foreach (object item in View.SelectedObjects)
            {
                HopDongThinhGiang hopDong = item as HopDongThinhGiang;
                if (hopDong != null)
                    hopDongList.Add(hopDong);
            }
            //
            if (hopDongList.Count > 0)
                Prosess_HopDongThingGiang.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), hopDongList);
            //
        }

        private void MailMerge_HopDongThinhGiangController_Activated(object sender, EventArgs e)
        {
            //
            //simpleAction1.Active["TruyCap"] = Common.IsCreateGranted<HopDongThinhGiang>();
        }
    }
}
