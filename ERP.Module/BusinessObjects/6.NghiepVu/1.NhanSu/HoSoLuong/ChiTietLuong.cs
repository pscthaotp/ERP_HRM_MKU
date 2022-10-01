using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.Commons;
using ERP.Module.Enum.NhanSu;
//
namespace ERP.Module.NghiepVu.NhanSu.HoSoLuong
{
    [DefaultClassOptions]
    [ImageName("BO_TroCap")]
    [ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Chi tiết - Hồ sơ tính lương")]
    [DefaultProperty("ThongTinNhanVien")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietLuong.Unique", DefaultContexts.Save, "HoSoTinhLuong;BoPhan;ThongTinNhanVien")]
    [Appearance("ChiTietLuong.Khoa", TargetItems = "*", Enabled = false, Criteria = "HoSoTinhLuong.KhoaSo")]    
    [Appearance("Hide_TinhThueMacDinh", TargetItems = "PhanTramTinhThue", Visibility = ViewItemVisibility.Hide, Criteria = "TinhThueMacDinh")]
    [Appearance("Hide_DaThayDoiLuongOrPhongBan", TargetItems = "NgayHieuLuc", Visibility = ViewItemVisibility.Hide, Criteria = "!QuyetDinhThayDoiLuong")]
    public class ChiTietLuong : BaseObject,IBoPhan
    {
        //
        private HoSoTinhLuong _HoSoTinhLuong;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private TinhTrang _TinhTrang;
        private ChucDanh _ChucDanh;
        private LoaiHopDong _LoaiHopDong;
        private PhanLoaiNhanSu _PhanLoaiNhanSu;
        private ChucVu _ChucVu;
        private DateTime _NgayVaoCongTy;
        private bool _TinhLuong;
        private PhanLoaiLuongEnum _PhanLoaiLuong;
        private NgachLuong _NgachLuong;
        private BacLuong _BacLuong;
        private decimal _PhanTramTinhLuong;
        private decimal _HeSoLuong;
        private decimal _MucLuongDongBHXH;
        private decimal _LuongCoBan;
        private decimal _LuongKinhDoanh;
        private decimal _LuongKhoan;
        private decimal _HieuQuaCongViec;
        private decimal _TienBHXH;
        private decimal _MucHoTroBHXH;
        private decimal _PhuCapHocVi;
        private decimal _HSPCChucVu;
        private decimal _HSPCVuotKhung;
        private decimal _HSPCThamNien;
        private decimal _HSPCChucVuDang;
        private decimal _HSPCChucVuDoan;
        private decimal _PhuCapKiemNhiem;
        private decimal _PhuCapTrachNhiem;
        private decimal _HSPCKhac;
        private decimal _HSTroCap;
        private decimal _HSDieuChinh;
        private decimal _PhuCapDienThoai;
        private decimal _PhuCapTienAn;
        private decimal _PhuCapTienXang;
        private decimal _PhuCapChucVu;
        private decimal _SoGioLamViec;
        private bool _KhongDongBHXH;
        private bool _KhongDongBHYT;
        private bool _KhongDongBHTN;
        private bool _KhongDongCongDoan;
        private bool _KhongGiamTruBanThan;
        private int _SoNguoiPhuThuoc;
        private int _SoThangGiamTru;
        private bool _TinhThueMacDinh = true;
        private decimal _PhanTramTinhThue;
        private string _GhiChu;
        //
        private int _SoThangLamViec;
        private decimal _PhuCapBanTru;
        private decimal _PhuCapNhaO;
        //Thông tin lữu trữ khi thay đổi lương
        private DateTime _NgayHieuLuc;
        private bool _QuyetDinhThayDoiLuong;
        //Phục vụ phân bổ
        private To _To;
        private NhomPhanBo _NhomPhanBo;
        //Import thuế TNCN -> Import lương nhân viên như thế nào thì xuất chứng từ như thế đó (ẩn cột)
        private decimal _ThuNhapChiuThueKhongTienNha2;
        private decimal _ThuNhapChiuThue2;
        private decimal _ThuNhapTinhThue2;
        private decimal _ThueTNCN2;
        private decimal _ThucNhan2;

        [Browsable(false)]
        [ModelDefault("Caption", "Hồ sơ lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("HoSoTinhLuong-ListChiTietLuong")]
        public HoSoTinhLuong HoSoTinhLuong
        {
            get
            {
                return _HoSoTinhLuong;
            }
            set
            {
                SetPropertyValue("HoSoTinhLuong", ref _HoSoTinhLuong, value);
            }
        }

        #region 1. Thông tin hồ sơ
        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    //
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                {
                    //
                    UpdateChiTietLuongCuaNhanVien(value);
                }
            }
        }

        [ModelDefault("Caption", "Tình trạng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public TinhTrang TinhTrang
        {
            get
            {
                return _TinhTrang;
            }
            set
            {
                SetPropertyValue("TinhTrang", ref _TinhTrang, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        public ChucVu ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
            }
        }

        [ModelDefault("Caption", "Chức danh")]
        public ChucDanh ChucDanh
        {
            get
            {
                return _ChucDanh;
            }
            set
            {
                SetPropertyValue("ChucDanh", ref _ChucDanh, value);
            }
        }

        [ModelDefault("Caption", "Loại hợp đồng")]
        public LoaiHopDong LoaiHopDong
        {
            get
            {
                return _LoaiHopDong;
            }
            set
            {
                SetPropertyValue("LoaiHopDong", ref _LoaiHopDong, value);
            }
        }
        [ModelDefault("Caption", "Phân loại nhân sự")]
        public PhanLoaiNhanSu PhanLoaiNhanSu
        {
            get
            {
                return _PhanLoaiNhanSu;
            }
            set
            {
                SetPropertyValue("PhanLoaiNhanSu", ref _PhanLoaiNhanSu, value);
            }
        }

        [ModelDefault("Caption", "Ngày vào công ty")]
        public DateTime NgayVaoCongTy
        {
            get
            {
                return _NgayVaoCongTy;
            }
            set
            {
                SetPropertyValue("NgayVaoCongTy", ref _NgayVaoCongTy, value);
            }
        }
        [ModelDefault("Caption", "Số tháng làm việc")]
        public int SoThangLamViec
        {
            get
            {
                return _SoThangLamViec;
            }
            set
            {
                SetPropertyValue("SoThangLamViec", ref _SoThangLamViec, value);
            }
        }

        [ModelDefault("Caption", "Tổ")]        
        public To To
        {
            get
            {
                return _To;
            }
            set
            {
                SetPropertyValue("To", ref _To, value);
            }
        }

        [ModelDefault("Caption", "Nhóm phân bổ")]        
        public NhomPhanBo NhomPhanBo
        {
            get
            {
                return _NhomPhanBo;
            }
            set
            {
                SetPropertyValue("NhomPhanBo", ref _NhomPhanBo, value);
            }
        }
        #endregion

        #region 2. Thông tin lương
        [ImmediatePostData]
        [ModelDefault("Caption", "Tính lương")]
        public bool TinhLuong
        {
            get
            {
                return _TinhLuong;
            }
            set
            {
                SetPropertyValue("TinhLuong", ref _TinhLuong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại")]
        public PhanLoaiLuongEnum PhanLoaiLuong
        {
            get
            {
                return _PhanLoaiLuong;
            }
            set
            {
                SetPropertyValue("PhanLoaiLuong", ref _PhanLoaiLuong, value);
                if (!IsLoading)
                {
                    if (value == PhanLoaiLuongEnum.LuongNgachBacGross || value == PhanLoaiLuongEnum.LuongNgachBacNet)
                    {
                        LuongKhoan = 0;
                    }
                    else
                    {
                        LuongCoBan = 0;
                        LuongKinhDoanh = 0;
                    }
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cấp bậc")]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
                if (!IsLoading)
                {
                    BacLuong = null;
                    //
                }
            }
        }

        [ImmediatePostData()]
        [ModelDefault("Caption", "Bậc lương")]
        [DataSourceProperty("NgachLuong.ListBacLuong")]
        public BacLuong BacLuong
        {
            get
            {
                return _BacLuong;
            }
            set
            {
                SetPropertyValue("BacLuong", ref _BacLuong, value);
                if (!IsLoading && value != null)
                {
                    LuongCoBan = value.LuongCoBan;
                    LuongKinhDoanh = value.LuongKinhDoanh;
                }
            }
        }

        [ModelDefault("Caption", "% tính lương")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal PhanTramTinhLuong
        {
            get
            {
                return _PhanTramTinhLuong;
            }
            set
            {
                SetPropertyValue("PhanTramTinhLuong", ref _PhanTramTinhLuong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoLuong
        {
            get
            {
                return _HeSoLuong;
            }
            set
            {
                SetPropertyValue("HeSoLuong", ref _HeSoLuong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Mức lương đóng BHXH")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal MucLuongDongBHXH
        {
            get
            {
                return _MucLuongDongBHXH;
            }
            set
            {
                SetPropertyValue("MucLuongDongBHXH", ref _MucLuongDongBHXH, value);
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Lương chức danh")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal LuongCoBan
        {
            get
            {
                return _LuongCoBan;
            }
            set
            {
                SetPropertyValue("LuongCoBan", ref _LuongCoBan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Lương bổ sung (HQCV)")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal LuongKinhDoanh
        {
            get
            {
                return _LuongKinhDoanh;
            }
            set
            {
                SetPropertyValue("LuongKinhDoanh", ref _LuongKinhDoanh, value);
            }
        }

        [ModelDefault("Caption", "Hiệu quả công việc")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal HieuQuaCongViec
        {
            get
            {
                return _HieuQuaCongViec;
            }
            set
            {
                SetPropertyValue("HieuQuaCongViec", ref _HieuQuaCongViec, value);
            }
        }
        [ModelDefault("Caption", "Phụ cấp BHXH")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TienBHXH
        {
            get
            {
                return _TienBHXH;
            }
            set
            {
                SetPropertyValue("TienBHXH", ref _TienBHXH, value);
            }
        }
        [ModelDefault("Caption", "Mức hỗ trợ BHXH")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal MucHoTroBHXH
        {
            get
            {
                return _MucHoTroBHXH;
            }
            set
            {
                SetPropertyValue("MucHoTroBHXH", ref _MucHoTroBHXH, value);
            }
        }
        [ModelDefault("Caption", "Phụ cấp học vị")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal PhuCapHocVi
        {
            get
            {
                return _PhuCapHocVi;
            }
            set
            {
                SetPropertyValue("PhuCapHocVi", ref _PhuCapHocVi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Lương khoán")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal LuongKhoan
        {
            get
            {
                return _LuongKhoan;
            }
            set
            {
                SetPropertyValue("LuongKhoan", ref _LuongKhoan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "HSPC chức vụ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChucVu
        {
            get
            {
                return _HSPCChucVu;
            }
            set
            {
                SetPropertyValue("HSPCChucVu", ref _HSPCChucVu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "HSPC vượt khung")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSPCVuotKhung
        {
            get
            {
                return _HSPCVuotKhung;
            }
            set
            {
                SetPropertyValue("HSPCVuotKhung", ref _HSPCVuotKhung, value);
            }
        }

        [ModelDefault("Caption", "HSPC thâm niên")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCThamNien
        {
            get
            {
                return _HSPCThamNien;
            }
            set
            {
                SetPropertyValue("HSPCThamNien", ref _HSPCThamNien, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "HSPC chức vụ Đảng")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChucVuDang
        {
            get
            {
                return _HSPCChucVuDang;
            }
            set
            {
                SetPropertyValue("HSPCChucVuDang", ref _HSPCChucVuDang, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "HSPC chức vụ Đoàn")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChucVuDoan
        {
            get
            {
                return _HSPCChucVuDoan;
            }
            set
            {
                SetPropertyValue("HSPCChucVuDoan", ref _HSPCChucVuDoan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "PC kiêm nhiệm")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapKiemNhiem
        {
            get
            {
                return _PhuCapKiemNhiem;
            }
            set
            {
                SetPropertyValue("PhuCapKiemNhiem", ref _PhuCapKiemNhiem, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "PC trách nhiệm")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTrachNhiem
        {
            get
            {
                return _PhuCapTrachNhiem;
            }
            set
            {
                SetPropertyValue("PhuCapTrachNhiem", ref _PhuCapTrachNhiem, value);
            }
        }

        [ModelDefault("Caption", "HSPC khác")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSPCKhac
        {
            get
            {
                return _HSPCKhac;
            }
            set
            {
                SetPropertyValue("HSPCKhac", ref _HSPCKhac, value);
            }
        }
        [ModelDefault("Caption", "Hệ số trợ cấp")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSTroCap
        {
            get
            {
                return _HSTroCap;
            }
            set
            {
                SetPropertyValue("HSTroCap", ref _HSTroCap, value);
            }
        }
        [ModelDefault("Caption", "Hệ số điều chỉnh trợ cấp")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSDieuChinh
        {
            get
            {
                return _HSDieuChinh;
            }
            set
            {
                SetPropertyValue("HSDieuChinh", ref _HSDieuChinh, value);
            }
        }
        [ModelDefault("Caption", "Không đóng BHXH")]
        public bool KhongDongBHXH
        {
            get
            {
                return _KhongDongBHXH;
            }
            set
            {
                SetPropertyValue("KhongDongBHXH", ref _KhongDongBHXH, value);
            }
        }

        [ModelDefault("Caption", "Không đóng BHYT")]
        public bool KhongDongBHYT
        {
            get
            {
                return _KhongDongBHYT;
            }
            set
            {
                SetPropertyValue("KhongDongBHYT", ref _KhongDongBHYT, value);
            }
        }

        [ModelDefault("Caption", "Không đóng BHTN")]
        public bool KhongDongBHTN
        {
            get
            {
                return _KhongDongBHTN;
            }
            set
            {
                SetPropertyValue("KhongDongBHTN", ref _KhongDongBHTN, value);
            }
        }

        [ModelDefault("Caption", "Không đóng công đoàn")]
        public bool KhongDongCongDoan
        {
            get
            {
                return _KhongDongCongDoan;
            }
            set
            {
                SetPropertyValue("KhongDongCongDoan", ref _KhongDongCongDoan, value);
            }
        }

        [ModelDefault("Caption", "PC điện thoại")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapDienThoai
        {
            get
            {
                return _PhuCapDienThoai;
            }
            set
            {
                SetPropertyValue("PhuCapDienThoai", ref _PhuCapDienThoai, value);
            }
        }

        [ModelDefault("Caption", "PC tiền ăn")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTienAn
        {
            get
            {
                return _PhuCapTienAn;
            }
            set
            {
                SetPropertyValue("PhuCapTienAn", ref _PhuCapTienAn, value);
            }
        }

        [ModelDefault("Caption", "PC tiền xăng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTienXang
        {
            get
            {
                return _PhuCapTienXang;
            }
            set
            {
                SetPropertyValue("PhuCapTienXang", ref _PhuCapTienXang, value);
            }
        }
        [ModelDefault("Caption", "PC chức vụ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapChucVu
        {
            get
            {
                return _PhuCapChucVu;
            }
            set
            {
                SetPropertyValue("PhuCapChucVu", ref _PhuCapChucVu, value);
            }
        }

        [ModelDefault("Caption", "Số giờ làm việc")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal SoGioLamViec
        {
            get
            {
                return _SoGioLamViec;
            }
            set
            {
                SetPropertyValue("SoGioLamViec", ref _SoGioLamViec, value);
            }
        }

        [ModelDefault("Caption", "Không giảm trừ bản thân")]
        public bool KhongGiamTruBanThan
        {
            get
            {
                return _KhongGiamTruBanThan;
            }
            set
            {
                SetPropertyValue("KhongGiamTruBanThan", ref _KhongGiamTruBanThan, value);
            }
        }

        [ModelDefault("Caption", "Số người phụ thuộc")]
        public int SoNguoiPhuThuoc
        {
            get
            {
                return _SoNguoiPhuThuoc;
            }
            set
            {
                SetPropertyValue("SoNguoiPhuThuoc", ref _SoNguoiPhuThuoc, value);
                if (!IsLoading)
                {
                    SoThangGiamTru = SoNguoiPhuThuoc * 12;
                }
            }
        }

        [ModelDefault("Caption", "Số tháng giảm trừ")]
        public int SoThangGiamTru
        {
            get
            {
                return _SoThangGiamTru;
            }
            set
            {
                SetPropertyValue("SoThangGiamTru", ref _SoThangGiamTru, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tính thuế mặc định")]
        public bool TinhThueMacDinh
        {
            get
            {
                return _TinhThueMacDinh;
            }
            set
            {
                SetPropertyValue("TinhThueMacDinh", ref _TinhThueMacDinh, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "% tính thuế")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [Appearance("HidePhanTramThue", TargetItems = "PhanTramTinhThue", Visibility = ViewItemVisibility.Hide, Criteria = "TinhThueMacDinh")]
        public decimal PhanTramTinhThue
        {
            get
            {
                return _PhanTramTinhThue;
            }
            set
            {
                SetPropertyValue("PhanTramTinhLuong", ref _PhanTramTinhThue, value);
            }
        }

        [ModelDefault("Caption", "PC bán trú")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapBanTru
        {
            get
            {
                return _PhuCapBanTru;
            }
            set
            {
                SetPropertyValue("PhuCapBanTru", ref _PhuCapBanTru, value);
            }
        }

        [ModelDefault("Caption", "PC nhà ở")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapNhaO
        {
            get
            {
                return _PhuCapNhaO;
            }
            set
            {
                SetPropertyValue("PhuCapNhaO", ref _PhuCapNhaO, value);
            }
        }
        #endregion

        #region 3. Lưu trữ để tính mức lương cũ
        [ModelDefault("Caption", "Ghi chú")]
        [Size(1000)]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        //
        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày quyết định")]
        public DateTime NgayHieuLuc
        {
            get
            {
                return _NgayHieuLuc;
            }
            set
            {
                SetPropertyValue("NgayHieuLuc", ref _NgayHieuLuc, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đã thay đổi lương")]
        public bool QuyetDinhThayDoiLuong
        {
            get
            {
                return _QuyetDinhThayDoiLuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhThayDoiLuong", ref _QuyetDinhThayDoiLuong, value);
            }
        }
        #endregion

        //begin import thuế
        [Browsable(false)]
        [ModelDefault("Caption", "Thu nhập chịu thuế (chưa bao gồm tiền nhà)")]
        public decimal ThuNhapChiuThueKhongTienNha2
        {
            get
            {
                return _ThuNhapChiuThueKhongTienNha2;
            }
            set
            {
                SetPropertyValue("ThuNhapChiuThueKhongTienNha", ref _ThuNhapChiuThueKhongTienNha2, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Thu nhập chịu thuế (Bao gồm tiền nhà)")]
        public decimal ThuNhapChiuThue2
        {
            get
            {
                return _ThuNhapChiuThue2;
            }
            set
            {
                SetPropertyValue("ThuNhapChiuThue", ref _ThuNhapChiuThue2, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Thu nhập tính thuế")]
        public decimal ThuNhapTinhThue2
        {
            get
            {
                return _ThuNhapTinhThue2;
            }
            set
            {
                SetPropertyValue("ThuNhapTinhThue", ref _ThuNhapTinhThue2, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Thuế TNCN")]
        public decimal ThueTNCN2
        {
            get
            {
                return _ThueTNCN2;
            }
            set
            {
                SetPropertyValue("ThueTNCN", ref _ThueTNCN2, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Thực lãnh của CBGVNV")]
        public decimal ThucNhan2
        {
            get
            {
                return _ThucNhan2;
            }
            set
            {
                SetPropertyValue("ThucNhan", ref _ThucNhan2, value);
            }
        }
        //end import thuế

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        public ChiTietLuong(Session session) : base(session) { }

        /// <summary>
        /// Cập nhật danh sách nhân viên
        /// </summary>
        private void UpdateNhanVienList()
        {
            //
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            //
            if (BoPhan == null)
                NVList.Criteria = new InOperator("Oid", Common.NhanVien_DanhSachNhanVienDuocPhanQuyen());
            else
                NVList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }

        /// <summary>
        /// Cập nhật thông tin lương từng nhân viên
        /// </summary>
        private void UpdateChiTietLuongCuaNhanVien(ThongTinNhanVien value)
        {
            BoPhan = value.BoPhan != null && value.BoPhan != BoPhan ? value.BoPhan : null;
            TinhLuong = value.NhanVienThongTinLuong.TinhLuong;
            PhanLoaiLuong = value.NhanVienThongTinLuong.PhanLoaiLuong;
            NgachLuong = value.NhanVienThongTinLuong.NgachLuong != null ? value.NhanVienThongTinLuong.NgachLuong : null;
            BacLuong = value.NhanVienThongTinLuong.BacLuong != null ? value.NhanVienThongTinLuong.BacLuong : null;
            NgayVaoCongTy = value.NgayVaoCongTy;
            SoThangLamViec = value.SoThangLamViec;
            LoaiHopDong = value.LoaiHopDong;
            PhanLoaiNhanSu = value.PhanLoaiNhanSu;
            ChucVu = value.ChucVu;
            ChucDanh = value.ChucDanh;
            PhanTramTinhLuong = value.NhanVienThongTinLuong.PhanTramTinhLuong;
            MucLuongDongBHXH = value.NhanVienThongTinLuong.MucLuongDongBHXH;
            LuongCoBan = value.NhanVienThongTinLuong.LuongCoBan;
            LuongKinhDoanh = value.NhanVienThongTinLuong.LuongKinhDoanh;
            LuongKhoan = value.NhanVienThongTinLuong.LuongKhoan;
            HeSoLuong = value.NhanVienThongTinLuong.HeSoLuong;
            HSPCChucVu = value.NhanVienThongTinLuong.HSPCChucVu;
            HSPCThamNien = value.NhanVienThongTinLuong.HSPCThamNien;
            HSPCVuotKhung = value.NhanVienThongTinLuong.HSPCVuotKhung;
            HSPCChucVuDoan = value.NhanVienThongTinLuong.HSPCChucVuDoan;
            HSPCChucVuDang = value.NhanVienThongTinLuong.HSPCChucVuDang;
            PhuCapKiemNhiem = value.NhanVienThongTinLuong.PhuCapKiemNhiem;
            PhuCapTrachNhiem = value.NhanVienThongTinLuong.PhuCapTrachNhiem;
            PhuCapChucVu = value.NhanVienThongTinLuong.PhuCapChucVu;
            HSPCKhac = value.NhanVienThongTinLuong.HSPCKhac;
            HSTroCap = value.NhanVienThongTinLuong.HSTroCap;
            HSDieuChinh = value.NhanVienThongTinLuong.HSDieuChinh;
            PhuCapDienThoai = value.NhanVienThongTinLuong.PhuCapDienThoai;
            PhuCapTienAn = value.NhanVienThongTinLuong.PhuCapTienAn;
            PhuCapTienXang = value.NhanVienThongTinLuong.PhuCapTienXang;
            KhongDongBHXH = value.NhanVienThongTinLuong.KhongDongBHXH;
            KhongDongBHYT = value.NhanVienThongTinLuong.KhongDongBHYT;
            KhongDongBHTN = value.NhanVienThongTinLuong.KhongDongBHTN;
            KhongDongCongDoan = value.NhanVienThongTinLuong.KhongDongCongDoan;
            KhongGiamTruBanThan = value.NhanVienThongTinLuong.KhongGiamTruBanThan;
            SoNguoiPhuThuoc = value.NhanVienThongTinLuong.SoNguoiPhuThuoc;
            SoThangGiamTru = value.NhanVienThongTinLuong.SoThangGiamTru;
            TinhThueMacDinh = value.NhanVienThongTinLuong.TinhThueMacDinh;
            PhanTramTinhThue = value.NhanVienThongTinLuong.PhanTramTinhThue;
            SoGioLamViec = value.LoaiGioLamViec != null ? value.LoaiGioLamViec.SoGio : 0;
            To = value.To;
            NhomPhanBo = value.NhomPhanBo;
            HieuQuaCongViec = value.NhanVienThongTinLuong.HieuQuaCongViec;
            PhuCapHocVi = value.NhanVienThongTinLuong.PhuCapHocVi;
            TienBHXH = value.NhanVienThongTinLuong.TienBHXH;
            MucHoTroBHXH = value.NhanVienThongTinLuong.MucHoTroBHXH;
        }
    }

}
