using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.HopDongs;
using ERP.Module.Enum.NhanSu;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.HeThong;

namespace ERP.Module.NghiepVu.NhanSu.NhanViens
{
    [NonPersistent]
    [ImageName("BO_Extract")]
    [ModelDefault("Caption", "Thông tin cán bộ")]
    public class ChiTietTimKiemNhanVien : BaseObject, IBoPhan, INonpersistentController
    {
        private ThongTinNhanVien _NhanVien;
        //

        #region Thông tin hồ sơ
        [ModelDefault("Caption", "Mã Trường/Công ty")]
        public string MaCongTy
        {
            get
            {
                return NhanVien.BoPhan.CongTy.MaQuanLy;
            }
        }

        [ModelDefault("Caption", "Tên Trường/Công ty")]
        public BoPhan CongTy
        {
            get
            {
                return NhanVien.BoPhan.CongTy;
            }
        }


        [ModelDefault("Caption", "Mã đơn vị")]
        public string MaDonVi
        {
            get
            {
                return NhanVien.BoPhan.MaQuanLy;
            }
        }

        [ModelDefault("Caption", "Tên đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return NhanVien.BoPhan;
            }
        }

        [ModelDefault("Caption", "Mã quản lý")]
        public string MaQuanLy
        {
            get
            {
                return NhanVien.MaTapDoan;
            }
        }

        [ModelDefault("Caption", "Họ")]
        public string Ho { get; set; }

        [ModelDefault("Caption", "Tên")]
        public string Ten { get; set; }

        [ModelDefault("Caption", "Họ và tên")]
        public ThongTinNhanVien NhanVien
        {
            get
            {
                if (_NhanVien != null)
                {
                    Ho = _NhanVien.Ho;
                    Ten = _NhanVien.Ten;
                }
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (value != null)
                {
                    Ho = _NhanVien.Ho;
                    Ten = _NhanVien.Ten;
                }
            }
        }

        [ModelDefault("Caption", "Tên gọi khác")]
        public string TenGoiKhac
        {
            get
            {
                return NhanVien.TenGoiKhac;
            }
        }

        [ModelDefault("Caption", "Giới tính")]
        public GioiTinhEnum GioiTinh
        {
            get
            {
                return NhanVien.GioiTinh;
            }
        }

