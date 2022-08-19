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
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Win.MailMerge.Prosess.QuyetDinh
{
    public static class Prosess_QuyetDinhKyLuat
    {
        public static void ShowMailMerge(IObjectSpace obs, List<QuyetDinhKyLuat> qdList)
        {
            var non_QuyetDinhHTList = new List<Non_QuyetDinhKyLuat>();
            var non_QuyetDinhHDQTList = new List<Non_QuyetDinhKyLuat>();
            Non_QuyetDinhKyLuat qd;
            foreach (QuyetDinhKyLuat quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhKyLuat();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.CongTy != null ? quyetDinh.CongTy.DonViChuQuan : string.Empty;
                qd.TenCongTyVietHoa = quyetDinh.CongTy != null ? quyetDinh.CongTy.TenBoPhan.ToUpper() : quyetDinh.TenCongTy;
                qd.TenCongTyVietThuong = quyetDinh.CongTy != null ? quyetDinh.CongTy.TenBoPhan : quyetDinh.TenCongTy;
                BoPhan bpNhanSu = obs.FindObject<BoPhan>(CriteriaOperator.Parse("CongTy.Oid = ? and TenBoPhan like ?", quyetDinh.CongTy.Oid, "%nhân sự%"));
                if (bpNhanSu != null)
                    qd.TenBoPhanNhanSu = bpNhanSu.TenBoPhan;
                else
                    qd.TenBoPhanNhanSu = "BP.Nhân sự";
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
                qd.SoBienBan = quyetDinh.SoBienBan;
                qd.NgayLapBienBan = quyetDinh.NgayLapBienBan != DateTime.MinValue ? quyetDinh.NgayLapBienBan.ToString("dd/MM/yyyy") : "";
                qd.HinhThucKyLuat = quyetDinh.HinhThucKyLuat != null ? quyetDinh.HinhThucKyLuat.TenHinhThucKyLuat : "";
                qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.DenNgay = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.TuNgayDate = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("dd/MM/yyyy") : "";
                qd.DenNgayDate = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("dd/MM/yyyy") : "";
                //
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

                if (quyetDinh.LoaiQuyetDinh == LoaiQuyetDinhEnum.HieuTruong || quyetDinh.LoaiQuyetDinh==LoaiQuyetDinhEnum.TongGiamDoc)
                {
                    non_QuyetDinhHTList.Add(qd);
                }
                if (quyetDinh.LoaiQuyetDinh == LoaiQuyetDinhEnum.HoiDongQuanTri)
                {
                    non_QuyetDinhHDQTList.Add(qd);
                }
            }
            //
            if (non_QuyetDinhHTList.Count > 0)
            {
                MailMergeTemplate merge = Common.GetTemplateWithValidDate(obs, "QuyetDinhKyLuat.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_QuyetDinhKyLuat>(non_QuyetDinhHTList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mẫu Quyết định trong hệ thống.");
            }
            //
            if (non_QuyetDinhHDQTList.Count > 0)
            {
                MailMergeTemplate merge = Common.GetTemplateWithValidDate(obs, "QuyetDinhKyLuatHDQT.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_QuyetDinhKyLuat>(non_QuyetDinhHDQTList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mẫu Quyết định trong hệ thống.");
            }
        }
    }
}
