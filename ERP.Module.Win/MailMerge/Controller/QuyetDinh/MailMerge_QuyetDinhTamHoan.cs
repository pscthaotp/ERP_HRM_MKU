using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhTamHoanController : ViewController
    {
        public MailMerge_QuyetDinhTamHoanController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhBietPhaiList = new List<QuyetDinhTamHoan>();
            //
            foreach (object item in View.SelectedObjects)
            {
                QuyetDinhTamHoan quyetDinh = item as QuyetDinhTamHoan;
                if (quyetDinh != null)
                    quyetDinhBietPhaiList.Add(quyetDinh);
            }
            //
            if (quyetDinhBietPhaiList.Count > 0)
                Prosess_QuyetDinhTamHoan.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), quyetDinhBietPhaiList);
            //
        }
    }
}
