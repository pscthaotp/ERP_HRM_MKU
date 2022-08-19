using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Utils;
using ERP.Module.NghiepVu.NhanSu.DaoTao;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.DaoTao
{
    public partial class DaoTao_DuyetDaoTaoController : ViewController
    {
        public DaoTao_DuyetDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
            //HamDungChung.DebugTrace("DaoTao_DuyetDaoTaoController");
        }

        private void DaoTao_DuyetDaoTaoController_Activated(object sender, EventArgs e)
        {
            simpleAction.Active["TruyCap"] = false;//Common.IsWriteGranted<QuanLyDaoTao>()
               // && Common.IsCreateGranted<DuyetDangKyDaoTao>();
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý...", "Vui lòng chờ"))
            {
                DangKyDaoTao dangKy = View.CurrentObject as DangKyDaoTao;
                if (dangKy != null && dangKy.QuanLyDaoTao != null)
                {
                    IObjectSpace obs = Application.CreateObjectSpace();
                    QuanLyDaoTao quanLy = obs.GetObjectByKey<QuanLyDaoTao>(dangKy.QuanLyDaoTao.Oid);
                    DuyetDangKyDaoTao duyet = null;
                    ChiTietDuyetDangKyDaoTao chiTiet;
                    foreach (DangKyDaoTao item in View.SelectedObjects)
                    {                   
                        //if (!quanLy.(item.ChuongTrinhDaoTao))
                        //{
                        //    duyet = obs.CreateObject<DuyetDangKyDaoTao>();
                        //    duyet.QuanLyDaoTao = quanLy;
                        //    duyet.DangKyDaoTao = obs.GetObjectByKey<DangKyDaoTao>(dangKy.Oid);
                        //    duyet.ChuongTrinhDaoTao = obs.GetObjectByKey<ChuongTrinhDaoTao>(item.ChuongTrinhDaoTao.Oid);
                        //    duyet.QuocGia = obs.GetObjectByKey<QuocGia>(item.QuocGia.Oid);
                        //    duyet.TruongDaoTao = obs.GetObjectByKey<TruongDaoTao>(item.TruongDaoTao.Oid);
                        //    duyet.NguonKinhPhi = obs.GetObjectByKey<NguonKinhPhi>(item.NguonKinhPhi.Oid);
                        //    duyet.DuKienTuNgay = item.DuKienTuNgay;
                        //    duyet.DuKienDenNgay = item.DuKienDenNgay;
                        //    duyet.TongChiPhiDuKien = item.TongChiPhiDuKien;
                        //    duyet.GhiChu = item.GhiChu;
                        //}

                        foreach (ChiTietDangKyDaoTao ctItem in item.ListChiTietDangKyDaoTao)
                        {
                            if (!duyet.IsExists(ctItem.ThongTinNhanVien))
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
                }
            }
        }
    }
}
