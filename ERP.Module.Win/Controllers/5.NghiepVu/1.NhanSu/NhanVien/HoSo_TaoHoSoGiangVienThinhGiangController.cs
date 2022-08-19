using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using DevExpress.Utils;
using System.Data;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.Commons;
using ERP.Module.Extends;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NonPersistentObjects.DanhMuc;
using System.Text;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.Xpo;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    public partial class HoSo_TaoHoSoGiangVienThinhGiangController : ViewController
    {     

        public HoSo_TaoHoSoGiangVienThinhGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "NhanVienDangLamViec_ListView;ThongTinNhanVien_DetailView;NhanSuCustomView_DetailView";
        }

        private void HoSo_TaoHoSoGiangVienThinhGiangController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = (Common.IsWriteGranted<GiangVienThinhGiang>());
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace obs = Application.CreateObjectSpace();
            ThongTinNhanVien nhanVien = null;
            if (View.Id == "NhanVienDangLamViec_ListView")
            {
                nhanVien = View.CurrentObject as ThongTinNhanVien;
            }

            if (View.Id == "NhanSuCustomView_DetailView")
            {
                NhanSuCustomView nsView = View.CurrentObject as NhanSuCustomView;
                if (nsView != null)
                    nhanVien = obs.GetObjectByKey<ThongTinNhanVien>(nsView.Oid);
            }

            //
            if (nhanVien != null)
            {
                if (DialogUtil.ShowYesNo(String.Format("Bạn thật sự muốn tạo hồ sơ thỉnh giảng cho cán bộ {0} - {1}", nhanVien.MaTapDoan, nhanVien.HoTen)) == System.Windows.Forms.DialogResult.Yes)
                {
                    XuLy(obs, nhanVien);
                }
            }
        }

        private static void XuLy(IObjectSpace obs, ThongTinNhanVien thongTinNhanVien)
        {
            using (DialogUtil.AutoWait())
            {
                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();
                    //
                    GiangVienThinhGiang thinhGiang = uow.FindObject<GiangVienThinhGiang>(CriteriaOperator.Parse("HoTen like ? and NgaySinh=?", thongTinNhanVien.HoTen, thongTinNhanVien.NgaySinh));
                    if (thinhGiang == null)
                    {
                        try
                        {
                            thinhGiang = new GiangVienThinhGiang(uow);

                            #region Thông tin cơ bản
                            thinhGiang.URLHinh = thongTinNhanVien.URLHinh;
                            thinhGiang.MaNhanVien = thongTinNhanVien.MaNhanVien;
                            thinhGiang.OidHoSoCha = thongTinNhanVien.Oid;
                            thinhGiang.Ho = thongTinNhanVien.Ho;
                            thinhGiang.Ten = thongTinNhanVien.Ten;                           
                            thinhGiang.TenGoiKhac = thongTinNhanVien.TenGoiKhac;
                            thinhGiang.GioiTinh = thongTinNhanVien.GioiTinh;
                            thinhGiang.NgaySinh = thongTinNhanVien.NgaySinh;
                            thinhGiang.NoiSinh = thongTinNhanVien.NoiSinh != null ? uow.GetObjectByKey<DiaChi>(thongTinNhanVien.NoiSinh.Oid) : null;
                            thinhGiang.CMND = thongTinNhanVien.CMND;
                            thinhGiang.SoHoChieu = thongTinNhanVien.SoHoChieu;
                            thinhGiang.NgayCap = thongTinNhanVien.NgayCap;
                            thinhGiang.NoiCap = thongTinNhanVien.NoiCap != null ? uow.GetObjectByKey<TinhThanh>(thongTinNhanVien.NoiCap.Oid) : null;
                            thinhGiang.QuocTich = thongTinNhanVien.QuocTich != null ? uow.GetObjectByKey<QuocGia>(thongTinNhanVien.QuocTich.Oid) : null;
                            thinhGiang.QueQuan = thongTinNhanVien.QueQuan != null ? uow.GetObjectByKey<DiaChi>(thongTinNhanVien.QueQuan.Oid) : null;
                            thinhGiang.DiaChiThuongTru = thongTinNhanVien.DiaChiThuongTru != null ? uow.GetObjectByKey<DiaChi>(thongTinNhanVien.DiaChiThuongTru.Oid) : null;
                            thinhGiang.NoiOHienNay = thongTinNhanVien.NoiOHienNay != null ? uow.GetObjectByKey<DiaChi>(thongTinNhanVien.NoiOHienNay.Oid) : null;
                            thinhGiang.Email = thongTinNhanVien.Email;
                            thinhGiang.DienThoaiDiDong = thongTinNhanVien.DienThoaiDiDong;
                            thinhGiang.DienThoaiNhaRieng = thongTinNhanVien.DienThoaiNhaRieng;
                            thinhGiang.TinhTrangHonNhan = thongTinNhanVien.TinhTrangHonNhan != null ? uow.GetObjectByKey<TinhTrangHonNhan>(thongTinNhanVien.TinhTrangHonNhan.Oid) : null;
                            thinhGiang.DanToc = thongTinNhanVien.DanToc != null ? uow.GetObjectByKey<DanToc>(thongTinNhanVien.DanToc.Oid) : null;
                            thinhGiang.TonGiao = thongTinNhanVien.TonGiao != null ? uow.GetObjectByKey<TonGiao>(thongTinNhanVien.TonGiao.Oid) : null;
                            thinhGiang.HinhThucTuyenDung = thongTinNhanVien.HinhThucTuyenDung;
                            thinhGiang.GhiChu = thongTinNhanVien.GhiChu;
                            thinhGiang.NgayVaoCongTy = thongTinNhanVien.NgayVaoCongTy;                           
                            //thinhGiang.ChucDanh = thongTinNhanVien.ChucDanh != null ? uow.GetObjectByKey<ChucDanh>(thongTinNhanVien.ChucDanh.Oid) : null;
                            //
                            thinhGiang.BoPhan = thongTinNhanVien.CongTy != null ? uow.GetObjectByKey<BoPhan>(thongTinNhanVien.CongTy.Oid) : null;
                            //thinhGiang.TaiBoMon = thongTinNhanVien.TaiBoMon != null ? uow.GetObjectByKey<BoPhan>(thongTinNhanVien.TaiBoMon.Oid) : uow.GetObjectByKey<BoPhan>(thongTinNhanVien.BoPhan.Oid);
                            //                           
                            //thinhGiang.CongViecHienNay = thongTinNhanVien.CongViecHienNay != null ? uow.GetObjectByKey<CongViec>(thongTinNhanVien.CongViecHienNay.Oid) : null;
                            thinhGiang.NgayTuyenDung = thongTinNhanVien.NgayTuyenDung;
                            thinhGiang.DonViTuyenDung = thongTinNhanVien.DonViTuyenDung;                          
                            thinhGiang.TinhTrang = uow.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Đang làm việc"));
                            #endregion

                            #region Nhân viên thông tin lương
                            if (thongTinNhanVien.NhanVienThongTinLuong != null)
                            {
                                thinhGiang.NhanVienThongTinLuong.MaSoThue = thongTinNhanVien.NhanVienThongTinLuong.MaSoThue;
                                thinhGiang.NhanVienThongTinLuong.TinhThueMacDinh = true;
                            }
                            #endregion

                            #region Nhân viên trình độ
                            if (thongTinNhanVien.NhanVienTrinhDo != null)
                            {
                                thinhGiang.NhanVienTrinhDo.TrinhDoVanHoa = thongTinNhanVien.NhanVienTrinhDo.TrinhDoVanHoa != null ? uow.GetObjectByKey<TrinhDoVanHoa>(thongTinNhanVien.NhanVienTrinhDo.TrinhDoVanHoa.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.TrinhDoChuyenMon = thongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? uow.GetObjectByKey<TrinhDoChuyenMon>(thongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.ChuyenNganhDaoTao = thongTinNhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao != null ? uow.GetObjectByKey<ChuyenNganhDaoTao>(thongTinNhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.TruongDaoTao = thongTinNhanVien.NhanVienTrinhDo.TruongDaoTao != null ? uow.GetObjectByKey<TruongDaoTao>(thongTinNhanVien.NhanVienTrinhDo.TruongDaoTao.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.HinhThucDaoTao = thongTinNhanVien.NhanVienTrinhDo.HinhThucDaoTao != null ? uow.GetObjectByKey<HinhThucDaoTao>(thongTinNhanVien.NhanVienTrinhDo.HinhThucDaoTao.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.NamTotNghiep = thongTinNhanVien.NhanVienTrinhDo.NamTotNghiep;
                                thinhGiang.NhanVienTrinhDo.HocHam = thongTinNhanVien.NhanVienTrinhDo.HocHam != null ? uow.GetObjectByKey<HocHam>(thongTinNhanVien.NhanVienTrinhDo.HocHam.Oid) : null;                            
                                thinhGiang.NhanVienTrinhDo.TrinhDoTinHoc = thongTinNhanVien.NhanVienTrinhDo.TrinhDoTinHoc != null ? uow.GetObjectByKey<TrinhDoTinHoc>(thongTinNhanVien.NhanVienTrinhDo.TrinhDoTinHoc.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.NgoaiNgu = thongTinNhanVien.NhanVienTrinhDo.NgoaiNgu != null ? uow.GetObjectByKey<NgoaiNgu>(thongTinNhanVien.NhanVienTrinhDo.NgoaiNgu.Oid) : null;
                                thinhGiang.NhanVienTrinhDo.TrinhDoNgoaiNgu = thongTinNhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu != null ? uow.GetObjectByKey<TrinhDoNgoaiNgu>(thongTinNhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu.Oid) : null;
                            }
                            #endregion

                            #region Tài khoản ngân hàng
                            foreach (TaiKhoanNganHang item in thongTinNhanVien.ListTaiKhoanNganHang)
                            {
                                TaiKhoanNganHang taiKhoan = new TaiKhoanNganHang(uow);
                                taiKhoan.NhanVien = thinhGiang;
                                taiKhoan.SoTaiKhoan = item.SoTaiKhoan;
                                taiKhoan.TaiKhoanChinh = item.TaiKhoanChinh;                              
                                taiKhoan.NganHang = item.NganHang != null ? uow.GetObjectByKey<NganHang>(item.NganHang.Oid) : null;
                                //
                                thinhGiang.ListTaiKhoanNganHang.Add(taiKhoan);
                            }
                            #endregion

                            #region Văn bằng
                            using (XPCollection<VanBang> vanBangList = new DevExpress.Xpo.XPCollection<VanBang>(uow, CriteriaOperator.Parse("HoSo=?", thongTinNhanVien.Oid)))
                            {
                                foreach (VanBang item in vanBangList)
                                {
                                    if (item.TrinhDoChuyenMon != null)
                                    {
                                        VanBang vanBang = new VanBang(uow);
                                        vanBang.HoSo = thinhGiang;
                                        vanBang.TrinhDoChuyenMon = item.TrinhDoChuyenMon != null ? uow.GetObjectByKey<TrinhDoChuyenMon>(item.TrinhDoChuyenMon.Oid) : null;
                                        vanBang.ChuyenNganhDaoTao = item.ChuyenNganhDaoTao != null ? uow.GetObjectByKey<ChuyenNganhDaoTao>(item.ChuyenNganhDaoTao.Oid) : null;
                                        vanBang.TruongDaoTao = item.TruongDaoTao != null ? uow.GetObjectByKey<TruongDaoTao>(item.TruongDaoTao.Oid) : null;
                                        vanBang.HinhThucDaoTao = item.HinhThucDaoTao != null ? uow.GetObjectByKey<HinhThucDaoTao>(item.HinhThucDaoTao.Oid) : null;
                                        vanBang.DiemTrungBinh = item.DiemTrungBinh;
                                        vanBang.XepLoai = item.XepLoai;
                                        vanBang.NamTotNghiep = item.NamTotNghiep;
                                        //vanBang.GiayToHoSo = item.GiayToHoSo != null ? uow.GetObjectByKey<GiayToHoSo>(item.GiayToHoSo.Oid) : null;
                                        //vanBang.LuuTruBangDiem = item.GiayToHoSo != null ? uow.GetObjectByKey<GiayToHoSo>(item.GiayToHoSo.Oid) : null;
                                        //
                                        thinhGiang.ListVanBang.Add(vanBang);
                                    }
                                }
                            }
                            #endregion

                            #region Chứng chỉ
                            using (XPCollection<ChungChi> chungChiList = new XPCollection<ChungChi>(uow, CriteriaOperator.Parse("HoSo=?", thongTinNhanVien.Oid)))
                            {
                                foreach (ChungChi item in chungChiList)
                                {
                                    if (item.LoaiChungChi != null)
                                    {
                                        ChungChi chungChi = new ChungChi(uow);
                                        chungChi.HoSo = thinhGiang;
                                        chungChi.TenChungChi = item.TenChungChi;
                                        chungChi.XepLoai = item.XepLoai;
                                        chungChi.NoiCap = item.NoiCap;
                                        chungChi.NgayCap = item.NgayCap;
                                        chungChi.Diem = item.Diem;
                                        chungChi.LoaiChungChi = item.LoaiChungChi != null ? uow.GetObjectByKey<LoaiChungChi>(item.LoaiChungChi.Oid) : null;
                                        //chungChi.GiayToHoSo = item.GiayToHoSo != null ? uow.GetObjectByKey<GiayToHoSo>(item.GiayToHoSo.Oid) : null;
                                        chungChi.LoaiChungChi = item.LoaiChungChi != null ? uow.GetObjectByKey<LoaiChungChi>(item.LoaiChungChi.Oid) : null;
                                        //
                                        thinhGiang.ListChungChi.Add(chungChi);
                                    }
                                }
                            }
                            #endregion

                            #region Trình độ ngoại ngữ khác
                            using (XPCollection<TrinhDoNgoaiNguKhac> trinhDoNgoaiNguKhacList = new XPCollection<TrinhDoNgoaiNguKhac>(uow, CriteriaOperator.Parse("HoSo=?", thongTinNhanVien.Oid)))
                            {
                                foreach (TrinhDoNgoaiNguKhac item in trinhDoNgoaiNguKhacList)
                                {
                                    if (item.NgoaiNgu != null && item.TrinhDoNgoaiNgu != null)
                                    {
                                        TrinhDoNgoaiNguKhac trinhDoNgoaiNguKhac = new TrinhDoNgoaiNguKhac(uow);
                                        trinhDoNgoaiNguKhac.HoSo = thinhGiang;
                                        trinhDoNgoaiNguKhac.NgoaiNgu = item.NgoaiNgu != null ? uow.GetObjectByKey<NgoaiNgu>(item.NgoaiNgu.Oid) : null;
                                        trinhDoNgoaiNguKhac.TrinhDoNgoaiNgu = item.TrinhDoNgoaiNgu != null ? uow.GetObjectByKey<TrinhDoNgoaiNgu>(item.TrinhDoNgoaiNgu.Oid) : null;
                                        trinhDoNgoaiNguKhac.Diem = item.Diem;
                                        //trinhDoNgoaiNguKhac.GiayToHoSo = item.GiayToHoSo != null ? uow.GetObjectByKey<GiayToHoSo>(item.GiayToHoSo.Oid) : null;
                                        //
                                        thinhGiang.ListNgoaiNgu.Add(trinhDoNgoaiNguKhac);
                                    }
                                }
                            }
                            #endregion                           

                            //Tiến hành lưu dữ liệu
                            uow.CommitChanges();
                            //
                            DialogUtil.ShowInfo(String.Format("Đã tạo hồ sơ thỉnh giảng cho cán bộ {0}.", thongTinNhanVien.HoTen));
                        }
                        catch (Exception ex)
                        {
                            //
                            uow.RollbackTransaction();
                            //
                            DialogUtil.ShowError(String.Format("Không thể tạo hồ sơ thỉnh giảng của cán bộ [{0}]. Vì {1}", thongTinNhanVien.HoTen, ex.Message));
                        }
                    }
                    else
                    {
                        DialogUtil.ShowError(String.Format("Hồ sơ của cán bộ [{0}] đã tồn tại trong thỉnh giảng.", thongTinNhanVien.HoTen));
                    }
                }
            }
        }
        
    }
}
