using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenChucVuDoan")]
    [ModelDefault("Caption", "Chức vụ Đoàn")]
    public class ChucVuDoan : BaseObject
    {
        private decimal _HSPCChucVuDoan;
        private string _MaQuanLy;
        private string _TenChucVuDoan;
        private bool _LaQuanLy;

        [ModelDefault("Caption", "Mã Quản Lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên Chức Vụ Đoàn")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChucVuDoan
        {
            get
            {
                return _TenChucVuDoan;
            }
            set
            {
                SetPropertyValue("TenChucVuDoan", ref _TenChucVuDoan, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "HSPC chức vụ")]
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

        [ModelDefault("Caption", "Là quản lý")]
        public bool LaQuanLy
        {
            get
            {
                return _LaQuanLy;
            }
            set
            {
                SetPropertyValue("LaQuanLy", ref _LaQuanLy, value);
            }
        }

        public ChucVuDoan(Session session) : base(session) { }
    }

}
