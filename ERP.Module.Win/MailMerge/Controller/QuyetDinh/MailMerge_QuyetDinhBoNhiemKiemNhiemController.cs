using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhBoNhiemKiemNhiemController : ViewController
    {
        public MailMerge_QuyetDinhBoNhiemKiemNhiemController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhBoNhiemKiemNhiemList = new List<QuyetDinhBoNhiemKiemNhiem>();
            //
            foreach (object item in View.SelectedObjects)
            {
                QuyetDinhBoNhiemKiemNhiem quyetDinh = item as QuyetDinhBoNhiemKiemNhiem;
                if (quyetDinh != null)
                    quyetDinhBoNhiemKiemNhiemList.Add(quyetDinh);
            }
            //
            if (quyetDinhBoNhiemKiemNhiemList.Count > 0)
                Prosess_QuyetDinhBoNhiemKiemNhiem.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), quyetDinhBoNhiemKiemNhiemList);
            //
        }
    }
}
