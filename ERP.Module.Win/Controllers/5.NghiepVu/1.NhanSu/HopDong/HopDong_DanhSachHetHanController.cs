using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using ERP.Module.Extends;
using ERP.Module.NghiepVu.NhanSu.HopDongs;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.HopDongs
{
    public partial class HopDong_DanhSachHetHanController : ViewController
    {
        public HopDong_DanhSachHetHanController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void HopDong_DanhSachHetHanController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;
            //
            if (view != null)
            {
                DanhSachHetHanHopDong hetHanHopDong = view.CurrentObject as DanhSachHetHanHopDong;
                if (hetHanHopDong != null)
                {
                    ControlViewItem controlSearch = ((DetailView)View).FindItem("btnSearch") as ControlViewItem;
                    //
                        if (controlSearch != null)
                        {
                            SimpleButton button = controlSearch.Control as SimpleButton;
                            if (button != null)
                            {
                                button.Text = "Tìm kiếm";
                                button.Click += (se, ea) =>
                                    {
                                        using (DialogUtil.AutoWait())
                                        {
                                            hetHanHopDong.LoadData();
                                        }
                                    };
                            }
                        }
                }
            }
        }
    }
}
