using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhBietPhaiController : ViewController
    {
        public MailMerge_QuyetDinhBietPhaiController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction2_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhBietPhaiList = new List<QuyetDinhBietPhai>();
            //
            foreach (object item in View.SelectedObjects)
            {
                QuyetDinhBietPhai quyetDinh = item as QuyetDinhBietPhai;
                if (quyetDinh != null)
                    quyetDinhBietPhaiList.Add(quyetDinh);
            }
            //
            if (quyetDinhBietPhaiList.Count > 0)
                Prosess_QuyetDinhBietPhai.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), quyetDinhBietPhaiList);
            //
        }
    }
}
