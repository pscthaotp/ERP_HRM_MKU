using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.DanhMuc.TienLuong
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenBacLuong")]
    [ModelDefault("Caption", "Bậc")]
    [RuleCombinationOfPropertiesIsUnique("BacLuong.Unique", DefaultContexts.Save, "NgachLuong;MaQuanLy;CongTy")]
    public class BacLuong : BaseObject
    {
        private NgachLuong _NgachLuong;
        private string _MaQuanLy;
        private string _TenBacLuong;
        private bool _BacLuongCu;
        private decimal _LuongCoBan;
        private decimal _LuongKinhDoanh;
        private decimal _LuongGop;
        private CongTy _CongTy;

        [RuleRequiredField(DefaultContexts.Save)]
        [Association("NgachLuong-ListBacLuong")]
        [ModelDefault("Caption", "Ngạch lương")]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Mã quản lý")]
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

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Tên bậc")]
        public string TenBacLuong
        {
            get
            {
                return _TenBacLuong;
            }
            set
            {
                SetPropertyValue("TenBacLuong", ref _TenBacLuong, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Lương chức danh")]
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

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Lương bổ sung(HQCV)")]
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

        [ModelDefault("Caption", "Bậc cũ")]
        public bool BacLuongCu
        {
            get
            {
                return _BacLuongCu;
            }
            set
            {
                SetPropertyValue("BacLuongCu", ref _BacLuongCu, value);
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

        public BacLuong(Session session) : base(session) { }
    }

}