        [ModelDefault("Caption", "Ngày sinh")]
        public DateTime NgaySinh
        {
            get
            {
                return NhanVien.NgaySinh;
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Nơi sinh")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi NoiSinh
        {
            get
            {
                return NhanVien.NoiSinh;
            }
        }

        [ModelDefault("Caption", "Số CMND")]
        public string CMND
        {
            get
            {
                return NhanVien.CMND;
            }
        }

        [ModelDefault("Caption", "Ngày cấp")]
        public DateTime NgayCap
        {
            get
            {
                return NhanVien.NgayCap;
            }
        }

        [ModelDefault("Caption", "Nơi cấp")]
        public TinhThanh NoiCap
        {
            get
            {
                return NhanVien.NoiCap;
            }
        }

        [ModelDefault("Caption", "Số hộ chiếu")]
        public string SoHoChieu
        {
            get
            {
                return NhanVien.SoHoChieu;
            }
        }

        [ModelDefault("Caption", "Ngày cấp hộ chiếu")]
        public DateTime NgayCapHoChieu
        {
            get
            {
                return NhanVien.NgayCapHoChieu;
            }
        }

        [ModelDefault("Caption", "Nơi cấp hộ chiếu")]
        public string NoiCapHoChieu
        {
            get
            {
                return NhanVien.NoiCapHoChieu;
            }
        }

        [ModelDefault("Caption", "Ngày hết hạn")]
        public DateTime NgayHetHan
        {
            get
            {
                return NhanVien.NgayHetHan;
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Quê quán")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi QueQuan
        {
            get
            {
                return NhanVien.QueQuan;
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Địa chỉ thường trú")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi DiaChiThuongTru
        {
            get
            {
                return NhanVien.DiaChiThuongTru;
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Nơi ở hiện nay")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi NoiOHienNay
        {
            get
            {
                return NhanVien.NoiOHienNay;
            }
        }

        [ModelDefault("Caption", "Email cá nhân")]
        public string Email
        {
            get
            {
                return NhanVien.Email;
            }
        }

        [ModelDefault("Caption", "Email nội bộ")]
        public string EmailNoiBo
        {
            get
            {
                return NhanVien.EmailNoiBo;
            }
        }

        [ModelDefault("Caption", "Điện thoại nội bộ")]
        public string DienThoaiNoiBo
        {
            get
            {
                return NhanVien.DienThoaiNoiBo;
            }
        }

        [ModelDefault("Caption", "Điện thoại di động")]
        public string DienThoaiDiDong
        {
            get
            {
                return NhanVien.DienThoaiDiDong;
            }
        }

        [ModelDefault("Caption", "Điện thoại nhà riêng")]
        public string DienThoaiNhaRieng
        {
            get
            {
                return NhanVien.DienThoaiNhaRieng;
            }
        }

        [ModelDefault("Caption", "Tình trạng hôn nhân")]
        public TinhTrangHonNhan TinhTrangHonNhan
        {
            get
            {
                return NhanVien.TinhTrangHonNhan;
            }
        }

        [ModelDefault("Caption", "Dân tộc")]
        public DanToc DanToc
        {
            get
            {
                return NhanVien.DanToc;
            }
        }

        [ModelDefault("Caption", "Tôn giáo")]
        public TonGiao TonGiao
        {
            get
            {
                return NhanVien.TonGiao;
            }
        }

        [ModelDefault("Caption", "Quốc tịch")]
        public QuocGia QuocTich
        {
            get
            {
                return NhanVien.QuocTich;
            }
        }

        [ModelDefault("Caption", "Công việc hiện nay")]
        public CongViec CongViecHienNay
        {
            get
            {
                return NhanVien.CongViecHienNay;
            }
        }      

        [ModelDefault("Caption", "Tình trạng")]
        public TinhTrang TinhTrang
        {
            get
            {
                return NhanVien.TinhTrang;
            }
        }

        [ModelDefault("Caption", "Ngày nghỉ việc")]
        public DateTime NgayNghiViec
        {
            get
            {
                return NhanVien.NgayNghiViec;
            }
        }

        [ModelDefault("Caption", "Ngày nghỉ thai sản")]
        public DateTime NgayNghiThaiSan
        {
            get
            {
                return NhanVien.NgayNghiThaiSan;
            }
        }       

        [ModelDefault("Caption", "Đơn vị tuyển dụng")]
        public string DonViTuyenDung
        {
            get
            {
                return NhanVien.DonViTuyenDung;
            }
        }

        [ModelDefault("Caption", "Công việc trước tuyển dụng")]
        public string CongViecTruocTuyenDung
        {
            get
            {
                return NhanVien.CongViecTruocTuyenDung;
            }
        }

        [ModelDefault("Caption", "Ngày vào Trường")]
        public DateTime NgayVaoCongTy
        {
            get
            {
                return NhanVien.NgayVaoCongTy;
            }
        }

        [ModelDefault("Caption", "Khối tập đoàn")]
        public KhoiTapDoan KhoiTapDoan
        {
            get
            {
                return NhanVien.KhoiTapDoan;
            }
        }

        [ModelDefault("Caption", "Ngày vào tập đoàn")]
        public DateTime NgayVaoTapDoan
        {
            get
            {
                return NhanVien.NgayVaoTapDoan;
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        public ChucVu ChucVu
        {
            get
            {
                return NhanVien.ChucVu;
            }
        }

        [ModelDefault("Caption", "Nhóm chức vụ")]
        public NhomChucVu NhomChucVu
        {
            get
            {
                return NhanVien.ChucVu != null ? NhanVien.ChucVu.NhomChucVu : null;
            }
        }

        [ModelDefault("Caption", "Cấp bậc chức vụ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal CapBacChucVu
        {
            get
            {
                return NhanVien.ChucVu != null ? NhanVien.ChucVu.CapBac : 0;
            }
        }

        [ModelDefault("Caption", "Chức danh")]
        public ChucDanh ChucDanh
        {
            get
            {
                return NhanVien.ChucDanh;
            }
        }     

        [ModelDefault("Caption", "Loại nhân sự")]
        public LoaiNhanSu LoaiNhanSu
        {
            get
            {
                return NhanVien.LoaiNhanSu;
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm CV")]
        public DateTime NgayBoNhiemChucVu
        {
            get
            {
                return NhanVien.NgayBoNhiemChucVu;
            }
        }

        [ModelDefault("Caption", "Nhóm máu")]
        public NhomMau NhomMau
        {
            get
            {
                return NhanVien.NhomMau;
            }
        }

        [ModelDefault("Caption", "Chiều cao (Cm)")]
        public int ChieuCao
        {
            get
            {
                return NhanVien.ChieuCao;
            }
        }

        [ModelDefault("Caption", "Cân nặng (Kg)")]
        public int CanNang
        {
            get
            {
                return NhanVien.CanNang;
            }
        }

        [ModelDefault("Caption", "Sức khỏe")]
        public SucKhoe TSucKhoe
        {
            get
            {
                return NhanVien.SucKhoe;
            }
        }
       
        [ModelDefault("Caption", "Nhóm phân bổ")]
        public NhomPhanBo NhomPhanBo
        {
            get
            {
                return NhanVien.NhomPhanBo;
            }
        }
        #endregion 

        #region Thông tin trình độ
        [ModelDefault("Caption", "Trình độ văn hóa")]
        public TrinhDoVanHoa TrinhDoVanHoa
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.TrinhDoVanHoa;
            }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.TrinhDoChuyenMon;
            }
        }

        [ModelDefault("Caption", "Chuyên ngành đào tạo")]
        public ChuyenNganhDaoTao ChuyenNganhDaoTao
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao;
            }
        }

        [ModelDefault("Caption", "Trường đào tạo")]
        public TruongDaoTao TruongDaoTao
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.TruongDaoTao;
            }
        }

        [ModelDefault("Caption", "Hình thức đào tạo")]
        public HinhThucDaoTao HinhThucDaoTao
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.HinhThucDaoTao;
            }
        }

        [ModelDefault("Caption", "Năm tốt nghiệp")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamTotNghiep
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.NamTotNghiep;
            }
        }

        [ModelDefault("Caption", "Trình độ tin học")]
        public TrinhDoTinHoc TrinhDoTinHoc
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.TrinhDoTinHoc;
            }
        }

        [ModelDefault("Caption", "Ngoại ngữ chính")]
        public NgoaiNgu NgoaiNgu
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.NgoaiNgu;
            }
        }

