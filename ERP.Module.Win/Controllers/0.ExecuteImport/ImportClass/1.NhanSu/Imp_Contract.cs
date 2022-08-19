using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.Extends;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.HopDongs;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NonPersistentObjects.NhanSu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.NhanSu
{

    public class Imp_Contract
    {
        #region 1. Hợp đồng lao động
        public static void ImportWorkContract(IObjectSpace obs, HopDong_ChonHopDongLamViec obj)
        {
            //
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            StringBuilder mainLog = new StringBuilder();
            //
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet1$A1:L]", obj.LoaiOffice))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            int idx_STT = 0;
                            int idx_MaQuanLy = 1;
                            int idx_HoTen = 2;
                            int idx_SoHopDong = 4;
                            int idx_NgayKy = 5;
                            int idx_HinhThucHopDong = 6;
                            int idx_TuNgay = 7;
                            int idx_DenNgay = 8;
                            int idx_LuongCoBan = 9; // Lương chức danh
                            int idx_LuongKinhDoanh = 10; // Lương HQCV
                            int idx_GhiChu = 11;
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
                                    String txt_STT = dr[idx_STT].ToString().FullTrim();
                                    String txt_MaQuanLy = dr[idx_MaQuanLy].ToString().FullTrim();
                                    String txt_HoTen = dr[idx_HoTen].ToString().FullTrim();
                                    String txt_SoHopDong = dr[idx_SoHopDong].ToString().FullTrim();
                                    String txt_NgayKy = dr[idx_NgayKy].ToString().FullTrim();
                                    String txt_HinhThucHopDong = dr[idx_HinhThucHopDong].ToString().FullTrim();
                                    String txt_TuNgay = dr[idx_TuNgay].ToString().FullTrim();
                                    String txt_DenNgay = dr[idx_DenNgay].ToString().FullTrim();
                                    String txt_LuongCoBan = dr[idx_LuongCoBan].ToString().FullTrim();
                                    String txt_LuongKinhDoanh = dr[idx_LuongKinhDoanh].ToString().FullTrim();
                                    String txt_GhiChu = dr[idx_GhiChu].ToString().FullTrim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                    #region Kiểm tra dữ
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaQuanLy))
                                    {
                                        ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("CongTy = ? and (MaTapDoan = ? or MaNhanVien = ?)", obj.CongTy.Oid, txt_MaQuanLy, txt_MaQuanLy));
                                        if (nhanVien == null)
                                        {
                                            mainLog.AppendLine("- STT: " + txt_STT);
                                            mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống {2}.", txt_MaQuanLy, txt_HoTen, obj.CongTy.TenBoPhan));
                                            //
                                            sucessImport = false;
                                        }
                                        else
                                        {
                                            HopDongLamViec hopDongLamViec = new HopDongLamViec(uow);
                                            hopDongLamViec.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                            hopDongLamViec.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                            if (nhanVien.NhanVienThongTinLuong.NgachLuong != null)
                                                hopDongLamViec.NgachLuong = uow.GetObjectByKey<NgachLuong>(nhanVien.NhanVienThongTinLuong.NgachLuong.Oid);
                                            if (nhanVien.NhanVienThongTinLuong.BacLuong != null)
                                                hopDongLamViec.BacLuong = uow.GetObjectByKey<BacLuong>(nhanVien.NhanVienThongTinLuong.BacLuong.Oid);                                            
                                            //
                                            //hopDongLamViec.QuanLyHopDong = uow.GetObjectByKey<QuanLyHopDong>(obj.QuanLyHopDong.Oid);
                                            //hopDongLamViec.PhanLoaiNguoiKy = uow.GetObjectByKey<PhanLoaiNguoiKy>(obj.PhanLoaiNguoiKy.Oid);
                                            //hopDongLamViec.ChucVuNguoiKy = uow.GetObjectByKey<ChucVuNguoiKy>(obj.ChucVuNguoiKy.Oid);
                                            hopDongLamViec.NguoiKy = uow.GetObjectByKey<ThongTinNhanVien>(obj.NguoiKy.Oid);

                                            #region 1. Số hợp đồng
                                            HopDong hopDong = null;
                                            if (!string.IsNullOrEmpty(txt_SoHopDong))
                                            {
                                                hopDong = uow.FindObject<HopDong>(CriteriaOperator.Parse("SoHopDong = ? and QuanLyHopDong = ?", txt_SoHopDong, obj.QuanLyHopDong.Oid));
                                                if (hopDong == null)
                                                {
                                                    hopDongLamViec.SoHopDong = txt_SoHopDong;

                                                    #region 2. Ngày ký
                                                    if (!string.IsNullOrEmpty(txt_NgayKy))
                                                    {
                                                        try
                                                        {
                                                            hopDongLamViec.NgayKy = Convert.ToDateTime(txt_NgayKy);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Ngày ký hợp đồng không hợp lệ: " + txt_NgayKy);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("+ Thiếu thông tin Ngày ký hợp đồng");
                                                    }
                                                    #endregion

                                                    #region 3. Hình thức hợp đồng
                                                    if (!string.IsNullOrEmpty(txt_HinhThucHopDong))
                                                    {
                                                        string tenLoaiHopDong;
                                                        int mucHuongLuong;
                                                        if (txt_HinhThucHopDong.ToLower().Contains("thử việc"))
                                                        {
                                                            tenLoaiHopDong = "Hợp đồng thử việc";
                                                            mucHuongLuong = 85;
                                                        }
                                                        else 
                                                        {
                                                            tenLoaiHopDong = "Hợp đồng lao động";
                                                            mucHuongLuong = 100;
                                                        }                                                       

                                                        LoaiHopDong loaiHopDong = uow.FindObject<LoaiHopDong>(CriteriaOperator.Parse("TenLoaiHopDong like ?", tenLoaiHopDong));
                                                        if (loaiHopDong == null)
                                                        {
                                                            loaiHopDong = new LoaiHopDong(uow);
                                                            loaiHopDong.TenLoaiHopDong = tenLoaiHopDong;
                                                            loaiHopDong.MaQuanLy = tenLoaiHopDong;
                                                        }
                                                        hopDongLamViec.LoaiHopDong = loaiHopDong;
                                                        hopDongLamViec.PhanTramTinhLuong = mucHuongLuong;

                                                        HinhThucHopDong hinhThucHopDong = uow.FindObject<HinhThucHopDong>(CriteriaOperator.Parse("TenHinhThucHopDong like ? && LoaiHopDong = ?", txt_HinhThucHopDong, loaiHopDong));
                                                        if (hinhThucHopDong == null)
                                                        {
                                                            hinhThucHopDong = new HinhThucHopDong(uow);
                                                            hinhThucHopDong.MaQuanLy = txt_HinhThucHopDong;
                                                            hinhThucHopDong.TenHinhThucHopDong = txt_HinhThucHopDong;
                                                            hinhThucHopDong.LoaiHopDong = loaiHopDong;
                                                        }
                                                        hopDongLamViec.HinhThucHopDong = hinhThucHopDong;
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("+ Thiếu thông tin hình thức hợp đồng");
                                                    }
                                                    #endregion

                                                    #region 4. Từ ngày - Ngày hưởng lương
                                                    if (!string.IsNullOrEmpty(txt_TuNgay))
                                                    {
                                                        try
                                                        {
                                                            hopDongLamViec.TuNgay = Convert.ToDateTime(txt_TuNgay);
                                                            hopDongLamViec.NgayHuongLuong = Convert.ToDateTime(txt_TuNgay);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Từ ngày không hợp lệ: " + txt_TuNgay);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("+ Thiếu thông tin Từ ngày");
                                                    }
                                                    #endregion

                                                    #region 5. Đến ngày
                                                    if (!string.IsNullOrEmpty(txt_DenNgay))
                                                    {
                                                        try
                                                        {
                                                            hopDongLamViec.DenNgay = Convert.ToDateTime(txt_DenNgay);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Đến ngày không hợp lệ: " + txt_DenNgay);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        hopDongLamViec.DenNgay = DateTime.MinValue;
                                                    }
                                                    #endregion
                                                    NamHoc namHoc = uow.FindObject<NamHoc>(CriteriaOperator.Parse("? BETWEEN (NgayBatDau,NgayKetThuc)", hopDongLamViec.TuNgay));
                                                    NienDoTaiChinh NienDoTaiChinh = uow.FindObject<NienDoTaiChinh>(CriteriaOperator.Parse("? BETWEEN (TuNgay,DenNgay)", hopDongLamViec.TuNgay));

                                                    if (NienDoTaiChinh == null)
                                                    {
                                                        NienDoTaiChinh NienDo = new NienDoTaiChinh(uow);
                                                        NienDo.CongTy = uow.GetObjectByKey<CongTy>(Guid.Parse("1B238BAD-2616-4FDD-B958-BA47F3111751"));
                                                        NienDo.TenNienDo = hopDongLamViec.TuNgay.Year.ToString();
                                                        DateTime TuNgayNienDo = new DateTime(hopDongLamViec.TuNgay.Year, 01, 01);
                                                        DateTime DenNgayNienDo = new DateTime(hopDongLamViec.TuNgay.Year, 12, 31);
                                                        NienDo.TuNgay = TuNgayNienDo;
                                                        NienDo.DenNgay = DenNgayNienDo;
                                                        NienDo.SoThang = 12;

                                                        NienDoTaiChinh = NienDo;
                                                    }

                                                    if(namHoc != null && NienDoTaiChinh != null)
                                                    {
                                                        QuanLyHopDong QuanLyHopDong = uow.FindObject<QuanLyHopDong>(CriteriaOperator.Parse("NamHoc = ? AND NienDoTaiChinh =?", namHoc.Oid, NienDoTaiChinh.Oid));
                                                        if(QuanLyHopDong == null)
                                                        {
                                                            QuanLyHopDong quly = new QuanLyHopDong(uow);
                                                            quly.CongTy = uow.GetObjectByKey<CongTy>(Guid.Parse("1B238BAD-2616-4FDD-B958-BA47F3111751"));
                                                            quly.NamHoc = namHoc;
                                                            quly.NienDoTaiChinh = NienDoTaiChinh;
                                                            hopDongLamViec.QuanLyHopDong = quly;
                                                        }
                                                        else
                                                        {
                                                            hopDongLamViec.QuanLyHopDong = QuanLyHopDong;
                                                        }
                                                    }

                                                    #region 6. Lương chức danh (chính)
                                                    if (!string.IsNullOrEmpty(txt_LuongCoBan))
                                                    {
                                                        try
                                                        {
                                                            hopDongLamViec.LuongCoBan = Convert.ToDecimal(txt_LuongCoBan);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Lương chức danh không hợp lệ: " + txt_LuongCoBan);
                                                        }
                                                    }
                                                    #endregion

                                                    #region 7. Lương bổ sung (HQCV)
                                                    if (!string.IsNullOrEmpty(txt_LuongKinhDoanh))
                                                    {
                                                        try
                                                        {
                                                            hopDongLamViec.LuongKinhDoanh = Convert.ToDecimal(txt_LuongKinhDoanh);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Lương bổ sung (HQCV) không hợp lệ: " + txt_LuongKinhDoanh);
                                                        }
                                                    }
                                                    #endregion

                                                    #region 8. Ghi chú
                                                    hopDongLamViec.GhiChu = txt_GhiChu;
                                                    #endregion
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(string.Format("+ Hợp đồng {0} đã tồn tại trong hệ thống", hopDong.SoHopDong));
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("+ Thiếu thông tin số hợp đồng");
                                            }
                                            #endregion

                                            #region 9. Ghi File log
                                            {
                                                //Đưa thông tin bị lỗi vào blog
                                                if (detailLog.Length > 0)
                                                {
                                                    mainLog.AppendLine("- STT: " + txt_STT);
                                                    mainLog.AppendLine(string.Format("- Mã quản lý: {0} Tên: {1} không import vào phần mềm được: ", nhanVien.MaTapDoan, nhanVien.HoTen));
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
                    }
                }
            }
        }
        #endregion

        #region 2 Hợp đồng khoán
        public static void ImportHardContract(IObjectSpace obs, HopDong_ChonHopDongKhoan obj)
        {
            //
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            StringBuilder mainLog = new StringBuilder();
            //
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet1$A1:M]", obj.LoaiOffice))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////
                            #region Khởi tạo các idx
                            int idx_STT = 0;
                            int idx_MaQuanLy = 1;
                            int idx_HoTen = 2;
                            int idx_SoHopDong = 4;
                            int idx_NgayKy = 5;
                            int idx_HinhThucHopDong = 6;
                            int idx_TuNgay = 7;
                            int idx_DenNgay = 8;
                            int idx_LuongKhoan = 9;
                            int idx_PhuCapTienXang = 10;
                            int idx_PhuCapTienAn = 11;
                            int idx_GhiChu = 12;
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
                                    String txt_STT = dr[idx_STT].ToString().FullTrim();
                                    String txt_MaQuanLy = dr[idx_MaQuanLy].ToString().FullTrim();
                                    String txt_HoTen = dr[idx_HoTen].ToString().FullTrim();
                                    String txt_SoHopDong = dr[idx_SoHopDong].ToString().FullTrim();
                                    String txt_NgayKy = dr[idx_NgayKy].ToString().FullTrim();
                                    String txt_HinhThucHopDong = dr[idx_HinhThucHopDong].ToString().FullTrim();
                                    String txt_TuNgay = dr[idx_TuNgay].ToString().FullTrim();
                                    String txt_DenNgay = dr[idx_DenNgay].ToString().FullTrim();
                                    String txt_LuongKhoan = dr[idx_LuongKhoan].ToString().FullTrim();
                                    String txt_PhuCapTienXang = dr[idx_PhuCapTienXang].ToString().FullTrim();
                                    String txt_PhuCapTienAn = dr[idx_PhuCapTienAn].ToString().FullTrim();
                                    String txt_GhiChu = dr[idx_GhiChu].ToString().FullTrim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                    #region Kiểm tra dữ
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaQuanLy))
                                    {
                                        ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("CongTy = ? and (MaTapDoan = ? or MaNhanVien = ?)", obj.CongTy.Oid, txt_MaQuanLy, txt_MaQuanLy));
                                        if (nhanVien == null)
                                        {
                                            mainLog.AppendLine("- STT: " + txt_STT);
                                            mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống {2}.", txt_MaQuanLy, txt_HoTen, obj.CongTy.TenBoPhan));
                                            //
                                            sucessImport = false;
                                        }
                                        else
                                        {
                                            //
                                            HopDongKhoan hopDongKhoan = new HopDongKhoan(uow);
                                            LoaiHopDong loaiHopDong = uow.FindObject<LoaiHopDong>(CriteriaOperator.Parse("TenLoaiHopDong like ?", "Hợp đồng khoán"));
                                            if (loaiHopDong == null)
                                            {
                                                loaiHopDong = new LoaiHopDong(uow);
                                                loaiHopDong.MaQuanLy = "Hợp đồng khoán";
                                                loaiHopDong.TenLoaiHopDong = "Hợp đồng khoán";
                                            }
                                            hopDongKhoan.LoaiHopDong = loaiHopDong;
                                            hopDongKhoan.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                            hopDongKhoan.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                            hopDongKhoan.HoTen = nhanVien.HoTen.ToString();
                                            hopDongKhoan.DiaChiThuongTru = nhanVien.DiaChiThuongTru.FullDiaChi.ToString();
                                            //
                                            hopDongKhoan.QuanLyHopDong = uow.GetObjectByKey<QuanLyHopDong>(obj.QuanLyHopDong.Oid);
                                            hopDongKhoan.PhanLoaiNguoiKy = uow.GetObjectByKey<PhanLoaiNguoiKy>(obj.PhanLoaiNguoiKy.Oid);
                                            hopDongKhoan.ChucVuNguoiKy = uow.GetObjectByKey<ChucVuNguoiKy>(obj.ChucVuNguoiKy.Oid);
                                            hopDongKhoan.NguoiKy = uow.GetObjectByKey<ThongTinNhanVien>(obj.NguoiKy.Oid);

                                            #region 1. Số hợp đồng
                                            HopDong hopDong;
                                            if (!string.IsNullOrEmpty(txt_SoHopDong))
                                            {
                                                hopDong = uow.FindObject<HopDong>(CriteriaOperator.Parse("SoHopDong = ? and QuanLyHopDong = ?", txt_SoHopDong, obj.QuanLyHopDong.Oid));
                                                if (hopDong == null)
                                                {
                                                    hopDongKhoan.SoHopDong = txt_SoHopDong;

                                                    #region 2. Ngày ký
                                                    if (!string.IsNullOrEmpty(txt_NgayKy))
                                                    {
                                                        try
                                                        {
                                                            hopDongKhoan.NgayKy = Convert.ToDateTime(txt_NgayKy);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Ngày ký hợp đồng không hợp lệ: " + txt_NgayKy);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("+ Thiếu thông tin Ngày ký hợp đồng");
                                                    }
                                                    #endregion

                                                    #region 3. Hình thức hợp đồng
                                                    if (!string.IsNullOrEmpty(txt_HinhThucHopDong))
                                                    {
                                                        HinhThucHopDong hinhThucHopDong = uow.FindObject<HinhThucHopDong>(CriteriaOperator.Parse("TenHinhThucHopDong =? && LoaiHopDong =?", txt_HinhThucHopDong, hopDongKhoan.LoaiHopDong));

                                                        if (hinhThucHopDong == null)
                                                        {
                                                            hinhThucHopDong = new HinhThucHopDong(uow);
                                                            hinhThucHopDong.MaQuanLy = txt_HinhThucHopDong;
                                                            hinhThucHopDong.TenHinhThucHopDong = txt_HinhThucHopDong;
                                                            hinhThucHopDong.LoaiHopDong = hopDongKhoan.LoaiHopDong;
                                                        }
                                                        hopDongKhoan.HinhThucHopDong = hinhThucHopDong;
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("+ Thiếu thông tin hình thức hợp đồng");
                                                    }
                                                    #endregion

                                                    #region 4. Từ ngày - Ngày hưởng lương
                                                    if (!string.IsNullOrEmpty(txt_TuNgay))
                                                    {
                                                        try
                                                        {
                                                            hopDongKhoan.TuNgay = Convert.ToDateTime(txt_TuNgay);
                                                            hopDongKhoan.NgayHuongLuong = Convert.ToDateTime(txt_TuNgay);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Từ ngày không hợp lệ: " + txt_TuNgay);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("+ Thiếu thông tin Từ ngày");
                                                    }
                                                    #endregion

                                                    #region 5. Đến ngày
                                                    if (!string.IsNullOrEmpty(txt_DenNgay))
                                                    {
                                                        try
                                                        {
                                                            hopDongKhoan.DenNgay = Convert.ToDateTime(txt_DenNgay);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Đến ngày không hợp lệ: " + txt_DenNgay);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        hopDongKhoan.DenNgay = DateTime.MinValue;
                                                    }
                                                    #endregion

                                                    #region 6. Lương cơ bản
                                                    if (!string.IsNullOrEmpty(txt_LuongKhoan))
                                                    {
                                                        try
                                                        {
                                                            hopDongKhoan.LuongKhoan = Convert.ToDecimal(txt_LuongKhoan);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Lương khoán không hợp lệ: " + txt_LuongKhoan);
                                                        }
                                                    }
                                                    #endregion

                                                    #region 7. Phụ cấp tiền xăng
                                                    if (!string.IsNullOrEmpty(txt_PhuCapTienXang))
                                                    {
                                                        try
                                                        {
                                                            hopDongKhoan.PhuCapTienXang = Convert.ToDecimal(txt_PhuCapTienXang);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Phụ cấp tiền xăng không hợp lệ: " + txt_PhuCapTienXang);
                                                        }
                                                    }
                                                    #endregion

                                                    #region 8. Phụ cấp tiền ăn
                                                    if (!string.IsNullOrEmpty(txt_PhuCapTienAn))
                                                    {
                                                        try
                                                        {
                                                            hopDongKhoan.PhuCapTienAn = Convert.ToDecimal(txt_PhuCapTienAn);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Phụ cấp tiền ăn không hợp lệ: " + txt_PhuCapTienAn);
                                                        }
                                                    }
                                                    #endregion

                                                    #region 9. Ghi chú
                                                    hopDongKhoan.GhiChu = txt_GhiChu;
                                                    #endregion
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(string.Format("+ Hợp đồng {0} đã tồn tại trong hệ thống", hopDong.SoHopDong));
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("+ Thiếu thông tin số hợp đồng");
                                            }
                                            #endregion

                                            #region 10. Ghi File log
                                            {
                                                //Đưa thông tin bị lỗi vào blog
                                                if (detailLog.Length > 0)
                                                {
                                                    mainLog.AppendLine("- STT: " + txt_STT);
                                                    mainLog.AppendLine(string.Format("- Mã quản lý: {0} Tên: {1} không import vào phần mềm được: ", nhanVien.MaTapDoan, nhanVien.HoTen));
                                                    mainLog.AppendLine(detailLog.ToString());
                                                    //
                                                    sucessImport = false;
                                                }
                                            }
                                            #endregion
                                        }
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
                    }
                }
            }
        }
        #endregion

        #region 3. Phụ lục hợp đồng
        public static void ImportAnnexContract(IObjectSpace obs, HopDong_ChonPhuLucHopDong obj)
        {
            //
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            StringBuilder mainLog = new StringBuilder();
            //
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet1$A1:O]", obj.LoaiOffice))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            int idx_STT = 0;
                            int idx_MaQuanLy = 1;
                            int idx_HoTen = 2;
                            int idx_SoHopDong = 4;
                            int idx_SoPhuLucHopDong = 5;
                            int idx_NgayKy = 6;
                            int idx_HinhThucHopDong = 7;
                            int idx_TuNgay = 8;
                            int idx_DenNgay = 9;
                            int idx_LuongCoBan = 10; // Lương chức danh
                            int idx_LuongKinhDoanh = 11; // Lương HQCV
                            int idx_DieuKhoanThayDoi = 12;
                            int idx_NoiDungThayDoi = 13;
                            int idx_InLaiThoaThuan = 14;
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
                                    String txt_STT = dr[idx_STT].ToString().FullTrim();
                                    String txt_MaQuanLy = dr[idx_MaQuanLy].ToString().FullTrim();
                                    String txt_HoTen = dr[idx_HoTen].ToString().FullTrim();
                                    String txt_SoHopDong = dr[idx_SoHopDong].ToString().FullTrim();
                                    String txt_SoPhuLucHopDong = dr[idx_SoPhuLucHopDong].ToString().FullTrim();
                                    String txt_NgayKy = dr[idx_NgayKy].ToString().FullTrim();
                                    String txt_HinhThucHopDong = dr[idx_HinhThucHopDong].ToString().FullTrim();
                                    String txt_TuNgay = dr[idx_TuNgay].ToString().FullTrim();
                                    String txt_DenNgay = dr[idx_DenNgay].ToString().FullTrim();
                                    String txt_LuongCoBan = dr[idx_LuongCoBan].ToString().FullTrim();
                                    String txt_LuongKinhDoanh = dr[idx_LuongKinhDoanh].ToString().FullTrim();
                                    String txt_DieuKhoanThayDoi = dr[idx_DieuKhoanThayDoi].ToString().FullTrim();
                                    String txt_NoiDungThayDoi = dr[idx_NoiDungThayDoi].ToString().FullTrim();
                                    String txt_InLaiThoaThuan = dr[idx_InLaiThoaThuan].ToString().FullTrim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                    #region Kiểm tra dữ
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaQuanLy))
                                    {
                                        ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("CongTy = ? and (MaTapDoan = ? or MaNhanVien = ?)", obj.CongTy.Oid, txt_MaQuanLy, txt_MaQuanLy));
                                        if (nhanVien == null)
                                        {
                                            mainLog.AppendLine("- STT: " + txt_STT);
                                            mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống {2}.", txt_MaQuanLy, txt_HoTen, obj.CongTy.TenBoPhan));
                                            //
                                            sucessImport = false;
                                        }
                                        else
                                        {
                                            HopDongLamViec phuLucHopDong = new HopDongLamViec(uow);
                                            phuLucHopDong.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                            phuLucHopDong.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                            if (nhanVien.NhanVienThongTinLuong.NgachLuong != null)
                                                phuLucHopDong.NgachLuong = uow.GetObjectByKey<NgachLuong>(nhanVien.NhanVienThongTinLuong.NgachLuong.Oid);
                                            if (nhanVien.NhanVienThongTinLuong.BacLuong != null)
                                                phuLucHopDong.BacLuong = uow.GetObjectByKey<BacLuong>(nhanVien.NhanVienThongTinLuong.BacLuong.Oid);
                                            //
                                            phuLucHopDong.QuanLyHopDong = uow.GetObjectByKey<QuanLyHopDong>(obj.QuanLyHopDong.Oid);
                                            phuLucHopDong.PhanLoaiNguoiKy = uow.GetObjectByKey<PhanLoaiNguoiKy>(obj.PhanLoaiNguoiKy.Oid);
                                            phuLucHopDong.ChucVuNguoiKy = uow.GetObjectByKey<ChucVuNguoiKy>(obj.ChucVuNguoiKy.Oid);
                                            phuLucHopDong.NguoiKy = uow.GetObjectByKey<ThongTinNhanVien>(obj.NguoiKy.Oid);

                                            #region 1. Số phụ lục hợp đồng
                                            HopDong hopDong = null;
                                            if (!string.IsNullOrEmpty(txt_SoPhuLucHopDong))
                                            {
                                                hopDong = uow.FindObject<HopDong>(CriteriaOperator.Parse("SoHopDong = ? and QuanLyHopDong = ?", txt_SoPhuLucHopDong, obj.QuanLyHopDong.Oid));
                                                if (hopDong == null)
                                                {
                                                    phuLucHopDong.SoHopDong = txt_SoPhuLucHopDong;

                                                    #region 2. Hợp đồng lao động
                                                    if (!string.IsNullOrEmpty(txt_SoHopDong))
                                                    {
                                                        HopDongLamViec hopDongLaoDong = uow.FindObject<HopDongLamViec>(CriteriaOperator.Parse("SoHopDong = ? and ThongTinNhanVien.Oid = ?", txt_SoHopDong, nhanVien.Oid));
                                                        if (hopDongLaoDong != null)                                                       
                                                            phuLucHopDong.HopDongLaoDong = hopDongLaoDong;
                                                        else                                                        
                                                            detailLog.AppendLine(" + Số hợp đồng không hợp lệ: " + txt_SoHopDong);
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("+ Thiếu thông tin Số hợp đồng");
                                                    }
                                                    #endregion

                                                    #region 3. Ngày ký
                                                    if (!string.IsNullOrEmpty(txt_NgayKy))
                                                    {
                                                        try
                                                        {
                                                            phuLucHopDong.NgayKy = Convert.ToDateTime(txt_NgayKy);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Ngày ký hợp đồng không hợp lệ: " + txt_NgayKy);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("+ Thiếu thông tin Ngày ký hợp đồng");
                                                    }
                                                    #endregion

                                                    #region 4. Hình thức hợp đồng
                                                    if (!string.IsNullOrEmpty(txt_HinhThucHopDong))
                                                    {
                                                        string tenLoaiHopDong;
                                                        int mucHuongLuong;
                                                        tenLoaiHopDong = "Phụ lục hợp đồng";
                                                        mucHuongLuong = 100;                                                      

                                                        LoaiHopDong loaiHopDong = uow.FindObject<LoaiHopDong>(CriteriaOperator.Parse("TenLoaiHopDong like ?", tenLoaiHopDong));
                                                        if (loaiHopDong == null)
                                                        {
                                                            loaiHopDong = new LoaiHopDong(uow);
                                                            loaiHopDong.TenLoaiHopDong = tenLoaiHopDong;
                                                            loaiHopDong.MaQuanLy = tenLoaiHopDong;
                                                        }
                                                        phuLucHopDong.LoaiHopDong = loaiHopDong;
                                                        phuLucHopDong.PhanTramTinhLuong = mucHuongLuong;

                                                        HinhThucHopDong hinhThucHopDong = uow.FindObject<HinhThucHopDong>(CriteriaOperator.Parse("TenHinhThucHopDong like ? && LoaiHopDong = ?", txt_HinhThucHopDong, loaiHopDong));
                                                        if (hinhThucHopDong == null)
                                                        {
                                                            hinhThucHopDong = new HinhThucHopDong(uow);
                                                            hinhThucHopDong.MaQuanLy = txt_HinhThucHopDong;
                                                            hinhThucHopDong.TenHinhThucHopDong = txt_HinhThucHopDong;
                                                            hinhThucHopDong.LoaiHopDong = loaiHopDong;
                                                        }
                                                        phuLucHopDong.HinhThucHopDong = hinhThucHopDong;
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("+ Thiếu thông tin hình thức hợp đồng");
                                                    }
                                                    #endregion

                                                    #region 5. Từ ngày - Ngày hưởng lương
                                                    if (!string.IsNullOrEmpty(txt_TuNgay))
                                                    {
                                                        try
                                                        {
                                                            phuLucHopDong.TuNgay = Convert.ToDateTime(txt_TuNgay);
                                                            phuLucHopDong.NgayHuongLuong = Convert.ToDateTime(txt_TuNgay);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Từ ngày không hợp lệ: " + txt_TuNgay);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("+ Thiếu thông tin Từ ngày");
                                                    }
                                                    #endregion

                                                    #region 6. Đến ngày
                                                    if (!string.IsNullOrEmpty(txt_DenNgay))
                                                    {
                                                        try
                                                        {
                                                            phuLucHopDong.DenNgay = Convert.ToDateTime(txt_DenNgay);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Đến ngày không hợp lệ: " + txt_DenNgay);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        phuLucHopDong.DenNgay = DateTime.MinValue;
                                                    }
                                                    #endregion

                                                    #region 7. Lương chức danh (chính)
                                                    if (!string.IsNullOrEmpty(txt_LuongCoBan))
                                                    {
                                                        try
                                                        {
                                                            phuLucHopDong.LuongCoBan = Convert.ToDecimal(txt_LuongCoBan);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Lương chức danh không hợp lệ: " + txt_LuongCoBan);
                                                        }
                                                    }
                                                    #endregion

                                                    #region 8. Lương bổ sung (HQCV)
                                                    if (!string.IsNullOrEmpty(txt_LuongKinhDoanh))
                                                    {
                                                        try
                                                        {
                                                            phuLucHopDong.LuongKinhDoanh = Convert.ToDecimal(txt_LuongKinhDoanh);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Lương bổ sung (HQCV) không hợp lệ: " + txt_LuongKinhDoanh);
                                                        }
                                                    }
                                                    #endregion

                                                    #region 9. Điều khoản thay đổi
                                                        phuLucHopDong.DieuKhoanThayDoi = txt_DieuKhoanThayDoi;
                                                    #endregion

                                                    #region 10. Nội dung thay đổi
                                                        phuLucHopDong.NoiDungDieuKhoanThayDoi = txt_NoiDungThayDoi;
                                                    #endregion

                                                    #region 11. In lại thỏa thuận
                                                    if (!string.IsNullOrEmpty(txt_InLaiThoaThuan))
                                                    {
                                                        phuLucHopDong.InThoaThuan = true;
                                                    }
                                                    #endregion
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(string.Format("+ Hợp đồng {0} đã tồn tại trong hệ thống", hopDong.SoHopDong));
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("+ Thiếu thông tin Số phụ lục hợp đồng");
                                            }
                                            #endregion

                                            #region 9. Ghi File log
                                            {
                                                //Đưa thông tin bị lỗi vào blog
                                                if (detailLog.Length > 0)
                                                {
                                                    mainLog.AppendLine("- STT: " + txt_STT);
                                                    mainLog.AppendLine(string.Format("- Mã quản lý: {0} Tên: {1} không import vào phần mềm được: ", nhanVien.MaTapDoan, nhanVien.HoTen));
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
                    }
                }
            }
        }
        #endregion

    }
}
