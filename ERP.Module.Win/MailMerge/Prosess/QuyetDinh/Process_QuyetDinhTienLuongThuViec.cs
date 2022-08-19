using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.MailMerge.NhanSu.QuyetDinh;
using ERP.Module.Win.MailMerge.Prosess.ShowMaiMerge;
using ERP.Module.Commons;
using ERP.Module.Extends;
using ERP.Module.MailMerge;
using ERP.Module.Enum.NhanSu;

namespace ERP.Module.Win.MailMerge.Prosess.QuyetDinh
{
    public static class Process_QuyetDinhTienLuongThuViec
    {
        public static void ShowMailMerge(IObjectSpace obs, List<QuyetDinhTienLuongThuViec> qdList)
        {
            var non_QuyetDinhLuongThuViecList = new List<Non_QuyetDinhTienLuongThuViec>();
            var non_QuyetDinhLuongThuViecHDQTList = new List<Non_QuyetDinhTienLuongThuViec>();
            Non_QuyetDinhTienLuongThuViec qd;
            foreach (QuyetDinhTienLuongThuViec quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhTienLuongThuViec();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.CongTy != null ? quyetDinh.CongTy.DonViChuQuan : string.Empty;
                qd.TenCongTyVietHoa = quyetDinh.CongTy != null ? quyetDinh.CongTy.TenBoPhan.ToUpper() : quyetDinh.TenCongTy;
                qd.TenCongTyVietThuong = quyetDinh.CongTy != null ? quyetDinh.CongTy.TenBoPhan : quyetDinh.TenCongTy;
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
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLucDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                qd.CanCu = quyetDinh.CanCu;
                qd.NoiNhan = quyetDinh.NoiNhan;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.ChucDanh.TenChucDanh.ToUpper() : "";
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
                qd.LuongCoBan = Math.Round((quyetDinh.LuongCoBanMoi * quyetDinh.MucHuongThuViec / 100),0).ToString("#,###");
                qd.LuongKinhDoanh = Math.Round((quyetDinh.LuongKinhDoanhMoi * quyetDinh.MucHuongThuViec / 100),0).ToString("#,###");
                qd.TongLuong = Math.Round(((quyetDinh.LuongCoBanMoi + quyetDinh.LuongKinhDoanhMoi) * quyetDinh.MucHuongThuViec / 100),0).ToString("#,###");
                qd.SoDoanhNghiep = quyetDinh.CongTy.MaSoThue != null ? quyetDinh.CongTy.MaSoThue.ToString() : ".....";
                if (quyetDinh.CongTy.DiaChi != null && quyetDinh.CongTy.DiaChi.TinhThanh != null)
                    qd.SoKHDT = quyetDinh.CongTy.DiaChi.TinhThanh.TenTinhThanh.ToString();
                else
                    qd.SoKHDT = ".....";

                if (quyetDinh.LoaiCongTy.MaQuanLy.ToString() == "01")
                {
                    if (quyetDinh.LoaiQuyetDinh == LoaiQuyetDinhEnum.HoiDongQuanTri)
                        qd.BanTongGiamDocHoacBanGiamHieu = "HỘI ĐỒNG QUẢN TRỊ";
                    else
                        qd.BanTongGiamDocHoacBanGiamHieu = "Ban Tổng giám đốc";
                    qd.DanhXungCongTyHoacTruong = "Công ty";
                }
                else if (quyetDinh.LoaiCongTy.MaQuanLy.ToString() == "02")
                {
                    if (quyetDinh.LoaiQuyetDinh == LoaiQuyetDinhEnum.HoiDongQuanTri)
                        qd.BanTongGiamDocHoacBanGiamHieu = "HỘI ĐỒNG THÀNH VIÊN";
                    else
                        qd.BanTongGiamDocHoacBanGiamHieu = "Ban giám hiệu";
                    qd.DanhXungCongTyHoacTruong = "Trường";
                }
                else
                    qd.BanTongGiamDocHoacBanGiamHieu = "";

                if (quyetDinh.LoaiQuyetDinh == LoaiQuyetDinhEnum.HieuTruong || quyetDinh.LoaiQuyetDinh == LoaiQuyetDinhEnum.TongGiamDoc)
                {
                    non_QuyetDinhLuongThuViecList.Add(qd);
                }
                if (quyetDinh.LoaiQuyetDinh == LoaiQuyetDinhEnum.HoiDongQuanTri)
                {
                    non_QuyetDinhLuongThuViecHDQTList.Add(qd);
                }
            }
            //
            if (non_QuyetDinhLuongThuViecList.Count > 0)
            {
                MailMergeTemplate merge = Common.GetTemplateWithValidDate(obs, "QuyetDinhTienLuongThuViec.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_QuyetDinhTienLuongThuViec>(non_QuyetDinhLuongThuViecList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mẫu Quyết định trong hệ thống.");
            }
            //
            if (non_QuyetDinhLuongThuViecHDQTList.Count > 0)
            {
                MailMergeTemplate merge = Common.GetTemplateWithValidDate(obs, "QuyetDinhTienLuongThuViecHDQT.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_QuyetDinhTienLuongThuViec>(non_QuyetDinhLuongThuViecHDQTList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mẫu Quyết định trong hệ thống.");
            }
        }
    }
}
