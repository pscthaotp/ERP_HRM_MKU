using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp;
using ERP.Module.NghiepVu.NhanSu.HopDongs;
using ERP.Module.MailMerge.NhanSu.HopDong;
using ERP.Module.Enum.NhanSu;
using ERP.Module.MailMerge;
using ERP.Module.Commons;
using ERP.Module.Win.MailMerge.Prosess.ShowMaiMerge;
using ERP.Module.Extends;

//
namespace ERP.Module.Win.MailMerge.Prosess.HopDong
{
    public static class Prosess_HopDongKhoan
    {
        public static void ShowMailMerge_Yersin(IObjectSpace obs, List<HopDongKhoan> hdList)
        {
            var hopDongCongTacVienGrossList = new List<Non_HopDongKhoan>();
            var hopDongCongTacVienNetList = new List<Non_HopDongKhoan>();
            //
            foreach (HopDongKhoan hopDong in hdList)
            {
                Non_HopDongKhoan hd = new Non_HopDongKhoan();
                //
                hd.Oid = hopDong.Oid.ToString();
                hd.DonViChuQuan = hopDong.QuanLyHopDong.CongTy != null ? hopDong.QuanLyHopDong.CongTy.DonViChuQuan : "";
                hd.TenCongTyVietHoa = hopDong.QuanLyHopDong.CongTy.TenBoPhan.ToUpper();
                hd.TenCongTyVietThuong = hopDong.QuanLyHopDong.CongTy.TenBoPhan;
                hd.DiaChi = hopDong.QuanLyHopDong.CongTy.DiaChi!= null ? hopDong.QuanLyHopDong.CongTy.DiaChi.FullDiaChi : "";
                hd.SoDienThoai = hopDong.QuanLyHopDong.CongTy.DienThoai;
                hd.SoFax = hopDong.QuanLyHopDong.CongTy.Fax;
                hd.LoaiHopDong = hopDong.LoaiHopDong != null ? hopDong.LoaiHopDong.TenLoaiHopDong : "";
                hd.LoaiHopDongKhoan = hopDong.LoaiHopDongKhoan == LoaiHopDongKhoanEnum.Gross ? "Hợp đồng cộng tác viên gorss" : "Hợp đồng cộng tác viên net";
                hd.SoHopDong = hopDong.SoHopDong;
                hd.NgayKy = hopDong.NgayKy.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.NgayKyDate = hopDong.NgayKy.ToString("dd/MM/yyyy");
                hd.TenChucVuNguoiKy = hopDong.ChucVuNguoiKy != null ? hopDong.ChucVuNguoiKy.ChucVu.TenChucVu : "";
                hd.CanCu = hopDong.CanCu;
                if (hopDong.NguoiKy != null)
                {
                    hd.DanhXungNguoiKy = hopDong.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    hd.TenNguoiKyVietHoa = hopDong.NguoiKy.HoTen != "" ? hopDong.NguoiKy.HoTen.ToUpper() : hopDong.TenNguoiKy;
                    hd.TenNguoiKyVietThuong = hopDong.NguoiKy.HoTen != "" ? hopDong.NguoiKy.HoTen  : hopDong.TenNguoiKy;
                }
                //
                if (hopDong.ThongTinNhanVien != null) // Đối với người có trong hồ sơ
                {   //
                    hd.TenChucVuNguoiNLD = hopDong.ThongTinNhanVien.ChucVu != null ? hopDong.ThongTinNhanVien.ChucVu.TenChucVu : "Không";
                    hd.DanhXungNLDVietHoa = hopDong.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    hd.DanhXungNLDVietThuong = hopDong.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    hd.TenNguoiLaoDongVietHoa = hopDong.ThongTinNhanVien.HoTen.ToUpper();
                    hd.TenNguoiLaoDongVietThuong = hopDong.ThongTinNhanVien.HoTen;
                    hd.QuocTich = hopDong.ThongTinNhanVien.QuocTich.TenQuocGia;
                    hd.NgaySinh = hopDong.ThongTinNhanVien.NgaySinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    hd.NgaySinhDate = hopDong.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                    hd.NoiSinh = hopDong.ThongTinNhanVien.NoiSinh != null ? hopDong.ThongTinNhanVien.NoiSinh.TinhThanh != null ? hopDong.ThongTinNhanVien.NoiSinh.TinhThanh.TenTinhThanh : "" : "";
                    hd.QueQuan = hopDong.ThongTinNhanVien.QueQuan != null ? hopDong.ThongTinNhanVien.QueQuan.FullDiaChi : "";
                    hd.TrinhDoChuyenMon = hopDong.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? hopDong.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                    hd.ChuyenNganhDaoTao = hopDong.ThongTinNhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao != null ? hopDong.ThongTinNhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao.TenChuyenNganhDaoTao : "";
                    hd.NamTotNghiep = hopDong.ThongTinNhanVien.NhanVienTrinhDo.NamTotNghiep > 0 ? hopDong.ThongTinNhanVien.NhanVienTrinhDo.NamTotNghiep.ToString("d") : "";
                    hd.DienThoaiDiDong = hopDong.ThongTinNhanVien.DienThoaiDiDong;
                    hd.DiaChiThuongTru = hopDong.ThongTinNhanVien.DiaChiThuongTru != null ? hopDong.ThongTinNhanVien.DiaChiThuongTru.FullDiaChi : "";
                    hd.NoiOHienNay = hopDong.ThongTinNhanVien.NoiOHienNay != null ? hopDong.ThongTinNhanVien.NoiOHienNay.FullDiaChi : "";
                    hd.CMND = hopDong.ThongTinNhanVien.CMND;
                    hd.NgayCap = hopDong.ThongTinNhanVien.NgayCap.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    hd.NgayCapDate = hopDong.ThongTinNhanVien.NgayCap.ToString("dd/MM/yyyy");
                    hd.NoiCap = hopDong.ThongTinNhanVien.NoiCap != null ? "CA. " + hopDong.ThongTinNhanVien.NoiCap.TenTinhThanh : "";
                }
                else
                {
                    hd.DiaChiThuongTru = hopDong.DiaChiThuongTru;
                    hd.TenNguoiLaoDongVietHoa = hopDong.HoTen.ToUpper();
                    hd.TenNguoiLaoDongVietThuong = hopDong.HoTen;
                }
                //
                hd.DiaDiemLamViec = hopDong.BoPhan != null ? hopDong.BoPhan.TenBoPhan : "";
                hd.TuNgay = hopDong.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.DenNgay = hopDong.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.TuNgayDate = hopDong.TuNgay.ToString("dd/MM/yyyy");
                hd.DenNgayDate = hopDong.DenNgay.ToString("dd/MM/yyyy");
                hd.SoThang = Common.GetMonthNumber(hopDong.TuNgay,hopDong.DenNgay).ToString();
                //
                hd.LuongKhoan = hopDong.LuongKhoan.ToString("N0");
                //
                if (hopDong.LoaiHopDongKhoan == LoaiHopDongKhoanEnum.Gross)
                {
                    hopDongCongTacVienGrossList.Add(hd);
                }
                if (hopDong.LoaiHopDongKhoan == LoaiHopDongKhoanEnum.Net)
                {
                    hopDongCongTacVienNetList.Add(hd);
                }            
            }

            MailMergeTemplate merge = null;
            if (hopDongCongTacVienGrossList.Count > 0)
            {
                merge = Common.GetTemplate(obs, "HopDongCongTacVienGross.rtf", hdList[0].QuanLyHopDong.CongTy.Oid);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_HopDongKhoan>(hopDongCongTacVienGrossList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu hợp đồng cộng tác viên gross trong hệ thống.");
            }
            if (hopDongCongTacVienNetList.Count > 0)
            {
                merge = Common.GetTemplate(obs, "HopDongCongTacVienNet.rtf", hdList[0].QuanLyHopDong.CongTy.Oid);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_HopDongKhoan>(hopDongCongTacVienNetList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu hợp đồng cộng tác viên net trong hệ thống.");
            }
        }
        public static void ShowMailMerge_ABI(IObjectSpace obs, List<HopDongKhoan> hdList)
        {
            var hopDongList = new List<Non_HopDongKhoan>();
            //
            foreach (HopDongKhoan hopDong in hdList)
            {
                Non_HopDongKhoan hd = new Non_HopDongKhoan();
                //
                hd.Oid = hopDong.Oid.ToString();
                hd.DonViChuQuan = hopDong.QuanLyHopDong.CongTy != null ? hopDong.QuanLyHopDong.CongTy.DonViChuQuan : "";
                hd.TenCongTyVietHoa = hopDong.QuanLyHopDong.CongTy.TenBoPhan.ToUpper();
                hd.TenCongTyVietThuong = hopDong.QuanLyHopDong.CongTy.TenBoPhan;
                hd.DiaChi = hopDong.QuanLyHopDong.CongTy.DiaChi != null ? hopDong.QuanLyHopDong.CongTy.DiaChi.FullDiaChi : "";
                hd.SoDienThoai = hopDong.QuanLyHopDong.CongTy.DienThoai;
                hd.SoFax = hopDong.QuanLyHopDong.CongTy.Fax;
                hd.LoaiHopDong = hopDong.LoaiHopDong != null ? hopDong.LoaiHopDong.TenLoaiHopDong : "";
                hd.LoaiHopDongKhoan = hopDong.LoaiHopDongKhoan == LoaiHopDongKhoanEnum.Gross ? "Hợp đồng cộng tác viên gorss" : "Hợp đồng cộng tác viên net";
                hd.HinhThucHopDong = hopDong.HinhThucHopDong != null ? hopDong.HinhThucHopDong.TenHinhThucHopDong : "";
                hd.SoHopDong = hopDong.SoHopDong;
                hd.NgayKy = hopDong.NgayKy.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.NgayKyDate = hopDong.NgayKy.ToString("dd/MM/yyyy");
                hd.TenChucVuNguoiKy = hopDong.ChucVuNguoiKy != null ? hopDong.ChucVuNguoiKy.ChucVu.TenChucVu : "";
                hd.CanCu = hopDong.CanCu;
                if (hopDong.NguoiKy != null)
                {
                    hd.DanhXungNguoiKy = hopDong.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    hd.TenNguoiKyVietHoa = hopDong.NguoiKy.HoTen != "" ? hopDong.NguoiKy.HoTen.ToUpper() : hopDong.TenNguoiKy;
                    hd.TenNguoiKyVietThuong = hopDong.NguoiKy.HoTen != "" ? hopDong.NguoiKy.HoTen : hopDong.TenNguoiKy;
                }
                //
                if (hopDong.ThongTinNhanVien != null) // Đối với người có trong hồ sơ
                {   //
                    hd.TenChucVuNguoiNLD = hopDong.ThongTinNhanVien.ChucVu != null ? hopDong.ThongTinNhanVien.ChucVu.TenChucVu : "Không";
                    hd.ChucDanh = hopDong.ThongTinNhanVien.ChucDanh != null ? hopDong.ThongTinNhanVien.ChucDanh.TenChucDanh : "";
                    hd.DanhXungNLDVietHoa = hopDong.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    hd.DanhXungNLDVietThuong = hopDong.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    hd.TenNguoiLaoDongVietHoa = hopDong.ThongTinNhanVien.HoTen.ToUpper();
                    hd.TenNguoiLaoDongVietThuong = hopDong.ThongTinNhanVien.HoTen;
                    hd.DonVi = hopDong.ThongTinNhanVien.BoPhan != null ? hopDong.ThongTinNhanVien.BoPhan.TenBoPhan : "";
                    hd.GioiTinh = hopDong.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Nam" : "Nữ";
                    hd.QuocTich = hopDong.ThongTinNhanVien.QuocTich.TenQuocGia;
                    hd.NgaySinh = hopDong.ThongTinNhanVien.NgaySinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    hd.NgaySinhDate = hopDong.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                    hd.NoiSinh = hopDong.ThongTinNhanVien.NoiSinh != null ? hopDong.ThongTinNhanVien.NoiSinh.TinhThanh != null ? hopDong.ThongTinNhanVien.NoiSinh.TinhThanh.TenTinhThanh : "" : "";
                    hd.QueQuan = hopDong.ThongTinNhanVien.QueQuan != null ? hopDong.ThongTinNhanVien.QueQuan.FullDiaChi : "";
                    hd.TrinhDoChuyenMon = hopDong.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? hopDong.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                    hd.ChuyenNganhDaoTao = hopDong.ThongTinNhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao != null ? hopDong.ThongTinNhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao.TenChuyenNganhDaoTao : "";
                    hd.NamTotNghiep = hopDong.ThongTinNhanVien.NhanVienTrinhDo.NamTotNghiep > 0 ? hopDong.ThongTinNhanVien.NhanVienTrinhDo.NamTotNghiep.ToString("d") : "";
                    hd.DienThoaiDiDong = hopDong.ThongTinNhanVien.DienThoaiDiDong;
                    hd.DiaChiThuongTru = hopDong.ThongTinNhanVien.DiaChiThuongTru != null ? hopDong.ThongTinNhanVien.DiaChiThuongTru.FullDiaChi : "";
                    hd.NoiOHienNay = hopDong.ThongTinNhanVien.NoiOHienNay != null ? hopDong.ThongTinNhanVien.NoiOHienNay.FullDiaChi : "";
                    hd.CMND = hopDong.ThongTinNhanVien.CMND;
                    hd.NgayCap = hopDong.ThongTinNhanVien.NgayCap.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    hd.NgayCapDate = hopDong.ThongTinNhanVien.NgayCap.ToString("dd/MM/yyyy");
                    hd.NoiCap = hopDong.ThongTinNhanVien.NoiCap != null ? "CA. " + hopDong.ThongTinNhanVien.NoiCap.TenTinhThanh : "";
                }
                else
                {
                    hd.DiaChiThuongTru = hopDong.DiaChiThuongTru;
                    hd.TenNguoiLaoDongVietHoa = hopDong.HoTen.ToUpper();
                    hd.TenNguoiLaoDongVietThuong = hopDong.HoTen;
                }
                //
                hd.DiaDiemLamViec = hopDong.BoPhan != null ? hopDong.BoPhan.TenBoPhan : "";
                hd.TuNgay = hopDong.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.DenNgay = hopDong.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.TuNgayDate = hopDong.TuNgay.ToString("dd/MM/yyyy");
                hd.DenNgayDate = hopDong.DenNgay.ToString("dd/MM/yyyy");
                hd.SoThang = Common.GetMonthNumber(hopDong.TuNgay, hopDong.DenNgay).ToString();
                hd.SoNgay = Common.GetDayNumberAddHoliday(hopDong.TuNgay, hopDong.DenNgay).ToString();
                //
                hd.LuongKhoan = hopDong.LuongKhoan.ToString("N0");
                hd.PCChucVu = hopDong.PhuCapChucVu.ToString("N0");
                hd.PCDienThoai = hopDong.PhuCapDienThoai.ToString("N0");
                hd.PCHieuQuaCV = hopDong.PhuCapHieuQuaCongViec.ToString("N0");
                hd.PCHocVi = hopDong.PhuCapHocVi.ToString("N0");
                hd.PCKiemNhiem = hopDong.PhuCapKiemNhiem.ToString("N0");
                hd.PCTienAn = hopDong.PhuCapTienAn.ToString("N0");
                hd.PCTienBHXH = hopDong.TienBHXH.ToString("N0");
                hd.PCTienXang = hopDong.PhuCapTienXang.ToString("N0");
                hd.TongLuong = hopDong.TongLuong.ToString("N0");


                //
                hopDongList.Add(hd);
            }

            MailMergeTemplate merge = null;
            //
            if (hopDongList.Count > 0)
            {
                merge = Common.GetTemplate(obs, "HopDongKhoan.rtf", hdList[0].QuanLyHopDong.CongTy.Oid);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_HopDongKhoan>(hopDongList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu Hợp đồng khoán trong hệ thống.");
            }
        }
    }
}
