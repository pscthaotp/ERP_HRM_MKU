using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhDieuDongController : ViewController
    {
        public MailMerge_QuyetDinhDieuDongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhDieuDongList = new List<QuyetDinhDieuDong>();
            //
            foreach (object item in View.SelectedObjects)
            {
                QuyetDinhDieuDong quyetDinh = item as QuyetDinhDieuDong;
                if (quyetDinh != null)
                    quyetDinhDieuDongList.Add(quyetDinh);
            }
            //
            if (quyetDinhDieuDongList.Count > 0)
                Prosess_QuyetDinhDieuDong.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), quyetDinhDieuDongList);
            //
        }
    }
}
