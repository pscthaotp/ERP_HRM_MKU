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
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace ERP.Module.Win.MailMerge.Prosess.QuyetDinh
{
    public static class Prosess_QuyetDinhThanhLapDonVi
    {
        public static void ShowMailMergeQuyetDinhCaNhan(IObjectSpace obs, List<QuyetDinhThanhLapDonVi> qdList)
        {
            var non_QuyetDinhHTList = new List<Non_QuyetDinhThanhLapDonViCaNhan>();
            var non_QuyetDinhHDQTList = new List<Non_QuyetDinhThanhLapDonViCaNhan>();
            Non_QuyetDinhThanhLapDonViCaNhan qd;
            foreach (QuyetDinhThanhLapDonVi quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhThanhLapDonViCaNhan();
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
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.ChucVu.TenChucVu.ToUpper() : "";
                if (quyetDinh.ChucVuNguoiKy != null)
                {
                    qd.TenNguoiKyVietHoa = quyetDinh.NguoiKy.HoTen != "" ? quyetDinh.NguoiKy.HoTen.ToUpper() : quyetDinh.TenNguoiKy.ToUpper();
                    qd.TenNguoiKyVietThuong = quyetDinh.NguoiKy.HoTen != "" ? quyetDinh.NguoiKy.HoTen : quyetDinh.TenNguoiKy;
                    qd.DanhXungNguoiKyVietHoa = quyetDinh.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungNguoiKyVietThuong = quyetDinh.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    //if (quyetDinh.NguoiKy.NhanVienTrinhDo.HocHam != null || quyetDinh.NguoiKy.NhanVienTrinhDo.TrinhDoChuyenMon != null)
                    //{
                    //    qd.HocHamHocViNguoiKy = quyetDinh.NguoiKy.NhanVienTrinhDo.HocHam != null ? quyetDinhNguoiKy.NhanVienTrinhDo.HocHam.MaQuanLy + ". " : ""
                    //        + quyetDinh.NguoiKy.NhanVienTrinhDo.TrinhDoChuyenMon != null ? quyetDinh.NguoiKy.NhanVienTrinhDo.TrinhDoChuyenMon.MaQuanLy + "." : "";
                    //}
                    //else
                    //{
                    //    qd.HocHamHocViNguoiKy = "";
                    //}
                }
                qd.DonViMoi = quyetDinh.BoPhan != null ? quyetDinh.BoPhan.TenBoPhan : "";
                qd.ChucNangDonViMoi = quyetDinh.ChucNangDonViMoi != null ? quyetDinh.ChucNangDonViMoi : "";
                qd.NhiemVuDonViMoi = quyetDinh.NhiemVuDonViMoi != null ? quyetDinh.NhiemVuDonViMoi : "";
                qd.DonViTach = quyetDinh.DonViTach != null ? quyetDinh.DonViTach : "";
                qd.TenTiengAnhDonViMoi = quyetDinh.TenTiengAnhBoPhanMoi != null ? quyetDinh.TenTiengAnhBoPhanMoi : "";
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

                if (quyetDinh.LoaiQuyetDinh == LoaiQuyetDinhEnum.HieuTruong || quyetDinh.LoaiQuyetDinh == LoaiQuyetDinhEnum.TongGiamDoc)
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
                MailMergeTemplate merge = Common.GetTemplateWithValidDate(obs, "QuyetDinhThanhLapDonViHT.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_QuyetDinhThanhLapDonViCaNhan>(non_QuyetDinhHTList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mẫu Quyết định trong hệ thống.");
            }
            //
            if (non_QuyetDinhHDQTList.Count > 0)
            {
                MailMergeTemplate merge = Common.GetTemplateWithValidDate(obs, "QuyetDinhThanhLapDonViHDQT.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_QuyetDinhThanhLapDonViCaNhan>(non_QuyetDinhHDQTList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mẫu Quyết định trong hệ thống.");
            }
        }

        public static void ShowMailMergeQuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhThanhLapDonVi> qdList)
        {
            var list = new List<Non_QuyetDinhThanhLapDonViTapThe>();
            Non_QuyetDinhThanhLapDonViTapThe qd;
            foreach (QuyetDinhThanhLapDonVi quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhThanhLapDonViTapThe();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.CongTy != null ? quyetDinh.CongTy.DonViChuQuan : string.Empty;
                qd.TenCongTyVietHoa = quyetDinh.CongTy != null ? quyetDinh.CongTy.TenBoPhan.ToUpper() : "";
                qd.TenCongTyVietThuong = quyetDinh.CongTy != null ? quyetDinh.CongTy.TenBoPhan : ""; ;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                qd.NgayHieuLucDate = quyetDinh.NgayHieuLuc.ToString("dd/MM/yyyy");
                qd.CanCu = quyetDinh.CanCu;
                qd.NhiemVuDonViMoi = quyetDinh.NhiemVuDonViMoi != null ? quyetDinh.NhiemVuDonViMoi : "";
                qd.NhiemVuDonViKhac = quyetDinh.NhiemVuDonViKhac != null ? quyetDinh.NhiemVuDonViKhac : "";
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.ChucVu.TenChucVu.ToUpper() : "";
                qd.ChucVuNguoiKyVietThuong = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.ChucVu.TenChucVu : "";
                qd.DonViMoi = quyetDinh.BoPhan != null ? quyetDinh.BoPhan.TenBoPhan : "";
                qd.NhiemVuDonViKhac = quyetDinh.NhiemVuDonViKhac != null ? quyetDinh.NhiemVuDonViKhac : "";
                if (quyetDinh.ChucVuNguoiKy != null)
                {
                    qd.TenNguoiKyVietHoa = quyetDinh.NguoiKy.HoTen != "" ? quyetDinh.NguoiKy.HoTen.ToUpper() : "";
                    qd.TenNguoiKyVietThuong = quyetDinh.NguoiKy.HoTen != "" ? quyetDinh.NguoiKy.HoTen : "";
                    //if (quyetDinh.NguoiKy.NhanVienTrinhDo.HocHam != null || quyetDinh.NguoiKy.NhanVienTrinhDo.TrinhDoChuyenMon != null)
                    //{
                    //    qd.HocHamHocViNguoiKy = quyetDinh.ChucVuNguoiKy.ThongTinNhanVien.NhanVienTrinhDo.HocHam != null ? quyetDinh.ChucVuNguoiKy.ThongTinNhanVien.NhanVienTrinhDo.HocHam.MaQuanLy + ". " : ""
                    //        + quyetDinh.ChucVuNguoiKy.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? quyetDinh.ChucVuNguoiKy.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.MaQuanLy + "." : "";
                    //}
                    //else
                    //{
                    //    qd.HocHamHocViNguoiKy = "";
                    //}
                    qd.DanhXungNguoiKyVietHoa = quyetDinh.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungNguoiKyVietThuong = quyetDinh.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                }
                //qd.SoLuongCanBo = quyetDinh.ListChiTietQuyetDinhThanhLapDonVi.Count.ToString();

                //master
                Non_QuyetDinhThanhLapDonViMaster master = new Non_QuyetDinhThanhLapDonViMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenCongTyVietHoa = qd.TenCongTyVietHoa;
                master.TenCongTyVietThuong = qd.TenCongTyVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.TenNguoiKyVietThuong;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.DotTuyenDung = qd.DotTuyenDung;
                master.NamTuyenDung = qd.NamTuyenDung;
               // master.SoNguoi = quyetDinh.ListChiTietQuyetDinhThanhLapDonVi.Count.ToString();


                qd.Master.Add(master);

                //detail
                Non_QuyetDinhThanhLapDonViDetail detail;
                //quyetDinh.ListChiTietQuyetDinhThanhLapDonVi.Sorting.Clear();
                //quyetDinh.ListChiTietQuyetDinhThanhLapDonVi.Sorting.Add(new DevExpress.Xpo.SortProperty("ThongTinNhanVien.Ten", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                //foreach (ChiTietQuyetDinhThanhLapDonVi item in quyetDinh.ListChiTietQuyetDinhThanhLapDonVi)
                //{
                //    detail = new Non_QuyetDinhThanhLapDonViDetail();
                //    detail.Oid = quyetDinh.Oid.ToString();
                //    detail.STT = stt.ToString();
                //    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                //    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                //    detail.HoTen = item.ThongTinNhanVien.HoTen;
                //    detail.DonVi = item.BoPhan.TenBoPhan;
                //    detail.NamSinh = item.ThongTinNhanVien.NgaySinh.Year.ToString();
                //    detail.NgaySinhDate = item.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                //    detail.QueQuan = item.ThongTinNhanVien.QueQuan.FullDiaChi;
                //    //detail.ChucDanh = item.ThongTinNhanVien.ChucDanh != null ? item.ThongTinNhanVien.ChucDanh.TenChucDanh : "";
                //    detail.ChucVu = item.ThongTinNhanVien.ChucDanh != null ? item.ThongTinNhanVien.ChucDanh.TenChucDanh : "";

                //    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nu)
                //        detail.GioiTinh = "Nữ";
                //    else
                //        detail.GioiTinh = "Nam";

                //    qd.Detail.Add(detail);
                //    stt++;
                //}

                list.Add(qd);

                MailMergeTemplate[] merge = new MailMergeTemplate[3];
                merge[1] = Common.GetTemplateWithValidDate(obs, "QuyetDinhThanhLapDonViTapTheMaster.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
                merge[2] = Common.GetTemplateWithValidDate(obs, "QuyetDinhThanhLapDonViTapTheDetail.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
                merge[0] = Common.GetTemplateWithValidDate(obs, "QuyetDinhThanhLapDonViTapThe.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);

                if (merge[0] != null)
                    Prosess_Show.ShowEditor<Non_QuyetDinhThanhLapDonViTapThe>(list, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mấu in QĐ tuyển dụng trong hệ thống.");
            }


        }
    }
}
