using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.TienLuong.Thue
{
    [DefaultClassOptions]
    [ImageName("BO_HoaDon")]
    [ModelDefault("Caption", "Chi tiết thuế TNCN")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietThueThuNhapCaNhan.Unique", DefaultContexts.Save, "QuanLyThueThuNhapCaNhan;BoPhan;ThongTinNhanVien")]
    public class ChiTietThueThuNhapCaNhan : BaseObject,IBoPhan
    {
        private QuanLyThueThuNhapCaNhan _QuanLyThueThuNhapCaNhan;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private string _CostCenter;
        private decimal _TongThuNhap;
        private decimal _TongBaoHiem;
        private decimal _ThuNhapChiuThue;
        private decimal _GiamTruBanThan;
        private decimal _GiamTruGiaCanh;
        private decimal _ThuNhapTinhThue;
        private decimal _ThueTNCN;

        [Browsable(false)]
        [Association("QuanLyThueThuNhapCaNhan-ChiTietThueThuNhapCaNhanList")]
        public QuanLyThueThuNhapCaNhan QuanLyThueThuNhapCaNhan
        {
            get
            {
                return _QuanLyThueThuNhapCaNhan;
            }
            set
            {
                SetPropertyValue("QuanLyThueThuNhapCaNhan", ref _QuanLyThueThuNhapCaNhan, value);
            }
        }

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
                if (!IsLoading)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [DataSourceProperty("NVList", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Mã phân bổ")]
        public string CostCenter
        {
            get
            {
                return _CostCenter;
            }
            set
            {
                SetPropertyValue("CostCenter", ref _CostCenter, value);
            }
        }

        [ModelDefault("Caption", "Tổng thu nhập")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TongThuNhap
        {
            get
            {
                return _TongThuNhap;
            }
            set
            {
                SetPropertyValue("TongThuNhap", ref _TongThuNhap, value);
            }
        }

        [ModelDefault("Caption", "Tổng tiền bảo hiểm")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TongBaoHiem
        {
            get
            {
                return _TongBaoHiem;
            }
            set
            {
                SetPropertyValue("TongBaoHiem", ref _TongBaoHiem, value);
            }
        }

        [ModelDefault("Caption", "Thu nhập chịu thuế")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThuNhapChiuThue
        {
            get
            {
                return _ThuNhapChiuThue;
            }
            set
            {
                SetPropertyValue("ThuNhapChiuThue", ref _ThuNhapChiuThue, value);
            }
        }

        [ModelDefault("Caption", "Giảm trừ bản thân")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal GiamTruBanThan
        {
            get
            {
                return _GiamTruBanThan;
            }
            set
            {
                SetPropertyValue("GiamTruBanThan", ref _GiamTruBanThan, value);
            }
        }

        [ModelDefault("Caption", "Giảm trừ gia cảnh")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal GiamTruGiaCanh
        {
            get
            {
                return _GiamTruGiaCanh;
            }
            set
            {
                SetPropertyValue("GiamTruGiaCanh", ref _GiamTruGiaCanh, value);
            }
        }

        [ModelDefault("Caption", "Thu nhập tính thuế")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThuNhapTinhThue
        {
            get
            {
                return _ThuNhapTinhThue;
            }
            set
            {
                SetPropertyValue("ThuNhapTinhThue", ref _ThuNhapTinhThue, value);
            }
        }

        [ModelDefault("Caption", "Thuế TNCN")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThueTNCN
        {
            get
            {
                return _ThueTNCN;
            }
            set
            {
                SetPropertyValue("ThueTNCN", ref _ThueTNCN, value);
            }
        }

        public ChiTietThueThuNhapCaNhan(Session session) : base(session) { }

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
        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateNhanVienList();
        }  
    }
}
