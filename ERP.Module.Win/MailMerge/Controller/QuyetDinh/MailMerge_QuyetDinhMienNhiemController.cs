using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhMienNhiemController : ViewController
    {
        public MailMerge_QuyetDinhMienNhiemController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhMienNhiemList = new List<QuyetDinhMienNhiem>();
            //
            foreach (object item in View.SelectedObjects)
            {
                QuyetDinhMienNhiem quyetDinh = item as QuyetDinhMienNhiem;
                if (quyetDinh != null)
                    quyetDinhMienNhiemList.Add(quyetDinh);
            }
            //
            if (quyetDinhMienNhiemList.Count > 0)
                Prosess_QuyetDinhMienNhiem.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), quyetDinhMienNhiemList);
            //
        }
    }
}
