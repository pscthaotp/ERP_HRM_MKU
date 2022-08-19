using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
using ERP.Module.Extends;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.DaoTao;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.NhanSu
{
    public class Imp_DangKyDaoTao
    {
        #region 1.Đăng ký đào tạo
        public static void ImportDangKyDaoTao(IObjectSpace obs, DangKyDaoTao obj, LoaiOfficeEnum loaiOffice)
        {
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
                            int idx_MaQuanLy = 1;
                            int idx_HoTen = 2; 
                            int idx_TenBoPhan = 3;
                            int idx_DonVi = 4;
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
                                    ThongTinNhanVien thongTinNhanVien;
                                    ChiTietDangKyDaoTao chiTietDangKyDaoTao;

                                    //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                    #region Đọc dữ liệu
                                    string txt_STT = dr[idx_STT].ToString().FullTrim();
                                    string txt_MaQuanLy = dr[idx_MaQuanLy].ToString().FullTrim();
                                    string txt_HoTen = dr[idx_HoTen].ToString().FullTrim(); ;
                                    string txt_TenBoPhan = dr[idx_TenBoPhan].ToString().FullTrim();
                                    string txt_DonVi = dr[idx_DonVi].ToString().FullTrim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                    #region Kiểm tra dữ
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaQuanLy) && !string.IsNullOrEmpty(txt_DonVi))
                                    {
                                        CriteriaOperator filter1 = CriteriaOperator.Parse("CongTy.MaBoPhan like ? and (MaNhanVien=? or MaTapDoan=?)", txt_DonVi, txt_MaQuanLy, txt_MaQuanLy);
                                        thongTinNhanVien = uow.FindObject<ThongTinNhanVien>(filter1);

                                        if (thongTinNhanVien != null)
                                        {
                                            chiTietDangKyDaoTao = uow.FindObject<ChiTietDangKyDaoTao>(CriteriaOperator.Parse("DangKyDaoTao.Oid=? and ThongTinNhanVien.Oid=?", obj.Oid, thongTinNhanVien.Oid));

                                            if (chiTietDangKyDaoTao == null)
                                            {
                                                chiTietDangKyDaoTao = new ChiTietDangKyDaoTao(uow);
                                                chiTietDangKyDaoTao.DangKyDaoTao = uow.GetObjectByKey<DangKyDaoTao>(obj.Oid);
                                                chiTietDangKyDaoTao.ThongTinNhanVien = thongTinNhanVien;
                                                chiTietDangKyDaoTao.BoPhan = uow.GetObjectByKey<BoPhan>(thongTinNhanVien.BoPhan.Oid);
                                                
                                                obj.ListChiTietDangKyDaoTao.Add(chiTietDangKyDaoTao);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Mã quản lý không tồn tại trong hệ thống: " + txt_MaQuanLy);
                                        }
                                       
                                        #region Ghi File log
                                        {
                                            //Đưa thông tin bị lỗi vào blog
                                            if (detailLog.Length > 0)
                                            {
                                                mainLog.AppendLine("- STT: " + txt_STT);
                                                mainLog.AppendLine(string.Format("- Nhân viên có mã quản lý {0} - {1} - {2} không import vào phần mềm được: ", txt_MaQuanLy, txt_HoTen, txt_TenBoPhan));
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
                                        mainLog.AppendLine(string.Format("- Mã quản lý của nhân viên không được trống. Mã đơn vị không được trống."));
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
