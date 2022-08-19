using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
using ERP.Module.Extends;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ERP.Module.NghiepVu.TienLuong.Luong;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.TienLuong.ThuNhapKhac;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.NghiepVu.TienLuong.Thuong;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using DevExpress.Xpo.DB;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong
{
    public class Imp_KhenThuongPhucLoi
    {
        public static void ImportCacKhenThuongPhucLoi(IObjectSpace obs, BangThuongNhanVien obj, LoaiOfficeEnum loaiOffice)
        {
            //
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            StringBuilder mainLog = new StringBuilder();
            DateTime ngayLap = DateTime.Now;
            if (obj.KyTinhLuong != null)
                ngayLap = obj.KyTinhLuong.DenNgay.AddDays(1);
            //

            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel 2003 file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet1$A1:I]", loaiOffice))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            int idx_STT = 0;
                            int idx_MaNhanVien = 1;
                            int idx_HoTen = 2;
                            int idx_DonVi = 3;
                            int idx_NgayLap = 4;
                            int idx_NgayThuong = 5;
                            int idx_SoTienKhongChiuThe = 6;
                            int idx_SoTienChiuThe = 7;
                            int idx_GhiChu = 8;
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
                                    string txt_MaNhanVien = dr[idx_MaNhanVien].ToString().Trim();
                                    string txt_HoTen = dr[idx_HoTen].ToString().Trim();
                                    string txt_DonVi = dr[idx_DonVi].ToString().Trim();
                                    string txt_NgayLap = dr[idx_NgayLap].ToString().Trim();
                                    string txt_NgayThuong = dr[idx_NgayThuong].ToString().Trim();
                                    string txt_SoTienKhongChiuThue = dr[idx_SoTienKhongChiuThe].ToString().Trim();
                                    string txt_SoTienChiuThue = dr[idx_SoTienChiuThe].ToString().Trim();
                                    string txt_GhiChu = dr[idx_GhiChu].ToString().Trim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                    #region Kiểm tra dữ
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaNhanVien))
                                    {
                                        //CriteriaOperator filter = CriteriaOperator.Parse("MaNhanVien like ? or MaTapDoan like ?", txt_MaQuanLy, txt_MaQuanLy);
                                        //XPCollection<ThongTinNhanVien> nhanvienlist = new XPCollection<ThongTinNhanVien>(uow, filter);     
                                        ThongTinNhanVien nhanVien_qd = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaNhanVien like ? or MaTapDoan like ?", txt_MaNhanVien, txt_MaNhanVien));
                                        if (nhanVien_qd == null)
                                        {
                                            mainLog.AppendLine("- STT: " + txt_STT);
                                            mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống của {2}.", txt_MaNhanVien, txt_HoTen, obj.CongTy.TenBoPhan));
                                            //
                                            sucessImport = false;
                                        }
                                        else
                                        {
                                            ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("(MaNhanVien like ? or MaTapDoan like ?)", txt_MaNhanVien, txt_MaNhanVien));
                                            if (nhanVien != null)
                                            {
                                                ChiTietThuongNhanVien chiTietThuNhapKhac = uow.FindObject<ChiTietThuongNhanVien>(CriteriaOperator.Parse("BangThuongNhanVien=? and ThongTinNhanVien=?", obj.Oid, nhanVien.Oid));
                                                if (chiTietThuNhapKhac == null)
                                                {
                                                    chiTietThuNhapKhac = new ChiTietThuongNhanVien(uow);
                                                    chiTietThuNhapKhac.BangThuongNhanVien = uow.GetObjectByKey<BangThuongNhanVien>(obj.Oid);
                                                    chiTietThuNhapKhac.BoPhan = nhanVien.BoPhan;
                                                    chiTietThuNhapKhac.ThongTinNhanVien = nhanVien;
                                                }

                                                #region 1. Số tiền
                                                if (!string.IsNullOrEmpty(txt_SoTienKhongChiuThue))
                                                {
                                                    try
                                                    {
                                                        decimal soTien = Convert.ToDecimal(txt_SoTienKhongChiuThue);
                                                        //
                                                        chiTietThuNhapKhac.SoTien = soTien;
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("Số tiền không hợp lệ: " + txt_SoTienKhongChiuThue);
                                                    }
                                                }

                                                #endregion

                                                #region 2. Số tiền chịu thuế
                                                if (!string.IsNullOrEmpty(txt_SoTienChiuThue))
                                                {
                                                    try
                                                    {
                                                        decimal soTien = Convert.ToDecimal(txt_SoTienChiuThue);
                                                        //
                                                        chiTietThuNhapKhac.SoTienChiuThue = soTien;
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("Số tiền chịu thuế không hợp lệ: " + txt_SoTienChiuThue);
                                                    }
                                                }
                                                #endregion

                                                #region 3. Ngày lập
                                                if (!string.IsNullOrEmpty(txt_NgayLap))
                                                {
                                                    try
                                                    {
                                                        DateTime NgayLap = Convert.ToDateTime(txt_NgayLap);
                                                        //
                                                        chiTietThuNhapKhac.NgayLap = NgayLap;
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine("Ngày lập không hợp lệ: " + txt_NgayLap);
                                                    }
                                                }
                                                #endregion

                                                #region 4. Ghi chú
                                                if (!string.IsNullOrEmpty(txt_GhiChu))
                                                {
                                                    chiTietThuNhapKhac.GhiChu = txt_GhiChu;
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

                                                //else
                                                //{
                                                //    //Kiểm tra xem quyết định này có phải mới nhất không
                                                //    CriteriaOperator filter_qd = CriteriaOperator.Parse("ThongTinNhanVien=? and GetMonth(NgayHieuLuc)=? and GetYear(NgayHieuLuc)=?", nhanVien_qd.Oid, obj.KyTinhLuong.Thang, obj.KyTinhLuong.Nam);
                                                //    SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                                                //    XPCollection<QuyetDinhLuanChuyen> qdLuanChuyenList = new XPCollection<QuyetDinhLuanChuyen>(uow, filter_qd, sort);
                                                //    qdLuanChuyenList.TopReturnedObjects = 1;
                                                //    //
                                                //    if (qdLuanChuyenList.Count > 0)//>0
                                                //    {
                                                //        foreach (QuyetDinhLuanChuyen qdLuanChuyen in qdLuanChuyenList)
                                                //        {
                                                //            ChiTietThuNhapKhac chiTietThuNhapKhac = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? and ThongTinNhanVien=?", obj.Oid, qdLuanChuyen.ThongTinNhanVien));
                                                //            if (chiTietThuNhapKhac == null)
                                                //            {
                                                //                chiTietThuNhapKhac = new ChiTietThuNhapKhac(uow);
                                                //                chiTietThuNhapKhac.BangThuNhapKhac = uow.GetObjectByKey<BangThuNhapKhac>(obj.Oid);
                                                //                chiTietThuNhapKhac.BoPhan = qdLuanChuyen.BoPhan;
                                                //                chiTietThuNhapKhac.ThongTinNhanVien = qdLuanChuyen.ThongTinNhanVien;
                                                //            }

                                                //            #region 1. Số tiền
                                                //            if (!string.IsNullOrEmpty(txt_SoTienKhongChiuThue))
                                                //            {
                                                //                try
                                                //                {
                                                //                    decimal soTien = Convert.ToDecimal(txt_SoTienKhongChiuThue);
                                                //                    //
                                                //                    chiTietThuNhapKhac.SoTien = soTien;
                                                //                }
                                                //                catch
                                                //                {
                                                //                    detailLog.AppendLine("Số tiền không hợp lệ: " + txt_SoTienKhongChiuThue);
                                                //                }
                                                //            }

                                                //            #endregion

                                                //            #region 2. Số tiền chịu thuế
                                                //            if (!string.IsNullOrEmpty(txt_SoTienChiuThue))
                                                //            {
                                                //                try
                                                //                {
                                                //                    decimal soTien = Convert.ToDecimal(txt_SoTienChiuThue);
                                                //                    //
                                                //                    chiTietThuNhapKhac.SoTienChiuThue = soTien;
                                                //                }
                                                //                catch
                                                //                {
                                                //                    detailLog.AppendLine("Số tiền chịu thuế không hợp lệ: " + txt_SoTienChiuThue);
                                                //                }
                                                //            }
                                                //            #endregion

                                                //            #region 3. Ngày lập
                                                //            if (!string.IsNullOrEmpty(txt_NgayLap))
                                                //            {
                                                //                try
                                                //                {
                                                //                    DateTime NgayLap = Convert.ToDateTime(txt_NgayLap);
                                                //                    //
                                                //                    chiTietThuNhapKhac.NgayLap = ngayLap;
                                                //                }
                                                //                catch
                                                //                {
                                                //                    detailLog.AppendLine("Ngày lập không hợp lệ: " + txt_NgayLap);
                                                //                }
                                                //            }
                                                //            #endregion

                                                //            #region 4. Ghi chú
                                                //            if (!string.IsNullOrEmpty(txt_GhiChu))
                                                //            {
                                                //                chiTietThuNhapKhac.GhiChu = txt_GhiChu;
                                                //            }
                                                //            #endregion
                                                //        }
                                                //    }
                                                //    else
                                                //    {
                                                //        detailLog.AppendLine("Nhân viên không thuộc đơn vị quản lý");
                                                //    }

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
                                    //
                                    #endregion

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

    }
}
