using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhChamDutThuViecController : ViewController
    {
        public MailMerge_QuyetDinhChamDutThuViecController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhThongBaoChamDutThuViecList = new List<QuyetDinhThongBaoChamDutThuViec>();
            //
            foreach (object item in View.SelectedObjects)
            {
                //thu viec
                QuyetDinhThongBaoChamDutThuViec quyetDinh = item as QuyetDinhThongBaoChamDutThuViec;
                if (quyetDinh != null)
                    quyetDinhThongBaoChamDutThuViecList.Add(quyetDinh);
            }
            //
            if (quyetDinhThongBaoChamDutThuViecList.Count > 0)
                Prosess_QuyetDinhThongBaoChamDutThuViec.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), quyetDinhThongBaoChamDutThuViecList);
            //
        }
    }
}
