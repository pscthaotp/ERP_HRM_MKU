using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Web.Internal;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.TuyenSinh;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.NonPersistentObjects.HeThong;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
//
namespace ERP.Module.Web.Controllers.ImportClass
{
    internal static class StringEx
    {
        internal static String FullTrim(this String source)
        {
            return source.Trim().Replace("  ", " ");
        }

        internal static String RemoveEmpty(this String source)
        {
            return source.Trim().Replace(" ", "");
        }
    }

    public class Imp_TuVanTuyenSinh
    {
        #region 1. Import tư vấn tuyển sinh
        public static void ImportTuVanTuyenSinh(IObjectSpace obs, TuVanTuyenSinh obj, OfficeBaseObject_Web typeOffice)
        {
            //
            int erorrNumber = 0;
            bool sucessImport = true;
            StringBuilder mainLog = new StringBuilder();
            //
            string fullPath = string.Empty;
            if (typeOffice.File != null)
                fullPath = HttpContext.Current.Server.MapPath("~/Downloads/" + typeOffice.File.FileName);

            //Lưu file lại theo đường dẫn cấu hình
            File.WriteAllBytes(fullPath, typeOffice.File.Content);
            //
            using (DataTable dt = DataProvider.GetDataTableFromExcel(fullPath, "[Sheet1$A1:F]", typeOffice.LoaiOffice))
            {
                /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                #region Khởi tạo các idx
                //
                int idx_MaQuanLy = 0;
                int idx_HoTen = 1;
                int idx_NgayTuVan = 2;
                int idx_HinhThuc = 3;
                int idx_LoaiKhachHang = 4;
                int idx_NoiDung = 5;
                #endregion

                /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                //Duyệt qua tất cả các dòng trong file excel
                foreach (DataRow dr in dt.Rows)
                {
                    StringBuilder detailLog = new StringBuilder();

                    //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                    #region Đọc dữ liệu
                    String txt_MaQuanLy = dr[idx_MaQuanLy].ToString().FullTrim();
                    String txt_HoTen = dr[idx_HoTen].ToString().FullTrim();
                    String txt_NgayTuVan = dr[idx_NgayTuVan].ToString().FullTrim();
                    String txt_HinhThuc = dr[idx_HinhThuc].ToString().FullTrim();
                    String txt_LoaiKhachHang = dr[idx_LoaiKhachHang].ToString().FullTrim();
                    String txt_NoiDung = dr[idx_NoiDung].ToString().FullTrim();
                    #endregion

                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                    #region Kiểm tra dữ
                    //
                    #region 1. Mã quản lý
                    if (!string.IsNullOrEmpty(txt_MaQuanLy))
                    {
                        ThongTinKhachHang khachHang = ((XPObjectSpace)obs).Session.FindObject<ThongTinKhachHang>(CriteriaOperator.Parse("MaKhachHang = ? AND CongTy = ?", txt_MaQuanLy, obj.CongTy.Oid));
                        if (khachHang == null)
                        {
                            mainLog.AppendLine(string.Format("- Mã quản lý: [{0}] của khách hàng: [{1}] không tồn tại trong hệ thống.", txt_MaQuanLy, txt_HoTen));
                            //
                            sucessImport = false;
                        }
                        else
                        {
                            //
                            TuVanTuyenSinh tuVanTuyenSinh = ((XPObjectSpace)obs).Session.GetObjectByKey<TuVanTuyenSinh>(obj.Oid);
                            if (tuVanTuyenSinh == null) break;
                            //
                            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinKhachHang=? and TuVanTuyenSinh=?", khachHang.Oid, obj.Oid);
                            ChiTietTuVanTuyenSinh chiTietTuVan = ((XPObjectSpace)obs).Session.FindObject<ChiTietTuVanTuyenSinh>(filter);
                            if (chiTietTuVan == null)
                            {
                                chiTietTuVan = new ChiTietTuVanTuyenSinh(((XPObjectSpace)obs).Session);
                            }
                            chiTietTuVan.TuVanTuyenSinh = tuVanTuyenSinh;
                            chiTietTuVan.ThongTinKhachHang = ((XPObjectSpace)obs).Session.GetObjectByKey<ThongTinKhachHang>(khachHang.Oid);
                            //

                            #region 2. Ngày tư vấn
                            if (!string.IsNullOrEmpty(txt_NgayTuVan))
                            {
                                try
                                {
                                    chiTietTuVan.NgayTuVan = Convert.ToDateTime(txt_NgayTuVan);
                                }
                                catch (Exception ex)
                                {
                                    detailLog.AppendLine(" + Ngày tư vấn không hợp lệ: " + txt_NgayTuVan);
                                }
                            }
                            else
                            {
                                detailLog.AppendLine(" + Ngày tư vấn không tìm thấy.");
                            }
                            #endregion

                            #region 3. Hình thức
                            if (!string.IsNullOrEmpty(txt_HinhThuc))
                            {
                                if (txt_HinhThuc.Contains("trực tiếp"))
                                    chiTietTuVan.HinhThucTuVan = Enum.TuyenSinh.HinhThucTuVanEnum.TrucTiep;
                                else if (txt_HinhThuc.Contains("email"))
                                    chiTietTuVan.HinhThucTuVan = Enum.TuyenSinh.HinhThucTuVanEnum.Email;
                                else
                                    chiTietTuVan.HinhThucTuVan = Enum.TuyenSinh.HinhThucTuVanEnum.DienThoai;
                            }
                            else
                            {
                                detailLog.AppendLine(" + Hình thức tư vấn không tìm thấy.");
                            }
                            #endregion

                            #region 4. Nguồn thu nhập
                            if (!string.IsNullOrEmpty(txt_LoaiKhachHang))
                            {
                                //
                                filter = CriteriaOperator.Parse("TenLoaiKhachHang like ?", txt_LoaiKhachHang);
                                LoaiKhachHang loaiKhachHang = ((XPObjectSpace)obs).Session.FindObject<LoaiKhachHang>(filter);
                                if (loaiKhachHang != null)
                                {
                                    chiTietTuVan.LoaiKhachHang = loaiKhachHang;
                                }
                                else
                                {
                                    detailLog.AppendLine(" + Tên loại khách hàng không hợp lệ: " + txt_LoaiKhachHang);
                                }
                            }
                            else
                            {
                                detailLog.AppendLine(" + Loại khách hàng không tìm thấy.");
                            }
                            #endregion

                            #region 5. Nội dung
                            if (!string.IsNullOrEmpty(txt_NoiDung))
                            {
                                chiTietTuVan.NoiDung = txt_NoiDung;
                            }
                            else
                            {
                                detailLog.AppendLine(" + Nội dung tư vấn không tìm thấy.");
                            }
                            #endregion

                            #region 18. Ghi File log
                            {
                                //Đưa thông tin bị lỗi vào blog
                                if (detailLog.Length > 0)
                                {
                                    mainLog.AppendLine(string.Format("- Cán bộ Mã: [{0}] Tên: [{1}] không import vào phần mềm được: ", khachHang.MaKhachHang, khachHang.HoTen));
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
                        mainLog.AppendLine(string.Format("- Mã của khách hàng: [{0}] không được trống.", txt_HoTen));
                        //
                        sucessImport = false;
                    }

                    #endregion
                    //
                    #endregion

                    ///////////////////////////NẾU THÀNH CÔNG THÌ SAVE/////////////////////////////////
                    if (!sucessImport)
                    {
                        erorrNumber++;
                        //
                        sucessImport = true;
                    }
                }
                // End Duyệt qua tất cả các dòng trong file excel

                //Xuất kết quả
                if (erorrNumber > 0)
                {
                    //
                    string message = "alert('Import không thành công: " + erorrNumber + " dòng.')";
                    message = message + "";
                    //
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                    //
                    string path = HttpContext.Current.Server.MapPath(Config.URLErorrImportExcel);

                    //Tạo file
                    Common.WriteDataToFile(path, mainLog.ToString());
                }
                else
                {
                    //Lưu dữ liệu khi không có dòng nào lỗi
                    obs.CommitChanges();
                    //
                    string message = "alert('Import thành công.')";
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                }
            }
        }
        #endregion

    }
}
