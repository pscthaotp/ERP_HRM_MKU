using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Utils;
using ERP.Module.NghiepVu.NhanSu.DaoTao;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.Data.Filtering;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.NghiepVu.NhanSu.Helper;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.DaoTao
{
    public partial class DaoTao_ChonNhanVienDuyetDaoTaoController : ViewController
    {
        private IObjectSpace obs;
        private HoSo_ChonDanhSachNhanVien danhSach;
        private DuyetDangKyDaoTao duyet;

        public DaoTao_ChonNhanVienDuyetDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
            //Common.DebugTrace("DaoTao_LapQuyetDinhDaoTaoController");
        }

        private void DaoTao_ChonNhanVienDuyetDaoTaoController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<QuanLyDaoTao>()
                && Common.IsWriteGranted<DuyetDangKyDaoTao>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            duyet = View.CurrentObject as DuyetDangKyDaoTao;
            if (duyet != null && duyet.DangKyDaoTao != null)
            {
                obs = Application.CreateObjectSpace();
                danhSach = obs.CreateObject<HoSo_ChonDanhSachNhanVien>();
                DaoTao_ChonNhanVien nhanVien;
                foreach (var item in duyet.DangKyDaoTao.ListChiTietDangKyDaoTao)
                {
                    nhanVien = obs.CreateObject<DaoTao_ChonNhanVien>();
                    nhanVien.Chon = true;
                    nhanVien.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                    nhanVien.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                    danhSach.ListNhanVien.Add(nhanVien);
                }
                e.View = Application.CreateDetailView(obs, danhSach);
            }
            else
                Common.ShowWarningMessage("Chưa nhập dữ liệu cho mục Đăng ký đào tạo");
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            foreach (var item in danhSach.ListNhanVien)
            {
                if (item.Chon)
                {
                    if (!DaoTaoHelper.IsExists(duyet, item.ThongTinNhanVien))
                    {
                        duyet.CreateListChiTietDuyetDangKyDaoTao(item);
                    }
                }
            }
            View.Refresh();
        }
    }
}
