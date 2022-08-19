using System;
using System.IO;
using System.Data;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Extends;
using ERP.Module.Commons;
using ERP.Module.Enum.NhanSu;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.NonPersistentObjects.NhanSu;

namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.NhanSu
{
    public class Imp_Candidate
    {
        #region 1. Nhập hồ sơ ứng viên từ tập tin excel (mới)
        public static void ImportCandidate(IObjectSpace obs, TuyenDung_NhapUngVien obj)
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
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[ThongTinUngVien$A4:BI]", obj.LoaiOffice))
                        {

                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx
                            //
                            #region Hồ sơ 0 --> 24
                            const int idxSTT = 0;
                            const int idxSoBaoDanh = 1;
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
                            #endregion
                            //
                            #region Nhân viên 25 --> 35
                            const int idxCoQuanCu = 25;
                            const int idxKinhNghiem = 26;
                            const int idxGhiChu = 27;
                            const int idxPhongBan = 28;
                            const int idxViTriUngTuyen = 29;
                            const int idxHinhThucTuyenDung = 30;
                            const int idxNgayDuTuyen = 31;
                            const int idxNhomMau = 32;
                            const int idxChieuCao = 33;
                            const int idxCanNang = 34;
                            const int idxSucKhoe = 35;
                            #endregion
                            //
                            #region Thông tin trình độ 36 --> 60
                            const int idxTrinhDoVanHoa = 36;
                            const int idxChuyenNganhDaoTao_TrungHoc = 37;
                            const int idxNoiDaoTao_TrungHoc = 38;
                            const int idxHinhThucDaoTao_TrungHoc = 39;
                            const int idxNamTotNghiep_TrungHoc = 40;
                            const int idxChuyenNganhDaoTao_CaoDang = 41;
                            const int idxNoiDaoTao_CaoDang = 42;
                            const int idxHinhThucDaoTao_CaoDang = 43;
                            const int idxNamTotNghiep_CaoDang = 44;
                            const int idxChuyenNganhDaoTao_DaiHoc = 45;
                            const int idxNoiDaoTao_DaiHoc = 46;
                            const int idxHinhThucDaoTao_DaiHoc = 47;
                            const int idxNamTotNghiep_DaiHoc = 48;
                            const int idxChuyenNganhDaoTao_ThacSi = 49;
                            const int idxNoiDaoTao_ThacSi = 50;
                            const int idxHinhThucDaoTao_ThacSi = 51;
                            const int idxNamTotNghiep_ThacSi = 52;
                            const int idxChuyenNganhDaoTao_TienSi = 53;
                            const int idxNoiDaoTao_TienSi = 54;
                            const int idxHinhThucDaoTao_TienSi = 55;
                            const int idxNamTotNghiep_TienSi = 56;
                            const int idxTrinhDoCaoNhat = 57;
                            const int idxTrinhTinHoc = 58;
                            const int idxNgoaiNgu = 59;
                            const int idxTrinhNgoaiNgu = 60;
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
                                    #region Hồ sơ 0 --> 24
                                    string txtSTT = dr[idxSTT].ToString();
                                    string txtSoBaoDanh = dr[idxSoBaoDanh].ToString();
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
                                    #endregion
                                    //
                                    #region Ứng viên 25 --> 35
                                    string txtCoQuanCu = dr[idxCoQuanCu].ToString();
                                    string txtKinhNghiem = dr[idxKinhNghiem].ToString();
                                    string txtGhiChu = dr[idxGhiChu].ToString();
                                    string txtPhongBan = dr[idxPhongBan].ToString();
                                    string txtViTriUngTuyen = dr[idxViTriUngTuyen].ToString();
                                    string txtHinhThucTuyenDung = dr[idxHinhThucTuyenDung].ToString();
                                    string txtNgayDuTuyen = dr[idxNgayDuTuyen].ToString();
                                    string txtNhomMau = dr[idxNhomMau].ToString();
                                    string txtChieuCao = dr[idxChieuCao].ToString();
                                    string txtCanNang = dr[idxCanNang].ToString();
                                    string txtSucKhoe = dr[idxSucKhoe].ToString();
                                    #endregion
                                    //
                                    #region Thông tin trình độ 36 --> 60
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
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////

                                    #region Kiểm tra dữ liệu

                                    UngVien ungVien = uow.FindObject<UngVien>(CriteriaOperator.Parse("CMND = ?", txtCMND));
                                    if (ungVien != null)
                                    {
                                        mainLog.AppendLine("- STT: " + txtSTT);
                                        mainLog.AppendLine(string.Format("- Số CMND :{0} đã tồn tại trong hệ thống.", txtCMND));
                                        //
                                        sucessImport = false;
                                    }
                                    else
                                    {
                                        //Tạo mới nhân viên
                                        ungVien = new UngVien(uow);
                                        ungVien.QuanLyTuyenDung = uow.GetObjectByKey<QuanLyTuyenDung>(obj.NhuCauTuyenDung.QuanLyTuyenDung.Oid);
                                        //nhu cầu tuyển dụng
                                        ungVien.NhuCauTuyenDung = uow.GetObjectByKey<NhuCauTuyenDung>(obj.NhuCauTuyenDung.Oid);

                                        #region Hồ Sơ 1 --> 24
                                        //
                                        #region 1. Mã nhân viên
                                        if (!string.IsNullOrEmpty(txtSoBaoDanh))
                                        {
                                            ungVien.SoBaoDanh = txtSoBaoDanh;
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu số báo danh ứng viên.");
                                        }
                                        #endregion

                                        #region 2.Họ
                                        if (!string.IsNullOrEmpty(txtHo))
                                        {
                                            ungVien.Ho = txtHo;
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu thông tin họ và tên đệm.");
                                        }
                                        #endregion

                                        #region 3. Tên
                                        if (!string.IsNullOrEmpty(txtTen))
                                        {
                                            ungVien.Ten = txtTen;
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu thông tin tên ứng viên.");
                                        }
                                        #endregion

                                        #region 4. Tên gọi khác
                                        if (!string.IsNullOrEmpty(txtTenGoiKhac))
                                        {
                                            ungVien.TenGoiKhac = txtTenGoiKhac;
                                        }
                                        #endregion

                                        #region 5. Ngày sinh
                                        if (!String.IsNullOrWhiteSpace(txtNgaySinh))
                                        {
                                            try
                                            {
                                                DateTime ngaySinh = Convert.ToDateTime(txtNgaySinh);
                                                ungVien.NgaySinh = ngaySinh;
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
                                            ungVien.NoiSinh = diaChi;
                                        }
                                        #endregion

                                        #region 7. Giới tính
                                        if (!string.IsNullOrEmpty(txtGioiTinh))
                                        {
                                            if (txtGioiTinh.ToLower() == "nam")
                                                ungVien.GioiTinh = GioiTinhEnum.Nam;
                                            else if (txtGioiTinh.ToLower() == "nữ" || txtGioiTinh.ToLower() == "nu")
                                                ungVien.GioiTinh = GioiTinhEnum.Nu;
                                            else
                                            {
                                                errorLog.AppendLine(" + Giới tính không hợp lệ: " + txtGioiTinh);
                                            }
                                        }
                                        #endregion

                                        #region 8. Số chứng minh nhân dân
                                        if (!string.IsNullOrEmpty(txtCMND))
                                        {
                                            ungVien.CMND = txtCMND;
                                        }
                                        #endregion

                                        #region 9. Ngày cấp CMND
                                        if (!string.IsNullOrWhiteSpace(txtNgayCapCMND))
                                        {
                                            try
                                            {
                                                DateTime ngayCap = Convert.ToDateTime(txtNgayCapCMND);
                                                ungVien.NgayCap = ngayCap;
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
                                            ungVien.NoiCap = tinh;
                                        }
                                        #endregion

                                        #region 11. Hộ Chiếu
                                        if (!string.IsNullOrEmpty(txtSoHoChieu))
                                        {
                                            ungVien.SoHoChieu = txtSoHoChieu;
                                        }
                                        #endregion

                                        #region 12. Ngày cấp hộ chiếu
                                        if (!string.IsNullOrWhiteSpace(txtNgayCapHoChieu))
                                        {
                                            try
                                            {
                                                DateTime ngayCapHoChieu = Convert.ToDateTime(txtNgayCapHoChieu);
                                                ungVien.NgayCapHoChieu = ngayCapHoChieu;
                                            }
                                            catch (Exception ex)
                                            {
                                                errorLog.AppendLine(" + Ngày cấp hộ chiếu không hợp lệ: " + txtNgayCapHoChieu);
                                            }
                                        }
                                        #endregion

                                        #region 13. Nơi cấp hộ chiếu
                                        if (!string.IsNullOrEmpty(txtNoiCapHoChieu))
                                        {                                
                                            ungVien.NoiCapHoChieu = txtNoiCapHoChieu;
                                        }
                                        #endregion

                                        #region 14. Ngày hết hạn hộ chiếu
                                        if (!string.IsNullOrWhiteSpace(txtNgayHetHanHoChieu))
                                        {
                                            try
                                            {
                                                DateTime ngayHetHan = Convert.ToDateTime(txtNgayHetHanHoChieu);
                                                ungVien.NgayHetHan = ngayHetHan;
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
                                            ungVien.QueQuan = diaChi;
                                        }
                                        #endregion

                                        #region 16. Địa chỉ thường trú
                                        if (!string.IsNullOrEmpty(txtDiaChiThuongTru))
                                        {
                                            //Địa chỉ thường trú
                                            DiaChi diaChi = new DiaChi(uow);
                                            diaChi.SoNha = txtDiaChiThuongTru;
                                            ungVien.DiaChiThuongTru = diaChi;
                                        }
                                        #endregion

                                        #region 17. Nơi ở hiện nay
                                        if (!string.IsNullOrEmpty(txtNoiOHienNay))
                                        {
                                            //Nơi ở hiện nay
                                            DiaChi diaChi = new DiaChi(uow);
                                            diaChi.SoNha = txtNoiOHienNay;
                                            ungVien.NoiOHienNay = diaChi;
                                        }
                                        #endregion

                                        #region 18. Email
                                        if (!string.IsNullOrEmpty(txtEmail))
                                        {
                                            ungVien.Email = txtEmail;
                                        }
                                        #endregion

                                        #region  19. Điện thoại di động
                                        if (!string.IsNullOrEmpty(txtDienThoaiDiDong))
                                        {
                                            ungVien.DienThoaiDiDong = txtDienThoaiDiDong;
                                        }
                                        #endregion

                                        #region 20. Điện thoại nhà riêng
                                        if (!string.IsNullOrEmpty(txtDienThoaiNhaRieng))
                                        {
                                            ungVien.DienThoaiNhaRieng = txtDienThoaiNhaRieng;
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
                                            ungVien.TinhTrangHonNhan = honNhan;
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
                                            ungVien.DanToc = danToc;
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
                                            ungVien.TonGiao = tonGiao;
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
                                            ungVien.QuocTich = quocTich;
                                        }
                                        else
                                        {
                                            //errorLog.AppendLine(" + Thiếu quốc tịch.");
                                        }
                                        #endregion

                                        //
                                        #endregion
                                        //
                                        #region Ứng viên 25 --> 35
                                        //
                                        #region 25. Cơ quan cũ
                                        if (!string.IsNullOrEmpty(txtCoQuanCu))
                                        {
                                            ungVien.CoQuanCu = txtCoQuanCu;
                                        }
                                        #endregion

                                        #region 26. Kinh nghiệm
                                        {
                                            if (!string.IsNullOrEmpty(txtKinhNghiem))
                                            {
                                                ungVien.KinhNghiem = txtKinhNghiem;
                                            }
                                        }

                                        #endregion

                                        #region 27. Ghi chú
                                        if (!string.IsNullOrEmpty(txtGhiChu))
                                        {
                                            ungVien.GhiChu = txtGhiChu;
                                        }
                                        #endregion

                                        #region 28. Phòng ban

                                        #endregion

                                        #region 29. Vị trí ứng tuyển

                                        #endregion

                                        #region  30. Hình thức tuyển dụng

                                        #endregion

                                        #region 31. Ngày dự tuyển
                                        if (!string.IsNullOrWhiteSpace(txtNgayDuTuyen))
                                        {
                                            try
                                            {
                                                DateTime ngayDuTuyen = Convert.ToDateTime(txtNgayDuTuyen);
                                                ungVien.NgayDuTuyen = ngayDuTuyen;
                                            }
                                            catch
                                            {
                                                errorLog.AppendLine(" + Ngày dự tuyển không hợp lệ: " + txtNgayDuTuyen);
                                            }
                                        }
                                        #endregion

                                        #region 32. Nhóm máu
                                        if (!string.IsNullOrEmpty(txtNhomMau))
                                        {
                                            NhomMau nhomMau = uow.FindObject<NhomMau>(CriteriaOperator.Parse("TenNhomMau like ?", txtNhomMau));
                                            if (nhomMau == null)
                                            {
                                                nhomMau = new NhomMau(uow);
                                                nhomMau.TenNhomMau = txtNhomMau;
                                                nhomMau.MaQuanLy = Guid.NewGuid().ToString();
                                            }
                                            ungVien.NhomMau = nhomMau;
                                        }
                                        #endregion

                                        #region 33. Chiều cao
                                        Decimal chieuCao;
                                        if (Decimal.TryParse(txtChieuCao, out chieuCao))
                                        {
                                            ungVien.ChieuCao = Convert.ToInt32(chieuCao);
                                        }
                                        #endregion

                                        #region 34. Cân nặng
                                        Decimal canNang;
                                        if (Decimal.TryParse(txtCanNang, out canNang))
                                        {
                                            ungVien.CanNang = Convert.ToInt32(canNang);
                                        }
                                        #endregion

                                        #region 35. Tình trạng sức khỏe
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
                                                ungVien.SucKhoe = sucKhoe;
                                            }
                                        }
                                        #endregion

                                        #endregion
                                        //
                                        #region Thông tin trình độ 36 --> 60

                                        #region 36. Trình độ văn hóa
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
                                                ungVien.TrinhDoVanHoa = trinhDo;
                                            }
                                        }
                                        #endregion

                                        #region 37 --> 40 Trình độ trung cấp
                                        if (!String.IsNullOrEmpty(txtChuyenNganhDaoTao_TrungHoc))
                                        {
                                            TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ? or TenTrinhDoChuyenMon Like ?", "trung cấp", "trung học%"));
                                            if (trinhDo != null)
                                            {
                                                VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.CMND like ? and TrinhDoChuyenMon = ?", ungVien.CMND, trinhDo));
                                                if (bang == null)
                                                {
                                                    bang = new VanBang(uow);
                                                    bang.HoSo = ungVien;
                                                    bang.TrinhDoChuyenMon = trinhDo;
                                                    ungVien.TrinhDoChuyenMon = trinhDo;

                                                    ChuyenNganhDaoTao chuyenMon = null;
                                                    chuyenMon = uow.FindObject<ChuyenNganhDaoTao>(CriteriaOperator.Parse("TenChuyenNganhDaoTao like ?", txtChuyenNganhDaoTao_TrungHoc));
                                                    if (chuyenMon == null)
                                                    {
                                                        chuyenMon = new ChuyenNganhDaoTao(uow);
                                                        chuyenMon.TenChuyenNganhDaoTao = txtChuyenNganhDaoTao_TrungHoc;
                                                        chuyenMon.MaQuanLy = txtChuyenNganhDaoTao_TrungHoc;

                                                    }
                                                    bang.ChuyenNganhDaoTao = chuyenMon;
                                                    ungVien.ChuyenNganhDaoTao = chuyenMon;

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
                                                        ungVien.TruongDaoTao = truong;
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
                                                        ungVien.HinhThucDaoTao = hinhThuc;
                                                    }

                                                    #region năm công nhận tốt nghiệp
                                                    int namTotNghiep;
                                                    if (int.TryParse(txtNamTotNghiep_TrungHoc, out namTotNghiep))
                                                    {
                                                        bang.NamTotNghiep = namTotNghiep;
                                                        ungVien.NamTotNghiep = namTotNghiep;
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                        #endregion

                                        #region 41 --> 44 Trình độ cao đẳng
                                        if (!String.IsNullOrEmpty(txtChuyenNganhDaoTao_CaoDang))
                                        {
                                            TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ? ", "cao đẳng"));
                                            if (trinhDo != null)
                                            {
                                                VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.CMND like ? and TrinhDoChuyenMon = ?", ungVien.CMND, trinhDo));
                                                if (bang == null)
                                                {
                                                    bang = new VanBang(uow);
                                                    bang.HoSo = ungVien;
                                                    bang.TrinhDoChuyenMon = trinhDo;
                                                    ungVien.TrinhDoChuyenMon = trinhDo;

                                                    ChuyenNganhDaoTao chuyenMon = null;
                                                    chuyenMon = uow.FindObject<ChuyenNganhDaoTao>(CriteriaOperator.Parse("TenChuyenNganhDaoTao like ?", txtChuyenNganhDaoTao_CaoDang));
                                                    if (chuyenMon == null)
                                                    {
                                                        chuyenMon = new ChuyenNganhDaoTao(uow);
                                                        chuyenMon.TenChuyenNganhDaoTao = txtChuyenNganhDaoTao_CaoDang;
                                                        chuyenMon.MaQuanLy = txtChuyenNganhDaoTao_CaoDang;

                                                    }
                                                    bang.ChuyenNganhDaoTao = chuyenMon;
                                                    ungVien.ChuyenNganhDaoTao = chuyenMon;

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
                                                        ungVien.TruongDaoTao = truong;
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
                                                        ungVien.HinhThucDaoTao = hinhThuc;
                                                    }
                                                    #region năm công nhận tốt nghiệp
                                                    int namTotNghiep;
                                                    if (int.TryParse(txtNamTotNghiep_CaoDang, out namTotNghiep))
                                                    {
                                                        bang.NamTotNghiep = namTotNghiep;
                                                        ungVien.NamTotNghiep = namTotNghiep;
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                        #endregion

                                        #region 45 --> 48 Trình độ đại học
                                        if (!String.IsNullOrEmpty(txtChuyenNganhDaoTao_DaiHoc))
                                        {
                                            TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ? or TenTrinhDoChuyenMon Like ? or TenTrinhDoChuyenMon Like ?", "đại học", "cử nhân", "kỹ sư"));
                                            if (trinhDo != null)
                                            {
                                                 VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.CMND like ? and TrinhDoChuyenMon = ?", ungVien.CMND, trinhDo));
                                                if (bang == null)
                                                {
                                                    bang = new VanBang(uow);
                                                    bang.HoSo = ungVien;
                                                    bang.TrinhDoChuyenMon = trinhDo;
                                                    ungVien.TrinhDoChuyenMon = trinhDo;

                                                    ChuyenNganhDaoTao chuyenMon = null;
                                                    chuyenMon = uow.FindObject<ChuyenNganhDaoTao>(CriteriaOperator.Parse("TenChuyenNganhDaoTao like ?", txtChuyenNganhDaoTao_DaiHoc));
                                                    if (chuyenMon == null)
                                                    {
                                                        chuyenMon = new ChuyenNganhDaoTao(uow);
                                                        chuyenMon.TenChuyenNganhDaoTao = txtChuyenNganhDaoTao_DaiHoc;
                                                        chuyenMon.MaQuanLy = txtChuyenNganhDaoTao_DaiHoc;

                                                    }
                                                    bang.ChuyenNganhDaoTao = chuyenMon;
                                                    ungVien.ChuyenNganhDaoTao = chuyenMon;

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
                                                        ungVien.TruongDaoTao = truong;
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
                                                        ungVien.HinhThucDaoTao = hinhThuc;
                                                    }
                                                    #region năm công nhận tốt nghiệp
                                                    int namTotNghiep;
                                                    if (int.TryParse(txtNamTotNghiep_DaiHoc, out namTotNghiep))
                                                    {
                                                        bang.NamTotNghiep = namTotNghiep;
                                                        ungVien.NamTotNghiep = namTotNghiep;
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                        #endregion

                                        #region 49 --> 52 Trình độ thạc sĩ
                                        if (!String.IsNullOrEmpty(txtChuyenNganhDaoTao_ThacSi))
                                        {
                                            TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ? or TenTrinhDoChuyenMon Like ?", "thạc sĩ", "thạc sỹ"));
                                            if (trinhDo != null)
                                            {
                                                 VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.CMND like ? and TrinhDoChuyenMon = ?", ungVien.CMND, trinhDo));
                                                if (bang == null)
                                                {
                                                    bang = new VanBang(uow);
                                                    bang.HoSo = ungVien;
                                                    bang.TrinhDoChuyenMon = trinhDo;
                                                    ungVien.TrinhDoChuyenMon = trinhDo;

                                                    ChuyenNganhDaoTao chuyenMon = null;
                                                    chuyenMon = uow.FindObject<ChuyenNganhDaoTao>(CriteriaOperator.Parse("TenChuyenNganhDaoTao like ?", txtChuyenNganhDaoTao_ThacSi));
                                                    if (chuyenMon == null)
                                                    {
                                                        chuyenMon = new ChuyenNganhDaoTao(uow);
                                                        chuyenMon.TenChuyenNganhDaoTao = txtChuyenNganhDaoTao_ThacSi;
                                                        chuyenMon.MaQuanLy = txtChuyenNganhDaoTao_ThacSi;

                                                    }
                                                    bang.ChuyenNganhDaoTao = chuyenMon;
                                                    ungVien.ChuyenNganhDaoTao = chuyenMon;

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
                                                        ungVien.TruongDaoTao = truong;
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
                                                        ungVien.HinhThucDaoTao = hinhThuc;
                                                    }
                                                    #region năm công nhận tốt nghiệp
                                                    int namTotNghiep;
                                                    if (int.TryParse(txtNamTotNghiep_ThacSi, out namTotNghiep))
                                                    {
                                                        bang.NamTotNghiep = namTotNghiep;
                                                        ungVien.NamTotNghiep = namTotNghiep;
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                        #endregion

                                        #region 53 --> 56 Trình độ tiến sĩ
                                        if (!String.IsNullOrEmpty(txtChuyenNganhDaoTao_TienSi))
                                        {
                                            TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ? or TenTrinhDoChuyenMon Like ?", "tiến sĩ", "tiến sỹ"));
                                            if (trinhDo != null)
                                            {
                                                 VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.CMND like ? and TrinhDoChuyenMon = ?", ungVien.CMND, trinhDo));
                                                if (bang == null)
                                                {
                                                    bang = new VanBang(uow);
                                                    bang.HoSo = ungVien;
                                                    bang.TrinhDoChuyenMon = trinhDo;
                                                    ungVien.TrinhDoChuyenMon = trinhDo;

                                                    ChuyenNganhDaoTao chuyenMon = null;
                                                    chuyenMon = uow.FindObject<ChuyenNganhDaoTao>(CriteriaOperator.Parse("TenChuyenNganhDaoTao like ?", txtChuyenNganhDaoTao_TienSi));
                                                    if (chuyenMon == null)
                                                    {
                                                        chuyenMon = new ChuyenNganhDaoTao(uow);
                                                        chuyenMon.TenChuyenNganhDaoTao = txtChuyenNganhDaoTao_TienSi;
                                                        chuyenMon.MaQuanLy = txtChuyenNganhDaoTao_TienSi;

                                                    }
                                                    bang.ChuyenNganhDaoTao = chuyenMon;
                                                    ungVien.ChuyenNganhDaoTao = chuyenMon;

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
                                                        ungVien.TruongDaoTao = truong;
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
                                                        ungVien.HinhThucDaoTao = hinhThuc;
                                                    }
                                                    #region Năm công nhận tốt nghiệp
                                                    int namTotNghiep;
                                                    if (int.TryParse(txtNamTotNghiep_TienSi, out namTotNghiep))
                                                    {
                                                        bang.NamTotNghiep = namTotNghiep;
                                                        ungVien.NamTotNghiep = namTotNghiep;
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                        #endregion

                                        //Trình độ hiện tại cao nhất 57 không cần vì là quá trình lưu từ 36 --> 56

                                        #region 58. Trình độ tin học
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
                                                ungVien.TrinhDoTinHoc = trinhDoTinHoc;
                                            }
                                        }
                                        #endregion

                                        #region 59.60 Trình độ ngoài ngữ khác
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
                                            ungVien.NgoaiNgu = ngoaiNgu;
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
                                            ungVien.TrinhDoNgoaiNgu = trinhDoNgoaiNgu;
                                            //
                                            tringDoNgoaiNguKhac.TrinhDoNgoaiNgu = trinhDoNgoaiNgu;
                                        }
                                        #endregion

                                        #endregion

                                        #endregion

                                        #region Ghi File log
                                        {
                                            //Đưa thông tin bị lỗi vào blog
                                            if (errorLog.Length > 0)
                                            {
                                                mainLog.AppendLine("- STT: " + txtSTT);
                                                mainLog.AppendLine(string.Format("- Ứng viên Mã: {0} Tên: {1} không import vào phần mềm được: ", ungVien.SoBaoDanh, ungVien.HoTen));
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




        #region 2. Nhập hồ sơ ứng viên từ tập tin excel (cũ)
        public static void XuLy(IObjectSpace obs, TuyenDung_NhapUngVien obj)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.FileName = "";
                dialog.Filter = "Excel 1997-2003 files (*.xls)|*.xls";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (DataTable dt = DataProvider.GetDataTableFromExcel(dialog.FileName, "[Sheet1$]", obj.LoaiOffice))
                    {
                        if (dt != null)
                        {
                            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {

                                UngVien ungVien;
                                BoPhan boPhan;

                                foreach (DataRow item in dt.Rows)
                                {
                                    ungVien = uow.FindObject<UngVien>(CriteriaOperator.Parse("CMND=?", item[6]));
                                    if (ungVien == null)
                                    {
                                        ungVien = new UngVien(uow);
                                        ungVien.QuanLyTuyenDung = uow.GetObjectByKey<QuanLyTuyenDung>(obj.NhuCauTuyenDung.QuanLyTuyenDung.Oid);

                                        //nhu cầu tuyển dụng
                                        boPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan=?", item[1]));
                                        if (boPhan != null)
                                            ungVien.NhuCauTuyenDung = uow.FindObject<NhuCauTuyenDung>(CriteriaOperator.Parse("ViTriTuyenDung=? and BoPhan=?", obj.NhuCauTuyenDung.Oid, boPhan.Oid));

                                        //họ
                                        if (!item.IsNull(2) && item[2].ToString().Trim() != string.Empty)
                                            //ungVien.Ho = HamDungChung.VietHoaChuDau(item[2].ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                                            ungVien.Ho = item[2].ToString();
                                        //tên
                                        if (!item.IsNull(3) && item[3].ToString().Trim() != string.Empty)
                                            //ungVien.Ten = CHamDungChung.VietHoaChuDau(new string[] { item[3].ToString().Trim() });
                                            ungVien.Ten = item[3].ToString().Trim();
                                        //ngày sinh
                                        int iTemp;
                                        if (!item.IsNull(4) && item[4].ToString().Trim() != string.Empty &&
                                            int.TryParse(item[4].ToString().Trim(), out iTemp))
                                            ungVien.NgaySinh = new DateTime(iTemp, 1, 1);

                                        //giới tính
                                        if (!item.IsNull(5) && item[5].ToString().Trim() != string.Empty &&
                                            item[5].ToString().Trim().ToLower() == "nam")
                                            ungVien.GioiTinh = GioiTinhEnum.Nam;
                                        else
                                            ungVien.GioiTinh = GioiTinhEnum.Nu;

                                        //CMND
                                        if (!item.IsNull(6) && item[6].ToString().Trim() != string.Empty)
                                            ungVien.CMND = item[6].ToString().Trim();

                                        //điện thoại di động
                                        if (!item.IsNull(7) && item[7].ToString().Trim() != string.Empty)
                                            ungVien.DienThoaiDiDong = item[7].ToString().Trim();

                                        //địa chỉ thường trú
                                        DiaChi diaChi;
                                        if (!item.IsNull(8) && item[8].ToString().Trim() != string.Empty)
                                        {
                                            diaChi = new DiaChi(uow);
                                            diaChi.SoNha = item[8].ToString().Trim();
                                            ungVien.DiaChiThuongTru = diaChi;
                                        }

                                        //nơi ở hiện nay
                                        if (!item.IsNull(9) && item[9].ToString().Trim() != string.Empty)
                                        {
                                            diaChi = new DiaChi(uow);
                                            diaChi.SoNha = item[9].ToString().Trim();
                                            ungVien.NoiOHienNay = diaChi;
                                        }

                                        uow.CommitChanges();
                                    }
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




