using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.DanhMuc.TienLuong;
//
namespace ERP.Module.NghiepVu.NhanSu.DieuKien
{
    [NonPersistent]
    [ModelDefault("Caption", "Thông tin lương")]
    public class DieuKien_ThongTinLuong : BaseObject
    {
        [ModelDefault("Caption", "Số tháng làm việc")]
        public int SoThangLamViec { get; set; }
        [ModelDefault("Caption", "Nhóm Cấp bậc")]
        public NhomNgachLuong NhomNgachLuong { get; set; }

        [ModelDefault("Caption", "Cấp bậc")]
        public NgachLuong NgachLuong { get; set; }

        [ModelDefault("Caption", "Ngày bổ nhiệm ngạch")]
        public DateTime NgayBoNhiemNgach { get; set; }

        [ModelDefault("Caption", "Ngày hưởng lương")]
        public DateTime NgayHuongLuong { get; set; }

        [ModelDefault("Caption", "Bậc lương")]
        public BacLuong BacLuong { get; set; }

        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("DisplayFormat","N2")]
        [ModelDefault("EditMask","N2")]
        public Double HeSoLuong { get; set; }

        [ModelDefault("Caption", "Lương chức danh")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public Double LuongCoBan { get; set; }

        [ModelDefault("Caption", "Lương bổ sung (HQCV)")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public Double LuongKinhDoanh { get; set; }

        [ModelDefault("Caption", "Lương khoán")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public Double LuongKhoan { get; set; }

        [ModelDefault("Caption", "% vượt khung")]
        public int VuotKhung { get; set; }

        [ModelDefault("Caption", "HSPC vượt khung")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCVuotKhung { get; set; }

        [ModelDefault("Caption", "Mốc nâng lương")]
        public DateTime MocNangLuong { get; set; }

        [ModelDefault("Caption", "Mốc nâng lương ĐC")]
        public DateTime MocNangLuongDieuChinh { get; set; }

        [ModelDefault("Caption", "Tính lương")]
        public bool TinhLuong { get; set; }

        [ModelDefault("Caption", "Không đóng công đoàn")]
        public bool KhongThamGiaCongDoan { get; set; }

        [ModelDefault("Caption", "Không đóng BHXH")]
        public bool KhongDongBHXH { get; set; }

        [ModelDefault("Caption", "Không đóng BHYT")]
        public bool KhongDongBHYT { get; set; }

        [ModelDefault("Caption", "Không đóng BHTN")]
        public bool KhongDongBHTN { get; set; }

        [ModelDefault("Caption", "HSPC chức vụ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCChucVu { get; set; }

        [ModelDefault("Caption", "Ngày hưởng PCCV")]
        public DateTime NgayHuongHSPCChucVu { get; set; }

        [ModelDefault("Caption", "HSPC khác")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCKhac { get; set; }

        [ModelDefault("Caption", "% Thâm niên")]
        public int ThamNien { get; set; }

        [ModelDefault("Caption", "HSPC Thâm niên")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCThamNien { get; set; }

        [ModelDefault("Caption", "Ngày hưởng thâm niên")]
        public DateTime NgayHuongThamNien { get; set; }

        [ModelDefault("Caption", "HSPC Trách nhiệm")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCTrachNhiem { get; set; }

        [ModelDefault("Caption", "HSPC Kiêm nhiệm")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCKiemNhiem { get; set; }

        [ModelDefault("Caption", "HSPC Đảng")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCChucVuDang { get; set; }

        [ModelDefault("Caption", "HSPC Đoàn")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public Double HSPCChucVuDoan { get; set; }

        [ModelDefault("Caption", "Phụ cấp tiền ăn")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public Double PhuCapTienAn { get; set; }

        [ModelDefault("Caption", "Phụ cấp điện thoại")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public Double PhuCapDienThoai { get; set; }

        [ModelDefault("Caption", "Phụ cấp tiền xăng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public Double PhuCapTienXang { get; set; }

        [ModelDefault("Caption", "Mã số thuế")]
        public string MaSoThue { get; set; }

        [ModelDefault("Caption", "Số người phụ thuộc")]
        public int SoNguoiPhuThuoc { get; set; }

        [ModelDefault("Caption", "Số tháng giảm trừ")]
        public int SoThangGiamTru { get; set; }


        public DieuKien_ThongTinLuong(Session session) : base(session) { }
    }

}
