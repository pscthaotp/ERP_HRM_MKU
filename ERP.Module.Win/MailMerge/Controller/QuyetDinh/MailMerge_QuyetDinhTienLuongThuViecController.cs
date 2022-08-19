using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhTienLuongThuViecController : ViewController
    {
        public MailMerge_QuyetDinhTienLuongThuViecController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhTienLuongThuViecList = new List<QuyetDinhTienLuongThuViec>();
            //
            foreach (object item in View.SelectedObjects)
            {
                QuyetDinhTienLuongThuViec quyetDinh = item as QuyetDinhTienLuongThuViec;
                if (quyetDinh != null)
                    quyetDinhTienLuongThuViecList.Add(quyetDinh);
            }
            //
            if (quyetDinhTienLuongThuViecList.Count > 0)
                Process_QuyetDinhTienLuongThuViec.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), quyetDinhTienLuongThuViecList);
            //
        }
    }
}
