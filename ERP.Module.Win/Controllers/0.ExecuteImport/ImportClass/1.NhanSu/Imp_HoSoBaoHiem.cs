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
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Enum.NhanSu;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.NhanSu
{
    public class Imp_HoSoBaoHiem
    {
        public static void XuLy(IObjectSpace obs, OfficeBaseObject typeOffice)
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
                    using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet1$A1:I]", typeOffice.LoaiOffice))
                    {                       
                        ThongTinNhanVien nhanVien;
                        HoSoBaoHiem hoSoBaoHiem;

                        using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                        {
                            uow.BeginTransaction();

                            using (DialogUtil.AutoWait())
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    //                                                                  
                                    int maQuanLy = 0;
                                    int hoTen = 1;
                                    int soSoBHXH = 2;
                                    int soTheBHYT = 3;
                                    int ngayThamGia = 4;
                                    int tuNgay = 5;
                                    int denNgay = 6;
                                    int noiDangKyKhamChuaBenh = 7;
                                    int tinhTrang = 8;

                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();
                                                                            
                                        String maQuanLyText = item[maQuanLy].ToString().FullTrim();     
                                        String hoTenText = item[hoTen].ToString().FullTrim();
                                        String soSoBHXHText = item[soSoBHXH].ToString().FullTrim();
                                        String soTheBHYTText = item[soTheBHYT].ToString().FullTrim();
                                        String ngayThamGiaText = item[ngayThamGia].ToString().FullTrim();
                                        String tuNgayText = item[tuNgay].ToString().FullTrim();
                                        String denNgayText = item[denNgay].ToString().FullTrim();
                                        String noiDangKyKhamChuaBenhText = item[noiDangKyKhamChuaBenh].ToString().FullTrim();
                                        String tinhTrangText = item[tinhTrang].ToString().FullTrim();

                                        #region 1. Mã quản lý
                                        if (!string.IsNullOrEmpty(maQuanLyText))
                                        {
                                            //Tìm nhân viên theo mã quản lý
                                            nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("CongTy = ? and (MaTapDoan = ? or MaNhanVien = ?)", typeOffice.CongTy.Oid, maQuanLyText, maQuanLyText));
                                            if (nhanVien != null)
                                            {
                                                if (!string.IsNullOrEmpty(soSoBHXHText))
                                                {
                                                    hoSoBaoHiem = uow.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien = ? and SoSoBHXH = ?", nhanVien.Oid, soTheBHYTText));
                                                    if (hoSoBaoHiem == null)
                                                    {
                                                        hoSoBaoHiem = new HoSoBaoHiem(uow);
                                                        hoSoBaoHiem.ThongTinNhanVien = nhanVien;                                                        
                                                    }
                                                   
                                                    #region Số thẻ BHXH
                                                    if (!string.IsNullOrEmpty(soTheBHYTText))
                                                    {
                                                        hoSoBaoHiem.SoTheBHYT = soTheBHYTText;                                                        
                                                    }                                                   
                                                    #endregion

                                                    #region Ngày tham gia
                                                    if (!string.IsNullOrEmpty(ngayThamGiaText))
                                                    {
                                                        try
                                                        {
                                                            hoSoBaoHiem.NgayThamGiaBHXH = Convert.ToDateTime(ngayThamGiaText);
                                                        }
                                                        catch
                                                        {
                                                            detailLog.AppendLine(" + Ngày tham gia không hợp lệ: " + ngayThamGiaText);
                                                        }
                                                    }                                                  
                                                    #endregion

                                                    #region Từ ngày
                                                    if (!string.IsNullOrEmpty(tuNgayText))
                                                    {
                                                        try
                                                        {
                                                            hoSoBaoHiem.TuNgay = Convert.ToDateTime(tuNgayText);
                                                        }
                                                        catch
                                                        {
                                                            detailLog.AppendLine(" + Từ ngày không hợp lệ: " + tuNgayText);
                                                        }
                                                    }                                                   
                                                    #endregion

                                                    #region Đến ngày
                                                    if (!string.IsNullOrEmpty(denNgayText))
                                                    {
                                                        try
                                                        {
                                                            hoSoBaoHiem.DenNgay = Convert.ToDateTime(denNgayText);
                                                        }
                                                        catch
                                                        {
                                                            detailLog.AppendLine(" + Đến ngày không hợp lệ: " + denNgayText);
                                                        }
                                                    }                                                   
                                                    #endregion                                                                                               

                                                    #region Nơi đăng ký khám chữa bệnh
                                                    if (!string.IsNullOrEmpty(noiDangKyKhamChuaBenhText))
                                                    {
                                                        BenhVien benhVien = uow.FindObject<BenhVien>(CriteriaOperator.Parse("TenBenhVien like ?", noiDangKyKhamChuaBenhText));
                                                        if (benhVien == null)
                                                        {
                                                            benhVien = new BenhVien(uow);
                                                            benhVien.TenBenhVien = noiDangKyKhamChuaBenhText;
                                                            uow.Save(benhVien);
                                                        }
                                                        hoSoBaoHiem.NoiDangKyKhamChuaBenh = benhVien;
                                                    }                                                    
                                                    #endregion

                                                    #region Tình trạng
                                                    if (!string.IsNullOrEmpty(tinhTrangText))
                                                    {
                                                        if (tinhTrangText.ToLower() == "đang tham gia")
                                                            hoSoBaoHiem.TrangThai = TrangThaiThamGiaBaoHiemEnum.DangThamGia;
                                                        else if (tinhTrangText.ToLower() == "giảm hẳn")
                                                            hoSoBaoHiem.TrangThai = TrangThaiThamGiaBaoHiemEnum.GiamHan;
                                                        else if (tinhTrangText.ToLower() == "giảm tạm thời")
                                                            hoSoBaoHiem.TrangThai = TrangThaiThamGiaBaoHiemEnum.GiamTamThoi;
                                                        else
                                                        {
                                                            detailLog.AppendLine(" + Tình trạng không hợp lệ: " + tinhTrangText);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("Tình trạng chưa có dữ liệu");
                                                    }
                                                    #endregion

                                                    //Đưa thông tin bị lỗi vào blog
                                                    if (detailLog.Length > 0)
                                                    {
                                                        mainLog.AppendLine(string.Format("- Không import hồ sơ bảo hiểm nhân viên [{0}] vào được: ", nhanVien.HoTen));
                                                        mainLog.AppendLine(detailLog.ToString());
                                                        sucessImport = false;
                                                    }
                                                }
                                                else
                                                {
                                                    mainLog.AppendLine(string.Format("- Số sổ BHXH hoặc Số thẻ BHYT của nhân viên : {0} không được trống.", hoTenText));
                                                    //
                                                    sucessImport = false;
                                                }
                                            }
                                            else
                                            {
                                                mainLog.AppendLine(string.Format("- Không có nhân viên {0}-{1} trong hệ thống.", maQuanLyText, hoTenText));
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
