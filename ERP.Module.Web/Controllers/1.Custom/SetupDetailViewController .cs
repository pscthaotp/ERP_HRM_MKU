using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.TreeListEditors.Web;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.ExpressApp.Actions;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.TuyenSinh;

namespace ERP.Module.Web.Controllers.Custom
{
    public partial class SetupDetailViewController  : ViewController
    {
        private DetailView _detailView;
        public SetupDetailViewController ()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        void SetupDetailViewController_Activated(object sender, EventArgs e)
        {
            View.ControlsCreated += View_ControlsCreated;
            View.Refresh();
        }

        void View_ControlsCreated(object sender, EventArgs e)
        {
            _detailView = View as DetailView;
            if(_detailView != null)
            {
                if (_detailView.Id == "ThongTinKhachHang_DetailView")
                {
                    UpdateListChiTietTuVanTuyenSinh();
                }
            }
        }

        private void UpdateListChiTietTuVanTuyenSinh()
        {
            ThongTinKhachHang _khachHang = _detailView.CurrentObject as ThongTinKhachHang;
            if(_khachHang != null)
            {
                _khachHang.LoadChiTietTuyenSinh();
            }
            
        }
        
    }
}
