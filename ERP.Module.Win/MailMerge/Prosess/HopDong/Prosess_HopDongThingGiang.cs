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
    public static class Prosess_HopDongThingGiang
    {
        public static void ShowMailMerge(IObjectSpace obs, List<HopDongThinhGiang> hdList)
        {
            var hopDongThinhGiangList = new List<Non_HopDongThinhGiang>();
            //
            foreach (HopDongThinhGiang hopDong in hdList)
            {
                Non_HopDongThinhGiang hd = new Non_HopDongThinhGiang();
                //
                hd.Oid = hopDong.Oid.ToString();
                hd.DonViChuQuan = hopDong.QuanLyHopDongThinhGiang.CongTy != null ? hopDong.QuanLyHopDongThinhGiang.CongTy.DonViChuQuan : "";
                hd.TenCongTyVietHoa = hopDong.QuanLyHopDongThinhGiang.CongTy.TenBoPhan.ToUpper();
                hd.TenCongTyVietThuong = hopDong.QuanLyHopDongThinhGiang.CongTy.TenBoPhan;
                hd.DiaChi = hopDong.QuanLyHopDongThinhGiang.CongTy.DiaChi!= null ? hopDong.QuanLyHopDongThinhGiang.CongTy.DiaChi.FullDiaChi : "";
                hd.SoDienThoai = hopDong.QuanLyHopDongThinhGiang.CongTy.DienThoai;
                hd.SoFax = hopDong.QuanLyHopDongThinhGiang.CongTy.Fax;
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
                if (hopDong.GiangVienThinhGiang != null) // Đối với người có trong hồ sơ
                {   //
                    hd.DanhXungNLDVietHoa = hopDong.GiangVienThinhGiang.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    hd.DanhXungNLDVietThuong = hopDong.GiangVienThinhGiang.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    hd.TenNguoiLaoDongVietHoa = hopDong.GiangVienThinhGiang.HoTen.ToUpper();
                    hd.TenNguoiLaoDongVietThuong = hopDong.GiangVienThinhGiang.HoTen;
                    hd.QuocTich = hopDong.GiangVienThinhGiang.QuocTich.TenQuocGia;
                    hd.NgaySinh = hopDong.GiangVienThinhGiang.NgaySinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    hd.NgaySinhDate = hopDong.GiangVienThinhGiang.NgaySinh.ToString("dd/MM/yyyy");
                    hd.NoiSinh = hopDong.GiangVienThinhGiang.NoiSinh != null ? hopDong.GiangVienThinhGiang.NoiSinh.TinhThanh != null ? hopDong.GiangVienThinhGiang.NoiSinh.TinhThanh.TenTinhThanh : "" : "";
                    hd.QueQuan = hopDong.GiangVienThinhGiang.QueQuan != null ? hopDong.GiangVienThinhGiang.QueQuan.FullDiaChi : "";
                    hd.HocHam = hopDong.GiangVienThinhGiang.NhanVienTrinhDo.HocHam != null ? hopDong.GiangVienThinhGiang.NhanVienTrinhDo.HocHam.TenHocHam : "";
                    hd.TrinhDoChuyenMon = hopDong.GiangVienThinhGiang.NhanVienTrinhDo.TrinhDoChuyenMon != null ? hopDong.GiangVienThinhGiang.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                    hd.ChuyenNganhDaoTao = hopDong.GiangVienThinhGiang.NhanVienTrinhDo.ChuyenNganhDaoTao != null ? hopDong.GiangVienThinhGiang.NhanVienTrinhDo.ChuyenNganhDaoTao.TenChuyenNganhDaoTao : "";
                    hd.NamTotNghiep = hopDong.GiangVienThinhGiang.NhanVienTrinhDo.NamTotNghiep > 0 ? hopDong.GiangVienThinhGiang.NhanVienTrinhDo.NamTotNghiep.ToString("d") : "";
                    hd.DienThoaiDiDong = hopDong.GiangVienThinhGiang.DienThoaiDiDong;
                    hd.DiaChiThuongTru = hopDong.GiangVienThinhGiang.DiaChiThuongTru != null ? hopDong.GiangVienThinhGiang.DiaChiThuongTru.FullDiaChi : "";
                    hd.NoiOHienNay = hopDong.GiangVienThinhGiang.NoiOHienNay != null ? hopDong.GiangVienThinhGiang.NoiOHienNay.FullDiaChi : "";
                    hd.CMND = hopDong.GiangVienThinhGiang.CMND;
                    hd.Email = hopDong.GiangVienThinhGiang.Email;
                    hd.NgayCap = hopDong.GiangVienThinhGiang.NgayCap.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    hd.NgayCapDate = hopDong.GiangVienThinhGiang.NgayCap.ToString("dd/MM/yyyy");
                    hd.NoiCap = hopDong.GiangVienThinhGiang.NoiCap != null ? "CA. " + hopDong.GiangVienThinhGiang.NoiCap.TenTinhThanh : "";
                    hd.CoQuanCongTac = hopDong.GiangVienThinhGiang.TaiBoMon != null ? hopDong.GiangVienThinhGiang.TaiBoMon.TenBoPhan != null ? hopDong.GiangVienThinhGiang.TaiBoMon.TenBoPhan : "" : "";
                }

                //ThuHuong sửa 

                hd.DiaDiemLamViec = hopDong.NoiLamViec;
                hd.TenTaiKhoan = hopDong.TenTaiKhoan;
                hd.SoTaiKhoan = hopDong.SoTaiKhoan;
                hd.TenNganHang = hopDong.NganHang != null ? hopDong.NganHang.TenNganHang : "";
                hd.MaSoThue = hopDong.MaSoThue;
                hd.NamHoc = hopDong.QuanLyHopDongThinhGiang != null ? hopDong.QuanLyHopDongThinhGiang.NamHoc.TenNamHoc : "";
                hd.NamBatDau = hopDong.QuanLyHopDongThinhGiang != null ? hopDong.QuanLyHopDongThinhGiang.NamHoc.NgayBatDau.Year.ToString() : "";
                //
                hd.LopHocPhan = hopDong.LopHocPhan != null ? hopDong.LopHocPhan.TenLopHocPhan : "";
                hd.SoTinChi = hopDong.SoTinChi.ToString();
                hd.SoTiet = hopDong.SoTiet.ToString();
                //
                if (hopDong.LoaiHocPhan_LT == true)
                    hd.SoTietLT = hopDong.SoTiet.ToString();
                else if (hopDong.LoaiHocPhan_TH == false)
                    hd.SoTietTH = hopDong.SoTiet.ToString();
                //
                hd.SoBaiCham = hopDong.SoBaiCham.ToString() != null ? hopDong.SoBaiCham.ToString() : "";
                hd.HuongDanDoAnTotNghiep = hopDong.HuongDanDoAnTotNghiep.ToString() != null ? hopDong.SoBaiCham.ToString() : "";
                hd.MaLopSV = hopDong.MaLopSV.ToString() != null ? hopDong.SoBaiCham.ToString() : "";
                hd.TenLopSV = hopDong.TenLopSV.ToString() != null ? hopDong.SoBaiCham.ToString() : "";
                hd.SiSo = hopDong.SiSo.ToString() ;
                hd.ThuLaoGiangDay = hopDong.ThuLaoGiangDay.ToString();
                hd.ThucLanhThuLao = hopDong.ThucLanhThuLao.ToString();
                hd.SoLuotDiLai = hopDong.SoLuotDiLai.ToString();
                hd.DonGiaKhoangCach = hopDong.DonGiaKhoangCach.ToString();
                hd.ChiPhiTienXe = hopDong.ChiPhiTienXe.ToString();

                hd.ChiPhiTienAn = hopDong.ChiPhiTienAn.ToString();
                //hd.ViTriHienTai = ""; //Hiện tại chưa dùng
                hd.NgoaiDaLat = hopDong.NgoaiDaLat.ToString();
                hd.TrongDaLat = hopDong.TrongDaLat.ToString();
                hd.LoaiHocPhan_LT = hopDong.LoaiHocPhan_LT.ToString();
                hd.LoaiHocPhan_TH = hopDong.LoaiHocPhan_TH.ToString();
                
                //

                hopDongThinhGiangList.Add(hd);          
            }

            MailMergeTemplate merge = null;
            if (hopDongThinhGiangList.Count > 0)
            {
                merge = Common.GetTemplate(obs, "HopDongThinhGiang.rtf", hdList[0].Oid);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_HopDongThinhGiang>(hopDongThinhGiangList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu hợp đồng thỉnh giảng trong hệ thống.");
            }
        }
    }
}
