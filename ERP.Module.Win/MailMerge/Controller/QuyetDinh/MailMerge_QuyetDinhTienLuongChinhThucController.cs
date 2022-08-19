using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhTienLuongChinhThucController : ViewController
    {
        public MailMerge_QuyetDinhTienLuongChinhThucController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhTienLuongChinhThucList = new List<QuyetDinhTienLuongChinhThuc>();
            //
            foreach (object item in View.SelectedObjects)
            {
                QuyetDinhTienLuongChinhThuc quyetDinh = item as QuyetDinhTienLuongChinhThuc;
                if (quyetDinh != null)
                    quyetDinhTienLuongChinhThucList.Add(quyetDinh);
            }
            //
            if (quyetDinhTienLuongChinhThucList.Count > 0)
                Process_QuyetDinhTienLuongChinhThuc.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), quyetDinhTienLuongChinhThucList);
            //
        }
    }
}
