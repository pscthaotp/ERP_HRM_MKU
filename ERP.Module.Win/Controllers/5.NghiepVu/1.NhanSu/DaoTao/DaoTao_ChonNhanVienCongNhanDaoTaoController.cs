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
    public partial class DaoTao_ChonNhanVienCongNhanDaoTaoController : ViewController
    {
        private IObjectSpace obs;
        private HoSo_ChonDanhSachNhanVien danhSach;
        private QuyetDinhCongNhanDaoTao qdCongNhanDaoTao;

        public DaoTao_ChonNhanVienCongNhanDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
            //Common.DebugTrace("DaoTao_LapQuyetDinhDaoTaoController");
        }

        private void DaoTao_ChonNhanVienCongNhanDaoTaoController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsCreateGranted<QuyetDinhCongNhanDaoTao>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            qdCongNhanDaoTao = View.CurrentObject as QuyetDinhCongNhanDaoTao;
            if (qdCongNhanDaoTao != null && qdCongNhanDaoTao.QuyetDinhDaoTao != null)
            {
                using (ERP.Module.Extends.DialogUtil.AutoWait("Hệ thống đang xử lý. Vui lòng chờ!"))
                {
                    obs = Application.CreateObjectSpace();
                    danhSach = obs.CreateObject<HoSo_ChonDanhSachNhanVien>();
                    DaoTao_ChonNhanVien nhanVien;
                    foreach (var item in qdCongNhanDaoTao.QuyetDinhDaoTao.ListChiTietQuyetDinhDaoTao)
                    {
                        nhanVien = obs.CreateObject<DaoTao_ChonNhanVien>();
                        nhanVien.Chon = true;
                        nhanVien.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                        nhanVien.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                        danhSach.ListNhanVien.Add(nhanVien);
                    }
                    e.View = Application.CreateDetailView(obs, danhSach);
                }
            }
            else
                Common.ShowWarningMessage("Chưa nhập dữ liệu cho mục Quyết định đào tạo");
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            foreach (var item in danhSach.ListNhanVien)
            {
                if (item.Chon)
                {
                    if (!DaoTaoHelper.IsExists(qdCongNhanDaoTao, item.ThongTinNhanVien))
                    {
                        qdCongNhanDaoTao.CreateListChiTietCongNhanDaoTao(item);
                    }
                }
            }
            View.Refresh();
        }
    }
}
