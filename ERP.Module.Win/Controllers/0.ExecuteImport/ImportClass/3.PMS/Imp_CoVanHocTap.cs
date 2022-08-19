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
using System.Data.SqlClient;
using ERP.Module.Enum.Systems;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.PMS
{  
    public class Imp_CoVanHocTap
    {

        #region 1. Chấm công
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
                open.Filter = "Excel 2003 file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet1$A2:D]", LoaiOfficeEnum.Office2003))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            const int idx_MaGV = 0;
                            const int idx_HoTen = 1;
                            const int idx_LopPhuTrach = 2;
                            const int idx_SoLuongSV = 3;
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
                                    string txt_MaGV = dr[idx_MaGV].ToString().Trim();
                                    string txt_HoTen = dr[idx_HoTen].ToString().Trim();
                                    string txt_LopPhuTrach = dr[idx_LopPhuTrach].ToString().Trim();
                                    string txt_SoLuongSV = dr[idx_SoLuongSV].ToString().Trim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////

                                    #region Kiểm tra dữ
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaGV))
                                    {
                                        NhanVien nhanVien = uow.FindObject<NhanVien>(CriteriaOperator.Parse("MaNhanVien = ?", txt_MaGV));
                                        if (nhanVien == null)
                                        {
                                            mainLog.AppendLine("- Mã: " + txt_MaGV);
                                            mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống.", txt_MaGV, txt_HoTen));
                                            //
                                            sucessImport = false;
                                        }
                                        else
                                        {
                                            sql += " Union All select N'" + nhanVien.Oid + "' as NhanVien"
                                                          + ", N'" + txt_LopPhuTrach + "' as LopPhuTrach"                                               
                                                          + ", N'" + txt_SoLuongSV + "' as SoLuongSV";
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
                                    DataProvider.ExecuteNonQuery("spd_PMS_Import_CoVanHocTap", CommandType.StoredProcedure, pImport);
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
        #endregion
    }
}
