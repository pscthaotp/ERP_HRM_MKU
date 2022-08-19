using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Extends;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.NonPersistentObjects.NhanSu;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.NhanSu.Helper;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.NhanSu
{
    public class Imp_QuyetDinhNangLuong
    {
        #region 1. Quyết định nâng lương
        public static void ImportQuyetDinhNangLuong(IObjectSpace obs, QuyetDinh_ChonNguoiKy obj)
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
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet1$A1:K]", obj.LoaiOffice))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            int idx_MaQuanLy = 0;
                            int idx_HoTen = 1;
                            int idx_NgachLuong = 3;
                            int idx_BacLuong = 4;
                            int idx_LuongChucDanh = 5;
                            int idx_LuongHQCV = 6;
                            int idx_NgayHuongLuong = 7;
                            int idx_SoQuyetDinh = 8;
                            int idx_NgayQuyetDinh = 9;
                            int idx_PhanTramHuongLuong = 10;
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
                                    String txt_MaQuanLy = dr[idx_MaQuanLy].ToString().FullTrim();
                                    String txt_HoTen = dr[idx_HoTen].ToString().FullTrim();
                                    String txt_NgachLuong = dr[idx_NgachLuong].ToString().FullTrim();
                                    String txt_BacLuong = dr[idx_BacLuong].ToString().FullTrim();
                                    String txt_LuongChucDanh = dr[idx_LuongChucDanh].ToString().FullTrim();
                                    String txt_LuongHQCV = dr[idx_LuongHQCV].ToString().FullTrim();
                                    String txt_NgayHuongLuong = dr[idx_NgayHuongLuong].ToString().FullTrim();
                                    String txt_SoQuyetDinh = dr[idx_SoQuyetDinh].ToString().FullTrim();
                                    String txt_NgayQuyetDinh = dr[idx_NgayQuyetDinh].ToString().FullTrim();
                                    String txt_PhanTramHuongLuong = dr[idx_PhanTramHuongLuong].ToString().FullTrim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                    #region Kiểm tra dữ
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaQuanLy))
                                    {
                                        ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("CongTy = ? and (MaTapDoan = ? or MaNhanVien = ?)", obj.CongTy.Oid, txt_MaQuanLy, txt_MaQuanLy));
                                        if (nhanVien == null)
                                        {
                                            mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống {2}.", txt_MaQuanLy,txt_HoTen,obj.CongTy.TenBoPhan));
                                            //
                                            sucessImport = false;
                                        }
                                        else
                                        {
                                            //
                                            QuyetDinhNangLuong quyetDinh = new QuyetDinhNangLuong(uow);
                                            quyetDinh.PhanLoaiNguoiKy = uow.GetObjectByKey<PhanLoaiNguoiKy>(obj.PhanLoaiNguoiKy.Oid);
                                            quyetDinh.ChucVuNguoiKy = uow.GetObjectByKey<ChucVuNguoiKy>(obj.ChucVuNguoiKy.Oid);
                                            quyetDinh.NguoiKy = uow.GetObjectByKey<ThongTinNhanVien>(obj.NguoiKy.Oid);
                                            quyetDinh.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                            //quyetDinh.IsDirty = true;
                                            //
                                            ChiTietQuyetDinhNangLuong chiTiet = new ChiTietQuyetDinhNangLuong(uow);
                                            chiTiet.ThongTinNhanVien = nhanVien;
                                            chiTiet.BoPhan = nhanVien.BoPhan;
                                            if (nhanVien.NhanVienThongTinLuong.NgachLuong != null)
                                                chiTiet.NgachLuongCu = uow.GetObjectByKey<NgachLuong>(nhanVien.NhanVienThongTinLuong.NgachLuong.Oid);
                                            if (nhanVien.NhanVienThongTinLuong.BacLuong != null)
                                                chiTiet.BacLuongCu = uow.GetObjectByKey<BacLuong>(nhanVien.NhanVienThongTinLuong.BacLuong.Oid);
                                            
                                            #region 1. Ngạch lương
                                            if (!string.IsNullOrEmpty(txt_NgachLuong))
                                            {
                                                NgachLuong ngachLuong = uow.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy=?", txt_NgachLuong));
                                                if (ngachLuong != null)
                                                {
                                                    chiTiet.NgachLuongMoi = ngachLuong;
                                                    if (!string.IsNullOrEmpty(txt_BacLuong))
                                                    {
                                                        BacLuong bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("MaQuanLy=?", txt_BacLuong));
                                                        if (bacLuong != null)
                                                        {
                                                            chiTiet.BacLuongMoi = bacLuong;
                                                            chiTiet.LuongCoBanMoi = bacLuong.LuongCoBan;
                                                            chiTiet.LuongKinhDoanhMoi = bacLuong.LuongKinhDoanh;
                                                        }
                                                        else
                                                        {
                                                            detailLog.AppendLine("+ Mã quản lý Bậc lương không hợp lệ.");
                                                        }
                                                    }
                                                    //else
                                                    //{
                                                    //    detailLog.AppendLine("+ Thiếu thông tin Bậc lương.");
                                                    //}
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("+ Mã quản lý Ngạch lương không hợp lệ.");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("+ Thiếu thông tin Ngạch lương.");
                                            //}
                                            #endregion

                                            #region 1. Lương chức danh
                                            if (!string.IsNullOrEmpty(txt_LuongChucDanh))
                                            {
                                                try
                                                {
                                                    decimal luongcb = Convert.ToDecimal(txt_LuongChucDanh);
                                                    if (chiTiet.LuongCoBanMoi != luongcb)
                                                        chiTiet.LuongCoBanMoi = luongcb;
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Lương chức danh mới không hợp lệ.");
                                                }
                                            }
                                            else 
                                            {
                                                detailLog.AppendLine("+ Thiếu thông tin Lương chức danh mới.");
                                            }
                                            #endregion

                                            #region 2. Lương hiệu quả công việc
                                            if (!string.IsNullOrEmpty(txt_LuongHQCV))
                                            {
                                                try
                                                {
                                                    decimal luongkd = Convert.ToDecimal(txt_LuongHQCV);
                                                    if (chiTiet.LuongKinhDoanhMoi != luongkd)
                                                        chiTiet.LuongKinhDoanhMoi = luongkd;
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Lương HQCV mới không hợp lệ.");
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("+ Thiếu thông tin Lương HQCV mới.");
                                            }
                                            #endregion

                                            #region 3. Ngày hưởng lương
                                            if (!string.IsNullOrEmpty(txt_NgayHuongLuong))
                                            {
                                                chiTiet.NgayHuongLuongMoi = Convert.ToDateTime(txt_NgayHuongLuong);
                                                quyetDinh.NgayHieuLuc = chiTiet.NgayHuongLuongMoi;
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("+ Thiếu thông tin Ngày hưởng lương mới.");
                                            }
                                            #endregion

                                            #region 4. Số quyết định
                                            if (!string.IsNullOrEmpty(txt_SoQuyetDinh))
                                            {
                                                quyetDinh.SoQuyetDinh = txt_SoQuyetDinh;
                                            }
                                            #endregion

                                            #region 5. Ngày quyết định
                                            if (!string.IsNullOrEmpty(txt_NgayQuyetDinh))
                                            {
                                                quyetDinh.NgayQuyetDinh = Convert.ToDateTime(txt_NgayQuyetDinh);
                                            }
                                            #endregion

                                            #region 6. Phần trăm hưởng lương
                                            if (!string.IsNullOrEmpty(txt_PhanTramHuongLuong))
                                            {
                                                chiTiet.PhanTramTinhLuongMoi = Convert.ToDecimal(txt_PhanTramHuongLuong);
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("+ Thiếu thông tin Phần trăm tính lương.");
                                            }
                                            #endregion

                                            //
                                            quyetDinh.ListChiTietQuyetDinhNangLuong.Add(chiTiet);
                                            //
                                            #region 6. Ghi File log
                                            {
                                                //Đưa thông tin bị lỗi vào blog
                                                if (detailLog.Length > 0)
                                                {
                                                    mainLog.AppendLine(string.Format("- Nhân viên có mã: {0} Tên: {1} không import vào phần mềm được: ", nhanVien.MaTapDoan, nhanVien.HoTen));
                                                    mainLog.AppendLine(detailLog.ToString());
                                                    //
                                                    sucessImport = false;
                                                }
                                            }
                                            #endregion

                                            //Nguyen cập nhật diễn biến lương sau khi import
                                            if (Convert.ToDateTime(txt_NgayHuongLuong) <= Common.GetServerCurrentTime())
                                            {
                                                nhanVien.NhanVienThongTinLuong.LuongCoBan = Convert.ToDecimal(txt_LuongChucDanh);
                                                nhanVien.NhanVienThongTinLuong.LuongKinhDoanh = Convert.ToDecimal(txt_LuongHQCV);
                                                nhanVien.NhanVienThongTinLuong.NgayHuongLuong = Convert.ToDateTime(txt_NgayHuongLuong);
                                                nhanVien.NhanVienThongTinLuong.PhanTramTinhLuong = Convert.ToDecimal(txt_PhanTramHuongLuong);

                                                chiTiet.JobUpdated = true;
                                            }

                                            //Cập nhật đến ngày của diễn biến lương trước đó = ngày hưởng lương mới - 1
                                            ProcessesHelper.UpdateDienBienLuong(uow, quyetDinh, nhanVien, Convert.ToDateTime(txt_NgayHuongLuong), true);

                                            //Tạo diễn biến lương khi lưu quyết định
                                            ProcessesHelper.CreateDienBienLuong(uow, quyetDinh, nhanVien, chiTiet);
                                            //
                                        }
                                    }
                                    else
                                    {
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
