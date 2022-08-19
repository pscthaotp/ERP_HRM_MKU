using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Win.MailMerge.Prosess.ShowMaiMerge;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;
using DevExpress.ExpressApp.Xpo;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhTuyenDungController : ViewController
    {
        public MailMerge_QuyetDinhTuyenDungController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var listCaNhan = new List<QuyetDinhTuyenDung>();
            var listTapThe = new List<QuyetDinhTuyenDung>();
            QuyetDinhTuyenDung qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhTuyenDung)item;
                if (qd != null && qd.ListChiTietQuyetDinhTuyenDung.Count == 1)
                    listCaNhan.Add(qd);
                else
                    listTapThe.Add(qd); 
            }


            if (listCaNhan.Count() > 0)
            {
                Process_QuyetDinhTuyenDung.ShowMailMergeQuyetDinhCaNhan(((XPObjectSpace)View.ObjectSpace), listCaNhan);
            }
            if (listTapThe.Count() > 0)
            {
                Process_QuyetDinhTuyenDung.ShowMailMergeQuyetDinhTapThe(((XPObjectSpace)View.ObjectSpace), listTapThe);
            }
           

        }
    }
}
