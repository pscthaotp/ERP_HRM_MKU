using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.Utils;
using DevExpress.Xpo;
using System.ComponentModel;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.Commons;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.NhanSu;

namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.NhanSu
{
    public partial class TuyenDung_ImportUngVienTuExcelController : ViewController
    {
        private IObjectSpace obs;
        private QuanLyTuyenDung qlTuyenDung;
        private TuyenDung_NhapUngVien nhapUngVien;

        public TuyenDung_ImportUngVienTuExcelController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            qlTuyenDung = View.CurrentObject as QuanLyTuyenDung;
            if (qlTuyenDung != null)
            {
                nhapUngVien = obs.CreateObject<TuyenDung_NhapUngVien>();
                nhapUngVien.SetNhuCauTuyenDungList(qlTuyenDung);
                e.View = Application.CreateDetailView(obs, nhapUngVien);
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //e.PopupWindow.View.ObjectSpace.CommitChanges();            
            obs = Application.CreateObjectSpace();
            Imp_Candidate.ImportCandidate(obs, nhapUngVien);
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

        private void BienDongAction_Activated(object sender, EventArgs e)
        {           
            popupWindowShowAction1.Active["TruyCap"] =
                Common.IsWriteGranted<QuanLyTuyenDung>() &&
                Common.IsWriteGranted<UngVien>();
        }
    }
}
