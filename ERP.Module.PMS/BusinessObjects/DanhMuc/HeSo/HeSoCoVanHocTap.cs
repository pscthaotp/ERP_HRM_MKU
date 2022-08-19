using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.PMS.DanhMuc;

namespace ERP.Module.PMS.CauHinh.HeSo
{

    [ModelDefault("Caption", "Hệ số lớp đông")]
    [DefaultProperty("NhomMonHoc")]
    public class HeSoCoVanHocTap : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoCoVanHocTap")]
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
        private int _TuKhoan;
        private int _DenKhoan;
        private decimal _HeSo_CoVanHocTap;
        private bool _MacDinh;
    
        [ModelDefault("Caption", "Từ khoản")]
        public int TuKhoan
        {
            get { return _TuKhoan; }
            set { SetPropertyValue("TuKhoan", ref _TuKhoan, value); }
        }

        [ModelDefault("Caption", "Đến khoản")]
        public int DenKhoan
        {
            get { return _DenKhoan; }
            set { SetPropertyValue("DenKhoan", ref _DenKhoan, value); }
        }

        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSoCoVanHocTap", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo_CoVanHocTap
        {
            get { return _HeSo_CoVanHocTap; }
            set { SetPropertyValue("HeSo_CoVanHocTap", ref _HeSo_CoVanHocTap, value); }
        }

        [ModelDefault("Caption", "Mặc định")]
        public bool MacDinh
        {
            get { return _MacDinh; }
            set { SetPropertyValue("MacDinh", ref _MacDinh, value); }
        }

        public HeSoCoVanHocTap(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            TuKhoan = 1;
            DenKhoan = 1000;
        }
    }
}