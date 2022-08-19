using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.HopDongs;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Extends;
using System.Windows.Forms;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
//
namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.HopDongs
{
    public partial class HopDong_TaoQuyetDinhChamDutHopDongTuTimKiemController : ViewController
    {
        private IObjectSpace obs;        
        private DanhSachHetHanHopDong dsHetHanHopDong;
        private ThongTinNhanVien nhanVien;    
        private HopDong hopDong;
        private HopDongLamViec hopDongLamViec = null;
        private HopDongKhoan hopDongKhoan = null;
        public HopDong_TaoQuyetDinhChamDutHopDongTuTimKiemController()
        {
            InitializeComponent();
            RegisterActions(components);            
        }
               
        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {            
            obs = Application.CreateObjectSpace();
            dsHetHanHopDong = View.CurrentObject as DanhSachHetHanHopDong;
            if (dsHetHanHopDong != null)
            {
                try
                {
                    if (DialogUtil.ShowYesNo("Bạn thật sự muốn lập quyết định chấm dứt hợp đồng cho nhân viên đã chọn?") == DialogResult.Yes)
                    {
                        foreach (ChiTietHetHanHopDong item in dsHetHanHopDong.ChiTietHetHanHopDongList)
                        {
                            if (dsHetHanHopDong.ChiTietHetHanHopDongList.Count > 0 && item != null && item.Chon)
                            {
                                hopDong = obs.GetObjectByKey<HopDong>(item.HopDong.Oid);
                                if (item.HopDong is HopDongLamViec)
                                    hopDongLamViec = obs.GetObjectByKey<HopDongLamViec>(item.HopDong.Oid);
                                else if (item.HopDong is HopDongKhoan)
                                    hopDongKhoan = obs.GetObjectByKey<HopDongKhoan>(item.HopDong.Oid);
                                break;
                            }                           
                        }
                        if (hopDong == null)
                            DialogUtil.ShowError("Vui lòng nhấn chọn dòng Hợp đồng cần tạo!");                            
                        else
                        {
                            QuyetDinhChamDutHopDong qdChamDutHopDong = obs.CreateObject<QuyetDinhChamDutHopDong>();
                            nhanVien = obs.GetObjectByKey<ThongTinNhanVien>(hopDong.ThongTinNhanVien.Oid);
                            qdChamDutHopDong.BoPhan = obs.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                            qdChamDutHopDong.CongTyCu = obs.GetObjectByKey<CongTy>(nhanVien.CongTy.Oid);
                            qdChamDutHopDong.CongTy = obs.GetObjectByKey<CongTy>(nhanVien.CongTy.Oid);
                            qdChamDutHopDong.ThongTinNhanVien = nhanVien;
                            if (nhanVien.CongTy.LoaiTruong != Enum.TuyenSinh_PT.LoaiTruongEnum.NA)
                                qdChamDutHopDong.LoaiQuyetDinh = Enum.NhanSu.LoaiQuyetDinhEnum.HieuTruong;
                            else
                                qdChamDutHopDong.LoaiQuyetDinh = Enum.NhanSu.LoaiQuyetDinhEnum.TongGiamDoc;

                            if (hopDongLamViec != null)
                            {
                                qdChamDutHopDong.NgayQuyetDinh = hopDongLamViec.DenNgay.AddDays(1);
                                qdChamDutHopDong.NgayHieuLuc = hopDongLamViec.DenNgay.AddDays(1);
                            }
                            else if (hopDongKhoan != null)
                            {
                                qdChamDutHopDong.NgayQuyetDinh = hopDongKhoan.DenNgay.AddDays(1);
                                qdChamDutHopDong.NgayHieuLuc = hopDongKhoan.DenNgay.AddDays(1);
                            }
                            qdChamDutHopDong.TenCongTy = hopDong.TenCongTy;
                            if (hopDong.LoaiCongTy != null)
                                qdChamDutHopDong.LoaiCongTy = obs.GetObjectByKey<LoaiCongTy>(hopDong.LoaiCongTy.Oid);
                            if (hopDong.PhanLoaiNguoiKy != null)
                                qdChamDutHopDong.PhanLoaiNguoiKy = obs.GetObjectByKey<PhanLoaiNguoiKy>(hopDong.PhanLoaiNguoiKy.Oid);
                            if (hopDong.ChucVuNguoiKy != null)
                                qdChamDutHopDong.ChucVuNguoiKy = obs.GetObjectByKey<ChucVuNguoiKy>(hopDong.ChucVuNguoiKy.Oid);
                            if (hopDong.NguoiKy != null)
                                qdChamDutHopDong.NguoiKy = obs.GetObjectByKey<ThongTinNhanVien>(hopDong.NguoiKy.Oid);
                            qdChamDutHopDong.CanCu = hopDong.CanCu;
                            qdChamDutHopDong.LyDo = Enum.NhanSu.LyDoChamDutHopDongEnum.TheoNguyenVong;
                            qdChamDutHopDong.HopDong = obs.GetObjectByKey<HopDong>(hopDong.Oid);
                            qdChamDutHopDong.NgayKyThoaThuan = hopDong.ThongTinNhanVien.NgayVaoCongTy;

                            e.ShowViewParameters.Context = TemplateContext.View;
                            e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                            e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, qdChamDutHopDong);                            
                        }
                        e.ShowViewParameters.CreatedView.ObjectSpace.Committed += obs_Committed;
                    }
                }
                catch (Exception ex) { }
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
            simpleAction1.Active.Clear();
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<DanhSachHetHanHopDong>();
        }
    }
}
