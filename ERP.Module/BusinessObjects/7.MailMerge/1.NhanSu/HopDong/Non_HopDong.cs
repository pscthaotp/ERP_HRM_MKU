using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;

namespace ERP.Module.MailMerge.NhanSu.HopDong
{
    public class Non_HopDong : IMailMergeBase
    {
        [Browsable(false)]
        public string Oid { get; set; }
        //
        [DisplayName("Đơn vị chủ quản")]
        public string DonViChuQuan { get; set; }

        [DisplayName("Tên Trường/Công ty viết hoa")]
        public string TenCongTyVietHoa { get; set; }

        [DisplayName("Tên Trường/Công ty viết thường")]
        public string TenCongTyVietThuong { get; set; }

        [DisplayName("Địa chỉ Trường/Công ty")]
        public string DiaChi { get; set; }

        [DisplayName("Căn cứ")]
        public string CanCu { get; set; }

        [DisplayName("Số điện thoại Trường/Công ty")]
        public string SoDienThoai { get; set; }

        [DisplayName("Số Fax Trường")]
        public string SoFax { get; set; }

        [DisplayName("Loại hợp đồng")]
        public string LoaiHopDong { get; set; }

        [DisplayName("Số hợp đồng")]
        public string SoHopDong { get; set; }

        [DisplayName("Ngày ký hợp đồng")]
        public string NgayKy { get; set; }

        [DisplayName("Ngày ký hợp đồng (date)")]
        public string NgayKyDate { get; set; }

        [DisplayName("Ngày hiệu lực")]
        public string NgayHieuLuc { get; set; }

        [DisplayName("Ngày hiệu lực (date)")]
        public string NgayHieuLucDate { get; set; }

        //Người ký
        [DisplayName("Danh xưng người ký")]
        public string DanhXungNguoiKy { get; set; }

        [DisplayName("Tên người ký viết thường")]
        public string TenNguoiKyVietThuong { get; set; }

        [DisplayName("Tên người ký viết hoa")]
        public string TenNguoiKyVietHoa { get; set; }

        [DisplayName("Tên chức vụ người ký")]
        public string TenChucVuNguoiKy { get; set; }

        [DisplayName("Quốc tịch người ký")]
        public string QuocTichNguoiKy { get; set; }

        [DisplayName("Ngày sinh người ký")]
        public string NgaySinhNguoiKy { get; set; }

        [DisplayName("Địa chỉ người ký")]
        public string DiaChiNguoiKy { get; set; }

        [DisplayName("CNMD người ký")]
        public string CMNDNguoiKy { get; set; }

        [DisplayName("Ngày cấp người ký")]
        public string NgayCapNguoiKy { get; set; }

        [DisplayName("Nơi cấp người ký")]
        public string NoiCapNguoiKy { get; set; }


        //Người lao động
        [DisplayName("Cơ quan công tác")]
        public string CoQuanCongTac { get; set; }
        [DisplayName("Danh xưng NLĐ viết thường")]
        public string DanhXungNLDVietThuong { get; set; }

        [DisplayName("Danh xưng NLĐ viết hoa")]
        public string DanhXungNLDVietHoa { get; set; }

        [DisplayName("Tên NLĐ viết hoa")]
        public string TenNguoiLaoDongVietHoa { get; set; }

        [DisplayName("Tên NLĐ viết thường")]
        public string TenNguoiLaoDongVietThuong { get; set; }
        [DisplayName("Đơn vị NLĐ")]
        public string DonVi { get; set; }

        [DisplayName("Quốc tịch NLĐ")]
        public string QuocTich { get; set; }

        [DisplayName("Ngày sinh NLĐ")]
        public string NgaySinh { get; set; }

        [DisplayName("Ngày sinh NLĐ (date)")]
        public string NgaySinhDate { get; set; }
        [DisplayName("Giới tính")]
        public string GioiTinh { get; set; }

        [DisplayName("Nơi sinh NLĐ")]
        public string NoiSinh { get; set; }

        [DisplayName("Quê quán NLĐ")]
        public string QueQuan { get; set; }

        [DisplayName("Học hàm NLĐ")]
        public string HocHam { get; set; }

        [DisplayName("Học vị NLĐ")]
        public string TrinhDoChuyenMon { get; set; }

        [DisplayName("Chuyên ngành đào tạo NLĐ")]
        public string ChuyenNganhDaoTao { get; set; }

        [DisplayName("Năm tốt nghiệp NLĐ")]
        public string NamTotNghiep { get; set; }

        [DisplayName("Tên chức vụ NLD")]
        public string TenChucVuNguoiNLD { get; set; }

        [DisplayName("ĐTDĐ NLĐ")]
        public string DienThoaiDiDong { get; set; }

        [DisplayName("ĐT nhà riêng NLĐ")]
        public string DienThoaiNhaRieng { get; set; }

        [DisplayName("Địa chỉ thường trú NLĐ")]
        public string DiaChiThuongTru { get; set; }

        [DisplayName("Nơi ở hiện nay NLĐ")]
        public string NoiOHienNay { get; set; }

        [DisplayName("CMND NLĐ")]
        public string CMND { get; set; }

        [DisplayName("Ngày cấp NLĐ")]
        public string NgayCap { get; set; }

        [DisplayName("Ngày cấp NLĐ (date)")]
        public string NgayCapDate { get; set; }

        [DisplayName("Nơi cấp NLĐ")]
        public string NoiCap { get; set; }

        [DisplayName("Địa điểm làm việc NLĐ")]
        public string DiaDiemLamViec { get; set; }

        [DisplayName("Email NLĐ")]
        public string Email { get; set; }

        [DisplayName("Chức danh NLĐ")]
        public string ChucDanh { get; set; }

        [DisplayName("Năm học")]
        public string NamHoc { get; set; }

        //
        public IList Master { get; set; }
        public IList Master1 { get; set; }
        public IList Detail { get; set; }
        public IList Detail1 { get; set; }
    }
}
