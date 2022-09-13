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
    public static class Prosess_QuyetDinhThoiChuc
    {
        public static void ShowMailMerge(IObjectSpace obs, List<QuyetDinhThoiChuc> qdList)
        {
            var non_QuyetDinhHTList = new List<Non_QuyetDinhThoiChuc>();
            var non_QuyetDinhHDQTList = new List<Non_QuyetDinhThoiChuc>();
            Non_QuyetDinhThoiChuc qd;
            foreach (QuyetDinhThoiChuc quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhThoiChuc();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.CongTy != null ? quyetDinh.CongTy.DonViChuQuan : string.Empty;
                qd.TenCongTyVietHoa = quyetDinh.CongTy != null ? quyetDinh.CongTy.TenBoPhan.ToUpper() : "";
                qd.TenCongTyVietThuong = quyetDinh.CongTy != null ? quyetDinh.CongTy.TenBoPhan : "";
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLucDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                qd.CanCu = quyetDinh.CanCu;
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
                //
                qd.ChucVuCu = quyetDinh.ChucVuCu != null ? quyetDinh.ChucVuCu.TenChucVu : "";
                qd.ChucDanhCu = quyetDinh.ChucDanhCu != null ? quyetDinh.ChucDanhCu.TenChucDanh : "";
                qd.PCKiemNhiemCu = quyetDinh.PhuCapKiemNhiemCu.ToString("N0");
                qd.PCTrachNhiemCu = quyetDinh.PhuCapTrachNhiemCu.ToString("N0");
                qd.NgayBNChucVuCu = quyetDinh.NgayBNChucVuCu != DateTime.MinValue ? quyetDinh.NgayBNChucVuCu.ToString("dd/MM/yyyy") : "";
                //
                qd.ChucVuMoi = quyetDinh.ChucVuCu != null ? quyetDinh.ChucVuCu.TenChucVu : "";
                qd.ChucDanhMoi = quyetDinh.ChucDanhMoi != null ? quyetDinh.ChucDanhMoi.TenChucDanh : "";
                qd.PCKiemNhiemMoi = quyetDinh.PhuCapKiemNhiemMoi.ToString("N0");
                qd.PCTrachNhiemMoi = quyetDinh.PhuCapTrachNhiemMoi.ToString("N0");
                qd.LyDo = quyetDinh.LyDo;
                //
                //
                if (quyetDinh.LoaiQuyetDinh == LoaiQuyetDinhEnum.HieuTruong)
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
                MailMergeTemplate merge = Common.GetTemplateWithValidDate(obs, "QuyetDinhThoiChucHT.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_QuyetDinhThoiChuc>(non_QuyetDinhHTList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mẫu Quyết định trong hệ thống.");
            }
            //
            if (non_QuyetDinhHDQTList.Count > 0)
            {
                MailMergeTemplate merge = Common.GetTemplateWithValidDate(obs, "QuyetDinhThoiChucHDQT.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_QuyetDinhThoiChuc>(non_QuyetDinhHDQTList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mẫu Quyết định trong hệ thống.");
            }

        }
    }
}
