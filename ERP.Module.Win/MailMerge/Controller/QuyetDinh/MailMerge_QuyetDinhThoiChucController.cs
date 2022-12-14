using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhThoiChucController : ViewController
    {
        public MailMerge_QuyetDinhThoiChucController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhThoiChucList = new List<QuyetDinhThoiChuc>();
            //
            foreach (object item in View.SelectedObjects)
            {
                QuyetDinhThoiChuc quyetDinh = item as QuyetDinhThoiChuc;
                if (quyetDinh != null)
                    quyetDinhThoiChucList.Add(quyetDinh);
            }
            //
            if (quyetDinhThoiChucList.Count > 0)
                Prosess_QuyetDinhThoiChuc.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), quyetDinhThoiChucList);
            //
        }
    }
}
