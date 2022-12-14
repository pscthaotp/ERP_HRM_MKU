using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhKhenThuongController : ViewController
    {
        public MailMerge_QuyetDinhKhenThuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhKhenThuongList = new List<QuyetDinhKhenThuong>();
            //
            QuyetDinhKhenThuong qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhKhenThuong)item;
                if (qd != null)
                    quyetDinhKhenThuongList.Add(qd);
            }
            //
            Prosess_QuyetDinhKhenThuong.Merge(((XPObjectSpace)View.ObjectSpace), quyetDinhKhenThuongList);
        }
    }
}
