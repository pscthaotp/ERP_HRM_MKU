using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhThoiChucKiemNhiemController : ViewController
    {
        public MailMerge_QuyetDinhThoiChucKiemNhiemController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhThoiChucKiemNhiemList = new List<QuyetDinhThoiChucKiemNhiem>();
            //
            foreach (object item in View.SelectedObjects)
            {
                QuyetDinhThoiChucKiemNhiem quyetDinh = item as QuyetDinhThoiChucKiemNhiem;
                if (quyetDinh != null)
                    quyetDinhThoiChucKiemNhiemList.Add(quyetDinh);
            }
            //
            if (quyetDinhThoiChucKiemNhiemList.Count > 0)
                Prosess_QuyetDinhThoiChucKiemNhiem.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), quyetDinhThoiChucKiemNhiemList);
            //
        }
    }
}
