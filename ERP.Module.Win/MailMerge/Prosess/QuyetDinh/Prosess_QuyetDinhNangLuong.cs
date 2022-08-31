using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using ERP.Module.Commons;
using ERP.Module.Enum.NhanSu;
using ERP.Module.Extends;
using ERP.Module.MailMerge;
using ERP.Module.MailMerge.NhanSu.QuyetDinh;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Win.MailMerge.Prosess.ShowMaiMerge;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Module.Win.MailMerge.Prosess.QuyetDinh
{
    public static class Prosess_QuyetDinhNangLuong
    {
        public static void Merge(IObjectSpace obs, List<QuyetDinhNangLuong> quyetDinhNangLuongList)
        {
            var quyetDinhCaNhan = from qd in quyetDinhNangLuongList
                         where qd.ListChiTietQuyetDinhNangLuong.Count == 1
                         select qd;
            //
            var quyetDinhTapThe = from qd in quyetDinhNangLuongList
                         where qd.ListChiTietQuyetDinhNangLuong.Count > 1
                         select qd;
            //
            if (quyetDinhCaNhan.Count() > 0)
            {
                //
                QuyetDinhCaNhan(obs, quyetDinhCaNhan.ToList());
            }
            if (quyetDinhTapThe.Count() > 0)
            {
                //
                QuyetDinhTapThe(obs, quyetDinhTapThe.ToList());
            }
        }

        private static void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhNangLuong> qdList)
        {
            var non_QuyetDinhList = new List<Non_QuyetDinhNangLuongCaNhan>();
            MailMergeTemplate merge = null;
            //
            foreach (QuyetDinhNangLuong quyetDinh in qdList)
            {
                Non_QuyetDinhNangLuongCaNhan qd = new Non_QuyetDinhNangLuongCaNhan();
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
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.ChucVu.TenChucVu.ToUpper() : "";
                if (quyetDinh.NguoiKy != null)
                {
                    qd.TenNguoiKyVietHoa = quyetDinh.NguoiKy.HoTen != "" ? quyetDinh.NguoiKy.HoTen.ToUpper() : quyetDinh.TenNguoiKy.ToUpper();
                    qd.TenNguoiKyVietThuong = quyetDinh.NguoiKy.HoTen != "" ? quyetDinh.NguoiKy.HoTen : quyetDinh.TenNguoiKy;
                    qd.DanhXungNguoiKyVietHoa = quyetDinh.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungNguoiKyVietThuong = quyetDinh.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                }
                //
                qd.SoDoanhNghiep = quyetDinh.CongTy.MaSoThue != null ? quyetDinh.CongTy.MaSoThue.Trim().ToString() : "....";
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
                foreach (ChiTietQuyetDinhNangLuong item in quyetDinh.ListChiTietQuyetDinhNangLuong)
                {
                    qd.ChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : "";
                    qd.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.LoaiHopDong = item.ThongTinNhanVien.LoaiHopDong != null ? item.ThongTinNhanVien.LoaiHopDong.TenLoaiHopDong : "";
                    qd.HoTenVietHoa = item.ThongTinNhanVien.HoTen.ToUpper();
                    qd.HoTenVietThuong = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = item.BoPhan.TenBoPhan;
                    qd.NgaySinh = item.ThongTinNhanVien.NgaySinh != DateTime.MinValue ? item.ThongTinNhanVien.NgaySinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                    qd.NgaySinhDate = item.ThongTinNhanVien.NgaySinh  != DateTime.MinValue ? item.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy") : "";
                    qd.ChucDanh = item.ThongTinNhanVien.ChucDanh != null ? item.ThongTinNhanVien.ChucDanh.TenChucDanh : "";
                    //Thông tin cũ
                    qd.TenNgachLuong = item.NgachLuongCu != null ? item.NgachLuongCu.TenNgachLuong : string.Empty;
                    qd.MaNgachLuong = item.NgachLuongCu != null ? item.NgachLuongCu.MaQuanLy : "";
                    qd.BacLuong = item.BacLuongCu != null ? item.BacLuongCu.TenBacLuong : "";
                    qd.NgayHuongLuong = item.NgayHuongLuongCu != DateTime.MinValue ? item.NgayHuongLuongCu.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                    qd.NgayHuongLuongDate = item.NgayHuongLuongCu != DateTime.MinValue ? item.NgayHuongLuongCu.ToString("dd/MM/yyyy") : "";
                    qd.LuongCoBan = item.LuongCoBanCu.ToString("N0");
                    qd.LuongKinhDoanh = item.LuongKinhDoanhCu.ToString("N0");

                    //Thông tin mới
                    qd.TenNgachLuongMoi = item.NgachLuongMoi != null ? item.NgachLuongMoi.TenNgachLuong : string.Empty;
                    qd.MaNgachLuongMoi = item.NgachLuongMoi != null ? item.NgachLuongMoi.MaQuanLy : "";
                    qd.NgayHuongLuongMoi = item.NgayHuongLuongMoi != DateTime.MinValue ? item.NgayHuongLuongMoi.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                    qd.NgayHuongLuongMoiDate = item.NgayHuongLuongMoi != DateTime.MinValue ? item.NgayHuongLuongMoi.ToString("dd/MM/yyyy") : "";
                    qd.BacLuongMoi = item.BacLuongMoi != null ? item.BacLuongMoi.TenBacLuong : "";
                    qd.NgayHuongLuongMoi = item.NgayHuongLuongMoi != DateTime.MinValue ? item.NgayHuongLuongMoi.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                    qd.NgayHuongLuongMoiDate = item.NgayHuongLuongMoi != DateTime.MinValue ? item.NgayHuongLuongMoi.ToString("dd/MM/yyyy") : "";
                    qd.LuongCoBanMoi = item.LuongCoBanMoi.ToString("N0");
                    qd.LuongKinhDoanhMoi = item.LuongKinhDoanhMoi.ToString("N0");
                    qd.PCTrachNhiemMoi = item.PhuCapTrachNhiemMoi.ToString("N0");
                    qd.PCKiemNhiemMoi = item.PhuCapKiemNhiemMoi.ToString("N0");
                    //
                    qd.TongLuongMoi = (item.LuongCoBanMoi + item.LuongKinhDoanhMoi + item.PhuCapKiemNhiemMoi).ToString("N0");
                    //
                    qd.LoaiNangLuong = "nâng lương thường xuyên";

                    if (quyetDinh.LoaiQuyetDinh == LoaiQuyetDinhEnum.HoiDongQuanTri)
                        merge = Common.GetTemplateWithValidDate(obs, "QuyetDinhNangLuongHDQT.rtf", item.QuyetDinhNangLuong.CongTy.Oid, item.QuyetDinhNangLuong.NgayHieuLuc);
                    else
                        merge = Common.GetTemplateWithValidDate(obs, "QuyetDinhNangLuong.rtf",item.QuyetDinhNangLuong.CongTy.Oid, item.QuyetDinhNangLuong.NgayHieuLuc);
                }
                //
                non_QuyetDinhList.Add(qd);
            }
            //
            if (merge != null)
               Prosess_Show.ShowEditor<Non_QuyetDinhNangLuongCaNhan>(non_QuyetDinhList, obs, merge);
            else
                DialogUtil.ShowError("Không tìm thấy mấu in Quyết định trong hệ thống.");
        }

        private static void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhNangLuong> qdList)
        {
            var non_QuyetDinhList = new List<Non_QuyetDinhNangLuongTapThe>();
            //
            foreach (QuyetDinhNangLuong quyetDinh in qdList)
            {
                Non_QuyetDinhNangLuongTapThe qd = new Non_QuyetDinhNangLuongTapThe();
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
                //
                qd.SoDoanhNghiep = quyetDinh.CongTy.MaSoThue != null ? quyetDinh.CongTy.MaSoThue.ToString() : ".....";
                if (quyetDinh.CongTy.DiaChi != null && quyetDinh.CongTy.DiaChi.TinhThanh != null)
                    qd.TinhThanh = quyetDinh.CongTy.DiaChi.TinhThanh.TenTinhThanh.ToString();
                else
                    qd.TinhThanh = ".....";
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

                //Master
                Non_QuyetDinhNangLuongTapTheMaster master = new Non_QuyetDinhNangLuongTapTheMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = quyetDinh.CongTy != null ? quyetDinh.CongTy.DonViChuQuan : string.Empty;
                master.TenCongTyVietHoa = quyetDinh.CongTy != null ? quyetDinh.CongTy.TenBoPhan.ToUpper() : "";
                master.TenCongTyVietThuong = quyetDinh.CongTy != null ? quyetDinh.CongTy.TenBoPhan : "";
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.TenNguoiKy = qd.TenNguoiKyVietThuong;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                qd.Master.Add(master);

                //Detail
                quyetDinh.ListChiTietQuyetDinhNangLuong.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhNangLuong.Sorting.Add(new DevExpress.Xpo.SortProperty("ThongTinNhanVien.Ten", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietQuyetDinhNangLuong item in quyetDinh.ListChiTietQuyetDinhNangLuong)
                {
                    Non_QuyetDinhNangLuongTapTheDetail detail = new Non_QuyetDinhNangLuongTapTheDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = item.BoPhan.TenBoPhan;                    
                    //
                    detail.MaNgachLuong = item.NgachLuongCu != null ? item.NgachLuongCu.MaQuanLy : "";
                    detail.NgachLuong = item.NgachLuongCu != null ? item.NgachLuongCu.TenNgachLuong : "";
                    //
                    detail.BacLuongCu = item.BacLuongCu != null ? item.BacLuongCu.TenBacLuong : "";
                    detail.MocNangLuongCu = item.MocNangLuongCu != DateTime.MinValue ? item.MocNangLuongCu.ToString("d") : "";
                    detail.NgayHuongLuongCu = item.NgayHuongLuongCu != DateTime.MinValue ? item.NgayHuongLuongCu.ToString("d") : "";
                    //
                    detail.BacLuongMoi = item.BacLuongMoi != null ? item.BacLuongMoi.TenBacLuong : "";
                    detail.NgayHuongLuongMoi = item.NgayHuongLuongMoi != DateTime.MinValue ? item.NgayHuongLuongMoi.ToString("d") : "";
                    //
                    qd.Detail.Add(detail);
                    //
                    stt++;
                }
                //
                qd.SoLuongCanBo = (stt-1).ToString("N0");
                //
                non_QuyetDinhList.Add(qd);
            }
            //
            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhNangLuongTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhNangLuongTapTheDetail.rtf")); ;
            merge[0] = Common.GetTemplateWithValidDate(obs, "QuyetDinhNangLuongTapThe.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
            if (merge[0] != null)
                Prosess_Show.ShowEditor<Non_QuyetDinhNangLuongTapThe>(non_QuyetDinhList, obs, merge);
            else
                DialogUtil.ShowError("Không tìm thấy mấu in Quyết định trong hệ thống.");
        }
    }
}
