using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ERP.Module.NghiepVu.TienLuong.KhauTru;
using ERP.Module.Enum.Systems;
using ERP.Module.Extends;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using DevExpress.Xpo.DB;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong
{
    public class Imp_KhauTruLuong
    {
        #region 1. Khấu trừ lương
        public static void ImportKhauTruLuong(IObjectSpace obs, BangKhauTruLuong obj, LoaiOfficeEnum loaiOffice)
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
                            int idx_MaQuanLy = 1;
                            int idx_HoTen = 2;
                            int idx_DonVi = 3;
                            int idx_NgayLap = 4;
                            int idx_SoTien = 5;
                            int idx_SoTienChiuThue = 6;
                            int idx_GhiChu = 7;
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
                                    String txt_DonVi = dr[idx_DonVi].ToString().FullTrim();
                                    String txt_NgayLap = dr[idx_NgayLap].ToString().FullTrim();
                                    String txt_SoTien = dr[idx_SoTien].ToString().FullTrim();
                                    String txt_SoTienChiuThue = dr[idx_SoTienChiuThue].ToString().FullTrim();
                                    String txt_GhiChu = dr[idx_GhiChu].ToString().FullTrim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                    #region Kiểm tra dữ
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaQuanLy))
                                    {
                                        ThongTinNhanVien nhanVien_qd = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaNhanVien like ? or MaTapDoan like ?", txt_MaQuanLy, txt_MaQuanLy));
                                        if (nhanVien_qd == null)
                                        {
                                            mainLog.AppendLine("- STT: " + txt_STT);
                                            mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống.", txt_MaQuanLy, txt_HoTen));
                                            //
                                            sucessImport = false;
                                        }
                                        else
                                        {
                                            //
                                            ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("CongTy = ? and (MaNhanVien like ? or MaTapDoan like ?)", obj.CongTy.Oid, txt_MaQuanLy, txt_MaQuanLy));
                                            if (nhanVien != null)
                                            {
                                                ChiTietKhauTruLuong chiTietKhauTru = uow.FindObject<ChiTietKhauTruLuong>(CriteriaOperator.Parse("BangKhauTruLuong=? and ThongTinNhanVien=?", obj.Oid, nhanVien.Oid));
                                                if (chiTietKhauTru == null)
                                                {
                                                    chiTietKhauTru = new ChiTietKhauTruLuong(uow);
                                                    chiTietKhauTru.BangKhauTruLuong = uow.GetObjectByKey<BangKhauTruLuong>(obj.Oid);
                                                    chiTietKhauTru.BoPhan = nhanVien.BoPhan;
                                                    chiTietKhauTru.ThongTinNhanVien = nhanVien;
                                                }

                                                #region 1.Số tiền khấu trừ
                                                if (!string.IsNullOrEmpty(txt_SoTien))
                                                {
                                                    try
                                                    {
                                                        Decimal soTienKhauTru = Convert.ToDecimal(txt_SoTien);
                                                        chiTietKhauTru.SoTien = soTienKhauTru;
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Số tiền khấu trừ không đúng định dạng: " + txt_SoTien);
                                                    }
                                                }
                                                #endregion

                                                #region 2. Số tiền chịu thuế
                                                if (!string.IsNullOrEmpty(txt_SoTienChiuThue))
                                                {
                                                    try
                                                    {
                                                        Decimal soTienChiuThue = Convert.ToDecimal(txt_SoTienChiuThue);
                                                        chiTietKhauTru.SoTienChiuThue = soTienChiuThue;
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Số tiền chịu thuế không đúng định dạng: " + txt_SoTienChiuThue);
                                                    }
                                                }
                                                #endregion

                                                #region 3. Ngày khấu trừ
                                                if (!string.IsNullOrEmpty(txt_NgayLap))
                                                {
                                                    try
                                                    {
                                                        DateTime ngayKhauTru = Convert.ToDateTime(txt_NgayLap);
                                                        chiTietKhauTru.NgayKhauTru = ngayKhauTru;
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Ngày khấu trừ không đúng định dạng: " + txt_NgayLap);
                                                    }

                                                }
                                                #endregion

                                                #region 4. Ghi chú
                                                if (!string.IsNullOrEmpty(txt_GhiChu))
                                                {
                                                    chiTietKhauTru.GhiChu = txt_GhiChu;
                                                }
                                                #endregion

                                                #region Ghi File log
                                                {
                                                    //Đưa thông tin bị lỗi vào blog
                                                    if (detailLog.Length > 0)
                                                    {
                                                        mainLog.AppendLine("- STT: " + txt_STT);
                                                        mainLog.AppendLine(string.Format("- Cán bộ Mã: {0} Tên: {1} không import vào phần mềm được: ", nhanVien.MaNhanVien, nhanVien.HoTen));
                                                        mainLog.AppendLine(detailLog.ToString());
                                                        //
                                                        sucessImport = false;
                                                    }
                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                //Kiểm tra xem nhân viên có quyết định luân chuyển trong tháng không ?
                                                CriteriaOperator filter_qd = CriteriaOperator.Parse("ThongTinNhanVien=? and GetMonth(NgayHieuLuc)=? and GetYear(NgayHieuLuc)=?", nhanVien_qd.Oid, obj.KyTinhLuong.Thang, obj.KyTinhLuong.Nam);
                                                SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                                                XPCollection<QuyetDinhLuanChuyen> qdLuanChuyenList = new XPCollection<QuyetDinhLuanChuyen>(uow, filter_qd, sort);
                                                qdLuanChuyenList.TopReturnedObjects = 1;
                                                //
                                                if (qdLuanChuyenList.Count > 0)
                                                {
                                                    foreach (QuyetDinhLuanChuyen qdLuanChuyen in qdLuanChuyenList)
                                                    {
                                                        ChiTietKhauTruLuong chiTietKhauTru = uow.FindObject<ChiTietKhauTruLuong>(CriteriaOperator.Parse("BangKhauTruLuong=? and ThongTinNhanVien=?", obj.Oid, qdLuanChuyen.ThongTinNhanVien));
                                                        if (chiTietKhauTru == null)
                                                        {
                                                            chiTietKhauTru = new ChiTietKhauTruLuong(uow);
                                                            chiTietKhauTru.BangKhauTruLuong = uow.GetObjectByKey<BangKhauTruLuong>(obj.Oid);
                                                            chiTietKhauTru.BoPhan = qdLuanChuyen.BoPhan;
                                                            chiTietKhauTru.ThongTinNhanVien = qdLuanChuyen.ThongTinNhanVien;
                                                        }

                                                        #region 1.Số tiền khấu trừ
                                                        if (!string.IsNullOrEmpty(txt_SoTien))
                                                        {
                                                            try
                                                            {
                                                                Decimal soTienKhauTru = Convert.ToDecimal(txt_SoTien);
                                                                chiTietKhauTru.SoTien = soTienKhauTru;
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                detailLog.Append("+ Số tiền khấu trừ không đúng định dạng: " + txt_SoTien);
                                                            }
                                                        }
                                                        #endregion

                                                        #region 2. Số tiền chịu thuế
                                                        if (!string.IsNullOrEmpty(txt_SoTienChiuThue))
                                                        {
                                                            try
                                                            {
                                                                Decimal soTienChiuThue = Convert.ToDecimal(txt_SoTienChiuThue);
                                                                chiTietKhauTru.SoTienChiuThue = soTienChiuThue;
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                detailLog.Append("+ Số tiền chịu thuế không đúng định dạng: " + txt_SoTienChiuThue);
                                                            }
                                                        }
                                                        #endregion

                                                        #region 3. Ngày khấu trừ
                                                        if (!string.IsNullOrEmpty(txt_NgayLap))
                                                        {
                                                            try
                                                            {
                                                                DateTime ngayKhauTru = Convert.ToDateTime(txt_NgayLap);
                                                                chiTietKhauTru.NgayKhauTru = ngayKhauTru;
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                detailLog.Append("+ Ngày khấu trừ không đúng định dạng: " + txt_NgayLap);
                                                            }

                                                        }
                                                        #endregion

                                                        #region 4. Ghi chú
                                                        if (!string.IsNullOrEmpty(txt_GhiChu))
                                                        {
                                                            chiTietKhauTru.GhiChu = txt_GhiChu;
                                                        }
                                                        #endregion
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("Nhân viên không thuộc đơn vị quản lý");
                                                }

                                                #region Ghi File log
                                                {
                                                    //Đưa thông tin bị lỗi vào blog
                                                    if (detailLog.Length > 0)
                                                    {
                                                        mainLog.AppendLine("- STT: " + txt_STT);
                                                        mainLog.AppendLine(string.Format("- Nhân viên Mã: {0} Tên: {1} không import vào phần mềm được: ", nhanVien_qd.MaNhanVien, nhanVien_qd.HoTen));
                                                        mainLog.AppendLine(detailLog.ToString());
                                                        //
                                                        sucessImport = false;
                                                    }
                                                }
                                                #endregion
                                            }
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
