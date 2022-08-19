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
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using ERP.Module.NghiepVu.NhanSu.NhanViens;

namespace ERP.Module.Win.Controllers._5.NghiepVu._1.NhanSu.NhanVien
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Onload_NhanVien_ThongTinNhanVien_Controller : ViewController
    {

        IObjectSpace _obs = null;
        Session _Session;
        public Onload_NhanVien_ThongTinNhanVien_Controller()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.

            TargetViewId = "ThongTinNhanVien_DetailView;NhanVien_DetailView";
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            _obs = Application.CreateObjectSpace();
            _Session = ((XPObjectSpace)_obs).Session;
            DetailView _DetailView = View as DetailView;

            if (_DetailView.ToString().Contains("ThongTinNhanVien_DetailView"))
            {
                ThongTinNhanVien ttNhanVien = View.CurrentObject as ThongTinNhanVien;
                if (ttNhanVien != null)
                {
                    ttNhanVien.OnLoad();
                    //
                    ttNhanVien.CapNhatChucVu();
                }
                    
            }
            else
                if (_DetailView.ToString().Contains("NhanVien_DetailView"))
                {
                    ERP.Module.NghiepVu.NhanSu.NhanViens.NhanVien nhanVien = View.CurrentObject as ERP.Module.NghiepVu.NhanSu.NhanViens.NhanVien;
                    if (nhanVien != null)
                        nhanVien.OnLoad();
                }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
