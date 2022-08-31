using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.MailMerge.NhanSu.QuyetDinh;
using ERP.Module.Enum.NhanSu;
using ERP.Module.MailMerge;
using ERP.Module.Win.MailMerge.Prosess.ShowMaiMerge;
using ERP.Module.Extends;
using ERP.Module.Commons;

namespace ERP.Module.Win.MailMerge.Prosess.QuyetDinh
{
    public static class Prosess_QuyetDinhChamDutHopDong
    {
        public static void ShowMailMerge(IObjectSpace obs, List<QuyetDinhChamDutHopDong> qdList)
        {
            var non_QuyetDinhLDList = new List<Non_QuyetDinhChamDutHopDong>();
            var non_QuyetDinhTVList = new List<Non_QuyetDinhChamDutHopDong>();
            Non_QuyetDinhChamDutHopDong qd;
            foreach (QuyetDinhChamDutHopDong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhChamDutHopDong();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.CongTy != null ? quyetDinh.CongTy.DonViChuQuan : string.Empty;
                qd.TenCongTyVietHoa = quyetDinh.CongTy != null ? quyetDinh.CongTy.TenBoPhan.ToUpper() : "";
                qd.TenCongTyVietThuong = quyetDinh.CongTy != null ? quyetDinh.CongTy.TenBoPhan : "";
                if (quyetDinh.CongTy.DiaChi != null && quyetDinh.CongTy.DiaChi.TinhThanh != null)
                {
                    if (quyetDinh.CongTy.DiaChi.TinhThanh.TenTinhThanh.ToLower().Contains("thành phố"))
                        qd.TinhThanh = quyetDinh.CongTy.DiaChi.TinhThanh.ToString().Replace("Thành phố", "Tp.");
                    else
                        qd.TinhThanh = quyetDinh.CongTy.DiaChi.TinhThanh.ToString().Replace("Tỉnh", "");
                }
                else
                    qd.TinhThanh = "..........";
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLucDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                qd.CanCu = quyetDinh.CanCu;
                qd.NoiNhan = quyetDinh.NoiNhan;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.ChucVu.TenChucVu.ToUpper() : "";
                if (quyetDinh.NguoiKy != null)
                {
                    qd.TenNguoiKyVietHoa = quyetDinh.NguoiKy.HoTen != "" ? quyetDinh.NguoiKy.HoTen.ToUpper() : quyetDinh.TenNguoiKy.ToUpper();
                    qd.TenNguoiKyVietThuong = quyetDinh.NguoiKy.HoTen != "" ? quyetDinh.NguoiKy.HoTen : quyetDinh.TenNguoiKy;
                    qd.DanhXungNguoiKyVietHoa = quyetDinh.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungNguoiKyVietThuong = quyetDinh.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                }
                qd.HoTenVietHoa = quyetDinh.ThongTinNhanVien.HoTen.ToUpper();
                qd.HoTenVietThuong = quyetDinh.ThongTinNhanVien.HoTen;
                qd.DonVi = quyetDinh.BoPhan.TenBoPhan;
                qd.DanhXungVietHoa = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                qd.DanhXungVietThuong = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                qd.LoaiHopDong = quyetDinh.ThongTinNhanVien.LoaiHopDong != null ? quyetDinh.ThongTinNhanVien.LoaiHopDong.TenLoaiHopDong : "";
                qd.ChucVu = quyetDinh.ThongTinNhanVien.ChucVu != null ? quyetDinh.ThongTinNhanVien.ChucVu.TenChucVu : "";
                qd.ChucDanh = quyetDinh.ThongTinNhanVien.ChucDanh != null ? quyetDinh.ThongTinNhanVien.ChucDanh.TenChucDanh : "";
                //
                qd.TuNgay = quyetDinh.NgayHieuLuc != DateTime.MinValue ? quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.TuNgayDate = quyetDinh.NgayHieuLuc != DateTime.MinValue ? quyetDinh.NgayHieuLuc.ToString("dd/MM/yyyy") : "";
                qd.LyDo = quyetDinh.NoiDung;
                qd.TrinhDoChuyenMon = quyetDinh.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? quyetDinh.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                qd.NgaySinhDate = quyetDinh.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                qd.NgaySinh = quyetDinh.ThongTinNhanVien.NgaySinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.DiaChiThuongTru = quyetDinh.ThongTinNhanVien.DiaChiThuongTru != null ? quyetDinh.ThongTinNhanVien.DiaChiThuongTru.FullDiaChi : "";
                //
                qd.SoHopDong = quyetDinh.HopDong != null ? quyetDinh.HopDong.SoHopDong : string.Empty;
                qd.NgayHopDong = quyetDinh.HopDong != null ? quyetDinh.HopDong.NgayKy.ToString("dd/MM/yyyy") : string.Empty;
                qd.NgayKyThoaThuan = quyetDinh.NgayKyThoaThuan.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayKyThoaThuanDate = quyetDinh.NgayKyThoaThuan.ToString("dd/MM/yyyy");
                qd.SoDoanhNghiep = quyetDinh.CongTy.MaSoThue != null ? quyetDinh.CongTy.MaSoThue.ToString() : ".....";
                if (quyetDinh.CongTy.DiaChi != null && quyetDinh.CongTy.DiaChi.TinhThanh != null)
                    qd.SoKHDT = quyetDinh.CongTy.DiaChi.TinhThanh.TenTinhThanh.ToString();
                else
                    qd.SoKHDT = ".....";
                //
                if (quyetDinh.LoaiCongTy.MaQuanLy.ToString() == "01")
                    qd.DanhXungCongTyHoacTruong = "Công ty";
                else if (quyetDinh.LoaiCongTy.MaQuanLy.ToString() == "02")

                    qd.DanhXungCongTyHoacTruong = "Trường";
                else
                    qd.DanhXungCongTyHoacTruong = "";
                //                
                if (quyetDinh.LoaiQuyetDinh == LoaiQuyetDinhEnum.HoiDongQuanTri)
                {
                    if (quyetDinh.CongTy.LoaiTruong == Enum.TuyenSinh_PT.LoaiTruongEnum.DH || quyetDinh.CongTy.LoaiTruong == Enum.TuyenSinh_PT.LoaiTruongEnum.NA)
                        qd.BanTongGiamDocHoacBanGiamHieu = "HỘI ĐỒNG QUẢN TRỊ";
                    else
                        qd.BanTongGiamDocHoacBanGiamHieu = "HỘI ĐỒNG THÀNH VIÊN";
                }
                else if (quyetDinh.LoaiQuyetDinh == LoaiQuyetDinhEnum.HieuTruong)
                    qd.BanTongGiamDocHoacBanGiamHieu = "BAN GIÁM HIỆU";
                else
                    qd.BanTongGiamDocHoacBanGiamHieu = "BAN TỔNG GIÁM ĐỐC";
                //           
                
                if (quyetDinh.HopDong != null && quyetDinh.HopDong.LoaiHopDong != null)
                {
                    if (quyetDinh.HopDong.LoaiHopDong.TenLoaiHopDong.Contains("thử việc"))                         
                        non_QuyetDinhTVList.Add(qd);
                    else
                        non_QuyetDinhLDList.Add(qd);
                }
                
            }  //

            if (non_QuyetDinhLDList.Count > 0)
            {
                MailMergeTemplate merge = Common.GetTemplateWithValidDate(obs, "QuyetDinhChamDutHopDong.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
                    if (merge != null)
                        Prosess_Show.ShowEditor<Non_QuyetDinhChamDutHopDong>(non_QuyetDinhLDList, obs, merge);
                    else
                        DialogUtil.ShowError("Không tìm thấy mẫu Quyết định trong hệ thống.");
            }

            if (non_QuyetDinhTVList.Count > 0)
            {
                MailMergeTemplate merge = Common.GetTemplateWithValidDate(obs, "QuyetDinhThongBaoChamDutThuViec.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_QuyetDinhChamDutHopDong>(non_QuyetDinhTVList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mẫu Quyết định trong hệ thống.");
            }
        }
    }
}
