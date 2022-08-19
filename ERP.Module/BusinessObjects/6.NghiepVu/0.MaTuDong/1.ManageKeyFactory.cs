using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using ERP.Module.Enum.NhanSu;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.NghiepVu.MaTuDong
{
    public static class ManageKeyFactory
    {
        /// <summary>
        /// Tạo mã quản lý, số, ...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string ManageKey(ManageKeyEnum type, params SqlParameter[] args)
        {
            ManageKeyBase maQuanLy = CreateManageKey(type);
            return maQuanLy.ManageKey(args);
        }

        public static string ManageKeyCompany(ManageKeyEnum type, CongTy congTy, params SqlParameter[] args)
        {
            ManageKeyCompanyBase maQuanLy = CreateManageCompanyKey(type);
            return maQuanLy.ManageKeyCompany(congTy, args);
        }

        private static ManageKeyBase CreateManageKey(ManageKeyEnum type)
        {
            switch (type)
            {
                case ManageKeyEnum.MaNhanVien:
                    return new MaNhanVien();
                case ManageKeyEnum.SoHopDongThinhGiang:
                    return new MaHopDongThinhGiang();
                // Tuyển sinh
                case ManageKeyEnum.MaKhachHang:
                    return new MaKhachHang_TuyenSinh();
                case ManageKeyEnum.MaDanhSachCho:
                    return new MaDanhSachCho_TuyenSinh();
                case ManageKeyEnum.MaDuThi:
                    return new MaDuThi_TuyenSinh();
                case ManageKeyEnum.MaHoSo:
                    return new MaHoSo_TuyenSinh();
                case ManageKeyEnum.MaMember:
                    return new MaMember_TuyenSinh();
                //
                case ManageKeyEnum.MaHoaDonHocPhi:
                    return new MaHoaDonHocPhi();
                case ManageKeyEnum.MaBienLaiHocPhi:
                    return new MaBienLaiHocPhi();
                //Mã phiếu chi
                case ManageKeyEnum.MaPhieuChi:                
                    return new MaPhieuChi();
                //Mã ký hiệu biên lai
                case ManageKeyEnum.MaKyHieuBienLai:
                    return new MaKyHieuBienLai();              
                default:
                    return new MaNhanVien();                
            }
        }


        private static ManageKeyCompanyBase CreateManageCompanyKey(ManageKeyEnum type)
        {
            switch (type)
            {
                case ManageKeyEnum.SoHopDongLamViec:
                    return new MaHopDongLamViec();
                case ManageKeyEnum.SoHopDongThuViec:
                    return new MaHopDongThuViec();
                case ManageKeyEnum.SoPhuLucHopDong:
                    return new MaPhuLucHopDong();
                case ManageKeyEnum.SoHopDongKhoan:
                    return new MaHopDongKhoan();                
                default:
                    return new MaHopDongLamViec();
            }
        }
    }
}
