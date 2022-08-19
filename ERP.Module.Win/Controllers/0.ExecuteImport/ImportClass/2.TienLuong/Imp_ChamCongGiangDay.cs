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
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.DanhMuc.TienLuong;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong
{  
    public class Imp_ChamCongGiangDay
    {

        #region 1. Chấm công giảng dạy
        public static void ImportChamCongGiangDay(IObjectSpace obs, OfficeBaseObject obj, Guid OidQuanLy)
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
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet1$A1:G]", obj.LoaiOffice))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            int idx_STT = 0;
                            int idx_MaQuanLy = 1;
                            int idx_HoTen = 2;
                            int idx_NhomPhanBo = 3;
                            int idx_TongCong = 4;
                            int idx_CongVuot = 5;                            
                            int idx_CongChuan = 6;
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
                                    String txt_NhomPhanBo = dr[idx_NhomPhanBo].ToString().FullTrim();
                                    String txt_TongCong = dr[idx_TongCong].ToString().FullTrim();
                                    String txt_CongVuot = dr[idx_CongVuot].ToString().FullTrim();
                                    String txt_CongChuan = dr[idx_CongChuan].ToString().FullTrim();
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
                                            CC_ChiTietCongGiangDay chiTiet = new CC_ChiTietCongGiangDay(uow);
                                            chiTiet.BoPhan = uow.GetObjectByKey <BoPhan>(nhanVien.BoPhan.Oid);
                                            chiTiet.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                            chiTiet.QuanLyCongGiangDay = uow.GetObjectByKey<CC_QuanLyCongGiangDay>(OidQuanLy);

                                            #region 1. Nhóm phân bổ
                                            if (!string.IsNullOrEmpty(txt_NhomPhanBo))
                                            {
                                                NhomPhanBo to = uow.FindObject<NhomPhanBo>(CriteriaOperator.Parse("CostCenter = ? or TenNhomPhanBo like ?", txt_NhomPhanBo, txt_NhomPhanBo));
                                                if (to != null)
                                                    chiTiet.NhomPhanBo = to;
                                                else detailLog.AppendLine("+ Nhóm phân bổ không hợp lệ: " + txt_NhomPhanBo);

                                            }
                                            else
                                                detailLog.AppendLine("+ Nhóm phân bổ không được trống");
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
                                            else
                                                detailLog.AppendLine("+ Tổng công không được trống");
                                            #endregion                                       

                                            #region 10. Công vượt
                                            if (!string.IsNullOrEmpty(txt_CongVuot))
                                            {
                                                try
                                                {
                                                    decimal songay = Convert.ToDecimal(txt_CongVuot.Replace(".", ","));
                                                    chiTiet.CongVuot = songay;
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine("+ Công vượt không hợp lệ: " + txt_CongVuot);

                                                }
                                            }
                                            #endregion                                       

                                            #region 12. Công chuẩn
                                            if (!string.IsNullOrEmpty(txt_CongChuan))
                                            {
                                                try
                                                {
                                                    decimal songay = Convert.ToDecimal(txt_CongChuan.Replace(".", ","));
                                                    chiTiet.CongChuan = songay;
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine("+ Công chuẩn không hợp lệ: " + txt_CongChuan);

                                                }
                                            }
                                            else
                                                detailLog.AppendLine("+ Công chuẩn không được trống");
                                            #endregion

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
                    }
                }
            }
        }
        #endregion
    }
}
