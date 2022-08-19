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
using ERP.Module.NghiepVu.HocSinh.HoSoNhapHocs;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.Xpo;
using ERP.Module.NonPersistentObjects.TuyenSinh_TP;
using ERP.Module.NghiepVu.TuyenSinh_TP;
using DevExpress.Web;
using ERP.Module.Enum.NhanSu;
using ERP.Module.HeThong;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_ChonHocSinh_TP_Controller : ViewController
    {

        private HoSoXetTuyen _hoSoXetTuyen;
        TuyenSinhPT_ChonHocSinh_XetTuyen _chonHocSinh;
        IObjectSpace _obs;
        public TuyenSinh_ChonHocSinh_TP_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            #region 1. Hồ sơ xét tuyển
            if (View.Id.Equals("HoSoXetTuyen_DetailView"))
            {
                _hoSoXetTuyen = View.CurrentObject as HoSoXetTuyen;
                if (_hoSoXetTuyen != null)
                {
                    {
                        _obs = Application.CreateObjectSpace();
                        //
                        _chonHocSinh = _obs.CreateObject<TuyenSinhPT_ChonHocSinh_XetTuyen>();
                        _chonHocSinh.NamHoc = _hoSoXetTuyen.NamHoc.Oid;
                        DetailView view = Application.CreateDetailView(_obs, _chonHocSinh);
                        view.ViewEditMode = ViewEditMode.Edit;
                        e.View = view;
                    }
                }
            }
            #endregion
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            #region 1. Hồ sơ xét tuyển
            if (View.Id.Equals("HoSoXetTuyen_DetailView"))
            {
                //
                if (_chonHocSinh != null)
                {
                    if (!_chonHocSinh.HocSinhCu)
                    {
                        DanhSachTre dstre = View.ObjectSpace.GetObjectByKey<DanhSachTre>(_chonHocSinh.DanhSachTre.Oid);
                        if (dstre != null)
                        {
                            //1. Lấy thông tin hồ sơ trẻ
                            _hoSoXetTuyen.Ho = dstre.Ho;
                            _hoSoXetTuyen.Ten = dstre.Ten;
                            _hoSoXetTuyen.NgaySinh = dstre.NgaySinh;
                            _hoSoXetTuyen.KhoiSIS = dstre.KhoiSIS;
                            _hoSoXetTuyen.ID_KHOI = dstre.ID_KHOI;
                            //_hoSoXetTuyen.CongTy = dstre.CongTy;
                            _hoSoXetTuyen.GioiTinh = dstre.GioiTinh;
                            _hoSoXetTuyen.TruongDaHoc = dstre.TruongDaHoc;
                            _hoSoXetTuyen.TruongSIS = dstre.TruongSIS;
                            _hoSoXetTuyen.ID_TruongSIS = dstre.ID_TruongSIS;
                            _hoSoXetTuyen.NoiSinh = dstre.NoiSinh;
                            _hoSoXetTuyen.DanhSachTre = dstre;

                            //2. Lấy thông tin khách hàng
                            if (dstre.ThongTinKhachHang != null)
                            {
                                _hoSoXetTuyen.ThongTinKhachHang = dstre.ThongTinKhachHang;
                                _hoSoXetTuyen.QuocTich = dstre.ThongTinKhachHang.QuocTich;
                                _hoSoXetTuyen.DiaChiThuongTru = dstre.ThongTinKhachHang.DiaChiThuongTru;
                                _hoSoXetTuyen.NguonThuThap = dstre.ThongTinKhachHang.NguonThuThap;
                            }
                        }
                    }
                    else
                    {
                        ThongTinKhachHang kh = ((XPObjectSpace)View.ObjectSpace).Session.GetObjectByKey<ThongTinKhachHang>(_chonHocSinh.TaiNhapHoc.ThongTinKhachHang != null ? _chonHocSinh.TaiNhapHoc.ThongTinKhachHang.Oid : Guid.Empty);
                        DanhSachTre dsTre = ((XPObjectSpace)View.ObjectSpace).Session.GetObjectByKey<DanhSachTre>(_chonHocSinh.TaiNhapHoc.DanhSachTre != null ? _chonHocSinh.TaiNhapHoc.DanhSachTre.Oid : Guid.Empty);
                        CriteriaOperator filter;
                        //1. Lấy thông tin hồ sơ trẻ
                        _hoSoXetTuyen.MaHocSinhCu = _chonHocSinh.TaiNhapHoc.MaQuanLy;
                        _hoSoXetTuyen.Ho = _chonHocSinh.TaiNhapHoc.Ho;
                        _hoSoXetTuyen.Ten = _chonHocSinh.TaiNhapHoc.Ten;
                        _hoSoXetTuyen.QuocTich = _chonHocSinh.TaiNhapHoc.QuocGia != null ? _chonHocSinh.TaiNhapHoc.QuocGia : null;


                        if (_chonHocSinh.TaiNhapHoc.NgaySinh != DateTime.MinValue)
                            _hoSoXetTuyen.NgaySinh = _chonHocSinh.TaiNhapHoc.NgaySinh;
                        _hoSoXetTuyen.GioiTinh = _chonHocSinh.TaiNhapHoc.GioiTinh;
                        if (dsTre != null)
                        {
                            _hoSoXetTuyen.DanhSachTre = dsTre;
                            //_hoSoXetTuyen.MaHocSinhCu = dsTre.MaHocSinh;
                            _hoSoXetTuyen.ThongTinKhachHang = dsTre.ThongTinKhachHang;
                        }

                        else if (kh != null)
                        {

                            _hoSoXetTuyen.DiaChiThuongTru = kh != null ? kh.DiaChiThuongTru : null;
                            _hoSoXetTuyen.ThongTinKhachHang = kh;
                            //
                            if (kh.GioiTinh == Enum.NhanSu.GioiTinhEnum.Nam)
                            {
                                kh.HoTenLLK = _chonHocSinh.TaiNhapHoc.HoTenMe;
                                kh.EmailLLK = _chonHocSinh.TaiNhapHoc.EmailMe;
                                kh.SDTLLK = _chonHocSinh.TaiNhapHoc.DienThoaiMe;
                                QuanHe qh = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe = ? and LoaiQuanHe", "Mẹ", LoaiQuanHeEnum.GiaDinh));
                                if (qh != null)
                                    kh.QuanHeLLK = qh;
                            }

                            if (kh.GioiTinh == GioiTinhEnum.Nu)
                            {
                                kh.HoTenLLK = _chonHocSinh.TaiNhapHoc.HoTenCha;
                                kh.EmailLLK = _chonHocSinh.TaiNhapHoc.EmailCha;
                                kh.SDTLLK = _chonHocSinh.TaiNhapHoc.DienThoaiCha;
                                QuanHe qh = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe = ? or TenQuanHe = ? and LoaiQuanHe = ?", "Cha", "Ba", LoaiQuanHeEnum.GiaDinh));
                                if (qh != null)
                                    kh.QuanHeLLK = qh;
                            }
                            filter = CriteriaOperator.Parse("HoTen like ? and NgaySinh = ? and GioiTinh = ? and ThongTinKhachHang = ?", "%" + _chonHocSinh.TaiNhapHoc.Ho + " " + _chonHocSinh.TaiNhapHoc.Ten + "%", _chonHocSinh.TaiNhapHoc.NgaySinh, _chonHocSinh.TaiNhapHoc.GioiTinh, kh.Oid);
                            dsTre = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<DanhSachTre>(filter);
                            if (dsTre != null)
                            {
                                _hoSoXetTuyen.DanhSachTre = dsTre;
                            }
                            else
                            {
                                dsTre = new DanhSachTre(((XPObjectSpace)View.ObjectSpace).Session);
                                dsTre.Ho = _chonHocSinh.TaiNhapHoc.Ho;
                                dsTre.Ten = _chonHocSinh.TaiNhapHoc.Ten;
                                dsTre.NgaySinh = _chonHocSinh.TaiNhapHoc.NgaySinh;
                                dsTre.GioiTinh = _chonHocSinh.TaiNhapHoc.GioiTinh;
                                dsTre.ThongTinKhachHang = kh;
                                //dsTre.NamHocDuKien = Common.GetCurrentNamHoc(((XPObjectSpace)View.ObjectSpace).Session);
                                _hoSoXetTuyen.DanhSachTre = dsTre;
                            }
                        }
                        else
                        {
                            filter = CriteriaOperator.Parse("(DienThoaiDiDong = ? Or SDTLLK = ?) or (DienThoaiDiDong = ? or SDTLLK = ?)", _chonHocSinh.TaiNhapHoc.DienThoaiCha, _chonHocSinh.TaiNhapHoc.DienThoaiMe, _chonHocSinh.TaiNhapHoc.DienThoaiMe, _chonHocSinh.TaiNhapHoc.DienThoaiCha);
                            kh = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<ThongTinKhachHang>(filter);
                            if (kh == null)
                            {
                                if ((!string.IsNullOrEmpty(_chonHocSinh.TaiNhapHoc.HoTenCha) || !string.IsNullOrEmpty(_chonHocSinh.TaiNhapHoc.DienThoaiCha)) || (!string.IsNullOrEmpty(_chonHocSinh.TaiNhapHoc.HoTenMe) || !string.IsNullOrEmpty(_chonHocSinh.TaiNhapHoc.DienThoaiMe)))
                                {
                                    kh = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<ThongTinKhachHang>(filter);
                                    kh = new ThongTinKhachHang(((XPObjectSpace)View.ObjectSpace).Session);
                                    kh.HoTen = _chonHocSinh.TaiNhapHoc.HoTenCha;
                                    kh.HoTenLLK = _chonHocSinh.TaiNhapHoc.HoTenMe;
                                    kh.DienThoaiDiDong = _chonHocSinh.TaiNhapHoc.DienThoaiCha;

                                    kh.SDTLLK = _chonHocSinh.TaiNhapHoc.DienThoaiMe;
                                    kh.Email = _chonHocSinh.TaiNhapHoc.EmailCha;
                                    kh.EmailLLK = _chonHocSinh.TaiNhapHoc.EmailMe;
                                    kh.NamHoc = Common.GetCurrentNamHoc(((XPObjectSpace)View.ObjectSpace).Session);
                                    //  
                                    QuanHe qh = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe = ? and LoaiQuanHe = ?", "Mẹ", LoaiQuanHeEnum.GiaDinh));
                                    if (qh != null)
                                        kh.QuanHeLLK = qh;
                                    QuanHe qhc = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe = ? and LoaiQuanHe = ?", "Cha", LoaiQuanHeEnum.GiaDinh));
                                    if (qhc != null)
                                        kh.QuanHe = qhc;
                                    _hoSoXetTuyen.ThongTinKhachHang = kh;
                                    //
                                    dsTre = new DanhSachTre(((XPObjectSpace)View.ObjectSpace).Session);
                                    dsTre.Ho = _chonHocSinh.TaiNhapHoc.Ho;
                                    dsTre.Ten = _chonHocSinh.TaiNhapHoc.Ten;
                                    dsTre.NgaySinh = _chonHocSinh.TaiNhapHoc.NgaySinh;
                                    dsTre.GioiTinh = _chonHocSinh.TaiNhapHoc.GioiTinh;
                                    dsTre.ThongTinKhachHang = kh;
                                    _hoSoXetTuyen.DanhSachTre = dsTre;
                                }
                            }
                            else
                            {

                                _hoSoXetTuyen.ThongTinKhachHang = kh;
                                if (kh.GioiTinh == GioiTinhEnum.Nu)
                                {
                                    if (!kh.HoTen.Equals(_chonHocSinh.TaiNhapHoc.HoTenMe))
                                    {
                                        _hoSoXetTuyen.ThongBao = true;
                                        _hoSoXetTuyen.Message = "Thông tin của phụ huynh học sinh giữa học vụ và tuyển sinh không khớp nhau. Vui lòng kiểm tra lại thông tin khách hàng trước khi thực hiện làm hồ sơ.";
                                    }
                                }

                                if (kh.GioiTinh == GioiTinhEnum.Nam)
                                {
                                    if (!kh.HoTen.Equals(_chonHocSinh.TaiNhapHoc.HoTenCha))
                                    {
                                        _hoSoXetTuyen.ThongBao = true;
                                        _hoSoXetTuyen.Message = "Thông tin của phụ huynh học sinh giữa học vụ và tuyển sinh không khớp nhau. Vui lòng kiểm tra lại thông tin khách hàng trước khi thực hiện làm hồ sơ.";
                                    }
                                }
                                filter = CriteriaOperator.Parse("HoTen like ? and NgaySinh = ? and GioiTinh = ? and ThongTinKhachHang = ?", "%" + _chonHocSinh.TaiNhapHoc.Ho + " " + _chonHocSinh.TaiNhapHoc.Ten + "%", _chonHocSinh.TaiNhapHoc.NgaySinh, _chonHocSinh.TaiNhapHoc.GioiTinh, kh.Oid);
                                dsTre = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<DanhSachTre>(filter);
                                if (dsTre != null)
                                {
                                    _hoSoXetTuyen.DanhSachTre = dsTre;
                                    _hoSoXetTuyen.MaHocSinhCu = _chonHocSinh.TaiNhapHoc.MaQuanLy;
                                }
                                // Chưa có tạo mới
                                else
                                {
                                    dsTre = new DanhSachTre(((XPObjectSpace)View.ObjectSpace).Session);
                                    dsTre.Ho = _chonHocSinh.TaiNhapHoc.Ho;
                                    dsTre.Ten = _chonHocSinh.TaiNhapHoc.Ten;
                                    dsTre.NgaySinh = _chonHocSinh.TaiNhapHoc.NgaySinh;
                                    dsTre.GioiTinh = _chonHocSinh.TaiNhapHoc.GioiTinh;
                                    dsTre.ThongTinKhachHang = kh;
                                    _hoSoXetTuyen.DanhSachTre = dsTre;
                                }

                            }
                        }
                    }
                }
            }
            #endregion
        }
        private void TuyenSinh_ChonHocSinh_TP_Controller_Activated(object sender, EventArgs e)
        {
            if (View is DetailView)
            {
                #region DetailView
                SecuritySystemUser_Custom user = Common.SecuritySystemUser_GetCurrentUser();
                HoSoXetTuyen hs = View.CurrentObject as HoSoXetTuyen;
                if (View.Id.Equals("HoSoXetTuyen_DetailView") && user.CongTy.Oid.Equals(Config.KeyTanPhu))
                {
                    if (hs != null && hs.Create == true)
                        popupWindowShowAction1.Active["TruyCap"] = true;
                    else
                        popupWindowShowAction1.Active["TruyCap"] = false;
                }
                else
                {
                    popupWindowShowAction1.Active["TruyCap"] = false;
                }
                #endregion
            }
            else
            {
                #region ListView
                if (View.Id.Equals(""))
                {
                    popupWindowShowAction1.Active["TruyCap"] = true;
                }
                else
                {
                    popupWindowShowAction1.Active["TruyCap"] = false;
                }
                #endregion
            }

        }
    }
}
