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
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_ChonTreController : ViewController
    {
        private HoSoNhapHoc _hoSoNhapHoc;
        private KiemTraIQ _kiemTraIQ;
        TuyenSinh_ChonTre _chonTre;
        TuyenSinh_ChonTre_KiemTraIQ _chonTreIQ;
        IObjectSpace _obs;
        public TuyenSinh_ChonTreController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            #region 1. Hồ sơ nhập học
            if (View.Id.Equals("HoSoNhapHoc_DetailView"))
            {
                _hoSoNhapHoc = View.CurrentObject as HoSoNhapHoc;
                if (_hoSoNhapHoc != null)
                {
                    if (_hoSoNhapHoc.NamHoc != null)
                    {
                        //
                        _obs = Application.CreateObjectSpace();
                        //
                        _chonTre = _obs.CreateObject<TuyenSinh_ChonTre>();
                        _chonTre.NamHoc = _hoSoNhapHoc.NamHoc.Oid;
                        DetailView view = Application.CreateDetailView(_obs, _chonTre);
                        view.ViewEditMode = ViewEditMode.Edit;
                        e.View = view;
                    }
                }
            }
            #endregion

            #region 2. Kiểm tra IQ

            if (View.Id.Equals("KiemTraIQ_DetailView"))
            {
                _kiemTraIQ = View.CurrentObject as KiemTraIQ;
                if (_kiemTraIQ != null)
                {
                    if (_kiemTraIQ.NamHoc != null)
                    {
                        //
                        _obs = Application.CreateObjectSpace();
                        //
                        _chonTreIQ = _obs.CreateObject<TuyenSinh_ChonTre_KiemTraIQ>();
                        _chonTreIQ.NamHoc = _kiemTraIQ.NamHoc.Oid;
                        DetailView view = Application.CreateDetailView(_obs, _chonTreIQ);
                        view.ViewEditMode = ViewEditMode.Edit;
                        e.View = view;
                    }
                }
            }
            #endregion

        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            #region 1. Hồ sơ nhập học
            if (View.Id.Equals("HoSoNhapHoc_DetailView"))
            //
            {
                if (_chonTre != null)
                {
                    if (_hoSoNhapHoc != null)
                    {
                        if (_chonTre.CheckIQ)
                        {
                            #region Kiểm tra IQ
                            KiemTraIQ kiemTra = View.ObjectSpace.GetObjectByKey<KiemTraIQ>(_chonTre.KiemTraIQ.Oid);
                            if (kiemTra != null)
                            {
                                //1. Lấy thông tin hồ sơ trẻ
                                _hoSoNhapHoc.Ho = kiemTra.Ho;
                                _hoSoNhapHoc.Ten = kiemTra.Ten;
                                _hoSoNhapHoc.NgaySinh = kiemTra.NgaySinhTre;
                                _hoSoNhapHoc.GioiTinh = kiemTra.GioiTinh;
                                _hoSoNhapHoc.OidStore = kiemTra.Oid;
                                if (kiemTra.DanhSachTre != null)
                                    _hoSoNhapHoc.TruongDaHoc = kiemTra.DanhSachTre.TruongDaHoc;

                                //2. Địa chỉ con
                                if (kiemTra.ThongTinKhachHang != null && kiemTra.ThongTinKhachHang.DiaChiTamTru != null)
                                {
                                    DiaChi diaChi = View.ObjectSpace.GetObjectByKey<DiaChi>(kiemTra.ThongTinKhachHang.DiaChiTamTru.Oid);
                                    _hoSoNhapHoc.DiaChiTamTru = diaChi;
                                }
                                if (kiemTra.ThongTinKhachHang != null && kiemTra.ThongTinKhachHang.DiaChiThuongTru != null)
                                {
                                    DiaChi diaChi = View.ObjectSpace.GetObjectByKey<DiaChi>(kiemTra.ThongTinKhachHang.DiaChiThuongTru.Oid);
                                    _hoSoNhapHoc.DiaChiThuongTru = diaChi;
                                    //DiaChi diaChi = new DiaChi(((XPObjectSpace)View.ObjectSpace).Session);
                                    //diaChi.QuocGia = View.ObjectSpace.GetObjectByKey<QuocGia>(kiemTra.ThongTinKhachHang.DiaChiThuongTru.QuocGia != null ? kiemTra.ThongTinKhachHang.DiaChiThuongTru.QuocGia.Oid : Guid.Empty);
                                    //diaChi.TinhThanh = View.ObjectSpace.GetObjectByKey<TinhThanh>(kiemTra.ThongTinKhachHang.DiaChiThuongTru.TinhThanh != null ? kiemTra.ThongTinKhachHang.DiaChiThuongTru.TinhThanh.Oid : Guid.Empty);
                                    //diaChi.QuanHuyen = View.ObjectSpace.GetObjectByKey<QuanHuyen>(kiemTra.ThongTinKhachHang.DiaChiThuongTru.QuanHuyen != null ? kiemTra.ThongTinKhachHang.DiaChiThuongTru.QuanHuyen.Oid : Guid.Empty);
                                    //diaChi.XaPhuong = View.ObjectSpace.GetObjectByKey<XaPhuong>(kiemTra.ThongTinKhachHang.DiaChiThuongTru.XaPhuong != null ? kiemTra.ThongTinKhachHang.DiaChiThuongTru.XaPhuong.Oid : Guid.Empty);
                                    //diaChi.SoNha = kiemTra.ThongTinKhachHang.DiaChiThuongTru.SoNha;
                                    //diaChi.FullDiaChi = kiemTra.ThongTinKhachHang.DiaChiThuongTru.FullDiaChi;
                                    ////
                                    //_hoSoNhapHoc.DiaChiThuongTru = diaChi;
                                }

                                //3. Lấy thông tin gia đình trẻ
                                if (kiemTra.ThongTinKhachHang != null)
                                {
                                    //
                                    GiaDinhTre giaDinhTre = new GiaDinhTre(((XPObjectSpace)View.ObjectSpace).Session);
                                    giaDinhTre.ThongTinKhachHang = View.ObjectSpace.GetObjectByKey<ThongTinKhachHang>(kiemTra.ThongTinKhachHang.Oid);
                                    giaDinhTre.HoTen = kiemTra.ThongTinKhachHang.HoTen;
                                    giaDinhTre.DienThoaiDiDong = kiemTra.ThongTinKhachHang.DienThoaiDiDong;
                                    giaDinhTre.Email = kiemTra.ThongTinKhachHang.Email;
                                    giaDinhTre.GioiTinh = kiemTra.ThongTinKhachHang.GioiTinh;
                                    giaDinhTre.CMND = kiemTra.ThongTinKhachHang.CMND;

                                    // Địa chỉ cha
                                    if (kiemTra.ThongTinKhachHang.DiaChiThuongTru != null)
                                    {
                                        //DiaChi diaChi = new DiaChi(((XPObjectSpace)View.ObjectSpace).Session);
                                        //diaChi.QuocGia = View.ObjectSpace.GetObjectByKey<QuocGia>(kiemTra.ThongTinKhachHang.DiaChiThuongTru.QuocGia != null ? kiemTra.ThongTinKhachHang.DiaChiThuongTru.QuocGia.Oid : Guid.Empty);
                                        //diaChi.TinhThanh = View.ObjectSpace.GetObjectByKey<TinhThanh>(kiemTra.ThongTinKhachHang.DiaChiThuongTru.TinhThanh != null ? kiemTra.ThongTinKhachHang.DiaChiThuongTru.TinhThanh.Oid : Guid.Empty);
                                        //diaChi.QuanHuyen = View.ObjectSpace.GetObjectByKey<QuanHuyen>(kiemTra.ThongTinKhachHang.DiaChiThuongTru.QuanHuyen != null ? kiemTra.ThongTinKhachHang.DiaChiThuongTru.QuanHuyen.Oid : Guid.Empty);
                                        //diaChi.XaPhuong = View.ObjectSpace.GetObjectByKey<XaPhuong>(kiemTra.ThongTinKhachHang.DiaChiThuongTru.XaPhuong != null ? kiemTra.ThongTinKhachHang.DiaChiThuongTru.XaPhuong.Oid : Guid.Empty);
                                        //diaChi.SoNha = kiemTra.ThongTinKhachHang.DiaChiThuongTru.SoNha;
                                        //diaChi.FullDiaChi = kiemTra.ThongTinKhachHang.DiaChiThuongTru.FullDiaChi;
                                        ////
                                        DiaChi diaChi = View.ObjectSpace.GetObjectByKey<DiaChi>(kiemTra.ThongTinKhachHang.DiaChiThuongTru.Oid);
                                        giaDinhTre.DiaChiThuongTru = diaChi;
                                    }

                                    //
                                    _hoSoNhapHoc.GiaDinhTreList.Reload();
                                    _hoSoNhapHoc.GiaDinhTreList.Add(giaDinhTre);
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region Thông tin khách hàng

                            DanhSachTre dsTre = View.ObjectSpace.GetObjectByKey<DanhSachTre>(_chonTre.DanhSachTre.Oid);

                            if (dsTre != null)
                            {
                                //1. Lấy thông tin hồ sơ trẻ
                                _hoSoNhapHoc.Ho = dsTre.Ho;
                                _hoSoNhapHoc.Ten = dsTre.Ten;
                                _hoSoNhapHoc.NgaySinh = dsTre.NgaySinh;
                                _hoSoNhapHoc.GioiTinh = dsTre.GioiTinh;
                                _hoSoNhapHoc.OidStore = dsTre.Oid;
                                _hoSoNhapHoc.TruongDaHoc = dsTre.TruongDaHoc;
                                _hoSoNhapHoc.DanToc = dsTre.DanToc;
                                _hoSoNhapHoc.TonGiao = dsTre.TonGiao;

                                //2. Địa chỉ
                                if (dsTre.ThongTinKhachHang.DiaChiThuongTru != null)
                                {
                                    DiaChi diachi = View.ObjectSpace.GetObjectByKey<DiaChi>(dsTre.ThongTinKhachHang.DiaChiThuongTru.Oid);
                                    if (diachi != null)
                                        _hoSoNhapHoc.DiaChiThuongTru = diachi;
                                }
                                if (dsTre.ThongTinKhachHang.DiaChiTamTru != null)
                                {
                                    DiaChi diachi = View.ObjectSpace.GetObjectByKey<DiaChi>(dsTre.ThongTinKhachHang.DiaChiTamTru.Oid);
                                    if (diachi != null)
                                        _hoSoNhapHoc.DiaChiTamTru = diachi;
                                }
                                //3. Lấy thông tin gia đình trẻ
                                if (dsTre.ThongTinKhachHang != null)
                                {
                                    //
                                    GiaDinhTre giaDinhTre = new GiaDinhTre(((XPObjectSpace)View.ObjectSpace).Session);
                                    giaDinhTre.ThongTinKhachHang = View.ObjectSpace.GetObjectByKey<ThongTinKhachHang>(dsTre.ThongTinKhachHang.Oid);
                                    giaDinhTre.HoTen = dsTre.ThongTinKhachHang.HoTen;
                                    giaDinhTre.DienThoaiDiDong = dsTre.ThongTinKhachHang.DienThoaiDiDong;
                                    giaDinhTre.CMND = dsTre.ThongTinKhachHang.CMND;
                                    if (dsTre.ThongTinKhachHang.Email != null)
                                        giaDinhTre.Email = dsTre.ThongTinKhachHang.Email;
                                    if (dsTre.ThongTinKhachHang.CMND != null)
                                        giaDinhTre.CMND = dsTre.ThongTinKhachHang.CMND;
                                    giaDinhTre.GioiTinh = dsTre.ThongTinKhachHang.GioiTinh;

                                    // Địa chỉ
                                    if (dsTre.ThongTinKhachHang.DiaChiThuongTru != null)
                                    {
                                        DiaChi diachi = View.ObjectSpace.GetObjectByKey<DiaChi>(dsTre.ThongTinKhachHang.DiaChiThuongTru.Oid);
                                        if (diachi != null)
                                            giaDinhTre.DiaChiThuongTru = diachi;
                                    }
                                    //Cập nhật thông tin của phụ huynh nếu chưa có
                                    ThongTinKhachHang khachHang = View.ObjectSpace.GetObjectByKey<ThongTinKhachHang>(dsTre.ThongTinKhachHang.Oid);
                                    if (khachHang != null)
                                    {
                                        if (string.IsNullOrEmpty(giaDinhTre.Email) && string.IsNullOrEmpty(khachHang.Email))
                                            khachHang.Email = giaDinhTre.Email;
                                        if (string.IsNullOrEmpty(giaDinhTre.CMND) && string.IsNullOrEmpty(khachHang.CMND))
                                            khachHang.CMND = giaDinhTre.CMND;
                                        if (string.IsNullOrEmpty(giaDinhTre.DienThoaiDiDong) && string.IsNullOrEmpty(khachHang.DienThoaiDiDong))
                                            khachHang.DienThoaiDiDong = giaDinhTre.DienThoaiDiDong;
                                    }
                                    //
                                    _hoSoNhapHoc.GiaDinhTreList.Reload();
                                    _hoSoNhapHoc.GiaDinhTreList.Add(giaDinhTre);
                                }
                                #endregion
                            }
                        }
                    }
                }
            }
            #endregion

            #region 2. Kiểm tra IQ
            if (View.Id.Equals("KiemTraIQ_DetailView"))
            {
                //
                if (_chonTreIQ != null)
                {
                    if (_kiemTraIQ != null)
                    {
                        DanhSachTre dstre = View.ObjectSpace.GetObjectByKey<DanhSachTre>(_chonTreIQ.DanhSachTre.Oid);
                        if (dstre != null)
                        {
                            //1. Lấy thông tin hồ sơ trẻ
                            _kiemTraIQ.Ho = dstre.Ho;
                            _kiemTraIQ.Ten = dstre.Ten;
                            _kiemTraIQ.NgaySinhTre = dstre.NgaySinh;
                            _kiemTraIQ.GioiTinh = dstre.GioiTinh;
                            _kiemTraIQ.DanhSachTre = dstre;
                            //2. Lấy thông tin khách hàng
                            if (dstre.ThongTinKhachHang != null)
                            {
                                _kiemTraIQ.ThongTinKhachHang = dstre.ThongTinKhachHang;
                            }
                        }
                    }
                }
            }
            #endregion

        }
        private void TuyenSinh_ChonTreController_Activated(object sender, EventArgs e)
        {
            if (View is DetailView)
            {
                #region DetailView
                if (View.Id.Equals("HoSoNhapHoc_DetailView")
                    || View.Id.Equals("KiemTraIQ_DetailView"))
                {
                    popupWindowShowAction1.Active["TruyCap"] = true;
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
