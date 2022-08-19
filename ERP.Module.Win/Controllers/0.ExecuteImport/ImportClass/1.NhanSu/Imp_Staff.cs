using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.Extends;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.Enum.NhanSu;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.NhanSu
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

    public class Imp_Staff
    {
        #region 1. Nhập hồ sơ nhân viên từ tập tin excel
        public static void ImportStaff(IObjectSpace obs, NhanVien_ChonBoPhan obj)
        {
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            var mainLog = new StringBuilder();
            //
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel 2003 file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[ThongTinNhanVien$A4:CW]", obj.LoaiOffice))
                        {

                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx
                            //
                            #region Hồ sơ 0 --> 27
                            const int idxSTT = 0;
                            const int idxMaQuanLy = 1;
                            const int idxHo = 2;
                            const int idxTen = 3;
                            const int idxTenGoiKhac = 4;
                            const int idxNgaySinh = 5;
                            const int idxNoiSinh = 6;
                            const int idxGioiTinh = 7;
                            const int idxCMND = 8;
                            const int idxNgayCapCMND = 9;
                            const int idxNoiCapCMND = 10;
                            const int idxSoHoChieu = 11;
                            const int idxNgayCapHoChieu = 12;
                            const int idxNoiCapHoChieu = 13;
                            const int idxNgayHetHanHoChieu = 14;
                            const int idxQueQuan = 15;
                            const int idxDiaChiThuongTru = 16;
                            const int idxNoiOHienNay = 17;
                            const int idxEmail = 18;
                            const int idxDienThoaiDiDong = 19;
                            const int idxDienThoaiNhaRieng = 20;
                            const int idxTinhTrangHonNhan = 21;
                            const int idxDanToc = 22;
                            const int idxTonGiao = 23;
                            const int idxQuocTich = 24;
                            const int idxThanhPhanXuatThan = 25;
                            const int idxUuTienGiaDinh = 26;
                            const int idxUuTienBanThan = 27;
                            #endregion
                            //
                            #region Nhân viên 28 -->46
                            const int idxChucDanhCongViec = 28;
                            const int idxDonVi = 29;
                            const int idxCongViecHienNay = 30;
                            const int idxNgayTuyenDung = 31;
                            const int idxCoQuanTuyenDung = 32;
                            const int idxCongViecTuyenDung = 33;
                            const int idxNgayVaoCongTy = 34;
                            const int idxTinhTrang = 35;
                            const int idxChucVu = 36;
                            const int idxChucVuKiemNhiem = 37;
                            const int idxNgayBoNhiem = 38;
                            const int idxNgayBoNhiemKiemNhiem = 39;
                            const int idxLoaiHopDong = 40;
                            const int idxLoaiNhanSu = 41;
                            const int idxngayVaoTapDoan = 42;
                            const int idxNhomMau = 43;
                            const int idxChieuCao = 44;
                            const int idxCanNang = 45;
                            const int idxSucKhoe = 46;
                            #endregion
                            //
                            #region Thông tin lương 47 --> 67
                            const int idxMaNgach = 47;
                            const int idxTenNgach = 48;
                            const int idxNgayBoNhiemNgach = 49;
                            const int idxNgayHuongLuong = 50;
                            const int idxBacLuong = 51;
                            const int idxHeSoLuong = 52;
                            const int idxLuongCoBan = 53;
                            const int idxLuongKinhDoanh = 54;
                            const int idxHSPCChucVu = 55;
                            const int idxHSPCKiemNhiem = 56;
                            const int idxHSPCTrachNhiem = 57;
                            const int idxHSPCDoan = 58;
                            const int idxHSPCDang = 59;
                            const int idxMocNangLuongLanSau = 60;
                            const int idxMaSoThue = 61;
                            const int idxSoSoBHXH = 62;
                            const int idxNgayBatDauDongBHXH = 63;
                            const int idxSoTheBHYT = 64;
                            const int idxTuNgayBH = 65;
                            const int idxDenNgayBH = 66;
                            const int idxNoiDangKyKhamChuaBenh = 67;
                            #endregion
                            //
                            #region Thông tin trình độ 68 --> 92
                            const int idxTrinhDoVanHoa = 68;
                            const int idxChuyenNganhDaoTao_TrungHoc = 69;
                            const int idxNoiDaoTao_TrungHoc = 70;
                            const int idxHinhThucDaoTao_TrungHoc = 71;
                            const int idxNamTotNghiep_TrungHoc = 72;
                            const int idxChuyenNganhDaoTao_CaoDang = 73;
                            const int idxNoiDaoTao_CaoDang = 74;
                            const int idxHinhThucDaoTao_CaoDang = 75;
                            const int idxNamTotNghiep_CaoDang = 76;
                            const int idxChuyenNganhDaoTao_DaiHoc = 77;
                            const int idxNoiDaoTao_DaiHoc = 78;
                            const int idxHinhThucDaoTao_DaiHoc = 79;
                            const int idxNamTotNghiep_DaiHoc = 80;
                            const int idxChuyenNganhDaoTao_ThacSi = 81;
                            const int idxNoiDaoTao_ThacSi = 82;
                            const int idxHinhThucDaoTao_ThacSi = 83;
                            const int idxNamTotNghiep_ThacSi = 84;
                            const int idxChuyenNganhDaoTao_TienSi = 85;
                            const int idxNoiDaoTao_TienSi = 86;
                            const int idxHinhThucDaoTao_TienSi = 87;
                            const int idxNamTotNghiep_TienSi = 88;
                            const int idxTrinhDoCaoNhat = 89;
                            const int idxTrinhTinHoc = 90;
                            const int idxNgoaiNgu = 91;
                            const int idxTrinhNgoaiNgu = 92;
                            #endregion
                            //
                            #region Đoàn - đảng 93 --> 96

                            const int idxNgayVaoDoan = 93;
                            const int idxChucVuDoan = 94;
                            const int idxNgayVaoDang = 95;
                            const int idxChucVuDang = 96;
                            #endregion
                            //
                            #region Tài khoản ngân hàng 97 --> 100
                            const int idxTenNganHang = 97;
                            const int idxSoTaiKhoan = 98;
                            const int idxTenNganHang2 = 99;
                            const int idxSoTaiKhoan2 = 100;
                            #endregion
                            //
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();

                                //Duyệt qua tất cả các dòng trong file excel
                                foreach (DataRow dr in dt.Rows)
                                {
                                    var errorLog = new StringBuilder();

                                    //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                    #region Đọc dữ liệu
                                    //
                                    #region Hồ sơ 0 --> 27
                                    string txtSTT = dr[idxSTT].ToString();
                                    string txtMaQuanLy = dr[idxMaQuanLy].ToString();
                                    string txtHo = dr[idxHo].ToString();
                                    string txtTen = dr[idxTen].ToString();
                                    string txtTenGoiKhac = dr[idxTenGoiKhac].ToString();
                                    string txtNgaySinh = dr[idxNgaySinh].ToString();
                                    string txtNoiSinh = dr[idxNoiSinh].ToString();
                                    string txtGioiTinh = dr[idxGioiTinh].ToString();
                                    string txtCMND = dr[idxCMND].ToString();
                                    string txtNgayCapCMND = dr[idxNgayCapCMND].ToString();
                                    string txtNoiCapCMND = dr[idxNoiCapCMND].ToString();
                                    string txtSoHoChieu = dr[idxSoHoChieu].ToString();
                                    string txtNgayCapHoChieu = dr[idxNgayCapHoChieu].ToString();
                                    string txtNoiCapHoChieu = dr[idxNoiCapHoChieu].ToString();
                                    string txtNgayHetHanHoChieu = dr[idxNgayHetHanHoChieu].ToString();
                                    string txtQueQuan = dr[idxQueQuan].ToString();
                                    string txtDiaChiThuongTru = dr[idxDiaChiThuongTru].ToString();
                                    string txtNoiOHienNay = dr[idxNoiOHienNay].ToString();
                                    string txtEmail = dr[idxEmail].ToString();
                                    string txtDienThoaiDiDong = dr[idxDienThoaiDiDong].ToString();
                                    string txtDienThoaiNhaRieng = dr[idxDienThoaiNhaRieng].ToString();
                                    string txtTinhTrangHonNhan = dr[idxTinhTrangHonNhan].ToString();
                                    string txtDanToc = dr[idxDanToc].ToString();
                                    string txtTonGiao = dr[idxTonGiao].ToString();
                                    string txtQuocTich = dr[idxQuocTich].ToString();
                                    string txtThanhPhanXuatThan = dr[idxThanhPhanXuatThan].ToString();
                                    string txtUuTienGiaDinh = dr[idxUuTienGiaDinh].ToString();
                                    string txtUuTienBanThan = dr[idxUuTienBanThan].ToString();
                                    #endregion
                                    //
                                    #region Nhân viên 28 -->46
                                    string txtChucDanhCongViec = dr[idxChucDanhCongViec].ToString();
                                    string txtDonVi = dr[idxDonVi].ToString();
                                    string txtCongViecHienNay = dr[idxCongViecHienNay].ToString();
                                    string txtNgayTuyenDung = dr[idxNgayTuyenDung].ToString();
                                    string txtCoQuanTuyenDung = dr[idxCoQuanTuyenDung].ToString();
                                    string txtCongViecTuyenDung = dr[idxCongViecTuyenDung].ToString();
                                    string txtNgayVaoCongTy = dr[idxNgayVaoCongTy].ToString();
                                    string txtTinhTrang = dr[idxTinhTrang].ToString();
                                    string txtChucVu = dr[idxChucVu].ToString();
                                    string txtChucVuKiemNhiem = dr[idxChucVuKiemNhiem].ToString();
                                    string txtNgayBoNhiem = dr[idxNgayBoNhiem].ToString();
                                    string txtNgayBoNhiemKiemNhiem = dr[idxNgayBoNhiemKiemNhiem].ToString();
                                    string txtLoaiHopDong = dr[idxLoaiHopDong].ToString();
                                    string txtLoaiNhanSu = dr[idxLoaiNhanSu].ToString();
                                    string txtNgayVaoTapDoan = dr[idxngayVaoTapDoan].ToString();
                                    string txtNhomMau = dr[idxNhomMau].ToString();
                                    string txtChieuCao = dr[idxChieuCao].ToString();
                                    string txtCanNang = dr[idxCanNang].ToString();
                                    string txtSucKhoe = dr[idxSucKhoe].ToString();
                                    #endregion
                                    //
                                    #region Thông tin lương 47 --> 65
                                    string txtMaNgach = dr[idxMaNgach].ToString();
                                    string txtTenNgach = dr[idxTenNgach].ToString();
                                    string txtNgayBoNhiemNgach = dr[idxNgayBoNhiemNgach].ToString();
                                    string txtNgayHuongLuong = dr[idxNgayHuongLuong].ToString();
                                    string txtBacLuong = dr[idxBacLuong].ToString();
                                    string txtHeSoLuong = dr[idxHeSoLuong].ToString();
                                    string txtLuongCoBan = dr[idxLuongCoBan].ToString();
                                    string txtLuongKinhDoanh = dr[idxLuongKinhDoanh].ToString();
                                    string txtHSPCChucVu = dr[idxHSPCChucVu].ToString();
                                    string txtPCKiemNhiem = dr[idxHSPCKiemNhiem].ToString();
                                    string txtPCTrachNhiem = dr[idxHSPCTrachNhiem].ToString();
                                    string txtHSPCDoan = dr[idxHSPCDoan].ToString();
                                    string txtHSPCDang = dr[idxHSPCDang].ToString();
                                    string txtMocNangLuongLanSau = dr[idxMocNangLuongLanSau].ToString();
                                    string txtMaSoThue = dr[idxMaSoThue].ToString();
                                    string txtSoSoBHXH = dr[idxSoSoBHXH].ToString();
                                    string txtNgayBatDauDongBHXH = dr[idxNgayBatDauDongBHXH].ToString();
                                    string txtSoTheBHYT = dr[idxSoTheBHYT].ToString();
                                    string txtTuNgayBH = dr[idxTuNgayBH].ToString();
                                    string txtDenNgayBH = dr[idxDenNgayBH].ToString();
                                    string txtNoiDangKyKhamChuaBenh = dr[idxNoiDangKyKhamChuaBenh].ToString();
                                    #endregion
                                    //
                                    #region Thông tin trình độ 66 --> 90
                                    string txtTrinhDoVanHoa = dr[idxTrinhDoVanHoa].ToString();
                                    string txtChuyenNganhDaoTao_TrungHoc = dr[idxChuyenNganhDaoTao_TrungHoc].ToString();
                                    string txtNoiDaoTao_TrungHoc = dr[idxNoiDaoTao_TrungHoc].ToString();
                                    string txtHinhThucDaoTao_TrungHoc = dr[idxHinhThucDaoTao_TrungHoc].ToString();
                                    string txtNamTotNghiep_TrungHoc = dr[idxNamTotNghiep_TrungHoc].ToString();
                                    string txtChuyenNganhDaoTao_CaoDang = dr[idxChuyenNganhDaoTao_CaoDang].ToString();
                                    string txtNoiDaoTao_CaoDang = dr[idxNoiDaoTao_CaoDang].ToString();
                                    string txtHinhThucDaoTao_CaoDang = dr[idxHinhThucDaoTao_CaoDang].ToString();
                                    string txtNamTotNghiep_CaoDang = dr[idxNamTotNghiep_CaoDang].ToString();
                                    string txtChuyenNganhDaoTao_DaiHoc = dr[idxChuyenNganhDaoTao_DaiHoc].ToString();
                                    string txtNoiDaoTao_DaiHoc = dr[idxNoiDaoTao_DaiHoc].ToString();
                                    string txtHinhThucDaoTao_DaiHoc = dr[idxHinhThucDaoTao_DaiHoc].ToString();
                                    string txtNamTotNghiep_DaiHoc = dr[idxNamTotNghiep_DaiHoc].ToString();
                                    string txtChuyenNganhDaoTao_ThacSi = dr[idxChuyenNganhDaoTao_ThacSi].ToString();
                                    string txtNoiDaoTao_ThacSi = dr[idxNoiDaoTao_ThacSi].ToString();
                                    string txtHinhThucDaoTao_ThacSi = dr[idxHinhThucDaoTao_ThacSi].ToString();
                                    string txtNamTotNghiep_ThacSi = dr[idxNamTotNghiep_ThacSi].ToString();
                                    string txtChuyenNganhDaoTao_TienSi = dr[idxChuyenNganhDaoTao_TienSi].ToString();
                                    string txtNoiDaoTao_TienSi = dr[idxNoiDaoTao_TienSi].ToString();
                                    string txtHinhThucDaoTao_TienSi = dr[idxHinhThucDaoTao_TienSi].ToString();
                                    string txtNamTotNghiep_TienSi = dr[idxNamTotNghiep_TienSi].ToString();
                                    string txtTrinhDoCaoNhat = dr[idxTrinhDoCaoNhat].ToString();
                                    string txtTrinhTinHoc = dr[idxTrinhTinHoc].ToString();
                                    string txtNgoaiNgu = dr[idxNgoaiNgu].ToString();
                                    string txtTrinhNgoaiNgu = dr[idxTrinhNgoaiNgu].ToString();
                                    #endregion
                                    //
                                    #region Đoàn - đảng 91 --> 94

                                    string txtNgayVaoDoan = dr[idxNgayVaoDoan].ToString();
                                    string txtChucVuDoan = dr[idxChucVuDoan].ToString();
                                    string txtNgayVaoDang = dr[idxNgayVaoDang].ToString();
                                    string txtChucVuDang = dr[idxChucVuDang].ToString();
                                    #endregion
                                    //
                                    #region Tài khoản ngân hàng 95 --> 98
                                    string txtTenNganHang = dr[idxTenNganHang].ToString();
                                    string txtSoTaiKhoan = dr[idxSoTaiKhoan].ToString();
                                    #endregion
                                    //
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////

                                    #region Kiểm tra dữ

                                    ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaTapDoan = ?", txtMaQuanLy));
                                    if (nhanVien != null)
                                    {
                                        mainLog.AppendLine("- STT: " + txtSTT);
                                        mainLog.AppendLine(string.Format("- Mã quản lý :{0} đã tồn tại trong hệ thống.", txtMaQuanLy));
                                        //
                                        sucessImport = false;
                                    }
                                    else
                                    {
                                        //Tạo mới nhân viên
                                        nhanVien = new ThongTinNhanVien(uow);

                                        //Loại hồ sơ là nhân viên                                        
                                        nhanVien.LoaiHoSo = LoaiHoSoEnum.NhanVien;
                                        //

                                        #region Hồ Sơ
                                        //
                                        #region 1. Mã nhân viên
                                        if (!string.IsNullOrEmpty(txtMaQuanLy))
                                        {
                                            nhanVien.MaTapDoan = txtMaQuanLy;
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu mã quản lý nhân viên.");
                                        }
                                        #endregion

                                        #region 2.Họ
                                        if (!string.IsNullOrEmpty(txtHo))
                                        {
                                            nhanVien.Ho = txtHo;
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu thông tin họ và tên đệm.");
                                        }
                                        #endregion

                                        #region 3. Tên
                                        if (!string.IsNullOrEmpty(txtTen))
                                        {
                                            nhanVien.Ten = txtTen;
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu thông tin tên nhân viên.");
                                        }
                                        #endregion

                                        #region 4. Tên gọi khác
                                        if (!string.IsNullOrEmpty(txtTenGoiKhac))
                                        {
                                            nhanVien.TenGoiKhac = txtTenGoiKhac;
                                        }
                                        #endregion

                                        #region 5. Ngày sinh
                                        if (!String.IsNullOrWhiteSpace(txtNgaySinh))
                                        {
                                            try
                                            {
                                                DateTime ngaySinh = Convert.ToDateTime(txtNgaySinh);
                                                nhanVien.NgaySinh = ngaySinh;
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Ngày sinh không hợp lệ: " + txtNgaySinh);
                                            }
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu thông tin ngày sinh.");
                                        }
                                        #endregion

                                        #region 6. Nơi sinh
                                        if (!string.IsNullOrEmpty(txtNoiSinh))
                                        {
                                            DiaChi diaChi = new DiaChi(uow);
                                            diaChi.SoNha = txtNoiSinh;
                                            nhanVien.NoiSinh = diaChi;
                                        }
                                        #endregion

                                        #region 7. Giới tính
                                        if (!string.IsNullOrEmpty(txtGioiTinh))
                                        {
                                            if (txtGioiTinh.ToLower() == "nam")
                                                nhanVien.GioiTinh = GioiTinhEnum.Nam;
                                            else if (txtGioiTinh.ToLower() == "nữ" || txtGioiTinh.ToLower() == "nu")
                                                nhanVien.GioiTinh = GioiTinhEnum.Nu;
                                            else
                                            {
                                                errorLog.AppendLine(" + Giới tính không hợp lệ: " + txtGioiTinh);
                                            }
                                        }
                                        #endregion

                                        #region 8. Số chứng minh nhân dân
                                        if (!string.IsNullOrEmpty(txtCMND))
                                        {
                                            nhanVien.CMND = txtCMND;
                                        }
                                        #endregion

                                        #region 9. Ngày cấp CMND
                                        if (!string.IsNullOrWhiteSpace(txtNgayCapCMND))
                                        {
                                            try
                                            {
                                                DateTime ngayCap = Convert.ToDateTime(txtNgayCapCMND);
                                                nhanVien.NgayCap = ngayCap;
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Ngày cấp CMND không hợp lệ: " + txtNgayCapCMND);
                                            }
                                        }
                                        #endregion

                                        #region 10. Nơi cấp CMND
                                        if (!string.IsNullOrEmpty(txtNoiCapCMND))
                                        {
                                            //
                                            var tinh = uow.FindObject<TinhThanh>(CriteriaOperator.Parse("TenTinhThanh like ?", txtNoiCapCMND));
                                            if (tinh == null)
                                            {
                                                tinh = new TinhThanh(uow);
                                                tinh.TenTinhThanh = txtNoiCapCMND;
                                                tinh.MaQuanLy = Guid.NewGuid().ToString();
                                                tinh.Save();
                                            }
                                            //
                                            nhanVien.NoiCap = tinh;
                                        }
                                        #endregion

                                        #region 11. Hộ Chiếu
                                        if (!string.IsNullOrEmpty(txtSoHoChieu))
                                        {
                                            nhanVien.SoHoChieu = txtSoHoChieu;
                                        }
                                        #endregion

                                        #region 12. Ngày cấp hộ chiếu
                                        if (!string.IsNullOrWhiteSpace(txtNgayCapHoChieu))
                                        {
                                            try
                                            {
                                                DateTime ngayCapHoChieu = Convert.ToDateTime(txtNgayCapHoChieu);
                                                nhanVien.NgayCapHoChieu = ngayCapHoChieu;
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Ngày cấp hộ chiếu không hợp lệ: " + txtNgayCapHoChieu);
                                            }
                                        }
                                        #endregion

                                        #region 13. Nơi cấp hộ chiếu
                                        if (!string.IsNullOrEmpty(txtNoiCapHoChieu))
                                        {//                                
                                            nhanVien.NoiCapHoChieu = txtNoiCapHoChieu;
                                        }
                                        #endregion

                                        #region 14. Ngày hết hạn hộ chiếu
                                        if (!string.IsNullOrWhiteSpace(txtNgayHetHanHoChieu))
                                        {
                                            DateTime? ngay = null;
                                            try
                                            {
                                                DateTime ngayHetHan = Convert.ToDateTime(txtNgayHetHanHoChieu);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Ngày hết hạn hộ chiếu không hợp lệ: " + txtNgayHetHanHoChieu);
                                            }
                                        }
                                        #endregion

                                        #region 15. Quê quán
                                        if (!string.IsNullOrEmpty(txtQueQuan))
                                        {
                                            //Quê quán
                                            DiaChi diaChi = new DiaChi(uow);
                                            diaChi.SoNha = txtQueQuan;
                                            nhanVien.QueQuan = diaChi;
                                        }
                                        #endregion

                                        #region 16. Địa chỉ thường trú
                                        if (!string.IsNullOrEmpty(txtDiaChiThuongTru))
                                        {
                                            //Địa chỉ thường trú
                                            DiaChi diaChi = new DiaChi(uow);
                                            diaChi.SoNha = txtDiaChiThuongTru;
                                            nhanVien.DiaChiThuongTru = diaChi;
                                        }
                                        #endregion

                                        #region 17. Nơi ở hiện nay
                                        if (!string.IsNullOrEmpty(txtNoiOHienNay))
                                        {
                                            //Nơi ở hiện nay
                                            DiaChi diaChi = new DiaChi(uow);
                                            diaChi.SoNha = txtNoiOHienNay;
                                            nhanVien.NoiOHienNay = diaChi;
                                        }
                                        #endregion

                                        #region 18. Email
                                        if (!string.IsNullOrEmpty(txtEmail))
                                        {
                                            nhanVien.Email = txtEmail;
                                        }
                                        #endregion

                                        #region  19. Điện thoại di động
                                        if (!string.IsNullOrEmpty(txtDienThoaiDiDong))
                                        {
                                            nhanVien.DienThoaiDiDong = txtDienThoaiDiDong;
                                        }
                                        #endregion

                                        #region 20. Điện thoại nhà riêng
                                        if (!string.IsNullOrEmpty(txtDienThoaiNhaRieng))
                                        {
                                            nhanVien.DienThoaiNhaRieng = txtDienThoaiNhaRieng;
                                        }
                                        #endregion

                                        #region  21. Tình trạng hôn nhân
                                        if (!string.IsNullOrEmpty(txtTinhTrangHonNhan))
                                        {
                                            TinhTrangHonNhan honNhan = uow.FindObject<TinhTrangHonNhan>(CriteriaOperator.Parse("TenTinhTrangHonNhan like ?", txtTinhTrangHonNhan));
                                            if (honNhan == null)
                                            {
                                                honNhan = new TinhTrangHonNhan(uow);
                                                honNhan.TenTinhTrangHonNhan = txtTinhTrangHonNhan;
                                                honNhan.MaQuanLy = Guid.NewGuid().ToString();
                                                honNhan.Save();
                                            }
                                            nhanVien.TinhTrangHonNhan = honNhan;
                                        }
                                        #endregion

                                        #region 22. Dân tộc
                                        if (!string.IsNullOrEmpty(txtDanToc))
                                        {
                                            DanToc danToc = null;
                                            danToc = uow.FindObject<DanToc>(CriteriaOperator.Parse("TenDanToc like ?", txtDanToc));
                                            if (danToc == null)
                                            {
                                                //
                                                danToc = new DanToc(uow);
                                                danToc.TenDanToc = txtDanToc;
                                                danToc.MaQuanLy = Guid.NewGuid().ToString();
                                                danToc.Save();
                                            }
                                            nhanVien.DanToc = danToc;
                                        }
                                        #endregion

                                        #region 23. Tôn giáo
                                        if (!string.IsNullOrEmpty(txtTonGiao))
                                        {
                                            TonGiao tonGiao = null;
                                            tonGiao = uow.FindObject<TonGiao>(CriteriaOperator.Parse("TenTonGiao like ?", txtTonGiao));
                                            if (tonGiao == null)
                                            {
                                                //tạo mới tôn giáo
                                                tonGiao = new TonGiao(uow);
                                                tonGiao.TenTonGiao = txtTonGiao;
                                                tonGiao.MaQuanLy = Guid.NewGuid().ToString();
                                                tonGiao.Save();
                                            }
                                            nhanVien.TonGiao = tonGiao;
                                        }
                                        #endregion

                                        #region 24. Quốc tịch
                                        if (!string.IsNullOrEmpty(txtQuocTich))
                                        {
                                            QuocGia quocTich = uow.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia like ?", txtQuocTich));
                                            if (quocTich == null)
                                            {
                                                quocTich = new QuocGia(uow);
                                                quocTich.TenQuocGia = txtQuocTich;
                                                quocTich.MaQuanLy = Guid.NewGuid().ToString();
                                                quocTich.Save();
                                            }
                                            nhanVien.QuocTich = quocTich;
                                        }
                                        else
                                        {
                                            //errorLog.AppendLine(" + Thiếu quốc tịch.");
                                        }
                                        #endregion

                                        //
                                        #endregion

                                        //

                                        #region Nhân viên
                                        //
                                        #region 28. Chức Danh - Công việc
                                        if (!string.IsNullOrEmpty(txtChucDanhCongViec))
                                        {
                                            ChucDanh chucDanh = null;
                                            chucDanh = uow.FindObject<ChucDanh>(CriteriaOperator.Parse("TenChucDanh like ?", txtChucDanhCongViec));
                                            if (chucDanh == null)
                                            {
                                                //
                                                chucDanh = new ChucDanh(uow);
                                                chucDanh.TenChucDanh = txtChucDanhCongViec;
                                                chucDanh.MaQuanLy = Guid.NewGuid().ToString();
                                            }
                                            nhanVien.ChucDanh = chucDanh;
                                        }
                                        else
                                        {
                                           // errorLog.AppendLine(" + Thiếu chức danh công việc.");
                                        }
                                        #endregion

                                        #region 29. Bộ phận
                                        {
                                            if (obj.TatCa == false)
                                            {
                                                nhanVien.BoPhan = uow.GetObjectByKey<BoPhan>(obj.BoPhan.Oid);
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(txtDonVi))
                                                {
                                                    BoPhan boPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan Like ? and CongTy.Oid = ?", txtDonVi,obj.CongTy.Oid));
                                                    if (boPhan == null)
                                                    {
                                                        boPhan = new BoPhan(uow);
                                                        boPhan.TenBoPhan = txtDonVi;
                                                        boPhan.BoPhanCha = uow.GetObjectByKey<BoPhan>(obj.CongTy.Oid);
                                                        boPhan.CongTy = obj.CongTy;
                                                    }
                                                    nhanVien.BoPhan = boPhan;
                                                }
                                                else
                                                {
                                                    errorLog.AppendLine(" + Bộ phận không được để trống.");
                                                }
                                            }
                                        }
                                        #endregion

                                        #region 30. Công việc hiện nay
                                        if (!string.IsNullOrEmpty(txtCongViecHienNay))
                                        {
                                            CongViec congViec = uow.FindObject<CongViec>(CriteriaOperator.Parse("TenCongViec Like ?", txtCongViecHienNay));
                                            if (congViec == null)
                                            {
                                                congViec = new CongViec(uow);
                                                congViec.TenCongViec = txtCongViecHienNay;
                                                congViec.MaQuanLy = Guid.NewGuid().ToString();
                                            }
                                            nhanVien.CongViecHienNay = congViec;
                                        }
                                        #endregion

                                        #region 31. Ngày tuyển dụng
                                        if (!string.IsNullOrWhiteSpace(txtNgayTuyenDung))
                                        {
                                            try
                                            {
                                                DateTime ngayTuyenDung = Convert.ToDateTime(txtNgayTuyenDung);
                                                nhanVien.NgayTuyenDung = ngayTuyenDung;
                                            }
                                            catch
                                            {
                                                errorLog.AppendLine(" + Ngày tuyển dụng lần đầu không hợp lệ: " + txtNgayTuyenDung);
                                            }
                                        }
                                        #endregion

                                        #region 32. Cơ quan tuyển dụng
                                        if (!string.IsNullOrEmpty(txtCongViecTuyenDung))
                                        {
                                            nhanVien.DonViTuyenDung = txtCongViecTuyenDung;
                                        }
                                        #endregion

                                        #region  33. Công việc tuyển dụng
                                        if (!string.IsNullOrEmpty(txtCoQuanTuyenDung))
                                        {
                                            nhanVien.DonViTuyenDung = txtCoQuanTuyenDung;
                                        }
                                        #endregion

                                        #region 34. Ngày vào công ty
                                        if (!string.IsNullOrWhiteSpace(txtNgayVaoCongTy))
                                        {
                                            try
                                            {
                                                DateTime ngayVaoCongTy = Convert.ToDateTime(txtNgayVaoCongTy);
                                                nhanVien.NgayVaoCongTy = ngayVaoCongTy;
                                            }
                                            catch
                                            {
                                                errorLog.AppendLine(" + Ngày vào công ty không hợp lệ: " + txtNgayVaoCongTy);
                                            }
                                        }
                                        #endregion

                                        #region 35. Tình trạng nhân viên
                                        if (!string.IsNullOrEmpty(txtTinhTrang))
                                        {
                                            TinhTrang tinhTrang = null;
                                            tinhTrang = uow.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", txtTinhTrang));
                                            if (tinhTrang == null)
                                            {
                                                tinhTrang = new TinhTrang(uow);
                                                tinhTrang.TenTinhTrang = txtTinhTrang;
                                                tinhTrang.MaQuanLy = Guid.NewGuid().ToString();
                                            }
                                            nhanVien.TinhTrang = tinhTrang;
                                        }
                                        #endregion

                                        #region 36. Chức vụ chính
                                        if (!string.IsNullOrEmpty(txtChucVu))
                                        {
                                            ChucVu chucVu = uow.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu like ?", txtChucVu));
                                            if (chucVu == null)
                                            {
                                                chucVu = new ChucVu(uow);
                                                chucVu.TenChucVu = txtChucVu;
                                                chucVu.MaQuanLy = Guid.NewGuid().ToString();
                                                chucVu.Save();
                                            }
                                            nhanVien.ChucVu = chucVu;
                                        }
                                        #endregion

                                        #region 37.39 Chức vụ kiêm nhiệm
                                        if (!string.IsNullOrEmpty(txtChucVuKiemNhiem))
                                        {
                                            ChucVuKiemNhiem chucVuKiemNhiem = new ChucVuKiemNhiem(uow);
                                            chucVuKiemNhiem.NhanVien = nhanVien;
                                            chucVuKiemNhiem.BoPhan = nhanVien.BoPhan;
                                            //
                                            ChucVu chucVu = uow.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu like ?", txtChucVuKiemNhiem));
                                            if (chucVu == null)
                                            {
                                                chucVu = new ChucVu(uow);
                                                chucVu.TenChucVu = txtChucVu;
                                                chucVu.MaQuanLy = Guid.NewGuid().ToString();
                                                chucVu.Save();
                                                //
                                                chucVuKiemNhiem.ChucVu = chucVu;
                                            }

                                            #region 39. Ngày bổ nhiệm kiêm nhiệm
                                            if (!string.IsNullOrWhiteSpace(txtNgayBoNhiemKiemNhiem))
                                            {
                                                try
                                                {
                                                    DateTime ngayBoNhiemKiemNhiem = Convert.ToDateTime(txtNgayBoNhiemKiemNhiem);
                                                    chucVuKiemNhiem.NgayBoNhiem = ngayBoNhiemKiemNhiem;
                                                }
                                                catch
                                                {
                                                    errorLog.AppendLine(" + Ngày vào bổ nhiệm kiêm nhiệm không hợp lệ: " + txtNgayBoNhiemKiemNhiem);
                                                }
                                            }
                                            #endregion
                                        }
                                        #endregion

                                        #region 38. Ngày bổ nhiệm
                                        if (!string.IsNullOrWhiteSpace(txtNgayBoNhiem))
                                        {
                                            try
                                            {
                                                DateTime ngayBoNhiem = Convert.ToDateTime(txtNgayBoNhiem);
                                                nhanVien.NgayBoNhiemChucVu = ngayBoNhiem;
                                            }
                                            catch
                                            {
                                                errorLog.AppendLine(" + Ngày vào bổ nhiệm chức vụ không hợp lệ: " + txtNgayBoNhiem);
                                            }
                                        }
                                        #endregion

                                        #region 40. Loại hợp đồng
                                        if (!string.IsNullOrEmpty(txtLoaiHopDong))
                                        {
                                            LoaiHopDong loaiHopDong = uow.FindObject<LoaiHopDong>(CriteriaOperator.Parse("TenLoaiHopDong like ?", txtLoaiHopDong));
                                            if (loaiHopDong == null)
                                            {
                                                loaiHopDong = new LoaiHopDong(uow);
                                                loaiHopDong.TenLoaiHopDong = txtLoaiHopDong;
                                                loaiHopDong.MaQuanLy = Guid.NewGuid().ToString();
                                            }
                                            nhanVien.LoaiHopDong = loaiHopDong;
                                        }
                                        #endregion

                                        #region 41. Loại nhân sự
                                        if (!string.IsNullOrEmpty(txtLoaiNhanSu))
                                        {
                                            LoaiNhanSu loaiNhanSu = uow.FindObject<LoaiNhanSu>(CriteriaOperator.Parse("TenLoaiNhanSu Like ?", txtLoaiNhanSu));
                                            if (loaiNhanSu == null)
                                            {
                                                loaiNhanSu = new LoaiNhanSu(uow);
                                                loaiNhanSu.TenLoaiNhanSu = txtLoaiNhanSu;
                                                loaiNhanSu.MaQuanLy = Guid.NewGuid().ToString();
                                            }
                                            nhanVien.LoaiNhanSu = loaiNhanSu;
                                        }
                                        #endregion

                                        #region 42. Ngày vào biên chế
                                        if (!string.IsNullOrWhiteSpace(txtNgayVaoTapDoan))
                                        {
                                            try
                                            {
                                                DateTime ngayVaoTapDoan = Convert.ToDateTime(txtNgayVaoTapDoan);
                                                nhanVien.NgayVaoTapDoan = ngayVaoTapDoan;
                                            }
                                            catch
                                            {
                                                errorLog.AppendLine(" + Ngày vào biên chế không hợp lệ: " + txtNgayVaoTapDoan);
                                            }
                                        }
                                        #endregion

                                        #region 43. Nhóm máu
                                        if (!string.IsNullOrEmpty(txtNhomMau))
                                        {
                                            NhomMau nhomMau = uow.FindObject<NhomMau>(CriteriaOperator.Parse("TenNhomMau like ?", txtNhomMau));
                                            if (nhomMau == null)
                                            {
                                                nhomMau = new NhomMau(uow);
                                                nhomMau.TenNhomMau = txtNhomMau;
                                                nhomMau.MaQuanLy = Guid.NewGuid().ToString();
                                            }
                                            nhanVien.NhomMau = nhomMau;
                                        }
                                        #endregion

                                        #region 44. Chiều cao
                                        Decimal chieuCao;
                                        if (Decimal.TryParse(txtChieuCao, out chieuCao))
                                        {
                                            nhanVien.ChieuCao = Convert.ToInt32(chieuCao);
                                        }
                                        #endregion

                                        #region 45. Cân nặng
                                        Decimal canNang;
                                        if (Decimal.TryParse(txtCanNang, out canNang))
                                        {
                                            nhanVien.CanNang = Convert.ToInt32(canNang);
                                        }
                                        #endregion

                                        #region 46. Tình trạng sức khỏe
                                        {
                                            if (!string.IsNullOrEmpty(txtSucKhoe))
                                            {
                                                SucKhoe sucKhoe = uow.FindObject<SucKhoe>(CriteriaOperator.Parse("TenSucKhoe like ?", txtSucKhoe));
                                                if (sucKhoe == null)
                                                {
                                                    sucKhoe = new SucKhoe(uow);
                                                    sucKhoe.TenSucKhoe = txtSucKhoe;
                                                    sucKhoe.MaQuanLy = Guid.NewGuid().ToString();
                                                }
                                                nhanVien.SucKhoe = sucKhoe;
                                            }
                                        }
                                        #endregion

                                        #endregion

                                        //

                                        #region Thông tin lương
                                        //
                                        #region 47 --> 54 Ngạch lương
                                        if (!string.IsNullOrEmpty(txtMaNgach))
                                        {
                                            NgachLuong ngach = uow.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy like ?", txtMaNgach));
                                            //                                    
                                            if (ngach != null)
                                            {
                                                nhanVien.NhanVienThongTinLuong.NgachLuong = ngach;

                                                #region 48. Bậc lương
                                                if (!string.IsNullOrEmpty(txtBacLuong))
                                                {
                                                    BacLuong bacLuong = null;
                                                    bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong = ? and MaQuanLy = ?", ngach.Oid, txtBacLuong));
                                                    //
                                                    if (bacLuong != null)
                                                    {
                                                        nhanVien.NhanVienThongTinLuong.BacLuong = bacLuong;
                                                        nhanVien.NhanVienThongTinLuong.LuongCoBan = bacLuong.LuongCoBan;
                                                        nhanVien.NhanVienThongTinLuong.LuongKinhDoanh = bacLuong.LuongKinhDoanh;
                                                    }
                                                    else
                                                    {
                                                        errorLog.AppendLine(" + Bậc lương không tồn tại: " + txtBacLuong);
                                                    }
                                                }
                                                else
                                                {
                                                    errorLog.AppendLine(" + Thiếu thông tin bậc lương.");
                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Ngạch lương không tồn tại: " + txtMaNgach);
                                            }
                                        }
                                        else
                                        {
                                            //errorLog.AppendLine(" + Thiếu thông tin ngạch lương.");
                                        }

                                        #endregion

                                        #region 49. Ngày bổ nhiệm ngạch
                                        if (!string.IsNullOrWhiteSpace(txtNgayBoNhiemNgach))
                                        {
                                            try
                                            {
                                                DateTime ngayBoNhiemNgach = Convert.ToDateTime(txtNgayBoNhiemNgach);
                                                nhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach = ngayBoNhiemNgach;
                                            }
                                            catch
                                            {
                                                errorLog.AppendLine(" + Ngày bổ nhiệm ngạch không hợp lệ: " + txtNgayBoNhiemNgach);
                                            }
                                        }
                                        #endregion

                                        #region 50. Ngày hưởng lương
                                        if (!string.IsNullOrWhiteSpace(txtNgayHuongLuong))
                                        {
                                            try
                                            {
                                                DateTime ngayHuongLuong = Convert.ToDateTime(txtNgayHuongLuong);
                                                nhanVien.NhanVienThongTinLuong.NgayHuongLuong = ngayHuongLuong;
                                            }
                                            catch
                                            {
                                                errorLog.AppendLine(" + Ngày hưởng lương không hợp lệ: " + txtNgayHuongLuong);
                                            }
                                        }
                                        #endregion

                                        #region 55. Hệ số chức vụ
                                        Decimal heSoChucVu;
                                        if (Decimal.TryParse(txtHSPCChucVu, out heSoChucVu))
                                        {
                                            nhanVien.NhanVienThongTinLuong.HSPCChucVu = heSoChucVu;
                                        }
                                        #endregion

                                        #region 56. Hệ số kiêm nhiệm
                                        Decimal heSoKiemNhiem;
                                        if (Decimal.TryParse(txtPCKiemNhiem, out heSoKiemNhiem))
                                        {
                                            nhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem = heSoKiemNhiem;
                                        }
                                        #endregion

                                        #region 57. Hệ số trách nhiệm
                                        Decimal heSoTrachNhiem;
                                        if (Decimal.TryParse(txtPCTrachNhiem, out heSoTrachNhiem))
                                        {
                                            nhanVien.NhanVienThongTinLuong.PhuCapTrachNhiem = heSoKiemNhiem;
                                        }
                                        #endregion

                                        #region 58. Hệ số pc đoàn
                                        Decimal heSoDoan;
                                        if (Decimal.TryParse(txtHSPCDoan, out heSoDoan))
                                        {
                                            nhanVien.NhanVienThongTinLuong.HSPCChucVuDoan = heSoDoan;
                                        }
                                        #endregion

                                        #region 59. Hệ số pc đảng
                                        Decimal heSoDang;
                                        if (Decimal.TryParse(txtHSPCDang, out heSoDang))
                                        {
                                            nhanVien.NhanVienThongTinLuong.HSPCChucVuDang = heSoKiemNhiem;
                                        }
                                        #endregion

                                        #region 60. Mốc tính lương lần sau
                                        if (!string.IsNullOrWhiteSpace(txtMocNangLuongLanSau))
                                        {
                                            try
                                            {
                                                DateTime mocTinhLuongLanSau = Convert.ToDateTime(txtMocNangLuongLanSau);
                                                nhanVien.NhanVienThongTinLuong.MocNangLuongLanSau = mocTinhLuongLanSau;
                                            }
                                            catch
                                            {
                                                errorLog.AppendLine(" + Mốc tính lương lần sau không hợp lệ: " + txtMocNangLuongLanSau);
                                            }
                                        }
                                        #endregion

                                        #region 61. Mã số thuế
                                        if (!string.IsNullOrWhiteSpace(txtMaSoThue))
                                        {
                                            nhanVien.NhanVienThongTinLuong.MaSoThue = txtMaSoThue;
                                        }
                                        #endregion

                                        #endregion

                                        //

                                        #region Thông bảo hiểm

                                        #region 62 --> 67
                                        if (!String.IsNullOrWhiteSpace(txtSoSoBHXH))
                                        {
                                            HoSoBaoHiem hoSoBaoHiem = uow.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien.MaTapDoan like ?", nhanVien.MaTapDoan));
                                            //nếu nhân viên đó chưa có bảo hiểm thì thêm hồ sơ bảo hiêm
                                            if (hoSoBaoHiem == null)
                                            {
                                                hoSoBaoHiem = new HoSoBaoHiem(uow);
                                                hoSoBaoHiem.ThongTinNhanVien = nhanVien;
                                                hoSoBaoHiem.SoSoBHXH = txtSoSoBHXH;

                                                #region 63. Ngày tham gia bảo hiểm xã hội
                                                if (!String.IsNullOrWhiteSpace(txtNgayBatDauDongBHXH))
                                                {
                                                    try
                                                    {
                                                        DateTime ngay = Convert.ToDateTime(txtNgayBatDauDongBHXH);
                                                        hoSoBaoHiem.NgayThamGiaBHXH = ngay;
                                                    }
                                                    catch
                                                    {
                                                        errorLog.AppendLine(" + Ngày tham gia bảo hiểm XH không hợp lệ: " + txtNgayBatDauDongBHXH);
                                                    }
                                                }
                                                #endregion

                                                #region 64. Thẻ bảo hiểm y tế
                                                hoSoBaoHiem.SoTheBHYT = txtSoTheBHYT;
                                                #endregion

                                                #region 65. Từ ngày
                                                if (!String.IsNullOrWhiteSpace(txtTuNgayBH))
                                                {
                                                    try
                                                    {
                                                        DateTime ngay = Convert.ToDateTime(txtTuNgayBH);
                                                        hoSoBaoHiem.TuNgay = ngay;
                                                    }
                                                    catch
                                                    {
                                                        errorLog.AppendLine(" + Từ ngày tham gia bảo hiểm XH không hợp lệ: " + txtTuNgayBH);
                                                    }
                                                }
                                                #endregion

                                                #region 66. Đến ngày
                                                if (!String.IsNullOrWhiteSpace(txtDenNgayBH))
                                                {
                                                    try
                                                    {
                                                        DateTime ngay = Convert.ToDateTime(txtDenNgayBH);
                                                        hoSoBaoHiem.DenNgay = ngay;
                                                    }
                                                    catch
                                                    {
                                                        errorLog.AppendLine(" + Đến ngày tham gia bảo hiểm XH không hợp lệ: " + txtDenNgayBH);
                                                    }
                                                }
                                                #endregion

                                                #region 67. Nơi đăng ký khám chữa bệnh
                                                if (!String.IsNullOrWhiteSpace(txtNoiDangKyKhamChuaBenh))
                                                {
                                                    BenhVien benhVien = null;
                                                    benhVien = uow.FindObject<BenhVien>(CriteriaOperator.Parse("TenBenhVien Like ?", txtNoiDangKyKhamChuaBenh));
                                                    if (benhVien == null)
                                                    {
                                                        benhVien = new BenhVien(uow);
                                                        benhVien.TenBenhVien = txtNoiDangKyKhamChuaBenh;
                                                        benhVien.MaQuanLy = Guid.NewGuid().ToString();
                                                    }
                                                    hoSoBaoHiem.NoiDangKyKhamChuaBenh = benhVien;
                                                }
                                                #endregion
                                            }
                                        }
                                        #endregion

                                        #endregion

                                        //

                                        #region Thông tin trình độ

                                        #region 68. Trình độ văn hóa
                                        {
                                            if (!string.IsNullOrEmpty(txtTrinhDoVanHoa))
                                            {
                                                TrinhDoVanHoa trinhDo = uow.FindObject<TrinhDoVanHoa>(CriteriaOperator.Parse("TenTrinhDoVanHoa Like ?", txtTrinhDoVanHoa));
                                                if (trinhDo == null)
                                                {
                                                    trinhDo = new TrinhDoVanHoa(uow);
                                                    trinhDo.TenTrinhDoVanHoa = txtTrinhDoVanHoa;
                                                    trinhDo.MaQuanLy = txtTrinhDoVanHoa;
                                                }
                                                nhanVien.NhanVienTrinhDo.TrinhDoVanHoa = trinhDo;
                                            }
                                        }
                                        #endregion

                                        #region 69 --> 72 Trình độ trung cấp
                                        if (!String.IsNullOrEmpty(txtChuyenNganhDaoTao_TrungHoc))
                                        {
                                            TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ? or TenTrinhDoChuyenMon Like ?", "trung cấp", "trung học%"));
                                            if (trinhDo != null)
                                            {
                                                VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.MaTapDoan like ? and TrinhDoChuyenMon = ?", nhanVien.MaTapDoan, trinhDo));
                                                if (bang == null)
                                                {
                                                    bang = new VanBang(uow);
                                                    bang.HoSo = nhanVien;
                                                    bang.TrinhDoChuyenMon = trinhDo;
                                                    nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDo;

                                                    ChuyenNganhDaoTao chuyenMon = null;
                                                    chuyenMon = uow.FindObject<ChuyenNganhDaoTao>(CriteriaOperator.Parse("TenChuyenNganhDaoTao like ?", txtChuyenNganhDaoTao_TrungHoc));
                                                    if (chuyenMon == null)
                                                    {
                                                        chuyenMon = new ChuyenNganhDaoTao(uow);
                                                        chuyenMon.TenChuyenNganhDaoTao = txtChuyenNganhDaoTao_TrungHoc;
                                                        chuyenMon.MaQuanLy = txtChuyenNganhDaoTao_TrungHoc;

                                                    }
                                                    bang.ChuyenNganhDaoTao = chuyenMon;
                                                    nhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao = chuyenMon;

                                                    //
                                                    if (!String.IsNullOrEmpty(txtNoiDaoTao_TrungHoc))
                                                    {
                                                        TruongDaoTao truong = null;
                                                        truong = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao like ?", txtNoiDaoTao_TrungHoc));
                                                        if (truong == null)
                                                        {
                                                            truong = new TruongDaoTao(uow);
                                                            truong.TenTruongDaoTao = txtNoiDaoTao_TrungHoc;
                                                            truong.MaQuanLy = txtNoiDaoTao_TrungHoc;
                                                        }
                                                        bang.TruongDaoTao = truong;
                                                        nhanVien.NhanVienTrinhDo.TruongDaoTao = truong;
                                                    }
                                                    //
                                                    if (!String.IsNullOrEmpty(txtHinhThucDaoTao_TrungHoc))
                                                    {
                                                        HinhThucDaoTao hinhThuc = null;
                                                        hinhThuc = uow.FindObject<HinhThucDaoTao>(CriteriaOperator.Parse("TenHinhThucDaoTao like ?", txtHinhThucDaoTao_TrungHoc));
                                                        if (hinhThuc == null)
                                                        {
                                                            hinhThuc = new HinhThucDaoTao(uow);
                                                            hinhThuc.TenHinhThucDaoTao = txtHinhThucDaoTao_TrungHoc;
                                                            hinhThuc.MaQuanLy = txtHinhThucDaoTao_TrungHoc;
                                                        }
                                                        bang.HinhThucDaoTao = hinhThuc;
                                                        nhanVien.NhanVienTrinhDo.HinhThucDaoTao = hinhThuc;
                                                    }

                                                    #region năm công nhận tốt nghiệp
                                                    int namTotNghiep;
                                                    if (int.TryParse(txtNamTotNghiep_TrungHoc, out namTotNghiep))
                                                    {
                                                        bang.NamTotNghiep = namTotNghiep;
                                                        nhanVien.NhanVienTrinhDo.NamTotNghiep = namTotNghiep;
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                        #endregion

                                        #region 73 --> 76 Trình độ cao đẳng
                                        if (!String.IsNullOrEmpty(txtChuyenNganhDaoTao_CaoDang))
                                        {
                                            TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ? ", "cao đẳng"));
                                            if (trinhDo != null)
                                            {
                                                VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.MaTapDoan like ? and TrinhDoChuyenMon = ?", nhanVien.MaTapDoan, trinhDo));
                                                if (bang == null)
                                                {
                                                    bang = new VanBang(uow);
                                                    bang.HoSo = nhanVien;
                                                    bang.TrinhDoChuyenMon = trinhDo;
                                                    nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDo;

                                                    ChuyenNganhDaoTao chuyenMon = null;
                                                    chuyenMon = uow.FindObject<ChuyenNganhDaoTao>(CriteriaOperator.Parse("TenChuyenNganhDaoTao like ?", txtChuyenNganhDaoTao_CaoDang));
                                                    if (chuyenMon == null)
                                                    {
                                                        chuyenMon = new ChuyenNganhDaoTao(uow);
                                                        chuyenMon.TenChuyenNganhDaoTao = txtChuyenNganhDaoTao_CaoDang;
                                                        chuyenMon.MaQuanLy = txtChuyenNganhDaoTao_CaoDang;

                                                    }
                                                    bang.ChuyenNganhDaoTao = chuyenMon;
                                                    nhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao = chuyenMon;

                                                    //
                                                    if (!String.IsNullOrEmpty(txtNoiDaoTao_CaoDang))
                                                    {
                                                        TruongDaoTao truong = null;
                                                        truong = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao like ?", txtNoiDaoTao_CaoDang));
                                                        if (truong == null)
                                                        {
                                                            truong = new TruongDaoTao(uow);
                                                            truong.TenTruongDaoTao = txtNoiDaoTao_CaoDang;
                                                            truong.MaQuanLy = txtNoiDaoTao_CaoDang;
                                                        }
                                                        bang.TruongDaoTao = truong;
                                                        nhanVien.NhanVienTrinhDo.TruongDaoTao = truong;
                                                    }
                                                    //
                                                    if (!String.IsNullOrEmpty(txtHinhThucDaoTao_CaoDang))
                                                    {
                                                        HinhThucDaoTao hinhThuc = null;
                                                        hinhThuc = uow.FindObject<HinhThucDaoTao>(CriteriaOperator.Parse("TenHinhThucDaoTao like ?", txtHinhThucDaoTao_CaoDang));
                                                        if (hinhThuc == null)
                                                        {
                                                            hinhThuc = new HinhThucDaoTao(uow);
                                                            hinhThuc.TenHinhThucDaoTao = txtHinhThucDaoTao_CaoDang;
                                                            hinhThuc.MaQuanLy = txtHinhThucDaoTao_CaoDang;
                                                        }
                                                        bang.HinhThucDaoTao = hinhThuc;
                                                        nhanVien.NhanVienTrinhDo.HinhThucDaoTao = hinhThuc;
                                                    }
                                                    #region năm công nhận tốt nghiệp
                                                    int namTotNghiep;
                                                    if (int.TryParse(txtNamTotNghiep_CaoDang, out namTotNghiep))
                                                    {
                                                        bang.NamTotNghiep = namTotNghiep;
                                                        nhanVien.NhanVienTrinhDo.NamTotNghiep = namTotNghiep;
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                        #endregion

                                        #region 77 --> 80 Trình độ đại học
                                        if (!String.IsNullOrEmpty(txtChuyenNganhDaoTao_DaiHoc))
                                        {
                                            TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ? or TenTrinhDoChuyenMon Like ? or TenTrinhDoChuyenMon Like ?", "đại học", "cử nhân", "kỹ sư"));
                                            if (trinhDo != null)
                                            {
                                                VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.MaTapDoan like ? and TrinhDoChuyenMon = ?", nhanVien.MaTapDoan, trinhDo));
                                                if (bang == null)
                                                {
                                                    bang = new VanBang(uow);
                                                    bang.HoSo = nhanVien;
                                                    bang.TrinhDoChuyenMon = trinhDo;
                                                    nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDo;

                                                    ChuyenNganhDaoTao chuyenMon = null;
                                                    chuyenMon = uow.FindObject<ChuyenNganhDaoTao>(CriteriaOperator.Parse("TenChuyenNganhDaoTao like ?", txtChuyenNganhDaoTao_DaiHoc));
                                                    if (chuyenMon == null)
                                                    {
                                                        chuyenMon = new ChuyenNganhDaoTao(uow);
                                                        chuyenMon.TenChuyenNganhDaoTao = txtChuyenNganhDaoTao_DaiHoc;
                                                        chuyenMon.MaQuanLy = txtChuyenNganhDaoTao_DaiHoc;

                                                    }
                                                    bang.ChuyenNganhDaoTao = chuyenMon;
                                                    nhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao = chuyenMon;

                                                    //
                                                    if (!String.IsNullOrEmpty(txtNoiDaoTao_DaiHoc))
                                                    {
                                                        TruongDaoTao truong = null;
                                                        truong = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao like ?", txtNoiDaoTao_DaiHoc));
                                                        if (truong == null)
                                                        {
                                                            truong = new TruongDaoTao(uow);
                                                            truong.TenTruongDaoTao = txtNoiDaoTao_DaiHoc;
                                                            truong.MaQuanLy = txtNoiDaoTao_DaiHoc;
                                                        }
                                                        bang.TruongDaoTao = truong;
                                                        nhanVien.NhanVienTrinhDo.TruongDaoTao = truong;
                                                    }
                                                    //
                                                    if (!String.IsNullOrEmpty(txtHinhThucDaoTao_DaiHoc))
                                                    {
                                                        HinhThucDaoTao hinhThuc = null;
                                                        hinhThuc = uow.FindObject<HinhThucDaoTao>(CriteriaOperator.Parse("TenHinhThucDaoTao like ?", txtHinhThucDaoTao_DaiHoc));
                                                        if (hinhThuc == null)
                                                        {
                                                            hinhThuc = new HinhThucDaoTao(uow);
                                                            hinhThuc.TenHinhThucDaoTao = txtHinhThucDaoTao_DaiHoc;
                                                            hinhThuc.MaQuanLy = txtHinhThucDaoTao_DaiHoc;
                                                        }
                                                        bang.HinhThucDaoTao = hinhThuc;
                                                        nhanVien.NhanVienTrinhDo.HinhThucDaoTao = hinhThuc;
                                                    }
                                                    #region năm công nhận tốt nghiệp
                                                    int namTotNghiep;
                                                    if (int.TryParse(txtNamTotNghiep_DaiHoc, out namTotNghiep))
                                                    {
                                                        bang.NamTotNghiep = namTotNghiep;
                                                        nhanVien.NhanVienTrinhDo.NamTotNghiep = namTotNghiep;
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                        #endregion

                                        #region 81 --> 84 Trình độ thạc sĩ
                                        if (!String.IsNullOrEmpty(txtChuyenNganhDaoTao_ThacSi))
                                        {
                                            TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ? or TenTrinhDoChuyenMon Like ?", "thạc sĩ", "thạc sỹ"));
                                            if (trinhDo != null)
                                            {
                                                VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.MaTapDoan like ? and TrinhDoChuyenMon = ?", nhanVien.MaTapDoan, trinhDo));
                                                if (bang == null)
                                                {
                                                    bang = new VanBang(uow);
                                                    bang.HoSo = nhanVien;
                                                    bang.TrinhDoChuyenMon = trinhDo;
                                                    nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDo;

                                                    ChuyenNganhDaoTao chuyenMon = null;
                                                    chuyenMon = uow.FindObject<ChuyenNganhDaoTao>(CriteriaOperator.Parse("TenChuyenNganhDaoTao like ?", txtChuyenNganhDaoTao_ThacSi));
                                                    if (chuyenMon == null)
                                                    {
                                                        chuyenMon = new ChuyenNganhDaoTao(uow);
                                                        chuyenMon.TenChuyenNganhDaoTao = txtChuyenNganhDaoTao_ThacSi;
                                                        chuyenMon.MaQuanLy = txtChuyenNganhDaoTao_ThacSi;

                                                    }
                                                    bang.ChuyenNganhDaoTao = chuyenMon;
                                                    nhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao = chuyenMon;

                                                    //
                                                    if (!String.IsNullOrEmpty(txtNoiDaoTao_ThacSi))
                                                    {
                                                        TruongDaoTao truong = null;
                                                        truong = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao like ?", txtNoiDaoTao_ThacSi));
                                                        if (truong == null)
                                                        {
                                                            truong = new TruongDaoTao(uow);
                                                            truong.TenTruongDaoTao = txtNoiDaoTao_ThacSi;
                                                            truong.MaQuanLy = txtNoiDaoTao_ThacSi;
                                                        }
                                                        bang.TruongDaoTao = truong;
                                                        nhanVien.NhanVienTrinhDo.TruongDaoTao = truong;
                                                    }
                                                    //
                                                    if (!String.IsNullOrEmpty(txtHinhThucDaoTao_ThacSi))
                                                    {
                                                        HinhThucDaoTao hinhThuc = null;
                                                        hinhThuc = uow.FindObject<HinhThucDaoTao>(CriteriaOperator.Parse("TenHinhThucDaoTao like ?", txtHinhThucDaoTao_ThacSi));
                                                        if (hinhThuc == null)
                                                        {
                                                            hinhThuc = new HinhThucDaoTao(uow);
                                                            hinhThuc.TenHinhThucDaoTao = txtHinhThucDaoTao_ThacSi;
                                                            hinhThuc.MaQuanLy = txtHinhThucDaoTao_ThacSi;
                                                        }
                                                        bang.HinhThucDaoTao = hinhThuc;
                                                        nhanVien.NhanVienTrinhDo.HinhThucDaoTao = hinhThuc;
                                                    }
                                                    #region năm công nhận tốt nghiệp
                                                    int namTotNghiep;
                                                    if (int.TryParse(txtNamTotNghiep_ThacSi, out namTotNghiep))
                                                    {
                                                        bang.NamTotNghiep = namTotNghiep;
                                                        nhanVien.NhanVienTrinhDo.NamTotNghiep = namTotNghiep;
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                        #endregion

                                        #region 85 --> 88 Trình độ tiến sĩ
                                        if (!String.IsNullOrEmpty(txtChuyenNganhDaoTao_TienSi))
                                        {
                                            TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ? or TenTrinhDoChuyenMon Like ?", "tiến sĩ", "tiến sỹ"));
                                            if (trinhDo != null)
                                            {
                                                VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.MaTapDoan like ? and TrinhDoChuyenMon = ?", nhanVien.MaTapDoan, trinhDo));
                                                if (bang == null)
                                                {
                                                    bang = new VanBang(uow);
                                                    bang.HoSo = nhanVien;
                                                    bang.TrinhDoChuyenMon = trinhDo;
                                                    nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDo;

                                                    ChuyenNganhDaoTao chuyenMon = null;
                                                    chuyenMon = uow.FindObject<ChuyenNganhDaoTao>(CriteriaOperator.Parse("TenChuyenNganhDaoTao like ?", txtChuyenNganhDaoTao_TienSi));
                                                    if (chuyenMon == null)
                                                    {
                                                        chuyenMon = new ChuyenNganhDaoTao(uow);
                                                        chuyenMon.TenChuyenNganhDaoTao = txtChuyenNganhDaoTao_TienSi;
                                                        chuyenMon.MaQuanLy = txtChuyenNganhDaoTao_TienSi;

                                                    }
                                                    bang.ChuyenNganhDaoTao = chuyenMon;
                                                    nhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao = chuyenMon;

                                                    //
                                                    if (!String.IsNullOrEmpty(txtNoiDaoTao_TienSi))
                                                    {
                                                        TruongDaoTao truong = null;
                                                        truong = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao like ?", txtNoiDaoTao_TienSi));
                                                        if (truong == null)
                                                        {
                                                            truong = new TruongDaoTao(uow);
                                                            truong.TenTruongDaoTao = txtNoiDaoTao_TienSi;
                                                            truong.MaQuanLy = txtNoiDaoTao_TienSi;
                                                        }
                                                        bang.TruongDaoTao = truong;
                                                        nhanVien.NhanVienTrinhDo.TruongDaoTao = truong;
                                                    }
                                                    //
                                                    if (!String.IsNullOrEmpty(txtHinhThucDaoTao_TienSi))
                                                    {
                                                        HinhThucDaoTao hinhThuc = null;
                                                        hinhThuc = uow.FindObject<HinhThucDaoTao>(CriteriaOperator.Parse("TenHinhThucDaoTao like ?", txtHinhThucDaoTao_TienSi));
                                                        if (hinhThuc == null)
                                                        {
                                                            hinhThuc = new HinhThucDaoTao(uow);
                                                            hinhThuc.TenHinhThucDaoTao = txtHinhThucDaoTao_TienSi;
                                                            hinhThuc.MaQuanLy = txtHinhThucDaoTao_TienSi;
                                                        }
                                                        bang.HinhThucDaoTao = hinhThuc;
                                                        nhanVien.NhanVienTrinhDo.HinhThucDaoTao = hinhThuc;
                                                    }
                                                    #region Năm công nhận tốt nghiệp
                                                    int namTotNghiep;
                                                    if (int.TryParse(txtNamTotNghiep_TienSi, out namTotNghiep))
                                                    {
                                                        bang.NamTotNghiep = namTotNghiep;
                                                        nhanVien.NhanVienTrinhDo.NamTotNghiep = namTotNghiep;
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                        #endregion

                                        //Trình độ hiện tại cao nhất 89 không cần vì là quá trình lưu từ 68 --> 88

                                        #region 90. Trình độ tin học
                                        {
                                            if (!string.IsNullOrEmpty(txtTrinhTinHoc))
                                            {
                                                TrinhDoTinHoc trinhDoTinHoc = uow.FindObject<TrinhDoTinHoc>(CriteriaOperator.Parse("TenTrinhDoTinHoc like ?", txtTrinhTinHoc));
                                                if (trinhDoTinHoc == null)
                                                {
                                                    trinhDoTinHoc = new TrinhDoTinHoc(uow);
                                                    trinhDoTinHoc.TenTrinhDoTinHoc = txtTrinhTinHoc;
                                                    trinhDoTinHoc.MaQuanLy = txtTrinhTinHoc;
                                                }
                                                nhanVien.NhanVienTrinhDo.TrinhDoTinHoc = trinhDoTinHoc;
                                            }
                                        }
                                        #endregion

                                        #region 91.92 Trình độ ngoài ngữ khác
                                        TrinhDoNgoaiNguKhac tringDoNgoaiNguKhac = null;

                                        #region 91. Ngoại ngữ                                 
                                        //
                                        if (!string.IsNullOrEmpty(txtNgoaiNgu))
                                        {
                                            tringDoNgoaiNguKhac = new TrinhDoNgoaiNguKhac(uow);
                                            //
                                            NgoaiNgu ngoaiNgu = uow.FindObject<NgoaiNgu>(CriteriaOperator.Parse("TenNgoaiNgu like ?", txtNgoaiNgu));
                                            if (ngoaiNgu == null)
                                            {
                                                ngoaiNgu = new NgoaiNgu(uow);
                                                ngoaiNgu.TenNgoaiNgu = txtTrinhNgoaiNgu;
                                                ngoaiNgu.MaQuanLy = txtTrinhNgoaiNgu;
                                            }
                                            nhanVien.NhanVienTrinhDo.NgoaiNgu = ngoaiNgu;
                                            //
                                            tringDoNgoaiNguKhac.NgoaiNgu = ngoaiNgu;
                                        }
                                        #endregion

                                        #region 92. Trình độ ngoại ngữ
                                        if (!string.IsNullOrEmpty(txtTrinhNgoaiNgu))
                                        {
                                            if (tringDoNgoaiNguKhac == null)
                                                tringDoNgoaiNguKhac = new TrinhDoNgoaiNguKhac(uow);
                                            //
                                            TrinhDoNgoaiNgu trinhDoNgoaiNgu = uow.FindObject<TrinhDoNgoaiNgu>(CriteriaOperator.Parse("TenTrinhDoNgoaiNgu like ?", txtTrinhNgoaiNgu));
                                            if (trinhDoNgoaiNgu == null)
                                            {
                                                trinhDoNgoaiNgu = new TrinhDoNgoaiNgu(uow);
                                                trinhDoNgoaiNgu.TenTrinhDoNgoaiNgu = txtTrinhNgoaiNgu;
                                                trinhDoNgoaiNgu.MaQuanLy = txtTrinhNgoaiNgu;
                                            }
                                            nhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu = trinhDoNgoaiNgu;
                                            //
                                            tringDoNgoaiNguKhac.TrinhDoNgoaiNgu = trinhDoNgoaiNgu;
                                        }
                                        #endregion

                                        #endregion

                                        #endregion

                                        //

                                        #region 93 --> 94 Ðoàn viên
                                        if (!string.IsNullOrEmpty(txtNgayVaoDoan))
                                        {
                                            try
                                            {
                                                nhanVien.NgayVaoDoan = Convert.ToDateTime(txtNgayVaoDoan);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Ngày kết nạp đoàn không hợp lệ - không đúng định dạng dd/MM/yyyy: " + txtNgayVaoDoan);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(txtChucVuDoan))
                                        {
                                            ChucDanh chucDanhDoan = null;
                                            chucDanhDoan = uow.FindObject<ChucDanh>(CriteriaOperator.Parse("TenChucDanh like ?", txtChucVuDoan));
                                            if (chucDanhDoan == null)
                                            {
                                                chucDanhDoan = new ChucDanh(uow);
                                                chucDanhDoan.TenChucDanh = txtChucVuDoan;
                                                chucDanhDoan.MaQuanLy = txtChucVuDoan;
                                            }
                                            nhanVien.ChucDanhDoan = chucDanhDoan;
                                        }
                                        #endregion

                                        //

                                        #region 95 --> 96 Đảng cột
                                        if (!string.IsNullOrEmpty(txtNgayVaoDang))
                                        {
                                            try
                                            {
                                                nhanVien.NgayVaoDang = Convert.ToDateTime(txtNgayVaoDang);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Ngày kết nạp đảng không hợp lệ - không đúng định dạng dd/MM/yyyy: " + txtNgayVaoDang);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(txtChucVuDang))
                                        {
                                            ChucDanh chucDanhDang = null;
                                            chucDanhDang = uow.FindObject<ChucDanh>(CriteriaOperator.Parse("TenChucDanh like ?", txtChucVuDang));
                                            if (chucDanhDang == null)
                                            {
                                                chucDanhDang = new ChucDanh(uow);
                                                chucDanhDang.TenChucDanh = txtChucVuDang;
                                                chucDanhDang.MaQuanLy = txtChucVuDang;
                                            }
                                            nhanVien.ChucDanhDang = chucDanhDang;
                                        }
                                        #endregion

                                        //

                                        #region 97 --> 98 Tài khoản ngân hàng
                                        if (!String.IsNullOrWhiteSpace(txtSoTaiKhoan))
                                        {
                                            TaiKhoanNganHang taiKhoanNganHang = new TaiKhoanNganHang(uow);
                                            taiKhoanNganHang.NhanVien = nhanVien;
                                            taiKhoanNganHang.TaiKhoanChinh = true;
                                            taiKhoanNganHang.SoTaiKhoan = txtSoTaiKhoan;
                                            if (!String.IsNullOrWhiteSpace(txtTenNganHang))
                                            {
                                                NganHang nganHang = null;
                                                nganHang = uow.FindObject<NganHang>(CriteriaOperator.Parse("TenNganHang like ?", txtTenNganHang));
                                                if (nganHang == null)
                                                {
                                                    nganHang = new NganHang(uow);
                                                    nganHang.TenNganHang = txtTenNganHang;
                                                }
                                                taiKhoanNganHang.NganHang = nganHang;
                                            }
                                        }
                                        //
                                        #endregion

                                        //

                                        #region Ghi File log
                                        {
                                            //Đưa thông tin bị lỗi vào blog
                                            if (errorLog.Length > 0)
                                            {
                                                mainLog.AppendLine("- STT: " + txtSTT);
                                                mainLog.AppendLine(string.Format("- Cán bộ Mã: {0} Tên: {1} không import vào phần mềm được: ", nhanVien.MaTapDoan, nhanVien.HoTen));
                                                mainLog.AppendLine(errorLog.ToString());
                                                //
                                                sucessImport = false;
                                            }
                                        }
                                        #endregion
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
                                // End Duyệt qua tất cả các dòng trong file excel
                            }
                        }
                    }
                    //

                    string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
                    DialogUtil.ShowInfo("Import Thành Công " + sucessNumber + " Nhân viên - Số nhân viên Import không thành công " + erorrNumber + " " + s + "!");

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
        #endregion

        #region 2. Nhập quá trình từ tập tin excel
        public static void ImportProsess(IObjectSpace obs, NhanVien_ChonQuaTrinh obj)
        {
            if (obj.LoaiQuaTrinh == LoaiQuaTrinhEnum.LichSuBanThan)
                Process_LichSuBanThan(obs, obj);
            if (obj.LoaiQuaTrinh == LoaiQuaTrinhEnum.DienBienLuong)
                Process_DienBienLuong(obs, obj);
            if (obj.LoaiQuaTrinh == LoaiQuaTrinhEnum.BoNhiem)
                Process_QuaTrinhBoNhiem(obs, obj);
            if (obj.LoaiQuaTrinh == LoaiQuaTrinhEnum.BoNhiemKiemNhiem)
                Process_QuaTrinhBoNhiemKiemNhiem(obs, obj);
            if (obj.LoaiQuaTrinh == LoaiQuaTrinhEnum.CongTac)
                Process_QuaTrinhCongTac(obs, obj);
            if (obj.LoaiQuaTrinh == LoaiQuaTrinhEnum.DieuDong)
                Process_QuaTrinhDieuDong(obs, obj);
        }

        #region 2.1 Lịch sử bản thân
        static void Process_LichSuBanThan(IObjectSpace obs, NhanVien_ChonQuaTrinh obj)
        {
            //
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            StringBuilder mainLog = new StringBuilder();
            //

            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet1$A2:AI]", obj.LoaiOffice))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            int idx_STT = 0;
                            int idx_CongTy = 1;
                            int idx_ChucDanh = 2;
                            int idx_MaQuanLy = 3;
                            int idx_HoTen = 4;
                            int idx_TuNam = 5;
                            int idx_DenNam = 6;
                            int idx_LyDoNghiViec = 7;
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
                                    String txt_STT = dr[idx_STT].ToString().FullTrim();
                                    String txt_CongTy = dr[idx_CongTy].ToString().FullTrim();
                                    String txt_ChucDanh = dr[idx_ChucDanh].ToString().FullTrim();
                                    String txt_MaQuanLy = dr[idx_MaQuanLy].ToString().FullTrim();
                                    String txt_HoTen = dr[idx_HoTen].ToString().FullTrim();
                                    String txt_TuNam = dr[idx_TuNam].ToString().FullTrim();
                                    String txt_DenNam = dr[idx_DenNam].ToString().FullTrim();
                                    String txt_LyDoNghiViec = dr[idx_LyDoNghiViec].ToString().FullTrim();
                                    String txt_GhiChu = dr[idx_GhiChu].ToString().FullTrim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                    #region Kiểm tra dữ
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaQuanLy))
                                    {
                                        ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaTapDoan = ? and CongTy = ?", txt_MaQuanLy, obj.CongTy.Oid));
                                        if (nhanVien == null)
                                        {
                                            mainLog.AppendLine("- STT: " + txt_STT);
                                            mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống {2}.", txt_MaQuanLy, txt_HoTen, obj.CongTy.TenBoPhan));
                                            //
                                            sucessImport = false;
                                        }
                                        else
                                        {
                                            //
                                            LichSuBanThan lichSuBanThan = new LichSuBanThan(uow);
                                            lichSuBanThan.ThongTinNhanVien = nhanVien;

                                            #region 1. Công ty
                                            if (!string.IsNullOrEmpty(txt_CongTy))
                                            {
                                                lichSuBanThan.CongTy = txt_CongTy;
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("+ Thiếu thông tin công ty.");
                                            }
                                            #endregion

                                            #region 2. Chức danh
                                            if (!string.IsNullOrEmpty(txt_ChucDanh))
                                            {
                                                lichSuBanThan.ChucDanh = txt_ChucDanh;
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("+ Thiếu thông tin chức danh.");
                                            }
                                            #endregion

                                            #region 3. Từ năm
                                            if (!string.IsNullOrEmpty(txt_TuNam))
                                            {
                                                lichSuBanThan.TuNam = txt_TuNam;
                                            }
                                            #endregion

                                            #region 4. Đến năm
                                            if (!string.IsNullOrEmpty(txt_DenNam))
                                            {
                                                lichSuBanThan.DenNam = txt_DenNam;
                                            }
                                            #endregion

                                            #region 5. Lý do nghỉ việc
                                            if (!string.IsNullOrEmpty(txt_LyDoNghiViec))
                                            {
                                                lichSuBanThan.LyDoNghiViec = txt_LyDoNghiViec;
                                            }
                                            #endregion

                                            #region 6. Ghi chú
                                            if (!string.IsNullOrEmpty(txt_GhiChu))
                                            {
                                                lichSuBanThan.GhiChu = txt_GhiChu;
                                            }
                                            #endregion

                                            #region 5. Ghi File log
                                            {
                                                //Đưa thông tin bị lỗi vào blog
                                                if (detailLog.Length > 0)
                                                {
                                                    mainLog.AppendLine("- STT: " + txt_STT);
                                                    mainLog.AppendLine(string.Format("- Cán bộ Mã: {0} Tên: {1} không import vào phần mềm được: ", nhanVien.MaTapDoan, nhanVien.HoTen));
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
                                DialogUtil.ShowInfo("Import Thành Công: " + sucessNumber + " dòng dữ liệu - Số dòng không thành công: " + erorrNumber + "-" + s + "!");

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

        #region 2.2 Diễn biến lương
        static void Process_DienBienLuong(IObjectSpace obs, NhanVien_ChonQuaTrinh obj)
        {
            //
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            StringBuilder mainLog = new StringBuilder();
            //

            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet1$A2:AU]", obj.LoaiOffice))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            int idx_STT = 0;
                            int idx_QuyetDinh = 1;
                            int idx_NgayQuyetDinh = 2;
                            int idx_TuNgay = 3;
                            int idx_DenNgay = 4;
                            int idx_MaQuanLy = 5;
                            int idx_HoTen = 6;
                            int idx_LuongCoBan = 7;
                            int idx_NgachLuong = 8;
                            int idx_BacLuong = 9;
                            int idx_PhuCapKiemNhiem = 10;
                            int idx_PhuCapDienThoai = 11;
                            int idx_PhuCapTienAn = 12;
                            int idx_PhuCapTienXang = 13;
                            int idx_LyDo = 14;
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
                                    String txt_SoQuyetDinh = dr[idx_QuyetDinh].ToString().FullTrim();
                                    String txt_NgayQuyetDinh = dr[idx_NgayQuyetDinh].ToString().FullTrim();
                                    String txt_HoTen = dr[idx_HoTen].ToString().FullTrim();
                                    String txt_TuNgay = dr[idx_TuNgay].ToString().FullTrim();
                                    String txt_DenNgay = dr[idx_DenNgay].ToString().FullTrim();
                                    String txt_MaQuanLy = dr[idx_MaQuanLy].ToString().FullTrim();
                                    String txt_LuongCoBan = dr[idx_LuongCoBan].ToString().FullTrim();
                                    String txt_NgachLuong = dr[idx_NgachLuong].ToString().FullTrim();
                                    String txt_BacLuong = dr[idx_BacLuong].ToString().FullTrim();
                                    String txt_PhuCapKiemNhiem = dr[idx_PhuCapKiemNhiem].ToString().FullTrim();
                                    String txt_PhuCapDienThoai = dr[idx_PhuCapDienThoai].ToString().FullTrim();
                                    String txt_PhuCapTienAn = dr[idx_PhuCapTienAn].ToString().FullTrim();
                                    String txt_PhuCapTienXang = dr[idx_PhuCapTienXang].ToString().FullTrim();
                                    String txt_LyDo = dr[idx_LyDo].ToString().FullTrim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                    #region Kiểm tra dữ liệu
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaQuanLy))
                                    {
                                        ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaTapDoan = ? and CongTy = ?", txt_MaQuanLy, obj.CongTy.Oid));
                                        if (nhanVien == null)
                                        {
                                            mainLog.AppendLine("- STT: " + txt_STT);
                                            mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống.", txt_MaQuanLy, txt_HoTen));
                                            //
                                            sucessImport = false;
                                        }
                                        else
                                        {
                                            //
                                            DienBienLuong dienBienLuong = new DienBienLuong(uow);
                                            dienBienLuong.ThongTinNhanVien = nhanVien;

                                            #region 1. Số quyết định
                                            if (!string.IsNullOrEmpty(txt_SoQuyetDinh))
                                            {
                                                dienBienLuong.SoQuyetDinh = txt_SoQuyetDinh;
                                            }
                                            #endregion

                                            #region 2. Ngày quyết định
                                            if (!string.IsNullOrEmpty(txt_NgayQuyetDinh))
                                            {
                                                try
                                                {
                                                    dienBienLuong.NgayQuyetDinh = Convert.ToDateTime(txt_NgayQuyetDinh);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + txt_NgayQuyetDinh);
                                                }
                                            }
                                            #endregion

                                            #region 3. Từ ngày
                                            if (!string.IsNullOrEmpty(txt_TuNgay))
                                            {
                                                try
                                                {
                                                    dienBienLuong.TuNgay = Convert.ToDateTime(txt_TuNgay);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Từ ngày không hợp lệ: " + txt_TuNgay);
                                                }
                                            }
                                            #endregion

                                            #region 4. Đến ngày
                                            if (!string.IsNullOrEmpty(txt_DenNgay))
                                            {
                                                try
                                                {
                                                    dienBienLuong.DenNgay = Convert.ToDateTime(txt_DenNgay);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Đến ngày không hợp lệ: " + txt_DenNgay);
                                                }
                                            }
                                            #endregion

                                            #region 5. Lương cơ bản
                                            {
                                                if (!string.IsNullOrEmpty(txt_LuongCoBan))
                                                {
                                                    try
                                                    {
                                                        dienBienLuong.LuongCoBan = Convert.ToDecimal(txt_LuongCoBan);
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine(" + Lương cơ bản không hợp lệ: " + txt_LuongCoBan);
                                                    }
                                                }
                                            }
                                            #endregion

                                            #region 6. Ngạch lương
                                            {
                                                if (!string.IsNullOrEmpty(txt_NgachLuong))
                                                {
                                                    NgachLuong ngachLuong = uow.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy Like ?", txt_NgachLuong));
                                                    if (ngachLuong != null)
                                                    {
                                                        dienBienLuong.NgachLuong = ngachLuong;
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine(" + Ngạch lương không tồn tại: " + txt_NgachLuong);
                                                    }
                                                }
                                            }
                                            #endregion

                                            #region 7. Bậc lương
                                            {
                                                if (!string.IsNullOrEmpty(txt_BacLuong))
                                                {
                                                    BacLuong bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("TenBacLuong Like ?", txt_BacLuong));
                                                    if (bacLuong != null)
                                                    {
                                                        dienBienLuong.BacLuong = bacLuong;
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine(" + Bậc lương không tồn tại: " + txt_BacLuong);
                                                    }
                                                }
                                            }
                                            #endregion

                                            #region 8. Phụ cấp kiêm nhiệm
                                            {
                                                if (!string.IsNullOrEmpty(txt_PhuCapKiemNhiem))
                                                {
                                                    try
                                                    {
                                                        dienBienLuong.PhuCapKiemNhiem = Convert.ToDecimal(txt_PhuCapKiemNhiem);
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine(" + Phụ cấp kiêm nhiệm không hợp lệ: " + txt_PhuCapKiemNhiem);
                                                    }
                                                }
                                            }
                                            #endregion

                                            #region 9. Phụ cấp điện thoại
                                            {
                                                if (!string.IsNullOrEmpty(txt_PhuCapDienThoai))
                                                {
                                                    try
                                                    {
                                                        dienBienLuong.PhuCapDienThoai = Convert.ToDecimal(txt_PhuCapDienThoai);
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine(" + Phụ cấp điện thoại không hợp lệ: " + txt_PhuCapDienThoai);
                                                    }
                                                }
                                            }
                                            #endregion

                                            #region 10. Phụ cấp tiền ăn
                                            {
                                                if (!string.IsNullOrEmpty(txt_PhuCapTienAn))
                                                {
                                                    try
                                                    {
                                                        dienBienLuong.PhuCapTienAn = Convert.ToDecimal(txt_PhuCapTienAn);
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine(" + Phụ cấp tiền ăn không hợp lệ: " + txt_PhuCapTienAn);
                                                    }
                                                }
                                            }
                                            #endregion

                                            #region 11. Phụ cấp tiền xăng
                                            {
                                                if (!string.IsNullOrEmpty(txt_PhuCapTienXang))
                                                {
                                                    try
                                                    {
                                                        dienBienLuong.PhuCapTienXang = Convert.ToDecimal(txt_PhuCapTienXang);
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine(" + Phụ cấp tiền xăng không hợp lệ: " + txt_PhuCapTienXang);
                                                    }
                                                }
                                            }
                                            #endregion                                       

                                            #region 12. Lý do
                                            if (!string.IsNullOrEmpty(txt_LyDo))
                                            {
                                                dienBienLuong.LyDo = txt_LyDo;
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("+ Thiếu thông tin lý do.");
                                            }
                                            #endregion

                                            #region 13. Ghi File log
                                            {
                                                //Đưa thông tin bị lỗi vào blog
                                                if (detailLog.Length > 0)
                                                {
                                                    mainLog.AppendLine("- STT: " + txt_STT);
                                                    mainLog.AppendLine(string.Format("- Cán bộ Mã: {0} Tên: {1} không import vào phần mềm được: ", nhanVien.MaTapDoan, nhanVien.HoTen));
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

        #region 2.3 Quá trình bổ nhiệm
        static void Process_QuaTrinhBoNhiem(IObjectSpace obs, NhanVien_ChonQuaTrinh obj)
        {
            //
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            StringBuilder mainLog = new StringBuilder();
            //

            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet1$A2:AJ]", obj.LoaiOffice))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            int idx_STT = 0;
                            int idx_QuyetDinh = 1;
                            int idx_NgayQuyetDinh = 2;
                            int idx_TuNam = 3;
                            int idx_DenNam = 4;
                            int idx_MaQuanLy = 5;
                            int idx_HoTen = 6;
                            int idx_ChucVu = 7;
                            int idx_PCChucVu = 8;
                            int idx_NgayChucVu = 9;
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
                                    String txt_SoQuyetDinh = dr[idx_QuyetDinh].ToString().FullTrim();
                                    String txt_NgayQuyetDinh = dr[idx_NgayQuyetDinh].ToString().FullTrim();
                                    String txt_HoTen = dr[idx_HoTen].ToString().FullTrim();
                                    String txt_TuNam = dr[idx_TuNam].ToString().FullTrim();
                                    String txt_DenNam = dr[idx_DenNam].ToString().FullTrim();
                                    String txt_MaQuanLy = dr[idx_MaQuanLy].ToString().FullTrim();
                                    String txt_ChucVu = dr[idx_ChucVu].ToString().FullTrim();
                                    String txt_PCKiemNhiem = dr[idx_PCChucVu].ToString().FullTrim();
                                    String txt_NgayChucVu = dr[idx_NgayChucVu].ToString().FullTrim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                    #region Kiểm tra dữ liệu
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaQuanLy))
                                    {
                                        ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaTapDoan = ? and CongTy = ?", txt_MaQuanLy, obj.CongTy.Oid));
                                        if (nhanVien == null)
                                        {
                                            mainLog.AppendLine("- STT: " + txt_STT);
                                            mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống {2}.", txt_MaQuanLy, txt_HoTen, obj.CongTy.TenBoPhan));
                                            //
                                            sucessImport = false;
                                        }
                                        else
                                        {
                                            //
                                            QuaTrinhBoNhiem quaTrinhBoNhiem = new QuaTrinhBoNhiem(uow);
                                            quaTrinhBoNhiem.ThongTinNhanVien = nhanVien;

                                            #region 1. Số quyết định
                                            if (!string.IsNullOrEmpty(txt_SoQuyetDinh))
                                            {
                                                quaTrinhBoNhiem.SoQuyetDinh = txt_SoQuyetDinh;
                                            }
                                            #endregion

                                            #region 2. Ngày quyết định
                                            if (!string.IsNullOrEmpty(txt_NgayQuyetDinh))
                                            {
                                                try
                                                {
                                                    quaTrinhBoNhiem.NgayQuyetDinh = Convert.ToDateTime(txt_NgayQuyetDinh);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + txt_NgayQuyetDinh);
                                                }
                                            }
                                            #endregion

                                            #region 3. Từ năm
                                            if (!string.IsNullOrEmpty(txt_TuNam))
                                            {
                                                quaTrinhBoNhiem.TuNam = txt_TuNam;

                                            }
                                            #endregion

                                            #region 4. Đến năm
                                            if (!string.IsNullOrEmpty(txt_DenNam))
                                            {
                                                quaTrinhBoNhiem.DenNam = txt_DenNam;
                                            }
                                            #endregion                                          

                                            #region 5. HSPC Chức vụ
                                            {
                                                if (!string.IsNullOrEmpty(txt_PCKiemNhiem))
                                                {
                                                    try
                                                    {
                                                        quaTrinhBoNhiem.PhuCapKiemNhiem = Convert.ToDecimal(txt_PCKiemNhiem);
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine(" + PC Kiêm nhiệm không hợp lệ: " + txt_PCKiemNhiem);
                                                    }
                                                }
                                            }
                                            #endregion                                         

                                            #region 6. Ngày Bổ nhiệm Chức vụ
                                            if (!string.IsNullOrEmpty(txt_NgayChucVu))
                                            {
                                                try
                                                {
                                                    quaTrinhBoNhiem.NgayBoNhiemChucVu = Convert.ToDateTime(txt_NgayChucVu);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày chức vụ không hợp lệ: " + txt_NgayChucVu);
                                                }
                                            }
                                            #endregion

                                            #region 7. Ghi File log
                                            {
                                                //Đưa thông tin bị lỗi vào blog
                                                if (detailLog.Length > 0)
                                                {
                                                    mainLog.AppendLine("- STT: " + txt_STT);
                                                    mainLog.AppendLine(string.Format("- Cán bộ Mã: {0} Tên: {1} không import vào phần mềm được: ", nhanVien.MaTapDoan, nhanVien.HoTen));
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

        #region 2.4 Quá trình bổ nhiệm kiêm nhiệm
        static void Process_QuaTrinhBoNhiemKiemNhiem(IObjectSpace obs, NhanVien_ChonQuaTrinh obj)
        {
            //
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            StringBuilder mainLog = new StringBuilder();
            //

            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet1$A2:AJ]", obj.LoaiOffice))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            int idx_STT = 0;
                            int idx_QuyetDinh = 1;
                            int idx_NgayQuyetDinh = 2;
                            int idx_TuNam = 3;
                            int idx_DenNam = 4;
                            int idx_MaQuanLy = 5;
                            int idx_HoTen = 6;
                            int idx_HSPCChucVu = 7;
                            int idx_NgayChucVu = 8;
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
                                    String txt_SoQuyetDinh = dr[idx_QuyetDinh].ToString().FullTrim();
                                    String txt_NgayQuyetDinh = dr[idx_NgayQuyetDinh].ToString().FullTrim();
                                    String txt_HoTen = dr[idx_HoTen].ToString().FullTrim();
                                    String txt_TuNam = dr[idx_TuNam].ToString().FullTrim();
                                    String txt_DenNam = dr[idx_DenNam].ToString().FullTrim();
                                    String txt_MaQuanLy = dr[idx_MaQuanLy].ToString().FullTrim();
                                    String txt_PCKiemNhiem = dr[idx_HSPCChucVu].ToString().FullTrim();
                                    String txt_NgayChucVu = dr[idx_NgayChucVu].ToString().FullTrim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                    #region Kiểm tra dữ liệu
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaQuanLy))
                                    {
                                        ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaTapDoan = ? and CongTy = ?", txt_MaQuanLy, obj.CongTy.Oid));
                                        if (nhanVien == null)
                                        {
                                            mainLog.AppendLine("- STT: " + txt_STT);
                                            mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống {2}.", txt_MaQuanLy, txt_HoTen, obj.CongTy.TenBoPhan));
                                            //
                                            sucessImport = false;
                                        }
                                        else
                                        {
                                            //
                                            QuaTrinhBoNhiemKiemNhiem quaTrinhBoNhiemKiemNhiem = new QuaTrinhBoNhiemKiemNhiem(uow);
                                            quaTrinhBoNhiemKiemNhiem.ThongTinNhanVien = nhanVien;

                                            #region 1. Số quyết định
                                            if (!string.IsNullOrEmpty(txt_SoQuyetDinh))
                                            {
                                                quaTrinhBoNhiemKiemNhiem.SoQuyetDinh = txt_SoQuyetDinh;
                                            }
                                            #endregion

                                            #region 2. Ngày quyết định
                                            if (!string.IsNullOrEmpty(txt_NgayQuyetDinh))
                                            {
                                                try
                                                {
                                                    quaTrinhBoNhiemKiemNhiem.NgayQuyetDinh = Convert.ToDateTime(txt_NgayQuyetDinh);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + txt_NgayQuyetDinh);
                                                }
                                            }
                                            #endregion

                                            #region 3. Từ năm
                                            if (!string.IsNullOrEmpty(txt_TuNam))
                                            {
                                                quaTrinhBoNhiemKiemNhiem.TuNam = txt_TuNam;

                                            }
                                            #endregion

                                            #region 4. Đến năm
                                            if (!string.IsNullOrEmpty(txt_DenNam))
                                            {
                                                quaTrinhBoNhiemKiemNhiem.DenNam = txt_DenNam;
                                            }
                                            #endregion

                                            #region 5. HSPC Chức vụ
                                            {
                                                if (!string.IsNullOrEmpty(txt_PCKiemNhiem))
                                                {
                                                    try
                                                    {
                                                        quaTrinhBoNhiemKiemNhiem.HSPCChucVu = Convert.ToDecimal(txt_PCKiemNhiem);
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine(" + HSPC Chức vụ không hợp lệ: " + txt_PCKiemNhiem);
                                                    }
                                                }
                                            }
                                            #endregion

                                            #region 6. Ngày hưởng HSPC Chức vụ
                                            if (!string.IsNullOrEmpty(txt_NgayChucVu))
                                            {
                                                try
                                                {
                                                    quaTrinhBoNhiemKiemNhiem.NgayHuongChucVu = Convert.ToDateTime(txt_NgayChucVu);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày chức vụ không hợp lệ: " + txt_NgayChucVu);
                                                }
                                            }
                                            #endregion

                                            #region 7. Ghi File log
                                            {
                                                //Đưa thông tin bị lỗi vào blog
                                                if (detailLog.Length > 0)
                                                {
                                                    mainLog.AppendLine("- STT: " + txt_STT);
                                                    mainLog.AppendLine(string.Format("- Cán bộ Mã: {0} Tên: {1} không import vào phần mềm được: ", nhanVien.MaTapDoan, nhanVien.HoTen));
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

        #region 2.5 Quá trình công tác
        static void Process_QuaTrinhCongTac(IObjectSpace obs, NhanVien_ChonQuaTrinh obj)
        {
            //
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            StringBuilder mainLog = new StringBuilder();
            //

            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet1$A2:AH]", obj.LoaiOffice))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            int idx_STT = 0;
                            int idx_QuyetDinh = 1;
                            int idx_NgayQuyetDinh = 2;
                            int idx_TuNam = 3;
                            int idx_DenNam = 4;
                            int idx_MaQuanLy = 5;
                            int idx_HoTen = 6;
                            int idx_NoiDung = 7;

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
                                    String txt_SoQuyetDinh = dr[idx_QuyetDinh].ToString().FullTrim();
                                    String txt_NgayQuyetDinh = dr[idx_NgayQuyetDinh].ToString().FullTrim();
                                    String txt_HoTen = dr[idx_HoTen].ToString().FullTrim();
                                    String txt_TuNam = dr[idx_TuNam].ToString().FullTrim();
                                    String txt_DenNam = dr[idx_DenNam].ToString().FullTrim();
                                    String txt_MaQuanLy = dr[idx_MaQuanLy].ToString().FullTrim();
                                    String txt_NoiDung = dr[idx_NoiDung].ToString().FullTrim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                    #region Kiểm tra dữ liệu
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaQuanLy))
                                    {
                                        ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaTapDoan = ? and CongTy = ?", txt_MaQuanLy, obj.CongTy.Oid));
                                        if (nhanVien == null)
                                        {
                                            mainLog.AppendLine("- STT: " + txt_STT);
                                            mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống {2}.", txt_MaQuanLy, txt_HoTen, obj.CongTy.TenBoPhan));
                                            //
                                            sucessImport = false;
                                        }
                                        else
                                        {
                                            //
                                            QuaTrinhCongTac quaTrinhCongTac = new QuaTrinhCongTac(uow);
                                            quaTrinhCongTac.ThongTinNhanVien = nhanVien;

                                            #region 1. Số quyết định
                                            if (!string.IsNullOrEmpty(txt_SoQuyetDinh))
                                            {
                                                quaTrinhCongTac.SoQuyetDinh = txt_SoQuyetDinh;
                                            }
                                            #endregion

                                            #region 2. Ngày quyết định
                                            if (!string.IsNullOrEmpty(txt_NgayQuyetDinh))
                                            {
                                                try
                                                {
                                                    quaTrinhCongTac.NgayQuyetDinh = Convert.ToDateTime(txt_NgayQuyetDinh);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + txt_NgayQuyetDinh);
                                                }
                                            }
                                            #endregion

                                            #region 3. Từ năm
                                            if (!string.IsNullOrEmpty(txt_TuNam))
                                            {
                                                quaTrinhCongTac.TuNam = txt_TuNam;

                                            }
                                            #endregion

                                            #region 4. Đến năm
                                            if (!string.IsNullOrEmpty(txt_DenNam))
                                            {
                                                quaTrinhCongTac.DenNam = txt_DenNam;
                                            }
                                            #endregion

                                            #region 5.  Nội dung
                                            {
                                                if (!string.IsNullOrEmpty(txt_NoiDung))
                                                {
                                                    quaTrinhCongTac.NoiDung = txt_NoiDung;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("+ Thiếu thông tin nội dung.");
                                                }
                                            }

                                            #endregion                                          

                                            #region 6. Ghi File log
                                            {
                                                //Đưa thông tin bị lỗi vào blog
                                                if (detailLog.Length > 0)
                                                {
                                                    mainLog.AppendLine("- STT: " + txt_STT);
                                                    mainLog.AppendLine(string.Format("- Cán bộ Mã: {0} Tên: {1} không import vào phần mềm được: ", nhanVien.MaTapDoan, nhanVien.HoTen));
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

        #region 2.6 Quá trình điều động
        static void Process_QuaTrinhDieuDong(IObjectSpace obs, NhanVien_ChonQuaTrinh obj)
        {
            //
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            StringBuilder mainLog = new StringBuilder();
            //

            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet1$A2:AI]", obj.LoaiOffice))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            int idx_STT = 0;
                            int idx_QuyetDinh = 1;
                            int idx_NgayQuyetDinh = 2;
                            int idx_MaQuanLy = 3;
                            int idx_HoTen = 4;
                            int idx_DonViCu = 5;
                            int idx_DonViMoi = 6;
                            int idx_NgayDieuChuyen = 7;
                            int idx_LyDo = 8;
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
                                    String txt_SoQuyetDinh = dr[idx_QuyetDinh].ToString().FullTrim();
                                    String txt_NgayQuyetDinh = dr[idx_NgayQuyetDinh].ToString().FullTrim();
                                    String txt_HoTen = dr[idx_HoTen].ToString().FullTrim();
                                    String txt_MaQuanLy = dr[idx_MaQuanLy].ToString().FullTrim();
                                    String txt_DonViCu = dr[idx_DonViCu].ToString().FullTrim();
                                    String txt_DonViMoi = dr[idx_DonViMoi].ToString().FullTrim();
                                    String txt_NgayDieuChuyen = dr[idx_NgayDieuChuyen].ToString().FullTrim();
                                    String txt_LyDo = dr[idx_LyDo].ToString().FullTrim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                    #region Kiểm tra dữ liệu
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaQuanLy))
                                    {
                                        ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaTapDoan = ? and CongTy = ?", txt_MaQuanLy, obj.CongTy.Oid));
                                        if (nhanVien == null)
                                        {
                                            mainLog.AppendLine("- STT: " + txt_STT);
                                            mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống {2}.", txt_MaQuanLy, txt_HoTen, obj.CongTy.TenBoPhan));
                                            //
                                            sucessImport = false;
                                        }
                                        else
                                        {
                                            //
                                            QuaTrinhDieuDong quaTrinhDieuDong = new QuaTrinhDieuDong(uow);
                                            quaTrinhDieuDong.ThongTinNhanVien = nhanVien;

                                            #region 1. Số quyết định
                                            if (!string.IsNullOrEmpty(txt_SoQuyetDinh))
                                            {
                                                quaTrinhDieuDong.SoQuyetDinh = txt_SoQuyetDinh;
                                            }
                                            #endregion

                                            #region 2. Ngày quyết định
                                            if (!string.IsNullOrEmpty(txt_NgayQuyetDinh))
                                            {
                                                try
                                                {
                                                    quaTrinhDieuDong.NgayQuyetDinh = Convert.ToDateTime(txt_NgayQuyetDinh);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + txt_NgayQuyetDinh);
                                                }
                                            }
                                            #endregion                                           

                                            #region 3. Đơn vị cũ
                                            {
                                                if (!string.IsNullOrEmpty(txt_DonViCu))
                                                {
                                                    BoPhan boPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan Like ?", txt_DonViCu));
                                                    if (boPhan != null)
                                                    {
                                                        quaTrinhDieuDong.DonViCu = boPhan;
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine(" + Đơn vị cũ không tồn tại: " + txt_DonViCu);
                                                    }

                                                }
                                            }
                                            #endregion

                                            #region 4. Đơn vị mới
                                            if (!string.IsNullOrEmpty(txt_DonViMoi))
                                            {
                                                BoPhan boPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan Like ?", txt_DonViMoi));
                                                if (boPhan != null)
                                                {
                                                    quaTrinhDieuDong.DonViMoi = boPhan;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Đơn vị mới không tồn tại: " + txt_DonViMoi);
                                                }
                                            }

                                            #endregion

                                            #region 5. Ngày điều chuyển
                                            if (!string.IsNullOrEmpty(txt_NgayDieuChuyen))
                                            {
                                                try
                                                {
                                                    quaTrinhDieuDong.NgayDieuChuyen = Convert.ToDateTime(txt_NgayDieuChuyen);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày điều chuyển không hợp lệ: " + txt_NgayDieuChuyen);
                                                }

                                            }
                                            #endregion

                                            #region 6. Lý do
                                            if (!string.IsNullOrEmpty(txt_LyDo))
                                            {
                                                quaTrinhDieuDong.LyDo = txt_LyDo;
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("+ Thiếu thông tin lý do.");
                                            }
                                            #endregion

                                            #region 7. Ghi File log
                                            {
                                                //Đưa thông tin bị lỗi vào blog
                                                if (detailLog.Length > 0)
                                                {
                                                    mainLog.AppendLine("- STT: " + txt_STT);
                                                    mainLog.AppendLine(string.Format("- Cán bộ Mã: {0} Tên: {1} không import vào phần mềm được: ", nhanVien.MaTapDoan, nhanVien.HoTen));
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

        #endregion

        #region 3. Nhập quan hệ gia đình từ tập tin excel
        public static void ImportFamily(IObjectSpace obs, NhanVien_ChonNhanVien obj)
        {
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            StringBuilder mainLog = new StringBuilder();
            //

            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet1$A2:AF]", obj.LoaiOffice))
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            int idx_STT = 0;
                            int idx_MaQuanLy = 1;
                            int idx_HoTenNhanVien = 2;
                            int idx_HoTenNguoiThan = 3;
                            int idx_QuanHe = 4;
                            int idx_PhanLoai = 5;
                            int idx_GioiTinh = 6;
                            int idx_NgaySinh = 7;
                            int idx_QueQuan = 8;
                            int idx_NoiOHienNay = 9;
                            int idx_DanToc = 10;
                            int idx_TonGiao = 11;
                            int idx_QuocTich = 12;
                            int idx_NuocCuTru = 13;
                            int idx_NamDiCu = 14;
                            int idx_NgheNghiep = 15;
                            int idx_NoiLamViec = 16;
                            int idx_TinhTrang = 17;
                            int idx_GiamTruGiaCanh = 18;
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
                                    String txt_HoTenNhanVien = dr[idx_HoTenNhanVien].ToString().FullTrim();
                                    String txt_HoTenNguoiThan = dr[idx_HoTenNguoiThan].ToString().FullTrim();
                                    String txt_QuanHe = dr[idx_QuanHe].ToString().FullTrim();
                                    String txt_PhanLoai = dr[idx_PhanLoai].ToString().FullTrim();
                                    String txt_GioiTinh = dr[idx_GioiTinh].ToString().FullTrim();
                                    String txt_NgaySinh = dr[idx_NgaySinh].ToString().FullTrim();
                                    String txt_QueQuan = dr[idx_QueQuan].ToString().FullTrim();
                                    String txt_NoiOHienNay = dr[idx_NoiOHienNay].ToString().FullTrim();
                                    String txt_DanToc = dr[idx_DanToc].ToString().FullTrim();
                                    String txt_TonGiao = dr[idx_TonGiao].ToString().FullTrim();
                                    String txt_QuocTich = dr[idx_QuocTich].ToString().FullTrim();
                                    String txt_NuocCuTru = dr[idx_NuocCuTru].ToString().FullTrim();
                                    String txt_NamDiCu = dr[idx_NamDiCu].ToString().FullTrim();
                                    String txt_NgheNghiep = dr[idx_NgheNghiep].ToString().FullTrim();
                                    String txt_NoiLamViec = dr[idx_NoiLamViec].ToString().FullTrim();
                                    String txt_TinhTrang = dr[idx_TinhTrang].ToString().FullTrim();
                                    String txt_GiamTruGiaCanh = dr[idx_GiamTruGiaCanh].ToString().FullTrim();
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                    #region Kiểm tra dữ
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaQuanLy))
                                    {
                                        ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaTapDoan = ? and CongTy = ?", txt_MaQuanLy, obj.CongTy.Oid));
                                        if (nhanVien == null)
                                        {
                                            mainLog.AppendLine("- STT: " + txt_STT);
                                            mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống {2}.", txt_MaQuanLy, txt_HoTenNhanVien, obj.CongTy.TenBoPhan));
                                            //
                                            sucessImport = false;
                                        }
                                        else
                                        {
                                            //
                                            QuanHeGiaDinh quanHeGiaDinh = new QuanHeGiaDinh(uow);
                                            quanHeGiaDinh.NhanVien = nhanVien;

                                            #region 2. Họ tên người thân
                                            if (!string.IsNullOrEmpty(txt_HoTenNguoiThan))
                                            {
                                                quanHeGiaDinh.HoTenNguoiThan = txt_HoTenNguoiThan;
                                            }
                                            #endregion

                                            #region 3. Quan hệ
                                            if (!string.IsNullOrEmpty(txt_QuanHe))
                                            {
                                                QuanHe quanHe = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe Like ?", txt_QuanHe));
                                                if (quanHe != null)
                                                {
                                                    quanHeGiaDinh.QuanHe = quanHe;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Quan hệ không tồn tại: " + txt_QuanHe);
                                                }

                                            }
                                            #endregion

                                            #region 4. Phân loại
                                            if (!string.IsNullOrEmpty(txt_PhanLoai))
                                            {
                                                if (txt_PhanLoai.ToLower() == "trong nước")
                                                    quanHeGiaDinh.PhanLoai = LoaiQuocGiaEnum.TrongNuoc;
                                                else if (txt_PhanLoai.ToLower() == "ngoài nước")
                                                    quanHeGiaDinh.PhanLoai = LoaiQuocGiaEnum.NgoaiNuoc;
                                                else
                                                {
                                                    detailLog.AppendLine(" + Phân loại không hợp lệ: " + txt_PhanLoai);
                                                }
                                            }
                                            #endregion

                                            #region 5. Giới tính
                                            if (!string.IsNullOrEmpty(txt_GioiTinh))
                                            {
                                                if (txt_GioiTinh.ToLower() == "nam")
                                                    quanHeGiaDinh.GioiTinh = GioiTinhEnum.Nam;
                                                else if (txt_GioiTinh.ToLower() == "nữ" || txt_GioiTinh.ToLower() == "nu")
                                                    quanHeGiaDinh.GioiTinh = GioiTinhEnum.Nu;
                                                else
                                                {
                                                    detailLog.AppendLine(" + Giới tính không hợp lệ: " + txt_GioiTinh);
                                                }
                                            }
                                            #endregion

                                            #region 6. Ngày sinh
                                            if (!string.IsNullOrEmpty(txt_NgaySinh))
                                            {
                                                try
                                                {
                                                    quanHeGiaDinh.NgaySinh = Convert.ToDateTime(txt_NgaySinh);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày sinh không hợp lệ: " + txt_NgaySinh);
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("+ Thiếu thông tin nội dung.");
                                            }
                                            #endregion

                                            #region 7. Quê quán
                                            if (!string.IsNullOrEmpty(txt_QueQuan))
                                            {
                                                TinhThanh tinhThanh = uow.FindObject<TinhThanh>(CriteriaOperator.Parse("TenTinhThanh Like ?", txt_QueQuan));
                                                if (tinhThanh != null)
                                                {
                                                    quanHeGiaDinh.QueQuan = tinhThanh;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Quê quán không tồn tại: " + txt_QueQuan);
                                                }
                                            }
                                            #endregion

                                            #region 8. Nơi ở hiện nay
                                            if (!string.IsNullOrEmpty(txt_NoiOHienNay))
                                            {
                                                quanHeGiaDinh.NoiOHienNay = txt_NoiOHienNay;
                                            }
                                            #endregion

                                            #region 9. Dân tộc
                                            if (!string.IsNullOrEmpty(txt_DanToc))
                                            {
                                                DanToc danToc = uow.FindObject<DanToc>(CriteriaOperator.Parse("TenDanToc Like ?", txt_DanToc));
                                                if (danToc != null)
                                                {
                                                    quanHeGiaDinh.DanToc = danToc;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Dân tộc không tồn tại: " + txt_DanToc);
                                                }
                                            }
                                            #endregion

                                            #region 10. Tôn giáo
                                            if (!string.IsNullOrEmpty(txt_TonGiao))
                                            {
                                                TonGiao tonGiao = uow.FindObject<TonGiao>(CriteriaOperator.Parse("TenTonGiao Like ?", txt_TonGiao));
                                                if (tonGiao != null)
                                                {
                                                    quanHeGiaDinh.TonGiao = tonGiao;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Tôn giáo không tồn tại: " + txt_TonGiao);
                                                }
                                            }
                                            #endregion

                                            #region 11. Quốc tịch
                                            if (!string.IsNullOrEmpty(txt_QuocTich))
                                            {
                                                QuocGia quocGia = uow.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia Like ?", txt_QuocTich));
                                                if (quocGia != null)
                                                {
                                                    quanHeGiaDinh.QuocTich = quocGia;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Quốc tịch không tồn tại: " + txt_QuocTich);
                                                }
                                            }
                                            #endregion

                                            #region 12. Nước cư trú
                                            if (!string.IsNullOrEmpty(txt_NuocCuTru))
                                            {
                                                QuocGia quocGia = uow.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia Like ?", txt_NuocCuTru));
                                                if (quocGia != null)
                                                {
                                                    quanHeGiaDinh.NuocCuTru = quocGia;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Nước cư trú không tồn tại: " + txt_NuocCuTru);
                                                }
                                            }
                                            #endregion

                                            #region 13. Năm di cư
                                            if (!string.IsNullOrEmpty(txt_NamDiCu))
                                            {
                                                try
                                                {
                                                    quanHeGiaDinh.NamDiCu = int.Parse(txt_NamDiCu);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Năm di cư không hợp lệ: " + txt_NamDiCu);
                                                }
                                            }
                                            #endregion

                                            #region 14. Nghề nghiệp
                                            if (!string.IsNullOrEmpty(txt_NgheNghiep))
                                            {
                                                quanHeGiaDinh.NgheNghiepHienTai = txt_NgheNghiep;
                                            }
                                            #endregion

                                            #region 15. Nơi làm việc
                                            if (!string.IsNullOrEmpty(txt_NoiLamViec))
                                            {
                                                quanHeGiaDinh.NoiLamViec = txt_NoiLamViec;
                                            }
                                            #endregion

                                            #region 16. Tình trạng
                                            if (!string.IsNullOrEmpty(txt_TinhTrang))
                                            {
                                                if (txt_TinhTrang.ToLower() == "còn sống")
                                                    quanHeGiaDinh.TinhTrang = TinhTrangEnum.ConSong;
                                                else if (txt_TinhTrang.ToLower() == "đã mất")
                                                    quanHeGiaDinh.TinhTrang = TinhTrangEnum.DaMat;
                                                else
                                                {
                                                    detailLog.AppendLine(" + Tình trạng không hợp lệ: " + txt_TinhTrang);
                                                }

                                            }
                                            #endregion

                                            #region 5. Ghi File log
                                            {
                                                //Đưa thông tin bị lỗi vào blog
                                                if (detailLog.Length > 0)
                                                {
                                                    mainLog.AppendLine("- STT: " + txt_STT);
                                                    mainLog.AppendLine(string.Format("- Cán bộ: {0} có người thân: {1} không import vào phần mềm được: ", txt_HoTenNhanVien, txt_HoTenNguoiThan));
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
                                        mainLog.AppendLine("- STT: " + txt_STT);
                                        mainLog.AppendLine(string.Format("- Mã quản lý của nhân viên : {0} không được trống.", txt_HoTenNhanVien));
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
                                DialogUtil.ShowInfo("Import Thành Công: " + sucessNumber + " dòng dữ liệu - Số dòng không thành công: " + erorrNumber + "-" + s + "!");

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

        #region 4. Nhập hồ sơ nhân viên từ tập tin excel - Không tạo danh mục

        public static void ImportStaffNew(IObjectSpace obs, NhanVien_ChonBoPhan obj)
        {
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            var mainLog = new StringBuilder();
            //
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel 2003 file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[ThongTinNhanVien$A4:CX]", obj.LoaiOffice))
                        {

                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx
                            //
                            #region Hồ sơ 0 --> 27
                            const int idxSTT = 0;
                            const int idxMaQuanLy = 1;
                            const int idxHo = 2;
                            const int idxTen = 3;
                            const int idxTenGoiKhac = 4;
                            const int idxNgaySinh = 5;
                            const int idxNoiSinh = 6;
                            const int idxGioiTinh = 7;
                            const int idxCMND = 8;
                            const int idxNgayCapCMND = 9;
                            const int idxNoiCapCMND = 10;
                            const int idxSoHoChieu = 11;
                            const int idxNgayCapHoChieu = 12;
                            const int idxNoiCapHoChieu = 13;
                            const int idxNgayHetHanHoChieu = 14;
                            const int idxQueQuan = 15;
                            const int idxDiaChiThuongTru = 16;
                            const int idxNoiOHienNay = 17;
                            const int idxEmail = 18;
                            const int idxDienThoaiDiDong = 19;
                            const int idxDienThoaiNhaRieng = 20;
                            const int idxTinhTrangHonNhan = 21;
                            const int idxDanToc = 22;
                            const int idxTonGiao = 23;
                            const int idxQuocTich = 24;
                            const int idxThanhPhanXuatThan = 25;
                            const int idxUuTienGiaDinh = 26;
                            const int idxUuTienBanThan = 27;
                            #endregion
                            //
                            #region Nhân viên 28 -->46
                            const int idxChucDanhCongViec = 28;
                            const int idxDonVi = 29;
                            const int idxCongViecHienNay = 30;
                            const int idxNgayTuyenDung = 31;
                            const int idxCoQuanTuyenDung = 32;
                            const int idxCongViecTuyenDung = 33;
                            const int idxNgayVaoCongTy = 34;
                            const int idxTinhTrang = 35;
                            const int idxChucVu = 36;
                            const int idxChucVuKiemNhiem = 37;
                            const int idxNgayBoNhiem = 38;
                            const int idxNgayBoNhiemKiemNhiem = 39;
                            const int idxLoaiHopDong = 40;
                            const int idxLoaiNhanSu = 41;
                            const int idxngayVaoTapDoan = 42;
                            const int idxNhomMau = 43;
                            const int idxChieuCao = 44;
                            const int idxCanNang = 45;
                            const int idxSucKhoe = 46;
                            #endregion
                            //
                            #region Thông tin lương 47 --> 67
                            const int idxMaNgach = 47;
                            const int idxTenNgach = 48;
                            const int idxNgayBoNhiemNgach = 49;
                            const int idxNgayHuongLuong = 50;
                            const int idxBacLuong = 51;
                            const int idxHeSoLuong = 52;
                            const int idxLuongCoBan = 53;
                            const int idxLuongKinhDoanh = 54;
                            const int idxHSPCChucVu = 55;
                            const int idxPCKiemNhiem = 56;
                            const int idxPCTrachNhiem = 57;
                            const int idxHSPCDoan = 58;
                            const int idxHSPCDang = 59;
                            const int idxMocNangLuongLanSau = 60;
                            const int idxMaSoThue = 61;
                            const int idxSoSoBHXH = 62;
                            const int idxNgayBatDauDongBHXH = 63;
                            const int idxSoTheBHYT = 64;
                            const int idxTuNgayBH = 65;
                            const int idxDenNgayBH = 66;
                            const int idxNoiDangKyKhamChuaBenh = 67;
                            #endregion
                            //
                            #region Thông tin trình độ 68 --> 92
                            const int idxTrinhDoVanHoa = 68;
                            const int idxChuyenNganhDaoTao_TrungHoc = 69;
                            const int idxNoiDaoTao_TrungHoc = 70;
                            const int idxHinhThucDaoTao_TrungHoc = 71;
                            const int idxNamTotNghiep_TrungHoc = 72;
                            const int idxChuyenNganhDaoTao_CaoDang = 73;
                            const int idxNoiDaoTao_CaoDang = 74;
                            const int idxHinhThucDaoTao_CaoDang = 75;
                            const int idxNamTotNghiep_CaoDang = 76;
                            const int idxChuyenNganhDaoTao_DaiHoc = 77;
                            const int idxNoiDaoTao_DaiHoc = 78;
                            const int idxHinhThucDaoTao_DaiHoc = 79;
                            const int idxNamTotNghiep_DaiHoc = 80;
                            const int idxChuyenNganhDaoTao_ThacSi = 81;
                            const int idxNoiDaoTao_ThacSi = 82;
                            const int idxHinhThucDaoTao_ThacSi = 83;
                            const int idxNamTotNghiep_ThacSi = 84;
                            const int idxChuyenNganhDaoTao_TienSi = 85;
                            const int idxNoiDaoTao_TienSi = 86;
                            const int idxHinhThucDaoTao_TienSi = 87;
                            const int idxNamTotNghiep_TienSi = 88;
                            const int idxTrinhDoCaoNhat = 89;
                            const int idxTrinhTinHoc = 90;
                            const int idxNgoaiNgu = 91;
                            const int idxTrinhNgoaiNgu = 92;
                            #endregion
                            //
                            #region Đoàn - đảng 93 --> 96

                            const int idxNgayVaoDoan = 93;
                            const int idxChucVuDoan = 94;
                            const int idxNgayVaoDang = 95;
                            const int idxChucVuDang = 96;
                            #endregion
                            //
                            #region Tài khoản ngân hàng 97 --> 100
                            const int idxTenNganHang = 97;
                            const int idxSoTaiKhoan = 98;
                            #endregion
                            //
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();

                                //Duyệt qua tất cả các dòng trong file excel
                                foreach (DataRow dr in dt.Rows)
                                {
                                    var errorLog = new StringBuilder();

                                    //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                    #region Đọc dữ liệu
                                    //
                                    #region Hồ sơ 0 --> 27
                                    string txtSTT = dr[idxSTT].ToString();
                                    string txtMaQuanLy = dr[idxMaQuanLy].ToString();
                                    string txtHo = dr[idxHo].ToString().FullTrim();;
                                    string txtTen = dr[idxTen].ToString().FullTrim();
                                    string txtTenGoiKhac = dr[idxTenGoiKhac].ToString();
                                    string txtNgaySinh = dr[idxNgaySinh].ToString();
                                    string txtNoiSinh = dr[idxNoiSinh].ToString();
                                    string txtGioiTinh = dr[idxGioiTinh].ToString();
                                    string txtCMND = dr[idxCMND].ToString();
                                    string txtNgayCapCMND = dr[idxNgayCapCMND].ToString();
                                    string txtNoiCapCMND = dr[idxNoiCapCMND].ToString();
                                    string txtSoHoChieu = dr[idxSoHoChieu].ToString();
                                    string txtNgayCapHoChieu = dr[idxNgayCapHoChieu].ToString();
                                    string txtNoiCapHoChieu = dr[idxNoiCapHoChieu].ToString();
                                    string txtNgayHetHanHoChieu = dr[idxNgayHetHanHoChieu].ToString();
                                    string txtQueQuan = dr[idxQueQuan].ToString();
                                    string txtDiaChiThuongTru = dr[idxDiaChiThuongTru].ToString();
                                    string txtNoiOHienNay = dr[idxNoiOHienNay].ToString();
                                    string txtEmail = dr[idxEmail].ToString();
                                    string txtDienThoaiDiDong = dr[idxDienThoaiDiDong].ToString();
                                    string txtDienThoaiNhaRieng = dr[idxDienThoaiNhaRieng].ToString();
                                    string txtTinhTrangHonNhan = dr[idxTinhTrangHonNhan].ToString();
                                    string txtDanToc = dr[idxDanToc].ToString();
                                    string txtTonGiao = dr[idxTonGiao].ToString();
                                    string txtQuocTich = dr[idxQuocTich].ToString();
                                    string txtThanhPhanXuatThan = dr[idxThanhPhanXuatThan].ToString();
                                    string txtUuTienGiaDinh = dr[idxUuTienGiaDinh].ToString();
                                    string txtUuTienBanThan = dr[idxUuTienBanThan].ToString();
                                    #endregion
                                    //
                                    #region Nhân viên 28 -->46
                                    string txtChucDanhCongViec = dr[idxChucDanhCongViec].ToString();
                                    string txtDonVi = dr[idxDonVi].ToString();
                                    string txtCongViecHienNay = dr[idxCongViecHienNay].ToString();
                                    string txtNgayTuyenDung = dr[idxNgayTuyenDung].ToString();
                                    string txtCoQuanTuyenDung = dr[idxCoQuanTuyenDung].ToString();
                                    string txtCongViecTuyenDung = dr[idxCongViecTuyenDung].ToString();
                                    string txtNgayVaoCongTy = dr[idxNgayVaoCongTy].ToString();
                                    string txtTinhTrang = dr[idxTinhTrang].ToString();
                                    string txtChucVu = dr[idxChucVu].ToString();
                                    string txtChucVuKiemNhiem = dr[idxChucVuKiemNhiem].ToString();
                                    string txtNgayBoNhiem = dr[idxNgayBoNhiem].ToString();
                                    string txtNgayBoNhiemKiemNhiem = dr[idxNgayBoNhiemKiemNhiem].ToString();
                                    string txtLoaiHopDong = dr[idxLoaiHopDong].ToString();
                                    string txtLoaiNhanSu = dr[idxLoaiNhanSu].ToString();
                                    string txtNgayVaoTapDoan = dr[idxngayVaoTapDoan].ToString();
                                    string txtNhomMau = dr[idxNhomMau].ToString();
                                    string txtChieuCao = dr[idxChieuCao].ToString();
                                    string txtCanNang = dr[idxCanNang].ToString();
                                    string txtSucKhoe = dr[idxSucKhoe].ToString();
                                    #endregion
                                    //
                                    #region Thông tin lương 47 --> 65
                                    string txtMaNgach = dr[idxMaNgach].ToString();
                                    string txtTenNgach = dr[idxTenNgach].ToString();
                                    string txtNgayBoNhiemNgach = dr[idxNgayBoNhiemNgach].ToString();
                                    string txtNgayHuongLuong = dr[idxNgayHuongLuong].ToString();
                                    string txtBacLuong = dr[idxBacLuong].ToString();
                                    string txtHeSoLuong = dr[idxHeSoLuong].ToString();
                                    string txtLuongCoBan = dr[idxLuongCoBan].ToString();
                                    string txtLuongKinhDoanh = dr[idxLuongKinhDoanh].ToString();
                                    string txtHSPCChucVu = dr[idxHSPCChucVu].ToString();
                                    string txtHSPCKiemNhiem = dr[idxPCKiemNhiem].ToString();
                                    string txtHSPCTrachNhiem = dr[idxPCTrachNhiem].ToString();
                                    string txtHSPCDoan = dr[idxHSPCDoan].ToString();
                                    string txtHSPCDang = dr[idxHSPCDang].ToString();
                                    string txtMocNangLuongLanSau = dr[idxMocNangLuongLanSau].ToString();
                                    string txtMaSoThue = dr[idxMaSoThue].ToString();
                                    string txtSoSoBHXH = dr[idxSoSoBHXH].ToString();
                                    string txtNgayBatDauDongBHXH = dr[idxNgayBatDauDongBHXH].ToString();
                                    string txtSoTheBHYT = dr[idxSoTheBHYT].ToString();
                                    string txtTuNgayBH = dr[idxTuNgayBH].ToString();
                                    string txtDenNgayBH = dr[idxDenNgayBH].ToString();
                                    string txtNoiDangKyKhamChuaBenh = dr[idxNoiDangKyKhamChuaBenh].ToString();
                                    #endregion
                                    //
                                    #region Thông tin trình độ 66 --> 90
                                    string txtTrinhDoVanHoa = dr[idxTrinhDoVanHoa].ToString();
                                    string txtChuyenNganhDaoTao_TrungHoc = dr[idxChuyenNganhDaoTao_TrungHoc].ToString();
                                    string txtNoiDaoTao_TrungHoc = dr[idxNoiDaoTao_TrungHoc].ToString();
                                    string txtHinhThucDaoTao_TrungHoc = dr[idxHinhThucDaoTao_TrungHoc].ToString();
                                    string txtNamTotNghiep_TrungHoc = dr[idxNamTotNghiep_TrungHoc].ToString();
                                    string txtChuyenNganhDaoTao_CaoDang = dr[idxChuyenNganhDaoTao_CaoDang].ToString();
                                    string txtNoiDaoTao_CaoDang = dr[idxNoiDaoTao_CaoDang].ToString();
                                    string txtHinhThucDaoTao_CaoDang = dr[idxHinhThucDaoTao_CaoDang].ToString();
                                    string txtNamTotNghiep_CaoDang = dr[idxNamTotNghiep_CaoDang].ToString();
                                    string txtChuyenNganhDaoTao_DaiHoc = dr[idxChuyenNganhDaoTao_DaiHoc].ToString();
                                    string txtNoiDaoTao_DaiHoc = dr[idxNoiDaoTao_DaiHoc].ToString();
                                    string txtHinhThucDaoTao_DaiHoc = dr[idxHinhThucDaoTao_DaiHoc].ToString();
                                    string txtNamTotNghiep_DaiHoc = dr[idxNamTotNghiep_DaiHoc].ToString();
                                    string txtChuyenNganhDaoTao_ThacSi = dr[idxChuyenNganhDaoTao_ThacSi].ToString();
                                    string txtNoiDaoTao_ThacSi = dr[idxNoiDaoTao_ThacSi].ToString();
                                    string txtHinhThucDaoTao_ThacSi = dr[idxHinhThucDaoTao_ThacSi].ToString();
                                    string txtNamTotNghiep_ThacSi = dr[idxNamTotNghiep_ThacSi].ToString();
                                    string txtChuyenNganhDaoTao_TienSi = dr[idxChuyenNganhDaoTao_TienSi].ToString();
                                    string txtNoiDaoTao_TienSi = dr[idxNoiDaoTao_TienSi].ToString();
                                    string txtHinhThucDaoTao_TienSi = dr[idxHinhThucDaoTao_TienSi].ToString();
                                    string txtNamTotNghiep_TienSi = dr[idxNamTotNghiep_TienSi].ToString();
                                    string txtTrinhDoCaoNhat = dr[idxTrinhDoCaoNhat].ToString();
                                    string txtTrinhTinHoc = dr[idxTrinhTinHoc].ToString();
                                    string txtNgoaiNgu = dr[idxNgoaiNgu].ToString();
                                    string txtTrinhNgoaiNgu = dr[idxTrinhNgoaiNgu].ToString();
                                    #endregion
                                    //
                                    #region Đoàn - đảng 91 --> 94

                                    string txtNgayVaoDoan = dr[idxNgayVaoDoan].ToString();
                                    string txtChucVuDoan = dr[idxChucVuDoan].ToString();
                                    string txtNgayVaoDang = dr[idxNgayVaoDang].ToString();
                                    string txtChucVuDang = dr[idxChucVuDang].ToString();
                                    #endregion
                                    //
                                    #region Tài khoản ngân hàng 95 --> 98
                                    string txtTenNganHang = dr[idxTenNganHang].ToString();
                                    string txtSoTaiKhoan = dr[idxSoTaiKhoan].ToString();
                                    #endregion
                                    //
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////

                                    #region Kiểm tra dữ

                                    ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaTapDoan = ? and CongTy = ?", txtMaQuanLy, obj.CongTy.Oid));
                                    if (nhanVien != null)
                                    {                                        
                                        //mainLog.AppendLine("- STT: " + txtSTT);
                                        //mainLog.AppendLine(string.Format("- Mã quản lý :{0} đã tồn tại trong hệ thống {1}.", txtMaQuanLy, obj.CongTy.TenBoPhan));
                                        //
                                        //sucessImport = false;

                                        #region 18. Email
                                        if (!string.IsNullOrEmpty(txtEmail))
                                        {
                                            nhanVien.Email = txtEmail;
                                        }
                                        #endregion

                                        #region 61. Mã số thuế
                                        if (!string.IsNullOrWhiteSpace(txtMaSoThue))
                                        {
                                            nhanVien.NhanVienThongTinLuong.MaSoThue = txtMaSoThue;
                                        }
                                        #endregion

                                        sucessImport = true;
                                    }
                                    else
                                    {
                                        //Tạo mới nhân viên
                                        nhanVien = new ThongTinNhanVien(uow);

                                        //Loại hồ sơ là nhân viên                                        
                                        nhanVien.LoaiHoSo = LoaiHoSoEnum.NhanVien;
                                        //

                                        #region Hồ Sơ
                                        //
                                        #region 1. Mã nhân viên
                                        if (!string.IsNullOrEmpty(txtMaQuanLy))
                                        {
                                            nhanVien.MaTapDoan = txtMaQuanLy;
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu mã quản lý nhân viên.");
                                        }
                                        #endregion

                                        #region 2.Họ
                                        if (!string.IsNullOrEmpty(txtHo))
                                        {
                                            nhanVien.Ho = txtHo;
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu thông tin họ và tên đệm.");
                                        }
                                        #endregion

                                        #region 3. Tên
                                        if (!string.IsNullOrEmpty(txtTen))
                                        {
                                            nhanVien.Ten = txtTen;
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu thông tin tên nhân viên.");
                                        }
                                        #endregion

                                        #region 4. Tên gọi khác
                                        if (!string.IsNullOrEmpty(txtTenGoiKhac))
                                        {
                                            nhanVien.TenGoiKhac = txtTenGoiKhac;
                                        }
                                        #endregion

                                        #region 5. Ngày sinh
                                        if (!String.IsNullOrWhiteSpace(txtNgaySinh))
                                        {
                                            try
                                            {
                                                DateTime ngaySinh = Convert.ToDateTime(txtNgaySinh);
                                                nhanVien.NgaySinh = ngaySinh;
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Ngày sinh không hợp lệ: " + txtNgaySinh);
                                            }
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu thông tin ngày sinh.");
                                        }
                                        #endregion

                                        #region 6. Nơi sinh
                                        if (!string.IsNullOrEmpty(txtNoiSinh))
                                        {
                                            DiaChi diaChi = new DiaChi(uow);
                                            diaChi.SoNha = txtNoiSinh;
                                            nhanVien.NoiSinh = diaChi;
                                        }
                                        #endregion

                                        #region 7. Giới tính
                                        if (!string.IsNullOrEmpty(txtGioiTinh))
                                        {
                                            if (txtGioiTinh.ToLower() == "nam")
                                                nhanVien.GioiTinh = GioiTinhEnum.Nam;
                                            else if (txtGioiTinh.ToLower() == "nữ" || txtGioiTinh.ToLower() == "nu")
                                                nhanVien.GioiTinh = GioiTinhEnum.Nu;
                                            else
                                            {
                                                errorLog.AppendLine(" + Giới tính không hợp lệ: " + txtGioiTinh);
                                            }
                                        }
                                        #endregion

                                        #region 8. Số chứng minh nhân dân
                                        if (!string.IsNullOrEmpty(txtCMND))
                                        {
                                            nhanVien.CMND = txtCMND;
                                        }
                                        #endregion

                                        #region 9. Ngày cấp CMND
                                        if (!string.IsNullOrWhiteSpace(txtNgayCapCMND))
                                        {
                                            try
                                            {
                                                DateTime ngayCap = Convert.ToDateTime(txtNgayCapCMND);
                                                nhanVien.NgayCap = ngayCap;
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Ngày cấp CMND không hợp lệ: " + txtNgayCapCMND);
                                            }
                                        }
                                        #endregion

                                        #region 10. Nơi cấp CMND
                                        if (!string.IsNullOrEmpty(txtNoiCapCMND))
                                        {
                                            //
                                            var tinh = uow.FindObject<TinhThanh>(CriteriaOperator.Parse("TenTinhThanh like ?", txtNoiCapCMND));
                                            if (tinh != null)
                                            {
                                                //
                                                nhanVien.NoiCap = tinh;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Nơi cấp CMND không hợp lệ: " + txtNoiCapCMND);
                                            }
                                        }
                                        #endregion

                                        #region 11. Hộ Chiếu
                                        if (!string.IsNullOrEmpty(txtSoHoChieu))
                                        {
                                            nhanVien.SoHoChieu = txtSoHoChieu;
                                        }
                                        #endregion

                                        #region 12. Ngày cấp hộ chiếu
                                        if (!string.IsNullOrWhiteSpace(txtNgayCapHoChieu))
                                        {
                                            try
                                            {
                                                DateTime ngayCapHoChieu = Convert.ToDateTime(dr[txtNgayCapHoChieu]);
                                                nhanVien.NgayCapHoChieu = ngayCapHoChieu;
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Ngày cấp hộ chiếu không hợp lệ: " + txtNgayCapHoChieu);
                                            }
                                        }
                                        #endregion

                                        #region 13. Nơi cấp hộ chiếu
                                        if (!string.IsNullOrEmpty(txtNoiCapHoChieu))
                                        {//                                
                                            nhanVien.NoiCapHoChieu = txtNoiCapHoChieu;
                                        }
                                        #endregion

                                        #region 14. Ngày hết hạn hộ chiếu
                                        if (!string.IsNullOrWhiteSpace(txtNgayHetHanHoChieu))
                                        {
                                            DateTime? ngay = null;
                                            try
                                            {
                                                DateTime ngayHetHan = Convert.ToDateTime(txtNgayHetHanHoChieu);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Ngày hết hạn hộ chiếu không hợp lệ: " + txtNgayHetHanHoChieu);
                                            }
                                        }
                                        #endregion

                                        #region 15. Quê quán
                                        if (!string.IsNullOrEmpty(txtQueQuan))
                                        {
                                            //Quê quán
                                            DiaChi diaChi = new DiaChi(uow);
                                            diaChi.SoNha = txtQueQuan;
                                            nhanVien.QueQuan = diaChi;
                                        }
                                        #endregion

                                        #region 16. Địa chỉ thường trú
                                        if (!string.IsNullOrEmpty(txtDiaChiThuongTru))
                                        {
                                            //Địa chỉ thường trú
                                            DiaChi diaChi = new DiaChi(uow);
                                            diaChi.SoNha = txtDiaChiThuongTru;
                                            nhanVien.DiaChiThuongTru = diaChi;
                                        }
                                        #endregion

                                        #region 17. Nơi ở hiện nay
                                        if (!string.IsNullOrEmpty(txtNoiOHienNay))
                                        {
                                            //Nơi ở hiện nay
                                            DiaChi diaChi = new DiaChi(uow);
                                            diaChi.SoNha = txtNoiOHienNay;
                                            nhanVien.NoiOHienNay = diaChi;
                                        }
                                        #endregion

                                        #region 18. Email
                                        if (!string.IsNullOrEmpty(txtEmail))
                                        {
                                            nhanVien.Email = txtEmail;
                                        }
                                        #endregion

                                        #region  19. Điện thoại di động
                                        if (!string.IsNullOrEmpty(txtDienThoaiDiDong))
                                        {
                                            nhanVien.DienThoaiDiDong = txtDienThoaiDiDong;
                                        }
                                        #endregion

                                        #region 20. Điện thoại nhà riêng
                                        if (!string.IsNullOrEmpty(txtDienThoaiNhaRieng))
                                        {
                                            nhanVien.DienThoaiNhaRieng = txtDienThoaiNhaRieng;
                                        }
                                        #endregion

                                        #region  21. Tình trạng hôn nhân
                                        if (!string.IsNullOrEmpty(txtTinhTrangHonNhan))
                                        {
                                            TinhTrangHonNhan honNhan = uow.FindObject<TinhTrangHonNhan>(CriteriaOperator.Parse("TenTinhTrangHonNhan like ?", txtTinhTrangHonNhan));
                                            if (honNhan != null)
                                            {
                                                nhanVien.TinhTrangHonNhan = honNhan;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Tình trạng hôn nhân không hợp lệ: " + txtTinhTrangHonNhan);
                                            }
                                        }
                                        #endregion

                                        #region 22. Dân tộc
                                        if (!string.IsNullOrEmpty(txtDanToc))
                                        {
                                            DanToc danToc = null;
                                            danToc = uow.FindObject<DanToc>(CriteriaOperator.Parse("TenDanToc like ?", txtDanToc));
                                            if (danToc != null)
                                            {
                                                //
                                                nhanVien.DanToc = danToc;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Dân tộc không hợp lệ: " + txtDanToc);
                                            }
                                        }
                                        #endregion

                                        #region 23. Tôn giáo
                                        if (!string.IsNullOrEmpty(txtTonGiao))
                                        {
                                            TonGiao tonGiao = null;
                                            tonGiao = uow.FindObject<TonGiao>(CriteriaOperator.Parse("TenTonGiao like ?", txtTonGiao));
                                            if (tonGiao != null)
                                            {
                                                nhanVien.TonGiao = tonGiao;
                                            }
                                            else
                                            {

                                                errorLog.AppendLine(" + Tôn giáo không hợp lệ: " + txtTonGiao);
                                            }
                                        }
                                        #endregion

                                        #region 24. Quốc tịch
                                        if (!string.IsNullOrEmpty(txtQuocTich))
                                        {
                                            QuocGia quocTich = uow.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia like ?", txtQuocTich));
                                            if (quocTich != null)
                                            {
                                                nhanVien.QuocTich = quocTich;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Quốc gia không hợp lệ: " + txtQuocTich);
                                            }
                                        }
                                        else
                                        {
                                            //errorLog.AppendLine(" + Thiếu quốc tịch.");
                                        }
                                        #endregion
                                        //
                                        #endregion

                                        //

                                        #region Nhân viên
                                        //
                                        #region 28. Chức Danh - Công việc
                                        if (!string.IsNullOrEmpty(txtChucDanhCongViec))
                                        {
                                            ChucDanh chucDanh = null;
                                            chucDanh = uow.FindObject<ChucDanh>(CriteriaOperator.Parse("TenChucDanh like ?", txtChucDanhCongViec));
                                            if (chucDanh != null)
                                            {
                                                nhanVien.ChucDanh = chucDanh;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Chức danh không hợp lệ: " + txtChucDanhCongViec);
                                            }
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu chức danh công việc.");
                                        }
                                        #endregion

                                        #region 29. Bộ phận
                                        {
                                            if (obj.TatCa == false)
                                            {
                                                nhanVien.BoPhan = uow.GetObjectByKey<BoPhan>(obj.BoPhan.Oid);                                                
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(txtDonVi))
                                                {
                                                    BoPhan boPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan Like ? and CongTy.Oid = ?", txtDonVi, obj.CongTy.Oid));
                                                    if (boPhan != null)
                                                    {
                                                        nhanVien.BoPhan = boPhan;
                                                    }
                                                    else
                                                    {
                                                        errorLog.AppendLine(string.Format(" + {0} không được tìm thấy trong {1}: ", txtDonVi, obj.CongTy.TenBoPhan.ToString()));
                                                    }
                                                }
                                                else
                                                {
                                                    errorLog.AppendLine(" + Bộ phận không được để trống.");
                                                }
                                            }
                                        }
                                        #endregion

                                        #region 30. Công việc hiện nay
                                        if (!string.IsNullOrEmpty(txtCongViecHienNay))
                                        {
                                            CongViec congViec = uow.FindObject<CongViec>(CriteriaOperator.Parse("TenCongViec Like ?", txtCongViecHienNay));
                                            if (congViec != null)
                                            {
                                                nhanVien.CongViecHienNay = congViec;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Công việc hiện nay không hợp lệ: " + txtCongViecHienNay);
                                            }
                                        }
                                        #endregion

                                        #region 31. Ngày tuyển dụng
                                        if (!string.IsNullOrWhiteSpace(txtNgayTuyenDung))
                                        {
                                            try
                                            {
                                                DateTime ngayTuyenDung = Convert.ToDateTime(txtNgayTuyenDung);
                                                nhanVien.NgayTuyenDung = ngayTuyenDung;
                                            }
                                            catch
                                            {
                                                errorLog.AppendLine(" + Ngày tuyển dụng lần đầu không hợp lệ: " + txtNgayTuyenDung);
                                            }
                                        }
                                        #endregion

                                        #region 32. Cơ quan tuyển dụng
                                        if (!string.IsNullOrEmpty(txtCoQuanTuyenDung))
                                        {
                                            nhanVien.DonViTuyenDung = txtCoQuanTuyenDung;
                                        }
                                        #endregion

                                        #region  33. Công việc tuyển dụng
                                        //if (!string.IsNullOrEmpty(txtCongViecTuyenDung))
                                        //{
                                        //    nhanVien.DonViTuyenDung = txtCongViecTuyenDung;
                                        //}
                                        #endregion

                                        #region 34. Ngày vào công ty
                                        if (!string.IsNullOrWhiteSpace(txtNgayVaoCongTy))
                                        {
                                            try
                                            {
                                                DateTime ngayVaoCongTy = Convert.ToDateTime(txtNgayVaoCongTy);
                                                nhanVien.NgayVaoCongTy = ngayVaoCongTy;
                                            }
                                            catch
                                            {
                                                errorLog.AppendLine(" + Ngày vào công ty không hợp lệ: " + txtNgayVaoCongTy);
                                            }
                                        }
                                        #endregion

                                        #region 35. Tình trạng nhân viên
                                        if (!string.IsNullOrEmpty(txtTinhTrang))
                                        {
                                            TinhTrang tinhTrang = null;
                                            tinhTrang = uow.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", txtTinhTrang));
                                            if (tinhTrang != null)
                                            {
                                                nhanVien.TinhTrang = tinhTrang;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Tình trạng không hợp lệ: " + txtTinhTrang);
                                            }
                                        }
                                        #endregion

                                        #region 36. Chức vụ chính
                                        if (!string.IsNullOrEmpty(txtChucVu))
                                        {
                                            ChucVu chucVu = uow.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu like ?", txtChucVu));
                                            if (chucVu != null)
                                            {
                                                nhanVien.ChucVu = chucVu;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Chức vụ không hợp lệ: " + txtChucVu);
                                            }
                                        }
                                        #endregion

                                        #region 37.39 Chức vụ kiêm nhiệm
                                        if (!string.IsNullOrEmpty(txtChucVuKiemNhiem))
                                        {
                                            ChucVuKiemNhiem chucVuKiemNhiem = new ChucVuKiemNhiem(uow);
                                            chucVuKiemNhiem.NhanVien = nhanVien;
                                            chucVuKiemNhiem.BoPhan = nhanVien.BoPhan;
                                            //
                                            ChucVu chucVu = uow.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu like ?", txtChucVuKiemNhiem));
                                            if (chucVu != null)
                                            {
                                                //
                                                chucVuKiemNhiem.ChucVu = chucVu;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Chức vụ kiêm nhiệm không hợp lệ: " + txtChucVuKiemNhiem);
                                            }

                                            #region 39. Ngày bổ nhiệm kiêm nhiệm
                                            if (!string.IsNullOrWhiteSpace(txtNgayBoNhiemKiemNhiem))
                                            {
                                                try
                                                {
                                                    DateTime ngayBoNhiemKiemNhiem = Convert.ToDateTime(txtNgayBoNhiemKiemNhiem);
                                                    chucVuKiemNhiem.NgayBoNhiem = ngayBoNhiemKiemNhiem;
                                                }
                                                catch
                                                {
                                                    errorLog.AppendLine(" + Ngày vào bổ nhiệm kiêm nhiệm không hợp lệ: " + txtNgayBoNhiemKiemNhiem);
                                                }
                                            }
                                            #endregion
                                        }
                                        #endregion

                                        #region 38. Ngày bổ nhiệm
                                        if (!string.IsNullOrWhiteSpace(txtNgayBoNhiem))
                                        {
                                            try
                                            {
                                                DateTime ngayBoNhiem = Convert.ToDateTime(txtNgayBoNhiem);
                                                nhanVien.NgayBoNhiemChucVu = ngayBoNhiem;
                                            }
                                            catch
                                            {
                                                errorLog.AppendLine(" + Ngày vào bổ nhiệm chức vụ không hợp lệ: " + txtNgayBoNhiem);
                                            }
                                        }
                                        #endregion

                                        #region 40. Loại hợp đồng
                                        if (!string.IsNullOrEmpty(txtLoaiHopDong))
                                        {
                                            LoaiHopDong loaiHopDong = uow.FindObject<LoaiHopDong>(CriteriaOperator.Parse("TenLoaiHopDong like ?", txtLoaiHopDong));
                                            if (loaiHopDong != null)
                                            {
                                                nhanVien.LoaiHopDong = loaiHopDong;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Loại hợp đồng không hợp lệ: " + txtLoaiHopDong);
                                            }
                                        }
                                        #endregion

                                        #region 41. Loại nhân sự
                                        if (!string.IsNullOrEmpty(txtLoaiNhanSu))
                                        {
                                            LoaiNhanSu loaiNhanSu = uow.FindObject<LoaiNhanSu>(CriteriaOperator.Parse("TenLoaiNhanSu Like ?", txtLoaiNhanSu));
                                            if (loaiNhanSu != null)
                                            {
                                                nhanVien.LoaiNhanSu = loaiNhanSu;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Loại nhân sự không hợp lệ: " + txtLoaiNhanSu);
                                            }
                                        }
                                        #endregion

                                        #region 42. Ngày vào biên chế
                                        if (!string.IsNullOrWhiteSpace(txtNgayVaoTapDoan))
                                        {
                                            try
                                            {
                                                DateTime ngayVaoTapDoan = Convert.ToDateTime(txtNgayVaoTapDoan);
                                                nhanVien.NgayVaoTapDoan = ngayVaoTapDoan;
                                            }
                                            catch
                                            {
                                                errorLog.AppendLine(" + Ngày vào biên chế không hợp lệ: " + txtNgayVaoTapDoan);
                                            }
                                        }
                                        #endregion

                                        #region 43. Nhóm máu
                                        if (!string.IsNullOrEmpty(txtNhomMau))
                                        {
                                            NhomMau nhomMau = uow.FindObject<NhomMau>(CriteriaOperator.Parse("TenNhomMau like ?", txtNhomMau));
                                            if (nhomMau != null)
                                            {
                                                nhanVien.NhomMau = nhomMau;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Nhóm máu không hợp lệ: " + txtNhomMau);
                                            }
                                        }
                                        #endregion

                                        #region 44. Chiều cao
                                        Decimal chieuCao;
                                        if (Decimal.TryParse(txtChieuCao, out chieuCao))
                                        {
                                            nhanVien.ChieuCao = Convert.ToInt32(chieuCao * 100);
                                        }
                                        #endregion

                                        #region 45. Cân nặng
                                        Decimal canNang;
                                        if (Decimal.TryParse(txtCanNang, out canNang))
                                        {
                                            nhanVien.CanNang = Convert.ToInt32(canNang * 100); ;
                                        }
                                        #endregion

                                        #region 46. Tình trạng sức khỏe
                                        {
                                            if (!string.IsNullOrEmpty(txtSucKhoe))
                                            {
                                                SucKhoe sucKhoe = uow.FindObject<SucKhoe>(CriteriaOperator.Parse("TenSucKhoe like ?", txtSucKhoe));
                                                if (sucKhoe != null)
                                                {
                                                    nhanVien.SucKhoe = sucKhoe;
                                                }
                                                else
                                                {
                                                    errorLog.AppendLine(" + Tình trạng sức khỏe không hợp lệ: " + txtSucKhoe);
                                                }
                                            }
                                        }
                                        #endregion

                                        #endregion

                                        //

                                        #region Thông tin lương
                                        //
                                        #region 47 --> 54 Ngạch lương
                                        if (!string.IsNullOrEmpty(txtMaNgach))
                                        {
                                            NgachLuong ngach = uow.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy like ?", txtMaNgach));
                                            //                                    
                                            if (ngach != null)
                                            {
                                                nhanVien.NhanVienThongTinLuong.NgachLuong = ngach;

                                                #region 48. Bậc lương
                                                if (!string.IsNullOrEmpty(txtBacLuong))
                                                {
                                                    BacLuong bacLuong = null;
                                                    bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong = ? and MaQuanLy = ?", ngach.Oid, txtBacLuong));
                                                    //
                                                    if (bacLuong != null)
                                                    {
                                                        nhanVien.NhanVienThongTinLuong.BacLuong = bacLuong;
                                                        nhanVien.NhanVienThongTinLuong.LuongCoBan = bacLuong.LuongCoBan;
                                                        nhanVien.NhanVienThongTinLuong.LuongKinhDoanh = bacLuong.LuongKinhDoanh;
                                                    }
                                                    else
                                                    {
                                                        errorLog.AppendLine(" + Bậc lương không tồn tại: " + txtBacLuong);
                                                    }
                                                }
                                                else
                                                {
                                                    errorLog.AppendLine(" + Thiếu thông tin bậc lương.");
                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Ngạch lương không tồn tại: " + txtMaNgach);
                                            }
                                        }
                                        else
                                        {
                                            //errorLog.AppendLine(" + Thiếu thông tin ngạch lương.");
                                        }

                                        #endregion

                                        #region 49. Ngày bổ nhiệm ngạch
                                        if (!string.IsNullOrWhiteSpace(txtNgayBoNhiemNgach))
                                        {
                                            try
                                            {
                                                DateTime ngayBoNhiemNgach = Convert.ToDateTime(txtNgayBoNhiemNgach);
                                                nhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach = ngayBoNhiemNgach;
                                            }
                                            catch
                                            {
                                                errorLog.AppendLine(" + Ngày bổ nhiệm ngạch không hợp lệ: " + txtNgayBoNhiemNgach);
                                            }
                                        }
                                        #endregion

                                        #region 50. Ngày hưởng lương
                                        if (!string.IsNullOrWhiteSpace(txtNgayHuongLuong))
                                        {
                                            try
                                            {
                                                DateTime ngayHuongLuong = Convert.ToDateTime(txtNgayHuongLuong);
                                                nhanVien.NhanVienThongTinLuong.NgayHuongLuong = ngayHuongLuong;
                                            }
                                            catch
                                            {
                                                errorLog.AppendLine(" + Ngày hưởng lương không hợp lệ: " + txtNgayHuongLuong);
                                            }
                                        }
                                        #endregion

                                        #region 55. Hệ số chức vụ
                                        Decimal heSoChucVu;
                                        if (Decimal.TryParse(txtHSPCChucVu, out heSoChucVu))
                                        {
                                            nhanVien.NhanVienThongTinLuong.HSPCChucVu = heSoChucVu;
                                        }
                                        #endregion

                                        #region 56. Hệ số kiêm nhiệm
                                        Decimal PhuCapKiemNhiem;
                                        if (Decimal.TryParse(txtHSPCKiemNhiem, out PhuCapKiemNhiem))
                                        {
                                            nhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem = PhuCapKiemNhiem;
                                        }
                                        #endregion

                                        #region 57. Hệ số trách nhiệm
                                        Decimal PhuCapTrachNhiem;
                                        if (Decimal.TryParse(txtHSPCTrachNhiem, out PhuCapTrachNhiem))
                                        {
                                            nhanVien.NhanVienThongTinLuong.PhuCapTrachNhiem = PhuCapTrachNhiem;
                                        }
                                        #endregion

                                        #region 58. Hệ số pc đoàn
                                        Decimal heSoDoan;
                                        if (Decimal.TryParse(txtHSPCDoan, out heSoDoan))
                                        {
                                            nhanVien.NhanVienThongTinLuong.HSPCChucVuDoan = heSoDoan;
                                        }
                                        #endregion

                                        #region 59. Hệ số pc đảng
                                        Decimal heSoDang;
                                        if (Decimal.TryParse(txtHSPCDang, out heSoDang))
                                        {
                                            nhanVien.NhanVienThongTinLuong.HSPCChucVuDang = PhuCapKiemNhiem;
                                        }
                                        #endregion

                                        #region 60. Mốc tính lương lần sau
                                        if (!string.IsNullOrWhiteSpace(txtMocNangLuongLanSau))
                                        {
                                            try
                                            {
                                                DateTime mocTinhLuongLanSau = Convert.ToDateTime(txtMocNangLuongLanSau);
                                                nhanVien.NhanVienThongTinLuong.MocNangLuongLanSau = mocTinhLuongLanSau;
                                            }
                                            catch
                                            {
                                                errorLog.AppendLine(" + Mốc tính lương lần sau không hợp lệ: " + txtMocNangLuongLanSau);
                                            }
                                        }
                                        #endregion

                                        #region 61. Mã số thuế
                                        if (!string.IsNullOrWhiteSpace(txtMaSoThue))
                                        {
                                            nhanVien.NhanVienThongTinLuong.MaSoThue = txtMaSoThue;
                                        }
                                        #endregion

                                        #endregion

                                        //

                                        #region Thông bảo hiểm

                                        #region 62 --> 67
                                        if (!String.IsNullOrWhiteSpace(txtSoSoBHXH))
                                        {
                                            HoSoBaoHiem hoSoBaoHiem = uow.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien.MaTapDoan like ?", nhanVien.MaTapDoan));
                                            //nếu nhân viên đó chưa có bảo hiểm thì thêm hồ sơ bảo hiêm
                                            if (hoSoBaoHiem == null)
                                            {
                                                hoSoBaoHiem = new HoSoBaoHiem(uow);
                                                hoSoBaoHiem.ThongTinNhanVien = nhanVien;
                                                hoSoBaoHiem.SoSoBHXH = txtSoSoBHXH;

                                                #region 63. Ngày tham gia bảo hiểm xã hội
                                                if (!String.IsNullOrWhiteSpace(txtNgayBatDauDongBHXH))
                                                {
                                                    try
                                                    {
                                                        DateTime ngay = Convert.ToDateTime(txtNgayBatDauDongBHXH);
                                                        hoSoBaoHiem.NgayThamGiaBHXH = ngay;
                                                    }
                                                    catch
                                                    {
                                                        errorLog.AppendLine(" + Ngày tham gia bảo hiểm XH không hợp lệ: " + txtNgayBatDauDongBHXH);
                                                    }
                                                }
                                                #endregion

                                                #region 64. Thẻ bảo hiểm y tế
                                                hoSoBaoHiem.SoTheBHYT = txtSoTheBHYT;
                                                #endregion

                                                #region 65. Từ ngày
                                                if (!String.IsNullOrWhiteSpace(txtTuNgayBH))
                                                {
                                                    try
                                                    {
                                                        DateTime ngay = Convert.ToDateTime(txtTuNgayBH);
                                                        hoSoBaoHiem.TuNgay = ngay;
                                                    }
                                                    catch
                                                    {
                                                        errorLog.AppendLine(" + Từ ngày tham gia bảo hiểm XH không hợp lệ: " + txtTuNgayBH);
                                                    }
                                                }
                                                #endregion

                                                #region 66. Đến ngày
                                                if (!String.IsNullOrWhiteSpace(txtDenNgayBH))
                                                {
                                                    try
                                                    {
                                                        DateTime ngay = Convert.ToDateTime(txtDenNgayBH);
                                                        hoSoBaoHiem.DenNgay = ngay;
                                                    }
                                                    catch
                                                    {
                                                        errorLog.AppendLine(" + Đến ngày tham gia bảo hiểm XH không hợp lệ: " + txtDenNgayBH);
                                                    }
                                                }
                                                #endregion

                                                #region 67. Nơi đăng ký khám chữa bệnh
                                                if (!String.IsNullOrWhiteSpace(txtNoiDangKyKhamChuaBenh))
                                                {
                                                    BenhVien benhVien = null;
                                                    benhVien = uow.FindObject<BenhVien>(CriteriaOperator.Parse("TenBenhVien Like ?", txtNoiDangKyKhamChuaBenh));
                                                    if (benhVien != null)
                                                    {
                                                        hoSoBaoHiem.NoiDangKyKhamChuaBenh = benhVien;
                                                    }
                                                    else
                                                    {
                                                        errorLog.AppendLine(" + Nơi khám chữa bệnh không hợp lệ: " + txtNoiDangKyKhamChuaBenh);
                                                    }
                                                }
                                                #endregion
                                            }
                                        }
                                        #endregion

                                        #endregion

                                        //

                                        #region Thông tin trình độ

                                        #region 68. Trình độ văn hóa
                                        {
                                            if (!string.IsNullOrEmpty(txtTrinhDoVanHoa))
                                            {
                                                TrinhDoVanHoa trinhDo = uow.FindObject<TrinhDoVanHoa>(CriteriaOperator.Parse("TenTrinhDoVanHoa Like ?", txtTrinhDoVanHoa));
                                                if (trinhDo != null)
                                                {
                                                    nhanVien.NhanVienTrinhDo.TrinhDoVanHoa = trinhDo;
                                                }
                                                else
                                                {
                                                    errorLog.AppendLine(" + Trình độ văn hóa không hợp lệ: " + txtTrinhDoVanHoa);
                                                }
                                            }
                                        }
                                        #endregion

                                        #region 69 --> 72 Trình độ trung cấp
                                        if (!String.IsNullOrEmpty(txtChuyenNganhDaoTao_TrungHoc))
                                        {
                                            TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ? or TenTrinhDoChuyenMon Like ?", "trung cấp", "trung học%"));
                                            if (trinhDo == null)
                                            {
                                                VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.MaTapDoan like ? and TrinhDoChuyenMon = ?", nhanVien.MaTapDoan, trinhDo));
                                                if (bang != null)
                                                {
                                                    bang = new VanBang(uow);
                                                    bang.HoSo = nhanVien;
                                                    bang.TrinhDoChuyenMon = trinhDo;
                                                    nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDo;

                                                    ChuyenNganhDaoTao chuyenMon = null;
                                                    chuyenMon = uow.FindObject<ChuyenNganhDaoTao>(CriteriaOperator.Parse("TenChuyenNganhDaoTao like ?", txtChuyenNganhDaoTao_TrungHoc));
                                                    if (chuyenMon != null)
                                                    {
                                                        bang.ChuyenNganhDaoTao = chuyenMon;
                                                        nhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao = chuyenMon;
                                                    }
                                                    else
                                                    {
                                                        errorLog.AppendLine(" + Chuyên ngành đào tạo - Trung cấp không hợp lệ: " + txtChuyenNganhDaoTao_TrungHoc);
                                                    }
                                                    //
                                                    if (!String.IsNullOrEmpty(txtNoiDaoTao_TrungHoc))
                                                    {
                                                        TruongDaoTao truong = null;
                                                        truong = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao like ?", txtNoiDaoTao_TrungHoc));
                                                        if (truong != null)
                                                        {
                                                            bang.TruongDaoTao = truong;
                                                            nhanVien.NhanVienTrinhDo.TruongDaoTao = truong;
                                                        }
                                                        else
                                                        {
                                                            errorLog.AppendLine(" + Trường đạo đào tạo - Trung cấp không hợp lệ: " + txtNoiDaoTao_TrungHoc);
                                                        }
                                                    }
                                                    //
                                                    if (!String.IsNullOrEmpty(txtHinhThucDaoTao_TrungHoc))
                                                    {
                                                        HinhThucDaoTao hinhThuc = null;
                                                        hinhThuc = uow.FindObject<HinhThucDaoTao>(CriteriaOperator.Parse("TenHinhThucDaoTao like ?", txtHinhThucDaoTao_TrungHoc));
                                                        if (hinhThuc != null)
                                                        {
                                                            bang.HinhThucDaoTao = hinhThuc;
                                                            nhanVien.NhanVienTrinhDo.HinhThucDaoTao = hinhThuc;
                                                        }
                                                        else
                                                        {
                                                            errorLog.AppendLine(" + Hình thức đào tạo - Trung cấp không hợp lệ: " + txtHinhThucDaoTao_TrungHoc);
                                                        }
                                                    }

                                                    #region năm công nhận tốt nghiệp
                                                    int namTotNghiep;
                                                    if (int.TryParse(txtNamTotNghiep_TrungHoc, out namTotNghiep))
                                                    {
                                                        bang.NamTotNghiep = namTotNghiep;
                                                        nhanVien.NhanVienTrinhDo.NamTotNghiep = namTotNghiep;
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                        #endregion

                                        #region 73 --> 76 Trình độ cao đẳng
                                        if (!String.IsNullOrEmpty(txtChuyenNganhDaoTao_CaoDang))
                                        {
                                            TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ? ", "cao đẳng"));
                                            if (trinhDo != null)
                                            {
                                                VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.MaTapDoan like ? and TrinhDoChuyenMon = ?", nhanVien.MaTapDoan, trinhDo));
                                                if (bang == null)
                                                {
                                                    bang = new VanBang(uow);
                                                    bang.HoSo = nhanVien;
                                                    bang.TrinhDoChuyenMon = trinhDo;
                                                    nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDo;

                                                    ChuyenNganhDaoTao chuyenMon = null;
                                                    chuyenMon = uow.FindObject<ChuyenNganhDaoTao>(CriteriaOperator.Parse("TenChuyenNganhDaoTao like ?", txtChuyenNganhDaoTao_CaoDang));
                                                    if (chuyenMon != null)
                                                    {
                                                        bang.ChuyenNganhDaoTao = chuyenMon;
                                                        nhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao = chuyenMon;
                                                    }
                                                    else
                                                    {
                                                        errorLog.AppendLine(" + Chuyên ngành đào tạo - Cao đẳng không hợp lệ: " + txtChuyenNganhDaoTao_CaoDang);
                                                    }

                                                    //
                                                    if (!String.IsNullOrEmpty(txtNoiDaoTao_CaoDang))
                                                    {
                                                        TruongDaoTao truong = null;
                                                        truong = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao like ?", txtNoiDaoTao_CaoDang));
                                                        if (truong != null)
                                                        {
                                                            bang.TruongDaoTao = truong;
                                                            nhanVien.NhanVienTrinhDo.TruongDaoTao = truong;
                                                        }
                                                        else
                                                        {
                                                            errorLog.AppendLine(" + Trường đào tạo - Cao đẳng không hợp lệ: " + txtNoiDaoTao_CaoDang);
                                                        }
                                                    }
                                                    //
                                                    if (!String.IsNullOrEmpty(txtHinhThucDaoTao_CaoDang))
                                                    {
                                                        HinhThucDaoTao hinhThuc = null;
                                                        hinhThuc = uow.FindObject<HinhThucDaoTao>(CriteriaOperator.Parse("TenHinhThucDaoTao like ?", txtHinhThucDaoTao_CaoDang));
                                                        if (hinhThuc != null)
                                                        {
                                                            bang.HinhThucDaoTao = hinhThuc;
                                                            nhanVien.NhanVienTrinhDo.HinhThucDaoTao = hinhThuc;
                                                        }
                                                        else
                                                        {
                                                            errorLog.AppendLine(" + Hình thức đào tạo - Cao đẳng không hợp lệ: " + txtHinhThucDaoTao_CaoDang);
                                                        }
                                                    }
                                                    #region năm công nhận tốt nghiệp
                                                    int namTotNghiep;
                                                    if (int.TryParse(txtNamTotNghiep_CaoDang, out namTotNghiep))
                                                    {
                                                        bang.NamTotNghiep = namTotNghiep;
                                                        nhanVien.NhanVienTrinhDo.NamTotNghiep = namTotNghiep;
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                        #endregion

                                        #region 77 --> 80 Trình độ đại học
                                        if (!String.IsNullOrEmpty(txtChuyenNganhDaoTao_DaiHoc))
                                        {
                                            TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ? or TenTrinhDoChuyenMon Like ? or TenTrinhDoChuyenMon Like ?", "đại học", "cử nhân", "kỹ sư"));
                                            if (trinhDo != null)
                                            {
                                                VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.MaTapDoan like ? and TrinhDoChuyenMon = ?", nhanVien.MaTapDoan, trinhDo));
                                                if (bang == null)
                                                {
                                                    bang = new VanBang(uow);
                                                    bang.HoSo = nhanVien;
                                                    bang.TrinhDoChuyenMon = trinhDo;
                                                    nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDo;

                                                    ChuyenNganhDaoTao chuyenMon = null;
                                                    chuyenMon = uow.FindObject<ChuyenNganhDaoTao>(CriteriaOperator.Parse("TenChuyenNganhDaoTao like ?", txtChuyenNganhDaoTao_DaiHoc));
                                                    if (chuyenMon != null)
                                                    {
                                                        bang.ChuyenNganhDaoTao = chuyenMon;
                                                        nhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao = chuyenMon;
                                                    }
                                                    else
                                                    {
                                                        errorLog.AppendLine(" + Chuyên ngành đào tạo - Đại học không hợp lệ: " + txtChuyenNganhDaoTao_DaiHoc);
                                                    }

                                                    //
                                                    if (!String.IsNullOrEmpty(txtNoiDaoTao_DaiHoc))
                                                    {
                                                        TruongDaoTao truong = null;
                                                        truong = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao like ?", txtNoiDaoTao_DaiHoc));
                                                        if (truong != null)
                                                        {
                                                            bang.TruongDaoTao = truong;
                                                            nhanVien.NhanVienTrinhDo.TruongDaoTao = truong;
                                                        }
                                                        else
                                                        {
                                                            errorLog.AppendLine(" + Trường đào tạo - Đại học không hợp lệ: " + txtNoiDaoTao_DaiHoc);
                                                        }
                                                    }
                                                    //
                                                    if (!String.IsNullOrEmpty(txtHinhThucDaoTao_DaiHoc))
                                                    {
                                                        HinhThucDaoTao hinhThuc = null;
                                                        hinhThuc = uow.FindObject<HinhThucDaoTao>(CriteriaOperator.Parse("TenHinhThucDaoTao like ?", txtHinhThucDaoTao_DaiHoc));
                                                        if (hinhThuc != null)
                                                        {
                                                            bang.HinhThucDaoTao = hinhThuc;
                                                            nhanVien.NhanVienTrinhDo.HinhThucDaoTao = hinhThuc;
                                                        }
                                                        else
                                                        {
                                                            errorLog.AppendLine(" + Hình thức đào tạo - Đại học không hợp lệ: " + txtHinhThucDaoTao_DaiHoc);
                                                        }
                                                    }
                                                    #region năm công nhận tốt nghiệp
                                                    int namTotNghiep;
                                                    if (int.TryParse(txtNamTotNghiep_DaiHoc, out namTotNghiep))
                                                    {
                                                        bang.NamTotNghiep = namTotNghiep;
                                                        nhanVien.NhanVienTrinhDo.NamTotNghiep = namTotNghiep;
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                        #endregion

                                        #region 81 --> 84 Trình độ thạc sĩ
                                        if (!String.IsNullOrEmpty(txtChuyenNganhDaoTao_ThacSi))
                                        {
                                            TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ? or TenTrinhDoChuyenMon Like ?", "thạc sĩ", "thạc sỹ"));
                                            if (trinhDo != null)
                                            {
                                                VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.MaTapDoan like ? and TrinhDoChuyenMon = ?", nhanVien.MaTapDoan, trinhDo));
                                                if (bang == null)
                                                {
                                                    bang = new VanBang(uow);
                                                    bang.HoSo = nhanVien;
                                                    bang.TrinhDoChuyenMon = trinhDo;
                                                    nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDo;

                                                    ChuyenNganhDaoTao chuyenMon = null;
                                                    chuyenMon = uow.FindObject<ChuyenNganhDaoTao>(CriteriaOperator.Parse("TenChuyenNganhDaoTao like ?", txtChuyenNganhDaoTao_ThacSi));
                                                    if (chuyenMon != null)
                                                    {
                                                        bang.ChuyenNganhDaoTao = chuyenMon;
                                                        nhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao = chuyenMon;
                                                    }
                                                    else
                                                    {
                                                        errorLog.AppendLine(" + Chuyên ngành đào tạo - Thạc sĩ không hợp lệ: " + txtChuyenNganhDaoTao_ThacSi);
                                                    }

                                                    //
                                                    if (!String.IsNullOrEmpty(txtNoiDaoTao_ThacSi))
                                                    {
                                                        TruongDaoTao truong = null;
                                                        truong = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao like ?", txtNoiDaoTao_ThacSi));
                                                        if (truong != null)
                                                        {
                                                            bang.TruongDaoTao = truong;
                                                            nhanVien.NhanVienTrinhDo.TruongDaoTao = truong;
                                                        }
                                                        else
                                                        {
                                                            errorLog.AppendLine(" + Trường đào tạo - Thạc sĩ không hợp lệ: " + txtNoiDaoTao_ThacSi);
                                                        }
                                                    }
                                                    //
                                                    if (!String.IsNullOrEmpty(txtHinhThucDaoTao_ThacSi))
                                                    {
                                                        HinhThucDaoTao hinhThuc = null;
                                                        hinhThuc = uow.FindObject<HinhThucDaoTao>(CriteriaOperator.Parse("TenHinhThucDaoTao like ?", txtHinhThucDaoTao_ThacSi));
                                                        if (hinhThuc != null)
                                                        {
                                                            bang.HinhThucDaoTao = hinhThuc;
                                                            nhanVien.NhanVienTrinhDo.HinhThucDaoTao = hinhThuc;
                                                        }
                                                        else
                                                        {
                                                            errorLog.AppendLine(" + Hình thức đào tạo - Thạc sĩ không hợp lệ: " + txtHinhThucDaoTao_ThacSi);
                                                        }
                                                    }
                                                    #region năm công nhận tốt nghiệp
                                                    int namTotNghiep;
                                                    if (int.TryParse(txtNamTotNghiep_ThacSi, out namTotNghiep))
                                                    {
                                                        bang.NamTotNghiep = namTotNghiep;
                                                        nhanVien.NhanVienTrinhDo.NamTotNghiep = namTotNghiep;
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                        #endregion

                                        #region 85 --> 88 Trình độ tiến sĩ
                                        if (!String.IsNullOrEmpty(txtChuyenNganhDaoTao_TienSi))
                                        {
                                            TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ? or TenTrinhDoChuyenMon Like ?", "tiến sĩ", "tiến sỹ"));
                                            if (trinhDo != null)
                                            {
                                                VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.MaTapDoan like ? and TrinhDoChuyenMon = ?", nhanVien.MaTapDoan, trinhDo));
                                                if (bang == null)
                                                {
                                                    bang = new VanBang(uow);
                                                    bang.HoSo = nhanVien;
                                                    bang.TrinhDoChuyenMon = trinhDo;
                                                    nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDo;

                                                    ChuyenNganhDaoTao chuyenMon = null;
                                                    chuyenMon = uow.FindObject<ChuyenNganhDaoTao>(CriteriaOperator.Parse("TenChuyenNganhDaoTao like ?", txtChuyenNganhDaoTao_TienSi));
                                                    if (chuyenMon != null)
                                                    {
                                                        bang.ChuyenNganhDaoTao = chuyenMon;
                                                        nhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao = chuyenMon;
                                                    }
                                                    else
                                                    {
                                                        errorLog.AppendLine(" + Chuyên ngành đào tạo - Tiến sĩ không hợp lệ: " + txtChuyenNganhDaoTao_TienSi);
                                                    }

                                                    //
                                                    if (!String.IsNullOrEmpty(txtNoiDaoTao_TienSi))
                                                    {
                                                        TruongDaoTao truong = null;
                                                        truong = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao like ?", txtNoiDaoTao_TienSi));
                                                        if (truong != null)
                                                        {
                                                            bang.TruongDaoTao = truong;
                                                            nhanVien.NhanVienTrinhDo.TruongDaoTao = truong;
                                                        }
                                                        else
                                                        {
                                                            errorLog.AppendLine(" + Trường đào tạo - Tiến sĩ không hợp lệ: " + txtNoiDaoTao_TienSi);
                                                        }
                                                    }
                                                    //
                                                    if (!String.IsNullOrEmpty(txtHinhThucDaoTao_TienSi))
                                                    {
                                                        HinhThucDaoTao hinhThuc = null;
                                                        hinhThuc = uow.FindObject<HinhThucDaoTao>(CriteriaOperator.Parse("TenHinhThucDaoTao like ?", txtHinhThucDaoTao_TienSi));
                                                        if (hinhThuc != null)
                                                        {
                                                            bang.HinhThucDaoTao = hinhThuc;
                                                            nhanVien.NhanVienTrinhDo.HinhThucDaoTao = hinhThuc;
                                                        }
                                                        else
                                                        {
                                                            errorLog.AppendLine(" + Hình thức đào tạo - Tiến sĩ không hợp lệ: " + txtHinhThucDaoTao_TienSi);
                                                        }
                                                    }
                                                    #region Năm công nhận tốt nghiệp
                                                    int namTotNghiep;
                                                    if (int.TryParse(txtNamTotNghiep_TienSi, out namTotNghiep))
                                                    {
                                                        bang.NamTotNghiep = namTotNghiep;
                                                        nhanVien.NhanVienTrinhDo.NamTotNghiep = namTotNghiep;
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                        #endregion

                                        //Trình độ hiện tại cao nhất 89 không cần vì là quá trình lưu từ 68 --> 88

                                        #region 90. Trình độ tin học
                                        {
                                            if (!string.IsNullOrEmpty(txtTrinhTinHoc))
                                            {
                                                TrinhDoTinHoc trinhDoTinHoc = uow.FindObject<TrinhDoTinHoc>(CriteriaOperator.Parse("TenTrinhDoTinHoc like ?", txtTrinhTinHoc));
                                                if (trinhDoTinHoc != null)
                                                {
                                                    nhanVien.NhanVienTrinhDo.TrinhDoTinHoc = trinhDoTinHoc;
                                                }
                                                else
                                                {
                                                    errorLog.AppendLine(" + Trình độ tin học không hợp lệ: " + txtTrinhTinHoc);
                                                }
                                            }
                                        }
                                        #endregion

                                        #region 91.92 Trình độ ngoài ngữ khác
                                        TrinhDoNgoaiNguKhac tringDoNgoaiNguKhac = null;

                                        #region 91. Ngoại ngữ                                 
                                        //
                                        if (!string.IsNullOrEmpty(txtNgoaiNgu))
                                        {
                                            tringDoNgoaiNguKhac = new TrinhDoNgoaiNguKhac(uow);
                                            //
                                            NgoaiNgu ngoaiNgu = uow.FindObject<NgoaiNgu>(CriteriaOperator.Parse("TenNgoaiNgu like ?", txtNgoaiNgu));
                                            if (ngoaiNgu != null)
                                            {
                                                nhanVien.NhanVienTrinhDo.NgoaiNgu = ngoaiNgu;
                                                //
                                                tringDoNgoaiNguKhac.NgoaiNgu = ngoaiNgu;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Ngoại ngữ không hợp lệ: " + txtNgoaiNgu);
                                            }
                                        }
                                        #endregion

                                        #region 92. Trình độ ngoại ngữ
                                        if (!string.IsNullOrEmpty(txtTrinhNgoaiNgu))
                                        {
                                            if (tringDoNgoaiNguKhac == null)
                                                tringDoNgoaiNguKhac = new TrinhDoNgoaiNguKhac(uow);
                                            //
                                            TrinhDoNgoaiNgu trinhDoNgoaiNgu = uow.FindObject<TrinhDoNgoaiNgu>(CriteriaOperator.Parse("TenTrinhDoNgoaiNgu like ?", txtTrinhNgoaiNgu));
                                            if (trinhDoNgoaiNgu != null)
                                            {
                                                nhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu = trinhDoNgoaiNgu;
                                                //
                                                tringDoNgoaiNguKhac.TrinhDoNgoaiNgu = trinhDoNgoaiNgu;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Trình độ ngoại ngữ không hợp lệ: " + txtTrinhNgoaiNgu);
                                            }
                                        }
                                        #endregion

                                        #endregion

                                        #endregion

                                        //

                                        #region 93 --> 94 Ðoàn viên
                                        if (!string.IsNullOrEmpty(txtNgayVaoDoan))
                                        {
                                            try
                                            {
                                                nhanVien.NgayVaoDoan = Convert.ToDateTime(txtNgayVaoDoan);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Ngày kết nạp đoàn không hợp lệ - không đúng định dạng dd/MM/yyyy: " + txtNgayVaoDoan);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(txtChucVuDoan))
                                        {
                                            ChucDanh chucDanhDoan = null;
                                            chucDanhDoan = uow.FindObject<ChucDanh>(CriteriaOperator.Parse("TenChucDang like ?", txtChucVuDoan));
                                            if (chucDanhDoan != null)
                                            {
                                                nhanVien.ChucDanhDoan = chucDanhDoan;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Chức danh đoàn không hợp lệ: " + txtChucVuDoan);
                                            }
                                        }
                                        #endregion

                                        //

                                        #region 95 --> 96 Đảng cột
                                        if (!string.IsNullOrEmpty(txtNgayVaoDang))
                                        {
                                            try
                                            {
                                                nhanVien.NgayVaoDang = Convert.ToDateTime(txtNgayVaoDang);
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Ngày kết nạp đảng không hợp lệ - không đúng định dạng dd/MM/yyyy: " + txtNgayVaoDang);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(txtChucVuDang))
                                        {
                                            ChucDanh chucDanhDang = null;
                                            chucDanhDang = uow.FindObject<ChucDanh>(CriteriaOperator.Parse("TenChucDanh like ?", txtChucVuDang));
                                            if (chucDanhDang != null)
                                            {
                                                nhanVien.ChucDanhDang = chucDanhDang;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Chức danh đảng không hợp lệ: " + txtChucVuDang);
                                            }
                                        }
                                        #endregion

                                        //

                                        #region 97 --> 98 Tài khoản ngân hàng
                                        if (!String.IsNullOrWhiteSpace(txtSoTaiKhoan))
                                        {
                                            TaiKhoanNganHang taiKhoanNganHang = new TaiKhoanNganHang(uow);
                                            taiKhoanNganHang.NhanVien = nhanVien;
                                            taiKhoanNganHang.TaiKhoanChinh = true;
                                            taiKhoanNganHang.SoTaiKhoan = txtSoTaiKhoan;
                                            if (!String.IsNullOrWhiteSpace(txtTenNganHang))
                                            {
                                                NganHang nganHang = null;
                                                nganHang = uow.FindObject<NganHang>(CriteriaOperator.Parse("TenNganHang like ?", txtTenNganHang));
                                                if (nganHang != null)
                                                {
                                                    taiKhoanNganHang.NganHang = nganHang;
                                                }
                                                else
                                                {
                                                    errorLog.AppendLine(" + Ngân hàng không hợp lệ: " + txtTenNganHang);
                                                }
                                            }
                                        }
                                        //
                                        #endregion

                                        //

                                        #region Ghi File log
                                        {
                                            //Đưa thông tin bị lỗi vào blog
                                            if (errorLog.Length > 0)
                                            {
                                                mainLog.AppendLine("- STT: " + txtSTT);
                                                mainLog.AppendLine(string.Format("- Cán bộ Mã: {0} Tên: {1} không import vào phần mềm được: ", nhanVien.MaTapDoan, nhanVien.HoTen));
                                                mainLog.AppendLine(errorLog.ToString());
                                                //
                                                sucessImport = false;
                                            }
                                        }
                                        #endregion
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
                                // End Duyệt qua tất cả các dòng trong file excel
                            }
                        }
                    }
                    //

                    string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
                    DialogUtil.ShowInfo("Import Thành Công " + sucessNumber + " Nhân viên - Số nhân viên Import không thành công " + erorrNumber + " " + s + "!");

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

        #endregion

    }
}
