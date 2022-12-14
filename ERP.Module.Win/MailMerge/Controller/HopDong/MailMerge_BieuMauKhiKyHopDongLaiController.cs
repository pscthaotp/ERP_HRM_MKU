using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.HopDongs;
using ERP.Module.Win.MailMerge.Prosess.HopDong;

namespace ERP.Module.Win.MailMerge.Controller.HopDongs
{
    public partial class MailMerge_BieuMauKhiKyHopDongLaiController : ViewController
    {
        public MailMerge_BieuMauKhiKyHopDongLaiController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var hopDongLamViecList = new List<HopDongLamViec>();
           
            foreach (object item in View.SelectedObjects)
            {
                HopDongLamViec hopDongLamViec = item as HopDongLamViec;
                if (hopDongLamViec != null)
                    hopDongLamViecList.Add(hopDongLamViec);
            }

            if (hopDongLamViecList.Count > 0)
                Process_BieuMauKhiKyLaiHopDong.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), hopDongLamViecList);
            //
        }

        private void MailMerge_BieuMauKhiDatTuyenDungController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<QuanLyHopDong>() && Common.IsWriteGranted<HopDong>();

        }
    }
}
