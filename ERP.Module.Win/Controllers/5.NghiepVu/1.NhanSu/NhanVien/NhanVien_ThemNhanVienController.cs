using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.Extends;
using DevExpress.ExpressApp.SystemModule;
using ERP.Module.NonPersistentObjects.NhanSu;
using System.Windows.Forms;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    public partial class NhanVien_ThemNhanVienController : ViewController
    {
        public NhanVien_ThemNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void NhanVien_ThemNhanVienController_Activated(object sender, EventArgs e)
        {
            if (View.Id.Contains("NhanVienDangLamViec_ListView")
                   || View.Id.Contains("NhanVienDangLamViec_DetailView")
                    || View.Id.Contains("ThongTinNhanVien_DetailView")
                    || View.Id.Contains("NhanSuCustomView_DetailView"))
            {
                simpleAction1.Active["TruyCap"] = true;
                simpleAction2.Active["TruyCap"] = true;

                //Tắt nút thêm mới mặc định của DevExpress
                DevExpress.ExpressApp.Win.SystemModule.WinNewObjectViewController addButtonListView = Frame.GetController<DevExpress.ExpressApp.Win.SystemModule.WinNewObjectViewController>();
                if (addButtonListView != null)
                    addButtonListView.NewObjectAction.Active["Visible"] = false;

                if (View.Id.Contains("NhanSuCustomView_DetailView"))
                {
                    DevExpress.ExpressApp.Win.SystemModule.WinModificationsController saveButtonListView = Frame.GetController<DevExpress.ExpressApp.Win.SystemModule.WinModificationsController>();
                    if (saveButtonListView != null)
                    {
                        saveButtonListView.SaveAction.Active["Visible"] = false;
                        saveButtonListView.SaveAndCloseAction.Active["Visible"] = false;
                        saveButtonListView.SaveAndNewAction.Active["Visible"] = false;
                    }
                }
            }
            else
            {
                simpleAction1.Active["TruyCap"] = false;
                simpleAction2.Active["TruyCap"] = false;
            }
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace _obs = Application.CreateObjectSpace();
            ERP.Module.NghiepVu.NhanSu.NhanViens.ThongTinNhanVien nv = _obs.CreateObject<ERP.Module.NghiepVu.NhanSu.NhanViens.ThongTinNhanVien>();
            Application.ShowView<ERP.Module.NghiepVu.NhanSu.NhanViens.ThongTinNhanVien>(_obs, nv);
        }

        private void simpleAction2_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace _obs = Application.CreateObjectSpace();
            ERP.Module.NghiepVu.NhanSu.NhanViens.ThongTinNhanVien nv = null;
            if (View.Id == "NhanSuCustomView_DetailView")
            {
                NhanSuCustomView nsView = View.CurrentObject as NhanSuCustomView;
                if (nsView != null)
                    nv = _obs.GetObjectByKey<ERP.Module.NghiepVu.NhanSu.NhanViens.ThongTinNhanVien>(nsView.Oid);
            }
            else
            {
                nv = View.CurrentObject as ERP.Module.NghiepVu.NhanSu.NhanViens.ThongTinNhanVien;
            }

            if (nv != null)
            {
                if (DialogUtil.ShowYesNo("Bạn có thực sự muốn xóa " + nv.HoTen + " ?") == DialogResult.Yes)
                {
                    nv.Delete();
                    _obs.CommitChanges();
                }
            }
        }
    }
}
