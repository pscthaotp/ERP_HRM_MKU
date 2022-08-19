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
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.NhanSu
{
    public class Imp_QuyetDinhKhenThuong
    {
        #region 1. Quyết định khen thưởng
        public static void ImportQuyetKhenThuong(IObjectSpace obs, QuyetDinh_ChonNguoiKy obj)
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
                            int idx_DanhHieu = 3;                           
                            int idx_NgayKhenThuong = 4;
                            int idx_SoQuyetDinh = 5;
                            int idx_NgayQuyetDinh = 6;
                            int idx_LyDo = 7;
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
                                    QuyetDinhKhenThuong quyetDinh;
                                    ChiTietQuyetDinhKhenThuongNhanVien chiTiet;

                                    //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                    #region Đọc dữ liệu
                                    String txt_MaQuanLy = dr[idx_MaQuanLy].ToString().FullTrim();
                                    String txt_HoTen = dr[idx_HoTen].ToString().FullTrim();
                                    String txt_DanhHieu = dr[idx_DanhHieu].ToString().FullTrim();
                                  
                                    String txt_NgayKhenThuong = dr[idx_NgayKhenThuong].ToString().FullTrim();
                                    String txt_SoQuyetDinh = dr[idx_SoQuyetDinh].ToString().FullTrim();
                                    String txt_NgayQuyetDinh = dr[idx_NgayQuyetDinh].ToString().FullTrim();
                                    String txt_LyDo = dr[idx_LyDo].ToString().FullTrim();
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
                                            mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống {2}.", txt_MaQuanLy, txt_HoTen, obj.CongTy.TenBoPhan));
                                            //
                                            sucessImport = false;
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(txt_SoQuyetDinh))
                                            {
                                                quyetDinh = uow.FindObject<QuyetDinhKhenThuong>(CriteriaOperator.Parse("CongTy = ? and SoQuyetDinh like ?", obj.CongTy.Oid, txt_SoQuyetDinh));
                                                if (quyetDinh == null)
                                                {
                                                    quyetDinh = new QuyetDinhKhenThuong(uow);
                                                    quyetDinh.PhanLoaiNguoiKy = uow.GetObjectByKey<PhanLoaiNguoiKy>(obj.PhanLoaiNguoiKy.Oid);
                                                    quyetDinh.ChucVuNguoiKy = uow.GetObjectByKey<ChucVuNguoiKy>(obj.ChucVuNguoiKy.Oid);
                                                    quyetDinh.NguoiKy = uow.GetObjectByKey<ThongTinNhanVien>(obj.NguoiKy.Oid);
                                                    quyetDinh.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                }
                                                //                                                
                                                chiTiet = uow.FindObject<ChiTietQuyetDinhKhenThuongNhanVien>(CriteriaOperator.Parse("QuyetDinhKhenThuong.Oid = ? and ThongTinNhanVien.Oid = ?", quyetDinh.Oid, nhanVien.Oid));
                                                if (chiTiet == null)
                                                {
                                                    chiTiet = new ChiTietQuyetDinhKhenThuongNhanVien(uow);
                                                    chiTiet.ThongTinNhanVien = nhanVien;
                                                    chiTiet.BoPhan = nhanVien.BoPhan;
                                                    chiTiet.QuyetDinhKhenThuong = quyetDinh;
                                                }

                                                #region 1. Danh hiệu
                                                if (!string.IsNullOrEmpty(txt_DanhHieu))
                                                {
                                                    DanhHieuKhenThuong danhHieu = uow.FindObject<DanhHieuKhenThuong>(CriteriaOperator.Parse("MaQuanLy=?", txt_DanhHieu));
                                                    if (danhHieu != null)
                                                    {
                                                        chiTiet.DanhHieuKhenThuong = danhHieu;
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("+ Mã quản lý Danh hiệu khen thưởng không hợp lệ.");
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("+ Thiếu thông tin Danh hiệu khen thưởng.");
                                                }
                                                #endregion

                                                #region 3. Ngày khen thưởng
                                                if (!string.IsNullOrEmpty(txt_NgayKhenThuong))
                                                {
                                                    quyetDinh.NgayHieuLuc = Convert.ToDateTime(txt_NgayKhenThuong);
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("+ Thiếu thông tin Ngày khen thưởng.");
                                                }
                                                #endregion

                                                #region 4. Số quyết định
                                                quyetDinh.SoQuyetDinh = txt_SoQuyetDinh;
                                                #endregion

                                                #region 5. Ngày quyết định
                                                if (!string.IsNullOrEmpty(txt_NgayQuyetDinh))
                                                {
                                                    quyetDinh.NgayQuyetDinh = Convert.ToDateTime(txt_NgayQuyetDinh);
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("+ Thiếu thông tin Ngày quyết định.");
                                                }
                                                #endregion

                                                #region 6. Lý do
                                                if (!string.IsNullOrEmpty(txt_LyDo))
                                                {
                                                    quyetDinh.LyDo = txt_LyDo;
                                                    chiTiet.LyDo = txt_LyDo;
                                                }
                                                #endregion

                                                quyetDinh.IsDirty = false;//Để lưu quá trình khi lần đầu import
                                                quyetDinh.ListChiTietQuyetDinhKhenThuongNhanVien.Add(chiTiet);                                                
                                                                                                
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
                                            }
                                            else
                                            {
                                                mainLog.AppendLine(string.Format("- Số quyết định không được trống"));
                                            }
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


