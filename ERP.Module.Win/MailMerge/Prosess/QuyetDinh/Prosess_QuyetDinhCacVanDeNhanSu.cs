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
    public static class Prosess_QuyetDinhCacVanDeNhanSu
    {
        public static void ShowMailMerge(IObjectSpace obs, List<QuyetDinhCacVanDeNhanSu> qdList)
        {
            var non_QuyetDinhList = new List<Non_QuyetDinhCacVanDeNhanSu>();
            Non_QuyetDinhCacVanDeNhanSu qd;
            foreach (QuyetDinhCacVanDeNhanSu quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhCacVanDeNhanSu();
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
                //
                qd.ChucVuCu = quyetDinh.ChucVuCu != null ? quyetDinh.ChucVuCu.TenChucVu : "";
                qd.ChucVuMoi = quyetDinh.ChucVuMoi != null ? quyetDinh.ChucVuMoi.TenChucVu : "";
                qd.ChucDanhCu = quyetDinh.ChucDanhCu != null ? quyetDinh.ChucDanhCu.TenChucDanh : "";
                qd.ChucDanhMoi = quyetDinh.ChucDanhMoi != null ? quyetDinh.ChucDanhMoi.TenChucDanh : "";
                qd.NgaySinh = quyetDinh.ThongTinNhanVien.NgaySinh != null ? quyetDinh.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy") : "...........";
                qd.MaNhanVien = quyetDinh.ThongTinNhanVien.MaTapDoan != null ? quyetDinh.ThongTinNhanVien.MaTapDoan.ToString() : "...............";
                qd.NgayVaoCoQuan = quyetDinh.ThongTinNhanVien.NgayVaoCongTy != null ? quyetDinh.ThongTinNhanVien.NgayVaoCongTy.ToString("dd/MM/yyyy") : "..............";
                qd.ChuyenNganhDaoTao = quyetDinh.ThongTinNhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao != null ? quyetDinh.ThongTinNhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao.ToString() : "...............";
                qd.TrinhDoChuyenMon = quyetDinh.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? quyetDinh.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.ToString() : "";
                qd.DonViMoi = quyetDinh.BoPhanMoi != null ? quyetDinh.BoPhanMoi.TenBoPhan.ToString() : quyetDinh.BoPhan.TenBoPhan.ToString();
                qd.TruongCongTyMoi = quyetDinh.BoPhanMoi!= null ? quyetDinh.BoPhanMoi.CongTy.TenBoPhan : "";
                qd.TongLuongCu = (quyetDinh.LuongCoBanCu + quyetDinh.LuongKinhDoanhCu).ToString("#,###");
                qd.TongLuongBoSungKhacCu = quyetDinh.LuongBoSungKhacCu.ToString("#,###");
                qd.TongLuongBoSungKhacMoi = quyetDinh.LuongBoSungKhacMoi.ToString("#,###");
                qd.CheDoCu = quyetDinh.CheDoCu;
                qd.CheDoMoi = quyetDinh.CheDoMoi;
                if (quyetDinh.LoaiQuyetDinhVaHopDong != LoaiQuyetDinhVaHopDongEnum.DieuChinhTienLuong)
                    qd.TongLuongMoi = qd.TongLuongCu;
                else
                     qd.TongLuongMoi = (quyetDinh.LuongCoBanMoi + quyetDinh.LuongKinhDoanhMoi).ToString("#,###");
              

                //
                if (quyetDinh.LoaiQuyetDinhVaHopDong == LoaiQuyetDinhVaHopDongEnum.BoNhiem)
                    qd.NoiDungDeXuat = "Bổ nhiệm";
                else if (quyetDinh.LoaiQuyetDinhVaHopDong == LoaiQuyetDinhVaHopDongEnum.TaiBoNhiem)
                    qd.NoiDungDeXuat = "Tái bổ nhiệm";
                else if (quyetDinh.LoaiQuyetDinhVaHopDong == LoaiQuyetDinhVaHopDongEnum.MienNhiem)
                    qd.NoiDungDeXuat = "Miễn nhiệm";
                else if (quyetDinh.LoaiQuyetDinhVaHopDong == LoaiQuyetDinhVaHopDongEnum.DieuDong)
                    qd.NoiDungDeXuat = "Điều động";
                else if (quyetDinh.LoaiQuyetDinhVaHopDong == LoaiQuyetDinhVaHopDongEnum.LuanChuyen)
                    qd.NoiDungDeXuat = "Luân chuyển";
                else if (quyetDinh.LoaiQuyetDinhVaHopDong == LoaiQuyetDinhVaHopDongEnum.ChamDutHDLD)
                    qd.NoiDungDeXuat = "Chấm dứt hợp đồng lao động";
                else if (quyetDinh.LoaiQuyetDinhVaHopDong == LoaiQuyetDinhVaHopDongEnum.DieuChinhTienLuong)
                    qd.NoiDungDeXuat = "Điều chỉnh tiền lương";
                else if (quyetDinh.LoaiQuyetDinhVaHopDong == LoaiQuyetDinhVaHopDongEnum.TamHoanHDLD)
                    qd.NoiDungDeXuat = "Tạm hoãn hợp đồng lao động";
                else if (quyetDinh.LoaiQuyetDinhVaHopDong == LoaiQuyetDinhVaHopDongEnum.UngCuNoiBo)
                    qd.NoiDungDeXuat = "Ứng cử nội bộ";
                else if (quyetDinh.LoaiQuyetDinhVaHopDong == LoaiQuyetDinhVaHopDongEnum.LoaiKhac)
                    qd.NoiDungDeXuat = "Khác : .................";
               


                if (quyetDinh.LoaiCongTy.MaQuanLy.ToString() == "01")
                {
                    qd.BanTongGiamDocHoacBanGiamHieu = "Ban Tổng giám đốc";
                    qd.DanhXungCongTyHoacTruong = "Công ty";
                }
                else if (quyetDinh.LoaiCongTy.MaQuanLy.ToString() == "02")
                {
                    qd.BanTongGiamDocHoacBanGiamHieu = "Ban giám hiệu";
                    qd.DanhXungCongTyHoacTruong = "Trường";
                }
                else
                    qd.BanTongGiamDocHoacBanGiamHieu = "";


                non_QuyetDinhList.Add(qd);

            }
           
            if (non_QuyetDinhList.Count > 0)
            {
                MailMergeTemplate merge = Common.GetTemplateWithValidDate(obs, "QuyetDinhCacVanDeNhanSu.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_QuyetDinhCacVanDeNhanSu>(non_QuyetDinhList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mẫu Quyết định trong hệ thống.");
            }  
        }
    }
}
