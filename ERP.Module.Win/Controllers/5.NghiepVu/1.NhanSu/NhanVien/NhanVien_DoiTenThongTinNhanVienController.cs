using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using ERP.Module.NghiepVu.NhanSu.NhanViens;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    public partial class NhanVien_DoiTenThongTinNhanVienController : ViewController<DetailView>
    {
        public NhanVien_DoiTenThongTinNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        void NhanVien_DoiTenThongTinNhanVienController_Activated(object sender, EventArgs e)
        {
            DetailView detail = View as DetailView;
            if (detail != null && detail.Id.Equals("ThongTinNhanVien_DetailView"))
            {
                ThongTinNhanVien nhanVien = View.CurrentObject as ThongTinNhanVien;
                if (nhanVien != null)
                {
                    if (nhanVien.BoPhan != null)
                        detail.Caption = String.Format("{0} - {1}", nhanVien.BoPhan.TenBoPhan, detail.Caption);
                }
            }
        }
    }
}
