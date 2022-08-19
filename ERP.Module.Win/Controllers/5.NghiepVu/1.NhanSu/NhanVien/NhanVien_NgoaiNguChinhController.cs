using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    public partial class NhanVien_NgoaiNguChinhController : ViewController
    {
        public NhanVien_NgoaiNguChinhController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            TrinhDoNgoaiNguKhac trinhDo = View.CurrentObject as TrinhDoNgoaiNguKhac;
            if (trinhDo != null)
            {
                if (trinhDo.HoSo is NhanVien)
                {
                    NhanVien nhanVien = View.ObjectSpace.GetObjectByKey<NhanVien>(trinhDo.HoSo.Oid);
                    if (nhanVien != null)
                    {
                        nhanVien.NhanVienTrinhDo.NgoaiNgu = trinhDo.NgoaiNgu;
                        nhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu = trinhDo.TrinhDoNgoaiNgu;
                    }
                }
                else
                {
                    UngVien ungVien = View.ObjectSpace.GetObjectByKey<UngVien>(trinhDo.HoSo.Oid);
                    if (ungVien != null)
                    {
                        ungVien.NgoaiNgu = trinhDo.NgoaiNgu;
                        ungVien.TrinhDoNgoaiNgu = trinhDo.TrinhDoNgoaiNgu;
                    }
                }
                //
                View.Refresh();
            }
        }
    }
}
