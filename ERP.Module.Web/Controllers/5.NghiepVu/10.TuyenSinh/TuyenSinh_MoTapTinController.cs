using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.TuyenSinh;
using DevExpress.ExpressApp.Web;
using ERP.Module.NghiepVu.HocSinh.HoSoNhapHocs;

namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_MoTapTinController : ViewController
    {
        public TuyenSinh_MoTapTinController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //1. Hồ sơ nhập học
            HoSoNhapHoc_TapTin tapTinHoSo = View.CurrentObject as HoSoNhapHoc_TapTin;
            //
            if (tapTinHoSo != null)
            {
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", "window.open('/Services/TuyenSinh/Download_HoSoNhapHoc.ashx?oidtaptin=" + tapTinHoSo.Oid + "');");
            }
        }

        private void TuyenSinh_MoTapTinController_Activated(object sender, EventArgs e)
        {
            //
            simpleAction1.Active["TruyCap"] = Commons.Common.IsWriteGranted<HoSoNhapHoc_TapTin>();
        }
    }
}
