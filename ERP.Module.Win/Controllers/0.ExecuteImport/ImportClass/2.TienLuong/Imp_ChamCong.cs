using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.Extends;
using ERP.Module.NghiepVu.TienLuong.ChamCong;
using ERP.Module.NonPersistentObjects.HeThong;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong
{
    //
    internal static class StringEx
    {
        internal static String FullTrim(this String source)
        {
            return source.Trim().Replace("  ", " ");
        }

        internal static String RemoveEmpty(this String source)
        {
            return source.Trim().Replace(" ", "");
        }
    }
    public class Imp_ChamCong
    {

        #region 1. Chấm công
        public static void ImportChamCong(IObjectSpace obs, OfficeBaseObject obj, Guid OidQuanLy)
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
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[QuanLyChamCong$A3:N]", obj.LoaiOffice))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            int idx_STT = 0;
                            int idx_MaQuanLy = 1;
                            int idx_HoTen = 2;
                            int idx_NgayHuongLuong = 4;
                            int idx_NgayHe = 5;
                            int idx_NgayHuongBHXH = 6;
                            int idx_NgayKhongLuong = 7;
                            int idx_NgayPhep = 8;
                            int idx_TongCong = 9;
                            int idx_TongCongCaNgay = 10;
                            int idx_CongTruocDieuChinh = 11;
                            int idx_CongSauDieuChinh = 12;
                            int idx_CongChuanTheoGioLamViec = 13;
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
                                    String txt_NgayHuongLuong = dr[idx_NgayHuongLuong].ToString().FullTrim();
                                    String txt_NgayHe = dr[idx_NgayHe].ToString().FullTrim();
                                    String txt_NgayHuongBHXH = dr[idx_NgayHuongBHXH].ToString().FullTrim();
                                    String txt_NgayKhongLuong = dr[idx_NgayKhongLuong].ToString().FullTrim();
                                    String txt_NgayPhep = dr[idx_NgayPhep].ToString().FullTrim();
                                    String txt_TongCong = dr[idx_TongCong].ToString().FullTrim();
                                    String txt_TongCongCaNgay = dr[idx_TongCongCaNgay].ToString().FullTrim();
                                    String txt_CongTruocDieuChinh = dr[idx_CongTruocDieuChinh].ToString().FullTrim();
                                    String txt_CongSauDieuChinh = dr[idx_CongSauDieuChinh].ToString().FullTrim();
                                    String txt_CongChuanTheoGioLamViec = dr[idx_CongChuanTheoGioLamViec].ToString().FullTrim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////

                                    #region Kiểm tra dữ
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaQuanLy))
                                    {
                                        ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("CongTy = ? and MaNhanVien like ? or MaTapDoan like ?", obj.CongTy.Oid, txt_MaQuanLy, txt_MaQuanLy));
                                        if (nhanVien == null)
                                        {
                                            mainLog.AppendLine("- STT: " + txt_STT);
                                            mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống.", txt_MaQuanLy,txt_HoTen));
                                            //
                                            sucessImport = false;
                                        }
                                        else
                                        {
                                            //
                                            CC_ChiTietChamCong chiTiet = new CC_ChiTietChamCong(uow);
                                            chiTiet.BoPhan = uow.GetObjectByKey <BoPhan>(nhanVien.BoPhan.Oid);
                                            chiTiet.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                            chiTiet.QuanLyChamCong = uow.GetObjectByKey<CC_QuanLyChamCong>(OidQuanLy);
                                            chiTiet.IsWeb = true;
                                            //

                                            #region 4. Ngày hưởng lương
                                            if (!string.IsNullOrEmpty(txt_NgayHuongLuong))
                                            {
                                                try
                                                {
                                                    decimal songay = Convert.ToDecimal(txt_NgayHuongLuong.Replace(".",","));
                                                    chiTiet.NgayHuongLuong = songay;
                                                }
                                                catch 
                                                {
                                                    detailLog.AppendLine("+ Ngày hưởng lương không hợp lệ: " + txt_NgayHuongLuong);
                                                    
                                                }
                                            }
                                            #endregion

                                            #region 5. Ngày hè
                                            if (!string.IsNullOrEmpty(txt_NgayHe))
                                            {
                                                try
                                                {
                                                    decimal songay = Convert.ToDecimal(txt_NgayHe.Replace(".", ","));
                                                    chiTiet.NgayHe = songay;
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine("+ Ngày hè không hợp lệ: " + txt_NgayHe);

                                                }
                                            }
                                            #endregion

                                            #region 6. Ngày hưởng BHXH
                                            if (!string.IsNullOrEmpty(txt_NgayHuongBHXH))
                                            {
                                                try
                                                {
                                                    decimal songay = Convert.ToDecimal(txt_NgayHuongBHXH.Replace(".", ","));
                                                    chiTiet.NgayHuongBHXH = songay;
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine("+ Ngày hưởng BHXH không hợp lệ: " + txt_NgayHuongBHXH);

                                                }
                                            }
                                            #endregion

                                            #region 7. Ngày phép năm
                                            if (!string.IsNullOrEmpty(txt_NgayPhep))
                                            {
                                                try
                                                {
                                                    decimal songay = Convert.ToDecimal(txt_NgayPhep.Replace(".", ","));
                                                    chiTiet.NgayPhep = songay;
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine("+ Ngày phép không hợp lệ: " + txt_NgayPhep);

                                                }
                                            }
                                            #endregion

                                            #region 8. Ngày không lương
                                            if (!string.IsNullOrEmpty(txt_NgayKhongLuong))
                                            {
                                                try
                                                {
                                                    decimal songay = Convert.ToDecimal(txt_NgayKhongLuong.Replace(".", ","));
                                                    chiTiet.NgayKhongLuong = songay;
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine("+ Ngày không lương không hợp lệ: " + txt_NgayPhep);

                                                }
                                            }
                                            #endregion

                                            #region 9. Tổng công
                                            if (!string.IsNullOrEmpty(txt_TongCong))
                                            {
                                                try
                                                {
                                                    decimal songay = Convert.ToDecimal(txt_TongCong.Replace(".", ","));
                                                    chiTiet.TongCong = songay;
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine("+ Tổng công không hợp lệ: " + txt_TongCong);

                                                }
                                            }
                                            #endregion

                                            #region 9. Tổng công cả ngày
                                            if (!string.IsNullOrEmpty(txt_TongCongCaNgay))
                                            {
                                                try
                                                {
                                                    decimal songay = Convert.ToDecimal(txt_TongCongCaNgay.Replace(".", ","));
                                                    chiTiet.TongCongCaNgay = songay;
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine("+ Tổng công cả ngày không hợp lệ: " + txt_TongCongCaNgay);

                                                }
                                            }
                                            #endregion

                                            #region 10. Công trước điều chỉnh
                                            if (!string.IsNullOrEmpty(txt_CongTruocDieuChinh))
                                            {
                                                try
                                                {
                                                    decimal songay = Convert.ToDecimal(txt_CongTruocDieuChinh.Replace(".", ","));
                                                    chiTiet.TongCongTruocDieuChinh = songay;
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine("+ Tổng công trước điều chỉnh không hợp lệ: " + txt_CongTruocDieuChinh);

                                                }
                                            }
                                            #endregion

                                            #region 11. Công sau điều chỉnh
                                            if (!string.IsNullOrEmpty(txt_CongSauDieuChinh))
                                            {
                                                try
                                                {
                                                    decimal songay = Convert.ToDecimal(txt_CongSauDieuChinh.Replace(".", ","));
                                                    chiTiet.TongCongSauDieuChinh = songay;
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine("+ Tổng công sau điều chỉnh không hợp lệ: " + txt_CongSauDieuChinh);

                                                }
                                            }
                                            #endregion

                                            #region 12. Công chuẩn theo loại giờ làm việc
                                            if (!string.IsNullOrEmpty(txt_CongChuanTheoGioLamViec))
                                            {
                                                try
                                                {
                                                    decimal songay = Convert.ToDecimal(txt_CongChuanTheoGioLamViec.Replace(".", ","));
                                                    chiTiet.CongChuanTheoLoaiGioLamViec = songay;
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine("+ Công chuẩn theo Loại giờ làm việc không hợp lệ: " + txt_CongChuanTheoGioLamViec);

                                                }
                                            }
                                            #endregion

                                            #region 11. Ghi File log
                                            {
                                                //Đưa thông tin bị lỗi vào blog
                                                if (detailLog.Length > 0)
                                                {
                                                    mainLog.AppendLine("- STT: " + txt_STT);
                                                    mainLog.AppendLine(string.Format("- Cán bộ Mã: {0} Tên: {1} không import vào phần mềm được: ", nhanVien.MaTapDoan, nhanVien.HoTen));
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
