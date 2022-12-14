using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhKyLuatController : ViewController
    {
        public MailMerge_QuyetDinhKyLuatController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhKyLuatList = new List<QuyetDinhKyLuat>();
            //
            foreach (object item in View.SelectedObjects)
            {
                QuyetDinhKyLuat quyetDinh = item as QuyetDinhKyLuat;
                if (quyetDinh != null)
                    quyetDinhKyLuatList.Add(quyetDinh);
            }
            //
            if (quyetDinhKyLuatList.Count > 0)
                Prosess_QuyetDinhKyLuat.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), quyetDinhKyLuatList);
            //
        }
    }
}
