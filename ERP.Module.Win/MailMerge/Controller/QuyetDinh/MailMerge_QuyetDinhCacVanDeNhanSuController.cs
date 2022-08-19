using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Win.MailMerge.Prosess.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Controller.QuyetDinh
{
     public partial class MailMerge_QuyetDinhCacVanDeNhanSuController : ViewController
    {
        public MailMerge_QuyetDinhCacVanDeNhanSuController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quyetDinhCacVanDeNhanSuList = new List<QuyetDinhCacVanDeNhanSu>();
            //
            foreach (object item in View.SelectedObjects)
            {
                QuyetDinhCacVanDeNhanSu quyetDinh = item as QuyetDinhCacVanDeNhanSu;
                if (quyetDinh != null)
                    quyetDinhCacVanDeNhanSuList.Add(quyetDinh);
            }

            if (quyetDinhCacVanDeNhanSuList.Count > 0)
                Prosess_QuyetDinhCacVanDeNhanSu.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), quyetDinhCacVanDeNhanSuList);
           
        }
    }
}
