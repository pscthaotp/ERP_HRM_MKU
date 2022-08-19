using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.HopDong
{
    //ThuHuong sửa 
    public class Non_HopDongThinhGiang : Non_HopDong
    {
        [DisplayName("Mã số thuế")]
        public string MaSoThue { get; set; }

        [DisplayName("Tên tài khoản")]
        public string TenTaiKhoan { get; set; }

        [DisplayName("Số tài khoản")]
        public string SoTaiKhoan { get; set; }

        [DisplayName("Tên ngân hàng")]
        public string TenNganHang { get; set; }
        //
        [DisplayName("Lớp Học phần")]
        public string LopHocPhan { get; set; }

        [DisplayName("Số tín chỉ")]
        public string SoTinChi { get; set; }

        [DisplayName("Số tiết")]
        public string SoTiet { get; set; }
        [DisplayName("Số tiết LT")]
        public string SoTietLT { get; set; }
        [DisplayName("Số tiết TH")]
        public string SoTietTH { get; set; }

        [DisplayName("Số bài chấm")]
        public string SoBaiCham { get; set; }

        [DisplayName("Hướng dẫn đồ án tốt nghiệp")]
        public string HuongDanDoAnTotNghiep { get; set; }

        [DisplayName("Mã lớp SV")]
        public string MaLopSV { get; set; }

        [DisplayName("Tên lớp SV")]
        public string TenLopSV { get; set; }

        [DisplayName("Sỉ số")]
        public string SiSo { get; set; }

        [DisplayName("Thù lao giảng dạy")]
        public string ThuLaoGiangDay { get; set; }
        [DisplayName("Thù lao thực lãnh")]
        public string ThucLanhThuLao { get; set; }
        [DisplayName("Số lượt đi lại")]
        public string SoLuotDiLai { get; set; }
        [DisplayName("Đơn giá khoảng cách")]
        public string DonGiaKhoangCach { get; set; }
        [DisplayName("Chi phí tiền xe")]
        public string ChiPhiTienXe { get; set; }
        [DisplayName("Chi phí tiền ăn")]
        public string ChiPhiTienAn { get; set; }
        [DisplayName("Vị trí hiện tại")]
        public string ViTriHienTai { get; set; }
        [DisplayName("Ngoài Đà Lạt")]
        public string NgoaiDaLat { get; set; }
        [DisplayName("Trong Đà Lạt")]
        public string TrongDaLat { get; set; }
        [DisplayName("Lý thuyết")]
        public string LoaiHocPhan_LT { get; set; }
        [DisplayName("Thực hành")]
        public string LoaiHocPhan_TH { get; set; }
        //
        [DisplayName("Năm bắt đầu")]
        public string NamBatDau { get; set; }

    }
}
