using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.Commons;
using ERP.Module.Extends;
using ERP.Module.Enum.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.TuyenDung
{
    public partial class TuyenDung_LapQuyetDinhTuyenDungController : ViewController
    {
        public TuyenDung_LapQuyetDinhTuyenDungController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            //
            QuanLyTuyenDung qlTuyenDung = View.CurrentObject as QuanLyTuyenDung;
            if (qlTuyenDung != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                QuyetDinhTuyenDung quyetDinh = obs.FindObject<QuyetDinhTuyenDung>(CriteriaOperator.Parse("QuanLyTuyenDung=?", qlTuyenDung.Oid));
                if (quyetDinh == null)
                {
                    quyetDinh = obs.CreateObject<QuyetDinhTuyenDung>();
                    quyetDinh.QuanLyTuyenDung = obs.GetObjectByKey<QuanLyTuyenDung>(qlTuyenDung.Oid);
                    quyetDinh.CongTy = obs.GetObjectByKey<CongTy>(qlTuyenDung.CongTy.Oid);
                    quyetDinh.TenCongTy = qlTuyenDung.CongTy.TenBoPhan;
                }
                quyetDinh.QuyetDinhMoi = true;

                foreach (TrungTuyen item in qlTuyenDung.ListTrungTuyen)
                {
                    if (item.TrangThaiNhanViec == TrangThaiNhanViecEnum.NhanViec)
                    {                      
                        ChiTietQuyetDinhTuyenDung chiTiet = obs.FindObject<ChiTietQuyetDinhTuyenDung>(CriteriaOperator.Parse("QuyetDinhTuyenDung.Oid=? and ThongTinNhanVien.CMND=?", quyetDinh.Oid, item.UngVien.CMND));
                        if (chiTiet == null)
                        {
                            ThongTinNhanVien nhanVien = TuyenDungHelper.HoSoNhanVien(obs, item);
                            if (nhanVien != null)
                            {
                                chiTiet = obs.CreateObject<ChiTietQuyetDinhTuyenDung>();
                                chiTiet.BoPhan = nhanVien.BoPhan;
                                chiTiet.ThongTinNhanVien = nhanVien;
                                chiTiet.ThoiGianTapSu = item.ThoiGianThuViec;
                                chiTiet.UngVien = obs.GetObjectByKey<UngVien>(item.UngVien.Oid);
                                chiTiet.NgayVaoTruong = item.NgayNhanViec;
                                quyetDinh.ListChiTietQuyetDinhTuyenDung.Add(chiTiet);
                            }
                        }                                             
                    }
                    obs.CommitChanges();
                }

                Application.ShowView<QuyetDinhTuyenDung>(obs, quyetDinh);
            }
        }

        private void ChuyenHoSoTuyenDungAction_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active.SetItemValue("SecurityAllowance", Common.IsWriteGranted<QuanLyTuyenDung>() &&
                Common.IsWriteGranted<QuyetDinhTuyenDung>());
        }
    }
}
