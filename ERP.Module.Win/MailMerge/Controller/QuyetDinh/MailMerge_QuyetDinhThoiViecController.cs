using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhThoiViecController : ViewController
    {
        public MailMerge_QuyetDinhThoiViecController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhThoiViecList = new List<QuyetDinhThoiViec>();
            //
            foreach (object item in View.SelectedObjects)
            {
                QuyetDinhThoiViec quyetDinh = item as QuyetDinhThoiViec;
                if (quyetDinh != null)
                    quyetDinhThoiViecList.Add(quyetDinh);
            }
            //
            if (quyetDinhThoiViecList.Count > 0)
                Prosess_QuyetDinhThoiViec.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), quyetDinhThoiViecList);
            //
        }
    }
}
