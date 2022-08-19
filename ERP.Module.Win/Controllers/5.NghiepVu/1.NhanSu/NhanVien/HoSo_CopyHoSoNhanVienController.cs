using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.Commons;
using ERP.Module.Extends;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.GiayTo;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    public partial class HoSo_CopyHoSoNhanVienController : ViewController
    {
        IObjectSpace _obs;
        HoSo_ChonHoSoCopy _source;

        public HoSo_CopyHoSoNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "NhanVienDangLamViec_ListView;ThongTinNhanVien_DetailView;NhanSuCustomView_DetailView";
        }

        private void HoSo_CopyHoSoNhanVienController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = (Common.IsWriteGranted<ThongTinNhanVien>()
                                                        && (Common.SecuritySystemUser_GetCurrentUser().LoaiTaiKhoan == Enum.Systems.LoaiTaiKhoanEnum.QuanTriCongTy
                                                           || Common.SecuritySystemUser_GetCurrentUser().LoaiTaiKhoan == Enum.Systems.LoaiTaiKhoanEnum.QuanTriHeThong));
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            ThongTinNhanVien nhanvien = View.CurrentObject as ThongTinNhanVien;
            if (nhanvien != null)
            {
                _obs = Application.CreateObjectSpace();
                _source = _obs.CreateObject<HoSo_ChonHoSoCopy>();
                ThongTinNhanVien nv = _obs.GetObjectByKey<ThongTinNhanVien>(nhanvien.Oid);
                //_source.ThongTinNhanVien = _obs.GetObjectByKey<ThongTinNhanVien>(nhanvien.Oid); //Nguyen
                _source.CongTy = nv.CongTy;
                _source.BoPhan = nv.BoPhan;
                _source.NhomPhanBo = nv.NhomPhanBo;
                _source.ThongTinNhanVien = nv; 
                e.View = Application.CreateDetailView(_obs, _source);
            }
            else
            {
                _obs = Application.CreateObjectSpace();
                _source = _obs.CreateObject<HoSo_ChonHoSoCopy>();
                e.View = Application.CreateDetailView(_obs, _source);
            }
            
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_source != null)
            {
                try
                {
                    using (DialogUtil.AutoWait())
                    {
                        ThongTinNhanVien nhanvien = new ThongTinNhanVien(((XPObjectSpace)_obs).Session);

                        //Oid của hồ sơ cha
                        nhanvien.OidHoSoCha = _source.ThongTinNhanVien.Oid;

                        //Các thông tin cá nhân đi kèm                        
                        nhanvien.MaNhanVien = _source.ThongTinNhanVien.MaNhanVien;
                        nhanvien.MaHoSo = _source.ThongTinNhanVien.MaHoSo;
                        nhanvien.Ho = _source.ThongTinNhanVien.Ho;
                        nhanvien.Ten = _source.ThongTinNhanVien.Ten;
                        nhanvien.TenGoiKhac = _source.ThongTinNhanVien.TenGoiKhac;
                        nhanvien.GioiTinh = _source.ThongTinNhanVien.GioiTinh;
                        nhanvien.URLHinh = _source.ThongTinNhanVien.URLHinh;
                        nhanvien.NgaySinh = _source.ThongTinNhanVien.NgaySinh;
                        nhanvien.NoiSinh = _source.ThongTinNhanVien.NoiSinh;
                        nhanvien.QuocTich = _source.ThongTinNhanVien.QuocTich;
                        nhanvien.SoHoChieu = _source.ThongTinNhanVien.SoHoChieu;
                        nhanvien.NgayCapHoChieu = _source.ThongTinNhanVien.NgayCapHoChieu;
                        nhanvien.NoiCapHoChieu = _source.ThongTinNhanVien.NoiCapHoChieu;
                        nhanvien.NgayHetHan = _source.ThongTinNhanVien.NgayHetHan;
                        nhanvien.CMND = _source.ThongTinNhanVien.CMND;
                        nhanvien.NgayCap = _source.ThongTinNhanVien.NgayCap;
                        nhanvien.NoiCap = _source.ThongTinNhanVien.NoiCap;
                        nhanvien.DanToc = _source.ThongTinNhanVien.DanToc;
                        nhanvien.TonGiao = _source.ThongTinNhanVien.TonGiao;
                        nhanvien.TinhTrangHonNhan = _source.ThongTinNhanVien.TinhTrangHonNhan;
                        nhanvien.SucKhoe = _source.ThongTinNhanVien.SucKhoe;
                        nhanvien.NhomMau = _source.ThongTinNhanVien.NhomMau;
                        nhanvien.CanNang = _source.ThongTinNhanVien.CanNang;
                        nhanvien.ChieuCao = _source.ThongTinNhanVien.ChieuCao;
                        nhanvien.LoaiGioLamViec = _source.BoPhanDen.CongTy.CauHinhChung.CauHinhHoSo.LoaiGioLamViec;
                        nhanvien.LoaiNhanSu = _source.LoaiNhanSu;
                        nhanvien.DienThoaiDiDong = _source.ThongTinNhanVien.DienThoaiDiDong;
                        nhanvien.DienThoaiNhaRieng = _source.ThongTinNhanVien.DienThoaiNhaRieng;
                        nhanvien.Email = _source.ThongTinNhanVien.Email;
                        nhanvien.EmailNoiBo = _source.ThongTinNhanVien.EmailNoiBo;
                        nhanvien.DienThoaiNoiBo = _source.ThongTinNhanVien.DienThoaiNoiBo;
                        nhanvien.DienThoaiDiDong = _source.ThongTinNhanVien.DienThoaiDiDong;
                        nhanvien.DiaChiThuongTru = _source.ThongTinNhanVien.DiaChiThuongTru;
                        nhanvien.QueQuan = _source.ThongTinNhanVien.QueQuan;
                        nhanvien.NoiOHienNay = _source.ThongTinNhanVien.NoiOHienNay;
                        nhanvien.KhoiTapDoan = _source.ThongTinNhanVien.KhoiTapDoan;
                        nhanvien.ChucDanhTapDoan = _source.ThongTinNhanVien.ChucDanhTapDoan;
                        nhanvien.NgayVaoTapDoan = _source.ThongTinNhanVien.NgayVaoTapDoan;

                        nhanvien.BoPhan = _source.BoPhanDen;
                        nhanvien.ChucVu = _source.ChucVu;
                        nhanvien.ChucDanh = _source.ChucDanh;
                        nhanvien.NgayVaoCongTy = _source.NgayVao;
                        nhanvien.ThamNienLamViec = _source.NgayVao;
                        nhanvien.TinhTrang = _source.ThongTinNhanVien.TinhTrang;
                        nhanvien.CongTy = _source.BoPhanDen.CongTy;
                        nhanvien.NhomPhanBo = _source.NhomPhanBo;

                        nhanvien.NhanVienTrinhDo.ChuyenNganhDaoTao = _source.ThongTinNhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao;
                        nhanvien.NhanVienTrinhDo.HinhThucDaoTao = _source.ThongTinNhanVien.NhanVienTrinhDo.HinhThucDaoTao;
                        nhanvien.NhanVienTrinhDo.HocHam = _source.ThongTinNhanVien.NhanVienTrinhDo.HocHam;
                        nhanvien.NhanVienTrinhDo.NamTotNghiep = _source.ThongTinNhanVien.NhanVienTrinhDo.NamTotNghiep;
                        nhanvien.NhanVienTrinhDo.NgayCapBang = _source.ThongTinNhanVien.NhanVienTrinhDo.NgayCapBang;
                        nhanvien.NhanVienTrinhDo.NgoaiNgu = _source.ThongTinNhanVien.NhanVienTrinhDo.NgoaiNgu;
                        nhanvien.NhanVienTrinhDo.TrinhDoChuyenMon = _source.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon;
                        nhanvien.NhanVienTrinhDo.TrinhDoNgoaiNgu = _source.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu;
                        nhanvien.NhanVienTrinhDo.TrinhDoTinHoc = _source.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoTinHoc;
                        nhanvien.NhanVienTrinhDo.TrinhDoVanHoa = _source.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoVanHoa;
                        nhanvien.NhanVienTrinhDo.TruongDaoTao = _source.ThongTinNhanVien.NhanVienTrinhDo.TruongDaoTao;

                        foreach (QuanHeGiaDinh item in _source.ThongTinNhanVien.ListQuanHeGiaDinh)
                        {
                            QuanHeGiaDinh giaDinh = new QuanHeGiaDinh(((XPObjectSpace)_obs).Session);
                            giaDinh.DanToc = item.DanToc;
                            giaDinh.DienThoaiDiDong = item.DienThoaiDiDong;
                            giaDinh.GioiTinh = item.GioiTinh;
                            giaDinh.HoTenNguoiThan = item.HoTenNguoiThan;
                            giaDinh.NamDiCu = item.NamDiCu;
                            giaDinh.NgaySinh = item.NgaySinh;
                            giaDinh.NgheNghiepHienTai = item.NgheNghiepHienTai;
                            giaDinh.NoiLamViec = item.NoiLamViec;
                            giaDinh.NoiOHienNay = item.NoiOHienNay;
                            giaDinh.NuocCuTru = item.NuocCuTru;
                            giaDinh.PhanLoai = item.PhanLoai;
                            giaDinh.QuanHe = item.QuanHe;
                            giaDinh.QueQuan = item.QueQuan;
                            giaDinh.QuocTich = item.QuocTich;
                            giaDinh.TinhTrang = item.TinhTrang;
                            giaDinh.TonGiao = item.TonGiao;
                            nhanvien.ListQuanHeGiaDinh.Add(giaDinh);
                        }

                        //Giảm trừ gia cảnh chỉ đăng ký được ở một nơi
                        //foreach (GiamTruGiaCanh item in _source.ThongTinNhanVien.ListGiamTruGiaCanh)
                        //{
                        //    GiamTruGiaCanh giamTru = new GiamTruGiaCanh(((XPObjectSpace)_obs).Session);
                        //    giamTru.DenNgay = item.DenNgay;
                        //    giamTru.GhiChu = item.GhiChu;
                        //    giamTru.LoaiGiamTruGiaCanh = item.LoaiGiamTruGiaCanh;
                        //    giamTru.MaSoThue = item.MaSoThue;
                        //    giamTru.NgungGiamTru = item.NgungGiamTru;
                        //    giamTru.QuanHeGiaDinh = item.QuanHeGiaDinh;
                        //    giamTru.TuNgay = item.TuNgay;
                        //    nhanvien.ListGiamTruGiaCanh.Add(giamTru);
                        //}

                        foreach (TaiKhoanNganHang item in _source.ThongTinNhanVien.ListTaiKhoanNganHang)
                        {
                            TaiKhoanNganHang taiKhoan = new TaiKhoanNganHang(((XPObjectSpace)_obs).Session);
                            taiKhoan.CongTy = item.CongTy;
                            taiKhoan.NganHang = item.NganHang;
                            taiKhoan.SoTaiKhoan = item.SoTaiKhoan;
                            taiKhoan.TaiKhoanChinh = item.TaiKhoanChinh;
                            nhanvien.ListTaiKhoanNganHang.Add(taiKhoan);
                        }

                        foreach (VanBang item in _source.ThongTinNhanVien.ListVanBang)
                        {
                            VanBang vanBang = new VanBang(((XPObjectSpace)_obs).Session);
                            vanBang.ChuyenNganhDaoTao = item.ChuyenNganhDaoTao;
                            vanBang.DiemTrungBinh = item.DiemTrungBinh;
                            vanBang.GhiChu = item.GhiChu;
                            vanBang.HinhThucDaoTao = item.HinhThucDaoTao;
                            vanBang.NamTotNghiep = item.NamTotNghiep;
                            vanBang.NgayCapBang = item.NgayCapBang;
                            vanBang.TrinhDoChuyenMon = item.TrinhDoChuyenMon;
                            vanBang.TruongDaoTao = item.TruongDaoTao;
                            vanBang.XepLoai = item.XepLoai;
                            nhanvien.ListVanBang.Add(vanBang);
                        }

                        foreach (TrinhDoNgoaiNguKhac item in _source.ThongTinNhanVien.ListNgoaiNgu)
                        {
                            TrinhDoNgoaiNguKhac ngoaiNguKhac = new TrinhDoNgoaiNguKhac(((XPObjectSpace)_obs).Session);
                            ngoaiNguKhac.Diem = item.Diem;
                            ngoaiNguKhac.GhiChu = item.GhiChu;
                            ngoaiNguKhac.GiayToList = item.GiayToList;
                            ngoaiNguKhac.NgayCap = item.NgayCap;
                            ngoaiNguKhac.NgoaiNgu = item.NgoaiNgu;
                            ngoaiNguKhac.NoiCap = item.NoiCap;
                            ngoaiNguKhac.TrinhDoNgoaiNgu = item.TrinhDoNgoaiNgu;
                            ngoaiNguKhac.XepLoai = item.XepLoai;
                            nhanvien.ListNgoaiNgu.Add(ngoaiNguKhac);
                        }

                        foreach (ChungChi item in _source.ThongTinNhanVien.ListChungChi)
                        {
                            ChungChi chungChi = new ChungChi(((XPObjectSpace)_obs).Session);
                            chungChi.Diem = item.Diem;
                            chungChi.GhiChu = item.GhiChu;
                            chungChi.LoaiChungChi = item.LoaiChungChi;
                            chungChi.NgayCap = item.NgayCap;
                            chungChi.NoiCap = item.NoiCap;
                            chungChi.TenChungChi = item.TenChungChi;
                            chungChi.XepLoai = item.XepLoai;
                            nhanvien.ListChungChi.Add(chungChi);
                        }

                        foreach (GiayToHoSo item in _source.ThongTinNhanVien.ListGiayToHoSo)
                        {
                            GiayToHoSo giayToHoSo = new GiayToHoSo(((XPObjectSpace)_obs).Session);
                            giayToHoSo.STT = item.STT;
                            giayToHoSo.TenGiayTo = item.TenGiayTo;
                            giayToHoSo.NgayLap = item.NgayLap;
                            giayToHoSo.LoaiGiayTo = item.LoaiGiayTo;
                            giayToHoSo.GhiChu = item.GhiChu;
                            giayToHoSo.DuongDanFile = item.DuongDanFile;
                            giayToHoSo.DuongDanFileWeb = item.DuongDanFileWeb;
                            nhanvien.ListGiayToHoSo.Add(giayToHoSo);
                        }

                        View.ObjectSpace.Refresh();
                        View.Refresh();
                    }

                    DialogUtil.ShowInfo("Copy thành công.");
                }
                catch (Exception ex)
                {
                    DialogUtil.ShowInfo("Lỗi copy: " + ex.ToString());
                }
            }
        }
    }
}
