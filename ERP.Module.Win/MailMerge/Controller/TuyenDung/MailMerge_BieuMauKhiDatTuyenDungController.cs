using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Win.MailMerge.Prosess.TuyenDung;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;

namespace ERP.Module.Win.MailMerge.Controller.TuyenDung
{
    public partial class MailMerge_BieuMauKhiDatTuyenDungController : ViewController
    {
        public MailMerge_BieuMauKhiDatTuyenDungController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            QuyetDinhTuyenDung quyetDinhTuyenDung = View.CurrentObject as QuyetDinhTuyenDung;
            var thongTinNhanVienList = new List<ThongTinNhanVien>();
            //
            foreach (object item in View.SelectedObjects)
            {
                ChiTietQuyetDinhTuyenDung chiTietQuyetDinhTuyenDung = item as ChiTietQuyetDinhTuyenDung;
                quyetDinhTuyenDung = View.ObjectSpace.GetObjectByKey<QuyetDinhTuyenDung>(chiTietQuyetDinhTuyenDung.QuyetDinhTuyenDung.Oid);
                ThongTinNhanVien thongTinNhanVien = View.ObjectSpace.GetObjectByKey<ThongTinNhanVien>(chiTietQuyetDinhTuyenDung.ThongTinNhanVien.Oid);
                if (thongTinNhanVien != null)
                    thongTinNhanVienList.Add(thongTinNhanVien);
            }
            //
            if (thongTinNhanVienList.Count > 0)
                Process_BieuMauKhiDatTuyenDung.ShowMailMerge(((XPObjectSpace)View.ObjectSpace), thongTinNhanVienList, quyetDinhTuyenDung);
            //
        }

        private void MailMerge_BieuMauKhiDatTuyenDungController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<QuanLyTuyenDung>() && Common.IsWriteGranted<TrungTuyen>();

        }
    }
}
