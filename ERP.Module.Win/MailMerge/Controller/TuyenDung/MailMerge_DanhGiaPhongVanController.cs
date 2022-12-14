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
    public partial class MailMerge_DanhGiaPhongVanController : ViewController
    {
        public MailMerge_DanhGiaPhongVanController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var quanLyTuyenDungList = new List<QuanLyTuyenDung>();
            //
            foreach (object item in View.SelectedObjects)
            {
                QuanLyTuyenDung quanLyTuyenDung = item as QuanLyTuyenDung;
                if (quanLyTuyenDung != null)
                    quanLyTuyenDungList.Add(quanLyTuyenDung);
            }
            //
            if (quanLyTuyenDungList.Count > 0)
                Process_DanhGiaPhongVan.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), quanLyTuyenDungList);
            //
        }

        private void MailMerge_DanhGiaPhongVanController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<QuanLyTuyenDung>() && Common.IsWriteGranted<ThiSinh>();

        }
    }
}
