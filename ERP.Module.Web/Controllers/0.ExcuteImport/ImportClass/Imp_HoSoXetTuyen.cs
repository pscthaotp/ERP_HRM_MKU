using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Web.Internal;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.DanhMuc.TuyenSinh;
using ERP.Module.DanhMuc.TuyenSinh_TP;
using ERP.Module.Enum.NhanSu;
using ERP.Module.Enum.TuyenSinh_PT;
using ERP.Module.HeThong;
using ERP.Module.NghiepVu.MaTuDong;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.NghiepVu.TuyenSinh_TP;
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
    public class Imp_HoSoXetTuyen
    {
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
            ////Loại file
            //           LoaiOfficeEnum loaiOffice = LoaiOfficeEnum.Office2003;
            //           if (open.SafeFileName.Contains(".xlsx"))
            //           { loaiOffice = LoaiOfficeEnum.Office2010; }
            using (DataTable dt = DataProvider.GetDataTableFromExcel(fullPath, "[Sheet$A2:DJ]", typeOffice.LoaiOffice))
            {
                /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                #region Khởi tạo các idx
                //
                int idx_STT = 0;
                int idx_MaHocSinh = 1;
                int idx_Lop = 2;
                int idx_HoHS = 3;
                int idx_TenHS = 4;

                int idx_NgaySinhHS = 5;
                int idx_NoiSinh = 6;
                int idx_DanToc = 7;
                int idx_TonGiao = 8;
                int idx_GioiTinhHS = 9;
                int idx_Khoi = 10;

                //Loại học sinh mới
                int idx_LoaiHocSinh = 11;
                int idx_CheDoHoc = 12;
                //Phòng nội trú bán trú
                int idx_PhongNTBT = 13;

                //Địa chỉ thường trú
                int idx_SoNha = 14;
                int idx_XaPhuong = 15;
                int idx_QuanHuyen = 16;
                int idx_TinhThanh = 17;
                //Địa chỉ liên lạc
                int idx_SoNhaLL = 18;
                int idx_XaPhuongLL = 19;
                int idx_QuanHuyenLL = 20;
                int idx_TinhThanhLL = 21;

                //Thông tin cha
                int idx_HoTenCha = 22;
                int idx_NgheNghiepCha = 23;
                int idx_DienThoaiCha = 24;
                int idx_EmailCha = 25;
                //Thông tin mẹ
                int idx_HoTenMe = 26;
                int idx_NgheNghiepMe = 27;
                int idx_DienThoaiMe = 28;
                int idx_EmailMe = 29;
                //Thông tin người đại diện
                int idx_HoTenNDD = 30;
                int idx_NamSinhNDD = 31;
                int idx_NgheNghiepNDD = 32;
                int idx_DienThoaiNDD = 33;
                int idx_MoiQuanHeNDD = 34;
                int idx_EmailNDD = 35;
                //Điểm 6
                int idx_Toan6 = 36;
                int idx_Ly6 = 37;
                int idx_Van6 = 38;
                int idx_Anh6 = 39;
                int idx_Sinh6 = 40;
                int idx_TBM6 = 41;
                int idx_XLHL6 = 42;
                int idx_XLHK6 = 43;
                //Điểm 7
                int idx_Toan7 = 44;
                int idx_Ly7 = 45;
                int idx_Van7 = 46;
                int idx_Anh7 = 47;
                int idx_Sinh7 = 48;
                int idx_TBM7 = 49;
                int idx_XLHL7 = 50;
                int idx_XLHK7 = 51;
                //Điểm 8
                int idx_Toan8 = 52;
                int idx_Ly8 = 53;
                int idx_Hoa8 = 54;
                int idx_Van8 = 55;
                int idx_Anh8 = 56;
                int idx_Sinh8 = 57;
                int idx_TBM8 = 58;
                int idx_XLHL8 = 59;
                int idx_XLHK8 = 60;

                //Điểm 9
                int idx_Toan9 = 61;
                int idx_Ly9 = 62;
                int idx_Hoa9 = 63;
                int idx_Van9 = 64;
                int idx_Anh9 = 65;
                int idx_Sinh9 = 66;
                int idx_TBM9 = 67;
                int idx_XLHL9 = 68;
                int idx_XLHK9 = 69;
                //
                int idx_TungNam9 = 70;
                int idx_TungNamTungMon85 = 71;
                int idx_TungNamTungMon8 = 72;
                int idx_TungNamTungMon7 = 73;
                //Điểm 10
                int idx_Toan10 = 74;
                int idx_Ly10 = 75;
                int idx_Hoa10 = 76;
                int idx_Van10 = 77;
                int idx_Anh10 = 78;
                int idx_Sinh10 = 79;
                int idx_TBM10 = 80;
                int idx_XLHL10 = 81;
                int idx_XLHK10 = 82;
                //Điểm 11
                int idx_Toan11 = 83;
                int idx_Ly11 = 84;
                int idx_Hoa11 = 85;
                int idx_Van11 = 86;
                int idx_Anh11 = 87;
                int idx_Sinh11 = 88;
                int idx_TBM11 = 89;
                int idx_XLHL11 = 90;
                int idx_XLHK11 = 91;
                //Khối thi
                int idx_KHTN = 92;
                int idx_KHXH = 93;

                //Giấy tờ
                int idx_HocBaTH = 94;
                int idx_HocBaTHCS = 95;
                int idx_HocBaTHPT = 96;
                int idx_GiayKhaiSinh = 97;
                int idx_GiayCNHTCTTH = 98;
                int idx_GiayTN_THCSTamThoi = 99;
                int idx_BangTotNghiep = 100;
                int idx_GiayNgheTHCS = 101;
                int idx_GiayTrungTuyenLop10 = 102;
                int idx_GiayNgheTHPT = 103;
                int idx_GiayTGCT = 104;
                // 4 năm
                int idx_4Nam = 105;

                //Chính sách
                int idx_ChinhSachMienGiam = 106;
                int idx_GhiChu = 107;
                //bổ sung
                int idx_LoaiTruong = 108;
                int idx_NgayNhapHoc = 109;
                int idx_HinhThucNopHS = 110;
                int idx_NguonThuThap = 111;
                int idx_NamHoc = 112;
                int idx_NguoiLap = 113;



                /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////
                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();

                    //Duyệt qua tất cả các dòng trong file excel
                    foreach (DataRow dr in dt.Rows)
                    {
                        StringBuilder detailLog = new StringBuilder();

                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////

                        String txt_HoHS = dr[idx_HoHS].ToString().FullTrim();
                        String txt_TenHS = dr[idx_TenHS].ToString().FullTrim();
                        String txt_GioiTinhHS = dr[idx_GioiTinhHS].ToString().FullTrim();
                        String txt_NgaySinh = dr[idx_NgaySinhHS].ToString().FullTrim();
                        String txt_NoiSinh = dr[idx_NoiSinh].ToString().FullTrim();
                        String txt_DanToc = dr[idx_DanToc].ToString().FullTrim();
                        String txt_TonGiao = dr[idx_TonGiao].ToString().FullTrim();
                        String txt_LoaiHS = dr[idx_LoaiHocSinh].ToString().FullTrim();
                        String txt_CheDoHoc = dr[idx_CheDoHoc].ToString().FullTrim();
                        String txt_Khoi = dr[idx_Khoi].ToString().FullTrim();
                        //Địa chỉ thường trú
                        String txt_SoNha = dr[idx_SoNha].ToString().FullTrim();
                        String txt_XaPhuong = dr[idx_XaPhuong].ToString().FullTrim();
                        String txt_QuanHuyen = dr[idx_QuanHuyen].ToString().FullTrim();
                        String txt_TinhThanh = dr[idx_TinhThanh].ToString().FullTrim();
                        //Địa chỉ liên lạc
                        String txt_SoNhaLL = dr[idx_SoNhaLL].ToString().FullTrim();
                        String txt_XaPhuongLL = dr[idx_XaPhuongLL].ToString().FullTrim();
                        String txt_QuanHuyenLL = dr[idx_QuanHuyenLL].ToString().FullTrim();
                        String txt_TinhThanhLL = dr[idx_TinhThanhLL].ToString().FullTrim();

                        //Thông tin ba
                        String txt_HoTenCha = dr[idx_HoTenCha].ToString().FullTrim();
                        String txt_NgheNghiepCha = dr[idx_NgheNghiepCha].ToString().FullTrim();
                        String txt_DienThoaiCha = dr[idx_DienThoaiCha].ToString().FullTrim();
                        String txt_EmailCha = dr[idx_EmailCha].ToString().FullTrim();

                        //Thông tin mẹ
                        String txt_HoTenMe = dr[idx_HoTenMe].ToString().FullTrim();
                        String txt_NgheNghiepMe = dr[idx_NgheNghiepMe].ToString().FullTrim();
                        String txt_DienThoaiMe = dr[idx_DienThoaiMe].ToString().FullTrim();
                        String txt_EmailMe = dr[idx_EmailMe].ToString().FullTrim();

                        //Thông tin Người đưa đón
                        String txt_HoTenNDD = dr[idx_HoTenNDD].ToString().FullTrim();
                        String txt_NgheNghiepNDD = dr[idx_NgheNghiepNDD].ToString().FullTrim();
                        String txt_DienThoaiNDD = dr[idx_DienThoaiNDD].ToString().FullTrim();
                        String txt_EmailNDD = dr[idx_EmailNDD].ToString().FullTrim();
                        String txt_MoiQuanHeNDD = dr[idx_MoiQuanHeNDD].ToString().FullTrim();
                        String txt_NgaySinhNDD = dr[idx_NamSinhNDD].ToString().FullTrim();

                        // Điểm khối 6
                        String txt_Toan6 = dr[idx_Toan6].ToString().FullTrim().Replace(".", ",");
                        String txt_Ly6 = dr[idx_Ly6].ToString().FullTrim().Replace(".", ",");
                        String txt_Van6 = dr[idx_Van6].ToString().FullTrim().Replace(".", ",");
                        String txt_Anh6 = dr[idx_Anh6].ToString().FullTrim().Replace(".", ",");
                        String txt_Sinh6 = dr[idx_Sinh6].ToString().FullTrim().Replace(".", ",");
                        String txt_TBM6 = dr[idx_TBM6].ToString().FullTrim().Replace(".", ",");
                        String txt_XLHL6 = dr[idx_XLHL6].ToString().FullTrim();
                        String txt_XLHK6 = dr[idx_XLHK6].ToString().FullTrim();


                        // Điểm khối 7
                        String txt_Toan7 = dr[idx_Toan7].ToString().FullTrim().Replace(".", ",");
                        String txt_Ly7 = dr[idx_Ly7].ToString().FullTrim().Replace(".", ",");
                        String txt_Van7 = dr[idx_Van7].ToString().FullTrim().Replace(".", ",");
                        String txt_Anh7 = dr[idx_Anh7].ToString().FullTrim().Replace(".", ",");
                        String txt_Sinh7 = dr[idx_Sinh7].ToString().FullTrim().Replace(".", ",");
                        String txt_TBM7 = dr[idx_TBM7].ToString().FullTrim().Replace(".", ",");
                        String txt_XLHL7 = dr[idx_XLHL7].ToString().FullTrim();
                        String txt_XLHK7 = dr[idx_XLHK7].ToString().FullTrim();

                        // Điểm khối 8
                        String txt_Toan8 = dr[idx_Toan8].ToString().FullTrim().Replace(".", ",");
                        String txt_Ly8 = dr[idx_Ly8].ToString().FullTrim().Replace(".", ",");
                        String txt_Hoa8 = dr[idx_Hoa8].ToString().FullTrim().Replace(".", ",");
                        String txt_Van8 = dr[idx_Van8].ToString().FullTrim().Replace(".", ",");
                        String txt_Anh8 = dr[idx_Anh8].ToString().FullTrim().Replace(".", ",");
                        String txt_Sinh8 = dr[idx_Sinh8].ToString().FullTrim().Replace(".", ",");
                        String txt_TBM8 = dr[idx_TBM8].ToString().FullTrim().Replace(".", ",");
                        String txt_XLHL8 = dr[idx_XLHL8].ToString().FullTrim();
                        String txt_XLHK8 = dr[idx_XLHK8].ToString().FullTrim();

                        // Điểm khối 9
                        String txt_Toan9 = dr[idx_Toan9].ToString().FullTrim().Replace(".", ",");
                        String txt_Ly9 = dr[idx_Ly9].ToString().FullTrim().Replace(".", ",");
                        String txt_Hoa9 = dr[idx_Hoa9].ToString().FullTrim().Replace(".", ",");
                        String txt_Van9 = dr[idx_Van9].ToString().FullTrim().Replace(".", ",");
                        String txt_Anh9 = dr[idx_Anh9].ToString().FullTrim().Replace(".", ",");
                        String txt_Sinh9 = dr[idx_Sinh9].ToString().FullTrim().Replace(".", ",");
                        String txt_TBM9 = dr[idx_TBM9].ToString().FullTrim().Replace(".", ",");
                        String txt_XLHL9 = dr[idx_XLHL9].ToString().FullTrim();
                        String txt_XLHK9 = dr[idx_XLHK9].ToString().FullTrim();


                        // Điểm khối 10
                        String txt_Toan10 = dr[idx_Toan10].ToString().FullTrim().Replace(".", ",");
                        String txt_Ly10 = dr[idx_Ly10].ToString().FullTrim().Replace(".", ",");
                        String txt_Hoa10 = dr[idx_Hoa10].ToString().FullTrim().Replace(".", ",");
                        String txt_Van10 = dr[idx_Van10].ToString().FullTrim().Replace(".", ",");
                        String txt_Anh10 = dr[idx_Anh10].ToString().FullTrim().Replace(".", ",");
                        String txt_Sinh10 = dr[idx_Sinh10].ToString().FullTrim().Replace(".", ",");
                        String txt_TBM10 = dr[idx_TBM10].ToString().FullTrim().Replace(".", ",");
                        String txt_XLHL10 = dr[idx_XLHL10].ToString().FullTrim();
                        String txt_XLHK10 = dr[idx_XLHK10].ToString().FullTrim();


                        // Điểm khối 11
                        String txt_Toan11 = dr[idx_Toan11].ToString().FullTrim().Replace(".", ",");
                        String txt_Ly11 = dr[idx_Ly11].ToString().FullTrim().Replace(".", ",");
                        String txt_Hoa11 = dr[idx_Hoa11].ToString().FullTrim().Replace(".", ",");
                        String txt_Van11 = dr[idx_Van11].ToString().FullTrim().Replace(".", ",");
                        String txt_Anh11 = dr[idx_Anh11].ToString().FullTrim().Replace(".", ",");
                        String txt_Sinh11 = dr[idx_Sinh11].ToString().FullTrim().Replace(".", ",");
                        String txt_TBM11 = dr[idx_TBM11].ToString().FullTrim().Replace(".", ",");
                        String txt_XLHL11 = dr[idx_XLHL11].ToString().FullTrim();
                        String txt_XLHK11 = dr[idx_XLHK11].ToString().FullTrim();
                        //Khoa học từ nhiên
                        String txt_KHTN = dr[idx_KHTN].ToString().FullTrim();
                        //Khoa học xã hội
                        String txt_KHXH = dr[idx_KHXH].ToString().FullTrim();

                        //Giấy tờ
                        String txt_HocBaTH = dr[idx_HocBaTH].ToString().FullTrim();
                        String txt_HocBaTHCS = dr[idx_HocBaTHCS].ToString().FullTrim();
                        String txt_HocBaTHPT = dr[idx_HocBaTHPT].ToString().FullTrim();
                        String txt_GiayKhaiSinh = dr[idx_GiayKhaiSinh].ToString().FullTrim();
                        String txt_GiayHTCTTH = dr[idx_GiayCNHTCTTH].ToString().FullTrim();
                        String txt_GiayCNTNTHCSTamThoi = dr[idx_GiayTN_THCSTamThoi].ToString().FullTrim();
                        String txt_BangTotNghiep = dr[idx_BangTotNghiep].ToString().FullTrim();
                        String txt_GiayNgheTHCS = dr[idx_GiayNgheTHCS].ToString().FullTrim();
                        String txt_GiayTrungTuyen10 = dr[idx_GiayTrungTuyenLop10].ToString().FullTrim();
                        String txt_GiayNgheTHPT = dr[idx_GiayNgheTHPT].ToString().FullTrim();
                        String txt_GiayGTCT = dr[idx_GiayTGCT].ToString().FullTrim();

                        //Chính sách miễn giảm
                        String txt_ChinhSachMienGiam = dr[idx_ChinhSachMienGiam].ToString().FullTrim();
                        String txt_GhiChu = dr[idx_GhiChu].ToString().FullTrim();
                        String txt_LoaiTruong = dr[idx_LoaiTruong].ToString().FullTrim();
                        String txt_NgayNhapHoc = dr[idx_NgayNhapHoc].ToString().FullTrim();
                        String txt_HinhThucNopHS = dr[idx_HinhThucNopHS].ToString().FullTrim();
                        String txt_NguonThuThap = dr[idx_NguonThuThap].ToString().FullTrim();
                        String txt_NamHoc = dr[idx_NamHoc].ToString().FullTrim();
                        String txt_NguoiLap = dr[idx_NguoiLap].ToString().FullTrim();

                        //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////

                        //
                        if (!string.IsNullOrEmpty(txt_LoaiHS) && txt_LoaiHS.Equals("HSM"))
                        {
                            if (!string.IsNullOrEmpty(txt_HoHS) && !string.IsNullOrEmpty(txt_TenHS) && !string.IsNullOrEmpty(txt_GioiTinhHS))
                            {
                                // Gán giới tính học sinh
                                GioiTinhEnum intGioiTinh;
                                if (txt_GioiTinhHS.Equals("Nam"))
                                {
                                    intGioiTinh = GioiTinhEnum.Nam;
                                }
                                else intGioiTinh = GioiTinhEnum.Nu;

                                DanhSachTre dstre = null;
                                ThongTinKhachHang ttkh = null;
                                DateTime dt_NgaySinh = Convert.ToDateTime(txt_NgaySinh.ToString());

                                // Tìm học sinh có trong Hồ sơ xét tuyển không?
                                //Session session = ((XPObjectSpace)obs).Session;
                                CriteriaOperator filter = CriteriaOperator.Parse("HoTen=? and NgaySinh = ? and GioiTinh = ?", txt_HoHS + " " + txt_TenHS, dt_NgaySinh, intGioiTinh);
                                HoSoXetTuyen hsxt = uow.FindObject<HoSoXetTuyen>(filter);

                                if (!string.IsNullOrEmpty(txt_Khoi))
                                {// Nếu chưa có thì
                                    if (hsxt == null)
                                    {
                                        //Kiểm tra danh sách trẻ và thông tin khách hàng
                                        if (!string.IsNullOrEmpty(txt_HoHS) && !string.IsNullOrEmpty(txt_TenHS) && !string.IsNullOrEmpty(txt_NgaySinh) && !string.IsNullOrEmpty(txt_GioiTinhHS))
                                        {
                                            // Kiểm tra tiếp học sinh đó có trong Danh sách trẻ chưa
                                            filter = CriteriaOperator.Parse("HoTen = ? and NgaySinh = ? and GioiTinh = ?", txt_HoHS + " " + txt_TenHS, dt_NgaySinh, intGioiTinh);

                                            dstre = uow.FindObject<DanhSachTre>(filter);
                                            //Nếu có rồi thì update lại thông tin
                                            if (dstre != null)
                                            {
                                                dstre.KhoiSIS = txt_Khoi;
                                                dstre.NoiSinh = txt_NoiSinh;

                                                //update thông tin 3 mẹ theo học sinh
                                                ttkh = uow.GetObjectByKey<ThongTinKhachHang>(dstre.ThongTinKhachHang.Oid);
                                                if (ttkh != null)
                                                {
                                                    if (!string.IsNullOrEmpty(txt_HoTenCha))
                                                    {
                                                        ttkh.HoTen = txt_HoTenCha;
                                                        ttkh.GioiTinh = GioiTinhEnum.Nam;
                                                        ttkh.DienThoaiDiDong = txt_DienThoaiCha;
                                                        ttkh.Email = txt_EmailCha;
                                                        ttkh.NgheNghiep = txt_NgheNghiepCha;
                                                        QuanHe qhgd = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", "Cha"));
                                                        if (qhgd != null)
                                                            ttkh.QuanHe = qhgd;
                                                        if (!string.IsNullOrEmpty(txt_HoTenMe))
                                                        {
                                                            ttkh.HoTenLLK = txt_HoTenMe;
                                                            ttkh.SDTLLK = txt_DienThoaiMe;
                                                            ttkh.EmailLLK = txt_EmailMe;
                                                            QuanHe llk = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", "Mẹ"));
                                                            if (llk != null)
                                                                ttkh.QuanHeLLK = llk;
                                                        }
                                                        else if (!string.IsNullOrEmpty(txt_HoTenNDD))
                                                        {
                                                            ttkh.HoTenLLK = txt_HoTenNDD;
                                                            ttkh.SDTLLK = txt_DienThoaiNDD;
                                                            ttkh.EmailLLK = txt_EmailNDD;
                                                            QuanHe llk = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", txt_MoiQuanHeNDD));
                                                            if (llk != null)
                                                                ttkh.QuanHeLLK = llk;
                                                        }
                                                    }
                                                    else if (!string.IsNullOrEmpty(txt_HoTenMe))
                                                    {
                                                        ttkh.HoTen = txt_HoTenMe;
                                                        ttkh.GioiTinh = GioiTinhEnum.Nu;
                                                        ttkh.DienThoaiDiDong = txt_DienThoaiMe;
                                                        ttkh.Email = txt_EmailMe;
                                                        ttkh.NgheNghiep = txt_NgheNghiepMe;
                                                        QuanHe qhgd = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", "Mẹ"));
                                                        if (qhgd != null)
                                                            ttkh.QuanHe = qhgd;
                                                        if (!string.IsNullOrEmpty(txt_HoTenNDD))
                                                        {
                                                            ttkh.HoTenLLK = txt_HoTenNDD;
                                                            ttkh.SDTLLK = txt_DienThoaiNDD;
                                                            ttkh.EmailLLK = txt_EmailNDD;
                                                            QuanHe llk = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", txt_MoiQuanHeNDD));
                                                            if (llk != null)
                                                                ttkh.QuanHeLLK = llk;
                                                        }
                                                    }
                                                    else if (!string.IsNullOrEmpty(txt_HoTenNDD))
                                                    {
                                                        ttkh.HoTen = txt_HoTenNDD;
                                                        ttkh.DienThoaiDiDong = txt_DienThoaiNDD;
                                                        ttkh.Email = txt_EmailNDD;
                                                        QuanHe qhgd = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", txt_MoiQuanHeNDD));
                                                        if (qhgd != null)
                                                            ttkh.QuanHe = qhgd;
                                                    }

                                                    DiaChi diaChi = new DiaChi(uow);
                                                    if (!string.IsNullOrEmpty(txt_TinhThanh))
                                                    {
                                                        TinhThanh tT = uow.FindObject<TinhThanh>(CriteriaOperator.Parse("TenTinhThanh like ? ", "%" + txt_TinhThanh + "%"));
                                                        if (tT != null)
                                                        {
                                                            diaChi.TinhThanh = tT;
                                                            if (!string.IsNullOrEmpty(txt_QuanHuyen))
                                                            {
                                                                QuanHuyen qh = uow.FindObject<QuanHuyen>(CriteriaOperator.Parse("TinhThanh = ? and                                     TenQuanHuyen like ?", tT, "%" + txt_QuanHuyen + "%"));
                                                                if (qh != null)
                                                                {
                                                                    diaChi.QuanHuyen = qh;
                                                                    if (!string.IsNullOrEmpty(txt_XaPhuong))
                                                                    {
                                                                        XaPhuong xp = uow.FindObject<XaPhuong>(CriteriaOperator.Parse("QuanHuyen = ? and                                       TenXaPhuong like ?", qh, "%" + txt_XaPhuong + "%"));
                                                                        if (xp != null)
                                                                        {
                                                                            diaChi.XaPhuong = xp;
                                                                        }
                                                                    }
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

                                                    ttkh.DiaChiThuongTru = diaChi;
                                                }
                                            }
                                            //Nếu chưa có danh sách trẻ thì thêm mới
                                            else
                                            {
                                                // Thêm thông tin danh sách trẻ
                                                dstre = new DanhSachTre(uow);
                                                dstre.Ho = txt_HoHS;
                                                dstre.Ten = txt_TenHS;
                                                dstre.NoiSinh = txt_NoiSinh;
                                                dstre.GioiTinh = intGioiTinh;

                                                try
                                                {
                                                    dstre.NgaySinh = Convert.ToDateTime(txt_NgaySinh);
                                                }
                                                catch (Exception ex)
                                                {
                                                }
                                                dstre.KhoiSIS = txt_Khoi;

                                                //Tạo mới khách hàng
                                                ttkh = new ThongTinKhachHang(uow);
                                                if (!string.IsNullOrEmpty(txt_HoTenCha))
                                                {
                                                    ttkh.HoTen = txt_HoTenCha;
                                                    ttkh.GioiTinh = GioiTinhEnum.Nam;
                                                    ttkh.DienThoaiDiDong = txt_DienThoaiCha;
                                                    ttkh.Email = txt_EmailCha;
                                                    ttkh.NgheNghiep = txt_NgheNghiepCha;
                                                    QuanHe qhgd = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", "Cha"));
                                                    if (qhgd != null)
                                                        ttkh.QuanHe = qhgd;
                                                    if (!string.IsNullOrEmpty(txt_HoTenMe))
                                                    {
                                                        ttkh.HoTenLLK = txt_HoTenMe;
                                                        ttkh.SDTLLK = txt_DienThoaiMe;
                                                        ttkh.EmailLLK = txt_EmailMe;
                                                        QuanHe llk = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", "Mẹ"));
                                                        if (llk != null)
                                                            ttkh.QuanHeLLK = llk;
                                                    }
                                                    else if (!string.IsNullOrEmpty(txt_HoTenNDD))
                                                    {
                                                        ttkh.HoTenLLK = txt_HoTenNDD;
                                                        ttkh.SDTLLK = txt_DienThoaiNDD;
                                                        ttkh.EmailLLK = txt_EmailNDD;
                                                        QuanHe llk = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", txt_MoiQuanHeNDD));
                                                        if (llk != null)
                                                            ttkh.QuanHeLLK = llk;
                                                    }
                                                }
                                                else if (!string.IsNullOrEmpty(txt_HoTenMe))
                                                {
                                                    ttkh.HoTen = txt_HoTenMe;
                                                    ttkh.GioiTinh = GioiTinhEnum.Nu;
                                                    ttkh.DienThoaiDiDong = txt_DienThoaiMe;
                                                    ttkh.Email = txt_EmailMe;
                                                    ttkh.NgheNghiep = txt_NgheNghiepMe;
                                                    QuanHe qhgd = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", "Mẹ"));
                                                    if (qhgd != null)
                                                        ttkh.QuanHe = qhgd;
                                                    if (!string.IsNullOrEmpty(txt_HoTenNDD))
                                                    {
                                                        ttkh.HoTenLLK = txt_HoTenNDD;
                                                        ttkh.SDTLLK = txt_DienThoaiNDD;
                                                        ttkh.EmailLLK = txt_EmailNDD;
                                                        QuanHe llk = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", txt_MoiQuanHeNDD));
                                                        if (llk != null)
                                                            ttkh.QuanHeLLK = llk;
                                                    }
                                                }
                                                else if (!string.IsNullOrEmpty(txt_HoTenNDD))
                                                {
                                                    ttkh.HoTen = txt_HoTenNDD;
                                                    ttkh.DienThoaiDiDong = txt_DienThoaiNDD;
                                                    ttkh.Email = txt_EmailNDD;
                                                    QuanHe qhgd = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", txt_MoiQuanHeNDD));
                                                    if (qhgd != null)
                                                        ttkh.QuanHe = qhgd;
                                                }


                                                DiaChi diaChi = new DiaChi(uow);
                                                if (!string.IsNullOrEmpty(txt_TinhThanh))
                                                {
                                                    TinhThanh tT = uow.FindObject<TinhThanh>(CriteriaOperator.Parse("TenTinhThanh like ? ", "%" + txt_TinhThanh + "%"));
                                                    if (tT != null)
                                                    {
                                                        diaChi.TinhThanh = tT;
                                                        if (!string.IsNullOrEmpty(txt_QuanHuyen))
                                                        {
                                                            QuanHuyen qh = uow.FindObject<QuanHuyen>(CriteriaOperator.Parse("TinhThanh = ? and                                       TenQuanHuyen like ?", tT, "%" + txt_QuanHuyen + "%"));
                                                            if (qh != null)
                                                            {
                                                                diaChi.QuanHuyen = qh;
                                                                if (!string.IsNullOrEmpty(txt_XaPhuong))
                                                                {
                                                                    XaPhuong xp = uow.FindObject<XaPhuong>(CriteriaOperator.Parse("QuanHuyen = ? and                                       TenXaPhuong like ?", qh, "%" + txt_XaPhuong + "%"));
                                                                    if (xp != null)
                                                                    {
                                                                        diaChi.XaPhuong = xp;
                                                                    }
                                                                }
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

                                                ttkh.DiaChiThuongTru = diaChi;
                                                // gán con vào cha
                                                dstre.ThongTinKhachHang = ttkh;
                                            }
                                        }
                                        // Tạo hồ sơ xét tuyển cho học sinh
                                        hsxt = new HoSoXetTuyen(uow);
                                        hsxt.Ho = txt_HoHS;
                                        hsxt.Ten = txt_TenHS;
                                        hsxt.ThongTinKhachHang = ttkh;
                                        hsxt.DiaChiThuongTru = ttkh.DiaChiThuongTru;
                                        hsxt.DanhSachTre = dstre;
                                        hsxt.IsImport = true;
                                        hsxt.GioiTinh = intGioiTinh;
                                        hsxt.NoiSinh = txt_NoiSinh;
                                        try
                                        {
                                            hsxt.NgaySinh = Convert.ToDateTime(txt_NgaySinh);
                                        }
                                        catch (Exception ex)
                                        {
                                        }
                                        //Gán giá trị cho khối
                                        if (!string.IsNullOrEmpty(txt_Khoi))
                                        {
                                            hsxt.KhoiSIS = txt_Khoi;
                                        }
                                        else
                                        {

                                        }
                                        if (!string.IsNullOrEmpty(txt_HinhThucNopHS))
                                        {
                                            if (txt_HinhThucNopHS.Equals("Trực tiếp"))
                                                hsxt.HinhThucNopHoSo = HinhThucNopHoSoEnum.TrucTiep;
                                            else if (txt_HinhThucNopHS.Equals("Online"))
                                                hsxt.HinhThucNopHoSo = HinhThucNopHoSoEnum.Online;
                                            else if (txt_HinhThucNopHS.Equals("Khác"))
                                                hsxt.HinhThucNopHoSo = HinhThucNopHoSoEnum.Khac;
                                        }


                                        TonGiao tg = uow.FindObject<TonGiao>(CriteriaOperator.Parse("TenTonGiao = ?", txt_TonGiao));
                                        if (tg != null)
                                            hsxt.TonGiao = tg;
                                        DanToc danToc = uow.FindObject<DanToc>(CriteriaOperator.Parse("TenDanToc = ?", txt_DanToc));
                                        if (danToc != null)
                                            hsxt.DanToc = danToc;
                                        LoaiTruong loaiTruong = uow.FindObject<LoaiTruong>(CriteriaOperator.Parse("TenLoaiTruong = ?", txt_LoaiTruong));
                                        if (loaiTruong != null)
                                            hsxt.LoaiTruong = loaiTruong;
                                        NamHoc namHoc = uow.FindObject<NamHoc>(CriteriaOperator.Parse("TenNamHoc = ?", txt_NamHoc));
                                        if (namHoc != null)
                                            hsxt.NamHoc = namHoc;
                                        SecuritySystemUser_Custom user = uow.FindObject<SecuritySystemUser_Custom>(CriteriaOperator.Parse("UserName = ?", txt_NguoiLap));
                                        if (user != null)
                                            hsxt.SecuritySystemUser = user;


                                        if (!string.IsNullOrEmpty(txt_CheDoHoc))
                                        {
                                            if (txt_CheDoHoc.Equals("Nội trú"))
                                            {
                                                hsxt.HinhThucHoc = HinhThucHocEnum.NoiTru;
                                            }
                                            else if (txt_CheDoHoc.Equals("Bán trú"))
                                            {
                                                hsxt.HinhThucHoc = HinhThucHocEnum.BanTru;
                                            }
                                            else if (txt_CheDoHoc.Equals("Hai buổi"))
                                            {
                                                hsxt.HinhThucHoc = HinhThucHocEnum.HaiBuoi;
                                            }
                                        }
                                        try
                                        {
                                            hsxt.NgayNhapHoc = Convert.ToDateTime(txt_NgayNhapHoc);
                                        }
                                        catch (Exception ex)
                                        {
                                        }
                                        // hồ sơ
                                        foreach (var item in hsxt.HoSoList)
                                        {
                                            if (!string.IsNullOrEmpty(txt_HocBaTH) && txt_HocBaTH.Equals("X"))
                                            {
                                                if (item.HoSoTuyenSinh.TenHoSo.Contains("Học bạ tiểu học"))
                                                {
                                                    item.DaNopHoSo = true;
                                                    item.BangChinh = true;
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(txt_HocBaTHCS) && txt_HocBaTHCS.Equals("X"))
                                            {
                                                if (item.HoSoTuyenSinh.TenHoSo.Contains("Học bạ lớp 6"))
                                                {
                                                    item.DaNopHoSo = true;
                                                    item.BangChinh = true;
                                                }
                                                else if (item.HoSoTuyenSinh.TenHoSo.Contains("Học bạ trung học cơ sở"))
                                                {
                                                    item.DaNopHoSo = true;
                                                    item.BangChinh = true;
                                                }

                                            }

                                            if (!string.IsNullOrEmpty(txt_HocBaTHPT) && txt_HocBaTHPT.Equals("X"))
                                            {
                                                if (item.HoSoTuyenSinh.TenHoSo.Contains("Học bạ lớp 10"))
                                                {
                                                    item.DaNopHoSo = true;
                                                    item.BangChinh = true;
                                                }
                                                else if (item.HoSoTuyenSinh.TenHoSo.Contains("Học bạ lớp 11"))
                                                {
                                                    item.DaNopHoSo = true;
                                                    item.BangChinh = true;
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(txt_GiayKhaiSinh) && txt_GiayKhaiSinh.Equals("X"))
                                            {
                                                if (item.HoSoTuyenSinh.TenHoSo.Contains("Giấy khai sinh"))
                                                {
                                                    item.DaNopHoSo = true;
                                                    item.BangChinh = true;
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(txt_GiayHTCTTH) && txt_GiayHTCTTH.Equals("X"))
                                            {
                                                if (item.HoSoTuyenSinh.TenHoSo.Contains("Giấy chứng nhận hoàn thành chương trình tiểu học"))
                                                {
                                                    item.DaNopHoSo = true;
                                                    item.BangChinh = true;
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(txt_GiayCNTNTHCSTamThoi) && txt_GiayCNTNTHCSTamThoi.Equals("X"))
                                            {
                                                if (item.HoSoTuyenSinh.TenHoSo.Contains("Giấy chứng nhận tốt nghiệp tạm thời"))
                                                {
                                                    item.DaNopHoSo = true;
                                                    item.BangChinh = true;
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(txt_BangTotNghiep) && txt_BangTotNghiep.Equals("X"))
                                            {
                                                if (item.HoSoTuyenSinh.TenHoSo.Contains("Bằng tốt nghiệp THCS"))
                                                {
                                                    item.DaNopHoSo = true;
                                                    item.BangChinh = true;
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(txt_GiayNgheTHCS) && txt_GiayNgheTHCS.Equals("X"))
                                            {
                                                if (item.HoSoTuyenSinh.TenHoSo.Contains("Giấy nghề THCS"))
                                                {
                                                    item.DaNopHoSo = true;
                                                    item.BangChinh = true;
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(txt_GiayTrungTuyen10) && txt_GiayTrungTuyen10.Equals("X"))
                                            {
                                                if (item.HoSoTuyenSinh.TenHoSo.Contains("Giấy trúng tuyển lớp 10"))
                                                {
                                                    item.DaNopHoSo = true;
                                                    item.BangChinh = true;
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(txt_GiayNgheTHPT) && txt_GiayNgheTHPT.Equals("X"))
                                            {
                                                if (item.HoSoTuyenSinh.TenHoSo.Contains("Nghề phổ thông"))
                                                {
                                                    item.DaNopHoSo = true;
                                                    item.BangChinh = true;
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(txt_GiayGTCT) && txt_GiayGTCT.Equals("X"))
                                            {
                                                if (item.HoSoTuyenSinh.TenHoSo.Contains("Giấy giới thiệu chuyển trường"))
                                                {
                                                    item.DaNopHoSo = true;
                                                    item.BangChinh = true;
                                                }
                                            }
                                        }
                                        // kết quá cuối năm
                                        foreach (var kq in hsxt.KetQuaList)
                                        {

                                            //Khối 6
                                            if (!string.IsNullOrEmpty(txt_TBM6) && "txt_TBM6".Contains(kq.KhoiSIS))
                                            {
                                                kq.DiemTrungBinh = Convert.ToDecimal(txt_TBM6);
                                            }
                                            if (!string.IsNullOrEmpty(txt_XLHL6) && "txt_XLHL6".Contains(kq.KhoiSIS))
                                            {
                                                if (txt_XLHL6.Equals("Khá"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.Kha;
                                                else if (txt_XLHL6.Equals("Giỏi"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.Gioi;
                                                else if (txt_XLHL6.Equals("TB") || txt_XLHL6.Equals("Trung bình"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.TrungBinh;
                                                else if (txt_XLHL6.Equals("Yếu"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.Yeu;
                                            }
                                            if (!string.IsNullOrEmpty(txt_XLHK6) && "txt_XLHK6".Contains(kq.KhoiSIS))
                                            {
                                                if (txt_XLHK6.Equals("Khá"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.Kha;
                                                else if (txt_XLHK6.Equals("Tốt"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.Tot;
                                                else if (txt_XLHK6.Equals("TB") || txt_XLHK6.Equals("Trung bình"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.TrungBinh;
                                                else if (txt_XLHK6.Equals("Yếu"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.Yeu;
                                            }

                                            //Khối 7
                                            if (!string.IsNullOrEmpty(txt_TBM7) && "txt_TBM7".Contains(kq.KhoiSIS))
                                            {
                                                kq.DiemTrungBinh = Convert.ToDecimal(txt_TBM7);

                                            }
                                            if (!string.IsNullOrEmpty(txt_XLHL7) && "txt_XLHL7".Contains(kq.KhoiSIS))
                                            {
                                                if (txt_XLHL7.Equals("Khá"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.Kha;
                                                else if (txt_XLHL7.Equals("Giỏi"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.Gioi;
                                                else if (txt_XLHL7.Equals("TB") || txt_XLHL7.Equals("Trung bình"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.TrungBinh;
                                                else if (txt_XLHL7.Equals("Yếu"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.Yeu;
                                            }
                                            if (!string.IsNullOrEmpty(txt_XLHK7) && "txt_XLHK7".Contains(kq.KhoiSIS))
                                            {
                                                if (txt_XLHK7.Equals("Khá"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.Kha;
                                                else if (txt_XLHK7.Equals("Tốt"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.Tot;
                                                else if (txt_XLHK7.Equals("TB") || txt_XLHK7.Equals("Trung bình"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.TrungBinh;
                                                else if (txt_XLHK7.Equals("Yếu"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.Yeu;
                                            }

                                            //Khối 8
                                            if (!string.IsNullOrEmpty(txt_TBM8) && "txt_TBM8".Contains(kq.KhoiSIS))
                                            {
                                                kq.DiemTrungBinh = Convert.ToDecimal(txt_TBM8);

                                            }
                                            if (!string.IsNullOrEmpty(txt_XLHL8) && "txt_XLHL8".Contains(kq.KhoiSIS))
                                            {
                                                if (txt_XLHL8.Equals("Khá"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.Kha;
                                                else if (txt_XLHL8.Equals("Giỏi"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.Gioi;
                                                else if (txt_XLHL8.Equals("TB") || txt_XLHL8.Equals("Trung bình"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.TrungBinh;
                                                else if (txt_XLHL8.Equals("Yếu"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.Yeu;
                                            }
                                            if (!string.IsNullOrEmpty(txt_XLHK8) && "txt_XLHK8".Contains(kq.KhoiSIS))
                                            {
                                                if (txt_XLHK8.Equals("Khá"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.Kha;
                                                else if (txt_XLHK8.Equals("Tốt"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.Tot;
                                                else if (txt_XLHK8.Equals("TB") || txt_XLHK8.Equals("Trung bình"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.TrungBinh;
                                                else if (txt_XLHK8.Equals("Yếu"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.Yeu;
                                            }


                                            //Khối 9
                                            if (!string.IsNullOrEmpty(txt_TBM9) && "txt_TBM9".Contains(kq.KhoiSIS))
                                            {
                                                kq.DiemTrungBinh = Convert.ToDecimal(txt_TBM9);

                                            }
                                            if (!string.IsNullOrEmpty(txt_XLHL9) && "txt_XLHL9".Contains(kq.KhoiSIS))
                                            {
                                                if (txt_XLHL9.Equals("Khá"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.Kha;
                                                else if (txt_XLHL9.Equals("Giỏi"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.Gioi;
                                                else if (txt_XLHL9.Equals("TB") || txt_XLHL9.Equals("Trung bình"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.TrungBinh;
                                                else if (txt_XLHL9.Equals("Yếu"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.Yeu;
                                            }
                                            if (!string.IsNullOrEmpty(txt_XLHK9) && "txt_XLHK9".Contains(kq.KhoiSIS))
                                            {
                                                if (txt_XLHK9.Equals("Khá"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.Kha;
                                                else if (txt_XLHK9.Equals("Tốt"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.Tot;
                                                else if (txt_XLHK9.Equals("TB") || txt_XLHK9.Equals("Trung bình"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.TrungBinh;
                                                else if (txt_XLHK9.Equals("Yếu"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.Yeu;
                                            }


                                            //Khối 10
                                            if (!string.IsNullOrEmpty(txt_TBM10) && "txt_TBM10".Contains(kq.KhoiSIS))
                                            {
                                                kq.DiemTrungBinh = Convert.ToDecimal(txt_TBM10);

                                            }
                                            if (!string.IsNullOrEmpty(txt_XLHL10) && "txt_XLHL10".Contains(kq.KhoiSIS))
                                            {
                                                if (txt_XLHL10.Equals("Khá"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.Kha;
                                                else if (txt_XLHL10.Equals("Giỏi"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.Gioi;
                                                else if (txt_XLHL10.Equals("TB") || txt_XLHL10.Equals("Trung bình"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.TrungBinh;
                                                else if (txt_XLHL10.Equals("Yếu"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.Yeu;
                                            }
                                            if (!string.IsNullOrEmpty(txt_XLHK10) && "txt_XLHK10".Contains(kq.KhoiSIS))
                                            {
                                                if (txt_XLHK10.Equals("Khá"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.Kha;
                                                else if (txt_XLHK10.Equals("Tốt"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.Tot;
                                                else if (txt_XLHK10.Equals("TB") || txt_XLHK10.Equals("Trung bình"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.TrungBinh;
                                                else if (txt_XLHK10.Equals("Yếu"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.Yeu;
                                            }


                                            //Khối 11
                                            if (!string.IsNullOrEmpty(txt_TBM11) && "txt_TBM11".Contains(kq.KhoiSIS))
                                            {
                                                kq.DiemTrungBinh = Convert.ToDecimal(txt_TBM11);

                                            }
                                            if (!string.IsNullOrEmpty(txt_XLHL11) && "txt_XLHL11".Contains(kq.KhoiSIS))
                                            {
                                                if (txt_XLHL11.Equals("Khá"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.Kha;
                                                else if (txt_XLHL11.Equals("Giỏi"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.Gioi;
                                                else if (txt_XLHL11.Equals("TB") || txt_XLHL11.Equals("Trung bình"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.TrungBinh;
                                                else if (txt_XLHL11.Equals("Yếu"))
                                                    kq.HocLuc = XepLoaiTotNghiepEnum.Yeu;
                                            }
                                            if (!string.IsNullOrEmpty(txt_XLHK11) && "txt_XLHK11".Contains(kq.KhoiSIS))
                                            {
                                                if (txt_XLHK11.Equals("Khá"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.Kha;
                                                else if (txt_XLHK11.Equals("Tốt"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.Tot;
                                                else if (txt_XLHK11.Equals("TB") || txt_XLHK11.Equals("Trung bình"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.TrungBinh;
                                                else if (txt_XLHK11.Equals("Yếu"))
                                                    kq.HanhKiem = XepLoaiHanhKiemEnum.Yeu;
                                            }
                                        }

                                        foreach (var diemTB in hsxt.ListDTB)
                                        {
                                            //XPCollection<DanhMucMonXetTuyen> mon = new XPCollection<DanhMucMonXetTuyen>(CriteriaOperator.Parse("KhoiSIS = ? And CongTy = ?", hsxt.KhoiSIS, hsxt.CongTy));
                                            // Toán
                                            if (diemTB.Mon.TenMonHoc.Equals("Toán") && !string.IsNullOrEmpty(txt_Toan6))
                                            {
                                                diemTB.DiemTrungBinh6 = Convert.ToDecimal(txt_Toan6);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Toán") && !string.IsNullOrEmpty(txt_Toan7))
                                            {
                                                diemTB.DiemTrungBinh7 = Convert.ToDecimal(txt_Toan7);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Toán") && !string.IsNullOrEmpty(txt_Toan8))
                                            {
                                                diemTB.DiemTrungBinh8 = Convert.ToDecimal(txt_Toan8);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Toán") && !string.IsNullOrEmpty(txt_Toan9))
                                            {
                                                diemTB.DiemTrungBinh9 = Convert.ToDecimal(txt_Toan9);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Toán") && !string.IsNullOrEmpty(txt_Toan10))
                                            {
                                                diemTB.DiemTrungBinh10 = Convert.ToDecimal(txt_Toan10);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Toán") && !string.IsNullOrEmpty(txt_Toan11))
                                            {
                                                diemTB.DiemTrungBinh11 = Convert.ToDecimal(txt_Toan11);
                                            }


                                            //Lý
                                            if (diemTB.Mon.TenMonHoc.Equals("Vật Lý") && !string.IsNullOrEmpty(txt_Ly6))
                                            {
                                                diemTB.DiemTrungBinh6 = Convert.ToDecimal(txt_Ly6);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Vật Lý") && !string.IsNullOrEmpty(txt_Ly7))
                                            {
                                                diemTB.DiemTrungBinh7 = Convert.ToDecimal(txt_Ly7);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Vật Lý") && !string.IsNullOrEmpty(txt_Ly8))
                                            {
                                                diemTB.DiemTrungBinh8 = Convert.ToDecimal(txt_Ly8);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Vật Lý") && !string.IsNullOrEmpty(txt_Ly9))
                                            {
                                                diemTB.DiemTrungBinh9 = Convert.ToDecimal(txt_Ly9);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Vật Lý") && !string.IsNullOrEmpty(txt_Ly10))
                                            {
                                                diemTB.DiemTrungBinh10 = Convert.ToDecimal(txt_Ly10);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Vật Lý") && !string.IsNullOrEmpty(txt_Ly11))
                                            {
                                                diemTB.DiemTrungBinh11 = Convert.ToDecimal(txt_Ly11);
                                            }
                                            //Hóa
                                            if (diemTB.Mon.TenMonHoc.Equals("Hóa Học") && !string.IsNullOrEmpty(txt_Hoa8))
                                            {
                                                diemTB.DiemTrungBinh8 = Convert.ToDecimal(txt_Hoa8);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Hóa Học") && !string.IsNullOrEmpty(txt_Hoa9))
                                            {
                                                diemTB.DiemTrungBinh9 = Convert.ToDecimal(txt_Hoa9);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Hóa Học") && !string.IsNullOrEmpty(txt_Hoa10))
                                            {
                                                diemTB.DiemTrungBinh10 = Convert.ToDecimal(txt_Hoa10);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Hóa Học") && !string.IsNullOrEmpty(txt_Hoa11))
                                            {
                                                diemTB.DiemTrungBinh11 = Convert.ToDecimal(txt_Hoa11);
                                            }
                                            // văn
                                            if (diemTB.Mon.TenMonHoc.Equals("Ngữ Văn") && !string.IsNullOrEmpty(txt_Van6))
                                            {
                                                diemTB.DiemTrungBinh6 = Convert.ToDecimal(txt_Van6);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Ngữ Văn") && !string.IsNullOrEmpty(txt_Van7))
                                            {
                                                diemTB.DiemTrungBinh7 = Convert.ToDecimal(txt_Van7);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Ngữ Văn") && !string.IsNullOrEmpty(txt_Van8))
                                            {
                                                diemTB.DiemTrungBinh8 = Convert.ToDecimal(txt_Van8);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Ngữ Văn") && !string.IsNullOrEmpty(txt_Van9))
                                            {
                                                diemTB.DiemTrungBinh9 = Convert.ToDecimal(txt_Van9);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Ngữ Văn") && !string.IsNullOrEmpty(txt_Van10))
                                            {
                                                diemTB.DiemTrungBinh10 = Convert.ToDecimal(txt_Van10);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Ngữ Văn") && !string.IsNullOrEmpty(txt_Van11))
                                            {
                                                diemTB.DiemTrungBinh11 = Convert.ToDecimal(txt_Van11);
                                            }
                                            // Anh văn
                                            if (diemTB.Mon.TenMonHoc.Equals("Anh Văn") && !string.IsNullOrEmpty(txt_Anh6))
                                            {
                                                diemTB.DiemTrungBinh6 = Convert.ToDecimal(txt_Anh6);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Anh Văn") && !string.IsNullOrEmpty(txt_Anh7))
                                            {
                                                diemTB.DiemTrungBinh7 = Convert.ToDecimal(txt_Anh7);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Anh Văn") && !string.IsNullOrEmpty(txt_Anh8))
                                            {
                                                diemTB.DiemTrungBinh8 = Convert.ToDecimal(txt_Anh8);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Anh Văn") && !string.IsNullOrEmpty(txt_Anh9))
                                            {
                                                diemTB.DiemTrungBinh9 = Convert.ToDecimal(txt_Anh9);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Anh Văn") && !string.IsNullOrEmpty(txt_Anh10))
                                            {
                                                diemTB.DiemTrungBinh10 = Convert.ToDecimal(txt_Anh10);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Anh Văn") && !string.IsNullOrEmpty(txt_Anh11))
                                            {
                                                diemTB.DiemTrungBinh11 = Convert.ToDecimal(txt_Anh11);
                                            }
                                            // Sinh Học
                                            if (diemTB.Mon.TenMonHoc.Equals("Sinh Học") && !string.IsNullOrEmpty(txt_Sinh6))
                                            {
                                                diemTB.DiemTrungBinh6 = Convert.ToDecimal(txt_Sinh6);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Sinh Học") && !string.IsNullOrEmpty(txt_Sinh7))
                                            {
                                                diemTB.DiemTrungBinh7 = Convert.ToDecimal(txt_Sinh7);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Sinh Học") && !string.IsNullOrEmpty(txt_Sinh8))
                                            {
                                                diemTB.DiemTrungBinh8 = Convert.ToDecimal(txt_Sinh8);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Sinh Học") && !string.IsNullOrEmpty(txt_Sinh9))
                                            {
                                                diemTB.DiemTrungBinh9 = Convert.ToDecimal(txt_Sinh9);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Sinh Học") && !string.IsNullOrEmpty(txt_Sinh10))
                                            {
                                                diemTB.DiemTrungBinh10 = Convert.ToDecimal(txt_Sinh10);
                                            }
                                            if (diemTB.Mon.TenMonHoc.Equals("Sinh Học") && !string.IsNullOrEmpty(txt_Sinh11))
                                            {
                                                diemTB.DiemTrungBinh11 = Convert.ToDecimal(txt_Sinh11);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(txt_ChinhSachMienGiam))
                                        {
                                            string maChinhSach = KiemTraChinhSach(txt_ChinhSachMienGiam);

                                            if (!string.IsNullOrEmpty(maChinhSach))
                                            {
                                                HoSoXetTuyen_ChinhSachMienGiam chinhsach = new HoSoXetTuyen_ChinhSachMienGiam(uow);
                                                chinhsach.TenChinhSach = txt_ChinhSachMienGiam;
                                                chinhsach.MaChinhSach = maChinhSach;
                                                chinhsach.HoSoXetTuyen = hsxt;
                                                hsxt.ListChinhSachMienGiam.Add(chinhsach);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(txt_NguonThuThap))
                                        {
                                            NguonThuThap nguon = uow.FindObject<NguonThuThap>(CriteriaOperator.Parse("TenNguonThuThap= ?", txt_NguonThuThap));
                                            if (nguon != null)
                                            {
                                                HoSoXetTuyen_NguonThuThap nguonThuThap = new HoSoXetTuyen_NguonThuThap(uow);
                                                nguonThuThap.NguonThuThap = nguon;
                                                nguonThuThap.HoSoXetTuyen = hsxt;
                                                nguonThuThap.Chon = true;
                                                hsxt.ListNguonThuThapDanhSach.Add(nguonThuThap);
                                            }
                                        }
                                    }

                                    else if (hsxt != null)
                                    {
                                        if (hsxt.ThongTinKhachHang != null)
                                        {
                                            if (!string.IsNullOrEmpty(txt_HoTenCha))
                                            {
                                                hsxt.ThongTinKhachHang.HoTen = txt_HoTenCha;
                                                hsxt.ThongTinKhachHang.GioiTinh = GioiTinhEnum.Nam;
                                                hsxt.ThongTinKhachHang.DienThoaiDiDong = txt_DienThoaiCha;
                                                hsxt.ThongTinKhachHang.Email = txt_EmailCha;
                                                hsxt.ThongTinKhachHang.NgheNghiep = txt_NgheNghiepCha;
                                                QuanHe qhgd = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", "Cha"));
                                                if (qhgd != null)
                                                    hsxt.ThongTinKhachHang.QuanHe = qhgd;
                                                if (!string.IsNullOrEmpty(txt_HoTenMe))
                                                {
                                                    hsxt.ThongTinKhachHang.HoTenLLK = txt_HoTenMe;
                                                    hsxt.ThongTinKhachHang.SDTLLK = txt_DienThoaiMe;
                                                    hsxt.ThongTinKhachHang.EmailLLK = txt_EmailMe;
                                                    QuanHe llk = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", "Mẹ"));
                                                    if (llk != null)
                                                        hsxt.ThongTinKhachHang.QuanHeLLK = llk;
                                                }
                                                else if (!string.IsNullOrEmpty(txt_HoTenNDD))
                                                {
                                                    hsxt.ThongTinKhachHang.HoTenLLK = txt_HoTenNDD;
                                                    hsxt.ThongTinKhachHang.SDTLLK = txt_DienThoaiNDD;
                                                    hsxt.ThongTinKhachHang.EmailLLK = txt_EmailNDD;
                                                    QuanHe llk = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", txt_MoiQuanHeNDD));
                                                    if (llk != null)
                                                        hsxt.ThongTinKhachHang.QuanHeLLK = llk;
                                                }
                                            }
                                            else if (!string.IsNullOrEmpty(txt_HoTenMe))
                                            {
                                                hsxt.ThongTinKhachHang.HoTen = txt_HoTenMe;
                                                hsxt.ThongTinKhachHang.GioiTinh = GioiTinhEnum.Nu;
                                                hsxt.ThongTinKhachHang.DienThoaiDiDong = txt_DienThoaiMe;
                                                hsxt.ThongTinKhachHang.Email = txt_EmailMe;
                                                hsxt.ThongTinKhachHang.NgheNghiep = txt_NgheNghiepMe;
                                                QuanHe qhgd = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", "Mẹ"));
                                                if (qhgd != null)
                                                    hsxt.ThongTinKhachHang.QuanHe = qhgd;
                                                if (!string.IsNullOrEmpty(txt_HoTenNDD))
                                                {
                                                    hsxt.ThongTinKhachHang.HoTenLLK = txt_HoTenNDD;
                                                    hsxt.ThongTinKhachHang.SDTLLK = txt_DienThoaiNDD;
                                                    hsxt.ThongTinKhachHang.EmailLLK = txt_EmailNDD;
                                                    QuanHe llk = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", txt_MoiQuanHeNDD));
                                                    if (llk != null)
                                                        hsxt.ThongTinKhachHang.QuanHeLLK = llk;
                                                }
                                            }
                                            else if (!string.IsNullOrEmpty(txt_HoTenNDD))
                                            {
                                                hsxt.ThongTinKhachHang.HoTen = txt_HoTenNDD;
                                                hsxt.ThongTinKhachHang.DienThoaiDiDong = txt_DienThoaiNDD;
                                                hsxt.ThongTinKhachHang.Email = txt_EmailNDD;
                                                QuanHe qhgd = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe like ?", txt_MoiQuanHeNDD));
                                                if (qhgd != null)
                                                    hsxt.ThongTinKhachHang.QuanHe = qhgd;
                                            }

                                            if (hsxt.ThongTinKhachHang.DiaChiThuongTru == null)
                                            {
                                                DiaChi diaChi = new DiaChi(uow);
                                                if (!string.IsNullOrEmpty(txt_TinhThanh))
                                                {
                                                    TinhThanh tT = uow.FindObject<TinhThanh>(CriteriaOperator.Parse("TenTinhThanh like ? ", "%" + txt_TinhThanh + "%"));
                                                    if (tT != null)
                                                    {
                                                        diaChi.TinhThanh = tT;
                                                        if (!string.IsNullOrEmpty(txt_QuanHuyen))
                                                        {
                                                            QuanHuyen qh = uow.FindObject<QuanHuyen>(CriteriaOperator.Parse("TinhThanh = ? and                                     TenQuanHuyen like ?", tT, "%" + txt_QuanHuyen + "%"));
                                                            if (qh != null)
                                                            {
                                                                diaChi.QuanHuyen = qh;
                                                                if (!string.IsNullOrEmpty(txt_XaPhuong))
                                                                {
                                                                    XaPhuong xp = uow.FindObject<XaPhuong>(CriteriaOperator.Parse("QuanHuyen = ? and                                       TenXaPhuong like ?", qh, "%" + txt_XaPhuong + "%"));
                                                                    if (xp != null)
                                                                    {
                                                                        diaChi.XaPhuong = xp;
                                                                    }
                                                                }
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

                                                hsxt.ThongTinKhachHang.DiaChiThuongTru = diaChi;
                                            }
                                        }
                                        //
                                        hsxt.DiaChiThuongTru = hsxt.ThongTinKhachHang.DiaChiThuongTru;
                                        hsxt.NoiSinh = txt_NoiSinh;
                                        TonGiao tg = uow.FindObject<TonGiao>(CriteriaOperator.Parse("TenTonGiao = ?", txt_TonGiao));
                                        if (tg != null)
                                            hsxt.TonGiao = tg;
                                        DanToc danToc = uow.FindObject<DanToc>(CriteriaOperator.Parse("TenDanToc = ?", txt_DanToc));
                                        if (danToc != null)
                                            hsxt.DanToc = danToc;
                                        LoaiTruong loaiTruong = uow.FindObject<LoaiTruong>(CriteriaOperator.Parse("TenLoaiTruong = ?", txt_LoaiTruong));
                                        if (loaiTruong != null)
                                            hsxt.LoaiTruong = loaiTruong;
                                        NamHoc namHoc = uow.FindObject<NamHoc>(CriteriaOperator.Parse("TenNamHoc = ?", txt_NamHoc));
                                        if (namHoc != null)
                                            hsxt.NamHoc = namHoc;
                                        SecuritySystemUser_Custom user = uow.FindObject<SecuritySystemUser_Custom>(CriteriaOperator.Parse("UserName = ?", txt_NguoiLap));

                                        if (user != null)
                                            hsxt.SecuritySystemUser = user;
                                        // update hồ sơ
                                        if (hsxt.HoSoList != null && hsxt.HoSoList.Count > 0)
                                        {
                                            foreach (var item in hsxt.HoSoList)
                                            {
                                                if (!string.IsNullOrEmpty(txt_HocBaTH) && txt_HocBaTH.Equals("X"))
                                                {
                                                    if (item.HoSoTuyenSinh.TenHoSo.Contains("Học bạ tiểu học"))
                                                    {
                                                        item.DaNopHoSo = true;
                                                        item.BangChinh = true;
                                                    }
                                                }

                                                if (!string.IsNullOrEmpty(txt_HocBaTHCS) && txt_HocBaTHCS.Equals("X"))
                                                {
                                                    if (item.HoSoTuyenSinh.TenHoSo.Contains("Học bạ lớp 6"))
                                                    {
                                                        item.DaNopHoSo = true;
                                                        item.BangChinh = true;
                                                    }
                                                    else if (item.HoSoTuyenSinh.TenHoSo.Contains("Học bạ trung học cơ sở"))
                                                    {
                                                        item.DaNopHoSo = true;
                                                        item.BangChinh = true;
                                                    }

                                                }

                                                if (!string.IsNullOrEmpty(txt_HocBaTHPT) && txt_HocBaTHPT.Equals("X"))
                                                {
                                                    if (item.HoSoTuyenSinh.TenHoSo.Contains("Học bạ lớp 10"))
                                                    {
                                                        item.DaNopHoSo = true;
                                                        item.BangChinh = true;
                                                    }
                                                    else if (item.HoSoTuyenSinh.TenHoSo.Contains("Học bạ lớp 11"))
                                                    {
                                                        item.DaNopHoSo = true;
                                                        item.BangChinh = true;
                                                    }
                                                }

                                                if (!string.IsNullOrEmpty(txt_GiayKhaiSinh) && txt_GiayKhaiSinh.Equals("X"))
                                                {
                                                    if (item.HoSoTuyenSinh.TenHoSo.Contains("Giấy khai sinh"))
                                                    {
                                                        item.DaNopHoSo = true;
                                                        item.BangChinh = true;
                                                    }
                                                }

                                                if (!string.IsNullOrEmpty(txt_GiayHTCTTH) && txt_GiayHTCTTH.Equals("X"))
                                                {
                                                    if (item.HoSoTuyenSinh.TenHoSo.Contains("Giấy chứng nhận hoàn thành chương trình tiểu học"))
                                                    {
                                                        item.DaNopHoSo = true;
                                                        item.BangChinh = true;
                                                    }
                                                }

                                                if (!string.IsNullOrEmpty(txt_GiayCNTNTHCSTamThoi) && txt_GiayCNTNTHCSTamThoi.Equals("X"))
                                                {
                                                    if (item.HoSoTuyenSinh.TenHoSo.Contains("Giấy chứng nhận tốt nghiệp tạm thời"))
                                                    {
                                                        item.DaNopHoSo = true;
                                                        item.BangChinh = true;
                                                    }
                                                }

                                                if (!string.IsNullOrEmpty(txt_BangTotNghiep) && txt_BangTotNghiep.Equals("X"))
                                                {
                                                    if (item.HoSoTuyenSinh.TenHoSo.Contains("Bằng tốt nghiệp THCS"))
                                                    {
                                                        item.DaNopHoSo = true;
                                                        item.BangChinh = true;
                                                    }
                                                }

                                                if (!string.IsNullOrEmpty(txt_GiayNgheTHCS) && txt_GiayNgheTHCS.Equals("X"))
                                                {
                                                    if (item.HoSoTuyenSinh.TenHoSo.Contains("Giấy nghề THCS"))
                                                    {
                                                        item.DaNopHoSo = true;
                                                        item.BangChinh = true;
                                                    }
                                                }

                                                if (!string.IsNullOrEmpty(txt_GiayTrungTuyen10) && txt_GiayTrungTuyen10.Equals("X"))
                                                {
                                                    if (item.HoSoTuyenSinh.TenHoSo.Contains("Giấy trúng tuyển lớp 10"))
                                                    {
                                                        item.DaNopHoSo = true;
                                                        item.BangChinh = true;
                                                    }
                                                }

                                                if (!string.IsNullOrEmpty(txt_GiayNgheTHPT) && txt_GiayNgheTHPT.Equals("X"))
                                                {
                                                    if (item.HoSoTuyenSinh.TenHoSo.Contains("Nghề phổ thông"))
                                                    {
                                                        item.DaNopHoSo = true;
                                                        item.BangChinh = true;
                                                    }
                                                }

                                                if (!string.IsNullOrEmpty(txt_GiayGTCT) && txt_GiayGTCT.Equals("X"))
                                                {
                                                    if (item.HoSoTuyenSinh.TenHoSo.Contains("Giấy giới thiệu chuyển trường"))
                                                    {
                                                        item.DaNopHoSo = true;
                                                        item.BangChinh = true;
                                                    }
                                                }
                                            }
                                        }

                                        // kết quá cuối năm
                                        if (hsxt.KetQuaList != null && hsxt.KetQuaList.Count > 0)
                                        {
                                            foreach (var kq in hsxt.KetQuaList)
                                            {

                                                //Khối 6
                                                if (!string.IsNullOrEmpty(txt_TBM6) && "txt_TBM6".Contains(kq.KhoiSIS))
                                                {
                                                    kq.DiemTrungBinh = Convert.ToDecimal(txt_TBM6);
                                                }
                                                if (!string.IsNullOrEmpty(txt_XLHL6) && "txt_XLHL6".Contains(kq.KhoiSIS))
                                                {
                                                    if (txt_XLHL6.Equals("Khá"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.Kha;
                                                    else if (txt_XLHL6.Equals("Giỏi"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.Gioi;
                                                    else if (txt_XLHL6.Equals("TB") || txt_XLHL6.Equals("Trung bình"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.TrungBinh;
                                                    else if (txt_XLHL6.Equals("Yếu"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.Yeu;
                                                }
                                                if (!string.IsNullOrEmpty(txt_XLHK6) && "txt_XLHK6".Contains(kq.KhoiSIS))
                                                {
                                                    if (txt_XLHK6.Equals("Khá"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.Kha;
                                                    else if (txt_XLHK6.Equals("Tốt"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.Tot;
                                                    else if (txt_XLHK6.Equals("TB") || txt_XLHK6.Equals("Trung bình"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.TrungBinh;
                                                    else if (txt_XLHK6.Equals("Yếu"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.Yeu;
                                                }

                                                //Khối 7
                                                if (!string.IsNullOrEmpty(txt_TBM7) && "txt_TBM7".Contains(kq.KhoiSIS))
                                                {
                                                    kq.DiemTrungBinh = Convert.ToDecimal(txt_TBM7);

                                                }
                                                if (!string.IsNullOrEmpty(txt_XLHL7) && "txt_XLHL7".Contains(kq.KhoiSIS))
                                                {
                                                    if (txt_XLHL7.Equals("Khá"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.Kha;
                                                    else if (txt_XLHL7.Equals("Giỏi"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.Gioi;
                                                    else if (txt_XLHL7.Equals("TB") || txt_XLHL7.Equals("Trung bình"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.TrungBinh;
                                                    else if (txt_XLHL7.Equals("Yếu"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.Yeu;
                                                }
                                                if (!string.IsNullOrEmpty(txt_XLHK7) && "txt_XLHK7".Contains(kq.KhoiSIS))
                                                {
                                                    if (txt_XLHK7.Equals("Khá"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.Kha;
                                                    else if (txt_XLHK7.Equals("Tốt"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.Tot;
                                                    else if (txt_XLHK7.Equals("TB") || txt_XLHK7.Equals("Trung bình"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.TrungBinh;
                                                    else if (txt_XLHK7.Equals("Yếu"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.Yeu;
                                                }

                                                //Khối 8
                                                if (!string.IsNullOrEmpty(txt_TBM8) && "txt_TBM8".Contains(kq.KhoiSIS))
                                                {
                                                    kq.DiemTrungBinh = Convert.ToDecimal(txt_TBM8);

                                                }
                                                if (!string.IsNullOrEmpty(txt_XLHL8) && "txt_XLHL8".Contains(kq.KhoiSIS))
                                                {
                                                    if (txt_XLHL8.Equals("Khá"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.Kha;
                                                    else if (txt_XLHL8.Equals("Giỏi"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.Gioi;
                                                    else if (txt_XLHL8.Equals("TB") || txt_XLHL8.Equals("Trung bình"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.TrungBinh;
                                                    else if (txt_XLHL8.Equals("Yếu"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.Yeu;
                                                }
                                                if (!string.IsNullOrEmpty(txt_XLHK8) && "txt_XLHK8".Contains(kq.KhoiSIS))
                                                {
                                                    if (txt_XLHK8.Equals("Khá"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.Kha;
                                                    else if (txt_XLHK8.Equals("Tốt"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.Tot;
                                                    else if (txt_XLHK8.Equals("TB") || txt_XLHK8.Equals("Trung bình"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.TrungBinh;
                                                    else if (txt_XLHK8.Equals("Yếu"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.Yeu;
                                                }


                                                //Khối 9
                                                if (!string.IsNullOrEmpty(txt_TBM9) && "txt_TBM9".Contains(kq.KhoiSIS))
                                                {
                                                    kq.DiemTrungBinh = Convert.ToDecimal(txt_TBM9);

                                                }
                                                if (!string.IsNullOrEmpty(txt_XLHL9) && "txt_XLHL9".Contains(kq.KhoiSIS))
                                                {
                                                    if (txt_XLHL9.Equals("Khá"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.Kha;
                                                    else if (txt_XLHL9.Equals("Giỏi"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.Gioi;
                                                    else if (txt_XLHL9.Equals("TB") || txt_XLHL9.Equals("Trung bình"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.TrungBinh;
                                                    else if (txt_XLHL9.Equals("Yếu"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.Yeu;
                                                }
                                                if (!string.IsNullOrEmpty(txt_XLHK9) && "txt_XLHK9".Contains(kq.KhoiSIS))
                                                {
                                                    if (txt_XLHK9.Equals("Khá"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.Kha;
                                                    else if (txt_XLHK9.Equals("Tốt"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.Tot;
                                                    else if (txt_XLHK9.Equals("TB") || txt_XLHK9.Equals("Trung bình"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.TrungBinh;
                                                    else if (txt_XLHK9.Equals("Yếu"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.Yeu;
                                                }


                                                //Khối 10
                                                if (!string.IsNullOrEmpty(txt_TBM10) && "txt_TBM10".Contains(kq.KhoiSIS))
                                                {
                                                    kq.DiemTrungBinh = Convert.ToDecimal(txt_TBM10);

                                                }
                                                if (!string.IsNullOrEmpty(txt_XLHL10) && "txt_XLHL10".Contains(kq.KhoiSIS))
                                                {
                                                    if (txt_XLHL10.Equals("Khá"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.Kha;
                                                    else if (txt_XLHL10.Equals("Giỏi"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.Gioi;
                                                    else if (txt_XLHL10.Equals("TB") || txt_XLHL10.Equals("Trung bình"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.TrungBinh;
                                                    else if (txt_XLHL10.Equals("Yếu"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.Yeu;
                                                }
                                                if (!string.IsNullOrEmpty(txt_XLHK10) && "txt_XLHK10".Contains(kq.KhoiSIS))
                                                {
                                                    if (txt_XLHK10.Equals("Khá"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.Kha;
                                                    else if (txt_XLHK10.Equals("Tốt"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.Tot;
                                                    else if (txt_XLHK10.Equals("TB") || txt_XLHK10.Equals("Trung bình"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.TrungBinh;
                                                    else if (txt_XLHK10.Equals("Yếu"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.Yeu;
                                                }


                                                //Khối 11
                                                if (!string.IsNullOrEmpty(txt_TBM11) && "txt_TBM11".Contains(kq.KhoiSIS))
                                                {
                                                    kq.DiemTrungBinh = Convert.ToDecimal(txt_TBM11);

                                                }
                                                if (!string.IsNullOrEmpty(txt_XLHL11) && "txt_XLHL11".Contains(kq.KhoiSIS))
                                                {
                                                    if (txt_XLHL11.Equals("Khá"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.Kha;
                                                    else if (txt_XLHL11.Equals("Giỏi"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.Gioi;
                                                    else if (txt_XLHL11.Equals("TB") || txt_XLHL11.Equals("Trung bình"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.TrungBinh;
                                                    else if (txt_XLHL11.Equals("Yếu"))
                                                        kq.HocLuc = XepLoaiTotNghiepEnum.Yeu;
                                                }
                                                if (!string.IsNullOrEmpty(txt_XLHK11) && "txt_XLHK11".Contains(kq.KhoiSIS))
                                                {
                                                    if (txt_XLHK11.Equals("Khá"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.Kha;
                                                    else if (txt_XLHK11.Equals("Tốt"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.Tot;
                                                    else if (txt_XLHK11.Equals("TB") || txt_XLHK11.Equals("Trung bình"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.TrungBinh;
                                                    else if (txt_XLHK11.Equals("Yếu"))
                                                        kq.HanhKiem = XepLoaiHanhKiemEnum.Yeu;
                                                }
                                            }
                                        }

                                        // update điểm trung bình nếu đã có tạo môn
                                        if (hsxt.ListDTB != null && hsxt.ListDTB.Count > 0)
                                        {
                                            foreach (var diemTB in hsxt.ListDTB)
                                            {
                                                //XPCollection<DanhMucMonXetTuyen> mon = new XPCollection<DanhMucMonXetTuyen>(CriteriaOperator.Parse("KhoiSIS = ? And CongTy = ?", hsxt.KhoiSIS, hsxt.CongTy));
                                                // Toán
                                                if (diemTB.Mon.TenMonHoc.Equals("Toán") && !string.IsNullOrEmpty(txt_Toan6))
                                                {
                                                    diemTB.DiemTrungBinh6 = Convert.ToDecimal(txt_Toan6);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Toán") && !string.IsNullOrEmpty(txt_Toan7))
                                                {
                                                    diemTB.DiemTrungBinh7 = Convert.ToDecimal(txt_Toan7);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Toán") && !string.IsNullOrEmpty(txt_Toan8))
                                                {
                                                    diemTB.DiemTrungBinh8 = Convert.ToDecimal(txt_Toan8);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Toán") && !string.IsNullOrEmpty(txt_Toan9))
                                                {
                                                    diemTB.DiemTrungBinh9 = Convert.ToDecimal(txt_Toan9);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Toán") && !string.IsNullOrEmpty(txt_Toan10))
                                                {
                                                    diemTB.DiemTrungBinh10 = Convert.ToDecimal(txt_Toan10);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Toán") && !string.IsNullOrEmpty(txt_Toan11))
                                                {
                                                    diemTB.DiemTrungBinh11 = Convert.ToDecimal(txt_Toan11);
                                                }


                                                //Lý
                                                if (diemTB.Mon.TenMonHoc.Equals("Vật Lý") && !string.IsNullOrEmpty(txt_Ly6))
                                                {
                                                    diemTB.DiemTrungBinh6 = Convert.ToDecimal(txt_Ly6);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Vật Lý") && !string.IsNullOrEmpty(txt_Ly7))
                                                {
                                                    diemTB.DiemTrungBinh7 = Convert.ToDecimal(txt_Ly7);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Vật Lý") && !string.IsNullOrEmpty(txt_Ly8))
                                                {
                                                    diemTB.DiemTrungBinh8 = Convert.ToDecimal(txt_Ly8);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Vật Lý") && !string.IsNullOrEmpty(txt_Ly9))
                                                {
                                                    diemTB.DiemTrungBinh9 = Convert.ToDecimal(txt_Ly9);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Vật Lý") && !string.IsNullOrEmpty(txt_Ly10))
                                                {
                                                    diemTB.DiemTrungBinh10 = Convert.ToDecimal(txt_Ly10);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Vật Lý") && !string.IsNullOrEmpty(txt_Ly11))
                                                {
                                                    diemTB.DiemTrungBinh11 = Convert.ToDecimal(txt_Ly11);
                                                }
                                                //Hóa
                                                if (diemTB.Mon.TenMonHoc.Equals("Hóa Học") && !string.IsNullOrEmpty(txt_Hoa8))
                                                {
                                                    diemTB.DiemTrungBinh8 = Convert.ToDecimal(txt_Hoa8);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Hóa Học") && !string.IsNullOrEmpty(txt_Hoa9))
                                                {
                                                    diemTB.DiemTrungBinh9 = Convert.ToDecimal(txt_Hoa9);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Hóa Học") && !string.IsNullOrEmpty(txt_Hoa10))
                                                {
                                                    diemTB.DiemTrungBinh10 = Convert.ToDecimal(txt_Hoa10);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Hóa Học") && !string.IsNullOrEmpty(txt_Hoa11))
                                                {
                                                    diemTB.DiemTrungBinh11 = Convert.ToDecimal(txt_Hoa11);
                                                }
                                                // văn
                                                if (diemTB.Mon.TenMonHoc.Equals("Ngữ Văn") && !string.IsNullOrEmpty(txt_Van6))
                                                {
                                                    diemTB.DiemTrungBinh6 = Convert.ToDecimal(txt_Van6);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Ngữ Văn") && !string.IsNullOrEmpty(txt_Van7))
                                                {
                                                    diemTB.DiemTrungBinh7 = Convert.ToDecimal(txt_Van7);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Ngữ Văn") && !string.IsNullOrEmpty(txt_Van8))
                                                {
                                                    diemTB.DiemTrungBinh8 = Convert.ToDecimal(txt_Van8);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Ngữ Văn") && !string.IsNullOrEmpty(txt_Van9))
                                                {
                                                    diemTB.DiemTrungBinh9 = Convert.ToDecimal(txt_Van9);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Ngữ Văn") && !string.IsNullOrEmpty(txt_Van10))
                                                {
                                                    diemTB.DiemTrungBinh10 = Convert.ToDecimal(txt_Van10);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Ngữ Văn") && !string.IsNullOrEmpty(txt_Van11))
                                                {
                                                    diemTB.DiemTrungBinh11 = Convert.ToDecimal(txt_Van11);
                                                }
                                                // Anh văn
                                                if (diemTB.Mon.TenMonHoc.Equals("Anh Văn") && !string.IsNullOrEmpty(txt_Anh6))
                                                {
                                                    diemTB.DiemTrungBinh6 = Convert.ToDecimal(txt_Anh6);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Anh Văn") && !string.IsNullOrEmpty(txt_Anh7))
                                                {
                                                    diemTB.DiemTrungBinh7 = Convert.ToDecimal(txt_Anh7);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Anh Văn") && !string.IsNullOrEmpty(txt_Anh8))
                                                {
                                                    diemTB.DiemTrungBinh8 = Convert.ToDecimal(txt_Anh8);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Anh Văn") && !string.IsNullOrEmpty(txt_Anh9))
                                                {
                                                    diemTB.DiemTrungBinh9 = Convert.ToDecimal(txt_Anh9);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Anh Văn") && !string.IsNullOrEmpty(txt_Anh10))
                                                {
                                                    diemTB.DiemTrungBinh10 = Convert.ToDecimal(txt_Anh10);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Anh Văn") && !string.IsNullOrEmpty(txt_Anh11))
                                                {
                                                    diemTB.DiemTrungBinh11 = Convert.ToDecimal(txt_Anh11);
                                                }
                                                // Sinh Học
                                                if (diemTB.Mon.TenMonHoc.Equals("Sinh Học") && !string.IsNullOrEmpty(txt_Sinh6))
                                                {
                                                    diemTB.DiemTrungBinh6 = Convert.ToDecimal(txt_Sinh6);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Sinh Học") && !string.IsNullOrEmpty(txt_Sinh7))
                                                {
                                                    diemTB.DiemTrungBinh7 = Convert.ToDecimal(txt_Sinh7);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Sinh Học") && !string.IsNullOrEmpty(txt_Sinh8))
                                                {
                                                    diemTB.DiemTrungBinh8 = Convert.ToDecimal(txt_Sinh8);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Sinh Học") && !string.IsNullOrEmpty(txt_Sinh9))
                                                {
                                                    diemTB.DiemTrungBinh9 = Convert.ToDecimal(txt_Sinh9);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Sinh Học") && !string.IsNullOrEmpty(txt_Sinh10))
                                                {
                                                    diemTB.DiemTrungBinh10 = Convert.ToDecimal(txt_Sinh10);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Sinh Học") && !string.IsNullOrEmpty(txt_Sinh11))
                                                {
                                                    diemTB.DiemTrungBinh11 = Convert.ToDecimal(txt_Sinh11);
                                                }
                                            }
                                        }
                                        // Nếu chưa tạo môn thì tạo và update điểm
                                        else
                                        {
                                            hsxt.CreateDiemTB();
                                            foreach (var diemTB in hsxt.ListDTB)
                                            {
                                                //XPCollection<DanhMucMonXetTuyen> mon = new XPCollection<DanhMucMonXetTuyen>(CriteriaOperator.Parse("KhoiSIS = ? And CongTy = ?", hsxt.KhoiSIS, hsxt.CongTy));
                                                // Toán
                                                if (diemTB.Mon.TenMonHoc.Equals("Toán") && !string.IsNullOrEmpty(txt_Toan6))
                                                {
                                                    diemTB.DiemTrungBinh6 = Convert.ToDecimal(txt_Toan6);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Toán") && !string.IsNullOrEmpty(txt_Toan7))
                                                {
                                                    diemTB.DiemTrungBinh7 = Convert.ToDecimal(txt_Toan7);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Toán") && !string.IsNullOrEmpty(txt_Toan8))
                                                {
                                                    diemTB.DiemTrungBinh8 = Convert.ToDecimal(txt_Toan8);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Toán") && !string.IsNullOrEmpty(txt_Toan9))
                                                {
                                                    diemTB.DiemTrungBinh9 = Convert.ToDecimal(txt_Toan9);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Toán") && !string.IsNullOrEmpty(txt_Toan10))
                                                {
                                                    diemTB.DiemTrungBinh10 = Convert.ToDecimal(txt_Toan10);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Toán") && !string.IsNullOrEmpty(txt_Toan11))
                                                {
                                                    diemTB.DiemTrungBinh11 = Convert.ToDecimal(txt_Toan11);
                                                }


                                                //Lý
                                                if (diemTB.Mon.TenMonHoc.Equals("Vật Lý") && !string.IsNullOrEmpty(txt_Ly6))
                                                {
                                                    diemTB.DiemTrungBinh6 = Convert.ToDecimal(txt_Ly6);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Vật Lý") && !string.IsNullOrEmpty(txt_Ly7))
                                                {
                                                    diemTB.DiemTrungBinh7 = Convert.ToDecimal(txt_Ly7);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Vật Lý") && !string.IsNullOrEmpty(txt_Ly8))
                                                {
                                                    diemTB.DiemTrungBinh8 = Convert.ToDecimal(txt_Ly8);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Vật Lý") && !string.IsNullOrEmpty(txt_Ly9))
                                                {
                                                    diemTB.DiemTrungBinh9 = Convert.ToDecimal(txt_Ly9);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Vật Lý") && !string.IsNullOrEmpty(txt_Ly10))
                                                {
                                                    diemTB.DiemTrungBinh10 = Convert.ToDecimal(txt_Ly10);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Vật Lý") && !string.IsNullOrEmpty(txt_Ly11))
                                                {
                                                    diemTB.DiemTrungBinh11 = Convert.ToDecimal(txt_Ly11);
                                                }
                                                //Hóa
                                                if (diemTB.Mon.TenMonHoc.Equals("Hóa Học") && !string.IsNullOrEmpty(txt_Hoa8))
                                                {
                                                    diemTB.DiemTrungBinh8 = Convert.ToDecimal(txt_Hoa8);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Hóa Học") && !string.IsNullOrEmpty(txt_Hoa9))
                                                {
                                                    diemTB.DiemTrungBinh9 = Convert.ToDecimal(txt_Hoa9);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Hóa Học") && !string.IsNullOrEmpty(txt_Hoa10))
                                                {
                                                    diemTB.DiemTrungBinh10 = Convert.ToDecimal(txt_Hoa10);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Hóa Học") && !string.IsNullOrEmpty(txt_Hoa11))
                                                {
                                                    diemTB.DiemTrungBinh11 = Convert.ToDecimal(txt_Hoa11);
                                                }
                                                // văn
                                                if (diemTB.Mon.TenMonHoc.Equals("Ngữ Văn") && !string.IsNullOrEmpty(txt_Van6))
                                                {
                                                    diemTB.DiemTrungBinh6 = Convert.ToDecimal(txt_Van6);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Ngữ Văn") && !string.IsNullOrEmpty(txt_Van7))
                                                {
                                                    diemTB.DiemTrungBinh7 = Convert.ToDecimal(txt_Van7);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Ngữ Văn") && !string.IsNullOrEmpty(txt_Van8))
                                                {
                                                    diemTB.DiemTrungBinh8 = Convert.ToDecimal(txt_Van8);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Ngữ Văn") && !string.IsNullOrEmpty(txt_Van9))
                                                {
                                                    diemTB.DiemTrungBinh9 = Convert.ToDecimal(txt_Van9);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Ngữ Văn") && !string.IsNullOrEmpty(txt_Van10))
                                                {
                                                    diemTB.DiemTrungBinh10 = Convert.ToDecimal(txt_Van10);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Ngữ Văn") && !string.IsNullOrEmpty(txt_Van11))
                                                {
                                                    diemTB.DiemTrungBinh11 = Convert.ToDecimal(txt_Van11);
                                                }
                                                // Anh văn
                                                if (diemTB.Mon.TenMonHoc.Equals("Anh Văn") && !string.IsNullOrEmpty(txt_Anh6))
                                                {
                                                    diemTB.DiemTrungBinh6 = Convert.ToDecimal(txt_Anh6);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Anh Văn") && !string.IsNullOrEmpty(txt_Anh7))
                                                {
                                                    diemTB.DiemTrungBinh7 = Convert.ToDecimal(txt_Anh7);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Anh Văn") && !string.IsNullOrEmpty(txt_Anh8))
                                                {
                                                    diemTB.DiemTrungBinh8 = Convert.ToDecimal(txt_Anh8);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Anh Văn") && !string.IsNullOrEmpty(txt_Anh9))
                                                {
                                                    diemTB.DiemTrungBinh9 = Convert.ToDecimal(txt_Anh9);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Anh Văn") && !string.IsNullOrEmpty(txt_Anh10))
                                                {
                                                    diemTB.DiemTrungBinh10 = Convert.ToDecimal(txt_Anh10);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Anh Văn") && !string.IsNullOrEmpty(txt_Anh11))
                                                {
                                                    diemTB.DiemTrungBinh11 = Convert.ToDecimal(txt_Anh11);
                                                }
                                                // Sinh Học
                                                if (diemTB.Mon.TenMonHoc.Equals("Sinh Học") && !string.IsNullOrEmpty(txt_Sinh6))
                                                {
                                                    diemTB.DiemTrungBinh6 = Convert.ToDecimal(txt_Sinh6);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Sinh Học") && !string.IsNullOrEmpty(txt_Sinh7))
                                                {
                                                    diemTB.DiemTrungBinh7 = Convert.ToDecimal(txt_Sinh7);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Sinh Học") && !string.IsNullOrEmpty(txt_Sinh8))
                                                {
                                                    diemTB.DiemTrungBinh8 = Convert.ToDecimal(txt_Sinh8);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Sinh Học") && !string.IsNullOrEmpty(txt_Sinh9))
                                                {
                                                    diemTB.DiemTrungBinh9 = Convert.ToDecimal(txt_Sinh9);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Sinh Học") && !string.IsNullOrEmpty(txt_Sinh10))
                                                {
                                                    diemTB.DiemTrungBinh10 = Convert.ToDecimal(txt_Sinh10);
                                                }
                                                if (diemTB.Mon.TenMonHoc.Equals("Sinh Học") && !string.IsNullOrEmpty(txt_Sinh11))
                                                {
                                                    diemTB.DiemTrungBinh11 = Convert.ToDecimal(txt_Sinh11);
                                                }
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(txt_ChinhSachMienGiam))
                                        {
                                            string maChinhSach = KiemTraChinhSach(txt_ChinhSachMienGiam);

                                            if (!string.IsNullOrEmpty(maChinhSach))
                                            {
                                                HoSoXetTuyen_ChinhSachMienGiam chinhsach = new HoSoXetTuyen_ChinhSachMienGiam(uow);
                                                chinhsach.TenChinhSach = txt_ChinhSachMienGiam;
                                                chinhsach.MaChinhSach = maChinhSach;
                                                chinhsach.HoSoXetTuyen = hsxt;
                                                hsxt.ListChinhSachMienGiam.Add(chinhsach);
                                            }
                                        }
                                        detailLog.AppendLine("      + Đã có hồ sơ xét tuyển trong hệ thống với mã là: " + hsxt.MaXetTuyen);
                                        detailLog.AppendLine("      + Đã update điểm vào hồ sơ có mã: " + hsxt.MaXetTuyen);
                                    }
                                }

                                else
                                {
                                    detailLog.AppendLine("      + Import không thành công vì tìm thấy thông tin Khối học");
                                }
                                //Đưa thông tin bị lỗi vào blog
                                if (detailLog.Length > 0)
                                {
                                    mainLog.AppendLine(string.Format("- Không import vào hệ thống học sinh: [{0}] , Ngày sinh: ", txt_HoHS + " " + txt_TenHS, dt_NgaySinh.ToShortDateString()));
                                    mainLog.AppendLine(detailLog.ToString());
                                    //
                                    //sucessImport = false;
                                }
                            }
                            else
                            {
                                detailLog.AppendLine("      + Import không thành công vì đủ dữ liệu cơ bản về học sinh (Họ , Tên, Ngày sinh,  Giới tinh).");
                            }
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
        }
                #endregion

        private static string KiemTraChinhSach(string TenChinhSach)
        {
            string maChinhSach = "";
            string _connect = DataProvider.GetConnectionString();
            var query = "";
            if (_connect.Contains(Config.KeyServerMamNon))
                query = "SELECT MaChinhSach, TenChinhSach from " + Config.KeyLinkServer + ".AccountsFee.dbo.tblChinhSach where TenChinhSach like N'%" + TenChinhSach + "%' and TenChinhSach != ''";
            else
                query = "SELECT MaChinhSach, TenChinhSach from AccountsFee.dbo.tblChinhSach where TenChinhSach like N'%" + TenChinhSach + "%' and TenChinhSach != ''";
            //
            using (DataTable dt = DataProvider.GetDataTable(query, CommandType.Text))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        maChinhSach = item["MaChinhSach"].ToString();
                    }
                }
            }
            return maChinhSach;
        }
    }
}
