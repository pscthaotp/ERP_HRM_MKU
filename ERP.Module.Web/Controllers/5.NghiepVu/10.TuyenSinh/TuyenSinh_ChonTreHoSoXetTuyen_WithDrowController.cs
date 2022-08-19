using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using DevExpress.Utils;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.NghiepVu.TuyenSinh_TP;
using ERP.Module.NonPersistentObjects.TuyenSinh;
using ERP.Module.NonPersistentObjects.TuyenSinh_TP;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.HocSinh.Lops;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.Xpo;
using ERP.Module.HeThong;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_ChonTreHoSoXetTuyen_WithDrowController : ViewController
    {
        private HoSoXetTuyen _hoSoXetTuyen;
        HoSoXetTuyen_ChonTreWithDrow _chonTre;
        IObjectSpace _obs;
        public TuyenSinh_ChonTreHoSoXetTuyen_WithDrowController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _hoSoXetTuyen = View.CurrentObject as HoSoXetTuyen;
            if (_hoSoXetTuyen != null)
            {
                //
                _hoSoXetTuyen.ToChucThi = null;
                _hoSoXetTuyen.MADUTHI = null;
                _hoSoXetTuyen.MaHocSinhCu = null;
                _hoSoXetTuyen.CongTy = null;
                _hoSoXetTuyen.QuocTich = null;
                _hoSoXetTuyen.NgaySinh = DateTime.MinValue;
                _hoSoXetTuyen.TruongDaHoc = null;
                _hoSoXetTuyen.LopDaHoc = null;
                _hoSoXetTuyen.NamHoc = null;
                //_hoSoXetTuyen.GiaDinhTreList.Reload();

                _obs = Application.CreateObjectSpace();
                //
                _chonTre = _obs.CreateObject<HoSoXetTuyen_ChonTreWithDrow>();
                DetailView view = Application.CreateDetailView(_obs, _chonTre);
                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;

            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Session session = ((XPObjectSpace)_obs).Session;
            var user = Common.SecuritySystemUser_GetCurrentUser(session);
            //
            if (_chonTre != null)
            {
                if (_hoSoXetTuyen != null)
                {
                    if (_chonTre.TaiNhapHoc != null)
                    {
                        ThongTinKhachHang kh = ((XPObjectSpace)View.ObjectSpace).Session.GetObjectByKey<ThongTinKhachHang>(_chonTre.TaiNhapHoc.ThongTinKhachHang != null ? _chonTre.TaiNhapHoc.ThongTinKhachHang.Oid : Guid.Empty);
                        DanhSachTre dsTre = ((XPObjectSpace)View.ObjectSpace).Session.GetObjectByKey<DanhSachTre>(_chonTre.TaiNhapHoc.DanhSachTre != null ? _chonTre.TaiNhapHoc.DanhSachTre.Oid : Guid.Empty);
                        CriteriaOperator filter;
                        //1. Lấy thông tin hồ sơ trẻ
                        _hoSoXetTuyen.MaHocSinhCu = _chonTre.TaiNhapHoc.MaQuanLy;
                        _hoSoXetTuyen.Ho = _chonTre.TaiNhapHoc.Ho;
                        _hoSoXetTuyen.Ten = _chonTre.TaiNhapHoc.Ten;

                        if (_chonTre.TaiNhapHoc.NgaySinh != DateTime.MinValue)
                            _hoSoXetTuyen.NgaySinh = _chonTre.TaiNhapHoc.NgaySinh;
                        _hoSoXetTuyen.GioiTinh = _chonTre.TaiNhapHoc.GioiTinh;
                        if (dsTre != null)
                        {
                            _hoSoXetTuyen.DanhSachTre = dsTre;
                            //_hoSoXetTuyen.MaHocSinhCu = dsTre.MaHocSinh;
                            _hoSoXetTuyen.ThongTinKhachHang = dsTre.ThongTinKhachHang;
                            _hoSoXetTuyen.QuocTich = dsTre.QuocTich != null ? dsTre.QuocTich : null;
                        }
                        else if (kh != null)
                        {

                            _hoSoXetTuyen.DiaChiThuongTru = kh != null ? kh.DiaChiThuongTru : null;
                            _hoSoXetTuyen.ThongTinKhachHang = kh;
                            //
                            if (kh.GioiTinh == Enum.NhanSu.GioiTinhEnum.Nam)
                            {
                                if (string.IsNullOrEmpty(kh.HoTen))
                                    kh.HoTen = _chonTre.TaiNhapHoc.HoTenCha;
                                if (string.IsNullOrEmpty(kh.Email))
                                    kh.Email = _chonTre.TaiNhapHoc.EmailCha;
                                if (string.IsNullOrEmpty(kh.DienThoaiDiDong))
                                    kh.DienThoaiDiDong = _chonTre.TaiNhapHoc.DienThoaiCha;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(kh.HoTen))
                                    kh.HoTen = _chonTre.TaiNhapHoc.HoTenMe;

                                if (string.IsNullOrEmpty(kh.Email))
                                    kh.Email = _chonTre.TaiNhapHoc.EmailMe;

                                if (string.IsNullOrEmpty(kh.DienThoaiDiDong))
                                    kh.DienThoaiDiDong = _chonTre.TaiNhapHoc.DienThoaiMe;
                            }

                            filter = CriteriaOperator.Parse("HoTen like ? and NgaySinh = ? and GioiTinh = ? and ThongTinKhachHang = ?", "%" + _chonTre.TaiNhapHoc.Ho + " " + _chonTre.TaiNhapHoc.Ten + "%", _chonTre.TaiNhapHoc.NgaySinh, _chonTre.TaiNhapHoc.GioiTinh, kh.Oid);
                            dsTre = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<DanhSachTre>(filter);
                            if (dsTre != null)
                            {
                                _hoSoXetTuyen.DanhSachTre = dsTre;
                                //_hoSoXetTuyen.MaHocSinhCu = dsTre.MaHocSinh;
                            }
                            else
                            {
                                dsTre = new DanhSachTre(((XPObjectSpace)View.ObjectSpace).Session);
                                dsTre.Ho = _chonTre.TaiNhapHoc.Ho;
                                dsTre.Ten = _chonTre.TaiNhapHoc.Ten;
                                dsTre.NgaySinh = _chonTre.TaiNhapHoc.NgaySinh;
                                dsTre.GioiTinh = _chonTre.TaiNhapHoc.GioiTinh;
                                dsTre.ThongTinKhachHang = kh;
                                dsTre.NamHocDuKien = Common.GetCurrentNamHoc(((XPObjectSpace)View.ObjectSpace).Session);
                                _hoSoXetTuyen.DanhSachTre = dsTre;
                            }
                        }
                        else
                        {
                            filter = CriteriaOperator.Parse("DienThoaiDiDong = ? Or DienThoaiDiDong = ?", _chonTre.TaiNhapHoc.DienThoaiCha, _chonTre.TaiNhapHoc.DienThoaiMe);
                            kh = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<ThongTinKhachHang>(filter);
                            if (kh == null)
                            {
                                if ((!string.IsNullOrEmpty(_chonTre.TaiNhapHoc.HoTenCha) || !string.IsNullOrEmpty(_chonTre.TaiNhapHoc.DienThoaiCha)) || (!string.IsNullOrEmpty(_chonTre.TaiNhapHoc.HoTenMe) || !string.IsNullOrEmpty(_chonTre.TaiNhapHoc.DienThoaiMe)))
                                {
                                    kh = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<ThongTinKhachHang>(filter);
                                    kh = new ThongTinKhachHang(((XPObjectSpace)View.ObjectSpace).Session);
                                    if (kh.GioiTinh == Enum.NhanSu.GioiTinhEnum.Nam)
                                    {
                                        kh.HoTen = _chonTre.TaiNhapHoc.HoTenCha;
                                        kh.DienThoaiDiDong = _chonTre.TaiNhapHoc.DienThoaiCha;
                                        kh.Email = _chonTre.TaiNhapHoc.EmailCha;
                                    }
                                    else
                                    {
                                        kh.HoTen = _chonTre.TaiNhapHoc.HoTenMe;
                                        kh.DienThoaiDiDong = _chonTre.TaiNhapHoc.DienThoaiMe;
                                        kh.Email = _chonTre.TaiNhapHoc.EmailMe;
                                    }

                                    kh.NamDuKien = Common.GetCurrentNamHoc(((XPObjectSpace)View.ObjectSpace).Session);
                                    kh.NamHoc = Common.GetCurrentNamHoc(((XPObjectSpace)View.ObjectSpace).Session);
                                    //
                                    _hoSoXetTuyen.ThongTinKhachHang = kh;
                                    //
                                    dsTre = new DanhSachTre(((XPObjectSpace)View.ObjectSpace).Session);
                                    dsTre.Ho = _chonTre.TaiNhapHoc.Ho;
                                    dsTre.Ten = _chonTre.TaiNhapHoc.Ten;
                                    dsTre.NgaySinh = _chonTre.TaiNhapHoc.NgaySinh;
                                    dsTre.GioiTinh = _chonTre.TaiNhapHoc.GioiTinh;
                                    dsTre.ThongTinKhachHang = kh;
                                    dsTre.NamHocDuKien = Common.GetCurrentNamHoc(((XPObjectSpace)View.ObjectSpace).Session);
                                    _hoSoXetTuyen.DanhSachTre = dsTre;
                                }
                            }
                            else
                            {
                                _hoSoXetTuyen.ThongTinKhachHang = kh;

                                if (kh.GioiTinh == Enum.NhanSu.GioiTinhEnum.Nam)
                                {
                                    if (string.IsNullOrEmpty(kh.HoTen))
                                        kh.HoTen = _chonTre.TaiNhapHoc.HoTenCha;
                                    if (string.IsNullOrEmpty(kh.Email))
                                        kh.Email = _chonTre.TaiNhapHoc.EmailCha;
                                    if (string.IsNullOrEmpty(kh.DienThoaiDiDong))
                                        kh.DienThoaiDiDong = _chonTre.TaiNhapHoc.DienThoaiCha;
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(kh.HoTen))
                                        kh.HoTen = _chonTre.TaiNhapHoc.HoTenMe;

                                    if (string.IsNullOrEmpty(kh.Email))
                                        kh.Email = _chonTre.TaiNhapHoc.EmailMe;

                                    if (string.IsNullOrEmpty(kh.DienThoaiDiDong))
                                        kh.DienThoaiDiDong = _chonTre.TaiNhapHoc.DienThoaiMe;
                                }
                                //
                                filter = CriteriaOperator.Parse("HoTen like ? and NgaySinh = ? and GioiTinh = ? and ThongTinKhachHang = ?", "%" + _chonTre.TaiNhapHoc.Ho + " " + _chonTre.TaiNhapHoc.Ten + "%", _chonTre.TaiNhapHoc.NgaySinh, _chonTre.TaiNhapHoc.GioiTinh, kh.Oid);
                                dsTre = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<DanhSachTre>(filter);
                                if (dsTre != null)
                                {
                                    _hoSoXetTuyen.DanhSachTre = dsTre;
                                    //_hoSoXetTuyen.MaHocSinhCu = dsTre.MaHocSinh;
                                }
                                // Chưa có tạo mới
                                else
                                {
                                    dsTre = new DanhSachTre(((XPObjectSpace)View.ObjectSpace).Session);
                                    dsTre.Ho = _chonTre.TaiNhapHoc.Ho;
                                    dsTre.Ten = _chonTre.TaiNhapHoc.Ten;
                                    dsTre.NgaySinh = _chonTre.TaiNhapHoc.NgaySinh;
                                    dsTre.GioiTinh = _chonTre.TaiNhapHoc.GioiTinh;
                                    dsTre.ThongTinKhachHang = kh;
                                    dsTre.NamHocDuKien = Common.GetCurrentNamHoc(((XPObjectSpace)View.ObjectSpace).Session);
                                    _hoSoXetTuyen.DanhSachTre = dsTre;
                                }

                            }
                        }

                        //
                        //_hoSoXetTuyen.ChoseStudent = true;
                        //_hoSoXetTuyen.GiaDinhTreList.Reload();
                        //2. Lấy thông tin gia đình trẻ
                        if (kh != null)
                        {
                            //if (kh.HoTen != null)
                            //{
                            //    //Thêm thông tin cha
                            //    GiaDinhTre giaDinhTre = new GiaDinhTre(((XPObjectSpace)View.ObjectSpace).Session);
                            //    giaDinhTre.ThongTinKhachHang = kh;
                            //    giaDinhTre.HoTen = kh.HoTen;
                            //    giaDinhTre.DienThoaiDiDong = kh.DienThoaiDiDong;
                            //    giaDinhTre.DiaChiThuongTru = kh.DiaChiThuongTru;
                            //    giaDinhTre.Email = kh.Email;
                            //    giaDinhTre.GioiTinh = kh.GioiTinh;
                            //    giaDinhTre.QuocGia = kh.QuocTich;
                            //    giaDinhTre.CMND = kh.CMND;
                            //    giaDinhTre.QuanHe = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe = ?", "Cha"));
                            //    //
                            //    _hoSoXetTuyen.GiaDinhTreList.Add(giaDinhTre);
                            //}
                            //if (kh.HoTenMe != null)
                            //{
                            //    //Thêm thông tin mẹ
                            //    GiaDinhTre thongTinMe = new GiaDinhTre(((XPObjectSpace)View.ObjectSpace).Session);
                            //    thongTinMe.ThongTinKhachHang = kh;
                            //    thongTinMe.HoTen = kh.HoTenMe;
                            //    thongTinMe.GioiTinh = Enum.NhanSu.GioiTinhEnum.Nu;
                            //    thongTinMe.Email = kh.EmailMe;
                            //    thongTinMe.DiaChiThuongTru = kh.DiaChiThuongTru;
                            //    thongTinMe.CMND = kh.CMNDMe;
                            //    thongTinMe.QuocGia = kh.QuocTichMe;
                            //    thongTinMe.DienThoaiDiDong = kh.SDTMe;
                            //    thongTinMe.QuanHe = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe = ?", "Mẹ"));
                            //    _hoSoXetTuyen.GiaDinhTreList.Add(thongTinMe);
                            //}
                            //if (kh.LienLacKhac != null)
                            //{
                            //    //Thêm thông tin liên lạc khác
                            //    GiaDinhTre thongTinKhac = new GiaDinhTre(((XPObjectSpace)View.ObjectSpace).Session);
                            //    thongTinKhac.ThongTinKhachHang = kh;
                            //    thongTinKhac.HoTen = kh.LienLacKhac;
                            //    thongTinKhac.DienThoaiDiDong = kh.SDTLienLacKhac;
                            //    thongTinKhac.QuanHe = kh.MoiQuanHe;
                            //    _hoSoXetTuyen.GiaDinhTreList.Add(thongTinKhac);
                            //}
                            View.Refresh();
                        }
                    }
                }

            }
        }

        private void TuyenSinh_ChonTreHoSoXetTuyen_WithDrowController_Activated(object sender, EventArgs e)
        {
            bool active = false;
            active = Common.IsWriteGranted<HoSoXetTuyen>();
            //
            SecuritySystemUser_Custom user = Common.SecuritySystemUser_GetCurrentUser();
            var hoSo = View.CurrentObject as HoSoXetTuyen;
            if (hoSo != null)
                if (hoSo.DaDongHocPhi || user.CongTy.Oid.Equals(Config.KeyTanPhu))
                    popupWindowShowAction1.Active["TruyCap"] = false;
                else
                    popupWindowShowAction1.Active["TruyCap"] = true;
        }
    }
}
