using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhThanhLapDonViController : ViewController
    {
        public MailMerge_QuyetDinhThanhLapDonViController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            List<QuyetDinhThanhLapDonVi> listCaNhan = new List<QuyetDinhThanhLapDonVi>();
            List<QuyetDinhThanhLapDonVi> listTapThe = new List<QuyetDinhThanhLapDonVi>();
            QuyetDinhThanhLapDonVi qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhThanhLapDonVi)item;
                if (qd != null)
                    listCaNhan.Add(qd);
                else
                    listTapThe.Add(qd);
            }


            if (listCaNhan.Count > 0)
            {
                Prosess_QuyetDinhThanhLapDonVi.ShowMailMergeQuyetDinhCaNhan(((XPObjectSpace)View.ObjectSpace), listCaNhan);
            }
            if (listTapThe.Count > 0)
            {
                Prosess_QuyetDinhThanhLapDonVi.ShowMailMergeQuyetDinhTapThe(((XPObjectSpace)View.ObjectSpace), listTapThe);
            }
        }
    }
}
