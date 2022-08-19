using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.Extends;
using ERP.Module.NghiepVu.TienLuong.ChamCong;
using ERP.Module.NonPersistentObjects.HeThong;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.TienLuong.ThuNhapKhac;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong
{  
    public class Imp_ChamCongGiangDayPhoThong
    {

        #region 1. Chấm công giảng dạy phổ thông
        public static void ImportChamCongVuotTietPhoThong(IObjectSpace obs, OfficeBaseObject obj, CC_QuanLyCongGiangDay OidQuanLy)
        {
            //
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            StringBuilder mainLog = new StringBuilder();
            //

            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel 2003 file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        try
                        {
                            #region Vượt tiết - Phụ đạo
                            using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Vuot tiet - Phu dao$A6:AE]", obj.LoaiOffice))
                            {
                                /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                                #region Khởi tạo các idx
                                int idx_STT = 0;
                                int idx_MaQuanLy = 1;
                                int idx_HoTen = 2;
                                int idx_TongSoTietGiangDinhMuc = 11;
                                int idx_SoTietGiangThucTeTH = 14;
                                int idx_SoTietGiangThucTeTHCS = 13;
                                int idx_SoTietGiangThucTeTHPT = 12;
                                int idx_TongSoTietGiangThucTe = 15;
                                int idx_SoTietGiangVuotTietTH = 19;
                                int idx_SoTietGiangVuotTietTHCS = 18;
                                int idx_SoTietGiangVuotTietTHPT = 17;
                                int idx_TongSoTietGiangVuotTiet = 16;
                                int idx_DonGiaChuan = 21;
                                int idx_HeSo = 22;
                                int idx_TienVuotTiet = 26;
                                #endregion

                                /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////
                                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                {
                                    //
                                    uow.BeginTransaction();

                                    //Duyệt qua tất cả các dòng trong file excel
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        StringBuilder detailLog = new StringBuilder();

                                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////

                                        #region Đọc dữ liệu
                                        string txt_STT = dr[idx_STT].ToString().FullTrim();
                                        string txt_MaQuanLy = dr[idx_MaQuanLy].ToString().FullTrim();
                                        string txt_HoTen = dr[idx_HoTen].ToString().FullTrim();
                                        string txt_TongSoTietGiangDinhMuc = dr[idx_TongSoTietGiangDinhMuc].ToString().FullTrim();
                                        string txt_SoTietGiangThucTeTH = dr[idx_SoTietGiangThucTeTH].ToString().FullTrim();
                                        string txt_SoTietGiangThucTeTHCS = dr[idx_SoTietGiangThucTeTHCS].ToString().FullTrim();
                                        string txt_SoTietGiangThucTeTHPT = dr[idx_SoTietGiangThucTeTHPT].ToString().FullTrim();
                                        string txt_TongSoTietGiangThucTe = dr[idx_TongSoTietGiangThucTe].ToString().FullTrim();
                                        string txt_SoTietGiangVuotTietTH = dr[idx_SoTietGiangVuotTietTH].ToString().FullTrim();
                                        string txt_SoTietGiangVuotTietTHCS = dr[idx_SoTietGiangVuotTietTHCS].ToString().FullTrim();
                                        string txt_SoTietGiangVuotTietTHPT = dr[idx_SoTietGiangVuotTietTHPT].ToString().FullTrim();
                                        string txt_TongSoTietGiangVuotTiet = dr[idx_TongSoTietGiangVuotTiet].ToString().FullTrim();
                                        string txt_DonGiaChuan = dr[idx_DonGiaChuan].ToString().FullTrim();
                                        string txt_HeSo = dr[idx_HeSo].ToString().FullTrim();
                                        string txt_TienVuotTiet = dr[idx_TienVuotTiet].ToString().FullTrim();
                                        #endregion

                                        //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////

                                        #region Kiểm tra dữ
                                        //
                                        #region 1. Mã quản lý
                                        if (!string.IsNullOrEmpty(txt_MaQuanLy))
                                        {
                                            ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("CongTy = ? and MaNhanVien like ? or MaTapDoan like ?", obj.CongTy.Oid, txt_MaQuanLy, txt_MaQuanLy));
                                            if (nhanVien == null || nhanVien.NhomPhanBo == null)
                                            {
                                                mainLog.AppendLine("- STT: " + txt_STT);
                                                mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống của công ty/trường hoặc không có nhóm phân bổ.", txt_MaQuanLy, txt_HoTen));
                                                //
                                                sucessImport = false;
                                            }
                                            else
                                            {                                                
                                                decimal tongTietDinhMuc = 0;
                                                if (!string.IsNullOrEmpty(txt_TongSoTietGiangDinhMuc))
                                                {
                                                    try
                                                    {
                                                        tongTietDinhMuc = Convert.ToDecimal(txt_TongSoTietGiangDinhMuc.Replace(".", ","));
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("+ Tổng tiết định mức không hợp lệ: " + txt_TongSoTietGiangDinhMuc);
                                                    }
                                                }

                                                decimal soTietTT_TH = 0;
                                                if (!string.IsNullOrEmpty(txt_SoTietGiangThucTeTH))
                                                {
                                                    try
                                                    {
                                                        soTietTT_TH = Convert.ToDecimal(txt_SoTietGiangThucTeTH.Replace(".", ","));
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("+ Số tiết giảng thực tế tiểu học không hợp lệ: " + txt_SoTietGiangThucTeTH);
                                                    }
                                                }

                                                decimal soTietTT_THCS = 0;
                                                if (!string.IsNullOrEmpty(txt_SoTietGiangThucTeTHCS))
                                                {
                                                    try
                                                    {
                                                        soTietTT_THCS = Convert.ToDecimal(txt_SoTietGiangThucTeTHCS.Replace(".", ","));
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("+ Số tiết giảng thực tế THCS không hợp lệ: " + txt_SoTietGiangThucTeTHCS);
                                                    }
                                                }

                                                decimal soTietTT_THPT = 0;
                                                if (!string.IsNullOrEmpty(txt_SoTietGiangThucTeTHPT))
                                                {
                                                    try
                                                    {
                                                        soTietTT_THPT = Convert.ToDecimal(txt_SoTietGiangThucTeTHPT.Replace(".", ","));
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("+ Số tiết giảng thực tế THPT không hợp lệ: " + txt_SoTietGiangThucTeTHPT);
                                                    }
                                                }

                                                decimal soTietVG_TH = 0;
                                                if (!string.IsNullOrEmpty(txt_SoTietGiangVuotTietTH))
                                                {
                                                    try
                                                    {
                                                        soTietVG_TH = Convert.ToDecimal(txt_SoTietGiangVuotTietTH.Replace(".", ","));
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("+ Số tiết giảng vượt giờ tiểu học không hợp lệ: " + txt_SoTietGiangVuotTietTH);
                                                    }
                                                }

                                                decimal soTietVG_THCS = 0;
                                                if (!string.IsNullOrEmpty(txt_SoTietGiangVuotTietTHCS))
                                                {
                                                    try
                                                    {
                                                        soTietVG_THCS = Convert.ToDecimal(txt_SoTietGiangVuotTietTHCS.Replace(".", ","));
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("+ Số tiết giảng vượt giờ THCS không hợp lệ: " + txt_SoTietGiangVuotTietTHCS);
                                                    }
                                                }

                                                decimal soTietVG_THPT = 0;
                                                if (!string.IsNullOrEmpty(txt_SoTietGiangVuotTietTHPT))
                                                {
                                                    try
                                                    {
                                                        soTietVG_THPT = Convert.ToDecimal(txt_SoTietGiangVuotTietTHPT.Replace(".", ","));
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("+ Số tiết giảng vượt giờ THPT không hợp lệ: " + txt_SoTietGiangVuotTietTHPT);
                                                    }
                                                }

                                                decimal tongSoVuotTiet = 0;
                                                if (!string.IsNullOrEmpty(txt_TongSoTietGiangVuotTiet))
                                                {
                                                    try
                                                    {
                                                        tongSoVuotTiet = Convert.ToDecimal(txt_TongSoTietGiangVuotTiet.Replace(".", ","));
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("+ Tổng số vượt tiết không hợp lệ: " + txt_TongSoTietGiangVuotTiet);
                                                    }
                                                }

                                                decimal donGiaChuan = 0;
                                                if (!string.IsNullOrEmpty(txt_DonGiaChuan))
                                                {
                                                    try
                                                    {
                                                        donGiaChuan = Convert.ToDecimal(txt_DonGiaChuan.Replace(".", ","));
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("+ Đơn giá chuẩn không hợp lệ: " + txt_DonGiaChuan);
                                                    }
                                                }

                                                decimal heSo = 0;
                                                if (!string.IsNullOrEmpty(txt_HeSo))
                                                {
                                                    try
                                                    {
                                                        heSo = Convert.ToDecimal(txt_HeSo.Replace(".", ","));
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("+ Hệ số không hợp lệ: " + txt_HeSo);
                                                    }
                                                }

                                                if (nhanVien.NhomPhanBo != null && !nhanVien.NhomPhanBo.TenNhomPhanBo.ToLower().Contains("tiểu học"))
                                                {
                                                    NhomPhanBo nhomPhanBoTHCS = null;
                                                    NhomPhanBo nhomPhanBoTHPT = null;
                                                    if (soTietTT_THCS > 0)
                                                        //nhomPhanBoTHCS = uow.FindObject<NhomPhanBo>(CriteriaOperator.Parse("TenNhomPhanBo like '%'+?+'%'", "Trung học cơ sở"));
                                                        nhomPhanBoTHCS = uow.FindObject<NhomPhanBo>(CriteriaOperator.Parse("CostCenter=?", "02_1002"));
                                                    if (soTietTT_THPT > 0)
                                                        //nhomPhanBoTHPT = uow.FindObject<NhomPhanBo>(CriteriaOperator.Parse("TenNhomPhanBo like '%'+?+'%'", "Trung học phổ thông"));
                                                        nhomPhanBoTHPT = uow.FindObject<NhomPhanBo>(CriteriaOperator.Parse("CostCenter=?", "02_1003"));

                                                    if (nhomPhanBoTHCS != null)
                                                    {
                                                        CC_ChiTietCongGiangDay chiTiet = uow.FindObject<CC_ChiTietCongGiangDay>(CriteriaOperator.Parse("CC_QuanLyCongGiangDay=? and ThongTinNhanVien=? and NhomPhanBo=?", OidQuanLy.Oid, nhanVien.Oid, nhomPhanBoTHCS.Oid));
                                                        if (chiTiet == null)
                                                        {
                                                            chiTiet = new CC_ChiTietCongGiangDay(uow);
                                                            chiTiet.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                            chiTiet.NhomPhanBo = nhomPhanBoTHCS;
                                                            chiTiet.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                                            chiTiet.QuanLyCongGiangDay = uow.GetObjectByKey<CC_QuanLyCongGiangDay>(OidQuanLy.Oid);
                                                        }
                                                        chiTiet.TongCong = soTietTT_THCS;
                                                        if (soTietVG_THCS > 0)
                                                            chiTiet.CongVuot = soTietVG_THCS;

                                                        chiTiet.CongChuan = tongTietDinhMuc;
                                                        chiTiet.DonGia = donGiaChuan;
                                                        chiTiet.HeSo = heSo;
                                                    }
                                                    if (nhomPhanBoTHPT != null)
                                                    {
                                                        CC_ChiTietCongGiangDay chiTiet = uow.FindObject<CC_ChiTietCongGiangDay>(CriteriaOperator.Parse("CC_QuanLyCongGiangDay=? and ThongTinNhanVien=? and NhomPhanBo=?", OidQuanLy.Oid, nhanVien.Oid, nhomPhanBoTHPT.Oid));
                                                        if (chiTiet == null)
                                                        {
                                                            chiTiet = new CC_ChiTietCongGiangDay(uow);
                                                            chiTiet.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                            chiTiet.NhomPhanBo = nhomPhanBoTHPT;
                                                            chiTiet.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                                            chiTiet.QuanLyCongGiangDay = uow.GetObjectByKey<CC_QuanLyCongGiangDay>(OidQuanLy);
                                                        }
                                                            chiTiet.TongCong = soTietTT_THPT;

                                                        if (soTietVG_THPT > 0)
                                                            chiTiet.CongVuot = soTietVG_THPT;

                                                        chiTiet.CongChuan = tongTietDinhMuc;
                                                        chiTiet.DonGia = donGiaChuan;
                                                        chiTiet.HeSo = heSo;
                                                    }
                                                }

                                                decimal tienVuotTiet = 0;
                                                if (!string.IsNullOrEmpty(txt_TienVuotTiet))
                                                {
                                                    try
                                                    {
                                                        tienVuotTiet = Convert.ToDecimal(txt_TienVuotTiet.Replace(".", ","));
                                                        if (tienVuotTiet > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK_VuotTiet = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienVuotTiet"));
                                                            if (loaiTNK_VuotTiet != null)
                                                            {
                                                                BangThuNhapKhac bangTNK_VuotTiet = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", OidQuanLy.KyTinhLuong.Oid, loaiTNK_VuotTiet.Oid));
                                                                if (bangTNK_VuotTiet == null)
                                                                {
                                                                    bangTNK_VuotTiet = new BangThuNhapKhac(uow);
                                                                    bangTNK_VuotTiet.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK_VuotTiet.LoaiThuNhapKhac = uow.GetObjectByKey<LoaiThuNhapKhac>(loaiTNK_VuotTiet.Oid);
                                                                    bangTNK_VuotTiet.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(OidQuanLy.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK_VuotTiet.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK_VuotTiet;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel (Bảng chấm công giảng dạy)";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = tienVuotTiet;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Vượt tiết trong Loại thu nhập khác");
                                                            }
                                                        }

                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("+ Tiền vượt tiết không hợp lệ: " + txt_TienVuotTiet);
                                                    }
                                                }

                                                #region 11. Ghi File log
                                                {
                                                    //Đưa thông tin bị lỗi vào blog
                                                    if (detailLog.Length > 0)
                                                    {
                                                        mainLog.AppendLine("- STT: " + txt_STT);
                                                        mainLog.AppendLine(string.Format("- Nhân viên Mã: {0} Tên: {1} không import vào phần mềm được: ", nhanVien.MaTapDoan, nhanVien.HoTen));
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

                                        #endregion
                                        //
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
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            #region Luyện thi - Bồi dưỡng
                            using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Luyen thi - Boi duong$A6:U]", obj.LoaiOffice))
                            {
                                /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                                #region Khởi tạo các idx
                                int idx_STT = 0;
                                int idx_MaQuanLy = 1;
                                int idx_HoTen = 2;
                                int idx_TongSoTietLTBDDinhMuc = 11;
                                int idx_SoTietLTBDThucTeTHPT = 7;
                                int idx_SoTietLTBDThucTeTHCS = 8;
                                int idx_SoTietLTBDThucTeTH = 9;                               
                                int idx_TongSoTietLTBDThucTe = 10;                             
                                int idx_DonGiaChuan = 13;
                                int idx_HeSo = 14;
                                int idx_TienLTBD = 18;
                                #endregion

                                /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////
                                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                {
                                    //
                                    uow.BeginTransaction();

                                    //Duyệt qua tất cả các dòng trong file excel
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        StringBuilder detailLog = new StringBuilder();

                                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////

                                        #region Đọc dữ liệu
                                        string txt_STT = dr[idx_STT].ToString().FullTrim();
                                        string txt_MaQuanLy = dr[idx_MaQuanLy].ToString().FullTrim();
                                        string txt_HoTen = dr[idx_HoTen].ToString().FullTrim();
                                        string txt_TongSoTietLTBDDinhMuc = dr[idx_TongSoTietLTBDDinhMuc].ToString().FullTrim();
                                        string txt_SoTietLTBDThucTeTH = dr[idx_SoTietLTBDThucTeTH].ToString().FullTrim();
                                        string txt_SoTietLTBDThucTeTHCS = dr[idx_SoTietLTBDThucTeTHCS].ToString().FullTrim();
                                        string txt_SoTietLTBDThucTeTHPT = dr[idx_SoTietLTBDThucTeTHPT].ToString().FullTrim();
                                        string txt_TongSoTietLTBDThucTe = dr[idx_TongSoTietLTBDThucTe].ToString().FullTrim();                                      
                                        string txt_DonGiaChuan = dr[idx_DonGiaChuan].ToString().FullTrim();
                                        string txt_HeSo = dr[idx_HeSo].ToString().FullTrim();
                                        string txt_TienLTBD = dr[idx_TienLTBD].ToString().FullTrim();
                                        #endregion

                                        //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////

                                        #region Kiểm tra dữ
                                        //
                                        #region 1. Mã quản lý
                                        if (!string.IsNullOrEmpty(txt_MaQuanLy))
                                        {
                                            ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("CongTy = ? and MaNhanVien like ? or MaTapDoan like ?", obj.CongTy.Oid, txt_MaQuanLy, txt_MaQuanLy));
                                            if (nhanVien == null || nhanVien.NhomPhanBo == null)
                                            {
                                                mainLog.AppendLine("- STT: " + txt_STT);
                                                mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống của công ty/trường hoặc không có nhóm phân bổ.", txt_MaQuanLy, txt_HoTen));
                                                //
                                                sucessImport = false;
                                            }
                                            else
                                            {                                                
                                                decimal tongTietDinhMuc = 0;
                                                if (!string.IsNullOrEmpty(txt_TongSoTietLTBDDinhMuc))
                                                {
                                                    try
                                                    {
                                                        tongTietDinhMuc = Convert.ToDecimal(txt_TongSoTietLTBDDinhMuc.Replace(".", ","));
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("+ Tổng tiết định mức không hợp lệ: " + txt_TongSoTietLTBDDinhMuc);
                                                    }
                                                }

                                                decimal soTietLTBD_TH = 0;
                                                if (!string.IsNullOrEmpty(txt_SoTietLTBDThucTeTH))
                                                {
                                                    try
                                                    {
                                                        soTietLTBD_TH = Convert.ToDecimal(txt_SoTietLTBDThucTeTH.Replace(".", ","));
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("+ Số tiết luyện thi, bồi dưỡng thực tế tiểu học không hợp lệ: " + txt_SoTietLTBDThucTeTH);
                                                    }
                                                }

                                                decimal soTietLTBD_THCS = 0;
                                                if (!string.IsNullOrEmpty(txt_SoTietLTBDThucTeTHCS))
                                                {
                                                    try
                                                    {
                                                        soTietLTBD_THCS = Convert.ToDecimal(txt_SoTietLTBDThucTeTHCS.Replace(".", ","));
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("+ Số tiết luyện thi, bồi dưỡng thực tế THCS không hợp lệ: " + txt_SoTietLTBDThucTeTHCS);
                                                    }
                                                }

                                                decimal soTietLTBD_THPT = 0;
                                                if (!string.IsNullOrEmpty(txt_SoTietLTBDThucTeTHPT))
                                                {
                                                    try
                                                    {
                                                        soTietLTBD_THPT = Convert.ToDecimal(txt_SoTietLTBDThucTeTHPT.Replace(".", ","));
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("+ Số tiết luyện thi, bồi dưỡng thực tế THPT không hợp lệ: " + txt_SoTietLTBDThucTeTHPT);
                                                    }
                                                }

                                                decimal donGiaChuan = 0;
                                                if (!string.IsNullOrEmpty(txt_DonGiaChuan))
                                                {
                                                    try
                                                    {
                                                        donGiaChuan = Convert.ToDecimal(txt_DonGiaChuan.Replace(".", ","));
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("+ Đơn giá chuẩn không hợp lệ: " + txt_DonGiaChuan);
                                                    }
                                                }

                                                decimal heSo = 0;
                                                if (!string.IsNullOrEmpty(txt_HeSo))
                                                {
                                                    try
                                                    {
                                                        heSo = Convert.ToDecimal(txt_HeSo.Replace(".", ","));
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("+ Hệ số không hợp lệ: " + txt_HeSo);
                                                    }
                                                }

                                                if (nhanVien.NhomPhanBo != null && !nhanVien.NhomPhanBo.TenNhomPhanBo.ToLower().Contains("tiểu học"))
                                                {
                                                    NhomPhanBo nhomPhanBoTHCS = null;
                                                    NhomPhanBo nhomPhanBoTHPT = null;
                                                    if (soTietLTBD_THCS > 0)
                                                        nhomPhanBoTHCS = uow.FindObject<NhomPhanBo>(CriteriaOperator.Parse("CostCenter=?", "02_1002"));
                                                    if (soTietLTBD_THPT > 0)
                                                        nhomPhanBoTHPT = uow.FindObject<NhomPhanBo>(CriteriaOperator.Parse("CostCenter=?", "02_1003"));

                                                    if (nhomPhanBoTHCS != null)
                                                    {                                                       
                                                        CC_ChiTietCongGiangDay chiTiet = uow.FindObject<CC_ChiTietCongGiangDay>(CriteriaOperator.Parse("CC_QuanLyCongGiangDay=? and ThongTinNhanVien=? and NhomPhanBo=?", OidQuanLy.Oid, nhanVien.Oid, nhomPhanBoTHCS.Oid));
                                                        if (chiTiet == null)
                                                        {
                                                            chiTiet = new CC_ChiTietCongGiangDay(uow);
                                                            chiTiet.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                            chiTiet.NhomPhanBo = nhomPhanBoTHCS;
                                                            chiTiet.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                                            chiTiet.QuanLyCongGiangDay = uow.GetObjectByKey<CC_QuanLyCongGiangDay>(OidQuanLy.Oid);
                                                        }
                                                        chiTiet.CongLuyenThi = soTietLTBD_THCS;
                                                        chiTiet.CongChuan = tongTietDinhMuc;
                                                        chiTiet.DonGia = donGiaChuan;
                                                        chiTiet.HeSo = heSo;
                                                    }
                                                    if (nhomPhanBoTHPT != null)
                                                    {
                                                        CC_ChiTietCongGiangDay chiTiet = uow.FindObject<CC_ChiTietCongGiangDay>(CriteriaOperator.Parse("CC_QuanLyCongGiangDay=? and ThongTinNhanVien=? and NhomPhanBo=?", OidQuanLy.Oid, nhanVien.Oid, nhomPhanBoTHPT.Oid));
                                                        if (chiTiet == null)
                                                        {
                                                            chiTiet.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                            chiTiet.NhomPhanBo = nhomPhanBoTHPT;
                                                            chiTiet.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                                            chiTiet.QuanLyCongGiangDay = uow.GetObjectByKey<CC_QuanLyCongGiangDay>(OidQuanLy);
                                                        }
                                                        chiTiet.CongLuyenThi = soTietLTBD_THPT;
                                                        chiTiet.CongChuan = tongTietDinhMuc;
                                                        chiTiet.DonGia = donGiaChuan;
                                                        chiTiet.HeSo = heSo;
                                                    }
                                                }

                                                decimal tienLTBD = 0;
                                                if (!string.IsNullOrEmpty(txt_TienLTBD))
                                                {
                                                    try
                                                    {
                                                        tienLTBD = Convert.ToDecimal(txt_TienLTBD.Replace(".", ","));
                                                        if (tienLTBD > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK_VuotTiet = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienLuyenThi"));
                                                            if (loaiTNK_VuotTiet != null)
                                                            {
                                                                BangThuNhapKhac bangTNK_VuotTiet = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", OidQuanLy.KyTinhLuong.Oid, loaiTNK_VuotTiet.Oid));
                                                                if (bangTNK_VuotTiet == null)
                                                                {
                                                                    bangTNK_VuotTiet = new BangThuNhapKhac(uow);
                                                                    bangTNK_VuotTiet.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK_VuotTiet.LoaiThuNhapKhac = uow.GetObjectByKey<LoaiThuNhapKhac>(loaiTNK_VuotTiet.Oid);
                                                                    bangTNK_VuotTiet.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(OidQuanLy.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK_VuotTiet.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK_VuotTiet;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel (Bảng chấm công giảng dạy)";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = tienLTBD;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Luyện thi trong Loại thu nhập khác");
                                                            }
                                                        }

                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("+ Tiền Luyện thi không hợp lệ: " + txt_TienLTBD);
                                                    }
                                                }

                                                #region 11. Ghi File log
                                                {
                                                    //Đưa thông tin bị lỗi vào blog
                                                    if (detailLog.Length > 0)
                                                    {
                                                        mainLog.AppendLine("- STT: " + txt_STT);
                                                        mainLog.AppendLine(string.Format("- Nhân viên Mã: {0} Tên: {1} không import vào phần mềm được: ", nhanVien.MaTapDoan, nhanVien.HoTen));
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

                                        #endregion
                                        //
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
                            #endregion
                        }
                    }
                }
            }
        }
        #endregion
    }
}
