using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
using ERP.Module.Extends;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Xpo.DB;
using ERP.Module.NghiepVu.NhanSu.DinhBien;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.NhanSu;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.NhanSu
{
    public class Imp_DinhBienChucDanh
    {
        #region 1.Định biên nhân sự
        public static void ImportDinhBienChucDanh(IObjectSpace obs, QuanLyDinhBienChucDanh obj, LoaiOfficeEnum loaiOffice)
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
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet1$A1:H]", loaiOffice))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            int idx_STT = 0;
                            int idx_MaQuanLyBoPhan = 1;
                            int idx_TenBoPhan = 2;
                            int idx_MaQuanLyChucVu = 3;
                            int idx_TenChucVu = 4;
                            int idx_MaQuanLyChucDanh= 5;
                            int idx_TenChucDanh = 6;                            
                            int idx_SoLuongDinhBien = 7;                          
                         
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
                                    BoPhan boPhan = null;
                                    ChucDanh chucDanh = null;
                                    ChucVu chucVu = null;

                                    //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                    #region Đọc dữ liệu
                                    String txt_STT = dr[idx_STT].ToString().FullTrim();
                                    String txt_MaQuanLyBoPhan = dr[idx_MaQuanLyBoPhan].ToString().FullTrim();
                                    String txt_MaQuanLyChucDanh = dr[idx_MaQuanLyChucDanh].ToString().FullTrim();
                                    String txt_MaQuanLyChucVu = dr[idx_MaQuanLyChucVu].ToString().FullTrim();
                                    String txt_SoLuongDinhBien = dr[idx_SoLuongDinhBien].ToString().FullTrim();                                  
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                    #region Kiểm tra dữ
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaQuanLyBoPhan) && !string.IsNullOrEmpty(txt_MaQuanLyChucVu) && !string.IsNullOrEmpty(txt_MaQuanLyChucDanh))
                                    {
                                        CriteriaOperator filter1 = CriteriaOperator.Parse("MaQuanLy=? and CongTy=?", txt_MaQuanLyBoPhan,obj.CongTy.Oid);
                                        boPhan = uow.FindObject<BoPhan>(filter1);
                                        if (boPhan == null)
                                            detailLog.AppendLine("Bộ phận không tồn tại. Mã quản lý Bộ phận không hợp lệ: " + txt_MaQuanLyBoPhan);

                                        CriteriaOperator filter2 = CriteriaOperator.Parse("MaQuanLy=?", txt_MaQuanLyChucVu);
                                        chucVu = uow.FindObject<ChucVu>(filter2);
                                        if (chucVu == null)
                                            detailLog.AppendLine("Chức vụ không tồn tại. Mã quản lý Chức vụ không hợp lệ: " + txt_MaQuanLyChucVu);
                                        else
                                        {
                                            CriteriaOperator filter3 = CriteriaOperator.Parse("MaQuanLy=? and ChucVu=?", txt_MaQuanLyChucDanh, chucVu.Oid);
                                            chucDanh = uow.FindObject<ChucDanh>(filter3);
                                            if (chucDanh == null)
                                                detailLog.AppendLine("Chức danh không tồn tại. Mã quản lý Chức danh không hợp lệ: " + txt_MaQuanLyChucVu);
                                        }

                                        if (boPhan != null && chucVu != null && chucDanh != null)
                                        {
                                            DinhBienChucDanh dbChucDanh = uow.FindObject<DinhBienChucDanh>(CriteriaOperator.Parse("BoPhan=? and ChucVu=? and ChucDanh=?", boPhan.Oid, chucVu.Oid, chucDanh.Oid));

                                            if (dbChucDanh == null)
                                            {
                                                dbChucDanh = new DinhBienChucDanh(uow);
                                                dbChucDanh.QuanLyDinhBienChucDanh = uow.GetObjectByKey<QuanLyDinhBienChucDanh>(obj.Oid);
                                                dbChucDanh.BoPhan = boPhan;
                                                dbChucDanh.ChucVu = chucVu;
                                                dbChucDanh.ChucDanh = chucDanh;
                                            }

                                            #region 4. Số lượng định biên
                                            if (!string.IsNullOrEmpty(txt_SoLuongDinhBien))
                                            {
                                                try
                                                {
                                                    int soLuong = Convert.ToInt16(txt_SoLuongDinhBien);
                                                    dbChucDanh.SoLuong = soLuong;
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine("Số lượng định biên không hợp lệ: " + txt_SoLuongDinhBien);
                                                }
                                            }
                                            #endregion                                           
                                        }

                                        #region Ghi File log
                                        {
                                            //Đưa thông tin bị lỗi vào blog
                                            if (detailLog.Length > 0)
                                            {
                                                mainLog.AppendLine("- STT: " + txt_STT);
                                                mainLog.AppendLine(string.Format("- Định biên cho {0} - {1} - {2} không import vào phần mềm được: ", txt_MaQuanLyBoPhan, txt_MaQuanLyChucVu, txt_MaQuanLyChucDanh));
                                                mainLog.AppendLine(detailLog.ToString());
                                                //
                                                sucessImport = false;
                                            }
                                        }
                                        #endregion
                                       
                                    }
                                    else
                                    {
                                        mainLog.AppendLine("- STT: " + txt_STT);
                                        mainLog.AppendLine(string.Format("- Mã quản lý của bộ phận, Mã quản lý của chức vụ, Mã Quản lý của chức danh không được trống."));
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
