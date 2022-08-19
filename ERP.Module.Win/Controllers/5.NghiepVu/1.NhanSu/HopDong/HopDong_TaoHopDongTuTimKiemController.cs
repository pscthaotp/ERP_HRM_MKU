using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.HopDongs;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.Extends;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.TienLuong;
//
namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.HopDongs
{
    public partial class HopDong_TaoHopDongTuTimKiemController : ViewController
    {
        private IObjectSpace obs;
        private HopDong_ChonLoaiHopDong chonHopDong;
        private DanhSachHetHanHopDong dsHetHanHopDong;
        private ThongTinNhanVien nhanVien;
        private HopDong hopDong_NhanVien;
        private HopDongLamViec hopDongLamViec = null;
        private HopDongKhoan hopDongKhoan = null;
        private NienDoTaiChinh nienDoTaiChinh;
        private QuanLyHopDong qlHopDong;

        public HopDong_TaoHopDongTuTimKiemController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            obs = Application.CreateObjectSpace();
            //
            dsHetHanHopDong = View.CurrentObject as DanhSachHetHanHopDong;
            if (dsHetHanHopDong != null)
            {
                try
                {
                    if (DialogUtil.ShowYesNo("Bạn thật sự muốn tạo hợp đồng mới cho nhân viên đã chọn?") == DialogResult.Yes)
                    {
                        foreach (ChiTietHetHanHopDong item in dsHetHanHopDong.ChiTietHetHanHopDongList)
                        {
                            if (item.Chon)
                            {
                                hopDong_NhanVien = item.HopDong as HopDong;
                                chonHopDong = obs.CreateObject<HopDong_ChonLoaiHopDong>();
                                chonHopDong.LoaiHopDong = obs.GetObjectByKey<LoaiHopDong>(item.HopDong.LoaiHopDong.Oid);
                                if (item.HopDong is HopDongLamViec)
                                {
                                    hopDongLamViec = obs.GetObjectByKey<HopDongLamViec>(item.HopDong.Oid);
                                    nienDoTaiChinh = Common.GetCurrentNienDoTaiChinhByDate(((XPObjectSpace)obs).Session, hopDongLamViec.QuanLyHopDong.CongTy, hopDongLamViec.DenNgay.AddDays(1));
                                }
                                else if (item.HopDong is HopDongKhoan)
                                {
                                    hopDongKhoan = obs.GetObjectByKey<HopDongKhoan>(item.HopDong.Oid);
                                    nienDoTaiChinh = Common.GetCurrentNienDoTaiChinhByDate(((XPObjectSpace)obs).Session, hopDongKhoan.QuanLyHopDong.CongTy, hopDongKhoan.DenNgay.AddDays(1));
                                }
                                e.View = Application.CreateDetailView(obs, chonHopDong);
                                break;
                            }                            
                        }
                        if (hopDong_NhanVien == null)
                        {
                            DialogUtil.ShowError("Vui lòng nhấn chọn dòng Hợp đồng cần tạo!");                            
                        }
                    }  
                    else
                    {

                       obs.Committed += obs_Committed;
                    }                 
                }
                catch (Exception ex) { }
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (hopDong_NhanVien != null)
            {
                e.PopupWindow.View.ObjectSpace.CommitChanges();
                obs = Application.CreateObjectSpace();

                if (nienDoTaiChinh != null)
                {
                    qlHopDong = obs.FindObject<QuanLyHopDong>(CriteriaOperator.Parse("NienDoTaiChinh=? && CongTy =?", nienDoTaiChinh.Oid, nienDoTaiChinh.CongTy.Oid));
                    if (qlHopDong == null)
                    {
                        qlHopDong = new QuanLyHopDong(((XPObjectSpace)obs).Session);
                        qlHopDong.CongTy = obs.GetObjectByKey<CongTy>(nienDoTaiChinh.CongTy.Oid);
                        qlHopDong.NienDoTaiChinh = obs.GetObjectByKey<NienDoTaiChinh>(nienDoTaiChinh.Oid);
                        qlHopDong.Save();
                    }

                    if (chonHopDong.LoaiHopDong.TenLoaiHopDong.Contains("khoán"))
                    {
                        HopDongKhoan hdKhoan = obs.CreateObject<HopDongKhoan>();
                        nhanVien = obs.GetObjectByKey<ThongTinNhanVien>(hopDongKhoan.ThongTinNhanVien.Oid);
                        hdKhoan.BoPhan = obs.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                        hdKhoan.QuanLyHopDong = qlHopDong;
                        hdKhoan.LoaiCongTy = obs.GetObjectByKey<LoaiCongTy>(hopDongKhoan.LoaiCongTy.Oid);
                        hdKhoan.TenCongTy = hopDongKhoan.TenCongTy;
                        hdKhoan.PhanLoaiNguoiKy = obs.GetObjectByKey<PhanLoaiNguoiKy>(hopDongKhoan.PhanLoaiNguoiKy.Oid);
                        hdKhoan.ChucVuNguoiKy = obs.GetObjectByKey<ChucVuNguoiKy>(hopDongKhoan.ChucVuNguoiKy.Oid);
                        hdKhoan.NguoiKy = obs.GetObjectByKey<ThongTinNhanVien>(hopDongKhoan.NguoiKy.Oid);
                        hdKhoan.CanCu = hopDongKhoan.CanCu;
                        hdKhoan.ThongTinNhanVien = nhanVien;
                        hdKhoan.LoaiHopDong = obs.GetObjectByKey<LoaiHopDong>(chonHopDong.LoaiHopDong.Oid);
                        hdKhoan.InThoaThuan = true;

                        if (hopDongKhoan != null)
                        {
                            if (hopDongKhoan.HinhThucHopDong != null)
                            {
                                hdKhoan.HinhThucHopDong = obs.GetObjectByKey<HinhThucHopDong>(hopDongKhoan.HinhThucHopDong.Oid);
                            }
                            hdKhoan.LoaiHopDongKhoan = hopDongKhoan.LoaiHopDongKhoan;
                            hdKhoan.TuNgay = hopDongKhoan.DenNgay.AddDays(1);
                            hdKhoan.NgayHuongLuong = hopDongKhoan.NgayHuongLuong;
                            hdKhoan.LuongKhoan = hopDongKhoan.LuongKhoan;
                            hdKhoan.PhuCapTienAn = hopDongKhoan.PhuCapTienAn;
                            hdKhoan.PhuCapTienXang = hopDongKhoan.PhuCapTienXang;
                        }

                        e.ShowViewParameters.Context = TemplateContext.View;
                        e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                        e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, hdKhoan);
                        e.ShowViewParameters.CreatedView.ObjectSpace.Committed += obs_Committed;

                    }
                    else
                    {
                        HopDongLamViec hdLamViec = obs.CreateObject<HopDongLamViec>();
                        nhanVien = obs.GetObjectByKey<ThongTinNhanVien>(hopDongLamViec.ThongTinNhanVien.Oid);
                        if (nhanVien.BoPhan != null)
                            hdLamViec.BoPhan = obs.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                        hdLamViec.ThongTinNhanVien = nhanVien;
                        hdLamViec.QuanLyHopDong = qlHopDong;
                        if (hopDongLamViec.LoaiCongTy != null)
                            hdLamViec.LoaiCongTy = obs.GetObjectByKey<LoaiCongTy>(hopDongLamViec.LoaiCongTy.Oid);
                        hdLamViec.TenCongTy = hopDongLamViec.TenCongTy;
                        if (hopDongLamViec.PhanLoaiNguoiKy != null)
                            hdLamViec.PhanLoaiNguoiKy = obs.GetObjectByKey<PhanLoaiNguoiKy>(hopDongLamViec.PhanLoaiNguoiKy.Oid);
                        if (hopDongLamViec.ChucVuNguoiKy != null)
                            hdLamViec.ChucVuNguoiKy = obs.GetObjectByKey<ChucVuNguoiKy>(hopDongLamViec.ChucVuNguoiKy.Oid);
                        if (hopDongLamViec.NguoiKy != null)
                            hdLamViec.NguoiKy = obs.GetObjectByKey<ThongTinNhanVien>(hopDongLamViec.NguoiKy.Oid);
                        hdLamViec.CanCu = hopDongLamViec.CanCu;
                        if (hopDongLamViec.LoaiHopDong != null)
                            hdLamViec.LoaiHopDong = obs.GetObjectByKey<LoaiHopDong>(chonHopDong.LoaiHopDong.Oid);
                        hdLamViec.InThoaThuan = true;

                        if (hopDongLamViec != null && hopDongLamViec.HinhThucHopDong != null)
                        {
                            if (hopDongLamViec.HinhThucHopDong.TenHinhThucHopDong.Contains("thử việc"))
                            {
                                hdLamViec.HinhThucHopDong = obs.FindObject<HinhThucHopDong>(CriteriaOperator.Parse("LoaiHopDong = ? and TenHinhThucHopDong like ?", chonHopDong.LoaiHopDong.Oid, "%1 năm%"));
                            }
                        }

                        hdLamViec.TuNgay = hopDongLamViec.DenNgay.AddDays(1);
                        if (hopDongLamViec.NgachLuong != null)
                            hdLamViec.NgachLuong = obs.GetObjectByKey<NgachLuong>(hopDongLamViec.NgachLuong.Oid);
                        if (hopDongLamViec.BacLuong != null)
                            hdLamViec.BacLuong = obs.GetObjectByKey<BacLuong>(hopDongLamViec.BacLuong.Oid);
                        if (nhanVien != null)
                        {
                            hdLamViec.LuongCoBan = nhanVien.NhanVienThongTinLuong.LuongCoBan;
                            hdLamViec.LuongKinhDoanh = nhanVien.NhanVienThongTinLuong.LuongKinhDoanh;
                            hdLamViec.NgayHuongLuong = nhanVien.NhanVienThongTinLuong.NgayHuongLuong;
                            hdLamViec.PhanTramTinhLuong = nhanVien.NhanVienThongTinLuong.PhanTramTinhLuong;
                        }
                        hdLamViec.InThoaThuan = true;
                        if (hopDongLamViec != null && chonHopDong.LoaiHopDong.TenLoaiHopDong.Contains("phụ lục"))
                        {
                            hdLamViec.HopDongLaoDong = obs.GetObjectByKey<HopDongLamViec>(hopDongLamViec.Oid);
                        }

                        e.ShowViewParameters.Context = TemplateContext.View;
                        e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                        e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, hdLamViec);
                        e.ShowViewParameters.CreatedView.ObjectSpace.Committed += obs_Committed;
                    }
                }
                else
                {
                    DialogUtil.ShowError("Vui lòng tạo Niên độ tài chính mới!");
                }
            }         
        }
        //reload listview after save object in detailvew
        void obs_Committed(object sender, EventArgs e)
        {
            obs.Refresh();
            //
            if (dsHetHanHopDong != null)
                dsHetHanHopDong.LoadData();
        }

        private void HopDong_TaoHopDongTuTimKiemController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active.Clear();
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<DanhSachHetHanHopDong>();
        }
    }
}
