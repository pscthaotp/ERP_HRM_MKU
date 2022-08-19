using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using System.Data;
using System.Data.SqlClient;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.DanhMuc.TienLuong;
using DevExpress.Persistent.Validation;
using ERP.Module.Enum.NhanSu;

namespace ERP.Module.NghiepVu.NhanSu.NhanViens
{
    [ImageName("BO_Resume")]
    [DefaultClassOptions, DefaultProperty("Caption")]
    [ModelDefault("Caption", "Thông tin lương")]
    public class NhanVienThongTinLuong : BaseObject
    {
        private PhanLoaiLuongEnum _PhanLoaiLuong;
        private bool _TinhLuong;
        private decimal _PhuCapDienThoai;
        private decimal _PhuCapTienAn;
        private decimal _PhuCapTienXang;
        private NgachLuong _NgachLuong;
        private BacLuong _BacLuong;
        private decimal _PhanTramTinhLuong;
        private DateTime _NgayBoNhiemNgach;
        private DateTime _NgayHuongLuong;
        private DateTime _MocNangLuongLanSau; 
        private string _LyDoDieuChinh;
        private DateTime _MocNangLuongDieuChinh;
        private decimal _LuongKhoan;
        private decimal _LuongCoBan;
        private decimal _LuongKinhDoanh;
        private decimal _LuongGop;
        private decimal _HieuQuaCongViec;
        private int _VuotKhung;
        private decimal _HSPCVuotKhung;
        private DateTime _NgayHuongVuotKhung;
        private decimal _ThamNien;
        private decimal _HSPCThamNien;
        private DateTime _NgayHuongThamNien;
        private decimal _HSPCChucVu;
        private DateTime _NgayHuongHSPCChucVu;
        private decimal _HSPCChucVuDang;
        private decimal _HSPCChucVuDoan;      
        private decimal _PhuCapKiemNhiem;     
        private decimal _PhuCapTrachNhiem;
        private decimal _HSPCKhac;
        //
        private decimal _PhuCapBanTru;
        private decimal _PhuCapNhaO;
        //Bảo hiểm
        private bool _KhongDongBHXH;
        private bool _KhongDongBHYT;
        private bool _KhongDongBHTN;
        private bool _KhongDongCongDoan;
        //Thuế TNCN
        private DateTime _NgayCapMST;
        private string _MaSoThue;
        private bool _KhongGiamTruBanThan;
        private int _SoNguoiPhuThuoc;
        private int _SoThangGiamTru;
        private bool _TinhThueMacDinh = true;
        private decimal _PhanTramTinhThue;        
        //Công chuẩn
        private bool _TinhCongChuanMacDinh = true;
        //

