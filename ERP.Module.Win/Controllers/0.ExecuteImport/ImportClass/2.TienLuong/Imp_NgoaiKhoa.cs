using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
using ERP.Module.Extends;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ERP.Module.NghiepVu.TienLuong.Luong;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.TienLuong.ThuNhapKhac;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong
{
    public class Imp_NgoaiKhoa
    {        
        public static void ImportNgoaiKhoa(IObjectSpace obs, HoSoTinhLuong obj, LoaiOfficeEnum loaiOffice)
        {
            //
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            StringBuilder mainLog = new StringBuilder();
            DateTime ngayLap = DateTime.Now;
            if (obj.KyTinhLuong != null)
                ngayLap = obj.KyTinhLuong.DenNgay.AddDays(1);
            //

            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel 2003 file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        using (DataTable dt = DataProvider.GetDataTableFromExcel(open.FileName, "[Sheet$A7:AW]", loaiOffice))//Sheet$A7:AW
                        {
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các idx
                            int idx_STT = 0;
                            int idx_MaQuanLy = 1;
                            int idx_HoTen = 2;
                            int idx_ChucDanh = 3;//Nguyen
                            int idx_DonVi = 4;                           
                            int idx_AbiKindyEngLish = 5;
                            int idx_AfterSchoolPhoThong = 6;
                            int idx_AfterSchoolMamNon = 7;
                            int idx_HappySix = 8;
                            int idx_HappySaturday = 9;
                            int idx_GoKid = 10;
                            int idx_NgheThuatSangTao = 11;
                            int idx_TaoHinh = 12;
                            int idx_Soroban = 13;
                            int idx_HappyKido = 14;                            
                            int idx_PhuDao = 15;
                            int idx_NangCaoBoiDuong = 16;
                            int idx_Nghe = 17;
                            int idx_HoatDongHe = 18;
                            int idx_ThuBayChuNhat = 19;
                            int idx_HocSinhGioi = 20;
                            int idx_LuyenThi = 21;
                            //int idx_GhiChu = 22; //Nguyen bo
                            int idx_TongCong = 22; //Nguyen them              
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////
                            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();

                                #region Mã phân bổ                                
                                string txt_AbiKindyEngLish_MPB = string.Empty;
                                string txt_AfterSchoolPhoThong_MPB = string.Empty;
                                string txt_AfterSchoolMamNon_MPB = string.Empty;
                                string txt_HappySix_MPB = string.Empty;
                                string txt_HappySaturday_MPB = string.Empty;
                                string txt_GoKid_MPB = string.Empty;
                                string txt_NgheThuatSangTao_MPB = string.Empty;
                                string txt_TaoHinh_MPB = string.Empty;
                                string txt_Soroban_MPB = string.Empty;
                                string txt_HappyKido_MPB = string.Empty;
                                string txt_PhuDao_MPB = string.Empty;
                                string txt_NangCaoBoiDuong_MPB = string.Empty;
                                string txt_Nghe_MPB = string.Empty;
                                string txt_HoatDongHe_MPB = string.Empty;
                                string txt_ThuBayChuNhat_MPB = string.Empty;
                                string txt_HocSinhGioi_MPB = string.Empty;
                                string txt_LuyenThi_MPB = string.Empty;                               
                                #endregion

                                //Duyệt qua tất cả các dòng trong file excel
                                foreach (DataRow dr in dt.Rows)
                                {
                                    StringBuilder detailLog = new StringBuilder();

                                    //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                    #region Đọc dữ liệu
                                    string txt_STT = dr[idx_STT].ToString().Trim();
                                    string txt_MaQuanLy = dr[idx_MaQuanLy].ToString().Trim();
                                    string txt_HoTen = dr[idx_HoTen].ToString().Trim();
                                    string txt_ChucDanh = dr[idx_ChucDanh].ToString().Trim();//Nguyen
                                    string txt_DonVi = dr[idx_DonVi].ToString().Trim();
                                    string txt_AbiKindyEngLish = dr[idx_AbiKindyEngLish].ToString().Trim();
                                    string txt_AfterSchoolPhoThong = dr[idx_AfterSchoolPhoThong].ToString().Trim();
                                    string txt_AfterSchoolMamNon = dr[idx_AfterSchoolMamNon].ToString().Trim();                                   
                                    string txt_HappySix = dr[idx_HappySix].ToString().Trim();                                    
                                    string txt_HappySaturday = dr[idx_HappySaturday].ToString().Trim();
                                    string txt_GoKid = dr[idx_GoKid].ToString().Trim();
                                    string txt_NgheThuatSangTao = dr[idx_NgheThuatSangTao].ToString().Trim();
                                    string txt_TaoHinh = dr[idx_TaoHinh].ToString().Trim();
                                    string txt_Soroban = dr[idx_Soroban].ToString().Trim();
                                    string txt_HappyKido = dr[idx_HappyKido].ToString().Trim();
                                    string txt_PhuDao = dr[idx_PhuDao].ToString().Trim();
                                    string txt_NangCaoBoiDuong = dr[idx_NangCaoBoiDuong].ToString().Trim();
                                    string txt_Nghe = dr[idx_Nghe].ToString().Trim();
                                    string txt_HoatDongHe = dr[idx_HoatDongHe].ToString().Trim();
                                    string txt_ThuBayChuNhat = dr[idx_ThuBayChuNhat].ToString().Trim();
                                    string txt_HocSinhGioi = dr[idx_HocSinhGioi].ToString().Trim();
                                    string txt_LuyenThi = dr[idx_LuyenThi].ToString().Trim();
                                    //string txt_GhiChu = dr[idx_GhiChu].ToString().Trim(); //Nguyen bo
                                    string txt_TongCong = dr[idx_TongCong].ToString().Trim(); //Nguyen them
                                    #endregion

                                    //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                    #region Kiểm tra dữ
                                    //
                                    #region 1. Mã quản lý
                                    if (!string.IsNullOrEmpty(txt_MaQuanLy))
                                    {
                                        ThongTinNhanVien nhanVien_qd = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("CongTy = ? and (MaNhanVien like ? or MaTapDoan like ?)", obj.CongTy.Oid, txt_MaQuanLy, txt_MaQuanLy));
                                        //if (!string.IsNullOrEmpty(txt_GhiChu))
                                        //{
                                        //    nhanVien_qd = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("CongTy.MaBoPhan like ? and (MaNhanVien like ? or MaTapDoan like ?)", txt_GhiChu, txt_MaQuanLy, txt_MaQuanLy));
                                        //} //Nguyen bo
                                        if (nhanVien_qd == null)
                                        {
                                            if (txt_MaQuanLy.Contains("Số TK") || txt_MaQuanLy.Contains("tài khoản"))
                                            {
                                                if (!string.IsNullOrEmpty(txt_AbiKindyEngLish))
                                                    txt_AbiKindyEngLish_MPB = txt_AbiKindyEngLish;
                                                if (!string.IsNullOrEmpty(txt_AfterSchoolPhoThong))
                                                    txt_AfterSchoolPhoThong_MPB = txt_AfterSchoolPhoThong;
                                                if (!string.IsNullOrEmpty(txt_AfterSchoolMamNon))
                                                    txt_AfterSchoolMamNon_MPB = txt_AfterSchoolMamNon;
                                                if (!string.IsNullOrEmpty(txt_HappySix))
                                                    txt_HappySix_MPB = txt_HappySix;
                                                if (!string.IsNullOrEmpty(txt_HappySaturday))
                                                    txt_HappySaturday_MPB = txt_HappySaturday;
                                                if (!string.IsNullOrEmpty(txt_TaoHinh))
                                                    txt_TaoHinh_MPB = txt_TaoHinh;
                                                if (!string.IsNullOrEmpty(txt_Soroban))
                                                    txt_Soroban_MPB = txt_Soroban;
                                                if (!string.IsNullOrEmpty(txt_HappyKido))
                                                    txt_HappyKido_MPB = txt_HappyKido;
                                                if (!string.IsNullOrEmpty(txt_PhuDao))
                                                    txt_PhuDao_MPB = txt_PhuDao;
                                                if (!string.IsNullOrEmpty(txt_NangCaoBoiDuong))
                                                    txt_NangCaoBoiDuong_MPB = txt_NangCaoBoiDuong;
                                                if (!string.IsNullOrEmpty(txt_Nghe))
                                                    txt_Nghe_MPB = txt_Nghe;
                                                if (!string.IsNullOrEmpty(txt_HoatDongHe))
                                                    txt_HoatDongHe_MPB = txt_HoatDongHe;
                                                if (!string.IsNullOrEmpty(txt_ThuBayChuNhat))
                                                    txt_ThuBayChuNhat_MPB = txt_ThuBayChuNhat;
                                                if (!string.IsNullOrEmpty(txt_HocSinhGioi))
                                                    txt_HocSinhGioi_MPB = txt_HocSinhGioi;
                                                if (!string.IsNullOrEmpty(txt_LuyenThi))
                                                    txt_LuyenThi_MPB = txt_LuyenThi;                                          
                                            }
                                            else
                                            {
                                                mainLog.AppendLine("- STT: " + txt_STT);
                                                mainLog.AppendLine(string.Format("- Mã quản lý :{0} của nhân viên : {1} không tồn tại trong hệ thống.", txt_MaQuanLy, txt_HoTen));
                                                //
                                                sucessImport = false;
                                            }
                                        }
                                        else
                                        {
                                            //
                                            ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("CongTy = ? and (MaNhanVien like ? or MaTapDoan like ?)", obj.CongTy.Oid, txt_MaQuanLy, txt_MaQuanLy));
                                            if (nhanVien != null)
                                            {                                               
                                                #region Phụ cấp Abi Kindy English
                                                if (!string.IsNullOrEmpty(txt_AbiKindyEngLish) && !txt_AbiKindyEngLish.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal abiKindyEngLish = Convert.ToDecimal(txt_AbiKindyEngLish);

                                                        if (abiKindyEngLish > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienAbiKindyEnglish"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = abiKindyEngLish;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Abi Kindy English (5) trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Abi Kindy English (5) không đúng định dạng: " + txt_AbiKindyEngLish);
                                                    }
                                                }
                                                #endregion

                                                #region AfterSchool Phổ thông                                                                                         
                                                if (!string.IsNullOrEmpty(txt_AfterSchoolPhoThong) && !txt_AfterSchoolPhoThong.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal afterSchoolPhoThong = Convert.ToDecimal(txt_AfterSchoolPhoThong);
                                                        if (afterSchoolPhoThong > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienAfterSchool"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = afterSchoolPhoThong;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Afterschool Phổ thông trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Afterschool Phổ thông (6) không đúng định dạng: " + txt_AfterSchoolPhoThong);
                                                    }
                                                }
                                                #endregion

                                                #region AfterSchool Mầm non
                                                if (!string.IsNullOrEmpty(txt_AfterSchoolMamNon) && !txt_AfterSchoolMamNon.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal afterSchoolMamNon = Convert.ToDecimal(txt_AfterSchoolMamNon);
                                                        if (afterSchoolMamNon > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "AfterschoolMamNon"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = afterSchoolMamNon;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Afterschool Mầm non (7) trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Afterschool Mầm non (7) không đúng định dạng: " + txt_AfterSchoolMamNon);
                                                    }
                                                }
                                                #endregion
                                                
                                                #region Happy Six
                                                if (!string.IsNullOrEmpty(txt_HappySix) && !txt_HappySix.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal happySix = Convert.ToDecimal(txt_HappySix);
                                                        if (happySix > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienHappySix"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = happySix;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Happy Six (8) trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Happy Six (8) không đúng định dạng: " + txt_HappySix);
                                                    }
                                                }
                                                #endregion

                                                #region Happy Saturday
                                                if (!string.IsNullOrEmpty(txt_HappySaturday) && !txt_HappySaturday.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal happySa = Convert.ToDecimal(txt_HappySaturday);
                                                        if (happySa > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienHappySaturday"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = happySa;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy HappySaturday (9) trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ HappySaturday (9) không đúng định dạng: " + txt_HappySaturday);
                                                    }
                                                }
                                                #endregion

                                                #region Gokids                                                                         
                                                if (!string.IsNullOrEmpty(txt_GoKid) && !txt_GoKid.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal goKid = Convert.ToDecimal(txt_GoKid);
                                                        if (goKid > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienGoKids"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = goKid;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Gokids (10) trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Gokids (10) không đúng định dạng: " + txt_GoKid);
                                                    }
                                                }
                                                #endregion

                                                #region Nghệ thuật sáng tạo                                                                                          
                                                if (!string.IsNullOrEmpty(txt_NgheThuatSangTao) && !txt_NgheThuatSangTao.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal ngheThuatSangTao = Convert.ToDecimal(txt_NgheThuatSangTao);
                                                        if (ngheThuatSangTao > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienNgheThuatSangTao"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = ngheThuatSangTao;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Nghệ thuật sáng tạo (11) trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Nghệ thuật sáng tạo (11) không đúng định dạng: " + txt_NgheThuatSangTao);
                                                    }
                                                }
                                                #endregion

                                                #region Tạo hình                                                                                          
                                                if (!string.IsNullOrEmpty(txt_TaoHinh) && !txt_TaoHinh.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal taoHinh = Convert.ToDecimal(txt_TaoHinh);
                                                        if (taoHinh > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienTaoHinh"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = taoHinh;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Tạo hình (12) trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Tạo hình (12) không đúng định dạng: " + txt_TaoHinh);
                                                    }
                                                }
                                                #endregion

                                                #region Soroban                                                                                          
                                                if (!string.IsNullOrEmpty(txt_Soroban) && !txt_Soroban.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal soroban = Convert.ToDecimal(txt_Soroban);

                                                        LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienSoroban"));
                                                        if (loaiTNK != null)
                                                        {
                                                            BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                            if (bangTNK == null)
                                                            {
                                                                bangTNK = new BangThuNhapKhac(uow);                                                               
                                                                bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                bangTNK.NgayLap = ngayLap;
                                                                bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                            }
                                                            ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                            if (chiTietTNK == null)
                                                            {
                                                                chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                chiTietTNK.NgayLap = ngayLap;
                                                                chiTietTNK.GhiChu = "Nhập từ file excel";
                                                            }
                                                            if (nhanVien.NhomPhanBo != null)
                                                                chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                            chiTietTNK.SoTien = 0;
                                                            chiTietTNK.SoTienChiuThue = soroban;
                                                        }
                                                        else
                                                        {
                                                            detailLog.Append("+ Không tìm thấy Soroban (13) trong Loại thu nhập khác");
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Soroban (13) không đúng định dạng: " + txt_Soroban);
                                                    }
                                                }
                                                #endregion

                                                #region Tiền HappyKido                                                                                        
                                                if (!string.IsNullOrEmpty(txt_HappyKido) && !txt_HappyKido.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal happyKido = Convert.ToDecimal(txt_HappyKido);
                                                        if (happyKido > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienHappyKido"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = happyKido;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Tiền HappyKido (14) trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Tiền HappyKido (14) không đúng định dạng: " + txt_HappyKido);
                                                    }
                                                }
                                                #endregion

                                                #region Phụ đạo                                                                                        
                                                if (!string.IsNullOrEmpty(txt_PhuDao) && !txt_PhuDao.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal phuDao = Convert.ToDecimal(txt_PhuDao);
                                                        if (phuDao > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienPhuDao"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = phuDao;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Phụ đạo (13) trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Phụ đạo (13) không đúng định dạng: " + txt_PhuDao);
                                                    }
                                                }
                                                #endregion

                                                #region Nâng cao = Học sinh giỏi = Bồi dưỡng                                                                                      
                                                if (!string.IsNullOrEmpty(txt_NangCaoBoiDuong) && !txt_NangCaoBoiDuong.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal nangCao = Convert.ToDecimal(txt_NangCaoBoiDuong);
                                                        if (nangCao > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienBoiDuong"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = nangCao;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Nâng cao = Học sinh giỏi = Bồi dưỡng (16) trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Nâng cao = Học sinh giỏi = Bồi dưỡng (16) không đúng định dạng: " + txt_NangCaoBoiDuong);
                                                    }
                                                }
                                                #endregion

                                                #region Nghề                                                                                      
                                                if (!string.IsNullOrEmpty(txt_Nghe) && !txt_Nghe.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal nghe = Convert.ToDecimal(txt_Nghe);
                                                        if (nghe > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienNghe"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = nghe;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Nghề (17) trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Nghề (17) không đúng định dạng: " + txt_Nghe);
                                                    }
                                                }
                                                #endregion

                                                #region Hoạt Động Hè                                                                                      
                                                if (!string.IsNullOrEmpty(txt_HoatDongHe) && !txt_HoatDongHe.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal hoatDongHe = Convert.ToDecimal(txt_HoatDongHe);
                                                        if (hoatDongHe > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienHoatDongHe"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = hoatDongHe;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Hoạt Động Hè (18) trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Hoạt Động Hè (18) không đúng định dạng: " + txt_HoatDongHe);
                                                    }
                                                }
                                                #endregion

                                                #region Thứ 7-Chủ nhật                                                                                    
                                                if (!string.IsNullOrEmpty(txt_ThuBayChuNhat) && !txt_ThuBayChuNhat.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal thubaychunhat = Convert.ToDecimal(txt_ThuBayChuNhat);
                                                        if (thubaychunhat > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienThuBayChuNhat"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = thubaychunhat;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Thứ 7-Chủ nhật (19) trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Thứ 7-Chủ nhật (19) không đúng định dạng: " + txt_ThuBayChuNhat);
                                                    }
                                                }
                                                #endregion

                                                #region Học sinh giỏi                                                                                    
                                                if (!string.IsNullOrEmpty(txt_HocSinhGioi) && !txt_HocSinhGioi.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal hocSinhGioi = Convert.ToDecimal(txt_HocSinhGioi);
                                                        if (hocSinhGioi > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienHocSinhGioi"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = hocSinhGioi;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Học sinh giỏi (20) trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Học sinh giỏi (20) không đúng định dạng: " + txt_HocSinhGioi);
                                                    }
                                                }
                                                #endregion

                                                #region Luyện thi                                                                                    
                                                if (!string.IsNullOrEmpty(txt_LuyenThi) && !txt_LuyenThi.Contains("-"))
                                                {
                                                    try
                                                    {
                                                        decimal luyenThi = Convert.ToDecimal(txt_LuyenThi);
                                                        if (luyenThi > 0)
                                                        {
                                                            LoaiThuNhapKhac loaiTNK = uow.FindObject<LoaiThuNhapKhac>(CriteriaOperator.Parse("MaQuanLy=?", "TienLuyenThi"));
                                                            if (loaiTNK != null)
                                                            {
                                                                BangThuNhapKhac bangTNK = uow.FindObject<BangThuNhapKhac>(CriteriaOperator.Parse("KyTinhLuong=? AND LoaiThuNhapKhac=?", obj.KyTinhLuong.Oid, loaiTNK.Oid));
                                                                if (bangTNK == null)
                                                                {
                                                                    bangTNK = new BangThuNhapKhac(uow);
                                                                    bangTNK.CongTy = uow.GetObjectByKey<CongTy>(obj.CongTy.Oid);
                                                                    bangTNK.LoaiThuNhapKhac = loaiTNK;
                                                                    bangTNK.NgayLap = ngayLap;
                                                                    bangTNK.KyTinhLuong = uow.GetObjectByKey<KyTinhLuong>(obj.KyTinhLuong.Oid);
                                                                }
                                                                ChiTietThuNhapKhac chiTietTNK = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? AND ThongTinNhanVien=?", bangTNK.Oid, nhanVien.Oid));
                                                                if (chiTietTNK == null)
                                                                {
                                                                    chiTietTNK = new ChiTietThuNhapKhac(uow);
                                                                    chiTietTNK.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                                    chiTietTNK.ThongTinNhanVien = nhanVien;
                                                                    chiTietTNK.BangThuNhapKhac = bangTNK;
                                                                    chiTietTNK.NgayLap = ngayLap;
                                                                    chiTietTNK.GhiChu = "Nhập từ file excel";
                                                                }
                                                                if (nhanVien.NhomPhanBo != null)
                                                                    chiTietTNK.NhomPhanBo = uow.GetObjectByKey<NhomPhanBo>(nhanVien.NhomPhanBo.Oid);
                                                                chiTietTNK.SoTien = 0;
                                                                chiTietTNK.SoTienChiuThue = luyenThi;
                                                            }
                                                            else
                                                            {
                                                                detailLog.Append("+ Không tìm thấy Luyện thi (21) trong Loại thu nhập khác");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.Append("+ Luyện thi (21) không đúng định dạng: " + txt_LuyenThi);
                                                    }
                                                }
                                                #endregion                                         

                                            }
                                            else
                                            {
                                                detailLog.AppendLine(string.Format("- Nhân viên Mã: {0} Tên: {1} không thuộc đơn vị quản lý ", nhanVien_qd.MaNhanVien, nhanVien_qd.HoTen));
                                            }

                                            #region Ghi File log
                                            {
                                                //Đưa thông tin bị lỗi vào blog
                                                if (detailLog.Length > 0)
                                                {
                                                    mainLog.AppendLine("- STT: " + txt_STT);
                                                    mainLog.AppendLine(string.Format("- Nhân viên Mã: {0} Tên: {1} không import vào phần mềm được: ", nhanVien_qd.MaNhanVien, nhanVien_qd.HoTen));
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
                                    //
                                    #endregion

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

    }
}
