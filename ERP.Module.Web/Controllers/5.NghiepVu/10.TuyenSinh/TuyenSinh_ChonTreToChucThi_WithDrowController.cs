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
using ERP.Module.Enum.NhanSu;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_ChonTreToChucThi_WithDrowController : ViewController
    {
        private ToChucThi _toChucThi;
        ToChucThi_ChonTreWithDrow _chonTre;
        IObjectSpace _obs;
        public TuyenSinh_ChonTreToChucThi_WithDrowController()
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
                _chonTre = _obs.CreateObject<ToChucThi_ChonTreWithDrow>();
                DetailView view = Application.CreateDetailView(_obs, _chonTre);
                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //Session session = ((XPObjectSpace)_obs).Session;
            //var user = Common.SecuritySystemUser_GetCurrentUser(session);
            if (_toChucThi != null && _chonTre.TaiNhapHoc != null)
            {
                ThongTinKhachHang kh = ((XPObjectSpace)View.ObjectSpace).Session.GetObjectByKey<ThongTinKhachHang>(_chonTre.TaiNhapHoc.ThongTinKhachHang != null ? _chonTre.TaiNhapHoc.ThongTinKhachHang.Oid : Guid.Empty);
                DanhSachTre dsTre = ((XPObjectSpace)View.ObjectSpace).Session.GetObjectByKey<DanhSachTre>(_chonTre.TaiNhapHoc.DanhSachTre != null ? _chonTre.TaiNhapHoc.DanhSachTre.Oid : Guid.Empty);

                //1. Lấy thông tin hồ sơ học sinh
                _toChucThi.MaHocSinhCu = _chonTre.TaiNhapHoc.MaQuanLy;
                _toChucThi.Ho = _chonTre.TaiNhapHoc.Ho;
                _toChucThi.Ten = _chonTre.TaiNhapHoc.Ten;
                _toChucThi.GioiTinh = _chonTre.TaiNhapHoc.GioiTinh;
                
                if (_chonTre.TaiNhapHoc.NgaySinh != DateTime.MinValue)
                    _toChucThi.NgaySinhTre = _chonTre.TaiNhapHoc.NgaySinh;
                // Thông tin học sinh đã có
                if (dsTre != null)
                {
                    _toChucThi.DanhSachTre = dsTre;
                    _toChucThi.MaHocSinhCu = dsTre.MaHocSinh;
                    _toChucThi.ThongTinKhachHang = dsTre.ThongTinKhachHang;
                    _toChucThi.QuocTich = dsTre.QuocTich != null ? dsTre.QuocTich : null;
                }
                // Thong tin phụ huynh đã có
                else if (kh != null)
                {
                    _toChucThi.DiaChi = kh.DiaChiThuongTru != null ?
                        kh.DiaChiThuongTru.FullDiaChi : string.Empty;
                    _toChucThi.DiaChiThuongTru = kh.DiaChiThuongTru != null ? kh.DiaChiThuongTru : null;
                    _toChucThi.ThongTinKhachHang = kh;

                    //
                    if (string.IsNullOrEmpty(kh.HoTen))
                        kh.HoTen = _chonTre.TaiNhapHoc.HoTenCha;
                    //if (string.IsNullOrEmpty(kh.HoTenMe))
                    //    kh.HoTenMe = _chonTre.TaiNhapHoc.HoTenMe;
                    if (string.IsNullOrEmpty(kh.Email))
                        kh.Email = _chonTre.TaiNhapHoc.EmailCha;
                    //if (string.IsNullOrEmpty(kh.EmailMe))
                    //    kh.EmailMe = _chonTre.TaiNhapHoc.EmailMe;
                    if (string.IsNullOrEmpty(kh.DienThoaiDiDong))
                        kh.DienThoaiDiDong = _chonTre.TaiNhapHoc.DienThoaiCha;
                    //if (string.IsNullOrEmpty(kh.SDTMe))
                    //    kh.SDTMe = _chonTre.TaiNhapHoc.DienThoaiMe;

                    // Kiểm tra thông tin học sinh

                    dsTre = new DanhSachTre(((XPObjectSpace)View.ObjectSpace).Session);
                    dsTre.Ho = _chonTre.TaiNhapHoc.Ho;
                    dsTre.Ten = _chonTre.TaiNhapHoc.Ten;
                    dsTre.NgaySinh = _chonTre.TaiNhapHoc.NgaySinh;
                    dsTre.GioiTinh = _chonTre.TaiNhapHoc.GioiTinh;
                    dsTre.ThongTinKhachHang = kh;
                }
                // Thông tin phụ huynh và học sinh điều không có
                else
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("DienThoaiDiDong = ? OR DienThoaiDiDong = ?", _chonTre.TaiNhapHoc.DienThoaiCha, _chonTre.TaiNhapHoc.DienThoaiMe);
                    //CriteriaOperator filter = CriteriaOperator.Parse("DienThoaiDiDong = ? Or SDTMe = ?", _chonTre.TaiNhapHoc.DienThoaiCha, _chonTre.TaiNhapHoc.DienThoaiMe);
                    kh = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<ThongTinKhachHang>(filter);
                    // nếu tìm thấy sdt của phụ huynh trong csdl
                    if (kh != null)
                    {
                        if (string.IsNullOrEmpty(kh.HoTen))
                            kh.HoTen = _chonTre.TaiNhapHoc.HoTenCha;
                        //if (string.IsNullOrEmpty(kh.HoTenMe))
                        //    kh.HoTenMe = _chonTre.TaiNhapHoc.HoTenMe;
                        if (string.IsNullOrEmpty(kh.Email))
                            kh.Email = _chonTre.TaiNhapHoc.EmailCha;
                        //if (string.IsNullOrEmpty(kh.EmailMe))
                        //    kh.EmailMe = _chonTre.TaiNhapHoc.EmailMe;
                        if (string.IsNullOrEmpty(kh.DienThoaiDiDong))
                            kh.DienThoaiDiDong = _chonTre.TaiNhapHoc.DienThoaiCha;
                        //if (string.IsNullOrEmpty(kh.SDTMe))
                        //    kh.SDTMe = _chonTre.TaiNhapHoc.DienThoaiMe;

                        _toChucThi.ThongTinKhachHang = kh;
                        filter = CriteriaOperator.Parse("HoTen like ? and NgaySinh = ? and GioiTinh = ? and ThongTinKhachHang = ?", "%" + _chonTre.TaiNhapHoc.Ho + " " + _chonTre.TaiNhapHoc.Ten + "%", _chonTre.TaiNhapHoc.NgaySinh, _chonTre.TaiNhapHoc.GioiTinh, kh.Oid);
                        dsTre = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<DanhSachTre>(filter);
                        if (dsTre != null)
                        {
                            _toChucThi.DanhSachTre = dsTre;
                            _toChucThi.MaHocSinhCu = dsTre.MaHocSinh;
                        }
                        // mà kh có thông tin học sinh
                        else
                        {
                            dsTre = new DanhSachTre(((XPObjectSpace)View.ObjectSpace).Session);
                            dsTre.Ho = _chonTre.TaiNhapHoc.Ho;
                            dsTre.Ten = _chonTre.TaiNhapHoc.Ten;
                            dsTre.NgaySinh = _chonTre.TaiNhapHoc.NgaySinh;
                            dsTre.GioiTinh = _chonTre.TaiNhapHoc.GioiTinh;
                            dsTre.ThongTinKhachHang = kh;
                            dsTre.NamHocDuKien = Common.GetCurrentNamHoc(((XPObjectSpace)View.ObjectSpace).Session);
                            _toChucThi.DanhSachTre = dsTre;
                        }
                    }
                    // Không tìm thấy sdt của phụ huynh trong csdl
                    // Kiểm tra thông tin từ SIS qua nếu có thông tin nào của phụ huynh thì tạo mới thông tin phụ huynh  mới
                    else if ((!string.IsNullOrEmpty(_chonTre.TaiNhapHoc.HoTenCha) || !string.IsNullOrEmpty(_chonTre.TaiNhapHoc.DienThoaiCha)) || (!string.IsNullOrEmpty(_chonTre.TaiNhapHoc.HoTenMe) || !string.IsNullOrEmpty(_chonTre.TaiNhapHoc.DienThoaiMe)))
                    {
                        kh = new ThongTinKhachHang(((XPObjectSpace)View.ObjectSpace).Session);
                        kh.HoTen = _chonTre.TaiNhapHoc.HoTenCha;
                        //kh.HoTenMe = _chonTre.TaiNhapHoc.HoTenMe;
                        kh.DienThoaiDiDong = _chonTre.TaiNhapHoc.DienThoaiCha;
                        //kh.SDTMe = _chonTre.TaiNhapHoc.DienThoaiMe;
                        kh.Email = _chonTre.TaiNhapHoc.EmailCha;
                        //kh.EmailMe = _chonTre.TaiNhapHoc.EmailMe;
                        kh.NamDuKien = Common.GetCurrentNamHoc(((XPObjectSpace)View.ObjectSpace).Session);
                        kh.NamHoc = Common.GetCurrentNamHoc(((XPObjectSpace)View.ObjectSpace).Session);
                        //
                        _toChucThi.ThongTinKhachHang = kh;
                        filter = CriteriaOperator.Parse("HoTen like ? and NgaySinh = ? and GioiTinh = ? and ThongTinKhachHang = ?", "%" + _chonTre.TaiNhapHoc.Ho + " " + _chonTre.TaiNhapHoc.Ten + "%", _chonTre.TaiNhapHoc.NgaySinh, _chonTre.TaiNhapHoc.GioiTinh, kh.Oid);
                        // Tìm thông tin học sinh từ thông tin đã có
                        dsTre = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<DanhSachTre>(filter);
                        // Nếu có
                        if (dsTre != null)
                        {
                            _toChucThi.DanhSachTre = dsTre;
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
                            _toChucThi.DanhSachTre = dsTre;
                        }

                    }
                }
            }
        }
        private void TuyenSinh_ChonTreToChucThi_WithDrowController_Activated(object sender, EventArgs e)
        {
            //
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<ToChucThi>();
        }
    }
}
