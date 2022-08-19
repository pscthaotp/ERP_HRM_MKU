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
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong
{
    public class Imp_CacKhoanThuNhapKhac
    {        
        public static void ImportCacKhoanThuNhapKhac(IObjectSpace obs, HoSoTinhLuong obj, LoaiOfficeEnum loaiOffice)
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
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet$A7:AU]", loaiOffice))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            int idx_STT = 0;
                            int idx_MaQuanLy = 1;
                            int idx_HoTen = 2;
                            int idx_DonVi = 4;
                            int idx_TienChamSocTreDB = 5;
                            int idx_TienDuGioVuotTiet = 6;
                            int idx_HieuQuaCongViec = 7;
                            int idx_HuuTri = 8;
                            int idx_ThuNhapKhac = 9;
                            int idx_TruyThu= 11;
                            int idx_KhauTruKhac = 12;
                            int idx_GhiChu = 14;                                        
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////
                            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();                              

                                #region Mã phân bổ                                
                                string txt_TienChamSocTreDB_MPB = string.Empty;
                                string txt_TienDuGioVuotTiet_MPB = string.Empty;
                                string txt_HieuQuaCongViec_MPB = string.Empty;
                                string txt_HuuTri_MPB = string.Empty;
                                string txt_ThuNhapKhac_MPB = string.Empty;
                                string txt_TruyThu_MPB = string.Empty;
                                string txt_KhauTruKhac_MPB = string.Empty;
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
                                    string txt_TienChamSocTreDB = dr[idx_TienChamSocTreDB].ToString().Trim();
                                    string txt_TienDuGioVuotTiet = dr[idx_TienDuGioVuotTiet].ToString().Trim();
                                    string txt_HieuQuaCongViec = dr[idx_HieuQuaCongViec].ToString().Trim();
                                    string txt_HuuTri = dr[idx_HuuTri].ToString().Trim();
                                    string txt_ThuNhapKhac = dr[idx_ThuNhapKhac].ToString().Trim();                                    
                                    string txt_TruyThu = dr[idx_TruyThu].ToString().Trim();
                                    string txt_KhauTruKhac = dr[idx_KhauTruKhac].ToString().Trim();    
                                    string txt_GhiChu = dr[idx_GhiChu].ToString().Trim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                    #region Kiểm tra dữ
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaQuanLy))
                                    {
                                        ThongTinNhanVien nhanVien_qd = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("CongTy = ? and (MaNhanVien like ? or MaTapDoan like ?)", obj.CongTy.Oid, txt_MaQuanLy, txt_MaQuanLy));
                                        if (!string.IsNullOrEmpty(txt_GhiChu))
                                        {
                                            nhanVien_qd = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("CongTy.MaBoPhan like ? and (MaNhanVien like ? or MaTapDoan like ?)", txt_GhiChu, txt_MaQuanLy, txt_MaQuanLy));
                                        }
                                        if (nhanVien_qd == null)
                                        {
                                            if (txt_MaQuanLy.Contains("TK") || txt_MaQuanLy.Contains("tài khoản"))
                                            {
                                                if (!string.IsNullOrEmpty(txt_TienChamSocTreDB))
                                                    txt_TienChamSocTreDB_MPB = txt_TienChamSocTreDB;
                                                if (!string.IsNullOrEmpty(txt_TienDuGioVuotTiet))
                                                    txt_TienDuGioVuotTiet_MPB = txt_TienDuGioVuotTiet;
                                                if (!string.IsNullOrEmpty(txt_HieuQuaCongViec))
                                                    txt_HieuQuaCongViec_MPB = txt_HieuQuaCongViec;
                                                if (!string.IsNullOrEmpty(txt_HuuTri))
                                                    txt_HuuTri_MPB = txt_HuuTri;
                                                if (!string.IsNullOrEmpty(txt_ThuNhapKhac))
                                                    txt_ThuNhapKhac_MPB = txt_ThuNhapKhac;
                                                if (!string.IsNullOrEmpty(txt_TruyThu))
                                                    txt_TruyThu_MPB = txt_TruyThu;
                                                if (!string.IsNullOrEmpty(txt_KhauTruKhac))
                                                    txt_KhauTruKhac_MPB = txt_KhauTruKhac;                                               
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
                                            if (nhanVien != null)
                                            {
                                                #region Tiền chăm sóc trẻ ĐB                                                  
                                                if (!string.IsNullOrEmpty(txt_TienChamSocTreDB) && !txt_TienChamSocTreDB.Contains("-"))
                                                {
                                                    try
                                                    {                                                       
                                                        decimal chamSocTreDB = Convert.ToDecimal(txt_TienChamSocTreDB);
                                                        if (chamSocTreDB > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienChamSocTreDacBiet"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);                                                                   
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;                                                                    
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = chamSocTreDB;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Tiền chăm sóc trẻ ĐB (5) trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Tiền chăm sóc trẻ ĐB (5) không đúng định dạng: " + txt_TienChamSocTreDB);
                                                    }
                                                }
                                                #endregion

                                                #region Dư giờ/Vượt tiết
                                                if (!string.IsNullOrEmpty(txt_TienDuGioVuotTiet) && !txt_TienDuGioVuotTiet.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal duGioVuotTiet = Convert.ToDecimal(txt_TienDuGioVuotTiet);
                                                        if (duGioVuotTiet > 0)
                                                        {                                                            
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienVuotTiet"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = uow.GetObjectByKey<LoaiThuNhapKhac>(loaiTNK.Oid);
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;                                                                    
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";                                                                    
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = duGioVuotTiet;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Dư giờ/Vượt tiết trong Loại thu nhập khác");
                                                            }                                                      
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Dư giờ/Vượt tiết (6) không đúng định dạng: " + txt_TienDuGioVuotTiet);
                                                    }
                                                }
                                                #endregion

                                                #region Hưu trí                                              
                                                if (!string.IsNullOrEmpty(txt_HuuTri) && !txt_HuuTri.Contains("-"))
                                                {
                                                    try
                                                    {                                                        
                                                        decimal huuTri = Convert.ToDecimal(txt_HuuTri);
                                                        if (huuTri > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "HuuTri"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;                                                                    
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = huuTri;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Hưu trí trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Hưu trí (8) không đúng định dạng: " + txt_HuuTri);
                                                    }
                                                }
                                                #endregion

                                                #region Thu nhập khác                                              
                                                if (!string.IsNullOrEmpty(txt_ThuNhapKhac) && !txt_ThuNhapKhac.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal thuNhapKhac = Convert.ToDecimal(txt_ThuNhapKhac);
                                                        if (thuNhapKhac > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "ThuNhapKhac"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;                                                                    
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = thuNhapKhac;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Thu nhập khác trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Thu nhập khác (9) không đúng định dạng: " + txt_ThuNhapKhac);
                                                    }
                                                }
                                                #endregion                                                                  

                                                #region Hiệu quả công việc
                                                if (!string.IsNullOrEmpty(txt_HieuQuaCongViec) && !txt_HieuQuaCongViec.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal hieuQuaCongViec = Convert.ToDecimal(txt_HieuQuaCongViec);
                                                        if (hieuQuaCongViec > 0)
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
                                                          
                                                            ChiTietLuongNhanVien ctLuongNhanVien = uow.FindObject<ChiTietLuongNhanVien>(CriteriaOperator.Parse("LuongNhanVien=? and MaChiTiet=?", luongNhanVien.Oid, "HieuQuaCongViec"));
                                                            if (ctLuongNhanVien == null)
                                                            {
                                                                ctLuongNhanVien = new ChiTietLuongNhanVien(uow);
                                                                ctLuongNhanVien.LuongNhanVien = luongNhanVien;
                                                                ctLuongNhanVien.MaChiTiet = "HieuQuaCongViec";
                                                                ctLuongNhanVien.DienGiai = "Hiệu quả công việc";
                                                                ctLuongNhanVien.CongTru = Enum.NhanSu.CongTruEnum.Cong;
                                                                ctLuongNhanVien.GhiChu = "Nhập từ file excel";
                                                            }
                                                            ctLuongNhanVien.CostCenter = txt_HieuQuaCongViec_MPB;
                                                            ctLuongNhanVien.TienLuong = hieuQuaCongViec;
                                                            ctLuongNhanVien.TongNgayCong = 0;
                                                            ctLuongNhanVien.SoTien = hieuQuaCongViec;
                                                            ctLuongNhanVien.SoTienChiuThue = hieuQuaCongViec;
                                                        }                                                     
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Hiệu quả công việc (7) không đúng định dạng: " + txt_HieuQuaCongViec);
                                                    }
                                                }
                                                #endregion                                              
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(string.Format("- Nhân viên Mã: {0} Tên: {1} không thuộc đơn vị quản lý ", nhanVien_qd.MaNhanVien, nhanVien_qd.HoTen));
                                            }

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
