using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.Extends;
//
namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    public partial class NhanVien_TrinhDoCaoNhat : ViewController
    {
        public NhanVien_TrinhDoCaoNhat()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            VanBang trinhDo = View.CurrentObject as VanBang;
            //
            if (trinhDo != null)
            {
                if (trinhDo.HoSo is NhanVien)
                {
                    NhanVien nhanVien = View.ObjectSpace.GetObjectByKey<NhanVien>(trinhDo.HoSo.Oid);
                    if (nhanVien != null)
                    {
                        nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDo.TrinhDoChuyenMon;
                        nhanVien.NhanVienTrinhDo.HinhThucDaoTao = trinhDo.HinhThucDaoTao;
                        nhanVien.NhanVienTrinhDo.TruongDaoTao = trinhDo.TruongDaoTao;
                        nhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao = trinhDo.ChuyenNganhDaoTao;
                        nhanVien.NhanVienTrinhDo.NamTotNghiep = trinhDo.NamTotNghiep;
                        if (trinhDo.NgayCapBang != DateTime.MinValue)
                            nhanVien.NhanVienTrinhDo.NgayCapBang = trinhDo.NgayCapBang;
                    }
                    else
                    {
                        DialogUtil.ShowWarning("Nhấn Lưu nhân viên trước khi tạo trình độ cao nhất.");
                    }
                }
                else
                {
                    UngVien ungVien = View.ObjectSpace.GetObjectByKey<UngVien>(trinhDo.HoSo.Oid);
                    if (ungVien != null)
                    {
                        ungVien.TrinhDoChuyenMon = trinhDo.TrinhDoChuyenMon;
                        ungVien.HinhThucDaoTao = trinhDo.HinhThucDaoTao;
                        ungVien.TruongDaoTao = trinhDo.TruongDaoTao;
                        ungVien.ChuyenNganhDaoTao = trinhDo.ChuyenNganhDaoTao;
                        ungVien.NamTotNghiep = trinhDo.NamTotNghiep;
                    }
                    else
                    {
                        DialogUtil.ShowWarning("Nhấn Lưu ứng viên trước khi tạo trình độ cao nhất.");
                    }
                }
                //
                View.Refresh();
            }
        }
    }
}
