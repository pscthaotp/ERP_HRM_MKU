using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhTaiBoNhiemController : ViewController
    {
        public MailMerge_QuyetDinhTaiBoNhiemController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhBoNhiemList = new List<QuyetDinhTaiBoNhiem>();
            //
            foreach (object item in View.SelectedObjects)
            {
                QuyetDinhTaiBoNhiem quyetDinh = item as QuyetDinhTaiBoNhiem;
                if (quyetDinh != null)
                    quyetDinhBoNhiemList.Add(quyetDinh);
            }
            //
            if (quyetDinhBoNhiemList.Count > 0)
                Prosess_QuyetDinhTaiBoNhiem.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), quyetDinhBoNhiemList);
            //
        }
    }
}
