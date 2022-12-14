using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.Win.MailMerge.Prosess.TuyenDung;

namespace ERP.Module.Win.MailMerge.Controller.TuyenDung
{
    public partial class MailMerge_ThuTuyenDungController : ViewController
    {
        public MailMerge_ThuTuyenDungController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var trungTuyenList = new List<TrungTuyen>();
            //
            foreach (object item in View.SelectedObjects)
            {
                TrungTuyen trungTuyen = item as TrungTuyen;
                if (trungTuyen != null)
                    trungTuyenList.Add(trungTuyen);
            }
            //
            if (trungTuyenList.Count > 0)
                Process_ThuTuyenDung.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), trungTuyenList);
            //
        }
    }
}
