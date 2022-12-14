using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhNghiHuuController : ViewController
    {
        public MailMerge_QuyetDinhNghiHuuController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhNghiHuuList = new List<QuyetDinhNghiHuu>();
            //
            foreach (object item in View.SelectedObjects)
            {
                QuyetDinhNghiHuu quyetDinh = item as QuyetDinhNghiHuu;
                if (quyetDinh != null)
                    quyetDinhNghiHuuList.Add(quyetDinh);
            }
            //
            if (quyetDinhNghiHuuList.Count > 0)
                Prosess_QuyetDinhNghiHuu.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), quyetDinhNghiHuuList);
            //
        }
    }
}
