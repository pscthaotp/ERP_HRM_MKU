using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.PMS.DanhMuc;
using ERP.Module.PMS.Enum;


namespace ERP.Module.PMS.CauHinh.HeSo
{
    [ModelDefault("Caption", "Hệ số ngành học")]
    [DefaultProperty("Caption")]
    public class HeSoNganhHoc : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoNganhHoc")]
        public QuanLyHeSo QuanLyHeSo
        {
            get
            {
                return _QuanLyHeSo;
            }
            set
            {
                SetPropertyValue("QuanLyHeSo", ref _QuanLyHeSo, value);
            }
        }
        private NganhHoc _NganhHoc;
        private KhoiNganh _KhoiNganh; 
        private decimal _HeSo_NganhHoc;
        private decimal _HeHeSo_NganhHoc_NgoaiDL;
        private bool _MacDinh;
        private LoaiGiangVienEnum? _LoaiGiangVien; 

        
        [ModelDefault("Caption", "Ngành học")]
        public NganhHoc NganhHoc
        {
            get { return _NganhHoc; }
            set { SetPropertyValue("NganhHoc", ref _NganhHoc, value); }
        }

        [ModelDefault("Caption", "Khối ngành")]
        public KhoiNganh KhoiNganh
        {
            get { return _KhoiNganh; }
            set { SetPropertyValue("KhoiNganh", ref _KhoiNganh, value); }
        }

        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSo_NganhHoc", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo_NganhHoc
        {
            get { return _HeSo_NganhHoc; }
            set { SetPropertyValue("HeSo_NganhHoc", ref _HeSo_NganhHoc, value); }
        }

        [ModelDefault("Caption", "Hệ số ngoài đà lạt")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSo_NganhHoc_NgoaiDL", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeHeSo_NganhHoc_NgoaiDL
        {
            get { return _HeHeSo_NganhHoc_NgoaiDL; }
            set { SetPropertyValue("HeHeSo_NganhHoc_NgoaiDL", ref _HeHeSo_NganhHoc_NgoaiDL, value); }
        }

        [ModelDefault("Caption", "Mặc định")]
        public bool MacDinh
        {
            get { return _MacDinh; }
            set { SetPropertyValue("MacDinh", ref _MacDinh, value); }
        }

        [ModelDefault("Caption", "Loại giảng viên")]
        public LoaiGiangVienEnum? LoaiGiangVien
        {
            get { return _LoaiGiangVien; }
            set { SetPropertyValue("LoaiGiangVien", ref _LoaiGiangVien, value); }
        }
        public HeSoNganhHoc(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}