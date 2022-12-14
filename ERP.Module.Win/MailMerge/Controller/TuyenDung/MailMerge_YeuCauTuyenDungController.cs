using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.Win.MailMerge.Prosess.TuyenDung;
using ERP.Module.Commons;

namespace ERP.Module.Win.MailMerge.Controller.TuyenDung
{
    public partial class MailMerge_YeuCauTuyenDungController : ViewController
    {
        public MailMerge_YeuCauTuyenDungController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var dangKyTuyenDungList = new List<DangKyTuyenDung>();
            //
            foreach (object item in View.SelectedObjects)
            {
                DangKyTuyenDung dangKyTuyenDung = item as DangKyTuyenDung;
                if (dangKyTuyenDung != null)
                    dangKyTuyenDungList.Add(dangKyTuyenDung);
            }
            //
            if (dangKyTuyenDungList.Count > 0)
                Process_YeuCauTuyenDung.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), dangKyTuyenDungList);
            //
        }

        private void MailMerge_YeuCauTuyenDungController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<QuanLyTuyenDung>() && Common.IsWriteGranted<ViTriTuyenDung>();

        }
    }
}
