using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Web.Internal;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.DanhMuc.TuyenSinh;
using ERP.Module.DanhMuc.TuyenSinh_TP;
using ERP.Module.Enum.NhanSu;
using ERP.Module.NghiepVu.MaTuDong;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
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
    public class Imp_ThongTinKhachHang
    {
        #region 1. Import thông tin khách hàng
        public static void ImportThongTinKhachHang(IObjectSpace obs, OfficeBaseObject_Web typeOffice)
        {
            //
            int erorrNumber = 0;
            int updateNumber = 0;
            int sucessNumber = 0;
            bool sucessImport = true;
            bool updateImport = false;
            StringBuilder mainLog = new StringBuilder();
            //
            string fullPath = string.Empty;

            if (typeOffice.File != null)
                fullPath = HttpContext.Current.Server.MapPath("~/Downloads/" + typeOffice.File.FileName);

            //Lưu file lại theo đường dẫn cấu hình
            File.WriteAllBytes(fullPath, typeOffice.File.Content);
            //
            var user = Common.SecuritySystemUser_GetCurrentUser();
            //Import KH Tân phú
            if (user.CongTy.Oid.Equals(Config.KeyTanPhu))
            {
                //[Sheet1$A2:P] Lấy từ sheet1  từ hàng  A3 trở đi và đến cột P
                using (DataTable dt = DataProvider.GetDataTableFromExcel(fullPath, "[Sheet$A2:T]", typeOffice.LoaiOffice))
                {
                    /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                    #region Khởi tạo các idx
                    //
                    int idx_HoTen = 0;
                    int idx_GioiTinh = 1;
                    int idx_NgaySinh = 2;
                    int idx_CMND = 3;
                    int idx_Email = 4;
                    int idx_SoNha = 5;
                    int idx_XaPhuong = 6;
                    int idx_QuanHuyen = 7;
                    int idx_TinhThanh = 8;
                    int idx_QuocGia = 9;
                    int idx_DienThoaiDiDong = 10;
                    int idx_GhiChu = 11;
                    int idx_NguonThuThap = 12;
                    //
                    //int idx_MoiQuanHe = 12;
                    //int idx_HoTenLLK = 12;
                    //int idx_EmailLLK = 12;
                    //int idx_SDTLLK = 12;
                    //int idx_MoiQuanHeLLK= 12;
                    //
                    int idx_HoTre = 13;
                    int idx_TenTre = 14;
                    int idx_NgaySinhTre = 15;
                    int idx_GioiTinhTre = 16;
                    //
                    int idx_TruongDaHoc = 17;
                    //int idx_TinhThanhTruong = 17;
                    //int idx_QuanHuyenTruong = 17;
                    //int idx_DiaChiTruong = 17;
                    ////
                    int idx_LopDaHoc = 18;
                    int idx_DoTuoi = 19;

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
                            String txt_HoTen = dr[idx_HoTen].ToString().FullTrim();
                            String txt_GioiTinh = dr[idx_GioiTinh].ToString().FullTrim();
                            String txt_NgaySinh = dr[idx_NgaySinh].ToString().FullTrim();
                            String txt_CMND = dr[idx_CMND].ToString().FullTrim();
                            String txt_Email = dr[idx_Email].ToString().FullTrim();
                            String txt_SoNha = dr[idx_SoNha].ToString().FullTrim();
                            String txt_XaPhuong = dr[idx_XaPhuong].ToString().FullTrim();
                            String txt_QuanHuyen = dr[idx_QuanHuyen].ToString().FullTrim();
                            String txt_TinhThanh = dr[idx_TinhThanh].ToString().FullTrim();
                            String txt_QuocGia = dr[idx_QuocGia].ToString().FullTrim();
                            String txt_DienThoaiDiDong = dr[idx_DienThoaiDiDong].ToString().FullTrim();
                            String txt_GhiChu = dr[idx_GhiChu].ToString().FullTrim();
                            String txt_NguonThuThap = dr[idx_NguonThuThap].ToString().FullTrim();
                            //
                            //String txt_MoiQuanHe = dr[idx_MoiQuanHe].ToString().FullTrim();
                            //String txt_HoTenLLK = dr[idx_HoTenLLK].ToString().FullTrim();
                            //String txt_EmailLLK = dr[idx_EmailLLK].ToString().FullTrim();
                            //String txt_SDTLLK = dr[idx_SDTLLK].ToString().FullTrim();
                            //String txt_MoiQuanHeLLK = dr[idx_MoiQuanHeLLK].ToString().FullTrim();
                            //
                            String txt_HoTre = dr[idx_HoTre].ToString().FullTrim();
                            String txt_TenTre = dr[idx_TenTre].ToString().FullTrim();
                            String txt_NgaySinhTre = dr[idx_NgaySinhTre].ToString().FullTrim();
                            String txt_GioiTinhTre = dr[idx_GioiTinhTre].ToString().FullTrim();
                            //
                            String txt_TruongDaHoc = dr[idx_TruongDaHoc].ToString().FullTrim();
                            //String txt_TinhThanhTruong = dr[idx_TinhThanhTruong].ToString().FullTrim();
                            //String txt_QuanHuyenTruong = dr[idx_QuanHuyenTruong].ToString().FullTrim();
                            //String txt_DiaChiTruong = dr[idx_DiaChiTruong].ToString().FullTrim();
                            //
                            String txt_LopDaHoc = dr[idx_LopDaHoc].ToString().FullTrim();
                            String txt_DoTuoi = dr[idx_DoTuoi].ToString().FullTrim();
                            #endregion

                            //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////

                            #region Kiểm tra dữ
                            //

                            if (!string.IsNullOrEmpty(txt_HoTen))
                            {
                                #region Thông tin khác hàng

                                CriteriaOperator filter = CriteriaOperator.Parse("HoTen = ? and isnull(CMND,'') = ? and isnull(DienThoaiDiDong,'') = ? and isnull(Email,'') = ?", txt_HoTen, txt_CMND, txt_DienThoaiDiDong, txt_Email);
                                ThongTinKhachHang thongTinKhachHang = uow.FindObject<ThongTinKhachHang>(filter);
                                if (thongTinKhachHang == null)
                                {
                                    thongTinKhachHang = new ThongTinKhachHang(uow);
                                    thongTinKhachHang.HoTen = txt_HoTen;

                                    #region 2. Ngày sinh
                                    if (!string.IsNullOrEmpty(txt_NgaySinh))
                                    {
                                        try
                                        {
                                            thongTinKhachHang.NgaySinh = Convert.ToDateTime(txt_NgaySinh);
                                        }
                                        catch (Exception ex)
                                        {
                                            thongTinKhachHang.NgaySinh = Convert.ToDateTime("01/01/" + txt_NgaySinh);
                                        }
                                    }
                                    else
                                    {
                                        //detailLog.AppendLine(" + Ngày sinh không tìm thấy.");
                                    }

                                    #endregion

                                    #region 3. Giới tính
                                    if (!string.IsNullOrEmpty(txt_GioiTinh))
                                    {
                                        if (txt_GioiTinh.Contains("Nam"))
                                            thongTinKhachHang.GioiTinh = Enum.NhanSu.GioiTinhEnum.Nam;
                                        else
                                            thongTinKhachHang.GioiTinh = Enum.NhanSu.GioiTinhEnum.Nu;
                                    }
                                    else
                                    {
                                        //detailLog.AppendLine(" + Giới tính không tìm thấy.");
                                    }
                                    #endregion

                                    #region 4. CMND
                                    if (!string.IsNullOrEmpty(txt_CMND))
                                    {
                                        thongTinKhachHang.CMND = txt_CMND;
                                    }
                                    #endregion

                                    #region 5. Email
                                    if (!string.IsNullOrEmpty(txt_Email))
                                    {
                                        thongTinKhachHang.Email = txt_Email;
                                    }
                                    #endregion

                                    #region 6. Điện thoại
                                    if (!string.IsNullOrEmpty(txt_DienThoaiDiDong))
                                    {
                                        thongTinKhachHang.DienThoaiDiDong = txt_DienThoaiDiDong;
                                    }
                                    #endregion

                                    #region 7. Ghi chú
                                    if (!string.IsNullOrEmpty(txt_GhiChu))
                                    {
                                        thongTinKhachHang.GhiChu = txt_GhiChu;
                                    }
                                    #endregion

                                    #region 8. Nguồn thu nhập
                                    if (!string.IsNullOrEmpty(txt_NguonThuThap))
                                    {
                                        //
                                        CriteriaOperator filter2 = CriteriaOperator.Parse("TenNguonThuThap = ?", txt_NguonThuThap);
                                        NguonThuThap nguonThuThap = uow.FindObject<NguonThuThap>(filter2);
                                        if (nguonThuThap == null)
                                        {
                                            nguonThuThap = new NguonThuThap(uow);
                                            nguonThuThap.TenNguonThuThap = txt_NguonThuThap;
                                            nguonThuThap.MaQuanLy = txt_NguonThuThap;
                                        }
                                        thongTinKhachHang.NguonThuThap = nguonThuThap;
                                    }
                                    #endregion


                                    #region 9.Địa chỉ

                                    if (!string.IsNullOrEmpty(txt_QuocGia) && txt_QuocGia.Contains("Việt Nam"))
                                    {
                                        DiaChi diaChi = new DiaChi(uow);
                                        if (!string.IsNullOrEmpty(txt_TinhThanh))
                                        {
                                            TinhThanh tT = uow.FindObject<TinhThanh>(CriteriaOperator.Parse("TenTinhThanh like ? ", "%" + txt_TinhThanh + "%"));
                                            if (tT != null)
                                            {
                                                diaChi.TinhThanh = tT;

                                            }
                                            if (!string.IsNullOrEmpty(txt_QuanHuyen))
                                            {
                                                QuanHuyen qh = uow.FindObject<QuanHuyen>(CriteriaOperator.Parse("TinhThanh = ? and TenQuanHuyen like ? ", tT, "%" + txt_QuanHuyen + "%"));
                                                if (qh != null)
                                                {
                                                    diaChi.QuanHuyen = qh;
                                                }
                                                if (!string.IsNullOrEmpty(txt_XaPhuong))
                                                {
                                                    XaPhuong xp = uow.FindObject<XaPhuong>(CriteriaOperator.Parse("QuanHuyen = ? and TenXaPhuong like ? ", qh, "%" + txt_XaPhuong + "%"));
                                                    if (xp != null)
                                                    {
                                                        diaChi.XaPhuong = xp;
                                                    }
                                                }
                                            }
                                        }
                                        if (diaChi.XaPhuong == null)
                                            txt_SoNha += txt_XaPhuong.Length > 0 ? ", " + txt_XaPhuong : "";
                                        if (diaChi.QuanHuyen == null)
                                            txt_SoNha += txt_QuanHuyen.Length > 0 ? ", " + txt_QuanHuyen : "";
                                        if (diaChi.TinhThanh == null)
                                            txt_SoNha += txt_TinhThanh.Length > 0 ? ", " + txt_TinhThanh : "";
                                        diaChi.SoNha = txt_SoNha;
                                        thongTinKhachHang.DiaChiThuongTru = diaChi;
                                    }
                                    #endregion

                                    #region 10. Mối quan hệ
                                    //if (!string.IsNullOrEmpty(txt_MoiQuanHe))
                                    //{
                                    //    //
                                    //    CriteriaOperator filter2 = CriteriaOperator.Parse("TenQuanHe like ?", txt_MoiQuanHe);
                                    //    QuanHe qh = uow.FindObject<QuanHe>(filter2);
                                    //    if (qh != null)
                                    //        thongTinKhachHang.QuanHe = qh;
                                    //}
                                    #endregion

                                    #region 11.Liên lạc khác

                                    //if (!string.IsNullOrEmpty(txt_HoTenLLK))
                                    //{
                                    //    thongTinKhachHang.HoTenLLK = txt_HoTenLLK;
                                    //    if(!string.IsNullOrEmpty(txt_EmailLLK))
                                    //        thongTinKhachHang.EmailLLK = txt_EmailLLK;
                                    //    if (!string.IsNullOrEmpty(txt_SDTLLK))
                                    //        thongTinKhachHang.SDTLLK= txt_SDTLLK;
                                    //    if (!string.IsNullOrEmpty(txt_MoiQuanHeLLK))
                                    //    {
                                    //        QuanHe qh = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", txt_MoiQuanHeLLK));
                                    //        if (qh != null)
                                    //            thongTinKhachHang.QuanHeLLK = qh;
                                    //    }
                                    //}
                                    #endregion
                                }
                                else
                                {
                                    //detailLog.AppendLine(" + Ngày sinh không tìm thấy.");
                                }

                                #region 10. Danh sách trẻ (Nếu có)
                                if (!string.IsNullOrEmpty(txt_HoTre) || !string.IsNullOrEmpty(txt_HoTre))
                                {
                                    filter = CriteriaOperator.Parse("(HoTen = ? or (Ho = ? and Ten = ? )) and ThongTinKhachHang = ?", txt_HoTre + " " + txt_TenTre, txt_HoTre, txt_TenTre, thongTinKhachHang.Oid);
                                    DanhSachTre danhSachTre = uow.FindObject<DanhSachTre>(filter);
                                    if (danhSachTre == null)
                                    {
                                        danhSachTre = new DanhSachTre(uow);
                                        danhSachTre.Ho = txt_HoTre;
                                        danhSachTre.Ten = txt_TenTre;
                                    }

                                    #region 11. Ngày sinh trẻ
                                    if (!string.IsNullOrEmpty(txt_NgaySinhTre))
                                    {
                                        try
                                        {
                                            danhSachTre.NgaySinh = Convert.ToDateTime(txt_NgaySinhTre);
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + Ngày sinh trẻ  " + txt_HoTen + " " + txt_TenTre + "  không hợp lệ: " + txt_NgaySinhTre);
                                        }
                                    }
                                    #endregion

                                    #region 12. Giới tính trẻ
                                    if (!string.IsNullOrEmpty(txt_GioiTinhTre))
                                    {
                                        if (txt_GioiTinhTre.Contains("Nam"))
                                            danhSachTre.GioiTinh = Enum.NhanSu.GioiTinhEnum.Nam;
                                        else
                                            danhSachTre.GioiTinh = Enum.NhanSu.GioiTinhEnum.Nu;
                                    }
                                    else
                                    {
                                        //detailLog.AppendLine(" + Giới tính không tìm thấy.");
                                    }
                                    #endregion

                                    #region 13. Trường đã học
                                    //if (!string.IsNullOrEmpty(txt_TruongDaHoc))
                                    //{
                                    //    DanhMucTruong truongDaHoc = uow.FindObject<DanhMucTruong>(CriteriaOperator.Parse("TenTruong like ? ", "%" + txt_TruongDaHoc + "%"));
                                    //    if (truongDaHoc != null)
                                    //    {
                                    //        danhSachTre.DanhMucTruong = truongDaHoc;
                                    //    }
                                    //    else
                                    //    {
                                    //        detailLog.AppendLine(" + Không tìm thấy trường đã học trong danh mục.");
                                    //    }
                                    //}
                                    #endregion

                                    #region 14. Lớp đã học
                                    if (!string.IsNullOrEmpty(txt_LopDaHoc))
                                    {
                                        danhSachTre.LopDaHoc = txt_LopDaHoc;
                                    }
                                    #endregion

                                    #region 15. Nhóm độ tuổi
                                    if (!string.IsNullOrEmpty(txt_DoTuoi))
                                    {
                                        danhSachTre.NhomTuoi = txt_DoTuoi;
                                    }
                                    #endregion
                                    //
                                    danhSachTre.ThongTinKhachHang = thongTinKhachHang;
                                }
                                #endregion

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
                                    mainLog.AppendLine(string.Format("- Khách hàng Tên: [{0}] không import vào phần mềm được: ", txt_HoTen));
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
                            //if (erorrNumber < 1)
                            {
                                //Lưu dữ liệu khi không có dòng nào lỗi
                                uow.CommitChanges();
                                uow.ReloadChangedObjects();
                            }
                        }
                        // End Duyệt qua tất cả các dòng trong file excel
                    }
                }
            }
            //Import KH ABI
            //else if (user.CongTy.Equals(Config.KeyABI))
            else
            {
                using (DataTable dt = DataProvider.GetDataTableFromExcel(fullPath, "[Sheet$A1:Y]", typeOffice.LoaiOffice))
                {
                    /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////
                    #region Khởi tạo các idx
                    //
                    int idx_STT = 0;
                    int idx_HoTen = 1;
                    int idx_GioiTinh = 2;
                    int idx_NgaySinh = 3;
                    int idx_CMND = 4;
                    int idx_Email = 5;
                    int idx_DienThoaiDiDong = 6;
                    int idx_QuocTich = 7;
                    int idx_QuocGia = 8;
                    int idx_TinhThanh = 9;
                    int idx_QuanHuyen = 10;
                    int idx_XaPhuong = 11;
                    int idx_SoNha = 12;
                    int idx_GhiChu = 13;
                    int idx_NguonThuThap = 14;
                    int idx_MoiQuanHe = 15;
                    //
                    int idx_NoiCongTac = 16;
                    int idx_NgheNghiep = 17;
                    //
                    int idx_HoTre = 18;
                    int idx_TenTre = 19;
                    int idx_NgaySinhTre = 20;
                    int idx_GioiTinhTre = 21;
                    int idx_NhomTuoi = 22;
                    int idx_TruongDaHoc = 23;
                    int idx_LopDaHoc = 24;
                    //
                    //int idx_HoTenLLK = 14;
                    //int idx_EmailLLK = 15;
                    //int idx_SDTLLK = 16;
                    //int idx_MoiQuanHeLLK = 17;

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
                            String txt_STT = dr[idx_STT].ToString().FullTrim();
                            String txt_HoTen = dr[idx_HoTen].ToString().FullTrim();
                            String txt_GioiTinh = dr[idx_GioiTinh].ToString().FullTrim();
                            String txt_NgaySinh = dr[idx_NgaySinh].ToString().FullTrim();
                            String txt_CMND = dr[idx_CMND].ToString().FullTrim();
                            String txt_Email = dr[idx_Email].ToString().FullTrim();
                            String txt_DienThoaiDiDong = dr[idx_DienThoaiDiDong].ToString().FullTrim();
                            String txt_QuocTich = dr[idx_QuocTich].ToString().FullTrim();

                            String txt_SoNha = dr[idx_SoNha].ToString().FullTrim();
                            String txt_XaPhuong = dr[idx_XaPhuong].ToString().FullTrim();
                            String txt_QuanHuyen = dr[idx_QuanHuyen].ToString().FullTrim();
                            String txt_TinhThanh = dr[idx_TinhThanh].ToString().FullTrim();
                            String txt_QuocGia = dr[idx_QuocGia].ToString().FullTrim();
                            String txt_GhiChu = dr[idx_GhiChu].ToString().FullTrim();
                            String txt_NguonThuThap = dr[idx_NguonThuThap].ToString().FullTrim();
                            String txt_QuanHe = dr[idx_MoiQuanHe].ToString().FullTrim();
                            String txt_NoiCongTac = dr[idx_NoiCongTac].ToString().FullTrim();
                            String txt_NgheNghiep = dr[idx_NgheNghiep].ToString().FullTrim();
                            //
                            //String txt_HoTenLLK = dr[idx_HoTenLLK].ToString().FullTrim();
                            //String txt_DienThoaiLLK = dr[idx_SDTLLK].ToString().FullTrim();
                            //String txt_EmailLLK = dr[idx_EmailLLK].ToString().FullTrim();
                            //String txt_QuanHeLLK = dr[idx_MoiQuanHeLLK].ToString().FullTrim();


                            String txt_HoTre = dr[idx_HoTre].ToString().FullTrim();
                            String txt_TenTre = dr[idx_TenTre].ToString().FullTrim();
                            String txt_NgaySinhTre = dr[idx_NgaySinhTre].ToString().FullTrim();
                            String txt_GioiTinhTre = dr[idx_GioiTinhTre].ToString().FullTrim();
                            String txt_NhomTuoi = dr[idx_NhomTuoi].ToString().FullTrim();
                            String txt_TruongDaHoc = dr[idx_TruongDaHoc].ToString().FullTrim();
                            String txt_LopDaHoc = dr[idx_LopDaHoc].ToString().FullTrim();

                            #endregion

                            //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////

                            #region Kiểm tra dữ
                            //

                            if (!string.IsNullOrEmpty(txt_HoTen) && !string.IsNullOrEmpty(txt_DienThoaiDiDong))
                            {
                                #region Thông tin khác hàng

                                CriteriaOperator filter = CriteriaOperator.Parse("isnull(DienThoaiDiDong,'') = ?", txt_DienThoaiDiDong, txt_DienThoaiDiDong);
                                ThongTinKhachHang thongTinKhachHang = uow.FindObject<ThongTinKhachHang>(filter);
                                if (thongTinKhachHang == null)
                                {
                                    thongTinKhachHang = new ThongTinKhachHang(uow);
                                    thongTinKhachHang.HoTen = txt_HoTen;

                                    #region 2. Ngày sinh
                                    if (!string.IsNullOrEmpty(txt_NgaySinh))
                                    {
                                        try
                                        {
                                            thongTinKhachHang.NgaySinh = Convert.ToDateTime(txt_NgaySinh);
                                        }
                                        catch (Exception ex)
                                        {
                                            txt_NgaySinh = "01/01/" + txt_NgaySinh;
                                            try
                                            {
                                                thongTinKhachHang.NgaySinh = Convert.ToDateTime(txt_NgaySinh);
                                            }
                                            catch (Exception ex2)
                                            {
                                                detailLog.AppendLine(" + Ngày sinh không hợp lệ: " + txt_NgaySinh);
                                                thongTinKhachHang.Delete();
                                            }


                                        }
                                    }
                                    
                                    #endregion

                                    #region 3. Giới tính
                                    if (!string.IsNullOrEmpty(txt_GioiTinh))
                                    {
                                        if (txt_GioiTinh.Contains("Nam"))
                                            thongTinKhachHang.GioiTinh = Enum.NhanSu.GioiTinhEnum.Nam;
                                        else
                                            thongTinKhachHang.GioiTinh = Enum.NhanSu.GioiTinhEnum.Nu;
                                    }
                                  
                                    #endregion

                                    #region 4. CMND
                                    if (!string.IsNullOrEmpty(txt_CMND))
                                    {
                                        thongTinKhachHang.CMND = txt_CMND;
                                    }
                                    #endregion

                                    #region 5. Email
                                    if (!string.IsNullOrEmpty(txt_Email))
                                    {
                                        thongTinKhachHang.Email = txt_Email;
                                    }
                                    #endregion

                                    #region 6. Điện thoại
                                    if (!string.IsNullOrEmpty(txt_DienThoaiDiDong))
                                    {
                                        thongTinKhachHang.DienThoaiDiDong = txt_DienThoaiDiDong;
                                    }
                                    else
                                        detailLog.AppendLine(" + Thông tin khách hàng không có dữ liệu về số điện thoại");
                                    #endregion

                                    #region 7.Địa chỉ

                                    if (!string.IsNullOrEmpty(txt_QuocGia) && txt_QuocGia.Contains("Việt Nam"))
                                    {
                                        DiaChi diaChi = new DiaChi(uow);
                                        if (!string.IsNullOrEmpty(txt_TinhThanh))
                                        {
                                            TinhThanh tT = uow.FindObject<TinhThanh>(CriteriaOperator.Parse("TenTinhThanh like ? ", txt_TinhThanh));
                                            if (tT != null)
                                            {
                                                diaChi.TinhThanh = tT;

                                            }
                                            if (!string.IsNullOrEmpty(txt_QuanHuyen) && tT != null)
                                            {
                                                QuanHuyen qh = uow.FindObject<QuanHuyen>(CriteriaOperator.Parse("TinhThanh = ? and TenQuanHuyen like ? ", tT.Oid, "%" + txt_QuanHuyen + "%"));
                                                if (qh != null)
                                                {
                                                    diaChi.QuanHuyen = qh;
                                                }
                                                if (!string.IsNullOrEmpty(txt_XaPhuong) && qh != null)
                                                {
                                                    XaPhuong xp = uow.FindObject<XaPhuong>(CriteriaOperator.Parse("QuanHuyen = ? and TenXaPhuong like ? ", qh.Oid, "%" + txt_XaPhuong + "%"));
                                                    if (xp != null)
                                                    {
                                                        diaChi.XaPhuong = xp;
                                                    }
                                                }
                                            }
                                        }
                                        if (diaChi.XaPhuong == null)
                                            txt_SoNha += txt_XaPhuong.Length > 0 ? ", " + txt_XaPhuong : "";
                                        if (diaChi.QuanHuyen == null)
                                            txt_SoNha += txt_QuanHuyen.Length > 0 ? ", " + txt_QuanHuyen : "";
                                        if (diaChi.TinhThanh == null)
                                            txt_SoNha += txt_TinhThanh.Length > 0 ? ", " + txt_TinhThanh : "";
                                        diaChi.SoNha = txt_SoNha;
                                        thongTinKhachHang.DiaChiThuongTru = diaChi;
                                    }

                                    #endregion

                                    #region 8. Ghi chú
                                    if (!string.IsNullOrEmpty(txt_GhiChu))
                                    {
                                        thongTinKhachHang.GhiChu = txt_GhiChu;
                                    }
                                    #endregion

                                    #region 9. Nguồn thu nhập
                                    if (!string.IsNullOrEmpty(txt_NguonThuThap))
                                    {
                                        //
                                        filter = CriteriaOperator.Parse("TenNguonThuThap = ?", txt_NguonThuThap);
                                        NguonThuThap nguonThuThap = uow.FindObject<NguonThuThap>(filter);
                                        if (nguonThuThap != null)
                                        {
                                            thongTinKhachHang.NguonThuThap = nguonThuThap;
                                        }

                                    }
                                    #endregion

                                    #region 10. Quan hệ
                                    if (!string.IsNullOrEmpty(txt_QuanHe))
                                    {
                                        //
                                        filter = CriteriaOperator.Parse("TenQuanHe = ?", txt_QuanHe);
                                        QuanHe quanHe = uow.FindObject<QuanHe>(filter);
                                        if (quanHe != null)
                                        {
                                            thongTinKhachHang.QuanHe = quanHe;
                                        }
                                    }
                                    #endregion

                                    #region 11. Nơi công tác vs nghề nghiệp
                                    if (!string.IsNullOrEmpty(txt_NoiCongTac))
                                    {
                                        thongTinKhachHang.NoiCongTac = txt_NoiCongTac;
                                    }
                                    if (!string.IsNullOrEmpty(txt_NgheNghiep))
                                    {
                                        thongTinKhachHang.NgheNghiep = txt_NgheNghiep;
                                    }
                                    #endregion
                                    #region 12. Quốc tịch
                                    if (!string.IsNullOrEmpty(txt_QuocTich))
                                    {
                                        //
                                        filter = CriteriaOperator.Parse("TenQuocGia = ?", txt_QuocTich);
                                        QuocGia quocTich = uow.FindObject<QuocGia>(filter);
                                        if (quocTich != null)
                                        {
                                            thongTinKhachHang.QuocTich = quocTich;
                                        }

                                    }
                                    #endregion

                                    sucessNumber++;

                                }
                                //
                                else if (thongTinKhachHang != null)
                                {
                                    thongTinKhachHang.HoTen = txt_HoTen;

                                    #region 2. Ngày sinh
                                    if (!string.IsNullOrEmpty(txt_NgaySinh))
                                    {
                                        try
                                        {
                                            thongTinKhachHang.NgaySinh = Convert.ToDateTime(txt_NgaySinh);
                                        }
                                        catch (Exception ex)
                                        {
                                            txt_NgaySinh = "01/01/" + txt_NgaySinh;
                                            try
                                            {
                                                thongTinKhachHang.NgaySinh = Convert.ToDateTime(txt_NgaySinh);
                                            }
                                            catch (Exception ex2)
                                            {
                                                detailLog.AppendLine(" + Ngày sinh không hợp lệ: " + txt_NgaySinh);
                                                thongTinKhachHang.Delete();
                                            }


                                        }
                                    }
                                    else
                                    {
                                        //detailLog.AppendLine(" + Ngày sinh không tìm thấy.");
                                    }

                                    #endregion

                                    #region 3. Giới tính
                                    if (!string.IsNullOrEmpty(txt_GioiTinh))
                                    {
                                        if (txt_GioiTinh.Contains("Nam"))
                                            thongTinKhachHang.GioiTinh = Enum.NhanSu.GioiTinhEnum.Nam;
                                        else
                                            thongTinKhachHang.GioiTinh = Enum.NhanSu.GioiTinhEnum.Nu;
                                    }
                                    else
                                    {
                                        //detailLog.AppendLine(" + Giới tính không tìm thấy.");
                                    }
                                    #endregion

                                    #region 4. CMND
                                    if (!string.IsNullOrEmpty(txt_CMND))
                                    {
                                        thongTinKhachHang.CMND = txt_CMND;
                                    }
                                    #endregion

                                    #region 5. Email
                                    if (!string.IsNullOrEmpty(txt_Email))
                                    {
                                        thongTinKhachHang.Email = txt_Email;
                                    }
                                    #endregion

                                    #region 6. Điện thoại
                                    if (!string.IsNullOrEmpty(txt_DienThoaiDiDong))
                                    {
                                        thongTinKhachHang.DienThoaiDiDong = txt_DienThoaiDiDong;
                                    }
                                    else
                                        detailLog.AppendLine(" + Thông tin khách hàng không có dữ liệu về số điện thoại");
                                    #endregion

                                    #region 7.Địa chỉ

                                    if (!string.IsNullOrEmpty(txt_QuocGia) && txt_QuocGia.Contains("Việt Nam"))
                                    {
                                        DiaChi diaChi = new DiaChi(uow);
                                        if (!string.IsNullOrEmpty(txt_TinhThanh))
                                        {
                                            TinhThanh tT = uow.FindObject<TinhThanh>(CriteriaOperator.Parse("TenTinhThanh like ? ", txt_TinhThanh));
                                            if (tT != null)
                                            {
                                                diaChi.TinhThanh = tT;

                                            }
                                            if (!string.IsNullOrEmpty(txt_QuanHuyen) && tT != null)
                                            {
                                                QuanHuyen qh = uow.FindObject<QuanHuyen>(CriteriaOperator.Parse("TinhThanh = ? and TenQuanHuyen like ? ", tT.Oid, "%" + txt_QuanHuyen + "%"));
                                                if (qh != null)
                                                {
                                                    diaChi.QuanHuyen = qh;
                                                }
                                                if (!string.IsNullOrEmpty(txt_XaPhuong) && qh != null)
                                                {
                                                    XaPhuong xp = uow.FindObject<XaPhuong>(CriteriaOperator.Parse("QuanHuyen = ? and TenXaPhuong like ? ", qh.Oid, "%" + txt_XaPhuong + "%"));
                                                    if (xp != null)
                                                    {
                                                        diaChi.XaPhuong = xp;
                                                    }
                                                }
                                            }
                                        }
                                        if (diaChi.XaPhuong == null)
                                            txt_SoNha += txt_XaPhuong.Length > 0 ? ", " + txt_XaPhuong : "";
                                        if (diaChi.QuanHuyen == null)
                                            txt_SoNha += txt_QuanHuyen.Length > 0 ? ", " + txt_QuanHuyen : "";
                                        if (diaChi.TinhThanh == null)
                                            txt_SoNha += txt_TinhThanh.Length > 0 ? ", " + txt_TinhThanh : "";
                                        diaChi.SoNha = txt_SoNha;
                                        thongTinKhachHang.DiaChiThuongTru = diaChi;
                                    }

                                    #endregion

                                    #region 8. Ghi chú
                                    if (!string.IsNullOrEmpty(txt_GhiChu))
                                    {
                                        thongTinKhachHang.GhiChu = txt_GhiChu;
                                    }
                                    #endregion

                                    #region 9. Nguồn thu nhập
                                    if (!string.IsNullOrEmpty(txt_NguonThuThap))
                                    {
                                        //
                                        filter = CriteriaOperator.Parse("TenNguonThuThap = ?", txt_NguonThuThap);
                                        NguonThuThap nguonThuThap = uow.FindObject<NguonThuThap>(filter);
                                        if (nguonThuThap != null)
                                        {
                                            thongTinKhachHang.NguonThuThap = nguonThuThap;
                                        }
                                       
                                    }
                                    #endregion

                                    #region 10. Quan hệ
                                    if (!string.IsNullOrEmpty(txt_QuanHe))
                                    {
                                        //
                                        filter = CriteriaOperator.Parse("TenQuanHe = ?", txt_QuanHe);
                                        QuanHe quanHe = uow.FindObject<QuanHe>(filter);
                                        if (quanHe != null)
                                        {
                                            thongTinKhachHang.QuanHe = quanHe;
                                        }
                                    }
                                    #endregion

                                    #region 11. Nơi công tác vs nghề nghiệp
                                    if (!string.IsNullOrEmpty(txt_NoiCongTac))
                                    {
                                        thongTinKhachHang.NoiCongTac = txt_NoiCongTac;
                                    }
                                    if (!string.IsNullOrEmpty(txt_NgheNghiep))
                                    {
                                        thongTinKhachHang.NgheNghiep = txt_NgheNghiep;
                                    }
                                    #endregion

                                    updateNumber++;
                                    updateImport = true;
                                    detailLog.AppendLine(string.Format("- Khách hàng Tên: [{0}] cập nhật thông tin vào phần mềm, số thứ tự là [{1}]", txt_HoTen, txt_STT));
                                }
                                #endregion

                                #region 10. Danh sách trẻ (Nếu có)
                                if (!string.IsNullOrEmpty(txt_HoTre) || !string.IsNullOrEmpty(txt_TenTre))
                                {
                                    filter = CriteriaOperator.Parse("(HoTen = ? or (Ho = ? and Ten = ? )) and ThongTinKhachHang = ?", txt_HoTre + " " + txt_TenTre, txt_HoTre, txt_TenTre, thongTinKhachHang.Oid);
                                    DanhSachTre danhSachTre = uow.FindObject<DanhSachTre>(filter);
                                    if (danhSachTre == null)
                                    {
                                        danhSachTre = new DanhSachTre(uow);
                                        danhSachTre.Ho = txt_HoTre;
                                        danhSachTre.Ten = txt_TenTre;

                                        if (!string.IsNullOrEmpty(txt_NgaySinhTre))
                                        {
                                            try
                                            {
                                                danhSachTre.NgaySinh = Convert.ToDateTime(txt_NgaySinhTre);
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Ngày sinh trẻ không hợp lệ: " + txt_NgaySinhTre);
                                                danhSachTre.Delete();
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(txt_GioiTinhTre))
                                        {
                                            if (txt_GioiTinhTre.Contains("Nam"))
                                                danhSachTre.GioiTinh = Enum.NhanSu.GioiTinhEnum.Nam;
                                            else
                                                danhSachTre.GioiTinh = Enum.NhanSu.GioiTinhEnum.Nu;
                                        }

                                        if (!string.IsNullOrEmpty(txt_TruongDaHoc))
                                        {
                                            danhSachTre.TruongDaHoc = txt_TruongDaHoc;
                                        }

                                        if (!string.IsNullOrEmpty(txt_LopDaHoc))
                                        {
                                            danhSachTre.LopDaHoc = txt_LopDaHoc;
                                        }

                                        if (!string.IsNullOrEmpty(txt_NhomTuoi))
                                        {
                                            danhSachTre.NhomTuoi = txt_NhomTuoi;
                                        }
                                        //
                                        danhSachTre.ThongTinKhachHang = thongTinKhachHang;
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(txt_NgaySinhTre))
                                        {
                                            try
                                            {
                                                danhSachTre.NgaySinh = Convert.ToDateTime(txt_NgaySinhTre);
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Ngày sinh trẻ không hợp lệ: " + txt_NgaySinhTre);
                                                danhSachTre.Delete();
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(txt_GioiTinhTre))
                                        {
                                            if (txt_GioiTinhTre.Contains("Nam"))
                                                danhSachTre.GioiTinh = Enum.NhanSu.GioiTinhEnum.Nam;
                                            else
                                                danhSachTre.GioiTinh = Enum.NhanSu.GioiTinhEnum.Nu;
                                        }

                                        if (!string.IsNullOrEmpty(txt_TruongDaHoc))
                                        {
                                            danhSachTre.TruongDaHoc = txt_TruongDaHoc;
                                        }

                                        if (!string.IsNullOrEmpty(txt_LopDaHoc))
                                        {
                                            danhSachTre.LopDaHoc = txt_LopDaHoc;
                                        }

                                        if (!string.IsNullOrEmpty(txt_NhomTuoi))
                                        {
                                            danhSachTre.NhomTuoi = txt_NhomTuoi;
                                        }
                                    }
                                }
                                #endregion

                               
                            }
                            else
                            {
                                detailLog.AppendLine(string.Format(" + Số thứ tự [{0}] : Không tìm thấy họ tên và số điện thoại của khách hàng.", txt_STT));
                                sucessImport = false;

                            }
                            #endregion

                            #region Ghi File log
                            {
                                //Đưa thông tin bị lỗi vào blog
                                if (detailLog.Length > 0 && !updateImport)
                                {
                                    mainLog.AppendLine(string.Format("- Khách hàng Tên: [{0}] không import vào phần mềm, số thứ tự là [{1}]", txt_HoTen,txt_STT));
                                    mainLog.AppendLine(detailLog.ToString());
                                    //
                                    sucessImport = false;
                                }
                                else if (detailLog.Length > 0 && updateImport)
                                {
                                    mainLog.AppendLine(detailLog.ToString());
                                    updateImport = false;
                                }
                                else if (sucessImport)
                                {
                                    detailLog.AppendLine(string.Format("- Khách hàng Tên: [{0}] import thành công vào phần mềm, số thứ tự là [{1}]", txt_HoTen, txt_STT));
                                    mainLog.AppendLine(detailLog.ToString());
                                }
                            }
                            #endregion

                            ///////////////////////////NẾU THÀNH CÔNG THÌ SAVE/////////////////////////////////
                            if (!sucessImport && !updateImport)
                            {
                                erorrNumber++;
                                //
                                sucessImport = true;
                            }
                            else
                            {
                                //Lưu dữ liệu khi không có dòng nào lỗi
                                try
                                {
                                    //uow.CommitTransaction();
                                    uow.CommitChanges();
                                    uow.ReloadChangedObjects();
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                        }
                        // End Duyệt qua tất cả các dòng trong file excel
                    }
                }
            }
            //Xuất kết quả
            if (erorrNumber > 0)
            {
                //
                string message = "alert('Import thành công: " + sucessNumber + " dòng - Cập nhật thành công: " + updateNumber+ " dòng - Import không thành công: " + erorrNumber + " dòng.')";
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
                string message = "alert('Import thành công " + sucessNumber + " - Cập nhật thành công " + updateNumber + "')";
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                string path = HttpContext.Current.Server.MapPath(Config.URLErorrImportExcel);

                Common.WriteDataToFile(path, mainLog.ToString());
             
            }
        }
        #endregion
    }
}
