using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.Win.MailMerge.Prosess.TuyenDung;

namespace ERP.Module.Win.MailMerge.Controller.TuyenDung
{
    public partial class MailMerge_ThuCamOnController : ViewController
    {
        public MailMerge_ThuCamOnController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var khongTrungTuyenList = new List<KhongTrungTuyen>();
            //
            foreach (object item in View.SelectedObjects)
            {
                KhongTrungTuyen khongTrungTuyen = item as KhongTrungTuyen;
                if (khongTrungTuyen != null)
                    khongTrungTuyenList.Add(khongTrungTuyen);
            }
            //
            if (khongTrungTuyenList.Count > 0)
                Process_ThuCamOn.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), khongTrungTuyenList);
            //
        }
    }
}