        [ModelDefault("Caption", "Trình độ ngoại ngữ chính")]
        public TrinhDoNgoaiNgu TrinhDoNgoaiNgu
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu;
            }
        }
        #endregion

        #region Thông tin lương
        [ModelDefault("Caption", "Số người phụ thuộc")]
        public int SoNguoiPhuThuoc
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.SoNguoiPhuThuoc;
            }
        }

        [ModelDefault("Caption", "Số tháng giảm trừ")]
        public int SoThangGiamTru
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.SoThangGiamTru;
            }
        }

        [ModelDefault("Caption", "Mã số thuế")]
        public string MaSoThue
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.MaSoThue;
            }
        }

        [ModelDefault("Caption", "Mã ngạch lương")]
        public string MaNgachLuong
        {
            get
            {
                string MaNgachLuong = string.Empty;
                MaNgachLuong = NhanVien.NhanVienThongTinLuong.NgachLuong != null ? NhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy: string.Empty;
                return MaNgachLuong;
            }
        }

        [ModelDefault("Caption", "Tên ngạch lương")]
        public string TenNgachLuong
        {
            get
            {
                string TenNgachLuong = string.Empty;
                TenNgachLuong = NhanVien.NhanVienThongTinLuong.NgachLuong != null ? NhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : string.Empty;
                return MaNgachLuong;
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm ngạch")]
        public DateTime NgayBoNhiemNgach
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach;
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương")]
        public DateTime NgayHuongLuong
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.NgayHuongLuong;
            }
        }

        [ModelDefault("Caption", "Bậc lương")]
        public BacLuong BacLuong
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.BacLuong;
            }
        }

        [ModelDefault("Caption", "Lương chức danh")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LuongCoBan
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.LuongCoBan;
            }
        }

        [ModelDefault("Caption", "Lương bổ sung (HQCV)")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LuongKinhDoanh
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.LuongKinhDoanh;
            }
        }

        [ModelDefault("Caption", "Phụ cấp kiêm nhiệm")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal PhuCapKiemNhiem
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem;
            }
        }

        [ModelDefault("Caption", "Phụ cấp chủ nhiệm")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal PhuCapTrachNhiem
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.PhuCapTrachNhiem;
            }
        }

        [ModelDefault("Caption", "Phụ cấp bán trú")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal PhuCapBanTru
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.PhuCapBanTru;
            }
        }

        [ModelDefault("Caption", "Phụ cấp nhà ở")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal PhuCapNhaO
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.PhuCapNhaO;
            }
        }

        [ModelDefault("Caption", "Hiệu quả công việc")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal HieuQuaCongViec
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.HieuQuaCongViec;
            }
        }

        [ModelDefault("Caption", "Phụ cấp tiền ăn")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTienAn
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.PhuCapTienAn;
            }
        }

        [ModelDefault("Caption", "Phụ cấp điện thoại")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapDienThoai
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.PhuCapDienThoai;
            }
        }

        [ModelDefault("Caption", "Phụ cấp tiền xăng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTienXang
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.PhuCapDienThoai;
            }
        }

        private string _SoTaiKhoan;
        [ModelDefault("Caption", "Số tài khoản")]
        [ImmediatePostData()]
        public string SoTaiKhoan
        {
            get
            {
                foreach (TaiKhoanNganHang TK in NhanVien.ListTaiKhoanNganHang)
                {
                    if (TK.TaiKhoanChinh)
                    {
                        _SoTaiKhoan = TK.SoTaiKhoan;
                        _NganHang = TK.NganHang;
                        break;
                    }
                }
                return _SoTaiKhoan;
            }
        }

        private NganHang _NganHang;
        [ModelDefault("Caption", "Ngân hàng")]
        public NganHang NganHang
        {
            get
            {
                return _NganHang;
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return NhanVien.GhiChu;
            }
        }
        #endregion

        #region Thông tin hợp đồng
        [ModelDefault("Caption", "Loại hợp đồng")]
        public LoaiHopDong LoaiHopDong
        {
            get
            {
                return NhanVien.LoaiHopDong;
            }
        }

        [ModelDefault("Caption", "Hợp đồng hiện tại")]
        public HopDong HopDongHienTai
        {
            get
            {
                return NhanVien.HopDongHienTai;
            }
        }

        [ModelDefault("Caption", "Hình thức hợp đồng")]
        public HinhThucHopDong HinhThucHopDong
        {
            get
            {
                try
                {
                    HopDongLamViec hopDongld = Session.GetObjectByKey<HopDongLamViec>(NhanVien.HopDongHienTai.Oid);
                    if (hopDongld != null)
                        return hopDongld.HinhThucHopDong;
                    else
                    {
                        HopDongKhoan hopdongkhoan = Session.GetObjectByKey<HopDongKhoan>(NhanVien.HopDongHienTai.Oid);
                        if (hopdongkhoan != null)
                            return hopdongkhoan.HinhThucHopDong;
                        else
                            return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        [ModelDefault("Caption", "Ngày ký hợp đồng")]
        public DateTime NgayKy
        {
            get
            {
                try
                {
                    return NhanVien.HopDongHienTai.NgayKy;
                }
                catch (Exception ex)
                {
                    return DateTime.MinValue;
                }
            }
        }

        [ModelDefault("Caption", "Hợp đồng từ ngày")]
        public DateTime TuNgay
        {
            get
            {
                try
                {
                    HopDongLamViec hopDongld = Session.GetObjectByKey<HopDongLamViec>(NhanVien.HopDongHienTai.Oid);
                    if (hopDongld != null)
                        return hopDongld.TuNgay;
                    else
                    {
                        HopDongKhoan hopdongkhoan = Session.GetObjectByKey<HopDongKhoan>(NhanVien.HopDongHienTai.Oid);
                        if (hopdongkhoan != null)
                            return hopdongkhoan.TuNgay;
                        else
                            return DateTime.MinValue;
                    }
                }
                catch (Exception ex)
                {
                    return DateTime.MinValue;
                }
            }
        }

        [ModelDefault("Caption", "Hợp đồng đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                try
                {
                    HopDongLamViec hopDongld = Session.GetObjectByKey<HopDongLamViec>(NhanVien.HopDongHienTai.Oid);
                    if (hopDongld != null)
                        return hopDongld.DenNgay;
                    else
                    {
                        HopDongKhoan hopdongkhoan = Session.GetObjectByKey<HopDongKhoan>(NhanVien.HopDongHienTai.Oid);
                        if (hopdongkhoan != null)
                            return hopdongkhoan.DenNgay;
                        else
                            return DateTime.MinValue;
                    }
                }
                catch (Exception ex)
                {
                    return DateTime.MinValue;
                }
            }
        }
        #endregion   

        public ChiTietTimKiemNhanVien(Session session) : base(session) { }
    }
}
