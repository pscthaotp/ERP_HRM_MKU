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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong
{
    //
    public class Imp_ChamCongNgoaiGio
    {

        #region 1. Chấm công ngoài giờ
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
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet1$A1:H]", obj.LoaiOffice))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            int idx_STT = 0;
                            int idx_MaQuanLy = 1;
                            int idx_HoTen = 2;
                            int idx_TongSoGio_KCT = 3;
                            int idx_TongSoGio_CT = 4;
                            int idx_CongChuan = 5;
                            int idx_DienGiai = 6;
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
                                    String txt_TongSoGio_CT = dr[idx_TongSoGio_CT].ToString().FullTrim();
                                    String txt_TongSoGio_KCT = dr[idx_TongSoGio_KCT].ToString().FullTrim();
                                    String txt_CongChuan = dr[idx_CongChuan].ToString().FullTrim();
                                    String txt_DienGiai = dr[idx_DienGiai].ToString().FullTrim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////

                                    #region Kiểm tra dữ
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaQuanLy))
                                    {
                                        ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("CongTy = ? and MaNhanVien like ? or MaTapDoan like ?", obj.CongTy.Oid, txt_MaQuanLy, txt_MaQuanLy ));
                                        if (nhanVien == null)
                                        {
                                            mainLog.AppendLine("- STT: " + txt_STT);
                                            mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống.", txt_MaQuanLy,txt_HoTen));
                                            //
                                            sucessImport = false;
                                        }
                                        else
                                        {
                                            // Thêm chi tiết công ngoài giờ
                                            CriteriaOperator filter = CriteriaOperator.Parse("QuanLyCongNgoaiGio.Oid=? and ThongTinNhanVien.Oid=?",OidQuanLy, nhanVien.Oid);
                                            CC_ChiTietCongNgoaiGio chiTiet = uow.FindObject<CC_ChiTietCongNgoaiGio>(filter);
                                            if (chiTiet == null)
                                            {
                                                //
                                                chiTiet = new CC_ChiTietCongNgoaiGio(uow);
                                                chiTiet.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                chiTiet.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                                chiTiet.QuanLyCongNgoaiGio = uow.GetObjectByKey<CC_QuanLyCongNgoaiGio>(OidQuanLy);
                                            }

                                            #region 1. Tổng số giờ chịu thuế
                                            if (!string.IsNullOrEmpty(txt_TongSoGio_CT))
                                            {
                                                try
                                                {
                                                    decimal soGio = Convert.ToDecimal(txt_TongSoGio_CT.Replace(".", ","));
                                                    chiTiet.TongSoGio_CT = soGio;
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine("+ Tổng số giờ chịu thuế không hợp lệ: " + txt_TongSoGio_CT);

                                                }
                                            }
                                            #endregion

                                            #region 2. Tổng số giờ không chịu thuế
                                            if (!string.IsNullOrEmpty(txt_TongSoGio_KCT))
                                            {
                                                try
                                                {
                                                    decimal soGio = Convert.ToDecimal(txt_TongSoGio_KCT.Replace(".", ","));
                                                    chiTiet.TongSoGio_KCT = soGio;
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine("+ Tổng số giờ không chịu thuế không hợp lệ: " + txt_TongSoGio_KCT);

                                                }
                                            }
                                            #endregion

                                            #region 3. Số ngày công chuẩn theo loại giờ làm việc
                                            if (!string.IsNullOrEmpty(txt_CongChuan))
                                            {
                                                try
                                                {
                                                    decimal congChuan = Convert.ToDecimal(txt_CongChuan.Replace(".", ","));
                                                    chiTiet.CongChuanTheoLoaiGioLamViec = congChuan;
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine("+ Công chuẩn theo Loại giờ làm việc không hợp lệ: " + txt_CongChuan);

                                                }
                                            }
                                            #endregion

                                            #region 4. Diễn giải
                                            if (!string.IsNullOrEmpty(txt_DienGiai))
                                            {
                                                chiTiet.DienGiai = txt_DienGiai;
                                            }
                                            #endregion

                                            #region 8. Ghi File log
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
