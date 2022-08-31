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

namespace ERP.Module.Win.MailMerge.Prosess.QuyetDinh
{
    public static class Prosess_QuyetDinhThoiChucKiemNhiem
    {
        public static void ShowMailMerge(IObjectSpace obs, List<QuyetDinhThoiChucKiemNhiem> qdList)
        {
            var non_QuyetDinhList = new List<Non_QuyetDinhThoiChucKiemNhiem>();
            Non_QuyetDinhThoiChucKiemNhiem qd;
            foreach (QuyetDinhThoiChucKiemNhiem quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhThoiChucKiemNhiem();
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
                qd.ChucVuMoi = quyetDinh.QuyetDinhBoNhiemKiemNhiem.ChucVuMoi != null ? quyetDinh.QuyetDinhBoNhiemKiemNhiem.ChucVuMoi.TenChucVu : "";
                qd.HSPCChucVuMoi = quyetDinh.QuyetDinhBoNhiemKiemNhiem.HSPCChucVuMoi.ToString();
                qd.NgayHuongHeSoMoi = quyetDinh.QuyetDinhBoNhiemKiemNhiem.NgayHuongHeSoMoi != DateTime.MinValue ? quyetDinh.QuyetDinhBoNhiemKiemNhiem.NgayHuongHeSoMoi.ToString("dd/MM/yyyy") : "";
                qd.NgayHetNhiemKy = quyetDinh.QuyetDinhBoNhiemKiemNhiem.NgayHetNhiemKy != DateTime.MinValue ? quyetDinh.QuyetDinhBoNhiemKiemNhiem.NgayHetNhiemKy.ToString("dd/MM/yyyy") : "";
                qd.LyDo = quyetDinh.LyDo;
     
                non_QuyetDinhList.Add(qd);
            }
            //
            if (non_QuyetDinhList.Count > 0)
            {
                MailMergeTemplate merge = Common.GetTemplateWithValidDate(obs, "QuyetDinhThoiChucKiemNhiem.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_QuyetDinhThoiChucKiemNhiem>(non_QuyetDinhList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mẫu Quyết định trong hệ thống.");
            }
           
        }
    }
}
