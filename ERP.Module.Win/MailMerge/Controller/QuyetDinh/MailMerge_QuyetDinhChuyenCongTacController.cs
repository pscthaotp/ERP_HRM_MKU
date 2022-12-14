using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhChuyenCongTacController : ViewController
    {
        public MailMerge_QuyetDinhChuyenCongTacController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhChuyenCongTacList = new List<QuyetDinhChuyenCongTac>();
            //
            foreach (object item in View.SelectedObjects)
            {
                QuyetDinhChuyenCongTac quyetDinh = item as QuyetDinhChuyenCongTac;
                if (quyetDinh != null)
                    quyetDinhChuyenCongTacList.Add(quyetDinh);
            }
            //
            if (quyetDinhChuyenCongTacList.Count > 0)
                Prosess_QuyetDinhChuyenCongTac.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), quyetDinhChuyenCongTacList);
            //
        }
    }
}
