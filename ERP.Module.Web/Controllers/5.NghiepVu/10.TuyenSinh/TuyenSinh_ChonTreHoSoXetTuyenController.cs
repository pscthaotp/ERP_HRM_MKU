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
using ERP.Module.NghiepVu.TuyenSinh_TP;
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
    public partial class TuyenSinh_ChonTreHoSoXetTuyenController : ViewController
    {
        private HoSoXetTuyen _hoSoXetTuyen;
        HoSoXetTuyen_ChonTre _chonTre;
        IObjectSpace _obs;
        public TuyenSinh_ChonTreHoSoXetTuyenController()
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
                //_hoSoXetTuyen.ChuongTrinhHoc = null;
                _hoSoXetTuyen.QuocTich = null;
                _hoSoXetTuyen.NgaySinh = DateTime.MinValue;
                _hoSoXetTuyen.TruongDaHoc = null;
                _hoSoXetTuyen.LopDaHoc = null;
                _hoSoXetTuyen.NamHoc = null;
                //_hoSoXetTuyen.GiaDinhTreList.Reload();

                _obs = Application.CreateObjectSpace();
                //
                _chonTre = _obs.CreateObject<HoSoXetTuyen_ChonTre>();
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
                    if (_chonTre.DaThi)
                    {
                        ToChucThi toChucThi = View.ObjectSpace.GetObjectByKey<ToChucThi>(_chonTre.ToChucThi.Oid);
                        if (toChucThi != null)
                        {
                            //1. Lấy thông tin hồ sơ trẻ
                            _hoSoXetTuyen.MADUTHI = toChucThi.MaHoSo;
                            _hoSoXetTuyen.MaHocSinhCu= toChucThi.MaHocSinhCu;
                            _hoSoXetTuyen.Ho = toChucThi.Ho;
                            _hoSoXetTuyen.Ten = toChucThi.Ten;
                       
                            if (toChucThi.NgaySinhTre != DateTime.MinValue)
                                _hoSoXetTuyen.NgaySinh = toChucThi.NgaySinhTre;
                            _hoSoXetTuyen.GioiTinh = toChucThi.GioiTinh.Value;
                            _hoSoXetTuyen.QuocTich = toChucThi.QuocTich;
                            _hoSoXetTuyen.DiaChiThuongTru = toChucThi.ThongTinKhachHang != null ? toChucThi.ThongTinKhachHang.DiaChiThuongTru : null;
                            _hoSoXetTuyen.CongTy = toChucThi.CongTy;
                            _hoSoXetTuyen.ID_HE = toChucThi.ID_HE;
                            _hoSoXetTuyen.ID_KHOI = toChucThi.ID_KHOI;
                            _hoSoXetTuyen.HeDaoTaoSIS = toChucThi.HeDaoTaoSIS;
                            _hoSoXetTuyen.KhoiSIS = toChucThi.KhoiSIS;
                            _hoSoXetTuyen.ToChucThi = toChucThi;
                            _hoSoXetTuyen.ThongTinKhachHang = toChucThi.ThongTinKhachHang;
                            //_hoSoXetTuyen.BoPhan = toChucThi.BoPhan;
                            _hoSoXetTuyen.DanhSachTre = toChucThi.DanhSachTre;
                            _hoSoXetTuyen.SecuritySystemUser = toChucThi.SecuritySystemUser != null ? toChucThi.SecuritySystemUser : _hoSoXetTuyen.SecuritySystemUser;
                            //
                            _hoSoXetTuyen.NamHoc = toChucThi.NamHoc;
                            if (toChucThi.DanhSachCho != null)
                            {
                                _hoSoXetTuyen.DanhSachCho = toChucThi.DanhSachCho;
                                _hoSoXetTuyen.MADANHSACHCHO = toChucThi.DanhSachCho.MaDanhSachCho;
                                toChucThi.DanhSachCho.DaLamHoSo = true;
                            }
                            _hoSoXetTuyen.ChoseStudent = true;
                            //_hoSoXetTuyen.GiaDinhTreList.Reload();
                            //2. Lấy thông tin gia đình trẻ
                            if (toChucThi.ThongTinKhachHang != null)
                            {
                                //if (toChucThi.ThongTinKhachHang.HoTen != null)
                                //{
                                //    //Thêm thông tin cha
                                //    GiaDinhTre giaDinhTre = new GiaDinhTre(((XPObjectSpace)View.ObjectSpace).Session);
                                //    giaDinhTre.ThongTinKhachHang = toChucThi.ThongTinKhachHang;
                                //    giaDinhTre.HoTen = toChucThi.ThongTinKhachHang.HoTen;
                                //    giaDinhTre.DienThoaiDiDong = toChucThi.ThongTinKhachHang.DienThoaiDiDong;
                                //    giaDinhTre.DiaChiThuongTru = toChucThi.ThongTinKhachHang.DiaChiThuongTru;
                                //    giaDinhTre.Email = toChucThi.ThongTinKhachHang.Email;
                                //    giaDinhTre.GioiTinh = toChucThi.ThongTinKhachHang.GioiTinh;
                                //    giaDinhTre.QuocGia = toChucThi.ThongTinKhachHang.QuocTich;
                                //    giaDinhTre.CMND = toChucThi.ThongTinKhachHang.CMND;
                                //    giaDinhTre.QuanHe = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe = ?", "Cha"));
                                //    //
                                //    _hoSoXetTuyen.GiaDinhTreList.Add(giaDinhTre);
                                //}
                                //if (toChucThi.ThongTinKhachHang.HoTenMe != null)
                                //{
                                //    //Thêm thông tin mẹ
                                //    GiaDinhTre thongTinMe = new GiaDinhTre(((XPObjectSpace)View.ObjectSpace).Session);
                                //    thongTinMe.ThongTinKhachHang = toChucThi.ThongTinKhachHang;
                                //    thongTinMe.HoTen = toChucThi.ThongTinKhachHang.HoTenMe;
                                //    thongTinMe.GioiTinh = Enum.NhanSu.GioiTinhEnum.Nu;
                                //    thongTinMe.Email = toChucThi.ThongTinKhachHang.EmailMe;
                                //    thongTinMe.DiaChiThuongTru = toChucThi.ThongTinKhachHang.DiaChiThuongTru;
                                //    thongTinMe.CMND = toChucThi.ThongTinKhachHang.CMNDMe;
                                //    thongTinMe.QuocGia = toChucThi.ThongTinKhachHang.QuocTichMe;
                                //    thongTinMe.DienThoaiDiDong = toChucThi.ThongTinKhachHang.SDTMe;
                                //    thongTinMe.QuanHe = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe = ?", "Mẹ"));
                                //    _hoSoXetTuyen.GiaDinhTreList.Add(thongTinMe);
                                //}

                                //if (toChucThi.ThongTinKhachHang.LienLacKhac != null)
                                //{
                                //    //Thêm thông tin liên lạc khác
                                //    GiaDinhTre thongTinKhac = new GiaDinhTre(((XPObjectSpace)View.ObjectSpace).Session);
                                //    thongTinKhac.ThongTinKhachHang = toChucThi.ThongTinKhachHang;
                                //    thongTinKhac.HoTen = toChucThi.ThongTinKhachHang.LienLacKhac;
                                //    thongTinKhac.DienThoaiDiDong = toChucThi.ThongTinKhachHang.SDTLienLacKhac;
                                //    thongTinKhac.QuanHe = toChucThi.ThongTinKhachHang.MoiQuanHe;
                                //    _hoSoXetTuyen.GiaDinhTreList.Add(thongTinKhac);
                                //}
                                View.Refresh();
                            }
                        }
                    }

                    else if (_chonTre.DaCoTrongDanhSachCho)
                    {
                        DanhSachHocSinhChoNhapHoc danhSachCho = View.ObjectSpace.GetObjectByKey<DanhSachHocSinhChoNhapHoc>(_chonTre.DanhSachCho.Oid);
                        if (danhSachCho != null)
                        {
                            //1. Lấy thông tin hồ sơ trẻ
                            _hoSoXetTuyen.MADANHSACHCHO = danhSachCho.MaDanhSachCho;
                            _hoSoXetTuyen.Ho = danhSachCho.Ho;
                            _hoSoXetTuyen.Ten = danhSachCho.Ten;
                            _hoSoXetTuyen.NgaySinh = danhSachCho.NgaySinh;
                            _hoSoXetTuyen.GioiTinh = danhSachCho.GioiTinh.Value;
                     
                            _hoSoXetTuyen.QuocTich = danhSachCho.QuocTich;
                            _hoSoXetTuyen.DiaChiThuongTru = danhSachCho.ThongTinKhachHang != null ? danhSachCho.ThongTinKhachHang.DiaChiThuongTru : null;
                            _hoSoXetTuyen.CongTy = danhSachCho.CongTy;
                            _hoSoXetTuyen.ID_HE = danhSachCho.ID_HE;
                            _hoSoXetTuyen.ID_KHOI = danhSachCho.ID_KHOI;
                            _hoSoXetTuyen.HeDaoTaoSIS = danhSachCho.HeDaoTaoSIS;
                            _hoSoXetTuyen.KhoiSIS = danhSachCho.KhoiSIS;
                            _hoSoXetTuyen.DanhSachCho = danhSachCho;
                            _hoSoXetTuyen.ThongTinKhachHang = danhSachCho.ThongTinKhachHang;
                            //_hoSoXetTuyen.BoPhan = danhSachCho.BoPhan;
                            _hoSoXetTuyen.DanhSachTre = danhSachCho.DanhSachTre;
                            _hoSoXetTuyen.SecuritySystemUser = danhSachCho.SecuritySystemUser != null ? danhSachCho.SecuritySystemUser : _hoSoXetTuyen.SecuritySystemUser;
                            _hoSoXetTuyen.NamHoc = danhSachCho.NamHocDuKien;
                            //_hoSoXetTuyen.GiaDinhTreList.Reload();
                            //2. Lấy thông tin gia đình trẻ
                            if (danhSachCho.ThongTinKhachHang != null)
                            {
                                //if (danhSachCho.ThongTinKhachHang.HoTen != null)
                                //{
                                //    //Thêm thông tin cha
                                //    GiaDinhTre giaDinhTre = new GiaDinhTre(((XPObjectSpace)View.ObjectSpace).Session);
                                //    giaDinhTre.ThongTinKhachHang = danhSachCho.ThongTinKhachHang;
                                //    giaDinhTre.HoTen = danhSachCho.ThongTinKhachHang.HoTen;
                                //    giaDinhTre.DienThoaiDiDong = danhSachCho.ThongTinKhachHang.DienThoaiDiDong;
                                //    giaDinhTre.DiaChiThuongTru = danhSachCho.ThongTinKhachHang.DiaChiThuongTru;
                                //    giaDinhTre.Email = danhSachCho.ThongTinKhachHang.Email;
                                //    giaDinhTre.GioiTinh = danhSachCho.ThongTinKhachHang.GioiTinh;
                                //    giaDinhTre.QuocGia = danhSachCho.ThongTinKhachHang.QuocTich;
                                //    giaDinhTre.CMND = danhSachCho.ThongTinKhachHang.CMND;
                                //    giaDinhTre.QuanHe = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe = ?", "Cha"));
                                //    //
                                //    _hoSoXetTuyen.GiaDinhTreList.Add(giaDinhTre);
                                //}
                                //if (danhSachCho.ThongTinKhachHang.HoTenMe != null)
                                //{
                                //    //Thêm thông tin mẹ
                                //    GiaDinhTre thongTinMe = new GiaDinhTre(((XPObjectSpace)View.ObjectSpace).Session);
                                //    thongTinMe.ThongTinKhachHang = danhSachCho.ThongTinKhachHang;
                                //    thongTinMe.HoTen = danhSachCho.ThongTinKhachHang.HoTenMe;
                                //    thongTinMe.GioiTinh = Enum.NhanSu.GioiTinhEnum.Nu;
                                //    thongTinMe.Email = danhSachCho.ThongTinKhachHang.EmailMe;
                                //    thongTinMe.DiaChiThuongTru = danhSachCho.ThongTinKhachHang.DiaChiThuongTru;
                                //    thongTinMe.CMND = danhSachCho.ThongTinKhachHang.CMNDMe;
                                //    thongTinMe.QuocGia = danhSachCho.ThongTinKhachHang.QuocTichMe;
                                //    thongTinMe.DienThoaiDiDong = danhSachCho.ThongTinKhachHang.SDTMe;
                                //    thongTinMe.QuanHe = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe = ?", "Mẹ"));
                                //    _hoSoXetTuyen.GiaDinhTreList.Add(thongTinMe);
                                //}

                                //if (danhSachCho.ThongTinKhachHang.LienLacKhac != null)
                                //{
                                //    //Thêm thông tin liên lạc khác
                                //    GiaDinhTre thongTinKhac = new GiaDinhTre(((XPObjectSpace)View.ObjectSpace).Session);
                                //    thongTinKhac.ThongTinKhachHang = danhSachCho.ThongTinKhachHang;
                                //    thongTinKhac.HoTen = danhSachCho.ThongTinKhachHang.LienLacKhac;
                                //    thongTinKhac.DienThoaiDiDong = danhSachCho.ThongTinKhachHang.SDTLienLacKhac;
                                //    thongTinKhac.QuanHe = danhSachCho.ThongTinKhachHang.MoiQuanHe;
                                //    _hoSoXetTuyen.GiaDinhTreList.Add(thongTinKhac);
                                //}
                                View.Refresh();

                            }
                        }
                    }

                    else
                    {
                        DanhSachTre dsTre = View.ObjectSpace.GetObjectByKey<DanhSachTre>(_chonTre.DanhSachTre.Oid);

                        //1. Lấy thông tin hồ sơ trẻ
                        _hoSoXetTuyen.DanhSachTre = dsTre;
                        _hoSoXetTuyen.Ho = dsTre.Ho;
                        _hoSoXetTuyen.Ten = dsTre.Ten;
                        _hoSoXetTuyen.MaHocSinhCu = dsTre.MaHocSinh;
                        _hoSoXetTuyen.QuocTich = dsTre.QuocTich;
                        if (dsTre.NgaySinh != DateTime.MinValue)
                            _hoSoXetTuyen.NgaySinh = dsTre.NgaySinh;
                        _hoSoXetTuyen.GioiTinh = dsTre.GioiTinh;
                        _hoSoXetTuyen.TruongDaHoc = dsTre.TruongDaHoc;
                        _hoSoXetTuyen.CongTy = dsTre.CongTy;
                        _hoSoXetTuyen.ID_HE = dsTre.ID_HE;
                        _hoSoXetTuyen.ID_KHOI = dsTre.ID_KHOI;
                        _hoSoXetTuyen.HeDaoTaoSIS = dsTre.HeDaoTaoSIS;
                        _hoSoXetTuyen.KhoiSIS = dsTre.KhoiSIS;
                        _hoSoXetTuyen.DiaChiThuongTru = dsTre.ThongTinKhachHang != null ? dsTre.ThongTinKhachHang.DiaChiThuongTru : null;
                        //_hoSoXetTuyen.BoPhan = Common.SecuritySystemUser_GetCurrentUser(((XPObjectSpace)View.ObjectSpace).Session).BoPhan;
                        _hoSoXetTuyen.LopDaHoc = dsTre.LopDaHoc;
                        //_hoSoXetTuyen.SecuritySystemUser = dsTre.SecuritySystemUser != null ? dsTre.SecuritySystemUser : _hoSoXetTuyen.SecuritySystemUser;
                        _hoSoXetTuyen.ThongTinKhachHang = dsTre.ThongTinKhachHang;
                        _hoSoXetTuyen.DuocMienThiTest = true;
                        //
                        _hoSoXetTuyen.NamHoc = dsTre.NamHocDuKien;
                        //_hoSoXetTuyen.GiaDinhTreList.Reload();

                        //2. Lấy thông tin gia đình trẻ
                        if (dsTre.ThongTinKhachHang != null)
                        {
                            //if (dsTre.ThongTinKhachHang.HoTen != null)
                            //{
                            //    //Thêm thông tin cha
                            //    GiaDinhTre giaDinhTre = new GiaDinhTre(((XPObjectSpace)View.ObjectSpace).Session);
                            //    giaDinhTre.ThongTinKhachHang = dsTre.ThongTinKhachHang;
                            //    giaDinhTre.HoTen = dsTre.ThongTinKhachHang.HoTen;
                            //    giaDinhTre.DienThoaiDiDong = dsTre.ThongTinKhachHang.DienThoaiDiDong;
                            //    giaDinhTre.Email = dsTre.ThongTinKhachHang.Email;
                            //    giaDinhTre.DiaChiThuongTru = dsTre.ThongTinKhachHang.DiaChiThuongTru;
                            //    giaDinhTre.GioiTinh = dsTre.ThongTinKhachHang.GioiTinh;
                            //    giaDinhTre.QuanHe = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe = ?", "Cha"));
                            //    giaDinhTre.CMND = dsTre.ThongTinKhachHang.CMND;
                            //    //
                            //    _hoSoXetTuyen.GiaDinhTreList.Add(giaDinhTre);
                            //}
                            //if (dsTre.ThongTinKhachHang.HoTenMe != null)
                            //{ //Thêm thông tin mẹ
                            //    GiaDinhTre thongTinMe = new GiaDinhTre(((XPObjectSpace)View.ObjectSpace).Session);
                            //    thongTinMe.ThongTinKhachHang = dsTre.ThongTinKhachHang;
                            //    thongTinMe.HoTen = dsTre.ThongTinKhachHang.HoTenMe;
                            //    thongTinMe.GioiTinh = Enum.NhanSu.GioiTinhEnum.Nu;
                            //    thongTinMe.DienThoaiDiDong = dsTre.ThongTinKhachHang.SDTMe;
                            //    thongTinMe.QuocGia = dsTre.ThongTinKhachHang.QuocTichMe;
                            //    thongTinMe.Email = dsTre.ThongTinKhachHang.EmailMe;
                            //    thongTinMe.DiaChiThuongTru = dsTre.ThongTinKhachHang.DiaChiThuongTru;
                            //    thongTinMe.CMND = dsTre.ThongTinKhachHang.CMNDMe;
                            //    thongTinMe.QuanHe = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe = ?", "Mẹ"));
                            //    _hoSoXetTuyen.GiaDinhTreList.Add(thongTinMe);
                            //}

                            //if (dsTre.ThongTinKhachHang.LienLacKhac != null)
                            //{ //Thêm thông tin liên lạc khác
                            //    GiaDinhTre thongTinKhac = new GiaDinhTre(((XPObjectSpace)View.ObjectSpace).Session);
                            //    thongTinKhac.ThongTinKhachHang = dsTre.ThongTinKhachHang;
                            //    thongTinKhac.HoTen = dsTre.ThongTinKhachHang.LienLacKhac;
                            //    thongTinKhac.DienThoaiDiDong = dsTre.ThongTinKhachHang.SDTLienLacKhac;
                            //    thongTinKhac.QuanHe = dsTre.ThongTinKhachHang.MoiQuanHe;
                            //    _hoSoXetTuyen.GiaDinhTreList.Add(thongTinKhac);
                            //}
                            View.Refresh();
                        }
                    }
                }
            }
        }

        private void TuyenSinh_ChonTreHoSoXetTuyenController_Activated(object sender, EventArgs e)
        {
            bool active = false;
            active = Common.IsWriteGranted<HoSoXetTuyen>();
            //
            SecuritySystemUser_Custom user = Common.SecuritySystemUser_GetCurrentUser();
            var hoSo = View.CurrentObject as HoSoXetTuyen;
            if (hoSo != null)
            {
                if (hoSo.DaDongHocPhi || user.CongTy.Oid.Equals(Config.KeyTanPhu))
                    popupWindowShowAction1.Active["TruyCap"] = false;
                else
                    popupWindowShowAction1.Active["TruyCap"] = true;
            }
        }
    }
}
