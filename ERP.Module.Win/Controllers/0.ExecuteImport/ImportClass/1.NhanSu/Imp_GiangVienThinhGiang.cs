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
    public class Imp_GiangVienThinhGiang
    {
        #region 1. Nhập hồ sơ Giảng viên từ tập tin excel
        public static void ImportGiangVienThinhGiang(IObjectSpace obs, GiangVien_ChonLoaiGiangVien obj)
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
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[GiangVienThinhGiang$A2:AU]", obj.LoaiOffice))
                        {

                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx
                            //
                            const int idx_MaNhanVien = 0;
                            const int idx_Ho = 1;
                            const int idx_Ten = 2;
                            const int idx_MaDonVi = 3;
                            const int idx_HocHam = 4;
                            const int idx_TrinhDoChuyenMon = 5;
                            const int idx_TinhTrang = 6;
                            const int idx_NgaySinh = 7;
                            const int idx_GioiTinh = 8;
                            const int idx_NoiSinh = 9;
                            const int idx_SoCMND = 10;
                            const int idx_NgayCapCMND = 11;
                            const int idx_NoiCapCMND = 12;
                            const int idx_TamTru = 13;
                            const int idx_ThuongTru = 14;
                            const int idx_Email = 15;
                            const int idx_DienThoai = 16;
                            const int idx_DienThoaiDD = 17;
                            const int idx_SoTaiKhoan = 18;
                            const int idx_QuocTich = 19;
                            const int idx_DonViCongTac = 20;
                        
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();

                                //Duyệt qua tất cả các dòng trong file excel
                                foreach (DataRow dr in dt.Rows)
                                {
                                    var detailLog = new StringBuilder();

                                    //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                    #region Đọc dữ liệu
                                    //
                                                                    
                                    string txtMaNhanVien = dr[idx_MaNhanVien].ToString();
                                    string txtHo = dr[idx_Ho].ToString();
                                    string txtTen = dr[idx_Ten].ToString();
                                    string txtMaDonvi = dr[idx_MaDonVi].ToString();
                                    string txtHocHam = dr[idx_HocHam].ToString();
                                    string txtTrinhDoChuyenMon = dr[idx_TrinhDoChuyenMon].ToString();
                                    string txtTinhTrang = dr[idx_TinhTrang].ToString();
                                    string txtNgaySinh = dr[idx_NgaySinh].ToString();
                                    string txtNoiSinh = dr[idx_NoiSinh].ToString();
                                    string txtGioiTinh = dr[idx_GioiTinh].ToString();
                                    string txtCMND = dr[idx_SoCMND].ToString();
                                    string txtNgayCapCMND = dr[idx_NgayCapCMND].ToString();
                                    string txtNoiCapCMND = dr[idx_NoiCapCMND].ToString();                               
                                    string txtDiaChiThuongTru = dr[idx_ThuongTru].ToString();
                                    string txtDiaChiTamTru = dr[idx_TamTru].ToString();
                                    string txtEmail = dr[idx_Email].ToString();
                                    string txtDienThoaiDiDong = dr[idx_DienThoaiDD].ToString();
                                    string txtDienThoaiNhaRieng = dr[idx_DienThoai].ToString();
                                    string txtSoTaiKhoan = dr[idx_SoTaiKhoan].ToString();
                                    string txtQuocTich = dr[idx_QuocTich].ToString();
                                    string txtDonViCongTac = dr[idx_DonViCongTac].ToString();    
                                
                                    #endregion
                                    //
                                  
                                    //////////////KIỂM TRA VÀ LẤY DỮ LIỆU////////////////////////////////

                                    #region Kiểm tra dữ

                                    GiangVienThinhGiang giangVienThinhGiang = uow.FindObject<GiangVienThinhGiang>(CriteriaOperator.Parse("CMND = ?", txtCMND));
                                    if (giangVienThinhGiang != null)
                                    {
                                        mainLog.AppendLine(string.Format("- Giảng viên :{0} {1} đã tồn tại trong hệ thống.", txtHo, txtTen));
                                        //
                                        sucessImport = false;
                                    }
                                    else
                                    {
                                        //Tạo mới nhân viên
                                        giangVienThinhGiang = new GiangVienThinhGiang(uow);
                                        //Loại hồ sơ  là giảng viên
                                        giangVienThinhGiang.LoaiHoSo = LoaiHoSoEnum.GiangVien;
                                        //

                                        #region Mã quản lý
                                        {
                                            if (!string.IsNullOrEmpty(txtMaNhanVien))
                                            {
                                                giangVienThinhGiang.MaTapDoan = txtMaNhanVien;
                                            }                                          
                                        }
                                        #endregion

                                        #region Họ tên
                                        {                                         
                                            if (!string.IsNullOrEmpty(txtHo))
                                            {
                                                giangVienThinhGiang.Ho = txtHo;                                               
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Thiếu thông tin họ");
                                            }
                                            if (!string.IsNullOrEmpty(txtTen))
                                            {
                                                giangVienThinhGiang.Ten = txtTen;
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Thiếu thông tin tên");
                                            }
                                        }
                                        #endregion

                                        #region Mã đơn vị
                                        {                                            
                                            if (!string.IsNullOrEmpty(txtMaDonvi))
                                            {
                                                BoPhan boPhan = null;
                                                boPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("MaQuanLy = ?", txtMaDonvi));
                                                if (boPhan != null)
                                                {
                                                    giangVienThinhGiang.BoPhan = boPhan;
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Thiếu thông tin mã đơn vị");
                                            }
                                        }
                                        #endregion

                                        #region Học hàm
                                        {                                           
                                            if (!string.IsNullOrEmpty(txtHocHam))
                                            {
                                                HocHam hocHam = null;
                                                hocHam = uow.FindObject<HocHam>(CriteriaOperator.Parse("TenHocHam like ?", txtHocHam));
                                                if (hocHam == null)
                                                {
                                                    hocHam = new HocHam(uow);
                                                    hocHam.TenHocHam = txtHocHam;
                                                    hocHam.MaQuanLy = Guid.NewGuid().ToString();
                                                }
                                                giangVienThinhGiang.NhanVienTrinhDo.HocHam = hocHam;
                                            }
                                        }
                                        #endregion

                                        #region Trình độ chuyên môn
                                        {                                         
                                            if (!string.IsNullOrEmpty(txtTrinhDoChuyenMon))
                                            {
                                                TrinhDoChuyenMon trinhDoChuyenMon = null;
                                                trinhDoChuyenMon = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon like ?", txtTrinhDoChuyenMon));
                                                if (trinhDoChuyenMon == null)
                                                {
                                                    //tạo mới trình độ chuyên môn
                                                    trinhDoChuyenMon = new TrinhDoChuyenMon(uow);
                                                    trinhDoChuyenMon.TenTrinhDoChuyenMon = txtTrinhDoChuyenMon;
                                                    trinhDoChuyenMon.MaQuanLy = Guid.NewGuid().ToString();
                                                    trinhDoChuyenMon.Save();
                                                }
                                                VanBang vanBang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.MaNhanVien like ? and TrinhDoChuyenMon = ?", giangVienThinhGiang.MaNhanVien, trinhDoChuyenMon));
                                                if (vanBang == null)
                                                {
                                                    vanBang = new VanBang(uow);
                                                    vanBang.HoSo = giangVienThinhGiang;
                                                    vanBang.TrinhDoChuyenMon = trinhDoChuyenMon;
                                                }
                                                giangVienThinhGiang.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDoChuyenMon;

                                            }
                                        }
                                        #endregion

                                        #region Tình trạng
                                        {
                                            if (!string.IsNullOrEmpty(txtTinhTrang))
                                            {
                                                TinhTrang tinhTrang = null;
                                                tinhTrang = uow.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", txtTinhTrang));
                                                if (tinhTrang == null)
                                                {
                                                    tinhTrang = new TinhTrang(uow);
                                                    tinhTrang.TenTinhTrang = txtTinhTrang;
                                                    tinhTrang.MaQuanLy = Guid.NewGuid().ToString();
                                                    tinhTrang.Save();
                                                }
                                                giangVienThinhGiang.TinhTrang = tinhTrang;
                                            }
                                        }
                                        #endregion

                                        #region Ngày sinh
                                        {
                                            if (!string.IsNullOrEmpty(txtNgaySinh))
                                            {
                                                try
                                                {
                                                    giangVienThinhGiang.NgaySinh = Convert.ToDateTime(txtNgaySinh);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày sinh không hợp lệ: " + txtNgaySinh);
                                                }
                                            }
                                            //else
                                            //{
                                            //    giangVienThinhGiang.NgaySinh = Convert.ToDateTime("01/01/2017");
                                            //    detailLog.AppendLine(" + Thiếu thông tin ngày sinh hoặc không đúng định dạng dd/MM/yyyy.");
                                            //}
                                        }
                                        #endregion

                                        #region Giới tính
                                        {
                                            if (!string.IsNullOrEmpty(txtGioiTinh))
                                            {
                                                if (txtGioiTinh.ToLower() == "nam")
                                                    giangVienThinhGiang.GioiTinh = GioiTinhEnum.Nam;
                                                else if (txtGioiTinh.ToLower() == "nữ" || txtGioiTinh.ToLower() == "nu")
                                                    giangVienThinhGiang.GioiTinh = GioiTinhEnum.Nu;
                                                else
                                                {
                                                    detailLog.AppendLine(" + Giới tính không hợp lệ: " + txtGioiTinh);
                                                }
                                            }
                                        }
                                        #endregion

                                        #region Nơi sinh
                                        {
                                            DiaChi diaChi = null;
                                            if (!string.IsNullOrEmpty(txtNoiSinh))
                                            {
                                                diaChi = uow.FindObject<DiaChi>(CriteriaOperator.Parse("SoNha like ?", txtNoiSinh));
                                                if (diaChi == null)
                                                {
                                                    diaChi = new DiaChi(uow);
                                                    diaChi.SoNha = txtNoiSinh;
                                                    diaChi.Save();
                                                }
                                                giangVienThinhGiang.NoiSinh = diaChi;
                                            }
                                        }
                                        #endregion

                                        #region Số CMND
                                        {
                                            if (!string.IsNullOrWhiteSpace(txtCMND))
                                            {
                                                giangVienThinhGiang.CMND = txtCMND;
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Thiếu thông tin Số CMND");
                                            }
                                        }
                                        #endregion

                                        #region Ngày cấp CMND
                                        {
                                            if (!string.IsNullOrEmpty(txtNgayCapCMND))
                                            {
                                                try
                                                {
                                                    giangVienThinhGiang.NgayCap = Convert.ToDateTime(txtNgayCapCMND);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày cấp CMND không hợp lệ: " + txtNgayCapCMND);
                                                }
                                            }
                                            //else
                                            //{
                                            //    //giangVienThinhGiang.NgayCap = Convert.ToDateTime("01/01/1960");
                                            //    detailLog.AppendLine(" + Thiếu thông tin cấp CMND hoặc không đúng định dạng dd/MM/yyyy.");
                                            //}


                                        }
                                        #endregion

                                        #region Nơi cấp CMND
                                        {
                                            if (!string.IsNullOrEmpty(txtNoiCapCMND))
                                            {
                                                TinhThanh tinhThanh = null;
                                                tinhThanh = uow.FindObject<TinhThanh>(CriteriaOperator.Parse("TenTinhThanh like ?", txtNoiCapCMND));
                                                if (tinhThanh == null)
                                                {
                                                    //tạo mới tỉnh thành
                                                    tinhThanh = new TinhThanh(uow);
                                                    tinhThanh.TenTinhThanh = txtNoiCapCMND;
                                                    tinhThanh.MaQuanLy = Guid.NewGuid().ToString();
                                                    tinhThanh.Save();                                                    
                                                }
                                                giangVienThinhGiang.NoiCap = tinhThanh;
                                            }
                                        }
                                        #endregion

                                        #region Thường trú
                                        {
                                            if (!string.IsNullOrEmpty(txtDiaChiThuongTru))
                                            {
                                                DiaChi diaChi = null;
                                                diaChi = uow.FindObject<DiaChi>(CriteriaOperator.Parse("SoNha like ?", txtDiaChiThuongTru));
                                                if (diaChi == null)
                                                {
                                                    diaChi = new DiaChi(uow);
                                                    diaChi.SoNha = txtDiaChiThuongTru;
                                                    diaChi.Save();
                                                }
                                                giangVienThinhGiang.DiaChiThuongTru = diaChi;
                                            }
                                        }
                                        #endregion

                                        #region Tạm trú - Nơi ở hiện nay
                                        {
                                            if (!string.IsNullOrEmpty(txtDiaChiTamTru))
                                            {
                                                DiaChi diaChi = null;
                                                diaChi = uow.FindObject<DiaChi>(CriteriaOperator.Parse("SoNha like ?", txtDiaChiTamTru));
                                                if (diaChi == null)
                                                {
                                                    diaChi = new DiaChi(uow);
                                                    diaChi.SoNha = txtDiaChiTamTru;
                                                    diaChi.Save();
                                                }
                                                giangVienThinhGiang.NoiOHienNay = diaChi;
                                            }
                                        }
                                        #endregion

                                        #region Số điện thoại
                                        {
                                            if (!string.IsNullOrEmpty(txtDienThoaiNhaRieng))
                                            {
                                                giangVienThinhGiang.DienThoaiNhaRieng = txtDienThoaiNhaRieng;
                                            }
                                        }
                                        #endregion

                                        #region Số điện thoại di động
                                        {
                                            if (!string.IsNullOrEmpty(txtDienThoaiDiDong))
                                            {
                                                giangVienThinhGiang.DienThoaiDiDong = txtDienThoaiDiDong;
                                            }
                                        }
                                        #endregion

                                        #region Email
                                        {
                                            if (!string.IsNullOrEmpty(txtEmail))
                                            {
                                                giangVienThinhGiang.Email = txtEmail;
                                            }
                                        }
                                        #endregion

                                        #region Số tài khoản
                                        if (!string.IsNullOrWhiteSpace(txtSoTaiKhoan))
                                        {
                                            TaiKhoanNganHang tknh = null;
                                            tknh = uow.FindObject<TaiKhoanNganHang>(CriteriaOperator.Parse("SoTaiKhoan = ?", txtSoTaiKhoan));
                                            if (tknh == null)
                                            {
                                                tknh = new TaiKhoanNganHang(uow);
                                                tknh.SoTaiKhoan = txtSoTaiKhoan;
                                                tknh.TaiKhoanChinh = true;
                                            }
                                            tknh.NhanVien = giangVienThinhGiang;
                                            tknh.Save();
                                        }
                                        #endregion

                                        #region Quốc tịch
                                        if (!string.IsNullOrWhiteSpace(txtSoTaiKhoan))
                                        {
                                            QuocGia qt = null;
                                            qt = uow.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia = ?", txtQuocTich));
                                            if (qt == null)
                                            {
                                                qt = new QuocGia(uow);
                                                qt.TenQuocGia = txtQuocTich;
                                                qt.MaQuanLy = Guid.NewGuid().ToString();
                                                qt.Save();
                                            }
                                            giangVienThinhGiang.QuocTich = qt;
                                            qt.Save();
                                        }
                                        #endregion

                                        #region Đơn vị công tác
                                        {
                                            if (!string.IsNullOrWhiteSpace(txtDonViCongTac))
                                            {
                                                giangVienThinhGiang.DonViCongTac = txtDonViCongTac;
                                            }
                                        }
                                        #endregion

                                        #region Ghi File log
                                        {
                                            //Đưa thông tin bị lỗi vào blog
                                            if (detailLog.Length > 0)
                                            {
                                                mainLog.AppendLine(string.Format("- Giảng viên thỉnh giảng Mã: {0} Tên: {1} không import vào phần mềm được: ", giangVienThinhGiang.MaNhanVien, giangVienThinhGiang.HoTen));
                                                mainLog.AppendLine(detailLog.ToString());
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
                    DialogUtil.ShowInfo("Import Thành Công " + sucessNumber + " Giảng viên thỉnh giảng - Số giảng viên import không thành công " + erorrNumber + " " + s + "!");

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

        #region 2. Nhập hồ sơ Giảng viên từ tập tin excel -Không tạo danh mục
        public static void ImportGiangVienThinhGiangNew(IObjectSpace obs, GiangVien_ChonLoaiGiangVien obj)
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
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[GiangVienThinhGiang$A2:AU]", obj.LoaiOffice))
                        {

                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx
                            //
                            const int idx_MaNhanVien = 0;
                            const int idx_Ho = 1;
                            const int idx_Ten = 2;
                            const int idx_MaDonVi = 3;
                            const int idx_HocHam = 4;
                            const int idx_TrinhDoChuyenMon = 5;
                            const int idx_TinhTrang = 6;
                            const int idx_NgaySinh = 7;
                            const int idx_GioiTinh = 8;
                            const int idx_NoiSinh = 9;
                            const int idx_SoCMND = 10;
                            const int idx_NgayCapCMND = 11;
                            const int idx_NoiCapCMND = 12;
                            const int idx_TamTru = 13;
                            const int idx_ThuongTru = 14;
                            const int idx_Email = 15;
                            const int idx_DienThoai = 16;
                            const int idx_DienThoaiDD = 17;
                            const int idx_SoTaiKhoan = 18;
                            const int idx_QuocTich = 19;
                            const int idx_DonViCongTac = 20;

                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();

                                //Duyệt qua tất cả các dòng trong file excel
                                foreach (DataRow dr in dt.Rows)
                                {
                                    var detailLog = new StringBuilder();

                                    //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                    #region Đọc dữ liệu
                                    //

                                    string txtMaNhanVien = dr[idx_MaNhanVien].ToString();
                                    string txtHo = dr[idx_Ho].ToString();
                                    string txtTen = dr[idx_Ten].ToString();
                                    string txtMaDonvi = dr[idx_MaDonVi].ToString();
                                    string txtHocHam = dr[idx_HocHam].ToString();
                                    string txtTrinhDoChuyenMon = dr[idx_TrinhDoChuyenMon].ToString();
                                    string txtTinhTrang = dr[idx_TinhTrang].ToString();
                                    string txtNgaySinh = dr[idx_NgaySinh].ToString();
                                    string txtNoiSinh = dr[idx_NoiSinh].ToString();
                                    string txtGioiTinh = dr[idx_GioiTinh].ToString();
                                    string txtCMND = dr[idx_SoCMND].ToString();
                                    string txtNgayCapCMND = dr[idx_NgayCapCMND].ToString();
                                    string txtNoiCapCMND = dr[idx_NoiCapCMND].ToString();
                                    string txtDiaChiThuongTru = dr[idx_ThuongTru].ToString();
                                    string txtDiaChiTamTru = dr[idx_TamTru].ToString();
                                    string txtEmail = dr[idx_Email].ToString();
                                    string txtDienThoaiDiDong = dr[idx_DienThoaiDD].ToString();
                                    string txtDienThoaiNhaRieng = dr[idx_DienThoai].ToString();
                                    string txtSoTaiKhoan = dr[idx_SoTaiKhoan].ToString();
                                    string txtQuocTich = dr[idx_QuocTich].ToString();
                                    string txtDonViCongTac = dr[idx_DonViCongTac].ToString();

                                    #endregion
                                    //

                                    //////////////KIỂM TRA VÀ LẤY DỮ LIỆU////////////////////////////////

                                    #region Kiểm tra dữ

                                    GiangVienThinhGiang giangVienThinhGiang = uow.FindObject<GiangVienThinhGiang>(CriteriaOperator.Parse("MaTapDoan = ?", txtMaNhanVien));
                                    if (giangVienThinhGiang != null)
                                    {
                                        mainLog.AppendLine(string.Format("- Mã quản lý :{0} đã tồn tại trong hệ thống.", txtMaNhanVien));
                                        //
                                        sucessImport = false;
                                    }
                                    else
                                    {
                                        //Tạo mới nhân viên
                                        giangVienThinhGiang = new GiangVienThinhGiang(uow);
                                        //Loại hồ sơ  là giảng viên
                                        giangVienThinhGiang.LoaiHoSo = LoaiHoSoEnum.GiangVien;
                                        //

                                        #region Mã quản lý
                                        {
                                            if (!string.IsNullOrEmpty(txtMaNhanVien))
                                            {
                                                giangVienThinhGiang.MaTapDoan = txtMaNhanVien;
                                            }
                                            else
                                            {
                                                //detailLog.AppendLine(" + Thiếu thông tin mã nhân viên");
                                            }

                                        }
                                        #endregion

                                        #region Họ tên
                                        {
                                            if (!string.IsNullOrEmpty(txtHo))
                                            {
                                                giangVienThinhGiang.Ho = txtHo;
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Thiếu thông tin họ");
                                            }
                                            if (!string.IsNullOrEmpty(txtTen))
                                            {
                                                giangVienThinhGiang.Ten = txtTen;
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Thiếu thông tin tên");
                                            }
                                        }
                                        #endregion

                                        #region Mã đơn vị
                                        {
                                            if (!string.IsNullOrEmpty(txtMaDonvi))
                                            {
                                                BoPhan boPhan = null;
                                                boPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("MaQuanLy = ?", txtMaDonvi));
                                                if (boPhan != null)
                                                {
                                                    giangVienThinhGiang.BoPhan = boPhan;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Mã đơn vị không hợp lệ: " + txtMaDonvi);
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Thiếu thông tin mã đơn vị");
                                            }
                                        }
                                        #endregion

                                        #region Tại khoa / bộ môn
                                        {
                                            //if (!string.IsNullOrEmpty(txtDonViCongTac))
                                            //{
                                            //    BoPhan boPhan = null;
                                            //    boPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan = ?", txtDonViCongTac));
                                            //    if (boPhan != null)
                                            //    {
                                            //        giangVienThinhGiang.TaiBoMon = boPhan;
                                            //    }
                                            //    else
                                            //    {
                                            //        detailLog.AppendLine(" + Tên Khoa / Bộ môn không hợp lệ: " + txtDonViCongTac);
                                            //    }
                                            //}
                                            //else
                                            //{
                                            //    detailLog.AppendLine(" + Thiếu thông tin Tên Khoa / Bộ môn");
                                            //}
                                        }
                                        #endregion

                                        #region Học hàm
                                        {
                                            if (!string.IsNullOrEmpty(txtHocHam))
                                            {
                                                HocHam hocHam = null;
                                                hocHam = uow.FindObject<HocHam>(CriteriaOperator.Parse("TenHocHam like ?", txtHocHam));
                                                if (hocHam != null)
                                                {
                                                    giangVienThinhGiang.NhanVienTrinhDo.HocHam = hocHam;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Học hàm không hợp lệ: " + txtHocHam);
                                                }
                                            }
                                        }
                                        #endregion

                                        #region Trình độ chuyên môn
                                        {
                                            if (!string.IsNullOrEmpty(txtTrinhDoChuyenMon))
                                            {
                                                TrinhDoChuyenMon trinhDoChuyenMon = null;
                                                trinhDoChuyenMon = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon like ?", txtTrinhDoChuyenMon));
                                                if (trinhDoChuyenMon != null)
                                                {
                                                    giangVienThinhGiang.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDoChuyenMon;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Trình độ chuyên môn không hợp lệ: " + txtTrinhDoChuyenMon);
                                                }

                                                VanBang vanBang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.MaNhanVien like ? and TrinhDoChuyenMon = ?", giangVienThinhGiang.MaNhanVien, trinhDoChuyenMon));
                                                if (vanBang == null)
                                                {
                                                    vanBang = new VanBang(uow);
                                                    vanBang.HoSo = giangVienThinhGiang;
                                                    vanBang.TrinhDoChuyenMon = trinhDoChuyenMon;
                                                }
                                            }
                                        }
                                        #endregion

                                        #region Tình trạng
                                        {
                                            if (!string.IsNullOrEmpty(txtTinhTrang))
                                            {
                                                TinhTrang tinhTrang = null;
                                                tinhTrang = uow.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", txtTinhTrang));
                                                if (tinhTrang != null)
                                                {
                                                    giangVienThinhGiang.TinhTrang = tinhTrang;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Tình trạng không hợp lệ: " + txtTinhTrang);
                                                }
                                            }
                                        }
                                        #endregion

                                        #region Ngày sinh
                                        {
                                            if (!string.IsNullOrEmpty(txtNgaySinh))
                                            {
                                                try
                                                {
                                                    giangVienThinhGiang.NgaySinh = Convert.ToDateTime(txtNgaySinh);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày sinh không hợp lệ: " + txtNgaySinh);
                                                }
                                            }
                                            //else
                                            //{
                                            //    giangVienThinhGiang.NgaySinh = Convert.ToDateTime("01/01/2017");
                                            //    detailLog.AppendLine(" + Thiếu thông tin ngày sinh hoặc không đúng định dạng dd/MM/yyyy.");
                                            //}
                                        }
                                        #endregion

                                        #region Giới tính
                                        {
                                            if (!string.IsNullOrEmpty(txtGioiTinh))
                                            {
                                                if (txtGioiTinh.ToLower().Equals("nam"))
                                                    giangVienThinhGiang.GioiTinh = GioiTinhEnum.Nam;
                                                else
                                                    giangVienThinhGiang.GioiTinh = GioiTinhEnum.Nu;
                                            }
                                        }
                                        #endregion

                                        #region Nơi sinh
                                        {
                                            DiaChi diaChi = null;
                                            if (!string.IsNullOrEmpty(txtNoiSinh))
                                            {
                                                diaChi = uow.FindObject<DiaChi>(CriteriaOperator.Parse("SoNha like ?", txtNoiSinh));
                                                if (diaChi == null)
                                                {
                                                    diaChi = new DiaChi(uow);
                                                    diaChi.SoNha = txtNoiSinh;
                                                    diaChi.Save();
                                                }
                                                giangVienThinhGiang.NoiSinh = diaChi;
                                            }
                                        }
                                        #endregion

                                        #region Số CMND
                                        {
                                            if (!string.IsNullOrWhiteSpace(txtCMND))
                                            {
                                                giangVienThinhGiang.CMND = txtCMND;
                                            }
                                        }
                                        #endregion

                                        #region Ngày cấp CMND
                                        {
                                            if (!string.IsNullOrEmpty(txtNgayCapCMND))
                                            {
                                                try
                                                {
                                                    giangVienThinhGiang.NgayCap = Convert.ToDateTime(txtNgayCapCMND);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày cấp CMND không hợp lệ: " + txtNgayCapCMND);
                                                }
                                            }
                                            //else
                                            //{
                                            //    //giangVienThinhGiang.NgayCap = Convert.ToDateTime("01/01/1960");
                                            //    detailLog.AppendLine(" + Thiếu thông tin cấp CMND hoặc không đúng định dạng dd/MM/yyyy.");
                                            //}


                                        }
                                        #endregion

                                        #region Nơi cấp CMND
                                        {
                                            if (!string.IsNullOrEmpty(txtNoiCapCMND))
                                            {
                                                TinhThanh tinhThanh = null;
                                                tinhThanh = uow.FindObject<TinhThanh>(CriteriaOperator.Parse("TenTinhThanh like ?", txtNoiCapCMND));
                                                if (tinhThanh != null)
                                                {
                                                    giangVienThinhGiang.NoiCap = tinhThanh;
                                                }
                                                else {
                                                    detailLog.AppendLine(" + Nơi cấp CMND không hợp lệ: " + txtNoiCapCMND);
                                                }
                                            }
                                        }
                                        #endregion

                                        #region Thường trú
                                        {
                                            if (!string.IsNullOrEmpty(txtDiaChiThuongTru))
                                            {
                                                DiaChi diaChi = null;
                                                diaChi = uow.FindObject<DiaChi>(CriteriaOperator.Parse("SoNha like ?", txtDiaChiThuongTru));
                                                if (diaChi == null)
                                                {
                                                    diaChi = new DiaChi(uow);
                                                    diaChi.SoNha = txtDiaChiThuongTru;
                                                    diaChi.Save();
                                                }
                                                giangVienThinhGiang.DiaChiThuongTru = diaChi;
                                            }
                                        }
                                        #endregion

                                        #region Tạm trú - Nơi ở hiện nay
                                        {
                                            if (!string.IsNullOrEmpty(txtDiaChiTamTru))
                                            {
                                                DiaChi diaChi = null;
                                                diaChi = uow.FindObject<DiaChi>(CriteriaOperator.Parse("SoNha like ?", txtDiaChiTamTru));
                                                if (diaChi == null)
                                                {
                                                    diaChi = new DiaChi(uow);
                                                    diaChi.SoNha = txtDiaChiTamTru;
                                                    diaChi.Save();
                                                }
                                                giangVienThinhGiang.NoiOHienNay = diaChi;
                                            }
                                        }
                                        #endregion

                                        #region Số điện thoại
                                        {
                                            if (!string.IsNullOrEmpty(txtDienThoaiNhaRieng))
                                            {
                                                giangVienThinhGiang.DienThoaiNhaRieng = txtDienThoaiNhaRieng;
                                            }
                                        }
                                        #endregion

                                        #region Số điện thoại di động
                                        {
                                            if (!string.IsNullOrEmpty(txtDienThoaiDiDong))
                                            {
                                                giangVienThinhGiang.DienThoaiDiDong = txtDienThoaiDiDong;
                                            }
                                        }
                                        #endregion

                                        #region Email
                                        {
                                            if (!string.IsNullOrEmpty(txtEmail))
                                            {
                                                giangVienThinhGiang.Email = txtEmail;
                                            }
                                        }
                                        #endregion

                                        #region Số tài khoản
                                        if (!string.IsNullOrWhiteSpace(txtSoTaiKhoan))
                                        {
                                            TaiKhoanNganHang tknh = null;
                                            tknh = uow.FindObject<TaiKhoanNganHang>(CriteriaOperator.Parse("SoTaiKhoan = ?", txtSoTaiKhoan));
                                            if (tknh == null)
                                            {
                                                tknh = new TaiKhoanNganHang(uow);
                                                tknh.SoTaiKhoan = txtSoTaiKhoan;
                                                tknh.TaiKhoanChinh = true;
                                            }
                                            tknh.NhanVien = giangVienThinhGiang;
                                            tknh.Save();
                                        }
                                        #endregion

                                        #region Quốc tịch
                                        if (!string.IsNullOrWhiteSpace(txtQuocTich))
                                        {
                                            QuocGia qt = null;
                                            qt = uow.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia = ?", txtQuocTich));
                                            if (qt != null)
                                            {
                                                giangVienThinhGiang.QuocTich = qt;
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Quốc gia không hợp lệ: " + txtQuocTich);
                                            }
                                        }
                                        #endregion

                                        #region Đơn vị công tác
                                        {
                                            if (!string.IsNullOrWhiteSpace(txtDonViCongTac))
                                            {
                                                giangVienThinhGiang.DonViCongTac = txtDonViCongTac;
                                            }
                                        }
                                        #endregion

                                        #region Ghi File log
                                        {
                                            //Đưa thông tin bị lỗi vào blog
                                            if (detailLog.Length > 0)
                                            {
                                                mainLog.AppendLine(string.Format("- Giảng viên thỉnh giảng Mã: {0} Tên: {1} không import vào phần mềm được: ", giangVienThinhGiang.MaNhanVien, giangVienThinhGiang.HoTen));
                                                mainLog.AppendLine(detailLog.ToString());
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
                    DialogUtil.ShowInfo("Import Thành Công " + sucessNumber + " Giảng viên thỉnh giảng - Số giảng viên import không thành công " + erorrNumber + " " + s + "!");

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
