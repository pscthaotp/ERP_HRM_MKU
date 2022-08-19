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
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.NhanSu
{
    public class Imp_QuyetDinhMienNhiem
    {
        public static void XuLy(IObjectSpace obs, QuyetDinh_ChonNguoiKy obj)
        {
            //
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            StringBuilder mainLog = new StringBuilder();
            StringBuilder detailLog;
            //
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet1$A1:I]", obj.LoaiOffice))
                    {
                        QuyetDinhMienNhiem quyetDinhMienNhiem;
                        ThongTinNhanVien nhanVien;                   

                        using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                        {
                            uow.BeginTransaction();

                            using (DialogUtil.AutoWait())
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    //
                                    int soQuyetDinh = 0;
                                    int ngayQuyetDinh = 1;
                                    int ngayHieuLuc = 2;                                 
                                    int maQuanLy = 3;
                                    int hoTen = 4;
                                    int chucVuMoi = 6;
                                    int chucDanhMoi = 7;                                   
                                    int ngayHetNhiemKy = 8;

                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();

                                        String soQuyetDinhText = item[soQuyetDinh].ToString().FullTrim();
                                        String ngayQuyetDinhText = item[ngayQuyetDinh].ToString().FullTrim();
                                        String ngayHieuLucText = item[ngayHieuLuc].ToString().FullTrim();                                       
                                        String maQuanLyText = item[maQuanLy].ToString().FullTrim();     
                                        String hoTenText = item[hoTen].ToString().FullTrim();     
                                        String chucVuMoiText = item[chucVuMoi].ToString().FullTrim();
                                        String chucDanhMoiText = item[chucDanhMoi].ToString().FullTrim();                                     
                                        String ngayHetNhiemKyText = item[ngayHetNhiemKy].ToString().FullTrim();

                                        #region 1. Mã quản lý
                                        if (!string.IsNullOrEmpty(maQuanLyText))
                                        {
                                            //Tìm nhân viên theo mã quản lý
                                            nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("CongTy = ? and (MaTapDoan = ? or MaNhanVien = ?)", obj.CongTy.Oid, maQuanLyText, maQuanLyText));
                                            if (nhanVien != null)
                                            {
                                                //Lấy dữ liệu quyết định đào tạo
                                                quyetDinhMienNhiem = new QuyetDinhMienNhiem(uow);
                                                quyetDinhMienNhiem.PhanLoaiNguoiKy = uow.GetObjectByKey<PhanLoaiNguoiKy>(obj.PhanLoaiNguoiKy.Oid);
                                                quyetDinhMienNhiem.ChucVuNguoiKy = uow.GetObjectByKey<ChucVuNguoiKy>(obj.ChucVuNguoiKy.Oid);
                                                quyetDinhMienNhiem.NguoiKy = uow.GetObjectByKey<ThongTinNhanVien>(obj.NguoiKy.Oid);
                                                quyetDinhMienNhiem.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);

                                                #region Số quyết định
                                                if (!string.IsNullOrEmpty(soQuyetDinhText))
                                                {
                                                    QuyetDinh quyetDinh = uow.FindObject<QuyetDinh>(CriteriaOperator.Parse("SoQuyetDinh =?", soQuyetDinhText));
                                                    if (quyetDinh == null)
                                                        quyetDinhMienNhiem.SoQuyetDinh = soQuyetDinhText;
                                                    else
                                                        detailLog.AppendLine("Số quyết định đã tồn tại: " + soQuyetDinhText);
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("Số quyết định chưa có dữ liệu");
                                                }
                                                #endregion

                                                #region Ngày quyết định
                                                if (!string.IsNullOrEmpty(ngayQuyetDinhText))
                                                {
                                                    try
                                                    {
                                                        quyetDinhMienNhiem.NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + ngayQuyetDinhText);
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("Ngày quyết định chưa có dữ liệu");
                                                }
                                                #endregion

                                                #region Ngày hiệu lực
                                                if (!string.IsNullOrEmpty(ngayHieuLucText))
                                                {
                                                    try
                                                    {
                                                        quyetDinhMienNhiem.NgayHieuLuc = Convert.ToDateTime(ngayHieuLucText);
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine(" + Ngày hiệu lực không hợp lệ: " + ngayHieuLucText);
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("Ngày hiệu lực chưa có dữ liệu");
                                                }
                                                #endregion

                                                #region Nhân viên
                                                if (!string.IsNullOrEmpty(maQuanLyText))
                                                {
                                                    quyetDinhMienNhiem.ThongTinNhanVien = nhanVien;
                                                    quyetDinhMienNhiem.BoPhan = nhanVien.BoPhan;
                                                    quyetDinhMienNhiem.BoPhanMoi = nhanVien.BoPhan;
                                                    quyetDinhMienNhiem.ChucVuCu = nhanVien.ChucVu;
                                                    quyetDinhMienNhiem.ChucDanhCu = nhanVien.ChucDanh;
                                                    quyetDinhMienNhiem.NgayBoNhiemCu = nhanVien.NgayBoNhiemChucVu;
                                                }
                                                #endregion

                                                #region Chức vụ mới
                                                if (!string.IsNullOrEmpty(chucVuMoiText))
                                                {
                                                    ChucVu ChucVu = uow.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu=?", chucVuMoiText));
                                                    if (ChucVu != null)
                                                        quyetDinhMienNhiem.ChucVuMoi = ChucVu;
                                                    else
                                                        detailLog.AppendLine("Chức vụ mới không hợp lệ");
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("Chức vụ mới chưa có dữ liệu");
                                                }
                                                #endregion

                                                #region Chức danh mới
                                                if (!string.IsNullOrEmpty(chucDanhMoiText))
                                                {
                                                    ChucDanh chucDanh = uow.FindObject<ChucDanh>(CriteriaOperator.Parse("TenChucDanh=?", chucDanhMoiText));
                                                    if (chucDanh != null)
                                                        quyetDinhMienNhiem.ChucDanhMoi = chucDanh;
                                                    else
                                                        detailLog.AppendLine("Chức danh mới không hợp lệ");
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("Chức danh mới chưa có dữ liệu");
                                                }
                                                #endregion

                                                #region Ngày hết nhiệm kỳ
                                                if (!string.IsNullOrEmpty(ngayHetNhiemKyText))
                                                {
                                                    try
                                                    {
                                                        quyetDinhMienNhiem.NgayHetNhiemKy = Convert.ToDateTime(ngayHetNhiemKyText);
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine(" + Ngày hết nhiệm kỳ không hợp lệ: " + ngayHetNhiemKyText);
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("Ngày hết nhiệm kỳ chưa có dữ liệu");
                                                }
                                                #endregion

                                                //Đưa thông tin bị lỗi vào blog
                                                if (detailLog.Length > 0)
                                                {
                                                    mainLog.AppendLine(string.Format("- Không import nhân viên [{0}] vào được: ", nhanVien.HoTen));
                                                    mainLog.AppendLine(detailLog.ToString());
                                                    sucessImport = false;
                                                }
                                            }
                                            else
                                            {
                                                mainLog.AppendLine(string.Format("- Không có nhân viên {0}-{1} trong hệ thống {2}.", maQuanLyText, hoTenText, obj.CongTy.TenBoPhan));
                                                //
                                                sucessImport = false;
                                            }
                                        }
                                        else
                                        {
                                            mainLog.AppendLine(string.Format("- Mã quản lý của nhân viên : {0} không được trống.", hoTenText));
                                            //
                                            sucessImport = false;
                                        }
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
                                }
                            }
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
