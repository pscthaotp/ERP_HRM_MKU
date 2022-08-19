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
    public partial class DaoTao_LapQuyetDinhCongNhanDaoTaoController : ViewController
    {
        private IObjectSpace obs;      
        private QuyetDinhDaoTao quyetDinhDaoTao;
        private QuyetDinhCongNhanDaoTao quyetDinhCongNhanDaoTao;

        public DaoTao_LapQuyetDinhCongNhanDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
            //Common.DebugTrace("DaoTao_LapQuyetDinhDaoTaoController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            obs = Application.CreateObjectSpace();

            quyetDinhDaoTao = View.CurrentObject as QuyetDinhDaoTao;

            if (quyetDinhDaoTao != null)
            {
                using (ERP.Module.Extends.DialogUtil.AutoWait("Hệ thống đang xử lý. Vui lòng chờ!"))
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinhDaoTao=?", quyetDinhDaoTao.Oid);
                    quyetDinhCongNhanDaoTao = obs.FindObject<QuyetDinhCongNhanDaoTao>(filter);
                    if (quyetDinhCongNhanDaoTao == null)
                        quyetDinhCongNhanDaoTao = obs.CreateObject<QuyetDinhCongNhanDaoTao>();
                    quyetDinhCongNhanDaoTao.QuyetDinhDaoTao = obs.GetObjectByKey<QuyetDinhDaoTao>(quyetDinhDaoTao.Oid); ;

                    ChiTietQuyetDinhCongNhanDaoTao chiTiet;
                    foreach (ChiTietQuyetDinhDaoTao item in quyetDinhDaoTao.ListChiTietQuyetDinhDaoTao)
                    {
                        if (quyetDinhCongNhanDaoTao != null)
                        {
                            CriteriaOperator filter2 = CriteriaOperator.Parse("QuyetDinhCongNhanDaoTao = ? and ThongTinNhanVien= ? ", quyetDinhCongNhanDaoTao.Oid, item.ThongTinNhanVien.Oid);
                            chiTiet = obs.FindObject<ChiTietQuyetDinhCongNhanDaoTao>(filter2);
                            if (chiTiet == null)
                            {
                                chiTiet = obs.CreateObject<ChiTietQuyetDinhCongNhanDaoTao>();
                                chiTiet.QuyetDinhCongNhanDaoTao = quyetDinhCongNhanDaoTao;
                                chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                                chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                                chiTiet.DaNopVanBangChungChi = true;
                                chiTiet.KhongDat = false;
                            }
                        }
                    }
                }

                e.Context = TemplateContext.View;
                e.View = Application.CreateDetailView(obs, quyetDinhCongNhanDaoTao);
                obs.Committed += obs_Committed;
            }
        }

        void obs_Committed(object sender, EventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

        private void DaoTao_LapQuyetDinhCongNhanDaoTaoController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] =
                Common.IsWriteGranted<QuanLyDaoTao>() &&
                Common.IsWriteGranted<QuyetDinhDaoTao>() &&
                Common.IsWriteGranted<QuyetDinhCongNhanDaoTao>();
        }
    }
}