        [Browsable(false)]
        public string Caption
        {
            get
            {
                return ObjectFormatter.Format("Lương chức danh: {LuongCoBan:N0}", this);
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
        [ModelDefault("Caption", "Cấp bậc (*)")]
        //[RuleRequiredField(DefaultContexts.Save)]
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
        [ModelDefault("Caption", "Bậc lương (*)")]
        [DataSourceProperty("NgachLuong.ListBacLuong")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public BacLuong BacLuong
        {
            get
            {
                return _BacLuong;
            }
            set
            {
                SetPropertyValue("BacLuong", ref _BacLuong, value);
                if (!IsLoading)
                {
                    if (value != null)
                    {
                        LuongCoBan = value.LuongCoBan;
                        LuongKinhDoanh = value.LuongKinhDoanh;
                    }
                    else
                    {
                        LuongCoBan = 0;
                        LuongKinhDoanh = 0;
                    }
                }
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm ngạch")]
        public DateTime NgayBoNhiemNgach
        {
            get
            {
                return _NgayBoNhiemNgach;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemNgach", ref _NgayBoNhiemNgach, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHuongLuong
        {
            get
            {
                return _NgayHuongLuong;
            }
            set
            {
                SetPropertyValue("NgayHuongLuong", ref _NgayHuongLuong, value);
                if (!IsLoading)
                {
                    if (value != DateTime.MinValue)
                    {
                        //Chưa vượt khung
                        if (VuotKhung == 0)
                            MocNangLuongLanSau = NgayHuongLuong.AddMonths(NgachLuong != null ? NgachLuong.ThoiGianNangBac : 0);
                        //Vượt khung thi mỗi năm vượt khung một lần
                        else
                            MocNangLuongLanSau = NgayHuongLuong.AddMonths(12);
                    }
                }
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

        [NonPersistent]
        [ModelDefault("Caption", "Lương gộp")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal LuongGop
        {
            get
            {
                return LuongCoBan + LuongKinhDoanh;
            }
        }

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

        [ImmediatePostData]
        [ModelDefault("Caption", "Mốc nâng lương điều chỉnh")]
        public DateTime MocNangLuongDieuChinh
        {
            get
            {
                return _MocNangLuongDieuChinh;
            }
            set
            {
                SetPropertyValue("MocNangLuongDieuChinh", ref _MocNangLuongDieuChinh, value);
                if (!IsLoading)
                {
                    if (MocNangLuongDieuChinh != DateTime.MinValue
                        && MocNangLuongDieuChinh > NgayHuongLuong)
                    {
                        if (VuotKhung == 0)
                            MocNangLuongLanSau = MocNangLuongDieuChinh.AddMonths(NgachLuong.ThoiGianNangBac);
                        else
                            MocNangLuongLanSau = MocNangLuongDieuChinh.AddMonths(12);
                    }
                    else
                    {
                        //chưa vượt khung
                        if (VuotKhung == 0)
                            MocNangLuongLanSau = NgayHuongLuong.AddMonths(NgachLuong.ThoiGianNangBac);
                        //vượt khung thi mỗi năm vượt khung một lần
                        else
                            MocNangLuongLanSau = NgayHuongLuong.AddMonths(12);
                    }
                }
            }
        }

        [Size(2000)]
        [ModelDefault("Caption", "Lý do điều chỉnh")]
        public string LyDoDieuChinh
        {
            get
            {
                return _LyDoDieuChinh;
            }
            set
            {
                SetPropertyValue("LyDoDieuChinh", ref _LyDoDieuChinh, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương lần sau")]
        [ModelDefault("AllowEdit", "False")]
        public DateTime MocNangLuongLanSau
        {
            get
            {
                return _MocNangLuongLanSau;
            }
            set
            {
                SetPropertyValue("MocNangLuongLanSau", ref _MocNangLuongLanSau, value);
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

        [ModelDefault("Caption", "Ngày hưởng HSPCCV")]
        public DateTime NgayHuongHSPCChucVu
        {
            get
            {
                return _NgayHuongHSPCChucVu;
            }
            set
            {
                SetPropertyValue("NgayHuongHSPCChucVu", ref _NgayHuongHSPCChucVu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "% vượt khung")]
        public int VuotKhung
        {
            get
            {
                return _VuotKhung;
            }
            set
            {
                SetPropertyValue("VuotKhung", ref _VuotKhung, value);   
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

        [ModelDefault("Caption", "Ngày hưởng vượt khung")]
        public DateTime NgayHuongVuotKhung
        {
            get
            {
                return _NgayHuongVuotKhung;
            }
            set
            {
                SetPropertyValue("NgayHuongVuotKhung", ref _NgayHuongVuotKhung, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("Caption", "% thâm niên")]
        public decimal ThamNien
        {
            get
            {
                return _ThamNien;
            }
            set
            {
                SetPropertyValue("ThamNien", ref _ThamNien, value);  
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

        [ModelDefault("Caption", "Ngày hưởng thâm niên")]
        public DateTime NgayHuongThamNien
        {
            get
            {
                return _NgayHuongThamNien;
            }
            set
            {
                SetPropertyValue("NgayHuongThamNien", ref _NgayHuongThamNien, value);
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
        [ModelDefault("Caption", "PC chủ nhiệm")]
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

        [ModelDefault("Caption", "Phụ cấp tiền xăng")]
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

        [ModelDefault("Caption", "Phụ cấp bán trú")]
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

        [ModelDefault("Caption", "Phụ cấp nhà ở")]
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

        [ModelDefault("Caption", "Mã số thuế")]
        public string MaSoThue
        {
            get
            {
                return _MaSoThue;
            }
            set
            {
                SetPropertyValue("MaSoThue", ref _MaSoThue, value);
            }
        }

        [ModelDefault("Caption", "Ngày cấp MST")]
        public DateTime NgayCapMST
        {
            get
            {
                return _NgayCapMST;
            }
            set
            {
                SetPropertyValue("NgayCapMST", ref _NgayCapMST, value);
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
                if (!IsLoading)
                {
                    if (!value)
                    {
                        PhanTramTinhThue = 0;
                    }
                }
            }
        }

        [ModelDefault("Caption", "% tính thuế")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
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

        [ModelDefault("Caption", "Tính công chuẩn mặc định")]
        public bool TinhCongChuanMacDinh
        {
            get
            {
                return _TinhCongChuanMacDinh;
            }
            set
            {
                SetPropertyValue("TinhCongChuanMacDinh", ref _TinhCongChuanMacDinh, value);
            }
        }

        public NhanVienThongTinLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            PhanTramTinhLuong = 100;
            PhanLoaiLuong = PhanLoaiLuongEnum.LuongNgachBacGross;
            TinhLuong = true;
        }
    }
}
