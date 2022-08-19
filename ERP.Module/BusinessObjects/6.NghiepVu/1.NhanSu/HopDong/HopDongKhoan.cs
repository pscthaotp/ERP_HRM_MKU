using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.Text;
using System.Data.SqlClient;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.MaTuDong;
using ERP.Module.Enum.NhanSu;
//
namespace ERP.Module.NghiepVu.NhanSu.HopDongs
{
    [ImageName("BO_Contract")]
    [DefaultProperty("SoHopDong")]
    [ModelDefault("Caption", "Hợp đồng cộng tác viên")]
    [Appearance("HopDongLamViec.CoTrongHoSo", TargetItems = "ThongTinNhanVien;BoPhan", Visibility = ViewItemVisibility.Hide, Criteria = "KhongCoTrongHoSo")]
    //
    [Appearance("HopDongLamViec.KhongCoTrongHoSo", TargetItems = "HoTen;DiaChiThuongTru", Visibility = ViewItemVisibility.Hide, Criteria = "!KhongCoTrongHoSo")]
    public class HopDongKhoan : HopDong
    {
        //
        private LoaiHopDongKhoanEnum _LoaiHopDongKhoan = LoaiHopDongKhoanEnum.Gross;
        private HinhThucHopDong _HinhThucHopDong;
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        //Điều khoản hợp đồng
        private decimal _LuongKhoan;
        private DateTime _NgayHuongLuong;
        private decimal _PhuCapTienXang;
        private decimal _PhuCapTienAn;
        //
        private string _HoTen;
        private string _DiaChiThuongTru;

        //      
        [ModelDefault("Caption", "Loại khoán")]
        public LoaiHopDongKhoanEnum LoaiHopDongKhoan
        {
            get
            {
                return _LoaiHopDongKhoan;
            }
            set
            {
                SetPropertyValue("LoaiHopDongKhoan", ref _LoaiHopDongKhoan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Hình thức hợp đồng")]
        [DataSourceProperty("HTHDList")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "HinhThucHopDong.CoThoiHan")]
        public HinhThucHopDong HinhThucHopDong
        {
            get
            {
                return _HinhThucHopDong;
            }
            set
            {
                SetPropertyValue("HinhThucHopDong", ref _HinhThucHopDong, value);
                if (!IsLoading && value != null && TuNgay != DateTime.MinValue)
                    DenNgay = TuNgay.AddMonths(value.SoThang);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tên người lao động")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "KhongCoTrongHoSo")]
        public string HoTen
        {
            get
            {
                return _HoTen;
            }
            set
            {
                SetPropertyValue("HoTen", ref _HoTen, value);
            }
        }

        [ModelDefault("Caption", "Địa chỉ thường trú")]
        public string DiaChiThuongTru
        {
            get
            {
                return _DiaChiThuongTru;
            }
            set
            {
                SetPropertyValue("DiaChiThuongTru", ref _DiaChiThuongTru, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading && TuNgay != DateTime.MinValue)
                {
                    if(HinhThucHopDong != null)
                    DenNgay = TuNgay.AddMonths(HinhThucHopDong.SoThang);
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Lương khoán")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
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


        [ModelDefault("Caption", "Ngày hưởng lương")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHuongLuong
        {
            get
            {
                return _NgayHuongLuong;
            }
            set
            {
                SetPropertyValue("NgayHuongLuong", ref _NgayHuongLuong, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp tiền ăn")]
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

        [Browsable(false)]
        public XPCollection<HinhThucHopDong> HTHDList { get; set; }

        public HopDongKhoan(Session session) : base(session) { }
        
        protected override void TaoSoHopDong()
        {
            if (QuanLyHopDong != null)
            {
                SqlParameter param = new SqlParameter("@QuanLyHopDong", QuanLyHopDong.Oid);
                //
                SoHopDong = ManageKeyFactory.ManageKeyCompany(ManageKeyEnum.SoHopDongKhoan, QuanLyHopDong.CongTy, param);
            }
        }

        protected override void AfterNhanVienChanged()
        {
            if (ThongTinNhanVien != null)
            {
                //
                NgayHuongLuong = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong;
                LuongKhoan = ThongTinNhanVien.NhanVienThongTinLuong.LuongKhoan;
                LoaiHopDongLuuTru = ThongTinNhanVien.LoaiHopDong;
            }
        }

        protected override void AfterLoaiHopDongChanged()
        {
            if(HTHDList ==null)
               HTHDList = new XPCollection<HinhThucHopDong>(Session);
            //
            if(this.LoaiHopDong!=null)
                HTHDList.Criteria = CriteriaOperator.Parse("LoaiHopDong.Oid=?", this.LoaiHopDong.Oid);
            //
            HinhThucHopDong = null;
        }
    }

}
