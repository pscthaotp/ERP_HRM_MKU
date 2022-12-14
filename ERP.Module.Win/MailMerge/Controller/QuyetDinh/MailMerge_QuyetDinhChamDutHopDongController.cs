using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhChamDutHopDongController : ViewController
    {
        public MailMerge_QuyetDinhChamDutHopDongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhChamDutHĐList = new List<QuyetDinhChamDutHopDong>();
            //
            foreach (object item in View.SelectedObjects)
            {
                QuyetDinhChamDutHopDong quyetDinh = item as QuyetDinhChamDutHopDong;
                if (quyetDinh != null)
                    quyetDinhChamDutHĐList.Add(quyetDinh);
            }
            //
            if (quyetDinhChamDutHĐList.Count > 0)
                Prosess_QuyetDinhChamDutHopDong.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), quyetDinhChamDutHĐList);
            //
        }
    }
}
