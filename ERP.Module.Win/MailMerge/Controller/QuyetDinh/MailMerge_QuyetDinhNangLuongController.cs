using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhNangLuongController : ViewController
    {
        public MailMerge_QuyetDinhNangLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhNangLuongList = new List<QuyetDinhNangLuong>();
            //
            QuyetDinhNangLuong qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhNangLuong)item;
                if (qd != null)
                    quyetDinhNangLuongList.Add(qd);
            }
            //
            Prosess_QuyetDinhNangLuong.Merge(((XPObjectSpace)View.ObjectSpace), quyetDinhNangLuongList);
        }
    }
}
