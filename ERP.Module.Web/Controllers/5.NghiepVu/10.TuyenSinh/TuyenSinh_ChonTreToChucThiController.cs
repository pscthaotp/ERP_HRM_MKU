using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using DevExpress.Utils;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.NonPersistentObjects.TuyenSinh;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.HocSinh.Lops;
using DevExpress.Xpo;
using ERP.Module.DanhMuc.NhanSu;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_ChonTreToChucThiController : ViewController
    {
        private ToChucThi _toChucThi;
        ToChucThi_ChonTre _chonTre;
        IObjectSpace _obs;
        public TuyenSinh_ChonTreToChucThiController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _toChucThi = View.CurrentObject as ToChucThi;
            if (_toChucThi != null)
            {
                //
                _obs = Application.CreateObjectSpace();
                //
                _chonTre = _obs.CreateObject<ToChucThi_ChonTre>();
                DetailView view = Application.CreateDetailView(_obs, _chonTre);
                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //Session session = ((XPObjectSpace)_obs).Session;
            //var user = Common.SecuritySystemUser_GetCurrentUser(session);
            if (_toChucThi != null && _chonTre.DanhSachCho != null)
            {
                DanhSachHocSinhChoNhapHoc dsCho = View.ObjectSpace.GetObjectByKey<DanhSachHocSinhChoNhapHoc>(_chonTre.DanhSachCho.Oid);

                //1. Lấy thông tin hồ sơ trẻ
                _toChucThi.Ho = dsCho.Ho;
                _toChucThi.Ten = dsCho.Ten;
                if (dsCho.NgaySinh != DateTime.MinValue)
                    _toChucThi.NgaySinhTre = dsCho.NgaySinh;
                _toChucThi.QuocTich = dsCho.QuocTich;
                _toChucThi.DiaChi = dsCho.ThongTinKhachHang.DiaChiThuongTru != null ?
                    dsCho.ThongTinKhachHang.DiaChiThuongTru.FullDiaChi : string.Empty;
                _toChucThi.DiaChiThuongTru = dsCho.ThongTinKhachHang.DiaChiThuongTru;
                _toChucThi.GioiTinh = dsCho.GioiTinh.Value;
                _toChucThi.DanhSachTre = dsCho.DanhSachTre;
                _toChucThi.SecuritySystemUser = dsCho.SecuritySystemUser != null ? dsCho.SecuritySystemUser : _toChucThi.SecuritySystemUser;
                _toChucThi.QuocTich = dsCho.QuocTich != null ? dsCho.QuocTich : null;
                _toChucThi.ThongTinKhachHang = dsCho.ThongTinKhachHang;
                _toChucThi.DanhSachCho = dsCho;
                _toChucThi.MADANHSACHCHO = dsCho.MaDanhSachCho;
                _toChucThi.CongTy = dsCho.CongTy;
                _toChucThi.ID_HE = dsCho.ID_HE;
                _toChucThi.ID_KHOI = dsCho.ID_KHOI;
                _toChucThi.HeDaoTaoSIS = dsCho.HeDaoTaoSIS;
                _toChucThi.KhoiSIS = dsCho.KhoiSIS;
                if (dsCho.NgaySinh != DateTime.MinValue)
                    _toChucThi.NgaySinhTre = dsCho.NgaySinh;
                _toChucThi.NamHoc = dsCho.NamHocDuKien;
                dsCho.DaToChucThi = true;

            }
            //
            else if (_chonTre != null && _chonTre.DanhSachTre != null)
            {
                if (_toChucThi != null && !_toChucThi.DaDongLePhi)
                {
                    DanhSachTre dsTre = View.ObjectSpace.GetObjectByKey<DanhSachTre>(_chonTre.DanhSachTre.Oid);

                    //1. Lấy thông tin hồ sơ trẻ
                    _toChucThi.Ho = dsTre.Ho;
                    _toChucThi.Ten = dsTre.Ten;
                    _toChucThi.MaHocSinhCu = dsTre.MaHocSinh;
                    if (dsTre.NgaySinh != DateTime.MinValue)
                        _toChucThi.NgaySinhTre = dsTre.NgaySinh;
                    _toChucThi.CongTy = dsTre.CongTy;
                    _toChucThi.ID_HE = dsTre.ID_HE;
                    _toChucThi.ID_KHOI = dsTre.ID_KHOI;
                    _toChucThi.HeDaoTaoSIS = dsTre.HeDaoTaoSIS;
                    _toChucThi.KhoiSIS = dsTre.KhoiSIS;
                    //_toChucThi.ChuongTrinhHoc = dsTre.ChuongTrinhHoc;
                    //_toChucThi.Khoi = dsTre.Khoi;
                    _toChucThi.DiaChi = dsTre.ThongTinKhachHang.DiaChiThuongTru != null ? dsTre.ThongTinKhachHang.DiaChiThuongTru.FullDiaChi : string.Empty;
                    _toChucThi.DiaChiThuongTru = dsTre.ThongTinKhachHang.DiaChiThuongTru;
                    _toChucThi.GioiTinh = dsTre.GioiTinh;
                    _toChucThi.DanhSachTre = dsTre;
                    //_toChucThi.SecuritySystemUser = dsTre.SecuritySystemUser != null ? dsTre.SecuritySystemUser : _toChucThi.SecuritySystemUser;
                    _toChucThi.QuocTich = dsTre.QuocTich != null ? dsTre.QuocTich : dsTre.ThongTinKhachHang.QuocTich;
                    _toChucThi.ThongTinKhachHang = dsTre.ThongTinKhachHang;
                    _toChucThi.NamHoc = dsTre.NamHocDuKien;
                }
            }
        }

        private void TuyenSinh_ChonTreToChucThiController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<ToChucThi>();
        }
    }
}
