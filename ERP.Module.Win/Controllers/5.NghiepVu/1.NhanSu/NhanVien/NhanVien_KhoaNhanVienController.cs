using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    public partial class NhanVien_KhoaNhanVienController : ViewController
    {
        public NhanVien_KhoaNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ThongTinNhanVien nhanVien = View.CurrentObject as ThongTinNhanVien;
            nhanVien.KhoaHoSo = !nhanVien.KhoaHoSo;
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

        private void NhanVien_KhoaNhanVienController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<ThongTinNhanVien>();
        }
    }
}
