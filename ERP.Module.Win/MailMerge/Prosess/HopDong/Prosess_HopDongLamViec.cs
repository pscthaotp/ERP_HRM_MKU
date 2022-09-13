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

namespace ERP.Module.Win.MailMerge.Prosess.HopDong
{
    public static class Prosess_HopDongLamViec
    {
        public static void ShowMailMerge_Yersin(IObjectSpace obs, List<HopDongLamViec> hdList)
        {
            var hopDongThuViecGVList = new List<Non_HopDongLamViec>();
            var hopDongThuViecHCList = new List<Non_HopDongLamViec>();
            //
            var hopDongCoThoiHanGV_KBHList = new List<Non_HopDongLamViec>();
            var hopDongCoThoiHanGV_CBHList = new List<Non_HopDongLamViec>();
            var hopDongCoThoiHanHC_KBHList = new List<Non_HopDongLamViec>();
            var hopDongCoThoiHanHC_CBHList = new List<Non_HopDongLamViec>();
            //
            var hopDongKhongThoiHanGV_KBHList = new List<Non_HopDongLamViec>();
            var hopDongKhongThoiHanGV_CBHList = new List<Non_HopDongLamViec>();
            var hopDongKhongThoiHanHC_KBHList = new List<Non_HopDongLamViec>();
            var hopDongKhongThoiHanHC_CBHList = new List<Non_HopDongLamViec>();
            //
            foreach (HopDongLamViec hopDong in hdList)
            {
                Non_HopDongLamViec hd = new Non_HopDongLamViec();
                //
                hd.Oid = hopDong.Oid.ToString();
                hd.DonViChuQuan = hopDong.QuanLyHopDong.CongTy != null ? hopDong.QuanLyHopDong.CongTy.DonViChuQuan : "";
                hd.TenCongTyVietHoa = hopDong.QuanLyHopDong.CongTy.TenBoPhan.ToUpper();
                hd.TenCongTyVietThuong = hopDong.QuanLyHopDong.CongTy.TenBoPhan;
                hd.DiaChi = hopDong.QuanLyHopDong.CongTy.DiaChi != null ? hopDong.QuanLyHopDong.CongTy.DiaChi.FullDiaChi : "";
                hd.SoDienThoai = hopDong.QuanLyHopDong.CongTy.DienThoai;
                hd.SoFax = hopDong.QuanLyHopDong.CongTy.Fax;
                hd.LoaiHopDong = hopDong.LoaiHopDong != null ? hopDong.LoaiHopDong.TenLoaiHopDong : "";
                hd.HinhThucHopDong = hopDong.HinhThucHopDong != null ? hopDong.HinhThucHopDong.TenHinhThucHopDong : "";
                hd.SoHopDong = hopDong.SoHopDong;
                hd.CanCu = hopDong.CanCu;
                if (hopDong.NgayKy.Month < 3 || hopDong.NgayKy.Month > 9) 
                    hd.NgayKy = hopDong.NgayKy.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                else
                    hd.NgayKy = hopDong.NgayKy.ToString("'ngày' dd 'tháng' M 'năm' yyyy");
                hd.NgayKyDate = hopDong.NgayKy.ToString("dd/MM/yyyy");
                
                //Người ký
                if (hopDong.NguoiKy != null)
                {
                    hd.DanhXungNguoiKy = hopDong.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    hd.TenNguoiKyVietHoa = hopDong.NguoiKy != null ? hopDong.NguoiKy.HoTen.ToUpper() : hopDong.TenNguoiKy.ToUpper();
                    hd.TenNguoiKyVietThuong = hopDong.NguoiKy != null ? hopDong.NguoiKy.HoTen : hopDong.TenNguoiKy;
                    hd.QuocTichNguoiKy = hopDong.NguoiKy.QuocTich != null ? hopDong.NguoiKy.QuocTich.TenQuocGia : "";
                    hd.DiaChiNguoiKy = hopDong.NguoiKy.DiaChiThuongTru != null ? hopDong.NguoiKy.DiaChiThuongTru.FullDiaChi : "";
                    hd.NgaySinhNguoiKy = hopDong.NguoiKy.NgaySinh.ToString("dd/MM/yyyy");
                    hd.CMNDNguoiKy = hopDong.NguoiKy.CMND;
                    hd.NgayCapNguoiKy = hopDong.NguoiKy.NgayCap.ToString("dd/MM/yyyy");
                    hd.NoiCapNguoiKy = hopDong.NguoiKy.NoiCap != null ? "CA. " + hopDong.NguoiKy.NoiCap.TenTinhThanh : "";
                    hd.TenChucVuNguoiKy = hopDong.ChucVuNguoiKy != null ? hopDong.ChucVuNguoiKy.ChucVu.TenChucVu : "";
                }
                //Người lao động
                if (hopDong.ThongTinNhanVien != null)
                {
                    hd.TenChucVuNguoiNLD = hopDong.ThongTinNhanVien.ChucVu != null ? hopDong.ThongTinNhanVien.ChucVu.TenChucVu : "Không";
                    hd.DanhXungNLDVietHoa = hopDong.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    hd.DanhXungNLDVietThuong = hopDong.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    hd.TenNguoiLaoDongVietHoa = hopDong.ThongTinNhanVien.HoTen.ToUpper();
                    hd.TenNguoiLaoDongVietThuong = hopDong.ThongTinNhanVien.HoTen;
                    hd.QuocTich = hopDong.ThongTinNhanVien.QuocTich != null ? hopDong.ThongTinNhanVien.QuocTich.TenQuocGia : "";
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
                    hd.ChucDanh = hopDong.ThongTinNhanVien.ChucDanh != null ? hopDong.ThongTinNhanVien.ChucDanh.TenChucDanh : "";
                }
                hd.DiaDiemLamViec = hopDong.BoPhan != null ? hopDong.BoPhan.TenBoPhan : "";
                hd.TuNgay = hopDong.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.DenNgay = hopDong.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.TuNgayDate = hopDong.TuNgay.ToString("dd/MM/yyyy");
                hd.DenNgayDate = hopDong.DenNgay.ToString("dd/MM/yyyy");
                hd.SoNgay = Common.GetDayNumberAddHoliday(hopDong.TuNgay, hopDong.DenNgay).ToString();
                hd.SoThang= Common.GetMonthNumber(hopDong.TuNgay, hopDong.DenNgay).ToString();
                //
                hd.MaNgach = hopDong.NgachLuong != null ? hopDong.NgachLuong.MaQuanLy : "";
                hd.NgachLuong = hopDong.NgachLuong != null ? hopDong.NgachLuong.TenNgachLuong : "";
                hd.BacLuong = hopDong.BacLuong != null ? hopDong.BacLuong.TenBacLuong : "";
                hd.LuongCoBan = (hopDong.LuongCoBan*hopDong.PhanTramTinhLuong/100).ToString("N0");
                //

                if (hopDong.LoaiHopDong != null && hopDong.LoaiHopDong.TenLoaiHopDong.Equals("Hợp đồng thử việc"))
                {
                    if (hopDong.LoaiKhoiHopDong == LoaiKhoiHopDongEnum.KhoiGiangVien)
                        hopDongThuViecGVList.Add(hd);
                    if (hopDong.LoaiKhoiHopDong == LoaiKhoiHopDongEnum.KhoiHanhChinh)
                        hopDongThuViecHCList.Add(hd);
                }

                if (hopDong.LoaiHopDong != null && hopDong.LoaiHopDong.TenLoaiHopDong.Equals("Hợp đồng xác định thời hạn"))
                {
                    if (hopDong.LoaiKhoiHopDong == LoaiKhoiHopDongEnum.KhoiHanhChinh)
                    {
                        if (hopDong.KhongDongBHXH)
                        {
                            hopDongCoThoiHanHC_KBHList.Add(hd);
                        }
                        if (!hopDong.KhongDongBHXH)
                        {
                            hopDongCoThoiHanHC_CBHList.Add(hd);
                        }
                    }
                    if (hopDong.LoaiKhoiHopDong == LoaiKhoiHopDongEnum.KhoiGiangVien)
                    {
                        if (hopDong.KhongDongBHXH)
                        {
                            hopDongCoThoiHanGV_KBHList.Add(hd);
                        }
                        if (!hopDong.KhongDongBHXH)
                        {
                            hopDongCoThoiHanGV_CBHList.Add(hd);
                        }
                    }
                }
                if (hopDong.LoaiHopDong != null && hopDong.LoaiHopDong.TenLoaiHopDong.Equals("Hợp đồng không xác định thời hạn"))
                {

                    if (hopDong.LoaiKhoiHopDong == LoaiKhoiHopDongEnum.KhoiHanhChinh)
                    {
                        if (hopDong.KhongDongBHXH)
                        {
                            hopDongKhongThoiHanHC_KBHList.Add(hd);
                        }
                        if (!hopDong.KhongDongBHXH)
                        {
                            hopDongKhongThoiHanHC_CBHList.Add(hd);
                        }
                    }
                    if (hopDong.LoaiKhoiHopDong == LoaiKhoiHopDongEnum.KhoiGiangVien)
                    {
                        if (hopDong.KhongDongBHXH)
                        {
                            hopDongKhongThoiHanGV_KBHList.Add(hd);
                        }
                        if (!hopDong.KhongDongBHXH)
                        {
                            hopDongKhongThoiHanGV_CBHList.Add(hd);
                        }
                    }
                }
            }

            MailMergeTemplate merge = null;
            //1. Thử việc
            if (hopDongThuViecGVList.Count > 0)
            {
                merge = Common.GetTemplateWithValidDate(obs, "HopDongThuViecKhoiGiangVien.rtf", hdList[0].QuanLyHopDong.CongTy.Oid, hdList[0].TuNgay);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_HopDongLamViec>(hopDongThuViecGVList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu hợp đồng thử việc giảng viên trong hệ thống.");
            }
            if (hopDongThuViecHCList.Count > 0)
            {
                merge = Common.GetTemplateWithValidDate(obs, "HopDongThuViecKhoiHanhChinh.rtf", hdList[0].QuanLyHopDong.CongTy.Oid, hdList[0].TuNgay);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_HopDongLamViec>(hopDongThuViecHCList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu hợp đồng thử việc hành chính trong hệ thống.");
            }
            //2. Có thời hạn
            if (hopDongCoThoiHanGV_CBHList.Count > 0)
            {
                merge = Common.GetTemplateWithValidDate(obs, "HopDongCoThoiHanKhoiGiangVienCoBaoHiem.rtf", hdList[0].QuanLyHopDong.CongTy.Oid, hdList[0].TuNgay);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_HopDongLamViec>(hopDongCoThoiHanGV_CBHList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu hợp đồng có thời hạn giảng viên có bảo hiểm trong hệ thống.");
            }
            if (hopDongCoThoiHanGV_KBHList.Count > 0)
            {
                merge = Common.GetTemplateWithValidDate(obs, "HopDongCoThoiHanKhoiGiangVienKhongBaoHiem.rtf", hdList[0].QuanLyHopDong.CongTy.Oid, hdList[0].TuNgay);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_HopDongLamViec>(hopDongCoThoiHanGV_KBHList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu hợp đồng có thời hạn giảng viên không bảo hiểm trong hệ thống.");
            }
            if (hopDongCoThoiHanHC_CBHList.Count > 0)
            {
                merge = Common.GetTemplateWithValidDate(obs, "HopDongCoThoiHanKhoiHanhChinhCoBaoHiem.rtf", hdList[0].QuanLyHopDong.CongTy.Oid, hdList[0].TuNgay);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_HopDongLamViec>(hopDongCoThoiHanHC_CBHList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu hợp đồng có thời hạn hành chính có bảo hiểm trong hệ thống.");
            }
            if (hopDongCoThoiHanHC_KBHList.Count > 0)
            {
                merge = Common.GetTemplateWithValidDate(obs, "HopDongCoThoiHanKhoiHanhChinhKhongBaoHiem.rtf", hdList[0].QuanLyHopDong.CongTy.Oid, hdList[0].TuNgay);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_HopDongLamViec>(hopDongCoThoiHanHC_KBHList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu hợp đồng có thời hạn hành chính không bảo hiểm trong hệ thống.");
            }
            //3. Không thời hạn
            if (hopDongKhongThoiHanGV_CBHList.Count > 0)
            {
                merge = Common.GetTemplateWithValidDate(obs, "HopDongKhongThoiHanKhoiGiangVienCoBaoHiem.rtf", hdList[0].QuanLyHopDong.CongTy.Oid, hdList[0].TuNgay);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_HopDongLamViec>(hopDongKhongThoiHanGV_CBHList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu hợp đồng không thời hạn giảng viên có bảo hiểm trong hệ thống.");
            }
            if (hopDongKhongThoiHanGV_KBHList.Count > 0)
            {
                merge = Common.GetTemplateWithValidDate(obs, "HopDongKhongThoiHanKhoiGiangVienKhongBaoHiem.rtf", hdList[0].QuanLyHopDong.CongTy.Oid, hdList[0].TuNgay);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_HopDongLamViec>(hopDongKhongThoiHanGV_KBHList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu hợp đồng không thời hạn giảng viên không bảo hiểm trong hệ thống.");
            }
            if (hopDongKhongThoiHanHC_CBHList.Count > 0)
            {
                merge = Common.GetTemplateWithValidDate(obs, "HopDongKhongThoiHanKhoiHanhChinhCoBaoHiem.rtf", hdList[0].QuanLyHopDong.CongTy.Oid, hdList[0].TuNgay);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_HopDongLamViec>(hopDongKhongThoiHanHC_CBHList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu hợp đồng không thời hạn hành chính có bảo hiểm trong hệ thống.");
            }
            if (hopDongKhongThoiHanHC_KBHList.Count > 0)
            {
                merge = Common.GetTemplateWithValidDate(obs, "HopDongKhongThoiHanKhoiHanhChinhKhongBaoHiem.rtf", hdList[0].QuanLyHopDong.CongTy.Oid, hdList[0].TuNgay);
                //
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_HopDongLamViec>(hopDongKhongThoiHanHC_KBHList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu hợp đồng không thời hạn hành chính không bảo hiểm trong hệ thống.");
            }
        }
        public static void ShowMailMerge(IObjectSpace obs, List<HopDongLamViec> hdList)
        {
            var hopDongThuViecList = new List<Non_HopDongLamViec>();
            //
            var hopDongLaoDongCTHList = new List<Non_HopDongLamViec>();
            var hopDongLaoDongKTHList = new List<Non_HopDongLamViec>();
            var hopDongLaoDongKhongBHXHList = new List<Non_HopDongLamViec>();
            var hopDongPhuLucHopDongList = new List<Non_HopDongLamViec>();
            //
            foreach (HopDongLamViec hopDong in hdList)
            {
                Non_HopDongLamViec hd = new Non_HopDongLamViec();
                //
                hd.Oid = hopDong.Oid.ToString();
                hd.DonViChuQuan = hopDong.QuanLyHopDong.CongTy != null ? hopDong.QuanLyHopDong.CongTy.DonViChuQuan : "";
                hd.TenCongTyVietHoa = hopDong.QuanLyHopDong.CongTy.TenBoPhan.ToUpper();
                hd.TenCongTyVietThuong = hopDong.QuanLyHopDong.CongTy.TenBoPhan;
                hd.DiaChi = hopDong.QuanLyHopDong.CongTy.DiaChi != null ? hopDong.QuanLyHopDong.CongTy.DiaChi.FullDiaChi : "";
                hd.SoDienThoai = hopDong.QuanLyHopDong.CongTy.DienThoai;
                hd.SoFax = hopDong.QuanLyHopDong.CongTy.Fax;
                hd.LoaiHopDong = hopDong.LoaiHopDong != null ? hopDong.LoaiHopDong.TenLoaiHopDong : "";
                hd.HinhThucHopDong = hopDong.HinhThucHopDong != null ? hopDong.HinhThucHopDong.TenHinhThucHopDong : "";
                hd.SoHopDong = hopDong.SoHopDong;
                hd.CanCu = hopDong.CanCu;
                hd.NgayKy = hopDong.NgayKy.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.NgayKyDate = hopDong.NgayKy.ToString("dd/MM/yyyy");
                if (hopDong.LoaiCongTy.MaQuanLy.ToString() == "01")
                {
                    //hd.BanTongGiamDocHoacBanGiamHieu = "Ban Tổng giám đốc";
                    hd.DanhXungCongTyHoacTruong = "Công ty";
                }
                else if (hopDong.LoaiCongTy.MaQuanLy.ToString() == "02")
                {
                    //hd.BanTongGiamDocHoacBanGiamHieu = "Ban giám hiệu";
                    hd.DanhXungCongTyHoacTruong = "Trường";
                }

                //Người ký
                if (hopDong.NguoiKy != null)
                {
                    hd.DanhXungNguoiKy = hopDong.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    hd.TenNguoiKyVietHoa = hopDong.NguoiKy != null ? hopDong.NguoiKy.HoTen.ToUpper() : hopDong.TenNguoiKy.ToUpper();
                    hd.TenNguoiKyVietThuong = hopDong.NguoiKy != null ? hopDong.NguoiKy.HoTen : hopDong.TenNguoiKy;
                    hd.QuocTichNguoiKy = hopDong.NguoiKy.QuocTich != null ? hopDong.NguoiKy.QuocTich.TenQuocGia : "";
                    hd.DiaChiNguoiKy = hopDong.NguoiKy.DiaChiThuongTru != null ? hopDong.NguoiKy.DiaChiThuongTru.FullDiaChi : "";
                    hd.NgaySinhNguoiKy = hopDong.NguoiKy.NgaySinh.ToString("dd/MM/yyyy");
                    hd.CMNDNguoiKy = hopDong.NguoiKy.CMND;
                    hd.NgayCapNguoiKy = hopDong.NguoiKy.NgayCap.ToString("dd/MM/yyyy");
                    hd.NoiCapNguoiKy = hopDong.NguoiKy.NoiCap != null ? "CA. " + hopDong.NguoiKy.NoiCap.TenTinhThanh : "";
                    hd.TenChucVuNguoiKy = hopDong.ChucVuNguoiKy != null ? hopDong.ChucVuNguoiKy.ChucVu.TenChucVu : "";
                    hd.SoDienThoaiNK = hopDong.NguoiKy.DienThoaiDiDong != null ? hopDong.NguoiKy.DienThoaiDiDong.ToString() : "" ;
                }
                //Người lao động
                if (hopDong.ThongTinNhanVien != null)
                {
                    hd.TenChucVuNguoiNLD = hopDong.ThongTinNhanVien.ChucVu != null ? hopDong.ThongTinNhanVien.ChucVu.TenChucVu : "Không";
                    hd.DanhXungNLDVietHoa = hopDong.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    hd.DanhXungNLDVietThuong = hopDong.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    hd.TenNguoiLaoDongVietHoa = hopDong.ThongTinNhanVien.HoTen.ToUpper();
                    hd.TenNguoiLaoDongVietThuong = hopDong.ThongTinNhanVien.HoTen;
                    hd.DonVi = hopDong.ThongTinNhanVien.BoPhan != null ? hopDong.ThongTinNhanVien.BoPhan.TenBoPhan : "";
                    hd.QuocTich = hopDong.ThongTinNhanVien.QuocTich != null ? hopDong.ThongTinNhanVien.QuocTich.TenQuocGia : "";
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
                    hd.ChucDanh = hopDong.ThongTinNhanVien.ChucDanh != null ? hopDong.ThongTinNhanVien.ChucDanh.TenChucDanh : "";
                    hd.GioTinhNLD = hopDong.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Nam" : "Nữ";
                }
                hd.DiaDiemLamViec = hopDong.BoPhan != null ? hopDong.BoPhan.TenBoPhan : "";
                hd.TuNgay = hopDong.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.DenNgay = hopDong.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.TuNgayDate = hopDong.TuNgay.ToString("dd/MM/yyyy");
                hd.DenNgayDate = hopDong.DenNgay.ToString("dd/MM/yyyy");
                hd.SoNgay = Common.GetDayNumberAddHoliday(hopDong.TuNgay, hopDong.DenNgay).ToString();
                hd.SoThang = Common.GetMonthNumber(hopDong.TuNgay, hopDong.DenNgay).ToString();
                //
                hd.MaNgach = hopDong.NgachLuong != null ? hopDong.NgachLuong.MaQuanLy : "";
                hd.NgachLuong = hopDong.NgachLuong != null ? hopDong.NgachLuong.TenNgachLuong : "";
                hd.BacLuong = hopDong.BacLuong != null ? hopDong.BacLuong.TenBacLuong : "";
                hd.LuongCoBan = (hopDong.LuongCoBan * hopDong.PhanTramTinhLuong/100).ToString("N0");
                hd.LuongCoBanThuan = hopDong.LuongCoBan.ToString("N0");
                hd.LuongBoSung = hopDong.LuongKinhDoanh.ToString("N0");
                hd.LuongGop = hopDong.LuongGop.ToString("N0");
                hd.PhuCapTienAn = hopDong.PhuCapTienAn.ToString("N0");
                hd.PhuCapTienXang = hopDong.PhuCapTienXang.ToString("N0");
                hd.PhuCapDienThoai = hopDong.PhuCapDienThoai.ToString("N0");
                hd.PhuCapChucVu = hopDong.PhuCapChucVu.ToString("N0");
                hd.TongLuong = hopDong.TongLuong.ToString("N0");

                //

                if (hopDong.LoaiHopDong != null && hopDong.LoaiHopDong.MaQuanLy.Equals("HĐTV"))
                {
                    //
                    hopDongThuViecList.Add(hd);
                }
                else if (hopDong.LoaiHopDong != null && hopDong.LoaiHopDong.MaQuanLy.Equals("HĐLĐCTH"))
                {
                    //
                    hopDongLaoDongCTHList.Add(hd);
                }
                else if (hopDong.LoaiHopDong != null && hopDong.LoaiHopDong.MaQuanLy.Equals("HĐLĐKTGBH"))
                {
                    //
                    hopDongLaoDongKhongBHXHList.Add(hd);
                }
                else if (hopDong.LoaiHopDong != null && hopDong.LoaiHopDong.MaQuanLy.Equals("PLHĐ"))
                {
                    hd.DieuThayDoi = hopDong.DieuThayDoi != null ? hopDong.DieuThayDoi.ToString() : ".....";
                    hd.KhoanThayDoi = hopDong.KhoanThayDoi != null ? hopDong.KhoanThayDoi.ToString() : ".....";
                    hd.DieuKhoanThayDoi = hopDong.DieuKhoanThayDoi != null ? hopDong.DieuKhoanThayDoi.ToString() : ".....";
                    hd.NoiDungDieuKhanThayDoi = hopDong.NoiDungDieuKhoanThayDoi != null ? hopDong.NoiDungDieuKhoanThayDoi.ToString() : "";
                    hd.SoHopDongLaoDong = hopDong.HopDongLaoDong.SoHopDong != null ? hopDong.HopDongLaoDong.SoHopDong.ToString() : "        ";
                    hd.NgayKyHopDongLaoDongNV = hopDong.HopDongLaoDong.NgayKy != null ? hopDong.HopDongLaoDong.NgayKy.ToString("dd/MM/yyyy") : "......"; 
                    hopDongPhuLucHopDongList.Add(hd);
                }
                else
                {
                    hopDongLaoDongKTHList.Add(hd);
                }
            }

            MailMergeTemplate merge = null;

            //1. Thử việc
            if (hopDongThuViecList.Count > 0)
            {
                merge = Common.GetTemplateWithValidDate(obs, "HopDongThuViec.rtf", hdList[0].QuanLyHopDong.CongTy.Oid, hdList[0].NgayKy);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_HopDongLamViec>(hopDongThuViecList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu hợp đồng thử việc trong hệ thống.");
            }

            //2. Có thời hạn
            if (hopDongLaoDongCTHList.Count > 0)
            {
                merge = Common.GetTemplateWithValidDate(obs, "HopDongLaoDongCoThoiHan.rtf", hdList[0].QuanLyHopDong.CongTy.Oid, hdList[0].NgayKy);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_HopDongLamViec>(hopDongLaoDongCTHList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu hợp đồng lao động có thời hạn trong hệ thống.");
            }

            //3. Không Có thời hạn
            if (hopDongLaoDongKTHList.Count > 0)
            {
                merge = Common.GetTemplateWithValidDate(obs, "HopDongLaoDongKhongThoiHan.rtf", hdList[0].QuanLyHopDong.CongTy.Oid, hdList[0].NgayKy);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_HopDongLamViec>(hopDongLaoDongKTHList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu hợp đồng lao động không thời hạn trong hệ thống.");
            }
            //4. Phụ lục hợp đồng
            if (hopDongPhuLucHopDongList.Count > 0)
            {
                merge = Common.GetTemplateWithValidDate(obs, "PhuLucHopDong.rtf", hdList[0].QuanLyHopDong.CongTy.Oid, hdList[0].NgayKy);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_HopDongLamViec>(hopDongPhuLucHopDongList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu phụ lục hợp đồng lao động trong hệ thống.");
            }

            //5. Không tham gia bảo hiểm xã hội
            if (hopDongLaoDongKhongBHXHList.Count > 0)
            {
                merge = Common.GetTemplateWithValidDate(obs, "HopDongLaoDongKhongThamGiaBHXH.rtf", hdList[0].QuanLyHopDong.CongTy.Oid, hdList[0].NgayKy);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_HopDongLamViec>(hopDongLaoDongCTHList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu hợp đồng lao động không tham gia BHXH trong hệ thống.");
            }
        }
    }
}
