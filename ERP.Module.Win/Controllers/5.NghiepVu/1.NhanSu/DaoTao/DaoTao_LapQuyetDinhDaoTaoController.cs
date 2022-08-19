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

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.DaoTao
{
    public partial class DaoTao_LapQuyetDinhDaoTaoController : ViewController
    {
        private IObjectSpace obs;
        private DuyetDangKyDaoTao duyet;
        private QuyetDinhDaoTao quyetDinh;

        public DaoTao_LapQuyetDinhDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
            //Common.DebugTrace("DaoTao_LapQuyetDinhDaoTaoController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            obs = Application.CreateObjectSpace();

            duyet = View.CurrentObject as DuyetDangKyDaoTao;

            if (duyet != null && duyet.DangKyDaoTao != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("DuyetDangKyDaoTao=?", duyet.Oid);
                quyetDinh = obs.FindObject<QuyetDinhDaoTao>(filter);
                if (quyetDinh == null)
                    quyetDinh = obs.CreateObject<QuyetDinhDaoTao>();
                quyetDinh.DuyetDangKyDaoTao = obs.GetObjectByKey<DuyetDangKyDaoTao>(duyet.Oid);
                quyetDinh.ChuongTrinhDaoTao = obs.GetObjectByKey<ChuongTrinhDaoTao>(duyet.ChuongTrinhDaoTao.Oid);
                quyetDinh.QuocGia = obs.GetObjectByKey<QuocGia>(duyet.QuocGia.Oid);
                quyetDinh.TruongDaoTao = obs.GetObjectByKey<TruongDaoTao>(duyet.TruongDaoTao.Oid);               
                quyetDinh.NguonKinhPhi = obs.GetObjectByKey<NguonKinhPhi>(duyet.NguonKinhPhi.Oid);
                quyetDinh.TuNgay = duyet.DangKyDaoTao.DuKienTuNgay;
                quyetDinh.DenNgay = duyet.DangKyDaoTao.DuKienDenNgay;
                quyetDinh.TongChiPhi = duyet.DangKyDaoTao.TongChiPhiDuKien;

                ChiTietQuyetDinhDaoTao chiTiet;
                foreach (ChiTietDuyetDangKyDaoTao item in duyet.ListChiTietDuyetDangKyDaoTao)
                {
                    if (quyetDinh != null)
                    {
                        CriteriaOperator filter2 = CriteriaOperator.Parse("QuyetDinhDaoTao = ? and ThongTinNhanVien= ? ", quyetDinh.Oid, item.ThongTinNhanVien.Oid);
                        chiTiet = obs.FindObject<ChiTietQuyetDinhDaoTao>(filter2);
                        if (chiTiet == null)
                        {
                            chiTiet = obs.CreateObject<ChiTietQuyetDinhDaoTao>();
                            chiTiet.QuyetDinhDaoTao = quyetDinh;
                            chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                            chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                        }              
                    }                    
                }

                e.Context = TemplateContext.View;
                e.View = Application.CreateDetailView(obs, quyetDinh);
                obs.Committed += obs_Committed;
            }
        }

        void obs_Committed(object sender, EventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

        private void DaoTao_LapQuyetDinhDaoTaoController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] =
                Common.IsWriteGranted<QuanLyDaoTao>() &&
                Common.IsWriteGranted<DuyetDangKyDaoTao>() &&
                Common.IsWriteGranted<QuyetDinhDaoTao>();
        }
    }
}
