using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;

namespace ERP.Module.NghiepVu.TienLuong.Thuong
{
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết thưởng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "BangThuongNhanVien;ThongTinNhanVien")]
    public class ChiTietThuongNhanVien : BaseObject, IBoPhan
    {
        private BangThuongNhanVien _BangThuongNhanVien;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private DateTime _NgayLap;
        private decimal _SoTien;
        private decimal _SoTienChiuThue;
        private string _GhiChu;
        private DateTime _NgayThuong;
        private string _CongThucTinhSoTienNhan;
        private string _CongThucTinhThueTNCN;
        private HoSoTinhLuong _HoSoTinhLuong;
        private string _CongThucTinhBangChu;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng thu nhập khác")]
        [Association("BangThuongNhanVien-ListChiTietThuongNhanVien")]
        public BangThuongNhanVien BangThuongNhanVien
        {
            get
            {
                return _BangThuongNhanVien;
            }
            set
            {
                SetPropertyValue("BangThuongNhanVien", ref _BangThuongNhanVien, value);
            }
        }

        //Chỉ dùng để lập công thức
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Kỳ tính lương")]
        public KyTinhLuong KyTinhLuong { get; set; }

        //Chỉ dùng để lập công thức
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Hồ sơ lương")]
        public NhanVienThongTinLuong HoSoLuong { get; set; }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading && value != null)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField("", DefaultContexts.Save)]
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
                    if (value.BoPhan != BoPhan)
                        BoPhan = value.BoPhan;
                }
            }
        }
        [ModelDefault("Caption", "Ngày lập")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
            }
        }

        [ModelDefault("Caption", "Ngày thưởng")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgayThuong
        {
            get
            {
                return _NgayThuong;
            }
            set
            {
                SetPropertyValue("NgayThuong", ref _NgayThuong, value);
            }
        }

        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal SoTien
        {
            get
            {
                return _SoTien;
            }
            set
            {
                SetPropertyValue("SoTien", ref _SoTien, value);
            }
        }

        [ModelDefault("Caption", "Số tiền chịu thuế")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal SoTienChiuThue
        {
            get
            {
                return _SoTienChiuThue;
            }
            set
            {
                SetPropertyValue("SoTienChiuThue", ref _SoTienChiuThue, value);
            }
        }

        [Size(-1)]
        [Browsable(false)]
        [ModelDefault("Caption", "Công thức tính số tiền nhận")]
        public string CongThucTinhSoTienNhan
        {
            get
            {
                return _CongThucTinhSoTienNhan;
            }
            set
            {
                SetPropertyValue("CongThucTinhSoTienNhan", ref _CongThucTinhSoTienNhan, value);
            }
        }

        [Size(-1)]
        [Browsable(false)]
        [ModelDefault("Caption", "Công thức tính bằng chữ")]
        public string CongThucTinhBangChu
        {
            get
            {
                return _CongThucTinhBangChu;
            }
            set
            {
                SetPropertyValue("CongThucTinhBangChu", ref _CongThucTinhBangChu, value);
            }
        }

        [Size(-1)]
        [Browsable(false)]
        [ModelDefault("Caption", "Công thức tính TNCT")]
        public string CongThucTinhThueTNCN
        {
            get
            {
                return _CongThucTinhThueTNCN;
            }
            set
            {
                SetPropertyValue("CongThucTinhThueTNCN", ref _CongThucTinhThueTNCN, value);
            }
        }

        [Browsable(false)]
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



        [Size(500)]
        [ModelDefault("Caption", "Ghi chú")]
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

        public ChiTietThuongNhanVien(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

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

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNhanVienList();
        }
        //

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateNhanVienList();
        }
    }

}
