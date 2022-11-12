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
    public class Imp_HoatDongKhac
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
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet$A2:H]", LoaiOfficeEnum.Office2010))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            const int idx_STT = 0;
                            const int idx_MaGV = 1;
                            const int idx_HoTen = 2;
                            const int idx_HocVi = 3;
                            const int idx_BoPhan = 4;
                            const int idx_LoaiHoatDong = 5;
                            const int idx_GioQuyDoi = 6;
                            const int idx_GhiChu = 7;
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
                                    string txt_LoaiHoatDong = dr[idx_LoaiHoatDong].ToString().Trim();
                                    string txt_GioQuyDoi = dr[idx_GioQuyDoi].ToString().Trim();
                                    string txt_GhiChu = dr[idx_GhiChu].ToString().Trim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////

                                    #region Kiểm tra dữ
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaGV) && !string.IsNullOrEmpty(txt_LoaiHoatDong) && !string.IsNullOrEmpty(txt_GioQuyDoi))
                                    {
                                        NhanVien nhanVien = uow.FindObject<NhanVien>(CriteriaOperator.Parse("MaNhanVien = ?", txt_MaGV));
                                        DanhMucHoatDongKhac _DanhMucHoatDongKhac = uow.FindObject<DanhMucHoatDongKhac>(CriteriaOperator.Parse("MaQuanLy = ? or TenLoaiHoatDong=?", txt_LoaiHoatDong, txt_LoaiHoatDong));
                                        if (nhanVien == null)
                                        {
                                            mainLog.AppendLine("- Mã: " + txt_MaGV);
                                            mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống.", txt_MaGV, txt_HoTen));
                                            //
                                            sucessImport = false;
                                        }
                                        else if (_DanhMucHoatDongKhac == null)
                                        {
                                            mainLog.AppendLine("- Danh mục hoạt động khác: " + txt_LoaiHoatDong);
                                            mainLog.AppendLine(string.Format("- Mã hoặc tên danh mục hoạt động khác :{0} không tồn tại trong hệ thống.", txt_LoaiHoatDong));
                                            
                                            sucessImport = false;
                                        }
                                        else
                                        {
                                            sql += " Union All select N'" + nhanVien.Oid + "' as NhanVien"
                                                          + ", N'" + _DanhMucHoatDongKhac.Oid + "' as LoaiHoatDong"
                                                          + ", N'" + txt_GhiChu + "' as GhiChu"
                                                          + ", N'" + txt_GioQuyDoi + "' as GioQuyDoi";
                                            sucessNumber++;
                                        }
                                    }
                                    else
                                    {
                                        mainLog.AppendLine("- Mã: " + txt_MaGV);
                                        mainLog.AppendLine(string.Format("- Dữ liệu của nhân viên : {0} không được để trống.", txt_HoTen));
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
                                    DataProvider.ExecuteNonQuery("spd_PMS_Import_HoatDongKhac", CommandType.StoredProcedure, pImport);
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
