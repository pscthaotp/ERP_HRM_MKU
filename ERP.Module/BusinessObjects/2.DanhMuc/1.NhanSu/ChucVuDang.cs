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
    [DefaultProperty("TenChucVuDang")]
    [ModelDefault("Caption", "Chức vụ Đảng")]
    public class ChucVuDang : BaseObject
    {
        private decimal _HSPCChucVuDang;
        private string _MaQuanLy;
        private string _TenChucVuDang;
        private bool _LaQuanLy;

        [ModelDefault("Caption", "Mã quản lý")]
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

        [ModelDefault("Caption", "Tên chức vụ Đảng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChucVuDang
        {
            get
            {
                return _TenChucVuDang;
            }
            set
            {
                SetPropertyValue("TenChucVuDang", ref _TenChucVuDang, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "HSPC chức vụ")]
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
        public ChucVuDang(Session session) : base(session) { }
    }

}
