using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;

namespace ERP.Module.DanhMuc.TienLuong
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Cấp bậc")]
    [RuleCombinationOfPropertiesIsUnique("NgachLuong.Unique", DefaultContexts.Save, "MaQuanLy;CongTy")]
    public class NgachLuong : BaseObject
    {
        private BacLuong _TotKhung;
        private NhomNgachLuong _NhomNgachLuong;
        private string _MaQuanLy;
        private string _TenNgachLuong;
        private int _ThoiGianNangBac;
        private decimal _CapBac;
        private CongTy _CongTy;

        [Browsable(false)]
        [ModelDefault("Caption", "Nhóm Cấp bậc")]
        public NhomNgachLuong NhomNgachLuong
        {
            get
            {
                return _NhomNgachLuong;
            }
            set
            {
                SetPropertyValue("NhomNgachLuong", ref _NhomNgachLuong, value);
            }
        }

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField("",DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tên cấp bậc")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNgachLuong
        {
            get
            {
                return _TenNgachLuong;
            }
            set
            {
                SetPropertyValue("TenNgachLuong", ref _TenNgachLuong, value);
            }
        }

        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Tên cấp bậc")]
        public string Caption
        {
            get
            {
                return ObjectFormatter.Format("{MaQuanLy} - {TenNgachLuong}", this);
            }
        }

        [ModelDefault("Caption", "Thời gian nâng bậc (tháng)")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int ThoiGianNangBac
        {
            get
            {
                return _ThoiGianNangBac;
            }
            set
            {
                SetPropertyValue("ThoiGianNangBac", ref _ThoiGianNangBac, value);
            }
        }

        [ModelDefault("Caption", "Tột khung")]
        [DataSourceProperty("ListBacLuong")]
        public BacLuong TotKhung
        {
            get
            {
                return _TotKhung;
            }
            set
            {
                SetPropertyValue("TotKhung", ref _TotKhung, value);
            }
        }

        [ModelDefault("Caption", "Cấp bậc")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal CapBac
        {
            get
            {
                return _CapBac;
            }
            set
            {
                SetPropertyValue("CapBac", ref _CapBac, value);
            }
        }

        [ModelDefault("Caption", "Công ty")]
        [RuleRequiredField(DefaultContexts.Save)]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
            }
        }

        [Aggregated]
        [Association("NgachLuong-ListBacLuong")]
        [ModelDefault("Caption", "Danh sách bậc")]
        public XPCollection<BacLuong> ListBacLuong
        {
            get
            {
                return GetCollection<BacLuong>("ListBacLuong");
            }
        }

        public NgachLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
        }
    }
}
