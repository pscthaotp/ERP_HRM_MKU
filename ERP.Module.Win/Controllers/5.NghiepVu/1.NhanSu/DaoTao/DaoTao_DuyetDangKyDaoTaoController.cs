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
    public partial class DaoTao_DuyetDangKyDaoTaoController : ViewController
    {
        private IObjectSpace obs;
        private HoSo_ChonDanhSachNhanVien danhSach;
        private DangKyDaoTao dangky;
        private DuyetDangKyDaoTao duyet;
        private QuanLyDaoTao qlDaoTao;

        public DaoTao_DuyetDangKyDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
            //Common.DebugTrace("DaoTao_LapQuyetDinhDaoTaoController");
        }

        private void DaoTao_ChonNhanVienDuyetDaoTaoController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<QuanLyDaoTao>()
                && Common.IsWriteGranted<DangKyDaoTao>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            dangky = View.CurrentObject as DangKyDaoTao;
            if (dangky != null)
            {
                obs = Application.CreateObjectSpace();
                danhSach = obs.CreateObject<HoSo_ChonDanhSachNhanVien>();
                danhSach.ChuongTrinhDaoTao = obs.GetObjectByKey<ChuongTrinhDaoTao>(dangky.ChuongTrinhDaoTao.Oid);
                danhSach.QuocGia = obs.GetObjectByKey<QuocGia>(dangky.QuocGia.Oid);
                danhSach.TruongDaoTao = obs.GetObjectByKey<TruongDaoTao>(dangky.TruongDaoTao.Oid);
                danhSach.NguonKinhPhi = obs.GetObjectByKey<NguonKinhPhi>(dangky.NguonKinhPhi.Oid);
                danhSach.DuKienTuNgay = dangky.DuKienTuNgay;
                danhSach.DuKienDenNgay = dangky.DuKienDenNgay;                
                danhSach.TongChiPhiDuKien = dangky.TongChiPhiDuKien;

                DaoTao_ChonNhanVien nhanVien;
                CriteriaOperator filter = CriteriaOperator.Parse("DangKyDaoTao=?", dangky.Oid);
                duyet = obs.FindObject<DuyetDangKyDaoTao>(filter);
                foreach (var item in dangky.ListChiTietDangKyDaoTao)
                {
                    if (duyet == null || (duyet != null && !duyet.IsExists(item.ThongTinNhanVien)))
                    {
                        nhanVien = obs.CreateObject<DaoTao_ChonNhanVien>();
                        nhanVien.Chon = true;
                        nhanVien.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                        nhanVien.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                        danhSach.ListNhanVien.Add(nhanVien);
                    }                    
                }
                e.View = Application.CreateDetailView(obs, danhSach);
            }
            else
                Common.ShowWarningMessage("Chưa nhập dữ liệu cho mục Đăng ký đào tạo");
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {        
            ChiTietDuyetDangKyDaoTao chiTiet;           
           if (danhSach != null && dangky != null)
           {
               CriteriaOperator filter = CriteriaOperator.Parse("DangKyDaoTao=?", dangky.Oid);
                duyet = obs.FindObject<DuyetDangKyDaoTao>(filter);
                if (duyet == null)
                {
                    duyet = obs.CreateObject<DuyetDangKyDaoTao>();                   
                }
                qlDaoTao = obs.GetObjectByKey<QuanLyDaoTao>(dangky.QuanLyDaoTao.Oid);
                duyet.QuanLyDaoTao = qlDaoTao;                 
                duyet.DangKyDaoTao = obs.GetObjectByKey<DangKyDaoTao>(dangky.Oid);
                duyet.ChuongTrinhDaoTao = obs.GetObjectByKey<ChuongTrinhDaoTao>(dangky.ChuongTrinhDaoTao.Oid);
                duyet.QuocGia = obs.GetObjectByKey<QuocGia>(dangky.QuocGia.Oid);
                duyet.TruongDaoTao = obs.GetObjectByKey<TruongDaoTao>(dangky.TruongDaoTao.Oid);
                duyet.NguonKinhPhi = obs.GetObjectByKey<NguonKinhPhi>(dangky.NguonKinhPhi.Oid);
                duyet.DuKienTuNgay = dangky.DuKienTuNgay;
                duyet.DuKienDenNgay = dangky.DuKienDenNgay;
                duyet.TongChiPhiDuKien = dangky.TongChiPhiDuKien;
                duyet.GhiChu = dangky.GhiChu;

                foreach (DaoTao_ChonNhanVien ctItem in danhSach.ListNhanVien)
                {
                    if (ctItem.Chon && !duyet.IsExists(ctItem.ThongTinNhanVien))
                    {
                        chiTiet = obs.CreateObject<ChiTietDuyetDangKyDaoTao>();
                        chiTiet.DuyetDangKyDaoTao = duyet;
                        chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(ctItem.BoPhan.Oid);
                        chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(ctItem.ThongTinNhanVien.Oid);
                        chiTiet.GhiChu = ctItem.GhiChu;
                    }
                }
           }


            obs.CommitChanges();
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
          
            //qlDaoTao.Reload();
        }
    }
}
