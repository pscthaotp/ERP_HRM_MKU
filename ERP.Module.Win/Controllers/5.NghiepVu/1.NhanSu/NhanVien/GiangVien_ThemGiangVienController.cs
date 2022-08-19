using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.Extends;
using DevExpress.ExpressApp.SystemModule;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using System.Windows.Forms;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    public partial class GiangVien_ThemGiangVienController : ViewController
    {
        public GiangVien_ThemGiangVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void GiangVien_ThemGiangVienController_Activated(object sender, EventArgs e)
        {
            if (View.Id.Contains("ThinhGiangCustomView_DetailView"))
            {
                simpleAction1.Active["TruyCap"] = true;
                simpleAction2.Active["TruyCap"] = true;

                //Tắt nút thêm mới mặc định của DevExpress
                DevExpress.ExpressApp.Win.SystemModule.WinNewObjectViewController addButtonListView = Frame.GetController<DevExpress.ExpressApp.Win.SystemModule.WinNewObjectViewController>();
                if (addButtonListView != null)
                    addButtonListView.NewObjectAction.Active["Visible"] = false;

                DevExpress.ExpressApp.Win.SystemModule.WinModificationsController saveButtonListView = Frame.GetController<DevExpress.ExpressApp.Win.SystemModule.WinModificationsController>();
                if (saveButtonListView != null)
                {
                    saveButtonListView.SaveAction.Active["Visible"] = false;
                    saveButtonListView.SaveAndCloseAction.Active["Visible"] = false;
                    saveButtonListView.SaveAndNewAction.Active["Visible"] = false;
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
            GiangVienThinhGiang nv = _obs.CreateObject<GiangVienThinhGiang>();
            Application.ShowView<GiangVienThinhGiang>(_obs, nv);
        }

        private void simpleAction2_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace _obs = Application.CreateObjectSpace();
            GiangVienThinhGiang nv = null;
            ThinhGiangCustomView nsView = View.CurrentObject as ThinhGiangCustomView;
            if (nsView != null)
                nv = _obs.GetObjectByKey<GiangVienThinhGiang>(nsView.Oid);
        
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
