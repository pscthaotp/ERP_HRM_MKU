using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Web.Internal;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.DanhMuc.TuyenSinh;
using ERP.Module.Enum.NhanSu;
using ERP.Module.NghiepVu.MaTuDong;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.NonPersistentObjects.HeThong;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
//
namespace ERP.Module.Web.Controllers.ImportClass
{//
    public class Imp_KeHoachTuyenSinh
    {
        #region 1. Import thông tin khách hàng
        public static void ImportHoSoXetTuyen(IObjectSpace obs, OfficeBaseObject_Web typeOffice)
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
            using (DataTable dt = DataProvider.GetDataTableFromExcel(fullPath, "[Sheet1$A2:P]", typeOffice.LoaiOffice))
            {
                /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                #region Khởi tạo các idx
                //Kế hoạch tuyển sinh
                int idx_NamHoc = 0;
                //chi tiết kế hoạch tuyển sinh
                int idx_TuThang = 1;
                int idx_DenThang = 2;
                // chi tiết kế hoạch khối
                int idx_Khoi = 3;
                int idx_SLHocSinh = 4;
                //Đối tượng tuyển sinh
                int idx_DoiTuongTuyenSinh = 5;
                // kế hoạch nhân sự
                int idx_BoPhan = 6;
                int idx_CanBo = 7;
                int idx_MaNhanVien = 8;
                int idx_MoTa = 9;
                #endregion

                /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////
                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();

                    //Duyệt qua tất cả các dòng trong file excel
                    foreach (DataRow dr in dt.Rows)
                    {
                        StringBuilder detailLog = new StringBuilder();

                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////

                        #region Đọc dữ liệu
                        String txt_NamHoc = dr[idx_NamHoc].ToString().FullTrim();
                        String txt_TuThang = dr[idx_TuThang].ToString().FullTrim();
                        String txt_DenThang = dr[idx_DenThang].ToString().FullTrim();
                        String txt_Khoi = dr[idx_Khoi].ToString().FullTrim();
                        String txt_SLHocSinh = dr[idx_SLHocSinh].ToString().FullTrim();
                        String txt_DoiTuongTuyenSinh = dr[idx_DoiTuongTuyenSinh].ToString().FullTrim();
                        String txt_BoPhan = dr[idx_BoPhan].ToString().FullTrim();
                        String txt_CanBo = dr[idx_CanBo].ToString().FullTrim();
                        String txt_MaNhanVien = dr[idx_MaNhanVien].ToString().FullTrim();
                        String txt_MoTa = dr[idx_MoTa].ToString().FullTrim();
                        #endregion

                        //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////

                        #region Kiểm tra dữ
                        //

                        if (!string.IsNullOrEmpty(txt_NamHoc))
                        {
                            #region Năm học
                            // lỗi nè kt lại đi
                            KeHoachTuyenSinh keHoach = new KeHoachTuyenSinh(uow);
                            CriteriaOperator filter1 = CriteriaOperator.Parse("TenNamHoc", txt_NamHoc);
                            NamHoc namHoc = uow.FindObject<NamHoc>(filter1);
                            if (namHoc != null)
                            {
                                keHoach.NamHoc = namHoc;
                            }
                            else
                            {
                                detailLog.AppendLine(" + Không tìm thấy năm học");
                            }
                            //
                            #endregion

                            #region 2. từ tháng đến tháng
                            if (!string.IsNullOrEmpty(txt_TuThang) && !string.IsNullOrEmpty(txt_DenThang))
                            {
                                ChiTietKeHoachTuyenSinh chiTietKeHoach = new ChiTietKeHoachTuyenSinh(uow);
                                chiTietKeHoach.KeHoachTuyenSinh = keHoach;
                                try
                                {
                                    chiTietKeHoach.Thang = Convert.ToDateTime(txt_TuThang);
                                    chiTietKeHoach.DenThang = Convert.ToDateTime(txt_DenThang);
                                }
                                catch (Exception ex)
                                {
                                    detailLog.AppendLine(" + Lỗi convert datetime");
                                }
                                #region 3. Khối
                                if (!string.IsNullOrEmpty(txt_Khoi) && !string.IsNullOrEmpty(txt_SLHocSinh))
                                {
                                    ChiTietKeHoach_Khoi chiTiet_Khoi = new ChiTietKeHoach_Khoi(uow);
                                    chiTiet_Khoi.ChiTietKeHoachTuyenSinh = chiTietKeHoach;
                                    chiTiet_Khoi.KhoiSIS = txt_Khoi;
                                    try
                                    {
                                        chiTiet_Khoi.SoLuong = int.Parse(txt_SLHocSinh);
                                    }
                                    catch (Exception ex)
                                    {

                                        detailLog.AppendLine(" + Lỗi convert int số lượng học sinh");
                                    }
                                }
                                else
                                {
                                    detailLog.AppendLine(" + Không tìm thấy tên khối hoặc số lượng học sinh.");
                                }
                                #endregion

                                #region 4. đối tượng
                                if (!string.IsNullOrEmpty(txt_DoiTuongTuyenSinh))
                                {
                                    CriteriaOperator filter2 = CriteriaOperator.Parse("TenDoiTuong = ?", txt_DoiTuongTuyenSinh);
                                    DoiTuongTuyenSinh doiTuong = uow.FindObject<DoiTuongTuyenSinh>(filter2);
                                    if (doiTuong != null)
                                    {
                                        ChiTietKeHoach_DoiTuong dtuong = new ChiTietKeHoach_DoiTuong(uow);
                                        dtuong.ChiTietKeHoachTuyenSinh = chiTietKeHoach;
                                        dtuong.DoiTuongTuyenSinh = doiTuong;
                                    }
                                }


                                #endregion

                                #region 5. Nhân sự
                                if (!string.IsNullOrEmpty(txt_BoPhan) && !string.IsNullOrEmpty(txt_CanBo))
                                {
                                    CriteriaOperator filter2 = CriteriaOperator.Parse("TenBoPhan = ?", txt_BoPhan);
                                    BoPhan boPhan = uow.FindObject<BoPhan>(filter2);
                                    if (boPhan != null)
                                    {
                                        ChiTietKeHoach_NhanSu nhanSu = new ChiTietKeHoach_NhanSu(uow);
                                        nhanSu.ChiTietKeHoachTuyenSinh = chiTietKeHoach;
                                        nhanSu.BoPhan = boPhan;
                                        CriteriaOperator filter3 = CriteriaOperator.Parse("MaNhanVien = ? and HoTen = ?", txt_MaNhanVien, txt_CanBo);
                                        //private string _MaTapDoan;
                                        //private string _MaNhanVien;
                                        //private string _MaHoSo;
                                        HoSo nhanvien = uow.FindObject<HoSo>(filter3);
                                        if (nhanvien != null)
                                        {
                                            var thongtinnhanvien = nhanvien as ThongTinNhanVien;
                                            nhanSu.ThongTinNhanVien = thongtinnhanvien;
                                        }
                                        nhanSu.MoTaCongViec = txt_MoTa;
                                    }
                                }

                                #endregion
                            }
                            else
                            {
                                detailLog.AppendLine(" + Không tìm thấy từ tháng hoặc đến tháng.");
                            }

                            #endregion

                          
                        }
                        else
                        {
                            detailLog.AppendLine(" + Không tìm thấy họ tên của khách hàng.");
                        }
                        #endregion

                        #region Ghi File log
                        {
                            //Đưa thông tin bị lỗi vào blog
                            if (detailLog.Length > 0)
                            {
                                mainLog.AppendLine(string.Format("- kế hoạch: [{0}] không import vào phần mềm được: ", txt_NamHoc));
                                mainLog.AppendLine(detailLog.ToString());
                                //
                                sucessImport = false;
                            }
                        }
                        #endregion

                        ///////////////////////////NẾU THÀNH CÔNG THÌ SAVE/////////////////////////////////
                        if (!sucessImport)
                        {
                            erorrNumber++;
                            //
                            sucessImport = true;
                        }
                        else
                        {
                            //Lưu dữ liệu khi không có dòng nào lỗi
                            uow.CommitChanges();
                            uow.ReloadChangedObjects();
                        }
                    }
                    // End Duyệt qua tất cả các dòng trong file excel
                }
            }

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
                //
                string message = "alert('Import thành công.')";
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
            }
        }
        #endregion
    }
}
