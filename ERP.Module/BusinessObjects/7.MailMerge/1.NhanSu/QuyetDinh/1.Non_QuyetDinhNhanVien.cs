using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhNhanVien : Non_QuyetDinh
    {
        //HoSo
        [DisplayName("Mã nhân viên")]
        public string MaNhanVien { get; set; }

        [DisplayName("Nhân viên viết thường")]
        public string HoTenVietThuong { get; set; }

        [DisplayName("Nhân viên viết hoa")]
        public string HoTenVietHoa { get; set; }

        [DisplayName("Danh xưng NV viết thường")]
        public string DanhXungVietThuong { get; set; }

        [DisplayName("Danh xưng NV viết hoa")]
        public string DanhXungVietHoa { get; set; }

        [DisplayName("Số CMND")]
        public string SoCMND { get; set; }

        [DisplayName("Ngày cấp CMND")]
        public string NgayCapCMND { get; set; }

        [DisplayName("Nơi cấp CMND")]
        public string NoiCapCMND { get; set; }

        [DisplayName("Ngày sinh")]
        public string NgaySinh { get; set; }

        [DisplayName("Ngày sinh (Date)")]
        public string NgaySinhDate { get; set; }
        
        [DisplayName("Nơi sinh")]
        public string NoiSinh { get; set; }

        [DisplayName("Giới tính")]
        public string GioiTinh { get; set; }

        [DisplayName("Quốc tịch")]
        public string QuocTich { get; set; }

        [DisplayName("Địa chỉ thường trú")]
        public string DiaChiThuongTru { get; set; }

        [DisplayName("Địa chỉ liên lạc")]
        public string DiaChiLienLac { get; set; }

        [DisplayName("Số điện thoại")]
        public string DienThoaiDiDong { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        //NhanVien

        [DisplayName("Công việc hiện tại")]
        public string CongViecHienTai { get; set; }

        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }

        [DisplayName("Chức vụ")]
        public string ChucVu { get; set; }

        [DisplayName("Chức danh")]
        public string ChucDanh { get; set; }

        [DisplayName("Loại hợp đồng")]
        public string LoaiHopDong { get; set; }

        [DisplayName("Hợp đồng lao động số")]
        public string HopDongLaoDongSo { get; set; }

        [DisplayName("Ngày ký hợp đồng")]
        public string NgayKyHopDong { get; set; }

        [DisplayName("Loại nhân sự")]
        public string LoaiNhanSu { get; set; }

        [DisplayName("Ngày vào Trường")]
        public string NgayVaoCoQuan { get; set; }

        //NhanVienThongTinLuong

        [DisplayName("Mã ngạch lương")]
        public string MaNgachLuong { get; set; }

        [DisplayName("Tên ngạch lương")]
        public string TenNgachLuong { get; set; }

        [DisplayName("Bậc lương")]
        public string BacLuong { get; set; }

        [DisplayName("Lương chức danh")]
        public string LuongCoBan { get; set; }

        [DisplayName("Lương bổ sung(HQCV)")]
        public string LuongKinhDoanh { get; set; }

        [DisplayName("Pc trách nhiệm ")]
        public string PCTrachNhiem { get; set; }

        [DisplayName("Pc kiêm nhiệm")]
        public string PCKiemNhiem { get; set; }

        [DisplayName("Mốc nâng lương")]
        public string MocNangLuong { get; set; }

        [DisplayName("Ngày hưởng lương")]
        public string NgayHuongLuong { get; set; }

        [DisplayName("Ngày hưởng lương")]
        public string NgayHuongLuongDate { get; set; }

        //NhanVienTrinhDo
        [DisplayName("Trình độ văn hóa")]
        public string TrinhDoVanHoa { get; set; }

        [DisplayName("Trình độ chuyên môn")]
        public string TrinhDoChuyenMon { get; set; }

        [DisplayName("Chuyên ngành đào tạo")]
        public string ChuyenNganhDaoTao { get; set; }

        [DisplayName("Trường đào tạo")]
        public string TruongDaoTao { get; set; }

        [DisplayName("Năm tốt nghiệp")]
        public string NamTotNghiep { get; set; }

        [DisplayName("Ngày hiện tại")]
        public string NgayHienTai { get; set; }

        [DisplayName("Ngày hiện tại (Date)")]
        public string NgayHienTaiDate { get; set; }
    }
}
