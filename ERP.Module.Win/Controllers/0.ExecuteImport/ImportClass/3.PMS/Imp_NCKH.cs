using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.Extends;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using System.Data.SqlClient;
using ERP.Module.Enum.Systems;
using ERP.Module.NghiepVu.PMS.DanhMuc;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.PMS
{
    public class Imp_NCKH
    {      
        public static void XuLy(IObjectSpace obs, Guid OidQuanLy)
        {
            //
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            StringBuilder mainLog = new StringBuilder();
            //

            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel 2010 file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet$A2:L]", LoaiOfficeEnum.Office2003))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            const int idx_STT = 0;
                            const int idx_MaGV = 1;
                            const int idx_HoTen = 2;
                            const int idx_HocVi = 3;
                            const int idx_BoPhan = 4;
                            const int idx_MaQuanLy = 5;
                            const int idx_CongViec = 6;
                            const int idx_ChiTiet = 7;
                            const int idx_DienGiai = 8;
                            const int idx_DonViTinh = 9;
                            const int idx_GioQuyDoi = 10;
                            const int idx_GhiChu = 11;
                            string sql = "";
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
                                    string txt_STT = dr[idx_STT].ToString().Trim();
                                    string txt_MaGV = dr[idx_MaGV].ToString().Trim();
                                    string txt_HoTen = dr[idx_HoTen].ToString().Trim();
                                    string txt_HocVi = dr[idx_HocVi].ToString().Trim();
                                    string txt_BoPhan = dr[idx_BoPhan].ToString().Trim();
                                    string txt_MaQuanLy = dr[idx_MaQuanLy].ToString().Trim();
                                    string txt_CongViec = dr[idx_CongViec].ToString().Trim();
                                    string txt_ChiTiet = dr[idx_ChiTiet].ToString().Trim();
                                    string txt_DienGiai = dr[idx_DienGiai].ToString().Trim();
                                    string txt_DonViTinh = dr[idx_DonViTinh].ToString().Trim();
                                    string txt_GioQuyDoi = dr[idx_GioQuyDoi].ToString().Trim();
                                    string txt_GhiChu = dr[idx_GhiChu].ToString().Trim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////

                                    #region Kiểm tra dữ
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaGV) && !string.IsNullOrEmpty(txt_MaQuanLy)
                                        && !string.IsNullOrEmpty(txt_CongViec) && !string.IsNullOrEmpty(txt_ChiTiet)
                                        && !string.IsNullOrEmpty(txt_DonViTinh)
                                        && !string.IsNullOrEmpty(txt_DienGiai) && !string.IsNullOrEmpty(txt_GioQuyDoi))
                                    {
                                        NhanVien nhanVien = uow.FindObject<NhanVien>(CriteriaOperator.Parse("MaNhanVien = ?", txt_MaGV));
                                        DonViTinh _DonViTinh = uow.FindObject<DonViTinh>(CriteriaOperator.Parse("MaQuanLy = ? or TenDonViTinh=?", txt_DonViTinh, txt_DonViTinh));
                                        if (nhanVien == null)
                                        {
                                            mainLog.AppendLine("- Mã: " + txt_MaGV);
                                            mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống.", txt_MaGV, txt_HoTen));
                                            //
                                            sucessImport = false;
                                        }
                                        else if (_DonViTinh == null)
                                        {
                                            mainLog.AppendLine(string.Format("- Mã hoặc tên đơn vị tính :{0} không tồn tại trong hệ thống.", txt_DonViTinh));

                                            sucessImport = false;
                                        }
                                        else
                                        {
                                            sql += " Union All select N'" + nhanVien.Oid + "' as NhanVien"
                                                          + ", N'" + txt_MaQuanLy + "' as MaQuanLy"
                                                          + ", N'" + txt_CongViec + "' as CongViec"
                                                          + ", N'" + txt_ChiTiet + "' as ChiTiet"
                                                           + ", N'" + txt_DienGiai + "' as DienGiai"
                                                          + ", N'" + _DonViTinh.Oid + "' as DonViTinh"
                                                          + ", N'" + txt_GhiChu + "' as GhiChu"
                                                          + ", N'" + txt_GioQuyDoi + "' as GioQuyDoi";
                                            sucessNumber++;
                                        }
                                    }
                                    else
                                    {
                                        mainLog.AppendLine("- Mã: " + txt_MaGV);
                                        mainLog.AppendLine(string.Format("- Mã quản lý của nhân viên : {0} không được trống.", txt_HoTen));
                                        //
                                        sucessImport = false;
                                    }
                                   
                                    #endregion
                                    //
                                    #endregion                          
                                }
                                // End Duyệt qua tất cả các dòng trong file excel

                                ///////////////////////////NẾU THÀNH CÔNG THÌ SAVE/////////////////////////////////
                                if (sucessImport)
                                {
                                    SqlParameter[] pImport = new SqlParameter[3];
                                    pImport[0] = new SqlParameter("@OidQuanLy", OidQuanLy);
                                    pImport[1] = new SqlParameter("@String", sql.Substring(11));
                                    pImport[2] = new SqlParameter("@User", Common.SecuritySystemUser_GetCurrentUser().UserName);
                                    DataProvider.ExecuteNonQuery("spd_PMS_Import_NCKH", CommandType.StoredProcedure, pImport);
                                    //Lưu
                                    //uow.CommitChanges();
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

                                //
                                string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
                                DialogUtil.ShowInfo("Import Thành Công " + (sucessNumber - 1) + " dòng dữ liệu - Số dòng không thành công " + erorrNumber + " " + s + "!");

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
