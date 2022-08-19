using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
    public partial class MailMerge_QuyetDinhLuanChuyenController : ViewController
    {
        public MailMerge_QuyetDinhLuanChuyenController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhBietPhaiList = new List<QuyetDinhLuanChuyen>();
            //
            foreach (object item in View.SelectedObjects)
            {
                QuyetDinhLuanChuyen quyetDinh = item as QuyetDinhLuanChuyen;
                if (quyetDinh != null)
                    quyetDinhBietPhaiList.Add(quyetDinh);
            }
            //
            if (quyetDinhBietPhaiList.Count > 0)
                Prosess_QuyetDinhLuanChuyen.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), quyetDinhBietPhaiList);
            //
        }
    }
}
