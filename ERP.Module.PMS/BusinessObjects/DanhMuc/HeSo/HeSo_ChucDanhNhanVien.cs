using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;

using ERP.Module.NghiepVu.NhanSu.NhanViens;


namespace ERP.Module.PMS.CauHinh.HeSo
{

    [ModelDefault("Caption", "Hệ số chức danh nhân viên")]
    [DefaultProperty("NhanVien")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "QuanLyHeSo;NhanVien", "Hệ số cho nhân viên đã tồn tại.")]
    public class HeSo_ChucDanhNhanVien : BaseObject
    {
        #region  key
        private QuanLyHeSo _QuanLyHeSo;
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSo_ChucDanhNhanVien")]
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
        #endregion

        private NhanVien _NhanVien;
        private decimal _HeSo_ChucDanh;
        private decimal _HeSo_ChucDanhMoi;


        [ModelDefault("Caption", "Nhân viên")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSoChucDanh_NhanVien", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo_ChucDanh
        {
            get { return _HeSo_ChucDanh; }
            set { SetPropertyValue("HeSo_ChucDanh", ref _HeSo_ChucDanh, value); }
        }


        public HeSo_ChucDanhNhanVien(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}