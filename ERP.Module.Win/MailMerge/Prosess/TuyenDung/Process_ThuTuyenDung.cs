using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using ERP.Module.Commons;
using ERP.Module.Enum.NhanSu;
using ERP.Module.MailMerge;
using ERP.Module.MailMerge.NhanSu.QuyetDinh;
using ERP.Module.MailMerge.NhanSu.TuyenDung;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.Win.MailMerge.Prosess.ShowMaiMerge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ERP.Module.Win.MailMerge.Prosess.TuyenDung
{
    public static class Process_ThuTuyenDung
    {
        public static void ShowMailMerge(DevExpress.ExpressApp.IObjectSpace obs, List<TrungTuyen> trungTuyenList)
        {
            var non_ThuTuyenDungList = new List<Non_ThuTuyenDung>();
            Non_ThuTuyenDung non_ThuTuyenDung;
            foreach (TrungTuyen trungTuyen in trungTuyenList)
            {
                non_ThuTuyenDung = new Non_ThuTuyenDung();
                non_ThuTuyenDung.Oid = trungTuyen.Oid.ToString();
                // Thông tin Côngty/Trường
                non_ThuTuyenDung.TenCongTyVietHoa = trungTuyen.QuanLyTuyenDung.CongTy != null ? trungTuyen.QuanLyTuyenDung.CongTy.TenBoPhan.ToUpper() : "";
                non_ThuTuyenDung.TenCongTyVietThuong = trungTuyen.QuanLyTuyenDung.CongTy != null ? trungTuyen.QuanLyTuyenDung.CongTy.TenBoPhan : "";
                non_ThuTuyenDung.DiaChiCongTyHoacTruong = trungTuyen.QuanLyTuyenDung.CongTy.DiaChi != null ? trungTuyen.QuanLyTuyenDung.CongTy.DiaChi.FullDiaChi : "";
                non_ThuTuyenDung.TinhThanh = (trungTuyen.QuanLyTuyenDung.CongTy.DiaChi != null && trungTuyen.QuanLyTuyenDung.CongTy.DiaChi.TinhThanh != null) ? trungTuyen.QuanLyTuyenDung.CongTy.DiaChi.TinhThanh.TenTinhThanh : "";
                non_ThuTuyenDung.SoDienThoaiCongTyHoacTruong = trungTuyen.QuanLyTuyenDung.CongTy.DienThoai;
                non_ThuTuyenDung.SoFaxCongTyHoacTruong = trungTuyen.QuanLyTuyenDung.CongTy.Fax;
                non_ThuTuyenDung.EmailCongTyHoacTruong = trungTuyen.QuanLyTuyenDung.CongTy.Email;
                non_ThuTuyenDung.WebsiteCongTyHoacTruong = trungTuyen.QuanLyTuyenDung.CongTy.WebSite;
                non_ThuTuyenDung.NgayTrungTuyen = Common.GetServerCurrentTime().ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                non_ThuTuyenDung.NgayTrungTuyenDate = Common.GetServerCurrentTime().ToString("dd/MM/yyyy");
                non_ThuTuyenDung.NgayNhanViec = trungTuyen.NgayNhanViec != DateTime.MinValue ? trungTuyen.NgayNhanViec.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                non_ThuTuyenDung.NgayNhanViecDate = trungTuyen.NgayNhanViec != DateTime.MinValue ? trungTuyen.NgayNhanViec.ToString("dd/MM/yyyy") : "";
                non_ThuTuyenDung.ThoiGianThuViec = Common.GetDayNumberAddHoliday(trungTuyen.NgayNhanViec, trungTuyen.NgayBoNhiemNgach).ToString();
                non_ThuTuyenDung.LuongGop = trungTuyen.LuongGop.ToString("N0");
                non_ThuTuyenDung.LuongCoBan = trungTuyen.LuongCoBan.ToString("N0");
                non_ThuTuyenDung.LuongKinhDoanh = trungTuyen.LuongKinhDoanh.ToString("N0");
                non_ThuTuyenDung.PhanTramTinhLuong = trungTuyen.PhanTramTinhLuong.ToString("N0");
                non_ThuTuyenDung.ChucVuNguoiKy = trungTuyen.QuanLyTuyenDung.ChucVuNguoiKy != null ? trungTuyen.QuanLyTuyenDung.ChucVuNguoiKy.ChucDanh.TenChucDanh.ToUpper() : "";
                non_ThuTuyenDung.ChucVuNguoiKyVietThuong = trungTuyen.QuanLyTuyenDung.ChucVuNguoiKy != null ? trungTuyen.QuanLyTuyenDung.ChucVuNguoiKy.ChucDanh.TenChucDanh : "";
                if (trungTuyen.QuanLyTuyenDung.NguoiKy != null)
                {
                    non_ThuTuyenDung.TenNguoiKyVietHoa = trungTuyen.QuanLyTuyenDung.NguoiKy.HoTen != "" ? trungTuyen.QuanLyTuyenDung.NguoiKy.HoTen.ToUpper() : trungTuyen.QuanLyTuyenDung.TenNguoiKy.ToUpper();
                    non_ThuTuyenDung.TenNguoiKyVietThuong = trungTuyen.QuanLyTuyenDung.NguoiKy.HoTen != "" ? trungTuyen.QuanLyTuyenDung.NguoiKy.HoTen : trungTuyen.QuanLyTuyenDung.TenNguoiKy;
                    non_ThuTuyenDung.DanhXungNguoiKyVietHoa = trungTuyen.QuanLyTuyenDung.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    non_ThuTuyenDung.DanhXungNguoiKyVietThuong = trungTuyen.QuanLyTuyenDung.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                }

                if (trungTuyen.UngVien.NhuCauTuyenDung.BoPhan == null || trungTuyen.UngVien.NhuCauTuyenDung.BoPhan.CongTy.LoaiTruong == Enum.TuyenSinh_PT.LoaiTruongEnum.NA)
                {
                    non_ThuTuyenDung.BanTongGiamDocHoacBanGiamHieu = "Ban Tổng giám đốc";
                    non_ThuTuyenDung.DanhXungCongTyHoacTruong = "Công ty";
                }
                else
                {
                    non_ThuTuyenDung.BanTongGiamDocHoacBanGiamHieu = "Ban giám hiệu";
                    non_ThuTuyenDung.DanhXungCongTyHoacTruong = "Trường";
                }

                // Thông tin ứng viên
                non_ThuTuyenDung.DonViChuQuan = trungTuyen.UngVien.NhuCauTuyenDung != null ? trungTuyen.UngVien.NhuCauTuyenDung.BoPhan.TenBoPhan : string.Empty;
                non_ThuTuyenDung.HoTenVietHoa = trungTuyen.UngVien != null ? trungTuyen.UngVien.HoTen.ToUpper() : "";
                non_ThuTuyenDung.HoTenVietThuong = trungTuyen.UngVien != null ? trungTuyen.UngVien.HoTen : "";
                non_ThuTuyenDung.DanhXungVietHoa = trungTuyen.UngVien.GioiTinh == GioiTinhEnum.Nam ? "Anh" : "Chị";
                non_ThuTuyenDung.DanhXungVietThuong = trungTuyen.UngVien.GioiTinh == GioiTinhEnum.Nam ? "anh" : "chị";
                non_ThuTuyenDung.ViTriTrungTuyen = trungTuyen.UngVien.NhuCauTuyenDung.ViTriTuyenDung != null ? trungTuyen.UngVien.NhuCauTuyenDung.ViTriTuyenDung.TenViTriTuyenDung : "";
                non_ThuTuyenDung.DienThoaiDiDong = trungTuyen.UngVien != null ? trungTuyen.UngVien.DienThoaiDiDong : "";

                non_ThuTuyenDungList.Add(non_ThuTuyenDung);
            }
            if (trungTuyenList.Count > 0)
            {
                MailMergeTemplate merge = Common.GetTemplateWithValidDate(obs, "ThuTuyenDung.rtf", trungTuyenList[0].QuanLyTuyenDung.CongTy.Oid, trungTuyenList[0].NgayNhanViec);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_ThuTuyenDung>(non_ThuTuyenDungList, obs, merge);
                else
                    XtraMessageBox.Show("Không tìm thấy mấu in thư tuyển dụng trong hệ thống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
