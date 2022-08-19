using DevExpress.XtraEditors;
using ERP.Module.Commons;
using ERP.Module.MailMerge;
using System;
using ERP.Module.Win.MailMerge.Prosess.ShowMaiMerge;
using System.Collections.Generic;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using System.Windows.Forms;
using ERP.Module.Enum.NhanSu;
using ERP.Module.NghiepVu.NhanSu.HopDongs;
using ERP.Module.MailMerge.NhanSu.QuyetDinh;

namespace ERP.Module.Win.MailMerge.Prosess.HopDong
{
    public static class Process_BieuMauKhiKyLaiHopDong
    {
        public static void ShowMailMerge(DevExpress.ExpressApp.IObjectSpace obs, List<HopDongLamViec> hopDongLamViecList)
        {
            var non_NhanVienList = new List<Non_QuyetDinhNhanVien>();
            Non_QuyetDinhNhanVien nhanVien;
            foreach (HopDongLamViec hopDongLamViec in hopDongLamViecList)
            {
                ThongTinNhanVien thongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(hopDongLamViec.ThongTinNhanVien.Oid);
                nhanVien = new Non_QuyetDinhNhanVien();

                nhanVien.Oid = thongTinNhanVien.Oid.ToString();
                nhanVien.NgayHienTai = Common.GetServerCurrentTime().ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                nhanVien.NgayHienTaiDate = Common.GetServerCurrentTime().ToString("dd/MM/yyyy");
                nhanVien.NgayVaoCoQuan = thongTinNhanVien.NgayVaoCongTy.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");

                nhanVien.DonViChuQuan = thongTinNhanVien.CongTy != null ? thongTinNhanVien.CongTy.DonViChuQuan : string.Empty;
                nhanVien.TenCongTyVietHoa = thongTinNhanVien.CongTy != null ? thongTinNhanVien.CongTy.TenBoPhan.ToUpper() : "";
                nhanVien.TenCongTyVietThuong = thongTinNhanVien.CongTy != null ? thongTinNhanVien.CongTy.TenBoPhan : "";
                nhanVien.DiaChiCongTyHoacTruong = (thongTinNhanVien.CongTy != null && thongTinNhanVien.CongTy.DiaChi != null) ? thongTinNhanVien.CongTy.DiaChi.FullDiaChi : "";

                nhanVien.ChucVuNguoiKy = hopDongLamViec.ChucVuNguoiKy != null ? hopDongLamViec.ChucVuNguoiKy.ChucDanh.TenChucDanh.ToUpper() : "";
                nhanVien.ChucVuNguoiKyVietThuong = hopDongLamViec.ChucVuNguoiKy != null ? hopDongLamViec.ChucVuNguoiKy.ChucDanh.TenChucDanh : "";
                if (hopDongLamViec.NguoiKy != null)
                {
                    nhanVien.TenNguoiKyVietHoa = hopDongLamViec.NguoiKy != null ? hopDongLamViec.NguoiKy.HoTen.ToUpper() : hopDongLamViec.TenNguoiKy.ToUpper();
                    nhanVien.TenNguoiKyVietThuong = hopDongLamViec.NguoiKy != null ? hopDongLamViec.NguoiKy.HoTen : hopDongLamViec.TenNguoiKy;
                    nhanVien.DanhXungNguoiKyVietHoa = hopDongLamViec.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    nhanVien.DanhXungNguoiKyVietThuong = hopDongLamViec.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                }

                if (thongTinNhanVien.CongTy.LoaiTruong == null || thongTinNhanVien.CongTy.LoaiTruong == Enum.TuyenSinh_PT.LoaiTruongEnum.NA)
                {
                    nhanVien.BanTongGiamDocHoacBanGiamHieu = "Ban Tổng giám đốc";
                    nhanVien.DanhXungCongTyHoacTruong = "Công ty";
                }
                else 
                {
                    nhanVien.BanTongGiamDocHoacBanGiamHieu = "Ban giám hiệu";
                    nhanVien.DanhXungCongTyHoacTruong = "Trường";
                }
               
                // Thông tin nhân viên
                nhanVien.HoTenVietHoa = thongTinNhanVien.HoTen.ToUpper();
                nhanVien.HoTenVietThuong = thongTinNhanVien.HoTen;
                nhanVien.SoCMND = thongTinNhanVien.CMND;
                nhanVien.NgayCapCMND = thongTinNhanVien.NgayCap != DateTime.MinValue ? thongTinNhanVien.NgayCap.ToString("dd/MM/yyyy") : "";
                nhanVien.NoiCapCMND = thongTinNhanVien.NoiCap != null ? thongTinNhanVien.NoiCap.TenTinhThanh : "";
                nhanVien.NgaySinh = thongTinNhanVien.NgaySinh != DateTime.MinValue ? thongTinNhanVien.NgaySinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                nhanVien.NgaySinhDate = thongTinNhanVien.NgaySinh != DateTime.MinValue ? thongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy") : "";
                nhanVien.NoiSinh = thongTinNhanVien.NoiSinh != null ? thongTinNhanVien.NoiSinh.FullDiaChi : "";
                nhanVien.GioiTinh = thongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Nam" : "Nữ";
                nhanVien.QuocTich = thongTinNhanVien.QuocTich != null ? thongTinNhanVien.QuocTich.TenQuocGia : "";
                nhanVien.DiaChiThuongTru = thongTinNhanVien.DiaChiThuongTru != null ? thongTinNhanVien.DiaChiThuongTru.FullDiaChi : "";
                nhanVien.DiaChiLienLac = thongTinNhanVien.NoiOHienNay != null ? thongTinNhanVien.NoiOHienNay.FullDiaChi : "";
                nhanVien.DienThoaiDiDong = thongTinNhanVien.DienThoaiDiDong;
                nhanVien.DonVi = thongTinNhanVien.BoPhan.TenBoPhan;
                nhanVien.DanhXungVietHoa = thongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                nhanVien.DanhXungVietThuong = thongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                nhanVien.LoaiHopDong = thongTinNhanVien.LoaiHopDong.TenLoaiHopDong;
                nhanVien.ChucVu = thongTinNhanVien.ChucVu != null ? thongTinNhanVien.ChucVu.TenChucVu : "";
                nhanVien.ChucDanh = thongTinNhanVien.ChucDanh != null ? thongTinNhanVien.ChucDanh.TenChucDanh : "";
                nhanVien.CongViecHienTai = thongTinNhanVien.CongViecHienNay != null ? thongTinNhanVien.CongViecHienNay.TenCongViec : "";
                nhanVien.TrinhDoVanHoa = thongTinNhanVien.NhanVienTrinhDo.TrinhDoVanHoa != null ? thongTinNhanVien.NhanVienTrinhDo.TrinhDoVanHoa.TenTrinhDoVanHoa : "";

                //
                if (hopDongLamViec.InThoaThuan) //Check in thoả thuận trong hợp đồng
                    non_NhanVienList.Add(nhanVien);
            }
            if (hopDongLamViecList.Count > 0)
            {
                MailMergeTemplate merge = Common.GetTemplateWithValidDate(obs, "BieuMauKhiDatTuyenDung.rtf", hopDongLamViecList[0].QuanLyHopDong.CongTy.Oid, hopDongLamViecList[0].NgayKy);
                if (merge != null)
                    Prosess_Show.ShowEditorWithValidDate<Non_QuyetDinhNhanVien>(non_NhanVienList, obs, hopDongLamViecList[0].NgayKy, merge);
                else
                    XtraMessageBox.Show("Không tìm thấy mẫu in thỏa thuận trong hệ thống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
