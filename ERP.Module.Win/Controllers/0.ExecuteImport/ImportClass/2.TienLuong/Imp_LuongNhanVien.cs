using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
using ERP.Module.Extends;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ERP.Module.NghiepVu.TienLuong.Luong;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.TienLuong.ThuNhapKhac;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.TienLuong.NgoaiGio;
using ERP.Module.NghiepVu.TienLuong.KhauTru;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong
{
    public class Imp_LuongNhanVien
    {        
        public static void ImportLuongNhanVien(IObjectSpace obs, HoSoTinhLuong obj, LoaiOfficeEnum loaiOffice)
        {
            //
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            StringBuilder mainLog = new StringBuilder();
            DateTime ngayLap = DateTime.Now;
            if (obj.KyTinhLuong != null)
                ngayLap = obj.KyTinhLuong.DenNgay.AddDays(1);
            //

            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel 2003 file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet$A6:AU]", loaiOffice))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            int idx_STT = 0;
                            int idx_MaQuanLy = 1;
                            int idx_HoTen = 2;
                            int idx_DonVi = 4;
                            int idx_LuongChucDanhThuan = 7;
                            int idx_LuongBoSungThuan = 8;                            
                            int idx_NgayCongTinhLuong = 11;
                            int idx_PhanTramTinhLuong = 12;
                            int idx_LuongChucDanh = 13;
                            int idx_LuongBoSung = 14;
                            int idx_KiemNhiem = 16;
                            int idx_ChuNhiem = 17;
                            int idx_BanTru = 18;
                            int idx_TienComKhongThue = 19;
                            int idx_NhaOKhongThue = 20;
                            int idx_DongPhucKhongThue =21;
                            int idx_TruyLinhKhongThue = 22;
                            int idx_NgoaiGioKhongThue = 23;                            
                            int idx_TienComChiuThue = 24;
                            int idx_NgoaiGioChiuThue = 25;
                            int idx_TruyLinhChiuThue = 26;
                            int idx_DienThoaiChiuThue = 27;
                            int idx_UuDaiHocPhiChiuThue = 28;
                            int idx_LuongThang13 = 29;
                            int idx_BaoHiemCongTy = 32;
                            int idx_CongDoanCongTy = 33;
                            int idx_GiamTruBanThan = 34;
                            int idx_GiamTruGiaCanh = 35;
                            int idx_ThuNhapChiuThueKhongTienNha = 36; //Nguyen
                            int idx_ThuNhapChiuThue = 37; //Nguyen
                            int idx_ThuNhapTinhThue = 38; //Nguyen
                            int idx_ThueTNCN = 39; //Nguyen
                            int idx_BaoHiemNhanVien = 40;
                            int idx_CongDoanNhanVien = 41;
                            int idx_TruyThuKhongThue = 42;
                            int idx_TruyThuChiuThue = 43;
                            int idx_KhauTruUuDaiHocPhi = 44;
                            int idx_ThucNhan = 45; //Nguyen
                            int idx_GhiChu = 46;                            
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////
                            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();                              

                                #region Mã phân bổ                                
                                string txt_LuongChucDanh_MPB = string.Empty;
                                string txt_LuongBoSung_MPB = string.Empty;
                                string txt_KiemNhiem_MPB = string.Empty;
                                string txt_ChuNhiem_MPB = string.Empty;
                                string txt_BanTru_MPB = string.Empty;
                                string txt_TienComKhongThue_MPB = string.Empty;
                                string txt_NhaOKhongThue_MPB = string.Empty;
                                string txt_DongPhucKhongThue_MPB = string.Empty;
                                string txt_TruyLinhKhongThue_MPB = string.Empty;
                                string txt_NgoaiGioKhongThue_MPB = string.Empty;
                                string txt_TienComChiuThue_MPB = string.Empty;
                                string txt_NgoaiGioChiuThue_MPB = string.Empty;
                                string txt_TruyLinhChiuThue_MPB = string.Empty;
                                string txt_DienThoaiChiuThue_MPB = string.Empty;
                                string txt_UuDaiHocPhiChiuThue_MPB = string.Empty;
                                string txt_LuongThang13_MPB = string.Empty;
                                string txt_BaoHiemCongTy_MPB = string.Empty;
                                string txt_CongDoanCongTy_MPB = string.Empty;
                                string txt_BaoHiemNhanVien_MPB = string.Empty;
                                string txt_CongDoanNhanVien_MPB = string.Empty;
                                string txt_TruyThuKhongThue_MPB = string.Empty;
                                string txt_TruyThuChiuThue_MPB = string.Empty;
                                string txt_KhauTruUuDaiHocPhi_MPB = string.Empty;                                
                                #endregion

                                //Duyệt qua tất cả các dòng trong file excel
                                foreach (DataRow dr in dt.Rows)
                                {
                                    StringBuilder detailLog = new StringBuilder();

                                    //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                    #region Đọc dữ liệu
                                    string txt_STT = dr[idx_STT].ToString().Trim();
                                    string txt_MaQuanLy = dr[idx_MaQuanLy].ToString().Trim();
                                    string txt_HoTen = dr[idx_HoTen].ToString().Trim();
                                    string txt_DonVi = dr[idx_DonVi].ToString().Trim();
                                    string txt_LuongChucDanhThuan = dr[idx_LuongChucDanhThuan].ToString().Trim();
                                    string txt_LuongBoSungThuan = dr[idx_LuongBoSungThuan].ToString().Trim();
                                    string txt_NgayCongTinhLuong = dr[idx_NgayCongTinhLuong].ToString().Trim();
                                    string txt_PhanTramTinhLuong = dr[idx_PhanTramTinhLuong].ToString().Trim();
                                    string txt_LuongChucDanh = dr[idx_LuongChucDanh].ToString().Trim();
                                    string txt_LuongBoSung = dr[idx_LuongBoSung].ToString().Trim();                                    
                                    string txt_KiemNhiem = dr[idx_KiemNhiem].ToString().Trim();
                                    string txt_ChuNhiem = dr[idx_ChuNhiem].ToString().Trim();
                                    string txt_BanTru = dr[idx_BanTru].ToString().Trim();
                                    string txt_TienComKhongThue = dr[idx_TienComKhongThue].ToString().Trim();
                                    string txt_NhaOKhongThue = dr[idx_NhaOKhongThue].ToString().Trim();
                                    string txt_DongPhucKhongThue = dr[idx_DongPhucKhongThue].ToString().Trim();
                                    string txt_TruyLinhKhongThue = dr[idx_TruyLinhKhongThue].ToString().Trim();
                                    string txt_NgoaiGioKhongThue = dr[idx_NgoaiGioKhongThue].ToString().Trim();
                                    string txt_TienComChiuThue = dr[idx_TienComChiuThue].ToString().Trim();
                                    string txt_NgoaiGioChiuThue = dr[idx_NgoaiGioChiuThue].ToString().Trim();
                                    string txt_TruyLinhChiuThue = dr[idx_TruyLinhChiuThue].ToString().Trim();
                                    string txt_DienThoaiChiuThue = dr[idx_DienThoaiChiuThue].ToString().Trim();
                                    string txt_UuDaiHocPhiChiuThue = dr[idx_UuDaiHocPhiChiuThue].ToString().Trim();
                                    string txt_LuongThang13 = dr[idx_LuongThang13].ToString().Trim();
                                    string txt_BaoHiemCongTy = dr[idx_BaoHiemCongTy].ToString().Trim();
                                    string txt_CongDoanCongTy = dr[idx_CongDoanCongTy].ToString().Trim();
                                    string txt_GiamTruBanThan = dr[idx_GiamTruBanThan].ToString().Trim();
                                    string txt_GiamTruGiaCanh = dr[idx_GiamTruGiaCanh].ToString().Trim();
                                    string txt_ThuNhapChiuThueKhongTienNha = dr[idx_ThuNhapChiuThueKhongTienNha].ToString().Trim(); //Nguyen
                                    string txt_ThuNhapChiuThue = dr[idx_ThuNhapChiuThue].ToString().Trim(); //Nguyen
                                    string txt_ThuNhapTinhThue = dr[idx_ThuNhapTinhThue].ToString().Trim(); //Nguyen
                                    string txt_ThueTNCN = dr[idx_ThueTNCN].ToString().Trim(); //Nguyen
                                    string txt_BaoHiemNhanVien = dr[idx_BaoHiemNhanVien].ToString().Trim();
                                    string txt_CongDoanNhanVien = dr[idx_CongDoanNhanVien].ToString().Trim();
                                    string txt_TruyThuKhongThue = dr[idx_TruyThuKhongThue].ToString().Trim();
                                    string txt_TruyThuChiuThue = dr[idx_TruyThuChiuThue].ToString().Trim();
                                    string txt_KhauTruUuDaiHocPhi = dr[idx_KhauTruUuDaiHocPhi].ToString().Trim();
                                    string txt_ThucNhan = dr[idx_ThucNhan].ToString().Trim(); //Nguyen
                                    string txt_GhiChu = dr[idx_GhiChu].ToString().Trim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                    #region Kiểm tra dữ
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaQuanLy))
                                    {
                                        ThongTinNhanVien nhanVien_qd = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaNhanVien like ? or MaTapDoan like ?", txt_MaQuanLy, txt_MaQuanLy));
                                        if (nhanVien_qd == null)
                                        {
                                            if (txt_MaQuanLy.Contains("Số TK") || txt_MaQuanLy.Contains("tài khoản"))
                                            {
                                                if (!string.IsNullOrEmpty(txt_LuongChucDanh))
                                                    txt_LuongChucDanh_MPB = txt_LuongChucDanh;
                                                if (!string.IsNullOrEmpty(txt_LuongBoSung))
                                                    txt_LuongBoSung_MPB = txt_LuongBoSung;
                                                if (!string.IsNullOrEmpty(txt_KiemNhiem))
                                                    txt_KiemNhiem_MPB = txt_KiemNhiem;
                                                if (!string.IsNullOrEmpty(txt_ChuNhiem))
                                                    txt_ChuNhiem_MPB = txt_ChuNhiem;
                                                if (!string.IsNullOrEmpty(txt_BanTru))
                                                    txt_BanTru_MPB = txt_BanTru;
                                                if (!string.IsNullOrEmpty(txt_TienComKhongThue))
                                                    txt_TienComKhongThue_MPB = txt_TienComKhongThue;
                                                if (!string.IsNullOrEmpty(txt_NhaOKhongThue))
                                                    txt_NhaOKhongThue_MPB = txt_NhaOKhongThue;
                                                if (!string.IsNullOrEmpty(txt_DongPhucKhongThue))
                                                    txt_DongPhucKhongThue_MPB = txt_DongPhucKhongThue;
                                                if (!string.IsNullOrEmpty(txt_TruyLinhKhongThue))
                                                    txt_TruyLinhKhongThue_MPB = txt_TruyLinhKhongThue;
                                                if (!string.IsNullOrEmpty(txt_NgoaiGioKhongThue))
                                                    txt_NgoaiGioKhongThue_MPB = txt_NgoaiGioKhongThue;
                                                if (!string.IsNullOrEmpty(txt_TienComChiuThue))
                                                    txt_TienComChiuThue_MPB = txt_TienComChiuThue;
                                                if (!string.IsNullOrEmpty(txt_NgoaiGioChiuThue))
                                                    txt_NgoaiGioChiuThue_MPB = txt_NgoaiGioChiuThue;
                                                if (!string.IsNullOrEmpty(txt_TruyLinhChiuThue))
                                                    txt_TruyLinhChiuThue_MPB = txt_TruyLinhChiuThue;
                                                if (!string.IsNullOrEmpty(txt_DienThoaiChiuThue))
                                                    txt_DienThoaiChiuThue_MPB = txt_DienThoaiChiuThue;
                                                if (!string.IsNullOrEmpty(txt_UuDaiHocPhiChiuThue))
                                                    txt_UuDaiHocPhiChiuThue_MPB = txt_UuDaiHocPhiChiuThue;
                                                if (!string.IsNullOrEmpty(txt_LuongThang13))
                                                    txt_LuongThang13_MPB = txt_LuongThang13;
                                                if (!string.IsNullOrEmpty(txt_BaoHiemCongTy))
                                                    txt_BaoHiemCongTy_MPB = txt_BaoHiemCongTy;
                                                if (!string.IsNullOrEmpty(txt_CongDoanCongTy))
                                                    txt_CongDoanCongTy_MPB = txt_CongDoanCongTy;
                                                if (!string.IsNullOrEmpty(txt_BaoHiemNhanVien))
                                                    txt_BaoHiemNhanVien_MPB = txt_BaoHiemNhanVien;
                                                if (!string.IsNullOrEmpty(txt_CongDoanNhanVien))
                                                    txt_CongDoanNhanVien_MPB = txt_CongDoanNhanVien;
                                                if (!string.IsNullOrEmpty(txt_TruyThuKhongThue))
                                                    txt_TruyThuKhongThue_MPB = txt_TruyThuKhongThue;
                                                if (!string.IsNullOrEmpty(txt_TruyThuChiuThue))
                                                    txt_TruyThuChiuThue_MPB = txt_TruyThuChiuThue;
                                                if (!string.IsNullOrEmpty(txt_KhauTruUuDaiHocPhi))
                                                    txt_KhauTruUuDaiHocPhi_MPB = txt_KhauTruUuDaiHocPhi;
                                            }
                                            else
                                            {
                                                mainLog.AppendLine("- STT: " + txt_STT);
                                                mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống.", txt_MaQuanLy, txt_HoTen));
                                                //
                                                sucessImport = false;
                                            }
                                        }
                                        else
                                        {
                                            //
                                            ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("CongTy = ? and (MaNhanVien like ? or MaTapDoan like ?)", obj.CongTy.Oid, txt_MaQuanLy, txt_MaQuanLy));
                                            if (!string.IsNullOrEmpty(txt_GhiChu))
                                            {
                                                nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("CongTy.MaBoPhan like ? and (MaNhanVien like ? or MaTapDoan like ?)", txt_GhiChu, txt_MaQuanLy, txt_MaQuanLy));
                                            }
                                            if (nhanVien != null)
                                            {
                                                BangLuongNhanVien bangLuongNhanVien = uow.FindObject<BangLuongNhanVien>(CriteriaOperator.Parse("KyTinhLuong=?", obj.KyTinhLuong.Oid));
                                                if (bangLuongNhanVien == null)
                                                {
                                                    bangLuongNhanVien = new BangLuongNhanVien(uow);
                                                    bangLuongNhanVien.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                    bangLuongNhanVien.NgayLap = ngayLap;
                                                    bangLuongNhanVien.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);                                                    
                                                }
                                                LuongNhanVien luongNhanVien = uow.FindObject<LuongNhanVien>(CriteriaOperator.Parse("BangLuongNhanVien=? and ThongTinNhanVien=?", bangLuongNhanVien.Oid, nhanVien.Oid));
                                                if (luongNhanVien == null)
                                                {
                                                    luongNhanVien = new LuongNhanVien(uow);
                                                    luongNhanVien.BangLuongNhanVien = bangLuongNhanVien;
                                                    luongNhanVien.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                    luongNhanVien.ThongTinNhanVien = nhanVien;
                                                }
                                                if (nhanVien.NhomPhanBo != null)
                                                    luongNhanVien.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);

                                                ChiTietLuong ctl = null;                                                
                                                ctl = uow.FindObject<ChiTietLuong>(CriteriaOperator.Parse("HoSoTinhLuong=? and ThongTinNhanVien=?", obj.Oid, nhanVien.Oid));
                                                if (ctl != null)
                                                    luongNhanVien.ChiTietLuong = ctl;
                                                else
                                                    detailLog.Append("+ Không tìm thấy chi tiết lương trong Hồ sơ tính lương");

                                                #region Phần trăm tính lương
                                                decimal phanTramTinhLuong = 0;
                                                if (!string.IsNullOrEmpty(txt_PhanTramTinhLuong) && ctl != null)
                                                {
                                                    try
                                                    {
                                                        phanTramTinhLuong = Convert.ToDecimal(txt_PhanTramTinhLuong);
                                                        if (phanTramTinhLuong <= 1)
                                                            ctl.PhanTramTinhLuong = phanTramTinhLuong * 100;
                                                        else
                                                            ctl.PhanTramTinhLuong = phanTramTinhLuong;
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Phần trăm tính lương (12) không đúng định dạng: " + txt_PhanTramTinhLuong);
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.Append("+ Phần trăm tính lương (12) không được trống");
                                                }
                                                #endregion

                                                #region Giảm trừ gia cảnh
                                                decimal giamTruGiaCanh = 0;
                                                if (!string.IsNullOrEmpty(txt_GiamTruGiaCanh) && ctl != null)
                                                {
                                                    try
                                                    {
                                                        giamTruGiaCanh = Convert.ToDecimal(txt_GiamTruGiaCanh);
                                                        ctl.SoNguoiPhuThuoc = Convert.ToInt32(giamTruGiaCanh / 4400000); //3600000    old                                                   
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Giảm trừ gia cảnh (35) không đúng định dạng: " + txt_GiamTruGiaCanh);
                                                    }
                                                }                                               
                                                #endregion

                                                #region Ngày công tính lương
                                                decimal ngayCongTinhLuong = 0;
                                                if (!string.IsNullOrEmpty(txt_NgayCongTinhLuong) && ctl != null)
                                                {
                                                    try
                                                    {
                                                        ngayCongTinhLuong = Convert.ToDecimal(txt_NgayCongTinhLuong);                                                      
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Ngày công tính lương (11) không đúng định dạng: " + txt_NgayCongTinhLuong);
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.Append("+ Ngày công tính lương (11) không được trống");
                                                }
                                                #endregion

                                                #region Lương chức danh
                                                decimal luongChucDanhThuan = 0;
                                                if (!string.IsNullOrEmpty(txt_LuongChucDanh) && !string.IsNullOrEmpty(txt_LuongChucDanhThuan))
                                                {                                                    
                                                    try
                                                    {                                                       
                                                        decimal luongChucDanh = Convert.ToDecimal(txt_LuongChucDanh);
                                                        luongChucDanhThuan = Convert.ToDecimal(txt_LuongChucDanhThuan);
                                                        if (luongChucDanh > 0)
                                                        {
                                                            ChiTietLuongNhanVien ctLuongNhanVien = uow.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien=? and MaChiTiet=?", luongNhanVien.Oid, "LuongChucDanh"));
                                                            if (ctLuongNhanVien == null)
                                                            {
                                                                ctLuongNhanVien = new ChiTietLuongNhanVien(uow);
                                                                ctLuongNhanVien.LuongNhanVien = luongNhanVien;
                                                                ctLuongNhanVien.MaChiTiet = "LuongChucDanh";
                                                                ctLuongNhanVien.DienGiai = "Lương chức danh";
                                                                ctLuongNhanVien.CongTru = Enum.NhanSu.CongTruEnum.Cong;
                                                                ctLuongNhanVien.GhiChu = "Nhập từ file excel";
                                                            }
                                                            ctLuongNhanVien.CostCenter = txt_LuongChucDanh_MPB;
                                                            ctLuongNhanVien.TienLuong = luongChucDanhThuan;
                                                            ctLuongNhanVien.TongNgayCong = ngayCongTinhLuong;
                                                            ctLuongNhanVien.SoTien = luongChucDanh;
                                                            ctLuongNhanVien.SoTienChiuThue = luongChucDanh;
                                                        }
                                                        if (ctl != null)
                                                            ctl.LuongCoBan = luongChucDanhThuan;
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Lương chức danh thuần (7) và Lương chức danh tính theo ngày công (13) không đúng định dạng: " + txt_LuongChucDanh);
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.Append("+ Lương chức danh thuần (7) và Lương chức danh tính theo ngày công (13) không được trống");
                                                }
                                                #endregion

                                                #region Lương bổ sung
                                                if (!string.IsNullOrEmpty(txt_LuongBoSungThuan) && !string.IsNullOrEmpty(txt_LuongBoSung))
                                                {
                                                    try
                                                    {
                                                        decimal luongBoSung = Convert.ToDecimal(txt_LuongBoSung);
                                                        decimal luongBoSungThuan = Convert.ToDecimal(txt_LuongBoSungThuan);
                                                        if (luongBoSung > 0)
                                                        {
                                                            ChiTietLuongNhanVien ctLuongNhanVien = uow.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien=? and MaChiTiet=?", luongNhanVien.Oid, "LuongHieuQuaCongViec"));
                                                            if (ctLuongNhanVien == null)
                                                            {
                                                                ctLuongNhanVien = new ChiTietLuongNhanVien(uow);
                                                                ctLuongNhanVien.LuongNhanVien = luongNhanVien;
                                                                ctLuongNhanVien.MaChiTiet = "LuongHieuQuaCongViec";
                                                                ctLuongNhanVien.DienGiai = "Lương hiệu quả công việc";
                                                                ctLuongNhanVien.CongTru = Enum.NhanSu.CongTruEnum.Cong;
                                                                ctLuongNhanVien.GhiChu = "Nhập từ file excel";
                                                            }
                                                            ctLuongNhanVien.CostCenter = txt_LuongBoSung_MPB;
                                                            ctLuongNhanVien.TienLuong = luongBoSungThuan;
                                                            ctLuongNhanVien.TongNgayCong = ngayCongTinhLuong;
                                                            ctLuongNhanVien.SoTien = luongBoSung;
                                                            ctLuongNhanVien.SoTienChiuThue = luongBoSung;
                                                        }
                                                        if (ctl != null)
                                                            ctl.LuongKinhDoanh = luongBoSungThuan;
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Lương bổ sung thuần (8) và Lương bổ sung tính theo ngày công (14) không đúng định dạng: " + txt_LuongChucDanh);
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.Append("+ Lương bổ sung thuần (8) và Lương bổ sung tính theo ngày công (14) không được trống");
                                                }
                                                #endregion

                                                #region Phụ cấp kiêm nhiệm
                                                if (!string.IsNullOrEmpty(txt_KiemNhiem))
                                                {
                                                    try
                                                    {
                                                        decimal kiemNhiem = Convert.ToDecimal(txt_KiemNhiem);
                                                        if (kiemNhiem > 0)
                                                        {
                                                            ChiTietLuongNhanVien ctLuongNhanVien = uow.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien=? and MaChiTiet=?", luongNhanVien.Oid, "PhuCapKiemNhiem"));
                                                            if (ctLuongNhanVien == null)
                                                            {
                                                                ctLuongNhanVien = new ChiTietLuongNhanVien(uow);
                                                                ctLuongNhanVien.LuongNhanVien = luongNhanVien;
                                                                ctLuongNhanVien.MaChiTiet = "PhuCapKiemNhiem";
                                                                ctLuongNhanVien.DienGiai = "Phụ cấp kiêm nhiệm";
                                                                ctLuongNhanVien.CongTru = Enum.NhanSu.CongTruEnum.Cong;
                                                                ctLuongNhanVien.GhiChu = "Nhập từ file excel";
                                                            }
                                                            ctLuongNhanVien.CostCenter = txt_KiemNhiem_MPB;
                                                            ctLuongNhanVien.TienLuong = kiemNhiem;
                                                            ctLuongNhanVien.TongNgayCong = ngayCongTinhLuong;
                                                            ctLuongNhanVien.SoTien = kiemNhiem;
                                                            ctLuongNhanVien.SoTienChiuThue = kiemNhiem;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Phụ cấp kiêm nhiệm (16) không đúng định dạng: " + txt_KiemNhiem);
                                                    }
                                                }                                             
                                                #endregion

                                                #region Phụ cấp chủ nhiệm
                                                if (!string.IsNullOrEmpty(txt_ChuNhiem))
                                                {
                                                    try
                                                    {
                                                        decimal chuNhiem = Convert.ToDecimal(txt_ChuNhiem);
                                                        if (chuNhiem > 0)
                                                        {
                                                            ChiTietLuongNhanVien ctLuongNhanVien = uow.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien=? and MaChiTiet=?", luongNhanVien.Oid, "PhuCapTrachNhiem"));
                                                            if (ctLuongNhanVien == null)
                                                            {
                                                                ctLuongNhanVien = new ChiTietLuongNhanVien(uow);
                                                                ctLuongNhanVien.LuongNhanVien = luongNhanVien;
                                                                ctLuongNhanVien.MaChiTiet = "PhuCapTrachNhiem";
                                                                ctLuongNhanVien.DienGiai = "Phụ cấp chủ nhiệm";
                                                                ctLuongNhanVien.CongTru = Enum.NhanSu.CongTruEnum.Cong;
                                                                ctLuongNhanVien.GhiChu = "Nhập từ file excel";
                                                            }
                                                            ctLuongNhanVien.CostCenter = txt_ChuNhiem_MPB;
                                                            ctLuongNhanVien.TienLuong = chuNhiem;
                                                            ctLuongNhanVien.TongNgayCong = ngayCongTinhLuong;
                                                            ctLuongNhanVien.SoTien = chuNhiem;
                                                            ctLuongNhanVien.SoTienChiuThue = chuNhiem;
                                                        }
                                                        
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Phụ cấp chủ nhiệm (17) không đúng định dạng: " + txt_ChuNhiem);
                                                    }
                                                }                                               
                                                #endregion                                       
                                           
                                                #region Phụ cấp bán trú
                                                if (!string.IsNullOrEmpty(txt_BanTru))
                                                {
                                                    try
                                                    {
                                                        decimal banTru = Convert.ToDecimal(txt_BanTru);
                                                        if (banTru > 0)
                                                        {
                                                            ChiTietLuongNhanVien ctLuongNhanVien = uow.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien=? and MaChiTiet=?", luongNhanVien.Oid, "PhuCapBanTru"));
                                                            if (ctLuongNhanVien == null)
                                                            {
                                                                ctLuongNhanVien = new ChiTietLuongNhanVien(uow);
                                                                ctLuongNhanVien.LuongNhanVien = luongNhanVien;
                                                                ctLuongNhanVien.MaChiTiet = "PhuCapBanTru";
                                                                ctLuongNhanVien.DienGiai = "Phụ cấp bán trú";
                                                                ctLuongNhanVien.CongTru = Enum.NhanSu.CongTruEnum.Cong;
                                                                ctLuongNhanVien.GhiChu = "Nhập từ file excel";
                                                            }
                                                            ctLuongNhanVien.CostCenter = txt_BanTru_MPB;
                                                            ctLuongNhanVien.TienLuong = banTru;
                                                            ctLuongNhanVien.TongNgayCong = ngayCongTinhLuong;
                                                            ctLuongNhanVien.SoTien = banTru;
                                                            ctLuongNhanVien.SoTienChiuThue = banTru;
                                                        }                                                  
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Phụ cấp bán trú (18) không đúng định dạng: " + txt_BanTru);
                                                    }
                                                }
                                                #endregion

                                                #region Tiền cơm
                                                decimal tienComChiuThue = 0;
                                                decimal tienComKhongThue = 0;
                                                if (!string.IsNullOrEmpty(txt_TienComChiuThue) || !string.IsNullOrEmpty(txt_TienComKhongThue))
                                                {
                                                    try
                                                    {
                                                        if (!string.IsNullOrEmpty(txt_TienComChiuThue))
                                                            tienComChiuThue = Convert.ToDecimal(txt_TienComChiuThue);
                                                        if (!string.IsNullOrEmpty(txt_TienComKhongThue))
                                                            tienComKhongThue = Convert.ToDecimal(txt_TienComKhongThue);

                                                        if (tienComChiuThue > 0 || tienComKhongThue > 0)
                                                        {
                                                            ChiTietLuongNhanVien ctLuongNhanVien = uow.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien=? and MaChiTiet=?", luongNhanVien.Oid, "PhuCapTienAn"));
                                                            if (ctLuongNhanVien == null)
                                                            {
                                                                ctLuongNhanVien = new ChiTietLuongNhanVien(uow);
                                                                ctLuongNhanVien.LuongNhanVien = luongNhanVien;
                                                                ctLuongNhanVien.MaChiTiet = "PhuCapTienAn";
                                                                ctLuongNhanVien.DienGiai = "Phụ cấp tiền ăn";
                                                                ctLuongNhanVien.CongTru = Enum.NhanSu.CongTruEnum.Cong;
                                                                ctLuongNhanVien.GhiChu = "Nhập từ file excel";
                                                            }
                                                            ctLuongNhanVien.CostCenter = txt_TienComKhongThue_MPB;
                                                            ctLuongNhanVien.TienLuong = tienComChiuThue + tienComKhongThue;
                                                            ctLuongNhanVien.TongNgayCong = ngayCongTinhLuong;
                                                            ctLuongNhanVien.SoTien = tienComChiuThue + tienComKhongThue;
                                                            ctLuongNhanVien.SoTienChiuThue = tienComChiuThue;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Tiền cơm không thuế (19) hoặc Tiền cơm chịu thuế (24) không đúng định dạng: " + txt_TienComChiuThue);
                                                    }
                                                }
                                                #endregion

                                                #region Nhà ở                                                                           
                                                if (!string.IsNullOrEmpty(txt_NhaOKhongThue))
                                                {
                                                    try
                                                    {
                                                        decimal nhaOKhongThue = Convert.ToDecimal(txt_NhaOKhongThue);
                                                        if (nhaOKhongThue > 0)
                                                        {
                                                            ChiTietLuongNhanVien ctLuongNhanVien = uow.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien=? and MaChiTiet=?", luongNhanVien.Oid, "PhuCapNhaO"));
                                                            if (ctLuongNhanVien == null)
                                                            {
                                                                ctLuongNhanVien = new ChiTietLuongNhanVien(uow);
                                                                ctLuongNhanVien.LuongNhanVien = luongNhanVien;
                                                                ctLuongNhanVien.MaChiTiet = "PhuCapNhaO";
                                                                ctLuongNhanVien.DienGiai = "Phụ cấp nhà ở";
                                                                ctLuongNhanVien.CongTru = Enum.NhanSu.CongTruEnum.Cong;
                                                                ctLuongNhanVien.GhiChu = "Nhập từ file excel";
                                                            }
                                                            ctLuongNhanVien.CostCenter = txt_NhaOKhongThue_MPB;
                                                            ctLuongNhanVien.TienLuong = nhaOKhongThue;
                                                            ctLuongNhanVien.TongNgayCong = ngayCongTinhLuong;
                                                            ctLuongNhanVien.SoTien = nhaOKhongThue;
                                                            ctLuongNhanVien.SoTienChiuThue = 0;
                                                        }                                                     
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ PC nhà ở (20) không đúng định dạng: " + txt_NhaOKhongThue);
                                                    }
                                                }
                                                #endregion

                                                #region PC đồng phục 
                                                BangThuNhapKhac bangTNK_TienDongPhuc;
                                                if (!string.IsNullOrEmpty(txt_DongPhucKhongThue))
                                                {
                                                    try
                                                    {
                                                        decimal dongPhuc = Convert.ToDecimal(txt_DongPhucKhongThue);
                                                        if (dongPhuc > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienDongPhuc"));
                                                            if (loaiTNK != null)
                                                            {
                                                                bangTNK_TienDongPhuc = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK_TienDongPhuc == null)
                                                                {
                                                                    bangTNK_TienDongPhuc = new BangThuNhapKhac(uow);
                                                                    bangTNK_TienDongPhuc.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK_TienDongPhuc.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK_TienDongPhuc.NgayLap = ngayLap;
                                                                    bangTNK_TienDongPhuc.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                    //bangTNK_TienDongPhuc.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    //bangTNK_TienDongPhuc.LoaiThuNhapKhac = loaiTNK;
                                                                    //bangTNK_TienDongPhuc.NgayLap = ngayLap;
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK_TienDongPhuc.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK_TienDongPhuc;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel_TienDongPhuc";//Nguyen
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = dongPhuc;
                                                                chiTietTNK.SoTienChiuThue = 0;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy PC đồng phục (19) trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ PC đồng phục (19) không đúng định dạng: " + txt_DongPhucKhongThue);
                                                    }
                                                }
                                                #endregion

                                                #region Điện thoại                                                                           
                                                if (!string.IsNullOrEmpty(txt_DienThoaiChiuThue))
                                                {
                                                    try
                                                    {
                                                        decimal dienThoai = Convert.ToDecimal(txt_DienThoaiChiuThue);
                                                        if (dienThoai > 0)
                                                        {
                                                            ChiTietLuongNhanVien ctLuongNhanVien = uow.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien=? and MaChiTiet=?", luongNhanVien.Oid, "PhuCapDienThoai"));
                                                            if (ctLuongNhanVien == null)
                                                            {
                                                                ctLuongNhanVien = new ChiTietLuongNhanVien(uow);
                                                                ctLuongNhanVien.LuongNhanVien = luongNhanVien;
                                                                ctLuongNhanVien.MaChiTiet = "PhuCapDienThoai";
                                                                ctLuongNhanVien.DienGiai = "Phụ cấp điện thoại";
                                                                ctLuongNhanVien.CongTru = Enum.NhanSu.CongTruEnum.Cong;
                                                                ctLuongNhanVien.GhiChu = "Nhập từ file excel";
                                                            }
                                                            ctLuongNhanVien.CostCenter = txt_DienThoaiChiuThue_MPB;
                                                            ctLuongNhanVien.TienLuong = dienThoai;
                                                            ctLuongNhanVien.TongNgayCong = ngayCongTinhLuong;
                                                            ctLuongNhanVien.SoTien = dienThoai;
                                                            ctLuongNhanVien.SoTienChiuThue = dienThoai;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ PC điện thoại (19) không đúng định dạng: " + txt_DienThoaiChiuThue);
                                                    }
                                                }
                                                #endregion

                                                #region Ưu đãi học phí nội bộ                                                 
                                                if (!string.IsNullOrEmpty(txt_UuDaiHocPhiChiuThue))
                                                {
                                                    try
                                                    {                                                       
                                                        decimal uUDaiHocPhi = Convert.ToDecimal(txt_UuDaiHocPhiChiuThue);
                                                        if (uUDaiHocPhi > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "UuDaiHocPhiNoiBo"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK_UuDaiHocPhi = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK_UuDaiHocPhi == null)
                                                                {
                                                                    bangTNK_UuDaiHocPhi = new BangThuNhapKhac(uow);                                                                   
                                                                    bangTNK_UuDaiHocPhi.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK_UuDaiHocPhi.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK_UuDaiHocPhi.NgayLap = ngayLap;
                                                                    bangTNK_UuDaiHocPhi.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);                                                                   
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK_UuDaiHocPhi.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK_UuDaiHocPhi;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = uUDaiHocPhi;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Ưu đãi học phí nội bộ (27) trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Ưu đãi học phí nội bộ (27) không đúng định dạng: " + txt_UuDaiHocPhiChiuThue);
                                                    }
                                                }
                                                #endregion

                                                #region Lương tháng 13 
                                                if (!string.IsNullOrEmpty(txt_LuongThang13))
                                                {
                                                    try
                                                    {
                                                        decimal luongthang13 = Convert.ToDecimal(txt_LuongThang13);
                                                        if (luongthang13 > 0)
                                                        {                                                            
                                                            LoaiThuNhapKhac loaiTNK_LuongThang13 = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "LuongThang13"));
                                                            if (loaiTNK_LuongThang13 != null)
                                                            {
                                                                BangThuNhapKhac bangTNK_LuongThang13 = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK_LuongThang13.Oid));
                                                                if (bangTNK_LuongThang13 == null)
                                                                {
                                                                    bangTNK_LuongThang13 = new BangThuNhapKhac(uow);                                                                    
                                                                    bangTNK_LuongThang13.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK_LuongThang13.LoaiThuNhapKhac = uow.GetObjectByKey<LoaiThuNhapKhac>(loaiTNK_LuongThang13.Oid);
                                                                    bangTNK_LuongThang13.NgayLap = ngayLap;
                                                                    bangTNK_LuongThang13.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK_LuongThang13.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);                                                                   
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK_LuongThang13;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";                                                                    
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = luongthang13;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Lương tháng 13/Thưởng danh hiệu thi đua trong Loại thu nhập khác");
                                                            }                                                      
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Lương tháng 13/Thưởng danh hiệu thi đua (28) không đúng định dạng: " + txt_LuongThang13);
                                                    }
                                                }
                                                #endregion

                                                #region Truy lĩnh                                              
                                                if (!string.IsNullOrEmpty(txt_TruyLinhKhongThue) || !string.IsNullOrEmpty(txt_TruyLinhChiuThue))
                                                {
                                                    try
                                                    {                                                        
                                                        decimal truyLinhKhongThue = 0;
                                                        decimal truyLinhChiuThue = 0;
                                                        if (!string.IsNullOrEmpty(txt_TruyLinhKhongThue))
                                                            truyLinhKhongThue = Convert.ToDecimal(txt_TruyLinhKhongThue);
                                                        if (!string.IsNullOrEmpty(txt_TruyLinhChiuThue))
                                                            truyLinhChiuThue = Convert.ToDecimal(txt_TruyLinhChiuThue);

                                                        if (truyLinhKhongThue > 0 || truyLinhChiuThue > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TruyLinhLuong"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK_TruyLinh = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK_TruyLinh == null)
                                                                {
                                                                    bangTNK_TruyLinh = new BangThuNhapKhac(uow);                                                                    
                                                                    bangTNK_TruyLinh.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK_TruyLinh.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK_TruyLinh.NgayLap = ngayLap;
                                                                    bangTNK_TruyLinh.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK_TruyLinh.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);                                                                    
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK_TruyLinh;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = truyLinhKhongThue;
                                                                chiTietTNK.SoTienChiuThue = truyLinhChiuThue;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Truy lĩnh lương trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Truy lĩnh/Bổ sung không thuế (22) hoặc Truy lĩnh/Bổ sung chịu thuế (26) không đúng định dạng: " + txt_TruyLinhKhongThue);
                                                    }
                                                }
                                                #endregion

                                                #region Ngoài giờ                                                                                        
                                                if (!string.IsNullOrEmpty(txt_NgoaiGioKhongThue) || !string.IsNullOrEmpty(txt_NgoaiGioChiuThue))
                                                {
                                                    try
                                                    {                                                       
                                                        decimal ngoaiGioKhongThue = 0;
                                                        decimal ngoaiGioChiuThue = 0;
                                                        if (!string.IsNullOrEmpty(txt_NgoaiGioKhongThue))
                                                            ngoaiGioKhongThue = Convert.ToDecimal(txt_NgoaiGioKhongThue);
                                                        if (!string.IsNullOrEmpty(txt_NgoaiGioChiuThue))
                                                            ngoaiGioChiuThue = Convert.ToDecimal(txt_NgoaiGioChiuThue);

                                                        if (ngoaiGioKhongThue > 0 || ngoaiGioChiuThue > 0)
                                                        {
                                                            BangLuongNgoaiGio bangLuongNG = uow.FindObject<BangLuongNgoaiGio>(CriteriaOperator.Parse("KyTinhLuong=?", obj.KyTinhLuong.Oid));
                                                            if (bangLuongNG == null)
                                                            {
                                                                bangLuongNG = new BangLuongNgoaiGio(uow);                                                                
                                                                bangLuongNG.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                bangLuongNG.NgayLap = ngayLap;
                                                                bangLuongNG.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);                                                                
                                                            }
                                                            LuongNgoaiGio luongNgoaiGio = uow.FindObject<LuongNgoaiGio>(CriteriaOperator.Parse("BangLuongNgoaiGio=? and ThongTinNhanVien=?", bangLuongNG.Oid, nhanVien.Oid));
                                                            if (luongNgoaiGio == null)
                                                            {
                                                                luongNgoaiGio = new LuongNgoaiGio(uow);                                                                
                                                                luongNgoaiGio.ThongTinNhanVien = nhanVien;
                                                                luongNgoaiGio.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                luongNgoaiGio.BangLuongNgoaiGio = bangLuongNG;
                                                                luongNgoaiGio.GhiChu = "Nhập từ file excel";                                                                
                                                            }
                                                            if (nhanVien.NhomPhanBo != null)
                                                                luongNgoaiGio.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);

                                                            if (ctl != null)
                                                                luongNgoaiGio.ChiTietLuong = ctl;                                                           

                                                            ChiTietLuongNgoaiGio chiTietLNG = uow.FindObject<ChiTietLuongNgoaiGio>(CriteriaOperator.Parse("LuongNgoaiGio=? AND MaChiTiet=?", luongNgoaiGio.Oid, "LuongNgoaiGio"));
                                                            if (chiTietLNG == null)
                                                            {
                                                                chiTietLNG = new ChiTietLuongNgoaiGio(uow);
                                                                chiTietLNG.LuongNgoaiGio = luongNgoaiGio;
                                                                chiTietLNG.MaChiTiet = "LuongNgoaiGio";
                                                                chiTietLNG.DienGiai = "Lương ngoài giờ (OT)";
                                                                chiTietLNG.CongTru = Enum.NhanSu.CongTruEnum.Cong;
                                                            }
                                                            chiTietLNG.CostCenter = txt_NgoaiGioKhongThue_MPB;
                                                            chiTietLNG.SoTien = ngoaiGioKhongThue;
                                                            chiTietLNG.SoTienChiuThue = ngoaiGioChiuThue;
                                                        }                                                  
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Lương ngoài giờ không thuế (23) hoặc Lương ngoài giờ chịu thuế (25) không đúng định dạng: " + txt_TruyLinhKhongThue);
                                                    }
                                                }
                                                #endregion

                                                #region Bảo hiểm công ty đóng
                                                if (!string.IsNullOrEmpty(txt_BaoHiemCongTy))
                                                {
                                                    try
                                                    {
                                                        decimal baoHiemCty = Convert.ToDecimal(txt_BaoHiemCongTy);
                                                        if (baoHiemCty > 0)
                                                        {
                                                            decimal BHXH_DN = Math.Round((obj.KyTinhLuong.ThongTinChung.PTBHXHCTY * luongChucDanhThuan) / 100);
                                                            ChiTietLuongNhanVien ctLuongNhanVien_BHXH = uow.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien=? and MaChiTiet=?", luongNhanVien.Oid, "BHXH_DN"));
                                                            if (ctLuongNhanVien_BHXH == null)
                                                            {
                                                                ctLuongNhanVien_BHXH = new ChiTietLuongNhanVien(uow);
                                                                ctLuongNhanVien_BHXH.LuongNhanVien = luongNhanVien;
                                                                ctLuongNhanVien_BHXH.MaChiTiet = "BHXH_DN";
                                                                ctLuongNhanVien_BHXH.DienGiai = "Bảo hiểm xã hội - Doanh nghiệp";
                                                                ctLuongNhanVien_BHXH.CongTru = Enum.NhanSu.CongTruEnum.Tru;
                                                                ctLuongNhanVien_BHXH.GhiChu = "Nhập từ file excel";
                                                            }
                                                            ctLuongNhanVien_BHXH.CostCenter = txt_BaoHiemCongTy_MPB;
                                                            ctLuongNhanVien_BHXH.TienLuong = BHXH_DN;
                                                            ctLuongNhanVien_BHXH.TongNgayCong = 0;
                                                            ctLuongNhanVien_BHXH.SoTien = BHXH_DN;
                                                            ctLuongNhanVien_BHXH.SoTienChiuThue = 0;

                                                            decimal BHYT_DN = Math.Round((obj.KyTinhLuong.ThongTinChung.PTBHYTCTY * luongChucDanhThuan) / 100);
                                                            ChiTietLuongNhanVien ctLuongNhanVien_BHYT = uow.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien=? and MaChiTiet=?", luongNhanVien.Oid, "BHYT_DN"));
                                                            if (ctLuongNhanVien_BHYT == null)
                                                            {
                                                                ctLuongNhanVien_BHYT = new ChiTietLuongNhanVien(uow);
                                                                ctLuongNhanVien_BHYT.LuongNhanVien = luongNhanVien;
                                                                ctLuongNhanVien_BHYT.MaChiTiet = "BHYT_DN";
                                                                ctLuongNhanVien_BHYT.DienGiai = "Bảo hiểm y tế - Doanh nghiệp";
                                                                ctLuongNhanVien_BHYT.CongTru = Enum.NhanSu.CongTruEnum.Tru;
                                                                ctLuongNhanVien_BHYT.GhiChu = "Nhập từ file excel";
                                                            }
                                                            ctLuongNhanVien_BHYT.CostCenter = txt_BaoHiemCongTy_MPB;
                                                            ctLuongNhanVien_BHYT.TienLuong = BHYT_DN;
                                                            ctLuongNhanVien_BHYT.TongNgayCong = 0;
                                                            ctLuongNhanVien_BHYT.SoTien = BHYT_DN;
                                                            ctLuongNhanVien_BHYT.SoTienChiuThue = 0;

                                                            decimal BHTN_DN = Math.Round((obj.KyTinhLuong.ThongTinChung.PTBHTNCTY * luongChucDanhThuan) / 100);
                                                            ChiTietLuongNhanVien ctLuongNhanVien_BHTN = uow.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien=? and MaChiTiet=?", luongNhanVien.Oid, "BHTN_DN"));
                                                            if (ctLuongNhanVien_BHTN == null)
                                                            {
                                                                ctLuongNhanVien_BHTN = new ChiTietLuongNhanVien(uow);
                                                                ctLuongNhanVien_BHTN.LuongNhanVien = luongNhanVien;
                                                                ctLuongNhanVien_BHTN.MaChiTiet = "BHTN_DN";
                                                                ctLuongNhanVien_BHTN.DienGiai = "Bảo hiểm thất nghiệp - Doanh nghiệp";
                                                                ctLuongNhanVien_BHTN.CongTru = Enum.NhanSu.CongTruEnum.Tru;
                                                                ctLuongNhanVien_BHTN.GhiChu = "Nhập từ file excel";
                                                            }
                                                            ctLuongNhanVien_BHTN.CostCenter = txt_BaoHiemCongTy_MPB;
                                                            ctLuongNhanVien_BHTN.TienLuong = BHTN_DN;
                                                            ctLuongNhanVien_BHTN.TongNgayCong = 0;
                                                            ctLuongNhanVien_BHTN.SoTien = BHTN_DN;
                                                            ctLuongNhanVien_BHTN.SoTienChiuThue = 0;

                                                            decimal tongBH_CTY_CT = BHXH_DN + BHYT_DN + BHTN_DN;
                                                            if ((baoHiemCty - tongBH_CTY_CT) > 0)
                                                            {
                                                                LoaiKhauTruLuong loaiKTK = uow.FindObject<LoaiKhauTruLuong>(CriteriaOperator.Parse("MaQuanLy=?", "TruyThuBaoHiem_CTY"));
                                                                if (loaiKTK != null)
                                                                {
                                                                    BangKhauTruLuong bangKTK = uow.FindObject<BangKhauTruLuong>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiKhauTruLuong=?", obj.KyTinhLuong.Oid, loaiKTK.Oid));
                                                                    if (bangKTK == null)
                                                                    {
                                                                        bangKTK = new BangKhauTruLuong(uow);
                                                                        bangKTK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                        bangKTK.LoaiKhauTruLuong = loaiKTK;
                                                                        bangKTK.NgayLap = ngayLap;
                                                                        bangKTK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                    }
                                                                    ChiTietKhauTruLuong chiTietKTK = uow.FindObject<ChiTietKhauTruLuong>(CriteriaOperator.Parse("BangKhauTruLuong=? AND ThongTinNhanVien=?", bangKTK.Oid, nhanVien.Oid));
                                                                    if (chiTietKTK == null)
                                                                    {
                                                                        chiTietKTK = new ChiTietKhauTruLuong(uow);
                                                                        chiTietKTK.ThongTinNhanVien = nhanVien;
                                                                        chiTietKTK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                        chiTietKTK.NgayKhauTru = ngayLap;
                                                                        chiTietKTK.BangKhauTruLuong = bangKTK;
                                                                        chiTietKTK.GhiChu = "Nhập từ file excel";
                                                                    }
                                                                    if (nhanVien.NhomPhanBo != null)
                                                                        chiTietKTK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                    chiTietKTK.SoTien = (baoHiemCty - tongBH_CTY_CT);
                                                                    chiTietKTK.SoTienChiuThue = 0;
                                                                }
                                                                else
                                                                {
                                                                    detailLog.Append("+ Không tìm thấy Truy thu BHXH-BHYT-BHTN (NSDLĐ nộp) trong Loại khấu trừ lương");
                                                                }
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ BHXH-BHYT-BHTN (21,5%) công ty đóng (32) không đúng định dạng: " + txt_BaoHiemCongTy);
                                                    }
                                                }
                                                #endregion

                                                #region Công đoàn công ty đóng
                                                if (!string.IsNullOrEmpty(txt_CongDoanCongTy))
                                                {
                                                    try
                                                    {
                                                        decimal congDoanCty = Convert.ToDecimal(txt_CongDoanCongTy);
                                                        if (congDoanCty > 0)
                                                        {
                                                            //decimal CongDoan_DN = Math.Round((obj.KyTinhLuong.ThongTinChung.PTCDCTY * luongChucDanhThuan) / 100);
                                                            ChiTietLuongNhanVien ctLuongNhanVien_CD = uow.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien=? and MaChiTiet=?", luongNhanVien.Oid, "CD_DN"));
                                                            if (ctLuongNhanVien_CD == null)
                                                            {
                                                                ctLuongNhanVien_CD = new ChiTietLuongNhanVien(uow);
                                                                ctLuongNhanVien_CD.LuongNhanVien = luongNhanVien;
                                                                ctLuongNhanVien_CD.MaChiTiet = "CD_DN";
                                                                ctLuongNhanVien_CD.DienGiai = "Công đoàn - Doanh nghiệp";
                                                                ctLuongNhanVien_CD.CongTru = Enum.NhanSu.CongTruEnum.Tru;
                                                                ctLuongNhanVien_CD.GhiChu = "Nhập từ file excel";
                                                            }
                                                            ctLuongNhanVien_CD.CostCenter = txt_CongDoanCongTy_MPB;
                                                            ctLuongNhanVien_CD.TienLuong = congDoanCty;
                                                            ctLuongNhanVien_CD.TongNgayCong = 0;
                                                            ctLuongNhanVien_CD.SoTien = congDoanCty;
                                                            ctLuongNhanVien_CD.SoTienChiuThue = congDoanCty;
                                                        }                                                     
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Kinh phí công đoàn (2%) công ty đóng (33) không đúng định dạng: " + txt_CongDoanCongTy);
                                                    }
                                                }
                                                #endregion

                                                #region Bảo hiểm NLĐ đóng
                                                if (!string.IsNullOrEmpty(txt_BaoHiemNhanVien))
                                                {
                                                    try
                                                    {
                                                        decimal baoHiemNLD = Convert.ToDecimal(txt_BaoHiemNhanVien);
                                                        if (baoHiemNLD > 0)
                                                        {
                                                            decimal BHXH_NLD = Math.Round((obj.KyTinhLuong.ThongTinChung.PTBHXH * luongChucDanhThuan) / 100);
                                                            ChiTietLuongNhanVien ctLuongNhanVien_BHXH = uow.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien=? and MaChiTiet=?", luongNhanVien.Oid, "BHXH_NLD"));
                                                            if (ctLuongNhanVien_BHXH == null)
                                                            {
                                                                ctLuongNhanVien_BHXH = new ChiTietLuongNhanVien(uow);
                                                                ctLuongNhanVien_BHXH.LuongNhanVien = luongNhanVien;
                                                                ctLuongNhanVien_BHXH.MaChiTiet = "BHXH_NLD";
                                                                ctLuongNhanVien_BHXH.DienGiai = "Bảo hiểm xã hội - Người lao động";
                                                                ctLuongNhanVien_BHXH.CongTru = Enum.NhanSu.CongTruEnum.Tru;
                                                                ctLuongNhanVien_BHXH.GhiChu = "Nhập từ file excel";
                                                            }
                                                            ctLuongNhanVien_BHXH.CostCenter = txt_BaoHiemNhanVien_MPB;
                                                            ctLuongNhanVien_BHXH.TienLuong = BHXH_NLD;
                                                            ctLuongNhanVien_BHXH.TongNgayCong = 0;
                                                            ctLuongNhanVien_BHXH.SoTien = BHXH_NLD;
                                                            ctLuongNhanVien_BHXH.SoTienChiuThue = 0;

                                                            decimal BHYT_NLD = Math.Round((obj.KyTinhLuong.ThongTinChung.PTBHYT * luongChucDanhThuan) / 100);
                                                            ChiTietLuongNhanVien ctLuongNhanVien_BHYT = uow.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien=? and MaChiTiet=?", luongNhanVien.Oid, "BHYT_NLD"));
                                                            if (ctLuongNhanVien_BHYT == null)
                                                            {
                                                                ctLuongNhanVien_BHYT = new ChiTietLuongNhanVien(uow);
                                                                ctLuongNhanVien_BHYT.LuongNhanVien = luongNhanVien;
                                                                ctLuongNhanVien_BHYT.MaChiTiet = "BHYT_NLD";
                                                                ctLuongNhanVien_BHYT.DienGiai = "Bảo hiểm y tế - Người lao động";
                                                                ctLuongNhanVien_BHYT.CongTru = Enum.NhanSu.CongTruEnum.Tru;
                                                                ctLuongNhanVien_BHYT.GhiChu = "Nhập từ file excel";
                                                            }
                                                            ctLuongNhanVien_BHYT.CostCenter = txt_BaoHiemNhanVien_MPB;
                                                            ctLuongNhanVien_BHYT.TienLuong = BHYT_NLD;
                                                            ctLuongNhanVien_BHYT.TongNgayCong = 0;
                                                            ctLuongNhanVien_BHYT.SoTien = BHYT_NLD;
                                                            ctLuongNhanVien_BHYT.SoTienChiuThue = 0;

                                                            decimal BHTN_NLD = Math.Round((obj.KyTinhLuong.ThongTinChung.PTBHTN * luongChucDanhThuan) / 100);
                                                            ChiTietLuongNhanVien ctLuongNhanVien_BHTN = uow.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien=? and MaChiTiet=?", luongNhanVien.Oid, "BHTN_NLD"));
                                                            if (ctLuongNhanVien_BHTN == null)
                                                            {
                                                                ctLuongNhanVien_BHTN = new ChiTietLuongNhanVien(uow);
                                                                ctLuongNhanVien_BHTN.LuongNhanVien = luongNhanVien;
                                                                ctLuongNhanVien_BHTN.MaChiTiet = "BHTN_NLD";
                                                                ctLuongNhanVien_BHTN.DienGiai = "Bảo hiểm thất nghiệp - Nguời lao động";
                                                                ctLuongNhanVien_BHTN.CongTru = Enum.NhanSu.CongTruEnum.Tru;
                                                                ctLuongNhanVien_BHTN.GhiChu = "Nhập từ file excel";
                                                            }
                                                            ctLuongNhanVien_BHTN.CostCenter = txt_BaoHiemNhanVien_MPB;
                                                            ctLuongNhanVien_BHTN.TienLuong = BHTN_NLD;
                                                            ctLuongNhanVien_BHTN.TongNgayCong = 0;
                                                            ctLuongNhanVien_BHTN.SoTien = BHTN_NLD;
                                                            ctLuongNhanVien_BHTN.SoTienChiuThue = 0;

                                                            decimal tongBH_NLD_CT = BHXH_NLD + BHYT_NLD + BHTN_NLD;
                                                            if ((baoHiemNLD- tongBH_NLD_CT) > 0)
                                                            {
                                                                LoaiKhauTruLuong loaiKTK = uow.FindObject<LoaiKhauTruLuong>(CriteriaOperator.Parse("MaQuanLy=?", "KhauTruBaoHiem"));
                                                                if (loaiKTK != null)
                                                                {
                                                                    BangKhauTruLuong bangKTK = uow.FindObject<BangKhauTruLuong>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiKhauTruLuong=?", obj.KyTinhLuong.Oid, loaiKTK.Oid));
                                                                    if (bangKTK == null)
                                                                    {
                                                                        bangKTK = new BangKhauTruLuong(uow);
                                                                        bangKTK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                        bangKTK.LoaiKhauTruLuong = loaiKTK;
                                                                        bangKTK.NgayLap = ngayLap;
                                                                        bangKTK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                    }
                                                                    ChiTietKhauTruLuong chiTietKTK = uow.FindObject<ChiTietKhauTruLuong>(CriteriaOperator.Parse("BangKhauTruLuong=? AND ThongTinNhanVien=?", bangKTK.Oid, nhanVien.Oid));
                                                                    if (chiTietKTK == null)
                                                                    {
                                                                        chiTietKTK = new ChiTietKhauTruLuong(uow);
                                                                        chiTietKTK.ThongTinNhanVien = nhanVien;
                                                                        chiTietKTK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                        chiTietKTK.NgayKhauTru = ngayLap;
                                                                        chiTietKTK.BangKhauTruLuong = bangKTK;
                                                                        chiTietKTK.GhiChu = "Nhập từ file excel";
                                                                    }
                                                                    if (nhanVien.NhomPhanBo != null)
                                                                        chiTietKTK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                    chiTietKTK.SoTien = (baoHiemNLD - tongBH_NLD_CT);
                                                                    chiTietKTK.SoTienChiuThue = 0;
                                                                }
                                                                else
                                                                {
                                                                    detailLog.Append("+ Không tìm thấy Truy thu BHXH-BHYT-BHTN (NLĐ nộp) trong Loại khấu trừ lương");
                                                                }
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ BHXH-BHYT-BHTN (10,5%) CBGVNV đóng (40) không đúng định dạng: " + txt_BaoHiemNhanVien);
                                                    }
                                                }
                                                #endregion

                                                #region Công đoàn người lao động đóng
                                                if (!string.IsNullOrEmpty(txt_CongDoanNhanVien))
                                                {
                                                    try
                                                    {
                                                        decimal congDoanNLD = Convert.ToDecimal(txt_CongDoanNhanVien);
                                                        if (congDoanNLD > 0)
                                                        {
                                                            //decimal congDoan_NLD = Math.Round((obj.KyTinhLuong.ThongTinChung.PTCD * luongChucDanhThuan) / 100);
                                                            ChiTietLuongNhanVien ctLuongNhanVien_CD = uow.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien=? and MaChiTiet=?", luongNhanVien.Oid, "CD_NLD"));
                                                            if (ctLuongNhanVien_CD == null)
                                                            {
                                                                ctLuongNhanVien_CD = new ChiTietLuongNhanVien(uow);
                                                                ctLuongNhanVien_CD.LuongNhanVien = luongNhanVien;
                                                                ctLuongNhanVien_CD.MaChiTiet = "CD_NLD";
                                                                ctLuongNhanVien_CD.DienGiai = "Công đoàn - Người lao động";
                                                                ctLuongNhanVien_CD.CongTru = Enum.NhanSu.CongTruEnum.Tru;
                                                                ctLuongNhanVien_CD.GhiChu = "Nhập từ file excel";
                                                            }
                                                            ctLuongNhanVien_CD.CostCenter = txt_CongDoanNhanVien_MPB;
                                                            ctLuongNhanVien_CD.TienLuong = congDoanNLD;
                                                            ctLuongNhanVien_CD.TongNgayCong = 0;
                                                            ctLuongNhanVien_CD.SoTien = congDoanNLD;
                                                            ctLuongNhanVien_CD.SoTienChiuThue = congDoanNLD;                                                           
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Đoàn phí (1%) CBGVNV đóng (41) không đúng định dạng: " + txt_CongDoanNhanVien);
                                                    }
                                                }
                                                #endregion

                                                #region Truy thu, khấu trừ                                                                                          
                                                if (!string.IsNullOrEmpty(txt_TruyThuKhongThue) || !string.IsNullOrEmpty(txt_TruyThuChiuThue))
                                                {
                                                    try
                                                    {                                                       
                                                        decimal truyThuKhongThue = 0;
                                                        decimal truyThuChiuThue = 0;
                                                        if (!string.IsNullOrEmpty(txt_TruyThuKhongThue))
                                                            truyThuKhongThue = Convert.ToDecimal(txt_TruyThuKhongThue);
                                                        if (!string.IsNullOrEmpty(txt_TruyThuChiuThue))
                                                            truyThuChiuThue = Convert.ToDecimal(txt_TruyThuChiuThue);

                                                        if (truyThuKhongThue > 0 || truyThuChiuThue > 0)
                                                        {
                                                            LoaiKhauTruLuong loaiKTK = uow.FindObject<LoaiKhauTruLuong>(CriteriaOperator.Parse("MaQuanLy=?", "TruyThuLuong"));
                                                            if (loaiKTK != null)
                                                            {
                                                                BangKhauTruLuong bangKTK = uow.FindObject<BangKhauTruLuong>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiKhauTruLuong=?", obj.KyTinhLuong.Oid, loaiKTK.Oid));
                                                                if (bangKTK == null)
                                                                {
                                                                    bangKTK = new BangKhauTruLuong(uow);                                                                   
                                                                    bangKTK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangKTK.LoaiKhauTruLuong = loaiKTK;
                                                                    bangKTK.NgayLap = ngayLap;
                                                                    bangKTK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietKhauTruLuong chiTietKTK = uow.FindObject<ChiTietKhauTruLuong>(CriteriaOperator.Parse("BangKhauTruLuong=? AND ThongTinNhanVien=?", bangKTK.Oid, nhanVien.Oid));
                                                                if (chiTietKTK == null)
                                                                {
                                                                    chiTietKTK = new ChiTietKhauTruLuong(uow);
                                                                    chiTietKTK.ThongTinNhanVien = nhanVien;
                                                                    chiTietKTK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietKTK.NgayKhauTru = ngayLap;
                                                                    chiTietKTK.BangKhauTruLuong = bangKTK;
                                                                    chiTietKTK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietKTK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietKTK.SoTien = truyThuKhongThue;
                                                                chiTietKTK.SoTienChiuThue = truyThuChiuThue;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Truy thu lương trong Loại khấu trừ lương");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Truy thu lương/Khấu trừ khác không thuế (42) hoặc Truy thu lương/Khấu trừ khác chịu thuế (43) không đúng định dạng: " + txt_TruyThuKhongThue);
                                                    }
                                                }
                                                #endregion

                                                #region Khấu trừ ưu đãi học phí                                                                                         
                                                if (!string.IsNullOrEmpty(txt_KhauTruUuDaiHocPhi))
                                                {
                                                    try
                                                    {                                                        
                                                        decimal khauTruUuDaiHP = 0;
                                                        if (!string.IsNullOrEmpty(txt_KhauTruUuDaiHocPhi))
                                                            khauTruUuDaiHP = Convert.ToDecimal(txt_KhauTruUuDaiHocPhi);

                                                        if (khauTruUuDaiHP > 0)
                                                        {
                                                            LoaiKhauTruLuong loaiKTK = uow.FindObject<LoaiKhauTruLuong>(CriteriaOperator.Parse("MaQuanLy=?", "KhauTruUuDaiHocPhiDaChi"));
                                                            if (loaiKTK != null)
                                                            {
                                                                BangKhauTruLuong bangKTK = uow.FindObject<BangKhauTruLuong>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiKhauTruLuong=?", obj.KyTinhLuong.Oid, loaiKTK.Oid));
                                                                if (bangKTK == null)
                                                                {
                                                                    bangKTK = new BangKhauTruLuong(uow);                                                                    
                                                                    bangKTK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangKTK.LoaiKhauTruLuong = loaiKTK;
                                                                    bangKTK.NgayLap = ngayLap;
                                                                    bangKTK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietKhauTruLuong chiTietKTK = uow.FindObject<ChiTietKhauTruLuong>(CriteriaOperator.Parse("BangKhauTruLuong=? AND ThongTinNhanVien=?", bangKTK.Oid, nhanVien.Oid));
                                                                if (chiTietKTK == null)
                                                                {
                                                                    chiTietKTK = new ChiTietKhauTruLuong(uow);
                                                                    chiTietKTK.ThongTinNhanVien = nhanVien;
                                                                    chiTietKTK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietKTK.NgayKhauTru = ngayLap;
                                                                    chiTietKTK.BangKhauTruLuong = bangKTK;
                                                                    chiTietKTK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietKTK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietKTK.SoTien = khauTruUuDaiHP;
                                                                chiTietKTK.SoTienChiuThue = 0;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Khấu trừ ưu đã học phí đã chi trong Loại khấu trừ lương");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Khấu trừ ưu đã học phí đã chi (44) không đúng định dạng: " + txt_KhauTruUuDaiHocPhi);
                                                    }
                                                }
                                                #endregion

                                                #region Thuế TNCN
                                                if (ctl != null)
                                                {
                                                    ctl.ThuNhapChiuThueKhongTienNha2 = Convert.ToDecimal(txt_ThuNhapChiuThueKhongTienNha);
                                                    ctl.ThuNhapChiuThue2 = Convert.ToDecimal(txt_ThuNhapChiuThue);
                                                    ctl.ThuNhapTinhThue2 = Convert.ToDecimal(txt_ThuNhapTinhThue);
                                                    ctl.ThueTNCN2 = Convert.ToDecimal(txt_ThueTNCN);
                                                    ctl.ThucNhan2 = Convert.ToDecimal(txt_ThucNhan);
                                                }
                                                #endregion
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine(string.Format("- Nhân viên Mã: {0} Tên: {1} không thuộc đơn vị quản lý hoặc chưa nhập Mã đơn vị chuyển đi tại cột ghi chú.", nhanVien_qd.MaNhanVien, nhanVien_qd.HoTen));
                                            //}//bo gay ra loi

                                            #region Ghi File log
                                            {
                                                //Đưa thông tin bị lỗi vào blog
                                                if (detailLog.Length > 0)
                                                {
                                                    mainLog.AppendLine("- STT: " + txt_STT);
                                                    mainLog.AppendLine(string.Format("- Nhân viên Mã: {0} Tên: {1} không import vào phần mềm được: ", nhanVien_qd.MaNhanVien, nhanVien_qd.HoTen));
                                                    mainLog.AppendLine(detailLog.ToString());
                                                    //
                                                    sucessImport = false;
                                                }
                                            }
                                            #endregion
                                        }
                                    }
                                    else
                                    {
                                        mainLog.AppendLine("- STT: " + txt_STT);
                                        mainLog.AppendLine(string.Format("- Mã quản lý của nhân viên : {0} không được trống.", txt_HoTen));
                                        //
                                        sucessImport = false;
                                    }
                                    //
                                    #endregion

                                    #endregion
                                    ///////////////////////////NẾU THÀNH CÔNG THÌ SAVE/////////////////////////////////
                                    if (sucessImport)
                                    {
                                        //Lưu
                                        uow.CommitChanges();
                                        //
                                        sucessNumber++;
                                    }
                                    else
                                    {
                                        uow.RollbackTransaction();
                                        erorrNumber++;
                                        //
                                        sucessImport = true;
                                    }
                                }
                                // End Duyệt qua tất cả các dòng trong file excel
                                
                                //
                                string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
                                DialogUtil.ShowInfo("Import Thành Công " + sucessNumber + " dòng dữ liệu - Số dòng không thành công " + erorrNumber + " " + s + "!");

                                //Mở file log lỗi lên
                                if (erorrNumber > 0)
                                {
                                    string tenFile = "Import_Log.txt";
                                    StreamWriter writer = new StreamWriter(tenFile);
                                    writer.WriteLine(mainLog.ToString());
                                    writer.Flush();
                                    writer.Close();
                                    writer.Dispose();
                                    Common.WriteDataToFile(tenFile, mainLog.ToString());
                                    Process.Start(tenFile);
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
