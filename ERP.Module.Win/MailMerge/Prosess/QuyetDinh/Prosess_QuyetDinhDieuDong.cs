using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.MailMerge.NhanSu.QuyetDinh;
using ERP.Module.Enum.NhanSu;
using ERP.Module.MailMerge;
using ERP.Module.Commons;
using ERP.Module.Win.MailMerge.Prosess.ShowMaiMerge;
using ERP.Module.Extends;
using DevExpress.Xpo;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Win.MailMerge.Prosess.QuyetDinh
{
    public static class Prosess_QuyetDinhDieuDong
    {
        public static void ShowMailMerge(IObjectSpace obs, List<QuyetDinhDieuDong> qdList)
        {
            var non_QuyetDinhHTList = new List<Non_QuyetDinhDieuDong>();
            var non_QuyetDinhHDQTList = new List<Non_QuyetDinhDieuDong>();
            Non_QuyetDinhDieuDong qd;
            foreach (QuyetDinhDieuDong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhDieuDong();
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
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLucDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                qd.CanCu = quyetDinh.CanCu;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.NoiNhan = quyetDinh.NoiNhan;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.ChucDanh.TenChucDanh.ToUpper() : "";
                qd.ChucVuNguoiKyVietThuong = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.ChucDanh.TenChucDanh: "";
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
                //
                qd.ChucDanh = quyetDinh.ChucDanhCu != null ? quyetDinh.ChucDanhCu.TenChucDanh : ""; 
                //
                qd.ChucDanhMoi = quyetDinh.ChucDanhMoi != null ? quyetDinh.ChucDanhMoi.TenChucDanh : "";
                qd.BoPhanMoi = quyetDinh.BoPhanMoi != null ? quyetDinh.BoPhanMoi.TenBoPhan : "";
                qd.TenTruongHoacCongTyMoi = quyetDinh.BoPhanMoi.CongTy.TenBoPhan!= null?quyetDinh.BoPhanMoi.CongTy.TenBoPhan :"";
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

                non_QuyetDinhHTList.Add(qd);

                //
                //if (quyetDinh.LoaiQuyetDinh == LoaiQuyetDinhEnum.HieuTruong || quyetDinh.LoaiQuyetDinh == LoaiQuyetDinhEnum.TongGiamDoc)
                //{
                //    non_QuyetDinhHTList.Add(qd);
                //}
                //if (quyetDinh.LoaiQuyetDinh == LoaiQuyetDinhEnum.HoiDongQuanTri)
                //{
                //    non_QuyetDinhHDQTList.Add(qd);
                //}
            }
            //
            if (non_QuyetDinhHTList.Count > 0)
            {
                MailMergeTemplate merge = Common.GetTemplateWithValidDate(obs, "QuyetDinhDieuDongHT.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_QuyetDinhDieuDong>(non_QuyetDinhHTList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mẫu Quyết định trong hệ thống.");
            }
            //
            //if (non_QuyetDinhHDQTList.Count > 0)
            //{
            //    MailMergeTemplate merge = Common.GetTemplate(obs, "QuyetDinhDieuDongHDQT.rtf", qdList[0].CongTy.Oid);
            //    if (merge != null)
            //        Prosess_Show.ShowEditor<Non_QuyetDinhDieuDong>(non_QuyetDinhHDQTList, obs, merge);
            //    else
            //        DialogUtil.ShowError("Không tìm thấy mẫu Quyết định trong hệ thống.");
            //}

        }
    }
}
