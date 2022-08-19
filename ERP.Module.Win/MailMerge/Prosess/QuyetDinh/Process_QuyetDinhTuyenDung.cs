using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using ERP.Module.Commons;
using ERP.Module.Enum.NhanSu;
using ERP.Module.MailMerge;
using ERP.Module.MailMerge.NhanSu.QuyetDinh;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Win.MailMerge.Prosess.ShowMaiMerge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ERP.Module.Win.MailMerge.Prosess.QuyetDinh
{
    public static class Process_QuyetDinhTuyenDung
    {
        public static void ShowMailMergeQuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhTuyenDung> qdList)
        {
            var list = new List<Non_QuyetDinhTuyenDungCaNhan>();
            Non_QuyetDinhTuyenDungCaNhan qd;
            foreach (QuyetDinhTuyenDung quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhTuyenDungCaNhan();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.CongTy != null ? quyetDinh.CongTy.DonViChuQuan : string.Empty;
                qd.TenCongTyVietHoa = quyetDinh.CongTy != null ? quyetDinh.CongTy.TenBoPhan.ToUpper() : "";
                qd.TenCongTyVietThuong = quyetDinh.CongTy != null ? quyetDinh.CongTy.TenBoPhan : "";
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                qd.NgayHieuLucDate = quyetDinh.NgayHieuLuc.ToString("dd/MM/yyyy"); qd.CanCu = quyetDinh.CanCu;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.ChucDanh.TenChucDanh.ToUpper() : "";
                if (quyetDinh.NguoiKy != null)
                {
                    qd.TenNguoiKyVietHoa = quyetDinh.NguoiKy.HoTen != "" ? quyetDinh.NguoiKy.HoTen.ToUpper() : quyetDinh.TenNguoiKy.ToUpper();
                    qd.TenNguoiKyVietThuong = quyetDinh.NguoiKy.HoTen != "" ? quyetDinh.NguoiKy.HoTen : quyetDinh.TenNguoiKy;
                    qd.DanhXungNguoiKyVietHoa = quyetDinh.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungNguoiKyVietThuong = quyetDinh.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                }                 
                if (quyetDinh.QuanLyTuyenDung == null)
                {
                    XtraMessageBox.Show("Không tìm thấy Quản lý tuyển dụng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    qd.DotTuyenDung = quyetDinh.QuanLyTuyenDung.DotTuyenDung;
                    qd.NamTuyenDung = quyetDinh.QuanLyTuyenDung.NienDoTaiChinh.TenNienDo;
                }

                foreach (ChiTietQuyetDinhTuyenDung item in quyetDinh.ListChiTietQuyetDinhTuyenDung)
                {
                    qd.ChucDanh = item.ThongTinNhanVien.ChucDanh.TenChucDanh;
                    qd.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.LoaiNhanSu = item.ThongTinNhanVien.LoaiNhanSu.TenLoaiNhanSu;
                    qd.HoTenVietHoa = item.ThongTinNhanVien.HoTen.ToUpper();
                    qd.HoTenVietThuong = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = item.BoPhan.TenBoPhan;
                    qd.MaNgach = item.NgachLuong != null ? item.NgachLuong.MaQuanLy : "";
                    qd.NgachLuong = item.NgachLuong != null ? item.NgachLuong.TenNgachLuong : "";
                    qd.BacLuong = item.BacLuong != null ? item.BacLuong.MaQuanLy : "";
                    qd.HeSoLuong = item.HeSoLuong.ToString("N2");
                    qd.NgayHuongLuong = item.NgayHuongLuong != DateTime.MinValue ? item.NgayHuongLuong.ToString("d") : "";
                    qd.ThoiGianTapSu = item.ThoiGianTapSu.ToString("N0");
                    break;
                }
                list.Add(qd);
            }

            MailMergeTemplate merge = Common.GetTemplate(obs, "QuyetDinhTuyenDung.rtf", qdList[0].CongTy.Oid);
            if (merge != null)
                Prosess_Show.ShowEditor<Non_QuyetDinhTuyenDungCaNhan>(list, obs, merge);
            else
                XtraMessageBox.Show("Không tìm thấy mấu in QĐ tuyển dụng trong hệ thống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ShowMailMergeQuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhTuyenDung> qdList)
        {
            var list = new List<Non_QuyetDinhTuyenDungTapThe>();
            Non_QuyetDinhTuyenDungTapThe qd;
            foreach (QuyetDinhTuyenDung quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhTuyenDungTapThe();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.CongTy != null ? quyetDinh.CongTy.DonViChuQuan : string.Empty;
                qd.TenCongTyVietHoa = quyetDinh.CongTy != null ? quyetDinh.CongTy.TenBoPhan.ToUpper() : "";
                qd.TenCongTyVietThuong = quyetDinh.CongTy != null ? quyetDinh.CongTy.TenBoPhan : "";;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                qd.NgayHieuLucDate = quyetDinh.NgayHieuLuc.ToString("dd/MM/yyyy");
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
                if (quyetDinh.QuanLyTuyenDung == null)
                {
                    XtraMessageBox.Show("Không tìm thấy Quản lý tuyển dụng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    qd.DotTuyenDung = quyetDinh.QuanLyTuyenDung.DotTuyenDung;
                    qd.NamTuyenDung = quyetDinh.QuanLyTuyenDung.NienDoTaiChinh.TenNienDo;
                }
                qd.SoLuongCanBo = quyetDinh.ListChiTietQuyetDinhTuyenDung.Count.ToString();

                //master
                Non_QuyetDinhTuyenDungMaster master = new Non_QuyetDinhTuyenDungMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenCongTyVietHoa = qd.TenCongTyVietHoa;
                master.TenCongTyVietThuong = qd.TenCongTyVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.TenNguoiKyVietThuong;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.DotTuyenDung = qd.DotTuyenDung;
                master.NamTuyenDung = qd.NamTuyenDung;
                master.SoNguoi = quyetDinh.ListChiTietQuyetDinhTuyenDung.Count.ToString();
                
                
                qd.Master.Add(master);

                //detail
                Non_QuyetDinhTuyenDungDetail detail;
                quyetDinh.ListChiTietQuyetDinhTuyenDung.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhTuyenDung.Sorting.Add(new DevExpress.Xpo.SortProperty("ThongTinNhanVien.Ten", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietQuyetDinhTuyenDung item in quyetDinh.ListChiTietQuyetDinhTuyenDung)
                {
                    detail = new Non_QuyetDinhTuyenDungDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = item.BoPhan.TenBoPhan;
                    detail.MaNgach = item.NgachLuong != null ? item.NgachLuong.MaQuanLy : "";
                    detail.NgachLuong = item.NgachLuong != null ? item.NgachLuong.TenNgachLuong : "";
                    detail.BacLuong = item.BacLuong != null ? item.BacLuong.MaQuanLy : "";
                    detail.HeSoLuong = item.HeSoLuong.ToString("N2");
                    detail.NgayHuongLuong = item.NgayHuongLuong != DateTime.MinValue ? item.NgayHuongLuong.ToString("d") : "";
                    detail.ThoiGianTapSu = item.ThoiGianTapSu.ToString("N0");
                    detail.NamSinh = item.ThongTinNhanVien.NgaySinh.Year.ToString();
                    detail.NgaySinhDate = item.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                    detail.QueQuan = item.ThongTinNhanVien.QueQuan.FullDiaChi;
                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nu)
                        detail.GioiTinh = "Nữ";
                    else
                        detail.GioiTinh = "Nam";

                    qd.Detail.Add(detail);
                    stt++;
                }

                list.Add(qd);
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhTuyenDungTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhTuyenDungTapTheDetail.rtf")); ;
            merge[0] = Common.GetTemplateWithValidDate(obs, "QuyetDinhTuyenDungTapThe.rtf", qdList[0].CongTy.Oid, qdList[0].NgayHieuLuc);
            if (merge[0] != null)
                Prosess_Show.ShowEditor<Non_QuyetDinhTuyenDungTapThe>(list, obs, merge);
            else
                XtraMessageBox.Show("Không tìm thấy mấu in QĐ tuyển dụng trong hệ thống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
