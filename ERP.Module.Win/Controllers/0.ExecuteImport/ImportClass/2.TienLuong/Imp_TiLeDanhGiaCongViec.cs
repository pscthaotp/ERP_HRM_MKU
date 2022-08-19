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
using ERP.Module.Enum.NhanSu;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong
{
    //
    public class Imp_TiLeDanhGiaCongViec
    {

        #region 1. Chấm công khác
        public static void ImportTiLeDanhGia(IObjectSpace obs, OfficeBaseObject obj, Guid OidTiLe)
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
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet1$A1:F]", obj.LoaiOffice))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            int idx_STT = 0;
                            int idx_MaQuanLy = 1;
                            int idx_HoTen = 2;
                            int idx_TiLe = 3;
                            int idx_DanhGia = 4;
                            int idx_DienGiai = 5;
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
                                    String txt_TiLe = dr[idx_TiLe].ToString().FullTrim();
                                    String txt_DanhGia = dr[idx_DanhGia].ToString().FullTrim();
                                    String txt_DienGiai = dr[idx_DienGiai].ToString().FullTrim();
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
                                            // Thêm chi tiết công khác
                                            CriteriaOperator filter = CriteriaOperator.Parse("TiLeDanhGiaCongViec.Oid=? and ThongTinNhanVien.Oid=?", OidTiLe, nhanVien.Oid);
                                            ChiTietDanhGiaCongViec chiTiet = uow.FindObject<ChiTietDanhGiaCongViec>(filter);
                                            if (chiTiet == null)
                                            {
                                                //
                                                chiTiet = new ChiTietDanhGiaCongViec(uow);
                                                chiTiet.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                chiTiet.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                                chiTiet.TiLeDanhGiaCongViec = uow.GetObjectByKey<TiLeDanhGiaCongViec>(OidTiLe);
                                            }

                                            #region 3. Tỉ lệ
                                            if (!string.IsNullOrEmpty(txt_TiLe))
                                            {
                                                try
                                                {
                                                    decimal tiLe = Convert.ToDecimal(txt_TiLe.Replace(".", ","));
                                                    chiTiet.TiLe = tiLe;
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine("+ Tỉ lệ không hợp lệ: " + txt_TiLe);

                                                }
                                            }
                                            #endregion

                                            #region 4. Đánh giá
                                            if (!string.IsNullOrEmpty(txt_DanhGia))
                                            {
                                                if (txt_DanhGia.ToLower().Contains("xuất sắc") || txt_DanhGia.Contains("1"))
                                                    chiTiet.DanhGia = DanhGiaEnum.XuatSac;
                                                else if (txt_DanhGia.ToLower().Contains("giỏi") || txt_DanhGia.Contains("2"))
                                                    chiTiet.DanhGia = DanhGiaEnum.Gioi;
                                                else if (txt_DanhGia.ToLower().Contains("khá") || txt_DanhGia.Contains("3"))
                                                    chiTiet.DanhGia = DanhGiaEnum.Kha;
                                                else chiTiet.DanhGia = DanhGiaEnum.Khong;
                                            }
                                            #endregion

                                            #region 5. Ghi chú
                                            if (!string.IsNullOrEmpty(txt_DienGiai))
                                            {
                                                chiTiet.GhiChu = txt_DienGiai;
                                            }
                                            #endregion

                                            #region 6. Ghi File log
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

                                    ///////////////////////////NẾU THÀNH CÔNG THÌ SAVE/////////////////////////////////
                                    if (sucessImport)
                                    {
                                        //Lưu
                                        uow.CommitChanges();
                                        obj.Reload();
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
                                    #endregion

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
