using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.TienLuong;
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
    public static class Prosess_QuyetDinhKhenThuong
    {
        public static void Merge(IObjectSpace obs, List<QuyetDinhKhenThuong> quyetDinhKhenThuongList)
        {
            var quyetDinhCaNhan = from qd in quyetDinhKhenThuongList
                         where qd.ListChiTietQuyetDinhKhenThuongNhanVien.Count == 1
                         select qd;
            //
            var quyetDinhTapThe = from qd in quyetDinhKhenThuongList
                         where qd.ListChiTietQuyetDinhKhenThuongNhanVien.Count > 1
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

        private static void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhKhenThuong> qdList)
        {
            var non_QuyetDinhList = new List<Non_QuyetDinhKhenThuongCaNhan>();
            MailMergeTemplate merge = null;
            //
            foreach (QuyetDinhKhenThuong quyetDinh in qdList)
            {
                Non_QuyetDinhKhenThuongCaNhan qd = new Non_QuyetDinhKhenThuongCaNhan();
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
                foreach (ChiTietQuyetDinhKhenThuongNhanVien item in quyetDinh.ListChiTietQuyetDinhKhenThuongNhanVien)
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

                    merge = Common.GetTemplateWithValidDate(obs, "QuyetDinhKhenThuong.rtf", item.QuyetDinhKhenThuong.CongTy.Oid, item.QuyetDinhKhenThuong.NgayHieuLuc);
                }
                //
                non_QuyetDinhList.Add(qd);
            }
            //
            if (merge != null)
                Prosess_Show.ShowEditor<Non_QuyetDinhKhenThuongCaNhan>(non_QuyetDinhList, obs, merge);
            else
                DialogUtil.ShowError("Không tìm thấy mấu in Quyết định trong hệ thống.");
        }

        private static void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhKhenThuong> qdList)
        {
            var non_QuyetDinhList = new List<Non_QuyetDinhKhenThuongTapThe>();
            //
            foreach (QuyetDinhKhenThuong quyetDinh in qdList)
            {
                Non_QuyetDinhKhenThuongTapThe qd = new Non_QuyetDinhKhenThuongTapThe();
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
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.ChucDanh.TenChucDanh.ToUpper() : "";
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

                NienDoTaiChinh nienDo = obs.FindObject<NienDoTaiChinh>(CriteriaOperator.Parse("CongTy.Oid = ? and TuNgay <= ? and DenNgay >= ?", quyetDinh.CongTy.Oid, quyetDinh.NgayHieuLuc, quyetDinh.NgayHieuLuc));
                if (nienDo != null)
                {
                    qd.TenNienDoVietThuong = nienDo.TenNienDo.ToLower();
                    qd.TenNienDoVietHoa = nienDo.TenNienDo.ToUpper();
                }
                else
                    qd.TenNienDoVietThuong = qd.TenNienDoVietHoa = "";

                //Master
                Non_QuyetDinhKhenThuongTheMaster master = new Non_QuyetDinhKhenThuongTheMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = quyetDinh.CongTy != null ? quyetDinh.CongTy.DonViChuQuan : string.Empty;
                master.TenCongTyVietHoa = quyetDinh.CongTy != null ? quyetDinh.CongTy.TenBoPhan.ToUpper() : "";
                master.TenCongTyVietThuong = quyetDinh.CongTy != null ? quyetDinh.CongTy.TenBoPhan : "";
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.TenNguoiKy = qd.TenNguoiKyVietThuong;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.NgayHieuLuc = qd.NgayHieuLuc;
                master.TenNienDoVietThuong = qd.TenNienDoVietThuong;
                master.TenNienDoVietHoa = qd.TenNienDoVietHoa;

                qd.Master.Add(master);

                //Detail
                quyetDinh.ListChiTietQuyetDinhKhenThuongNhanVien.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhKhenThuongNhanVien.Sorting.Add(new DevExpress.Xpo.SortProperty("ThongTinNhanVien.Ten", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietQuyetDinhKhenThuongNhanVien item in quyetDinh.ListChiTietQuyetDinhKhenThuongNhanVien)
                {
                    Non_QuyetDinhKhenThuongTapTheDetail detail = new Non_QuyetDinhKhenThuongTapTheDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = item.BoPhan.TenBoPhan;                    
                    //
                    detail.MaTapDoan = item.ThongTinNhanVien.MaTapDoan;
                    detail.MaNhanVien = item.ThongTinNhanVien.MaNhanVien;
                    detail.ChucDanh = item.ThongTinNhanVien.ChucDanh != null ? item.ThongTinNhanVien.ChucDanh.TenChucDanh : "";
                    detail.NgayVaoCT = item.ThongTinNhanVien.NgayVaoCongTy != DateTime.MinValue ? item.ThongTinNhanVien.NgayVaoCongTy.ToString("d") : "";
                    detail.NgayVaoTD = item.ThongTinNhanVien.NgayVaoTapDoan != DateTime.MinValue ? item.ThongTinNhanVien.NgayVaoTapDoan.ToString("d") : "";
                    //
                    detail.DanhHieuKhenThuong = item.DanhHieuKhenThuong != null ? item.DanhHieuKhenThuong.TenDanhHieu : "";
                    detail.NgayKhenThuong = qd.NgayHieuLuc;
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

            //mở hai cửa sổ cho quyết định và danh sách khi in qđ            
            MailMergeTemplate[] merge1 = new MailMergeTemplate[3];
            merge1[0] = Common.GetTemplateWithValidDate(obs, "QuyetDinhKhenThuongTapTheEmpty.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
            merge1[1] = Common.GetTemplateWithValidDate(obs, "QuyetDinhKhenThuongTapTheMaster.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
            merge1[2] = Common.GetTemplateWithValidDate(obs, "QuyetDinhKhenThuongTapTheDetail.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);

            MailMergeTemplate[] merge = new MailMergeTemplate[1];
            merge[0] = Common.GetTemplateWithValidDate(obs, "QuyetDinhKhenThuongTapThe.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
            
            if (merge[0] != null)
            {
                Prosess_Show.ShowEditor<Non_QuyetDinhKhenThuongTapThe>(non_QuyetDinhList, obs, merge);
                if (merge1[0] != null && merge1[1] != null)
                    Prosess_Show.ShowEditor<Non_QuyetDinhKhenThuongTapThe>(non_QuyetDinhList, obs, merge1);
                else
                    DialogUtil.ShowError("Không tìm thấy mẫu in Chi tiết quyết định trong hệ thống.");
            }
            else
                DialogUtil.ShowError("Không tìm thấy mẫu in Quyết định trong hệ thống.");
            
        }
    }
}
