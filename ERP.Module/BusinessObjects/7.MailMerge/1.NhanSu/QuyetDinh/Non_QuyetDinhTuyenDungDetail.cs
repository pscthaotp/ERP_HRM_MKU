using System;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.QuyetDinh
{
    public class Non_QuyetDinhTuyenDungDetail : Non_MergeItem
    {
        [DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Giới tính")]
        public string GioiTinh { get; set; }
        [DisplayName("Năm sinh")]
        public string NamSinh { get; set; }
        [DisplayName("Ngày sinh (date)")]
        public string NgaySinhDate { get; set; }
        [DisplayName("Quê quán")]
        public string QueQuan { get; set; }
        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }


        [DisplayName("Nhóm ngạch lương")]
        public string NhomNgachLuong { get; set; }
        [DisplayName("Mã ngạch")]
        public string MaNgach { get; set; }
        [DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }
        [DisplayName("Bậc lương")]
        public string BacLuong { get; set; }
        [DisplayName("Hệ số lương")]
        public string HeSoLuong { get; set; }
        [DisplayName("Hưởng 85% mức lương")]
        public string Huong85PhanTramMucLuong { get; set; }
        [DisplayName("Ngày hưởng lương")]
        public string NgayHuongLuong { get; set; }
        [DisplayName("Thời gian tập sự")]
        public string ThoiGianTapSu { get; set; }
        
    }
}
