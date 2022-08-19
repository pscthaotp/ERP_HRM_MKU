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
    [ModelDefault("Caption", "Thông tin thỉnh giảng")]
    public class ChiTietTimKiemThinhGiang : BaseObject, IBoPhan, INonpersistentController
    {
        private GiangVienThinhGiang _ThinhGiang;

        //0
        [ModelDefault("Caption", "Mã đơn vị")]
        public string MaDonVi
        {
            get
            {
                return ThinhGiang.BoPhan.MaQuanLy;
            }
        }

        //1
        [ModelDefault("Caption", "Tên đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return ThinhGiang.BoPhan;
            }
        }

        //1
        [ModelDefault("Caption", "Tại khoa/bộ môn")]
        public BoPhan TaiBoMon
        {
            get
            {
                return ThinhGiang.TaiBoMon;
            }
        }

        //2
        [ModelDefault("Caption", "Mã nhân viên")]
        public string MaNhanVien
        {
            get
            {
                return ThinhGiang.MaNhanVien;
            }
        }

        //3
        [ModelDefault("Caption", "Họ và tên")]
        public GiangVienThinhGiang ThinhGiang
        {
            get
            {
                return _ThinhGiang;
            }
            set
            {
                SetPropertyValue("ThinhGiang", ref _ThinhGiang, value);
            }
        }

        //10
        [ModelDefault("Caption", "Tên gọi khác")]
        public string TenGoiKhac
        {
            get
            {
                return ThinhGiang.TenGoiKhac;
            }
        }

        //5
        [ModelDefault("Caption", "Giới tính")]
        public GioiTinhEnum GioiTinh
        {
            get
            {
                return ThinhGiang.GioiTinh;
            }
        }

        //6
        [ModelDefault("Caption", "Ngày sinh")]
        public DateTime NgaySinh
        {
            get
            {
                return ThinhGiang.NgaySinh;
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
                return ThinhGiang.NoiSinh;
            }
        }

        [ModelDefault("Caption", "Số CMND")]
        public string CMND
        {
            get
            {
                return ThinhGiang.CMND;
            }
        }

        [ModelDefault("Caption", "Ngày cấp")]
        public DateTime NgayCap
        {
            get
            {
                return ThinhGiang.NgayCap;
            }
        }

        [ModelDefault("Caption", "Nơi cấp")]
        public TinhThanh NoiCap
        {
            get
            {
                return ThinhGiang.NoiCap;
            }
        }

        [ModelDefault("Caption", "Số hộ chiếu")]
        public string SoHoChieu
        {
            get
            {
                return ThinhGiang.SoHoChieu;
            }
        }

        [ModelDefault("Caption", "Ngày cấp hộ chiếu")]
        public DateTime NgayCapHoChieu
        {
            get
            {
                return ThinhGiang.NgayCapHoChieu;
            }
        }

        [ModelDefault("Caption", "Nơi cấp hộ chiếu")]
        public string NoiCapHoChieu
        {
            get
            {
                return ThinhGiang.NoiCapHoChieu;
            }
        }

        [ModelDefault("Caption", "Ngày hết hạn")]
        public DateTime NgayHetHan
        {
            get
            {
                return ThinhGiang.NgayHetHan;
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
                return ThinhGiang.QueQuan;
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
                return ThinhGiang.DiaChiThuongTru;
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
                return ThinhGiang.NoiOHienNay;
            }
        }

        [ModelDefault("Caption", "Email")]
        public string Email
        {
            get
            {
                return ThinhGiang.Email;
            }
        }

        [ModelDefault("Caption", "Điện thoại di động")]
        public string DienThoaiDiDong
        {
            get
            {
                return ThinhGiang.DienThoaiDiDong;
            }
        }

        [ModelDefault("Caption", "Điện thoại nhà riêng")]
        public string DienThoaiNhaRieng
        {
            get
            {
                return ThinhGiang.DienThoaiNhaRieng;
            }
        }

        [ModelDefault("Caption", "Tình trạng hôn nhân")]
        public TinhTrangHonNhan TinhTrangHonNhan
        {
            get
            {
                return ThinhGiang.TinhTrangHonNhan;
            }
        }

        [ModelDefault("Caption", "Dân tộc")]
        public DanToc DanToc
        {
            get
            {
                return ThinhGiang.DanToc;
            }
        }

        [ModelDefault("Caption", "Tôn giáo")]
        public TonGiao TonGiao
        {
            get
            {
                return ThinhGiang.TonGiao;
            }
        }

        [ModelDefault("Caption", "Quốc tịch")]
        public QuocGia QuocTich
        {
            get
            {
                return ThinhGiang.QuocTich;
            }
        }              

        [ModelDefault("Caption", "Công việc hiện nay")]
        public CongViec CongViecHienNay
        {
            get
            {
                return ThinhGiang.CongViecHienNay;
            }
        }

        [ModelDefault("Caption", "Hợp đồng hiện tại")]
        public HopDong HopDongHienTai
        {
            get
            {
                return ThinhGiang.HopDongHienTai;
            }
        }      

        [ModelDefault("Caption", "Hình thức tuyển dụng")]
        public HinhThucTuyenDungEnum HinhThucTuyenDung
        {
            get
            {
                return ThinhGiang.HinhThucTuyenDung;
            }
        }

        [ModelDefault("Caption", "Ngày tuyển dụng")]
        public DateTime NgayTuyenDung
        {
            get
            {
                return ThinhGiang.NgayTuyenDung;
            }
        }

        [ModelDefault("Caption", "Đơn vị tuyển dụng")]
        public string DonViTuyenDung
        {
            get
            {
                return ThinhGiang.DonViTuyenDung;
            }
        }   
        
        [ModelDefault("Caption", "Ngày vào trường")]
        public DateTime NgayVaoCoQuan
        {
            get
            {
                return ThinhGiang.NgayVaoCongTy;
            }
        }

        [ModelDefault("Caption", "Đơn vị công tác")]
        public string DonViCongTac
        {
            get
            {
                return ThinhGiang.DonViCongTac;
            }
        }

        [ModelDefault("Caption", "Tài liệu giảng dạy")]
        public string TaiLieuGiangDay
        {
            get
            {
                return ThinhGiang.TaiLieuGiangDay;
            }
        }

        //37
        private string _SoTaiKhoan;
        [ModelDefault("Caption", "Số tài khoản")]
        [ImmediatePostData()]
        public string SoTaiKhoan
        {
            get
            {
                foreach (TaiKhoanNganHang TK in ThinhGiang.ListTaiKhoanNganHang)
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

        //38
        private NganHang _NganHang;
        [ModelDefault("Caption", "Ngân hàng mở tài khoản")]
        public NganHang NganHang
        {
            get
            {
                return _NganHang;
            }
        }


        [ModelDefault("Caption", "Trình độ văn hóa")]
        public TrinhDoVanHoa TrinhDoVanHoa
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.TrinhDoVanHoa;
            }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.TrinhDoChuyenMon;
            }
        }

        [ModelDefault("Caption", "Chuyên ngành đào tạo")]
        public ChuyenNganhDaoTao ChuyenNganhDaoTao
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.ChuyenNganhDaoTao;
            }
        }

        [ModelDefault("Caption", "Trường đào tạo")]
        public TruongDaoTao TruongDaoTao
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.TruongDaoTao;
            }
        }

        [ModelDefault("Caption", "Hình thức đào tạo")]
        public HinhThucDaoTao HinhThucDaoTao
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.HinhThucDaoTao;
            }
        }

        [ModelDefault("Caption", "Năm tốt nghiệp")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamTotNghiep
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.NamTotNghiep;
            }
        }      

        [ModelDefault("Caption", "Trình độ tin học")]
        public TrinhDoTinHoc TrinhDoTinHoc
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.TrinhDoTinHoc;
            }
        }

        [ModelDefault("Caption", "Ngoại ngữ chính")]
        public NgoaiNgu NgoaiNgu
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.NgoaiNgu;
            }
        }

        [ModelDefault("Caption", "Trình độ ngoại ngữ chính")]
        public TrinhDoNgoaiNgu TrinhDoNgoaiNgu
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.TrinhDoNgoaiNgu;
            }
        }

        [ModelDefault("Caption", "Học hàm")]
        public HocHam HocHam
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.HocHam;
            }
        }
     
        [ModelDefault("Caption", "Mã số thuế")]
        public string MaSoThue
        {
            get
            {
                return ThinhGiang.NhanVienThongTinLuong.MaSoThue;
            }
        }        

        [ModelDefault("Caption", "Tình trạng")]
        public TinhTrang TinhTrang
        {
            get
            {
                return ThinhGiang.TinhTrang;
            }
        }

        //106
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return ThinhGiang.GhiChu;
            }
        }

        public ChiTietTimKiemThinhGiang(Session session) : base(session) { }
    }
}
