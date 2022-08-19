using DevExpress.XtraEditors;
using ERP.Module.Commons;
using ERP.Module.Enum.NhanSu;
using ERP.Module.MailMerge;
using ERP.Module.MailMerge.NhanSu.TuyenDung;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.Win.MailMerge.Prosess.ShowMaiMerge;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ERP.Module.Win.MailMerge.Prosess.TuyenDung
{
    public static class Process_ThuCamOn
    {
        public static void ShowMailMerge(DevExpress.ExpressApp.IObjectSpace obs, List<KhongTrungTuyen> khongTrungTuyenList)
        {
            var non_UngVienList = new List<Non_UngVien>();
            Non_UngVien non_UngVien;
            foreach (KhongTrungTuyen khongTrungTuyen in khongTrungTuyenList)
            {
                non_UngVien = new Non_UngVien();
                non_UngVien.Oid = khongTrungTuyen.Oid.ToString();
                non_UngVien.DonViChuQuan = khongTrungTuyen.QuanLyTuyenDung.CongTy != null ? khongTrungTuyen.QuanLyTuyenDung.CongTy.DonViChuQuan : string.Empty;
                non_UngVien.TenCongTyVietHoa = khongTrungTuyen.QuanLyTuyenDung.CongTy != null ? khongTrungTuyen.QuanLyTuyenDung.CongTy.TenBoPhan.ToUpper() : "";
                non_UngVien.TenCongTyVietThuong = khongTrungTuyen.QuanLyTuyenDung.CongTy != null ? khongTrungTuyen.QuanLyTuyenDung.CongTy.TenBoPhan : "";
                non_UngVien.DiaChiCongTyHoacTruong = khongTrungTuyen.QuanLyTuyenDung.CongTy.DiaChi != null ? khongTrungTuyen.QuanLyTuyenDung.CongTy.DiaChi.FullDiaChi : "";
                non_UngVien.TinhThanh = (khongTrungTuyen.QuanLyTuyenDung.CongTy.DiaChi != null && khongTrungTuyen.QuanLyTuyenDung.CongTy.DiaChi.TinhThanh != null) ? khongTrungTuyen.QuanLyTuyenDung.CongTy.DiaChi.TinhThanh.TenTinhThanh : "";
                non_UngVien.SoDienThoaiCongTyHoacTruong = khongTrungTuyen.QuanLyTuyenDung.CongTy.DienThoai;
                non_UngVien.SoFaxCongTyHoacTruong = khongTrungTuyen.QuanLyTuyenDung.CongTy.Fax;
                non_UngVien.EmailCongTyHoacTruong = khongTrungTuyen.QuanLyTuyenDung.CongTy.Email;
                non_UngVien.WebsiteCongTyHoacTruong = khongTrungTuyen.QuanLyTuyenDung.CongTy.WebSite;
                non_UngVien.NgayThongBaoKhongTrungTuyen = Common.GetServerCurrentTime().ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                non_UngVien.DanhXungVietThuong = khongTrungTuyen.UngVien.GioiTinh == GioiTinhEnum.Nam ? "anh" : "chị";
                non_UngVien.DanhXungVietHoa = khongTrungTuyen.UngVien.GioiTinh == GioiTinhEnum.Nam ? "Anh" : "Chị";
                non_UngVien.HoTenVietHoa = khongTrungTuyen.UngVien.HoTen != null ? khongTrungTuyen.UngVien.HoTen.ToUpper() : "";
                non_UngVien.HoTenVietThuong = khongTrungTuyen.UngVien.HoTen != null ? khongTrungTuyen.UngVien.HoTen : "";

                non_UngVienList.Add(non_UngVien);
            }
            if (khongTrungTuyenList.Count > 0)
            {
                MailMergeTemplate merge = Common.GetTemplate(obs, "ThuCamOn.rtf", khongTrungTuyenList[0].QuanLyTuyenDung.CongTy.Oid);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_UngVien>(non_UngVienList, obs, merge);
                else
                    XtraMessageBox.Show("Không tìm thấy mấu in thư cảm ơn trong hệ thống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
